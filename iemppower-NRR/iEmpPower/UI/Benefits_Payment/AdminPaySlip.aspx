<%@ Page Title="Payslip" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdminPaySlip.aspx.cs"
    Inherits="iEmpPower.UI.Benefits_Payment.AdminPaySlip" Culture="auto"
    EnableEventValidation="false" UICulture="en-GB" Theme="SkinFile" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        
           .completionList {
            border: solid 1px #444444;
            margin: 0px;
            padding: 2px;
            height: 100px;
            overflow: auto;
            background-color: #FFFFFF;
            font-size: xx-small;
        }

        .listItem {
            color: #1C1C1C;
        }

        .itemHighlighted {
            background-color: #ffc0c0;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript">
        //function CallPrint(iFrame) {

        //    iFrame.focus();
        //    iFrame.print();
        //    return false;
        //}
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
    <span class="hidden">
        <asp:Button ID="Button1" runat="server" Text="" /></span>
    <div class="header">
        <div class="row clearfix">
            <div class="col-xs-12 col-sm-6">

                <span class="HeadFontSize">&nbsp;Pay Slip</span><br />

            </div>
        </div>

        <asp:Label ID="lblMessageBoard" runat="server" CssClass="lblMsg" meta:resourcekey="lblMessageBoardResource1"></asp:Label>
    </div>

    <div>


        <div class="body">
            <div id="real_time_chart" class="dashboard-flot-chart">

                <div class="divfr" id="divbrdr">
                    <div class="search-section">
                        <div class="form-inline">

                            <div class="form-group">
                                <div class="col-sm-1 htCr">Employee&nbsp;<b>:</b></div>
                                <div class="col-sm-2 ">
                                    <asp:TextBox ID="DDLEmpList" runat="server" CssClass="txtDropDownwidth" TabIndex="1"></asp:TextBox>
                                    <Ajx:AutoCompleteExtender ID="EmpNameAutoCompleteExtender" runat="server" TargetControlID="DDLEmpList" MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="5" CompletionInterval="1" FirstRowSelected="True"
                                        ServiceMethod="GetEmployeeNamesAndId" ServicePath="~/WebService/Service.asmx"
                                        CompletionListCssClass="completionList"
                                        CompletionListHighlightedItemCssClass="itemHighlighted"
                                        CompletionListItemCssClass="listItem">
                                    </Ajx:AutoCompleteExtender>
                                    <%--<asp:DropDownList ID="DDLEmpList" runat="server" CssClass="txtDropDownwidth" TabIndex="1"
                                          onfocus="this.style.backgroundColor='lightgrey'"  onblur="this.style.backgroundColor='white'" > 
                                        </asp:DropDownList>
                                        <Ajx:CascadingDropDown ID="CDD_DDLEmpList" runat="server" Category="EmpList" EmptyText="- SELECT -" EmptyValue="0"
                                            LoadingText="[LOADING Emplyoee List....]" PromptText="- SELECT -" PromptValue="0" TargetControlID="DDLEmpList"
                                            ServicePath="~/WebService/Service.asmx" ServiceMethod="GetEmpList">
                                        </Ajx:CascadingDropDown>--%>

                                </div>

                                <div class="col-sm-1 htCr" style="width:40px;">Year&nbsp;<b>:</b></div>
                                <div class="col-sm-2">
                                    <asp:DropDownList ID="drpdwnYear" runat="server" CssClass="txtDropDownwidth" Width="100px" TabIndex="2">
                                    </asp:DropDownList>
                                    <Ajx:CascadingDropDown ID="CCD_drpdwnYear" runat="server" ServicePath="~/WebService/Service.asmx" ServiceMethod="GetYearPayslip" Category="QYear"
                                        UseContextKey="false" TargetControlID="drpdwnYear">
                                    </Ajx:CascadingDropDown>
                                </div>

                                <div class="col-sm-1 htCr" style="width:50px;">Month&nbsp;<b>:</b></div>
                                <div class="col-sm-2">
                                    <asp:DropDownList ID="drpdwnMonth" runat="server" CssClass="txtDropDownwidth" TabIndex="3"
                                        meta:resourcekey="drpdwnMonthResource1">
                                        <asp:ListItem Value="01" meta:resourcekey="ListItemResource4">January</asp:ListItem>
                                        <asp:ListItem Value="02" meta:resourcekey="ListItemResource5">February</asp:ListItem>
                                        <asp:ListItem Value="03" meta:resourcekey="ListItemResource6">March</asp:ListItem>
                                        <asp:ListItem Value="04" meta:resourcekey="ListItemResource7">April</asp:ListItem>
                                        <asp:ListItem Value="05" meta:resourcekey="ListItemResource8">May</asp:ListItem>
                                        <asp:ListItem Value="06" meta:resourcekey="ListItemResource9">June</asp:ListItem>
                                        <asp:ListItem Value="07" meta:resourcekey="ListItemResource10">July</asp:ListItem>
                                        <asp:ListItem Value="08" meta:resourcekey="ListItemResource11">August</asp:ListItem>
                                        <asp:ListItem Value="09" meta:resourcekey="ListItemResource12">September</asp:ListItem>
                                        <asp:ListItem Value="10" meta:resourcekey="ListItemResource13">October</asp:ListItem>
                                        <asp:ListItem Value="11" meta:resourcekey="ListItemResource14">November</asp:ListItem>
                                        <asp:ListItem Value="12" meta:resourcekey="ListItemResource15">December</asp:ListItem>

                                    </asp:DropDownList>
                                </div>
                                <div class="form-group">
                                    <div class="btn-group-sm">
                                        <asp:Button ID="btnGeneratePaySlip" runat="server" Text="Generate payslip" OnClientClick="clearDirty();" TabIndex="4" ValidationGroup="vg1" CausesValidation="true"
                                            OnClick="btnGeneratePaySlip_Click" meta:resourcekey="btnGeneratePaySlipResource1" />
                                    </div>
                                </div>
                            </div>
                            <div>   
                                <div>
                                    <asp:RequiredFieldValidator ID="RFV_DDLEmpList" runat="server" ControlToValidate="DDLEmpList" Display="Dynamic" CssClass="lblValidation"
                                        ErrorMessage="Please select Employee" ForeColor="Red" InitialValue="0" ValidationGroup="vg1"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>



                        <br />

                        <div class="clear"></div>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <iframe id="ifrm" name="ifrm" runat="server" width="70%" height="1190px" scrolling="no" frameborder="0" marginheight="0" marginwidth="0" visible="False"></iframe>

                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
