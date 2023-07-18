<%@ Page Title="" Language="C#" MasterPageFile="~/UI/SubSiteMaster.master" AutoEventWireup="true" CodeBehind="IT_IncomeOtherSources.aspx.cs"
    Inherits="iEmpPower.UI.IT.IT_IncomeOtherSources" Theme="SkinFile" EnableEventValidation="false" Culture="en-GB"
    MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<%--<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .TblCls {
            border-collapse: collapse;
        }

        .Td01 {
            /*color: #004080;*/
            font-size: 13px;
            width: 165px;
            height: 28px;
            padding: 3px;
            text-align: justify !important;
        }

        .Td02 {
            /*color: #004080;*/
            font-size: 13px;
            width: 10px;
            height: 28px;
            padding: 8px;
            text-align: center;
            line-height: 12px !important;
        }

        .Td03 {
            width: 235px;
            height: 28px;
            padding: 3px;
        }

        .Td1 {
            /*color: #004080;*/
            font-size: 14px;
            height: 28px;
            padding: 3px;
            text-align: justify !important;
        }
       
    </style>
</asp:Content>--%>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <asp:Label ID="LblLockSts" runat="server" CssClass="msgboard"></asp:Label>
    <div id="DivOthers" runat="server">

    <div id="Div_Proptyp" runat="server">
        <h2>Income from Other Sources </h2>
        <asp:DropDownList ID="DDl_TYPE" runat="server" Font-Size="12px" CssClass="textbox" OnSelectedIndexChanged="DDl_TYPE_SelectedIndexChanged" AutoPostBack="true" TabIndex="1">
            <asp:ListItem Text="SELECT" Value="0" Selected="True"></asp:ListItem>
            <asp:ListItem Text="House Property" Value="1"></asp:ListItem>
            <asp:ListItem Text="Other Sources" Value="2"></asp:ListItem>
        </asp:DropDownList>

    </div>


    <div id="DIVINCOMETYP" runat="server">
        <asp:MultiView ID="MV_IncomeSources" runat="server" >
            <asp:View ID="ViewHousing" runat="server">
                <h2>Income from House Property :  From April 1<sup>st</sup>
                    <asp:Label ID="LblFromDate" runat="server"></asp:Label>
                    To March 31<sup>st</sup>
                    <asp:Label ID="LblToDate" runat="server"></asp:Label></h2>
                   <asp:Label ID="lblMessageBoard" runat="server" CssClass="msgboard"></asp:Label>
                <div>
                    <table class="TblCls">

                        <tr>
                            <td class="Td1" colspan="3" style="font-size: 16px;">Property Type </td>
                        </tr>
                        <tr>
                            <td class="Td1">
                                <asp:RadioButtonList ID="RB_PropTyp" runat="server" Font-Size="14px" OnSelectedIndexChanged="RB_PropTyp_SelectedIndexChanged" AutoPostBack="true" TabIndex="2">
                                    <asp:ListItem Value="1" Text="Self-Occupied/Deemed Self Occupied House Property" Selected="True"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="Partly Let Out House Property"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="Wholly Let Out House Property"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <%-- <tr>
                            <td class="Td1"  colspan="4"><asp:RadioButton id="RB_Self" runat="server" Text="Self-Occupied/Deemed Self Occupied House Property"/></td>
                        </tr>
                        <tr>
                            <td class="Td1" colspan="4"><asp:RadioButton id="RB_Partly" runat="server" Text="Partly Let Out House Property"/></td>
                        </tr>
                        <tr>
                            <td class="Td1" colspan="4"><asp:RadioButton id="RB_Wholly" runat="server" Text="Wholly Let Out House Property"/></td>
                        </tr>--%>
                    </table>
                </div>


                <div style="width: 55%" id="Div1" runat="server" visible="true">
                    <fieldset>
                        <legend style="font-size: 17px;"><b>&nbsp;Deduction Details&nbsp;</b></legend>
                        <table class="TblCls" id="Tab1" runat="server">

                            <tr>
                                <td class="Td01">Deduce - Interest u/s 24
                                </td>
                                <td class="Td02">
                                    <b>:</b></td>
                                <td class="Td03">
                                    <asp:TextBox ID="TXT_DedIntr" runat="server" CssClass="textbox" TabIndex="3"></asp:TextBox>&nbsp;INR
                                            
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="RFV_TXT_DedIntr" runat="server" ErrorMessage="Please Enter Ded. Interest u/s 24"
                                        ForeColor="Red" ControlToValidate="TXT_DedIntr" ValidationGroup="VG1"></asp:RequiredFieldValidator>
                                    <cc1:FilteredTextBoxExtender ID="FTB_TXT_DedIntr" runat="server" FilterMode="ValidChars" FilterType="Custom,Numbers"
                                        TargetControlID="TXT_DedIntr" ValidChars=".">
                                    </cc1:FilteredTextBoxExtender>

                                    <asp:RegularExpressionValidator ID="REVTXT_DedIntrt" runat="server" ControlToValidate="TXT_DedIntr"
                                        ErrorMessage="Must be greater than 0" ForeColor="Red" ValidationExpression="^[1-9][0-9]*(\.[0-9]+)?|0+\.[0-9]*[1-9][0-9]*$"
                                        ValidationGroup="VG1"></asp:RegularExpressionValidator>
                                </td>


                            </tr>
                        </table>

                    </fieldset>

                </div>

                <div style="width: 80%" id="Div2" runat="server" visible="false">
                    <fieldset>
                        <legend style="font-size: 17px;"><b>&nbsp;Deduction Details&nbsp;</b></legend>
                        <table class="TblCls" id="Table1" runat="server" style="float: left; width: 50%">

                            <tr>
                                <td class="Td01">Final Lettable Value
                                </td>
                                <td class="Td02">
                                    <b>:</b></td>
                                <td class="Td03">
                                    <asp:TextBox ID="TXT_FNLLETVALUE" runat="server" CssClass="textbox" OnTextChanged="TXT_FNLLETVALUE_TextChanged" AutoPostBack="true" TabIndex="4"></asp:TextBox>&nbsp;INR
                                   <cc1:FilteredTextBoxExtender ID="FTB_TXT_FNLLETVALUE" runat="server" FilterMode="ValidChars" FilterType="Custom,Numbers"
                                       TargetControlID="TXT_FNLLETVALUE" ValidChars=".">
                                   </cc1:FilteredTextBoxExtender>

                                </td>
                                <%--<td>
                                <asp:RequiredFieldValidator ID="RFV_TXT_FNLLETVALUE" runat="server" ErrorMessage="Please Enter Final Lettable Value"
                                    ForeColor="Red" ControlToValidate="TXT_FNLLETVALUE" ValidationGroup="VG1"></asp:RequiredFieldValidator>
                             

                                <asp:RegularExpressionValidator ID="REV_TXT_FNLLETVALUE" runat="server" ControlToValidate="TXT_FNLLETVALUE"
                                    ErrorMessage="Must be greater than 0" ForeColor="Red" ValidationExpression="^[1-9][0-9]*(\.[0-9]+)?|0+\.[0-9]*[1-9][0-9]*$"
                                    ValidationGroup="VG1"></asp:RegularExpressionValidator>
                            </td>--%>
                            </tr>
                            <%-- <tr>
                            <td class="Td01">Deduce - Repair u/s 24
                            </td>
                            <td class="Td02">
                                <b>:</b></td>
                            <td class="Td03">
                                <asp:Label ID="LBL_DedRepair" runat="server" Text="0"></asp:Label>&nbsp;INR
                                            
                            </td>
                             
                        </tr>--%>
                            <tr>
                                <td class="Td01">Deduce - Interest u/s 24
                                </td>
                                <td class="Td02">
                                    <b>:</b></td>
                                <td class="Td03">
                                    <asp:TextBox ID="TXT_DEDINT" runat="server" CssClass="textbox" TabIndex="5"></asp:TextBox>&nbsp;INR
                                 <cc1:FilteredTextBoxExtender ID="FTB_TXT_DEDINT" runat="server" FilterMode="ValidChars" FilterType="Custom,Numbers"
                                     TargetControlID="TXT_DEDINT" ValidChars=".">
                                 </cc1:FilteredTextBoxExtender>

                                </td>
                                <%--<td>
                                <asp:RequiredFieldValidator ID="RFV_TXT_DEDINT" runat="server" ErrorMessage="Ded. - Interest u/s 24"
                                    ForeColor="Red" ControlToValidate="TXT_DEDINT" ValidationGroup="VG1"></asp:RequiredFieldValidator>
                               

                                <asp:RegularExpressionValidator ID="REV_TXT_DEDINT" runat="server" ControlToValidate="TXT_DEDINT"
                                    ErrorMessage="Must be greater than 0" ForeColor="Red" ValidationExpression="^[1-9][0-9]*(\.[0-9]+)?|0+\.[0-9]*[1-9][0-9]*$"
                                    ValidationGroup="VG1"></asp:RegularExpressionValidator>
                            </td>--%>
                            </tr>
                            <%--  <tr>
                            <td class="Td01">Deduce - Others u/s 24
                            </td>
                            <td class="Td02">
                                <b>:</b></td>
                            <td class="Td03">
                                <asp:TextBox ID="TXTDECOTHERS" runat="server" CssClass="textbox"></asp:TextBox>&nbsp;INR
                                            
                            </td>
                          <td>
                                <asp:RequiredFieldValidator ID="RFV_TXTDECOTHERS" runat="server" ErrorMessage="Deduce - Others u/s 24"
                                    ForeColor="Red" ControlToValidate="TXTDECOTHERS" ValidationGroup="VG1"></asp:RequiredFieldValidator>
                                <cc1:FilteredTextBoxExtender ID="FTB_TXTDECOTHERS" runat="server" FilterMode="ValidChars" FilterType="Custom,Numbers"
                                    TargetControlID="TXTDECOTHERS" ValidChars=".">
                                </cc1:FilteredTextBoxExtender>

                                <asp:RegularExpressionValidator ID="REV_TXTDECOTHERS" runat="server" ControlToValidate="TXTDECOTHERS"
                                    ErrorMessage="Must be greater than 0" ForeColor="Red" ValidationExpression="^[1-9][0-9]*(\.[0-9]+)?|0+\.[0-9]*[1-9][0-9]*$"
                                    ValidationGroup="VG1"></asp:RegularExpressionValidator>
                            </td>


                        </tr>--%>
                            <tr>
                                <td class="Td01">TDS On Other Income
                                </td>
                                <td class="Td02">
                                    <b>:</b></td>
                                <td class="Td03">
                                    <asp:TextBox ID="TXTTDS" runat="server" CssClass="textbox" TabIndex="7"></asp:TextBox>&nbsp;INR
                                  <cc1:FilteredTextBoxExtender ID="FTB_TXTTDS" runat="server" FilterMode="ValidChars" FilterType="Custom,Numbers"
                                      TargetControlID="TXTTDS" ValidChars=".">
                                  </cc1:FilteredTextBoxExtender>


                                </td>
                                <%--<td>
                                <asp:RequiredFieldValidator ID="RFV_TXTTDS" runat="server" ErrorMessage="Please Enter TDS On Other Income"
                                    ForeColor="Red" ControlToValidate="TXTTDS" ValidationGroup="VG1"></asp:RequiredFieldValidator>
                              
                                <asp:RegularExpressionValidator ID="REV_TXTTDS" runat="server" ControlToValidate="TXTTDS"
                                    ErrorMessage="Must be greater than 0" ForeColor="Red" ValidationExpression="^[1-9][0-9]*(\.[0-9]+)?|0+\.[0-9]*[1-9][0-9]*$"
                                    ValidationGroup="VG1"></asp:RegularExpressionValidator>
                            </td>--%>
                            </tr>
                        </table>
                        <table class="TblCls" id="Table2" runat="server" style="float: left; width: 50%">

                            <%--<tr>
                            <td class="Td01">Final Lettable Value
                            </td>
                            <td class="Td02">
                                <b>:</b></td>
                            <td class="Td03">
                                <asp:TextBox ID="TextBox1" runat="server" CssClass="textbox"></asp:TextBox>&nbsp;INR
                                            
                            </td>--%>
                            <%--<td>
                                <asp:RequiredFieldValidator ID="RFV_TXT_FNLLETVALUE" runat="server" ErrorMessage="Please Enter Final Lettable Value"
                                    ForeColor="Red" ControlToValidate="TXT_FNLLETVALUE" ValidationGroup="VG1"></asp:RequiredFieldValidator>
                                <cc1:FilteredTextBoxExtender ID="FTB_TXT_FNLLETVALUE" runat="server" FilterMode="ValidChars" FilterType="Custom,Numbers"
                                    TargetControlID="TXT_FNLLETVALUE" ValidChars=".">
                                </cc1:FilteredTextBoxExtender>

                                <asp:RegularExpressionValidator ID="REV_TXT_FNLLETVALUE" runat="server" ControlToValidate="TXT_FNLLETVALUE"
                                    ErrorMessage="Must be greater than 0" ForeColor="Red" ValidationExpression="^[1-9][0-9]*(\.[0-9]+)?|0+\.[0-9]*[1-9][0-9]*$"
                                    ValidationGroup="VG1"></asp:RegularExpressionValidator>
                            </td>--%>


                            <%--</tr>--%>
                            <tr>
                                <td class="Td01">Deduce - Repair u/s 24
                                </td>
                                <td class="Td02">
                                    <b>:</b></td>
                                <td class="Td03">
                                    <asp:Label ID="Lbl_DEDREPAIR" runat="server" Text="0"></asp:Label>&nbsp;INR
                                            
                                </td>

                            </tr>
                            <%-- <tr>
                            <td class="Td01">Deduce - Interest u/s 24
                            </td>
                            <td class="Td02">
                                <b>:</b></td>
                            <td class="Td03">
                                <asp:TextBox ID="TextBox2" runat="server" CssClass="textbox"></asp:TextBox>&nbsp;INR
                                            
                            </td>
                           <td>
                                <asp:RequiredFieldValidator ID="RFV_TXT_DEDINT" runat="server" ErrorMessage="Ded. - Interest u/s 24"
                                    ForeColor="Red" ControlToValidate="TXT_DEDINT" ValidationGroup="VG1"></asp:RequiredFieldValidator>
                                <cc1:FilteredTextBoxExtender ID="FTB_TXT_DEDINT" runat="server" FilterMode="ValidChars" FilterType="Custom,Numbers"
                                    TargetControlID="TXT_DEDINT" ValidChars=".">
                                </cc1:FilteredTextBoxExtender>

                                <asp:RegularExpressionValidator ID="REV_TXT_DEDINT" runat="server" ControlToValidate="TXT_DEDINT"
                                    ErrorMessage="Must be greater than 0" ForeColor="Red" ValidationExpression="^[1-9][0-9]*(\.[0-9]+)?|0+\.[0-9]*[1-9][0-9]*$"
                                    ValidationGroup="VG1"></asp:RegularExpressionValidator>
                            </td>>


                        </tr>--%>
                            <tr>
                                <td class="Td01">Deduce - Others u/s 24
                                </td>
                                <td class="Td02">
                                    <b>:</b></td>
                                <td class="Td03">
                                    <asp:TextBox ID="TXTDECOTHERS" runat="server" CssClass="textbox" OnTextChanged="TXTDECOTHERS_TextChanged" AutoPostBack="true" TabIndex="6"></asp:TextBox>&nbsp;INR
                                     <cc1:FilteredTextBoxExtender ID="FTB_TXTDECOTHERS" runat="server" FilterMode="ValidChars" FilterType="Custom,Numbers"
                                         TargetControlID="TXTDECOTHERS" ValidChars=".">
                                     </cc1:FilteredTextBoxExtender>
                                </td>
                                <%-- <td>
                                <asp:RequiredFieldValidator ID="RFV_TXTDECOTHERS" runat="server" ErrorMessage="Deduce - Others u/s 24"
                                    ForeColor="Red" ControlToValidate="TXTDECOTHERS" ValidationGroup="VG1"></asp:RequiredFieldValidator>
                               

                                <asp:RegularExpressionValidator ID="REV_TXTDECOTHERS" runat="server" ControlToValidate="TXTDECOTHERS"
                                    ErrorMessage="Must be greater than 0" ForeColor="Red" ValidationExpression="^[1-9][0-9]*(\.[0-9]+)?|0+\.[0-9]*[1-9][0-9]*$"
                                    ValidationGroup="VG1"></asp:RegularExpressionValidator>
                            </td>--%>
                            </tr>
                            <%-- <tr>
                            <td class="Td01">TDS On Other Income
                            </td>
                            <td class="Td02">
                                <b>:</b></td>
                            <td class="Td03">
                                <asp:TextBox ID="TextBox4" runat="server" CssClass="textbox"></asp:TextBox>&nbsp;INR
                                            
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RFV_TXTTDS" runat="server" ErrorMessage="Please Enter TDS On Other Income"
                                    ForeColor="Red" ControlToValidate="TXTTDS" ValidationGroup="VG1"></asp:RequiredFieldValidator>
                                <cc1:FilteredTextBoxExtender ID="FTB_TXTTDS" runat="server" FilterMode="ValidChars" FilterType="Custom,Numbers"
                                    TargetControlID="TXTTDS" ValidChars=".">
                                </cc1:FilteredTextBoxExtender>

                                <asp:RegularExpressionValidator ID="REV_TXTTDS" runat="server" ControlToValidate="TXTTDS"
                                    ErrorMessage="Must be greater than 0" ForeColor="Red" ValidationExpression="^[1-9][0-9]*(\.[0-9]+)?|0+\.[0-9]*[1-9][0-9]*$"
                                    ValidationGroup="VG1"></asp:RegularExpressionValidator>
                            </td>


                        </tr>--%>
                        </table>
                    </fieldset>

                </div>

                     <div style="width: 55%" runat="server" id="DIVCOMMENTS">
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
                            <td>
                                
                            </td>

                        </tr>

                    </table>
                    </fieldset></div>

              <%--  <asp:Button id="BTN_SubmitView1" runat="server" Text="Submit" ValidationGroup="VG1" CausesValidation="true" OnClick="BTN_SubmitView1_Click"/>--%>

            </asp:View>
            <asp:View ID="ViewOtherSources" runat="server">
                <h2>Income from Other Sources :  From April 1<sup>st</sup>
                    <asp:Label ID="LblFrom" runat="server"></asp:Label>
                    To March 31<sup>st</sup>
                    <asp:Label ID="LblTo" runat="server"></asp:Label></h2>
                   <asp:Label ID="LblMsg" runat="server" CssClass="msgboard"></asp:Label>
                <div>
                    <table class="TblCls">



                        <tr>
                            <td class="Td02">1 </td>
                            <td class="Td01">Business Profits
                            </td>
                            <td class="Td02">
                                <b>:</b></td>
                            <td class="Td03">
                                <asp:TextBox ID="TXT_BusnProfits" runat="server" CssClass="textbox" TabIndex="9"></asp:TextBox>&nbsp;INR
                                            
                            </td>
                            <td>

                                <cc1:FilteredTextBoxExtender ID="FTB_TXT_BusnProfits" runat="server" FilterMode="ValidChars" FilterType="Custom,Numbers"
                                    TargetControlID="TXT_BusnProfits" ValidChars=".">
                                </cc1:FilteredTextBoxExtender>
                               <%-- <asp:RequiredFieldValidator ID="RFV_TXT_BusnProfits" runat="server" ErrorMessage="Please Enter Business Profits"
                                    ForeColor="Red" ControlToValidate="TXT_BusnProfits" ValidationGroup="VG1"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="REVTXT_BusnProfits" runat="server" ControlToValidate="TXT_BusnProfits"
                                    ErrorMessage="Must be greater than 0" ForeColor="Red" ValidationExpression="^[1-9][0-9]*(\.[0-9]+)?|0+\.[0-9]*[1-9][0-9]*$"
                                    ValidationGroup="VG1"></asp:RegularExpressionValidator>--%>
                            </td>

                        </tr>

                         <tr>
                            <td class="Td02">2</td>
                            <td class="Td01">Long-Term Capital Gains
                            </td>
                            <td class="Td02">
                                <b>:</b></td>
                            <td class="Td03" style="width:350px">
                                <asp:TextBox ID="TXT_LTCG" runat="server" CssClass="textbox" TabIndex="10"></asp:TextBox>&nbsp;INR&nbsp;&nbsp;(Normal Rate)
                                            
                            </td>
                            <td>

                                <cc1:FilteredTextBoxExtender ID="FTB_TXT_LTCG" runat="server" FilterMode="ValidChars" FilterType="Custom,Numbers"
                                    TargetControlID="TXT_LTCG" ValidChars=".">
                                </cc1:FilteredTextBoxExtender>
                               <%-- <asp:RequiredFieldValidator ID="RFV_TXT_BusnProfits" runat="server" ErrorMessage="Please Enter Business Profits"
                                    ForeColor="Red" ControlToValidate="TXT_BusnProfits" ValidationGroup="VG1"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="REVTXT_BusnProfits" runat="server" ControlToValidate="TXT_BusnProfits"
                                    ErrorMessage="Must be greater than 0" ForeColor="Red" ValidationExpression="^[1-9][0-9]*(\.[0-9]+)?|0+\.[0-9]*[1-9][0-9]*$"
                                    ValidationGroup="VG1"></asp:RegularExpressionValidator>--%>
                            </td>

                        </tr>

                        <tr>
                            <td class="Td02">3</td>
                            <td class="Td01">Long-Term Capital Gains
                            </td>
                            <td class="Td02">
                                <b>:</b></td>
                            <td class="Td03" style="width:350px">
                                <asp:TextBox ID="TXT_LTCGS" runat="server" CssClass="textbox" TabIndex="11"></asp:TextBox>&nbsp;INR&nbsp;&nbsp;(Special Rate)
                                            
                            </td>
                            <td>

                                <cc1:FilteredTextBoxExtender ID="FTB_TXT_LTCGS" runat="server" FilterMode="ValidChars" FilterType="Custom,Numbers"
                                    TargetControlID="TXT_LTCGS" ValidChars=".">
                                </cc1:FilteredTextBoxExtender>
                               <%-- <asp:RequiredFieldValidator ID="RFV_TXT_BusnProfits" runat="server" ErrorMessage="Please Enter Business Profits"
                                    ForeColor="Red" ControlToValidate="TXT_BusnProfits" ValidationGroup="VG1"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="REVTXT_BusnProfits" runat="server" ControlToValidate="TXT_BusnProfits"
                                    ErrorMessage="Must be greater than 0" ForeColor="Red" ValidationExpression="^[1-9][0-9]*(\.[0-9]+)?|0+\.[0-9]*[1-9][0-9]*$"
                                    ValidationGroup="VG1"></asp:RegularExpressionValidator>--%>
                            </td>

                        </tr>

                        <tr>
                            <td class="Td02">4</td>
                            <td class="Td01">Short-Term Capital Gains
                            </td>
                            <td class="Td02">
                                <b>:</b></td>
                            <td class="Td03" style="width:350px">
                                <asp:TextBox ID="TXT_STCG" runat="server" CssClass="textbox" TabIndex="12"></asp:TextBox>&nbsp;INR
                                            
                            </td>
                            <td>

                                <cc1:FilteredTextBoxExtender ID="FTB_TXT_STCG" runat="server" FilterMode="ValidChars" FilterType="Custom,Numbers"
                                    TargetControlID="TXT_STCG" ValidChars=".">
                                </cc1:FilteredTextBoxExtender>
                               <%-- <asp:RequiredFieldValidator ID="RFV_TXT_BusnProfits" runat="server" ErrorMessage="Please Enter Business Profits"
                                    ForeColor="Red" ControlToValidate="TXT_BusnProfits" ValidationGroup="VG1"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="REVTXT_BusnProfits" runat="server" ControlToValidate="TXT_BusnProfits"
                                    ErrorMessage="Must be greater than 0" ForeColor="Red" ValidationExpression="^[1-9][0-9]*(\.[0-9]+)?|0+\.[0-9]*[1-9][0-9]*$"
                                    ValidationGroup="VG1"></asp:RegularExpressionValidator>--%>
                            </td>

                        </tr>

                        <tr>
                            <td class="Td02">5</td>
                            <td class="Td01">Short-Term Capital Gains
                            </td>
                            <td class="Td02">
                                <b>:</b></td>
                            <td class="Td03" style="width:350px">
                                <asp:TextBox ID="TXT_STCGLS" runat="server" CssClass="textbox" TabIndex="13"></asp:TextBox>&nbsp;INR&nbsp;&nbsp;(Listed Securities)
                                            
                            </td>
                            <td>

                                <cc1:FilteredTextBoxExtender ID="FTB_TXT_STCGLS" runat="server" FilterMode="ValidChars" FilterType="Custom,Numbers"
                                    TargetControlID="TXT_STCGLS" ValidChars=".">
                                </cc1:FilteredTextBoxExtender>
                               <%-- <asp:RequiredFieldValidator ID="RFV_TXT_BusnProfits" runat="server" ErrorMessage="Please Enter Business Profits"
                                    ForeColor="Red" ControlToValidate="TXT_BusnProfits" ValidationGroup="VG1"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="REVTXT_BusnProfits" runat="server" ControlToValidate="TXT_BusnProfits"
                                    ErrorMessage="Must be greater than 0" ForeColor="Red" ValidationExpression="^[1-9][0-9]*(\.[0-9]+)?|0+\.[0-9]*[1-9][0-9]*$"
                                    ValidationGroup="VG1"></asp:RegularExpressionValidator>--%>
                            </td>

                        </tr>

                        <tr>
                            <td class="Td02">6</td>
                            <td class="Td01">Income From Dividend
                            </td>
                            <td class="Td02">
                                <b>:</b></td>
                            <td class="Td03" style="width:350px">
                                <asp:TextBox ID="TXT_IFD" runat="server" CssClass="textbox" TabIndex="14"></asp:TextBox>&nbsp;INR
                                            
                            </td>
                            <td>

                                <cc1:FilteredTextBoxExtender ID="FTB_TXT_IFD" runat="server" FilterMode="ValidChars" FilterType="Custom,Numbers"
                                    TargetControlID="TXT_IFD" ValidChars=".">
                                </cc1:FilteredTextBoxExtender>
                               <%-- <asp:RequiredFieldValidator ID="RFV_TXT_BusnProfits" runat="server" ErrorMessage="Please Enter Business Profits"
                                    ForeColor="Red" ControlToValidate="TXT_BusnProfits" ValidationGroup="VG1"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="REVTXT_BusnProfits" runat="server" ControlToValidate="TXT_BusnProfits"
                                    ErrorMessage="Must be greater than 0" ForeColor="Red" ValidationExpression="^[1-9][0-9]*(\.[0-9]+)?|0+\.[0-9]*[1-9][0-9]*$"
                                    ValidationGroup="VG1"></asp:RegularExpressionValidator>--%>
                            </td>

                        </tr>

                        <tr>
                            <td class="Td02">7</td>
                            <td class="Td01">Income From Interest
                            </td>
                            <td class="Td02">
                                <b>:</b></td>
                            <td class="Td03" style="width:350px">
                                <asp:TextBox ID="TXT_IFI" runat="server" CssClass="textbox" TabIndex="15"></asp:TextBox>&nbsp;INR
                                            
                            </td>
                            <td>

                                <cc1:FilteredTextBoxExtender ID="FTB_TXT_IFI" runat="server" FilterMode="ValidChars" FilterType="Custom,Numbers"
                                    TargetControlID="TXT_IFI" ValidChars=".">
                                </cc1:FilteredTextBoxExtender>
                               <%-- <asp:RequiredFieldValidator ID="RFV_TXT_BusnProfits" runat="server" ErrorMessage="Please Enter Business Profits"
                                    ForeColor="Red" ControlToValidate="TXT_BusnProfits" ValidationGroup="VG1"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="REVTXT_BusnProfits" runat="server" ControlToValidate="TXT_BusnProfits"
                                    ErrorMessage="Must be greater than 0" ForeColor="Red" ValidationExpression="^[1-9][0-9]*(\.[0-9]+)?|0+\.[0-9]*[1-9][0-9]*$"
                                    ValidationGroup="VG1"></asp:RegularExpressionValidator>--%>
                            </td>

                        </tr>

                        <tr>
                            <td class="Td02">8</td>
                            <td class="Td01">Other Income Unspecified
                            </td>
                            <td class="Td02">
                                <b>:</b></td>
                            <td class="Td03" style="width:350px">
                                <asp:TextBox ID="TXT_OI" runat="server" CssClass="textbox" TabIndex="16"></asp:TextBox>&nbsp;INR
                                            
                            </td>
                            <td>

                                <cc1:FilteredTextBoxExtender ID="FTB_TXT_OI" runat="server" FilterMode="ValidChars" FilterType="Custom,Numbers"
                                    TargetControlID="TXT_OI" ValidChars=".">
                                </cc1:FilteredTextBoxExtender>
                               <%-- <asp:RequiredFieldValidator ID="RFV_TXT_BusnProfits" runat="server" ErrorMessage="Please Enter Business Profits"
                                    ForeColor="Red" ControlToValidate="TXT_BusnProfits" ValidationGroup="VG1"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="REVTXT_BusnProfits" runat="server" ControlToValidate="TXT_BusnProfits"
                                    ErrorMessage="Must be greater than 0" ForeColor="Red" ValidationExpression="^[1-9][0-9]*(\.[0-9]+)?|0+\.[0-9]*[1-9][0-9]*$"
                                    ValidationGroup="VG1"></asp:RegularExpressionValidator>--%>
                            </td>

                        </tr>

                        <tr>
                            <td class="Td02">9</td>
                            <td class="Td01">TDS On Other Income
                            </td>
                            <td class="Td02">
                                <b>:</b></td>
                            <td class="Td03" style="width:350px">
                                <asp:TextBox ID="TXT_TDSI" runat="server" CssClass="textbox" TabIndex="17"></asp:TextBox>&nbsp;INR
                                            
                            </td>
                            <td>

                                <cc1:FilteredTextBoxExtender ID="FTB_TXT_TDSI" runat="server" FilterMode="ValidChars" FilterType="Custom,Numbers"
                                    TargetControlID="TXT_TDSI" ValidChars=".">
                                </cc1:FilteredTextBoxExtender>
                               <%-- <asp:RequiredFieldValidator ID="RFV_TXT_BusnProfits" runat="server" ErrorMessage="Please Enter Business Profits"
                                    ForeColor="Red" ControlToValidate="TXT_BusnProfits" ValidationGroup="VG1"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="REVTXT_BusnProfits" runat="server" ControlToValidate="TXT_BusnProfits"
                                    ErrorMessage="Must be greater than 0" ForeColor="Red" ValidationExpression="^[1-9][0-9]*(\.[0-9]+)?|0+\.[0-9]*[1-9][0-9]*$"
                                    ValidationGroup="VG1"></asp:RegularExpressionValidator>--%>
                            </td>

                        </tr>
                      <%--  <tr><td></td></tr>
                        <tr>
                            <td class="Td02"></td>
                            <td class="Td01">Comments
                            </td>
                            <td class="Td02">
                                <b>:</b></td>
                            <td class="Td03" style="width:350px">
                               <asp:TextBox ID="TXTCOMMENTSView2" runat="server" CssClass="textbox" TextMode="MultiLine"
                                    Columns="9"></asp:TextBox>             
                            </td>
                            

                        </tr>--%>
                    </table>
                </div>
                   <div style="width: 55%" runat="server" id="DIV3">
                <fieldset style="border-color: white; border-style: none;">

                 
             <table class="TblCls">

                        <tr>
                            <td class="Td01">Comments
                            </td>
                            <td class="Td02">
                                <b>:</b></td>
                            <td class="Td03">
                               <asp:TextBox ID="TXTCOMMENTS2" runat="server" CssClass="textbox" TextMode="MultiLine"
                                    Columns="9" TabIndex="18"></asp:TextBox>
                            </td>
                            <td>
                                
                            </td>

                        </tr>

                    </table>
                    </fieldset></div>  
           <%-- <asp:Button id="BTN_SubmitView2" runat="server" Text="Submit" OnClick="BTN_SubmitView2_Click"/>--%>

            </asp:View>
        </asp:MultiView>
    </div>


     <asp:Button id="BTNSubmitHousingOthers" runat="server" Text="Submit" OnClick="BTNSubmitHousingOthers_Click" TabIndex="19"/>
      <asp:Button id="BtnEDIT" runat="server" Text="Edit" OnClick="BtnEDIT_Click" TabIndex="20"/>
         <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdateITHousingOthers_Click"  CausesValidation="true" TabIndex="21" />
         <asp:Button id="BtnCancel" runat="server" Text="Cancel" OnClick="BtnCancel_Click" TabIndex="22"/>


    </div>
</asp:Content>
