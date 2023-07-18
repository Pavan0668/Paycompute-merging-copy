<%@ Page Title="" Language="C#" MasterPageFile="~/UI/SubSiteMaster.master" AutoEventWireup="true" CodeBehind="FBP_Declare.aspx.cs"
    Inherits="iEmpPower.UI.FBP.FBP_Declare" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%--<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .rightalign {
            text-align: right;
        }

        .centeralign {
            text-align: center;
        }

        .colwidth {
            width: 80px;
        }

        .borderhide {
            border: 0 none !important;
            background-color: white !important;
        }

        table#GV_FBPDeclare {
            border: 0 none !important;
        }

        .btnStyle
{
    border-style: solid;
    border-width: 1px;
    border-color: #008FDB #005CB9 #002851 #002142;
    padding: 2px 10px 2px 10px;
    background-image: url('../images/btn.jpg');
    background-repeat: repeat-x;
    background-position: left;
    color: #FFFFFF;
    background-color: #0080C0;
}
.btnStyle:hover
{
    background-image: url('../images/btn-hover.jpg');
    background-repeat: repeat-x;
    background-position: left;
    color: #FFFFFF;
    background-color: #0196e1;
}

    </style>

    <script src="http://code.jquery.com/jquery-latest.min.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        function calculateGrandTotal() {
            $("[id*=txtAnnualAllocation], input[type=text]").on("keyup keydown change", function () {
                var TotalWorkHrs = 0;
                $("[id*=txtAnnualAllocation]").each(function () {
                    if (!jQuery.trim($(this).val()) == '') {
                        if (!isNaN(parseFloat($(this).val() == '' ? 0.00 : $(this).val()))) {
                            //alert($(this).val() == '' ? 0 : $(this).val());
                            TotalWorkHrs = TotalWorkHrs + parseFloat($(this).val() == '' ? 0.00 : $(this).val());

                            var row = $(this).closest("tr");
                            $("[id*=lblMonthlyAllocation]", row).html((parseFloat($(this).val()) / 12).toFixed(2).toString());
                        }
                    }
                });
                $("[id*=lblAnnualAllocationTotal]").html(TotalWorkHrs.toFixed(2).toString());
                $("[id*=HFAllocationTotal]").val(TotalWorkHrs.toFixed(2).toString());


                var sallow = 0;
                sallow = parseFloat($("[id*=lblBasketTotalAmount]").html().replace(',', '')) - parseFloat($("[id*=lblAnnualAllocationTotal]").html());
                $("[id*=lblFBPSpecialAllowance]").html(sallow.toFixed(2).toString());
            });
        }

        $(document).ready(function () {
            var TotalWorkHrs = 0;
            $("[id*=txtAnnualAllocation]").each(function () {
                if (!jQuery.trim($(this).val()) == '') {
                    if (!isNaN(parseFloat($(this).val() == '' ? 0.00 : $(this).val()))) {
                        //alert($(this).val() == '' ? 0 : $(this).val());
                        TotalWorkHrs = TotalWorkHrs + parseFloat($(this).val() == '' ? 0.00 : $(this).val());

                        var row = $(this).closest("tr");
                        $("[id*=lblMonthlyAllocation]", row).html((parseFloat($(this).val()) / 12).toFixed(2).toString());
                    }
                }
            });
            $("[id*=lblAnnualAllocationTotal]").html(TotalWorkHrs.toFixed(2).toString());
            $("[id*=HFAllocationTotal]").val(TotalWorkHrs.toFixed(2).toString());

            var sallow = 0;
            sallow = parseFloat($("[id*=lblBasketTotalAmount]").html().replace(',', '')) - parseFloat($("[id*=lblAnnualAllocationTotal]").html());
            $("[id*=lblFBPSpecialAllowance]").html(sallow.toFixed(2).toString());
        });

        function validate(Msg) {
            if (Page_ClientValidate())
                return confirm('Do you want to ' + Msg + ' this FBP declaration ?');
        }

    </script>
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <h2>Flexible Benefit Plan Declaration : From April 1<sup>st</sup> <asp:Label ID="LblFromDate" runat="server"></asp:Label> To March 31<sup>st</sup> <asp:Label ID="LblToDate" runat="server"></asp:Label></h2>
        <asp:Label ID="lblMessageBoard" runat="server" CssClass="msgboard"></asp:Label>

        <div id="divview" runat="server" visible="true">

            <table class="tablebody" border="0" cellpadding="1" cellspacing="1">
                <tr>
                    <td align="left">
                        <table style="width: 100%;">
                            <tr>
                                <td>Plan</td>
                                <td>: 
                                <asp:Label ID="lblPlanDate" runat="server" CssClass="label"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>Basket Total</td>
                                <td>: 
                                <asp:Label ID="lblBasketTotalAmount" runat="server" CssClass="label"></asp:Label>

                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <asp:GridView ID="GV_FBPDeclare" runat="server" AutoGenerateColumns="False" DataKeyNames="ALLOWANCEID" ShowFooter="true"
                FooterStyle-HorizontalAlign="Right" BorderStyle="None">
                <Columns>
                    <asp:BoundField DataField="ALLOWANCEID" HeaderText="Id">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="ALLOWANCETEXT" HeaderText="Heads of Allowances">
                        <ItemStyle HorizontalAlign="left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="AMOUNT" HeaderText="Annual Entitlement">
                        <ItemStyle HorizontalAlign="right" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Monthly Allocation">
                        <ItemStyle HorizontalAlign="right" />
                        <ItemTemplate>
                            <asp:Label ID="lblMonthlyAllocation" runat="server" Text='<%# Eval("MONTHLY") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            Total Allocated<br />
                            FBP Special Allowance
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Annual Allocation">
                        <ItemStyle HorizontalAlign="right" />
                        <ItemTemplate>
                            <asp:TextBox ID="txtAnnualAllocation" runat="server" Style="text-align: right" onkeyup="calculateGrandTotal();" ValidationGroup="VG1" Text='<%# Eval("ANNUAL") %>'></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender16" FilterType="Custom, Numbers" ValidChars="." runat="server" Enabled="True" TargetControlID="txtAnnualAllocation">
                            </cc1:FilteredTextBoxExtender>
                            <asp:Label ID="lblAnnualAllocation" runat="server" Text="0.0" />
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lblAnnualAllocationTotal" runat="server" /><br />
                            <asp:Label ID="lblFBPSpecialAllowance" runat="server" />
                            <asp:HiddenField ID="HFAllocationTotal" runat="server" />
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="">
                        <ItemStyle />
                        <ItemTemplate>
                            <asp:RangeValidator ID="RVtxtAnnualAllocation" runat="server" ErrorMessage="Cannot declare more than eligibility" ControlToValidate="txtAnnualAllocation"
                                MinimumValue="0" MaximumValue='<%# Eval("AMOUNT") %>' Type="Double" ForeColor="Red" Display="Dynamic" ValidationGroup="VG1" SetFocusOnError="true"></asp:RangeValidator>
                        </ItemTemplate>
                        <HeaderStyle CssClass="borderhide" />
                        <ItemStyle CssClass="borderhide" />
                        <FooterStyle CssClass="borderhide" />
                    </asp:TemplateField>
                </Columns>
                <EditRowStyle BackColor="#2461BF" />

                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Font-Size="10pt" Font-Names="verdana" />

                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />

                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />

                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            </asp:GridView>
            <br />
            <table border="1px">
                <tr style="background-color: #507CD1; color: #FFFFFF">
                    <th>Joining Date</th>
                    <th>Effective Date</th>
                    <th>Pay Date</th>
                    <th>Last Updated Date</th>
                </tr>
                <tr>
                    <td class="centeralign">
                        <asp:Label ID="lblJoiningDate" runat="server" Enabled="False"></asp:Label></td>
                    <td class="centeralign">
                        <asp:Label ID="lblEffectiveDate" runat="server" Enabled="False"></asp:Label></td>
                    <td class="centeralign">
                        <asp:Label ID="lblPayDate" runat="server" Enabled="False"></asp:Label></td>
                    <td class="centeralign">
                        <asp:Label ID="lblLastUpdatedDate" runat="server" Enabled="False"></asp:Label></td>
                </tr>
            </table>
            <div class="buttonrow">
                <asp:Button ID="btnSubmit" runat="server" Text="Submit" Width="60px" OnClick="btnSubmit_Click" OnClientClick="return validate('Submit');" ValidationGroup="VG1"/>
                &nbsp;
            <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" OnClientClick="javascript:return confirm('Do you want to Cancel ?')" CausesValidation="False"/>
            </div>

        </div>

        <div id="DivAllDeclaration">
            <h3>Declaration of Current Financial Year</h3>
            
            <asp:Label ID="lblmsg" runat="server"></asp:Label>
            <br />
            <asp:GridView ID="grdFbpDeclaration" runat="server" AutoGenerateColumns="false" Width="100%" CellPadding="4" ForeColor="#333333"
                GridLines="None" Font-Size="8" Font-Names="verdana" BorderWidth="1px" CellSpacing="1" BorderColor="#2461BF"
                BorderStyle="Solid"
                OnRowDataBound="grdFbpDeclaration_RowDataBound" DataKeyNames="AA_AMT01,AA_AMT02,AA_AMT03,AA_AMT04,AA_AMT05,AA_AMT06,AA_AMT07,AA_AMT08,AA_AMT09,AA_AMT10,AA_AMT11,AA_AMT12">
                <Columns>
                    <asp:TemplateField HeaderText="Sl.No." ControlStyle-CssClass="col-center">
                        <ItemTemplate>
                            <%# Container.DataItemIndex+1 %>
                        </ItemTemplate>

                        <ItemStyle CssClass="col-center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Month" ControlStyle-CssClass="col-center" ItemStyle-CssClass="col-center">
                        <ItemTemplate>
                            <%-- <%# Eval("BEGDA") %>--%>
                            <%# Eval("BEGDA", "{0:dd-MM-yyyy}") %>
                        </ItemTemplate>

                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Medical" ControlStyle-CssClass="col-center" ItemStyle-CssClass="col-center">
                        <ItemTemplate>
                            <%# Eval("AA_AMT01") %>
                        </ItemTemplate>

                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="LTA" ControlStyle-CssClass="col-center" ItemStyle-CssClass="col-center">
                        <ItemTemplate>
                            <%# Eval("AA_AMT02") %>
                        </ItemTemplate>

                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Meal Voucher" ControlStyle-CssClass="col-center" ItemStyle-CssClass="col-center">
                        <ItemTemplate>
                            <%# Eval("AA_AMT03") %>
                        </ItemTemplate>

                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Car EMI" ControlStyle-CssClass="col-center" ItemStyle-CssClass="col-center">
                        <ItemTemplate>
                            <%# Eval("AA_AMT04") %>
                        </ItemTemplate>

                    </asp:TemplateField>
                    <%--<asp:TemplateField HeaderText="Fuel" ControlStyle-CssClass="col-center" ItemStyle-CssClass="col-center">
                        <ItemTemplate>
                            <%# Eval("AA_AMT05") %>
                        </ItemTemplate>

                    </asp:TemplateField>--%>
                    <%--<asp:TemplateField HeaderText="Driver's Salary" ControlStyle-CssClass="col-center" ItemStyle-CssClass="col-center">
                        <ItemTemplate>
                            <%# Eval("AA_AMT06") %>
                        </ItemTemplate>

                    </asp:TemplateField>--%>
                    <%--<asp:TemplateField HeaderText="Car Insurance & Maintenance" ControlStyle-CssClass="col-center" ItemStyle-CssClass="col-center">
                        <ItemTemplate>
                            <%# Eval("AA_AMT07") %>
                        </ItemTemplate>

                    </asp:TemplateField>--%>
                    <asp:TemplateField HeaderText="Mobile & Telephone Reimbursement" ControlStyle-CssClass="col-center" ItemStyle-CssClass="col-center">
                        <ItemTemplate>
                            <%# Eval("AA_AMT08") %>
                        </ItemTemplate>

                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Car Fuel Reimbursment-Self" ControlStyle-CssClass="col-center" ItemStyle-CssClass="col-center">
                        <ItemTemplate>
                            <%# Eval("AA_AMT09") %>
                        </ItemTemplate>

                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Mobile Purchase" ControlStyle-CssClass="col-center" ItemStyle-CssClass="col-center">
                        <ItemTemplate>
                            <%# Eval("AA_AMT10") %>
                        </ItemTemplate>

                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Car Related Reimbursement" ControlStyle-CssClass="col-center" ItemStyle-CssClass="col-center">
                        <ItemTemplate>
                            <%# Eval("AA_AMT11") %>
                        </ItemTemplate>

                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Education" ControlStyle-CssClass="col-center" ItemStyle-CssClass="col-center">
                        <ItemTemplate>
                            <%# Eval("AA_AMT12") %>
                        </ItemTemplate>

                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Created on" ControlStyle-CssClass="col-center" ItemStyle-CssClass="col-center" SortExpression="CREATED_ON">
                        <ItemTemplate>
                            <%--    <%# Eval("CREATED_ON") %>--%>
                            <%# Eval("CREATED_ON", "{0:dd-MM-yyyy}") %>
                        </ItemTemplate>

                    </asp:TemplateField>

                </Columns>
                <RowStyle BackColor="#EFF3FB" Font-Size="8" Font-Names="verdana" />
                <HeaderStyle BackColor="#2781ba" Font-Bold="True" ForeColor="White" Font-Size="8" Font-Names="verdana" />
                <AlternatingRowStyle BackColor="White" />
                <PagerStyle BackColor="#2781ba" ForeColor="White" HorizontalAlign="Center" Font-Size="8" Font-Names="verdana" />
                <SelectedRowStyle BackColor="#E1EAF7" Font-Bold="True" ForeColor="#333333" />
                <FooterStyle BackColor="White" Font-Bold="True" ForeColor="White" Font-Size="9" Font-Names="verdana" />
            </asp:GridView>
        </div>
        <div class="DivSpacer01"></div>
        <div id="Exportbtn" runat="server" >
        <asp:Button ID="BtnExporttoXl" runat="server" Text="Export To Excel" OnClick="btnExportToExcel_Click" CausesValidation="false" CssClass="btnStyle" TabIndex="5" />
        &nbsp;&nbsp;
                    <asp:Button ID="BtnExporttoPDF" runat="server" Text="Export To PDF" OnClick="ExportToPDF_Click" TabIndex="6" CssClass="btnStyle" />

    </div>
    </div>


     <script src="http://code.jquery.com/jquery-latest.min.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        function calculateGrandTotal() {
            $("[id*=txtAnnualAllocation], input[type=text]").on("keyup keydown change", function () {
                var TotalWorkHrs = 0;
                $("[id*=txtAnnualAllocation]").each(function () {
                    if (!jQuery.trim($(this).val()) == '') {
                        if (!isNaN(parseFloat($(this).val() == '' ? 0.00 : $(this).val()))) {
                            //alert($(this).val() == '' ? 0 : $(this).val());
                            TotalWorkHrs = TotalWorkHrs + parseFloat($(this).val() == '' ? 0.00 : $(this).val());

                            var row = $(this).closest("tr");
                            $("[id*=lblMonthlyAllocation]", row).html((parseFloat($(this).val()) / 12).toFixed(2).toString());
                        }
                    }
                });
                $("[id*=lblAnnualAllocationTotal]").html(TotalWorkHrs.toFixed(2).toString());
                $("[id*=HFAllocationTotal]").val(TotalWorkHrs.toFixed(2).toString());


                var sallow = 0;
                sallow = parseFloat($("[id*=lblBasketTotalAmount]").html().replace(',', '')) - parseFloat($("[id*=lblAnnualAllocationTotal]").html());
                $("[id*=lblFBPSpecialAllowance]").html(sallow.toFixed(2).toString());
            });
        }

        $(document).ready(function () {
            var TotalWorkHrs = 0;
            $("[id*=txtAnnualAllocation]").each(function () {
                if (!jQuery.trim($(this).val()) == '') {
                    if (!isNaN(parseFloat($(this).val() == '' ? 0.00 : $(this).val()))) {
                        //alert($(this).val() == '' ? 0 : $(this).val());
                        TotalWorkHrs = TotalWorkHrs + parseFloat($(this).val() == '' ? 0.00 : $(this).val());

                        var row = $(this).closest("tr");
                        $("[id*=lblMonthlyAllocation]", row).html((parseFloat($(this).val()) / 12).toFixed(2).toString());
                    }
                }
            });
            $("[id*=lblAnnualAllocationTotal]").html(TotalWorkHrs.toFixed(2).toString());
            $("[id*=HFAllocationTotal]").val(TotalWorkHrs.toFixed(2).toString());

            var sallow = 0;
            sallow = parseFloat($("[id*=lblBasketTotalAmount]").html().replace(',', '')) - parseFloat($("[id*=lblAnnualAllocationTotal]").html());
            $("[id*=lblFBPSpecialAllowance]").html(sallow.toFixed(2).toString());
        });

        function validate(Msg) {
            if (Page_ClientValidate())
                return confirm('Do you want to ' + Msg + ' this FBP declaration ?');
        }

    </script>
</asp:Content>
