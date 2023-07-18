<%@ Page Title="Employment Proof" Language="C#" MasterPageFile="~/Site.master" EnableEventValidation="false"
    AutoEventWireup="true" Inherits="UI_Manager_Self_Service_employmentproof" Culture="auto" meta:resourcekey="PageResource1"
    UICulture="auto" Theme="SkinFile" CodeBehind="employmentproof.aspx.cs" %>



<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

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
    <span class="hidden">
        <asp:Button ID="btnEntryKey" runat="server" Text="" /></span>
    <div id="DirtyLabel" style="color: Red;" class="hide"></div>
    <div class="header">
        <div class="row clearfix">
            <div class="col-xs-12 col-sm-6">

                <span class="HeadFontSize">&nbsp;Employment proof</span><br />

            </div>
        </div>

        <asp:Label ID="lblMessageBoard" runat="server" CssClass="lblMsg" meta:resourcekey="lblMessageBoardResource1"></asp:Label>
    </div>

    <div class="body">
        <div id="real_time_chart" class="dashboard-flot-chart">

            <div id="divbrdr" class="divfr">
                <div class="search-section">
                    <div>
                        <%--<h2>Employment proof</h2>
  <asp:Label ID="lblMessageBoard" runat="server"  CssClass="msgboard" 
        meta:resourcekey="lblMessageBoardResource1"></asp:Label><br />--%>
                        <div class="buttonrow">
                            <asp:Button ID="btnSave" runat="server" TabIndex="1"
                                Text="Save" OnClick="btnSave_Click" OnClientClick="clearDirty();"
                                meta:resourcekey="btnSaveResource1" />
                            <asp:Button ID="btnExit" runat="server" TabIndex="2"
                                OnClick="btnExit_Click" Text="Exit" />
                        </div>
                    </div>
                    <div class="DivSpacer01"></div>
                    <div class="DivSpacer01"></div>

                    <div class="respovrflw">
                        <%--   <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" 
        AutoDataBind="True" DisplayGroupTree="False" DisplayToolbar="False" 
        EnableDatabaseLogonPrompt="False" EnableParameterPrompt="False" 
        Height="50px" meta:resourcekey="CrystalReportViewer1Resource1" Width="350px" />--%>

                        <CR:CrystalReportViewer runat="server" ID="CrystalReportViewer1" AutoDataBind="true" DisplayToolbar="False" CssClass="MainContent_CrystalReportViewer1__UI"
                            Height="50px" Width="350px" EnableDatabaseLogonPrompt="False" EnableParameterPrompt="False" ToolPanelView="None"
                            HasToggleGroupTreeButton="false" HasToggleParameterPanelButton="false" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

