<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="frmPMSUpload.aspx.cs"
    Inherits="iEmpPower.UI.Performance_Management_System.frmPMSUpload" Theme="SkinFile" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="../../Scripts/tabcontent.js" type="text/javascript"></script>
    <style type="text/css">
        .TblCls
        {
            border-collapse: collapse;
        }

        .TblWidth
        {
            width: 100%;
        }

        .Fnt01
        {
            font: normal normal normal 11px/22px Verdana,Arial,Helvetica,sans-serif;
            color: #474747;
        }

        .Td01
        {
            width: 20%;
            padding: 2px 0 2px 5px;
        }

        .Td02
        {
            width: 18px;
            text-align: center;
        }

        #divFile p
        {
            font: 13px tahoma, arial;
        }

        #divFile h3
        {
            font: 16px arial, tahoma;
            font-weight: bold;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <asp:HiddenField ID="hdHours" runat="server" />
        <div class="header">
            <div class="row clearfix">
                <div class="col-xs-12 col-sm-12">
                    <span class="HeadFontSize">EMPLOYEE &nbsp;PERFORMANCE &nbsp; - UPLOAD</span>
                </div>

            </div>
        </div>


        <div class="body">
            <div class="divfr">
                <div style="float: right">
                    <a href="../../UI/Performance_Management_System/EmailTemplates/Employee_End-User_Document.pdf" target="_blank">Help?</a>
                </div>
                <ul class="tabs" data-persist="true">
                    <li><a href="#view1">Self Appraisal</a></li>
                    <li><a href="#view2">Manager&#39;s Review</a></li>
                </ul>
                <div class="tabcontents">
                    <div id="view1">
                        <b>Self Appraisal</b>
                        <hr class="HrCls" />
                        <div class="DivSpacer01">
                        </div>
                        <div class="DivSpacer01">
                            <asp:Label ID="LblEmpUploadMsg" runat="server"></asp:Label>
                        </div>
                        <div id="divFile">
                            <div class="form-inline">
                                <div class="form-group">
                                    <div class="col-sm-3">
                                        <asp:FileUpload ID="FU_EmpUpload" runat="server" Font-Size="11px" /><%--multiple="true"--%>
                                    </div>
                                    <div class="col-sm-1">
                                        <asp:Button ID="BtnEmpUpload" runat="server" ValidationGroup="EmpVG" Text="Upload"
                                            OnClick="BtnEmpUpload_Click" />
                                    </div>
                                    <div class="col-sm-7">
                                        <asp:RequiredFieldValidator ID="RFV_FU_EmpUpload" runat="server" ControlToValidate="FU_EmpUpload"
                                            ErrorMessage="Select file" SetFocusOnError="true" ForeColor="Red" ValidationGroup="EmpVG" Display="Dynamic"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="REV_FU_EmpUpload" runat="server" ControlToValidate="FU_EmpUpload"
                                            ValidationGroup="EmpVG" Display="Dynamic" ErrorMessage="Select only PDF file (.pdf)" SetFocusOnError="true" ForeColor="Red"
                                            ValidationExpression="^.+(.pdf|.PDF)$"></asp:RegularExpressionValidator>
                                    </div>
                                </div>
                            </div>
                            <p>
                                <asp:Label ID="lblFileList" runat="server"></asp:Label>
                            </p>
                            <p>
                                <asp:Label ID="lblUploadStatus" runat="server"></asp:Label>
                            </p>
                            <p>
                                <asp:Label ID="lblFailedStatus" runat="server"></asp:Label>
                            </p>
                        </div>
                        <div class="DivSpacer01">
                        </div>
                    </div>
                    <div id="view2">
                        <b>Manager&#39;s Review</b>
                        <hr class="HrCls" />
                        <div class="DivSpacer01">
                        </div>
                        <div class="DivSpacer01">
                            <asp:Label ID="LblMngrUploadMsg" runat="server"></asp:Label>
                        </div>
                        <div id="div1">
                            <div class="form-inline">
                                <div class="form-group">
                                    <div class="col-sm-3">
                                        <asp:FileUpload ID="FU_MngrUpload" runat="server" Font-Size="11px" /><%-- multiple="true"--%>
                                    </div>
                                    <div class="col-sm-1">
                                        <asp:Button ID="BtnMngrUpload" runat="server" ValidationGroup="MngrVG" Text="Upload"
                                            OnClick="BtnMngrUpload_Click" />
                                    </div>
                                    <div class="col-sm-7">
                                        <asp:RequiredFieldValidator ID="RFV_FU_MngrUpload" runat="server" ControlToValidate="FU_MngrUpload"
                                            ErrorMessage="Select file" SetFocusOnError="true" ForeColor="Red" ValidationGroup="MngrVG" Display="Dynamic"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="REV_FU_MngrUpload" runat="server" ControlToValidate="FU_MngrUpload"
                                            ValidationGroup="MngrVG" Display="Dynamic" ErrorMessage="Select only PDF file (.pdf)" SetFocusOnError="true" ForeColor="Red"
                                            ValidationExpression="^.+(.pdf|.PDF)$"></asp:RegularExpressionValidator>
                                    </div>
                                </div>
                            </div>
                            <p>
                                <asp:Label ID="Label2" runat="server"></asp:Label>
                            </p>
                            <p>
                                <asp:Label ID="Label3" runat="server"></asp:Label>
                            </p>
                            <p>
                                <asp:Label ID="Label4" runat="server"></asp:Label>
                            </p>
                        </div>
                        <div class="DivSpacer01">
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <%--<script src="../../Scripts/jquery-1.8.3.min.js" type="text/javascript"></script>--%>
    <%-- <script type="text/javascript">
        $('#BtnEmpUpload').click(function () {
            if (fileUpload.value.length == 0) {    // CHECK IF FILE(S) SELECTED.
                alert('No files selected.');
                return false;
            }
        });
    </script>
    <script src="../../Scripts/jquery.MultiFile.js" type="text/javascript"></script>--%>
</asp:Content>
