<%@ Page Title="" Language="C#" MasterPageFile="~/UI/SubSiteMaster.Master" AutoEventWireup="true" CodeBehind="Punch_InOut.aspx.cs"
    Inherits="iEmpPower.UI.Configuration.Punch_InOut" Theme="SkinFile" Culture="en-GB" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript">

        var validFilesTypes = ["txt", "text"];

        function ValidateFile() {

            var file = document.getElementById("<%=FL_punchinoutdata.ClientID%>");

            var label = document.getElementById("<%=lbldocupmssg.ClientID%>");

            var path = file.value;

            var ext = path.substring(path.lastIndexOf(".") + 1, path.length).toLowerCase();

            var isValidFile = false;

            for (var i = 0; i < validFilesTypes.length; i++) {

                if (ext == validFilesTypes[i]) {

                    isValidFile = true;

                    break;

                }

            }

            if (!isValidFile) {

                label.style.color = "red";

                label.innerHTML = "Invalid File. Please upload a file with text format";

            }

            return isValidFile;

        }

    </script>

    <style>
        .HrCls {
            width: 100%;
            border: 0;
            height: 1px;
            background: #333;
            background-image: linear-gradient(to right, #333, #333, #ccc);
            padding: 0;
            margin: 3px 0;
        }

        .popUpStyle {
            /*font: normal 11px auto "Trebuchet MS", Verdana;*/
            background-color: #000000;
            /*color: #4f6b72;*/
            /*padding: 6px;*/
            filter: alpha(opacity=80);
            opacity: 0.15;
        }

        .modalBackground {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }


        .modalPopupDefault {
            text-align: left;
            background-color: #FFFFFF;
            /* border-width: 3px;
            border-style: solid;
            border-color: black;
            padding: 10px;*/
            /*padding-left: 10px;*/
            width: auto;
            height: 80vh;
            overflow: auto;
        }
    </style>


    <div class="row">
        <div class="col-12">
            <div class="page-title-box">
                <div class="page-title-right">
                    <ol class="breadcrumb m-0">
                        <li class="breadcrumb-item"><a href="../Default.aspx">Home</a></li>
                        <li class="breadcrumb-item active">Punch In / Out Data</li>
                    </ol>
                </div>
                <h4 class="page-title">Punch In / Out Data
                    <asp:Label ID="Label1" runat="server" CssClass="msgboard"></asp:Label>
                </h4>
            </div>
        </div>
    </div>

    <div class="card-box">
        <div id="real_time_chart" class="row">
            <div style="width: 99%; margin: 0 auto; padding: 5px 0 40px 0;">
                <div class="header-title">&nbsp;&nbsp;Upload Punch In/Out Data</div>
                <hr class="HrCls" />
                <br />
                <asp:UpdatePanel ID="b" runat="server">
                    <ContentTemplate>
                        <div class="row">
                            <div class="col-md-8">
                                <asp:FileUpload ID="FL_punchinoutdata" runat="server" AllowMultiple="false" />
                            </div>

                            <div class="col-md-4">
                                <asp:Button ID="btninoutdataupload" runat="server" Text="Upload" OnClick="btninoutdataupload_Click" ValidationGroup="vald" />
                            </div>

                        </div>
                        <div class="row">
                            <div class="col-md-8">
                                <asp:Label ID="lbldocupmssg" runat="server"></asp:Label>
                            </div>
                        </div>

                        <br />
                        <br />

                        <div class="header-title" id="dv_gv" runat="server">&nbsp;&nbsp;Employee's Punch In/Out Details</div>
                        <hr class="HrCls" />
                        <br />

                        <div class="form-group">
                            <div class="row">
                                <asp:RangeValidator ID="RV_TxtToDate" runat="server" ControlToValidate="TxtToDate" Display="Dynamic"
                                    CssClass="lblMsg" MinimumValue='01/01/2010' MaximumValue='01/01/2019' ValidationGroup="PVg"
                                    ErrorMessage='<%# string.Format("To dates should be less than {0}.", new DateTime(DateTime.Today.Year, DateTime.Today.Month,  DateTime.Today.Day).AddDays(-1).ToString("dd/MM/yyyy"))%>'
                                    Type="Date" ForeColor="Red"></asp:RangeValidator>
                                &nbsp;&nbsp;
                                            <asp:DropDownList ID="DDLEmpNames" runat="server" TabIndex="1" CssClass="txtDropDownwidth" ValidationGroup="PVg"></asp:DropDownList>

                                <asp:RequiredFieldValidator ID="RFV_DDLEmpNames" runat="server" ControlToValidate="DDLEmpNames" ErrorMessage="*"
                                    CssClass="lblValidation" Display="dynamic" InitialValue="0" ValidationGroup="PVg" ForeColor="Red"></asp:RequiredFieldValidator>
                                &nbsp;&nbsp;                                        
                                            <asp:TextBox ID="TxtFrmDate" runat="server" placeholder="DD/MM/YYYY" CssClass="txtDropDownwidth" TabIndex="2" ValidationGroup="PVg"></asp:TextBox>
                                <Ajx:MaskedEditExtender ID="MEE_TxtFrmDate" runat="server" AcceptNegative="Left" CultureName="en-GB"
                                    DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                    MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError"
                                    TargetControlID="TxtFrmDate" />
                                <Ajx:CalendarExtender ID="CE_TxtFrmDate" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                    TargetControlID="TxtFrmDate">
                                </Ajx:CalendarExtender>
                                <asp:RequiredFieldValidator ID="RFV_TxtFrmDate" runat="server" ControlToValidate="TxtFrmDate" ErrorMessage="*"
                                    CssClass="lblValidation" Display="Static" ValidationGroup="PVg" ForeColor="Red"></asp:RequiredFieldValidator>
                                &nbsp;&nbsp;
                                            <asp:TextBox ID="TxtToDate" runat="server" placeholder="DD/MM/YYYY" CssClass="txtDropDownwidth" TabIndex="3" ValidationGroup="PVg"></asp:TextBox>
                                <Ajx:MaskedEditExtender ID="MEE_TxtToDate" runat="server" AcceptNegative="Left" CultureName="en-GB"
                                    DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                    MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError"
                                    TargetControlID="TxtToDate" />
                                <Ajx:CalendarExtender ID="CE_TxtToDate" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                    TargetControlID="TxtToDate">
                                </Ajx:CalendarExtender>
                                <asp:RequiredFieldValidator ID="RFV_TxtToDate" runat="server" ControlToValidate="TxtToDate" ErrorMessage="*"
                                    CssClass="lblValidation" Display="Static" ValidationGroup="PVg" ForeColor="Red"></asp:RequiredFieldValidator>

                                &nbsp;&nbsp;
                                        <asp:Button ID="BtnSubmit" Width="65px" runat="server" Text="Search" ValidationGroup="PVg" OnClick="BtnSubmit_Click" CausesValidation="true" />
                                &nbsp;&nbsp;
                                        <asp:Button ID="btnClear" Width="65px" runat="server" Text="Reset" OnClick="btnClear_Click"/>
                                &nbsp;&nbsp;
                                        <asp:Button ID="btnExportToExcel" Width="65px" runat="server" Text="Export" Visible="false" OnClick="btnExportToExcel_Click"/>



                            </div>
                            <asp:CompareValidator ID="CV_TxtToDate" runat="server" ControlToCompare="TxtFrmDate" CssClass="lblValidation"
                                ControlToValidate="TxtToDate" Display="Dynamic" ErrorMessage="From date should be less than to date"
                                Operator="GreaterThanEqual" Type="Date" ValidationGroup="PVg" ForeColor="Red"></asp:CompareValidator>

                        </div>


                        <div class="respovrflw">
                            <%--<div class="col-md-12 text-right" id="dvcnt" runat="server"></div>--%>
                            <asp:GridView ID="GV_punchindetails" runat="server" AutoGenerateColumns="False" DataKeyNames="ROWNUMBERCICO,DATES,count,PERNR"
                                OnPageIndexChanging="GV_punchindetails_PageIndexChanging" AllowPaging="true" OnRowCommand="GV_punchindetails_RowCommand" OnRowDataBound="GV_punchindetails_RowDataBound">

                                <Columns>
                                    <asp:BoundField DataField="ROWNUMBERCICO" HeaderText="Sl No."></asp:BoundField>
                                    <asp:BoundField DataField="PERNR" HeaderText="Emp ID"></asp:BoundField>
                                    <asp:BoundField DataField="DATES1" HeaderText="Date"></asp:BoundField>
                                    <asp:BoundField DataField="PUNCH_IN" HeaderText="Punch In"></asp:BoundField>
                                    <asp:BoundField DataField="PUNCH_OUT" HeaderText="Punch Out"></asp:BoundField>
                                    <asp:BoundField DataField="TOTAL_HOURS" HeaderText="Total Working Hrs" DataFormatString="{0:hh:mm:ss}"></asp:BoundField>
                                    <asp:BoundField DataField="brkhrs" HeaderText="Total Break Hrs"></asp:BoundField>
                                    <asp:BoundField DataField="STATUS" HeaderText="Status"></asp:BoundField>
                                    <asp:BoundField DataField="TXT_LONG" HeaderText="Holiday Name"></asp:BoundField>

                                    <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkviewindetail" ToolTip="View" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CausesValidation="false"
                                                CommandName="view" CssClass="fe-eye"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblID" runat="server" Text='<%#Eval("PERNR") %>'></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>

                             <div class="row col-md-12" id="DivPaging" runat="server">
                        <div class="col-md-3" style="margin-top: 5px" id="divpendingrecordcount" runat="server"></div>
                        <div class="col-md-9 DivSpacer01 Div02 repeater text-xl-right">
                            <asp:Repeater ID="RptrClkInClkOutPager" runat="server">
                                <ItemTemplate>
                                    <ul class="pagination pagination-rounded" style="display: inline-block">
                                        <li class='paginate_button page-item <%# Convert.ToBoolean(Eval("Enabled")) ? "" : "active" %>'>
                                            <asp:LinkButton ID="lnkPage" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>' Enabled='<%# Eval("Enabled") %>' OnClick="Page_Changed" CssClass='page-link <%# Convert.ToBoolean(Eval("Enabled")) ? "page_enabled" : "page_disabled" %>'></asp:LinkButton>
                                        </li>
                                    </ul>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>


                        </div>

                        <Ajx:ModalPopupExtender BackgroundCssClass="popUpStyle" ID="modal1"
                            runat="server" PopupControlID="divpopupcomp" TargetControlID="Button1" CancelControlID="LK_closeComp">
                        </Ajx:ModalPopupExtender>
                        <button style="display: none;" id="Button1" runat="server"></button>

                        <div id="divpopupcomp" runat="server" class="modalPopupDefault" align="center">
                            <div class="modal-dialog modal-lg">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <h4 class="modal-title">Punch In/ Out Details </h4>
                                        <asp:LinkButton ID="LK_closeComp" class="close" data-dismiss="modal" aria-hidden="true" runat="server" Text="X" />
                                    </div>
                                    <div class="modal-body">
                                        <asp:Panel ID="pnl_empdesidmgr" runat="server">
                                            <asp:GridView ID="GV_punchinfulldetails" runat="server" ShowFooter="true">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl NO.">
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex + 1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="DATES" HeaderText="Date" DataFormatString="{0:yyyy-MM-dd}" />
                                                    <asp:BoundField DataField="DATES" HeaderText="Day" DataFormatString="{0:dddd}" />
                                                    <asp:BoundField DataField="PUNCH_OUT" HeaderText="Punch-Out" />
                                                    <asp:TemplateField HeaderText="Punch-In">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblpunchin" runat="server" Text='<%#Eval("PUNCH_IN") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblfoottext" runat="server" Text="Total Break Hrs :" Font-Bold="true"></asp:Label>
                                                        </FooterTemplate>

                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Break Hrs">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblbrk" runat="server" Text='<%#Eval("TOTAL_HOURS") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lbltotal" runat="server" Font-Bold="true"></asp:Label>
                                                        </FooterTemplate>

                                                    </asp:TemplateField>

                                                </Columns>
                                                <FooterStyle ForeColor="#6c758d" />
                                            </asp:GridView>
                                        </asp:Panel>

                                    </div>
                                </div>
                            </div>
                        </div>




                        <div class="respovrflw" style="display: none">
                            <div class="col-sm-6">
                                <div class="col-md-12 text-right" id="divcnt" runat="server"></div>
                                <div style="max-height: 250px;">
                                    <asp:GridView ID="GV_punchinfiles" runat="server" DataKeyNames="ID,EMPID"
                                        OnRowCommand="GV_punchinfiles_RowCommand" AllowPaging="true" PageSize="25" OnPageIndexChanging="GV_punchinfiles_PageIndexChanging">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRowNumber" runat="server" Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Date" HeaderText="Created On" />
                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LK_punchinfiledownload" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                        CommandName="filedown" ToolTip="Download"><i class="fe-download"></i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>

                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btninoutdataupload" />
                        <asp:PostBackTrigger ControlID="btnExportToExcel" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>
