<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" Inherits="iEmpPower.iEmpPower_DT_Wisard_Dataset.UI_Configuration_iEmpPower_DT_Wizard"
    CodeBehind="iEmpPower_DT_Wizard.aspx.cs" EnableEventValidation="false" Culture="en-GB" Theme="SkinFile" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        .btnStyle
        {
            width: 150px;
        }

        .colwdth
        {
            width: 50px;
        }

        .TxtOverHide
        {
            display: block;
            position: relative;
            height: 100%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="DivSpacer01">
        <div class="header">
            <div class="row clearfix">
                <div class="col-xs-12 col-sm-6">

                    <span class="HeadFontSize">&nbsp;DT&nbsp;WIZARD&nbsp;</span><br />

                </div>
            </div>
            <asp:Label ID="LblMsg" runat="server" CssClass="Fl" Font-Size="11px"></asp:Label>
        </div>

        <div class="divfr" id="divbrdr">
            <div class="DivSpacer01">&nbsp;</div>

            <%--<table class="Tblcls">
                    <tr>
                        <td class="Td01a TxtL"><b>1.</b></td>
                        <td class="Td03">Generate Users</td>
                        <td class="Td01"><b>:</b></td>
                        <td class="Td10 Td02 TxtOverHide">
                            <asp:FileUpload ID="FU_GenerateUsers" runat="server" CssClass="Fnt01 Td02 TxtOverHide" />
                        </td>
                        <td class="Td05">
                            <asp:Button ID="BtnGenerateUser" runat="server" Text="Generate Users" ValidationGroup="VgUsers" OnClick="BtnGenerateUser_Click" /></td>
                        <td>
                            <asp:RequiredFieldValidator ID="RFV_FU_GenerateUsers" runat="server" ControlToValidate="FU_GenerateUsers" ValidationGroup="VgUsers" ErrorMessage="&nbsp;Select User XML !" CssClass="Fnt01" ForeColor="Red"></asp:RequiredFieldValidator></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="Td01a TxtL"><b>2.</b></td>
                        <td class="Td03">Import Master data</td>
                        <td class="Td01"><b>:</b></td>
                        <td class="Td10 Td02 TxtOverHide">
                            <asp:FileUpload ID="FU_ImportMasters" runat="server" CssClass="Fnt01 Td02 TxtOverHide" />
                        </td>
                        <td class="Td05">
                            <asp:Button ID="BtnImportMasters" runat="server" Text="Import" ValidationGroup="VgMasters" OnClick="BtnImportMasters_Click" /></td>
                        <td>
                            <asp:RequiredFieldValidator ID="RFV_FU_ImportMasters" runat="server" ControlToValidate="FU_ImportMasters" ValidationGroup="VgMasters" ErrorMessage="&nbsp;Select Masters XML !" CssClass="Fnt01" ForeColor="Red"></asp:RequiredFieldValidator></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="Td01a TxtL"><b>3.</b></td>
                        <td class="Td03">Import Dropdown data</td>
                        <td class="Td01"><b>:</b></td>
                        <td class="Td10 Td02 TxtOverHide">
                            <asp:FileUpload ID="FU_ImportDropDownMasters" runat="server" CssClass="Fnt01 Td02 TxtOverHide" />
                        </td>
                        <td class="Td05">
                            <asp:Button ID="BtnImportDropdownMasters" runat="server" Text="Import" ValidationGroup="VgDropDown" OnClick="BtnImportDropdownMasters_Click" /></td>
                        <td>
                            <asp:RequiredFieldValidator ID="RFV_FU_ImportDropDownMasters" runat="server" ControlToValidate="FU_ImportDropDownMasters" ValidationGroup="VgDropDown" ErrorMessage="&nbsp;Select Dropdown XML !" CssClass="Fnt01" ForeColor="Red"></asp:RequiredFieldValidator></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="Td01a TxtL"><b>4.</b></td>
                        <td class="Td03">Import Organization data</td>
                        <td class="Td01"><b>:</b></td>
                        <td class="Td10 Td02 TxtOverHide">
                            <asp:FileUpload ID="FU_ImportOrganizationMasters" runat="server" CssClass="Fnt01 Td02 TxtOverHide" />
                        </td>
                        <td class="Td05">
                            <asp:Button ID="BtnImportOrgMasters" runat="server" Text="Import" ValidationGroup="VgOrg" OnClick="BtnImportOrgMasters_Click" /></td>
                        <td>
                            <asp:RequiredFieldValidator ID="RFV_FU_ImportOrganizationMasters" runat="server" ControlToValidate="FU_ImportOrganizationMasters" ValidationGroup="VgOrg" ErrorMessage="&nbsp;Select Organization XML !" CssClass="Fnt01" ForeColor="Red"></asp:RequiredFieldValidator></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td colspan="5">&nbsp;</td>
                    </tr>
                    <%-- <tr>
                        <td class="Td01a"><b>3.</b></td>
                        <td class="Td03">Import Dropdown data</td>
                        <td class="Td01"><b>:</b></td>
                        <td></td>
                        <td></td>
                    </tr>--%X>
                </table>--%>

            <div class="form-inline">
                <%-- <table class="Tblcls">--%>
                <div class="form-group">
                    <div class="col-sm-1 colwdth"><b>1.</b></div>
                    <div class="col-sm-3 htCr TxtOverHide" style="width: 185px;">Generate Users&nbsp;<b>:</b></div>
                    <%-- <div class="col-sm-1 colwdth"></div>--%>
                    <div class="col-sm-3">
                        <asp:FileUpload ID="FU_GenerateUsers" runat="server" CssClass="txtDropDownwidth" />
                    </div>
                    <div class="col-sm-1">
                        <asp:Button ID="BtnGenerateUser" runat="server" Text="Generate Users" ValidationGroup="VgUsers" OnClick="BtnGenerateUser_Click" />
                    </div>
                    <div class="col-sm-2">
                        <asp:RequiredFieldValidator ID="RFV_FU_GenerateUsers" runat="server" ControlToValidate="FU_GenerateUsers" ValidationGroup="VgUsers" ErrorMessage="&nbsp;Select User XML !" CssClass="Fnt01" ForeColor="Red"></asp:RequiredFieldValidator>
                    </div>

                </div>
                <div class="form-group">
                    <div class="col-sm-1 colwdth"><b>2.</b></div>
                    <div class="col-sm-3 htCr TxtOverHide" style="width: 185px">Import Master data&nbsp;<b>:</b></div>
                    <%--<div class="col-sm-1 colwdth"><b>:</b></div>--%>
                    <div class="col-sm-3">
                        <asp:FileUpload ID="FU_ImportMasters" runat="server" CssClass="txtDropDownwidth" />
                    </div>
                    <div class="col-sm-1">
                        <asp:Button ID="BtnImportMasters" runat="server" Text="Import" ValidationGroup="VgMasters" OnClick="BtnImportMasters_Click" />
                    </div>
                    <div class="col-sm-2">
                        <asp:RequiredFieldValidator ID="RFV_FU_ImportMasters" runat="server" ControlToValidate="FU_ImportMasters" ValidationGroup="VgMasters" ErrorMessage="&nbsp;Select Masters XML !" CssClass="Fnt01" ForeColor="Red"></asp:RequiredFieldValidator>
                    </div>

                </div>
                <div class="form-group">
                    <div class="col-sm-1 colwdth"><b>3.</b></div>
                    <div class="col-sm-3 htCr TxtOverHide" style="width: 185px">Import Dropdown data&nbsp;<b>:</b></div>
                    <%--<div class="col-sm-1 colwdth"><b>:</b></div>--%>
                    <div class="col-sm-3 ">
                        <asp:FileUpload ID="FU_ImportDropDownMasters" runat="server" CssClass="txtDropDownwidth" />
                    </div>
                    <div class="col-sm-1 ">
                        <asp:Button ID="BtnImportDropdownMasters" runat="server" Text="Import" ValidationGroup="VgDropDown" OnClick="BtnImportDropdownMasters_Click" />
                    </div>
                    <div class="col-sm-2">
                        <asp:RequiredFieldValidator ID="RFV_FU_ImportDropDownMasters" runat="server" ControlToValidate="FU_ImportDropDownMasters" ValidationGroup="VgDropDown" ErrorMessage="&nbsp;Select Dropdown XML !" CssClass="Fnt01" ForeColor="Red"></asp:RequiredFieldValidator>
                    </div>

                </div>
                <div class="form-group">
                    <div class="col-sm-1 colwdth"><b>4.</b></div>
                    <div class="col-sm-3 htCr TxtOverHide" style="width: 185px">Import Organization data&nbsp;<b>:</b></div>
                    <%--<div class="col-sm-1 colwdth"><b>:</b></div>--%>
                    <div class="col-sm-3 ">
                        <asp:FileUpload ID="FU_ImportOrganizationMasters" runat="server" CssClass="txtDropDownwidth" />
                    </div>
                    <div class="col-sm-1 ">
                        <asp:Button ID="BtnImportOrgMasters" runat="server" Text="Import" ValidationGroup="VgOrg" OnClick="BtnImportOrgMasters_Click" />
                    </div>
                    <div class="col-sm-2">
                        <asp:RequiredFieldValidator ID="RFV_FU_ImportOrganizationMasters" runat="server" ControlToValidate="FU_ImportOrganizationMasters" ValidationGroup="VgOrg" ErrorMessage="&nbsp;Select Organization XML !" CssClass="Fnt01" ForeColor="Red"></asp:RequiredFieldValidator>
                    </div>

                </div>
                <%--<tr>
                        <td colspan="5">&nbsp;</td>
                    </tr>
                     <tr>
                        <td class="Td01a"><b>3.</b></td>
                        <td class="Td03">Import Dropdown data</td>
                        <td class="Td01"><b>:</b></td>
                        <td></td>
                        <td></td>
                    </tr>--%>
                <%--</table>--%>
            </div>
        </div>

    </div>
</asp:Content>
