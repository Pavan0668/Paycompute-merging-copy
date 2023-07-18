<%@ Page Title="Pay Slip" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" Inherits="UI_Working_Time_payslip" Culture="auto"
    EnableEventValidation="false" UICulture="en-GB" Theme="SkinFile" CodeBehind="payslip.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/jquery-ui.js" type="text/javascript"></script>
    <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/themes/blitzer/jquery-ui.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="DivSpacer01">
        <fieldset>
            <legend><b>PAY&nbsp;SLIP</b></legend>
            <div class="DivSpacer01">
                <div style="width: 80%; float: left; padding: 0 0 0 3px;">
                    <asp:Label ID="LblMsg" runat="server" CssClass="Fl" Font-Size="11px"></asp:Label>
                </div>
                <div style="width: 10%; float: right">
                </div>
            </div>
            <div style="width: 23.5%; float: left;">
                <div class="DivSpacer01 Div05">
                    <span class="label">Payslip Year : </span>
                    <asp:DropDownList ID="DDLPayslipyear" runat="server" OnSelectedIndexChanged="DDLPayslipyear_SelectedIndexChanged" AutoPostBack="true"
                        Style="letter-spacing: 1px; border: 1px solid #666666; margin-bottom: 4px; margin-left: 2px; padding: 2px; font-size: 11px; width: 125px;">
                    </asp:DropDownList>
                    <%-- <Ajx:CascadingDropDown ID="CCD_DDLPayslipyear" runat="server" TargetControlID="DDLPayslipyear" ServicePath="~/WebService/Service.asmx"
                    Enabled="True" LoadingText="[LOADING YEAR...]" PromptText="- SELECT YEAR -" ServiceMethod="GetQuotaYear" Category="Year">
                </Ajx:CascadingDropDown>--%>
                </div>
                <div class="DivSpacer01 Div04">
                    <asp:GridView ID="GV_Payslip" runat="server" AutoGenerateColumns="False" AllowPaging="false" Width="100%" DataKeyNames="FilePath,FileName"
                         OnRowCommand="GV_Payslip_RowCommand"  OnRowDeleting="GV_Payslip_RowDeleting" OnRowEditing="GV_Payslip_RowEditing" OnRowUpdating="GV_Payslip_RowUpdating">
                        <Columns>
                            <asp:BoundField DataField="RowNumber" HeaderText="Slno" SortExpression="RowNumber">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="35px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="FileName" HeaderText="Pay slip" SortExpression="FileName">
                                <ItemStyle Width="48%" />
                            </asp:BoundField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <a onclick='ShowPDF(<%# Eval("FileName") %>);' href="javascript:void(0);" style="text-decoration: none;">View</a>
                                    <%--  <asp:LinkButton ID="GVViewPayslip" runat="server" CausesValidation="false" CommandName="VIEW" OnClientClick='<%# Bind("FileName") %>'
                                        CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CssClass="Fnt02" Text="View"></asp:LinkButton>--%>
                                    &nbsp;
                                 <asp:LinkButton ID="GVDownloadPayslip" runat="server" CausesValidation="false" CommandName="DOWNLOAD"
                                     CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CssClass="Fnt02" Text="Download"></asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            <div id="DivPDF" style="width: 72%; height: 500px; float: right; border: 1px dashed #ccc; -webkit-box-shadow: -2px 3px 25px -11px rgba(0,0,0,0.75); -moz-box-shadow: -2px 3px 25px -11px rgba(0,0,0,0.75); box-shadow: -2px 3px 25px -11px rgba(0,0,0,0.75); margin: 0 20px 20px 0;">
            </div>

        </fieldset>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#DivPDF").empty();
            var Text = "<div style=\" width:90%; margin:10% auto; font-size:30px; font-weight:bold; text-align:center;word-spacing: 23px;"
            Text += "text-shadow: 4px 3px 2px rgba(168, 154, 202, 0.62); opacity: 0.2;font-family: Verdana,sans-ser\">SELECT PAYSLIP TO PREVIEW</div>";
            $("#DivPDF").html(Text);

        });
        function ShowPDF(PDFFile) {
            
            var object = "<object data=\"{FileName}\" type=\"application/pdf\" width=\"100%\" height=\"100%\">";
            object += "If you are unable to view file, you can download from <a href = \"{FileName}\">here</a>";
            object += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
            object += "</object>";
            object = object.replace(/{FileName}/g, "Exported/Payslip/" + ('00000' + PDFFile).slice(-14) + ".pdf");
            $("#DivPDF").empty();
            $("#DivPDF").html(object);
        }
    </script>

</asp:Content>

