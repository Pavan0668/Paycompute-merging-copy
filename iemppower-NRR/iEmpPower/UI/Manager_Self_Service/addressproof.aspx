<%@ Page Title="Address Proof" Language="C#" MasterPageFile="~/Site.master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="addressproof.aspx.cs"
    Inherits="UI_Manager_Self_Service_addressproof" Culture="auto" UICulture="auto" Theme="SkinFile" %>



<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        .MainContent_CrystalReportViewer1__UI
        {
            width: 95% !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <script src="../../Utilities/ValidationMessages.js" type="text/javascript"></script>
    <script src="../../Utilities/Validations.js" type="text/javascript"></script>

    <script type="text/javascript">
        function setDirty() {
            document.body.onbeforeunload = showMessage;
            //debugger;      
            document.getElementById("DirtyLabel").className = "show";
        }
        function clearDirty() {
            document.body.onbeforeunload = "";
            document.getElementById("DirtyLabel").className = "hide";
        }

        function showMessage() {
            return "If you click OK, the changes you have made will be lost."
        }
        function setControlChange() {
            if (typeof (event.srcElement) != 'undefined') {
                event.srcElement.onchange = setDirty;
            }
        }

        document.body.onclick = setControlChange;
        document.body.onkeyup = setControlChange;
    </script>
    <div id="DirtyLabel" style="color: Red;" class="hide"></div>
    <div class="header">
        <div class="row clearfix">
            <div class="col-xs-12 col-sm-6">

                <span class="HeadFontSize">&nbsp;Address proof</span><br />

            </div>
        </div>

        <asp:Label ID="lblMessageBoard" runat="server" CssClass="lblMsg" meta:resourcekey="lblMessageBoardResource1"></asp:Label>
    </div>

    <div class="body">
        <div id="real_time_chart" class="dashboard-flot-chart">

            <div id="divbrdr" class="divfr">
                <div class="search-section">
                    <div>

                         <div class="form-inline" style="overflow: auto; width: 100%">
                            <div class="form-group">
                                <div class=" col-md-1 htCr" style="width: 125px">
                                    Address type&nbsp;<b>:</b>
                                </div>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="drpdwnAddressType" runat="server" CssClass="txtDropDownwidth" TabIndex="1"
                                       
                                        meta:resourcekey="drpdwnAddressTypeResource1">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-1" style="width:103px;">
                                    <asp:Button ID="btnAddrssProof" runat="server" Width="98px" Text="Generate" TabIndex="2" OnClientClick="clearDirty();"
                                        OnClick="btnAddrssProof_Click" meta:resourcekey="btnAddrssProofResource1" />
                                </div>
                                <div class="col-md-1" style="width:103px;">
                                    <asp:Button ID="btnSave" runat="server" Width="98px" TabIndex="3" Text="Save" OnClick="btnSave_Click" OnClientClick="clearDirty();"
                                        meta:resourcekey="btnSaveResource1" />
                                </div>
                                <div class="col-md-1">
                                    <asp:Button ID="btnExit" runat="server" Width="98px" TabIndex="4" OnClick="btnExit_Click" Text="Exit"/>
                                </div>
                            </div>
                        </div>
                        <%--<table>
                            <tr>
                                <td>&nbsp;Address type&nbsp;</td>
                                <td>&nbsp;:&nbsp;</td>
                                <td>
                                    <asp:DropDownList ID="drpdwnAddressType" runat="server" CssClass="textbox" TabIndex="1"
                                        onfocus="this.style.backgroundColor='lightgrey'" onblur="this.style.backgroundColor='white'"
                                        meta:resourcekey="drpdwnAddressTypeResource1">
                                    </asp:DropDownList><br />
                                </td>

                                <td>&nbsp;&nbsp;&nbsp;</td>
                                <td>
                                    <asp:Button ID="btnAddrssProof" runat="server" Text="Generate address proof" TabIndex="2" OnClientClick="clearDirty();"
                                        OnClick="btnAddrssProof_Click" meta:resourcekey="btnAddrssProofResource1" /></td>
                                <td>&nbsp;</td>
                                <td>
                                    <asp:Button ID="btnSave" runat="server" TabIndex="3" Text="Save as PDF" OnClick="btnSave_Click" OnClientClick="clearDirty();"
                                        meta:resourcekey="btnSaveResource1" /></td>
                                <td>&nbsp;</td>
                                <td>
                                    <asp:Button ID="btnExit" runat="server" TabIndex="4" OnClick="btnExit_Click" Text="Exit" /></td>
                            </tr>
                        </table>--%>
                        <%-- <asp:Label ID="lblAddressType" runat="server" Text="Address type" 
        CssClass="label" meta:resourcekey="lblAddressTypeResource1"></asp:Label>
    <asp:DropDownList ID="drpdwnAddressType" runat="server" CssClass="textbox" 
        meta:resourcekey="drpdwnAddressTypeResource1">
    </asp:DropDownList><br />
    <div class="buttonrow">
    <asp:Button ID="btnAddrssProof" runat="server" Text="Generate address proof" OnClientClick ="clearDirty();"
        onclick="btnAddrssProof_Click" 
        meta:resourcekey="btnAddrssProofResource1" />
        <asp:Button ID="btnSave" runat="server" 
        Text="Save as PDF" onclick="btnSave_Click" OnClientClick ="clearDirty();"
        meta:resourcekey="btnSaveResource1" />
        <asp:Button ID="btnExit" runat="server" 
            onclick="btnExit_Click" Text="Exit" />--%>
                        <%-- </div>--%>
                    </div>

                    <div class="DivSpacer01"></div>
                    <div class="DivSpacer01"></div>
                    <div class="respovrflw">
                        <%--  <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" 
            AutoDataBind="True" DisplayGroupTree="False" DisplayToolbar="False" 
            EnableDatabaseLogonPrompt="False" 
             Height="50px" 
            Width="350px" meta:resourcekey="CrystalReportViewer1Resource1"  />--%>

                        <CR:CrystalReportViewer runat="server" ID="CrystalReportViewer1" AutoDataBind="true" DisplayToolbar="False" CssClass="MainContent_CrystalReportViewer1__UI"
                            Height="50px" Width="350px" EnableDatabaseLogonPrompt="False" EnableParameterPrompt="False"
                            ToolPanelView="None" HasToggleGroupTreeButton="false" HasToggleParameterPanelButton="false" />
                    </div>

                </div>
            </div>
        </div>
    </div>
</asp:Content>

