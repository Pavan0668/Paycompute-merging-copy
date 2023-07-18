<%@ Page Title="" Language="C#" MasterPageFile="~/UI/SubSiteMaster.master" AutoEventWireup="true" CodeBehind="Income_Tax.aspx.cs" Inherits="iEmpPower.UI.IT.Income_Tax" EnableEventValidation="false"
    MaintainScrollPositionOnPostback="true" Culture="en-GB" %>

<%--Culture="en-GB"--%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript">
        function cal() {
            var a = document.getElementById("ContentPlaceHolder1_MainContent_txtITPreEmprIT").value;
            var b = document.getElementById("ContentPlaceHolder1_MainContent_txtITPreEmprSurD").value;
            var c = document.getElementById("ContentPlaceHolder1_MainContent_txtITPreEmprCess").value;
            // alert(a); alert(c); alert(b);
            if (a == '') a = 0.0;
            if (b == '') b = 0.0;
            if (c == '') c = 0.0;
            document.getElementById("ContentPlaceHolder1_MainContent_lblPretot").innerHTML = parseFloat(a) + parseFloat(b) + parseFloat(c);

        }



    </script>

    <style type="text/css">
        .rbl input[type="radio"]
        {
            margin-left: 10px;
            margin-right: 1px;
        }

        .Initial
        {
            display: inline;
            padding: 7px 16px;
            margin: 0;
            margin-right: 3px;
            border-radius: 3px 3px 0 0;
            outline: none;
            color: black;
        }

        .Clicked
        {
            display: inline;
            padding: 7px 16px;
            margin: 0;
            margin-right: 3px;
            border-radius: 3px 3px 0 0;
            outline: none;
            /*background: white;*/
            /*color: black;*/
            background: rgb(0,125,233);
            background: linear-gradient(90deg, rgba(0,125,233,1) 0%, rgba(0,212,197,1) 100%);
            color: #fff;
        }

        #Tab5
        {
            background: #6c757d !important;
            color: #FFF !important;
        }

        #ContentPlaceHolder1_MainContent_GVSec80Header tr td:nth-child(10) a
        {
            width: 85px !important;
        }

        #ContentPlaceHolder1_MainContent_GVSec80CHeader tr td:nth-child(10) a
        {
            width: 85px !important;
        }

        #ContentPlaceHolder1_MainContent_GVHousingHeader tr td:nth-child(9) a
        {
            width: 85px !important;
        }

        #ContentPlaceHolder1_MainContent_GVOthersHeader tr td:nth-child(9) a
        {
            width: 85px !important;
        }

        #ContentPlaceHolder1_MainContent_grdPreEmptIncHead tr td:nth-child(9) a
        {
            width: 85px !important;
        }
    </style>
    <!-- start page title -->
    <div class="row">
        <div class="col-12">
            <div class="page-title-box">
                <div class="page-title-right">
                    <ol class="breadcrumb m-0">
                        <li class="breadcrumb-item"><a href="../Default.aspx">Home</a></li>
                        <li class="breadcrumb-item active">Income Tax Declaration</li>
                    </ol>
                </div>
                <h4 class="page-title">Income Tax Declaration
                <asp:Label ID="lblMessageBoard" runat="server" CssClass="msgboard"></asp:Label>
                </h4>
            </div>
        </div>
    </div>
    <!-- end page title -->


    <div class="row">

        <ul class="nav nav-pills navtab-bg" style="margin-left: 12px;">
            <li class="nav-item font-16">
                <asp:LinkButton runat="server" ID="Tab2" class="nav-link active p-2" OnClick="Tab2_Click"><i class="fe-check-square"></i>
   Section 80 C </asp:LinkButton></li>
            <li class="nav-item font-16">
                <asp:LinkButton runat="server" ID="Tab1" class="nav-link  p-2" OnClick="Tab1_Click"><i class="fe-pie-chart"></i>
   Section 80 </asp:LinkButton></li>
            <li class="nav-item font-16">
                <asp:LinkButton runat="server" ID="Tab3" class="nav-link p-2" OnClick="Tab3_Click"><i class="fe-umbrella"></i>
   Housing </asp:LinkButton></li>
            <li class="nav-item font-16">
                <asp:LinkButton runat="server" ID="Tab6" class="nav-link p-2" OnClick="Tab6_Click"><i class="fe-file-text"></i>
  Previous Employment Income</asp:LinkButton></li>
            <li class="nav-item font-16">
                <asp:LinkButton runat="server" ID="Tab4" class="nav-link p-2" OnClick="Tab4_Click"><i class="fe-file-text"></i>
   Income from Other Sources </asp:LinkButton></li>
            <li class="nav-item font-16">
                <asp:LinkButton runat="server" ID="Tab5" class="nav-link p-2" OnClick="Tab5_Click"><i class="fe-layers"></i><%--Style="background: #6c757d !important; color: #FFF !important;"--%>
   IT History </asp:LinkButton></li>
            <li class="nav-item font-16">
                <asp:LinkButton runat="server" ID="Tab7" class="nav-link p-2" OnClick="Tab7_Click" OnClientClick="return confirm('Check IT history before submitting, Once submitted changes are not allowed');" Visible="false"><i class="fe-upload"></i>
   IT Submit All </asp:LinkButton></li>

            <li class="nav-item font-16" style="display:none;">
                <a href="Guidelines.zip" target="_blank" class="nav-link active p-2">Guidelines for IT proof submission</a>
            </li>
        </ul>

        <%-- <a href="Proof Submission Folder.zip" target="_blank">Guidelines for IT proof submission</a>--%>

        <!-- Tab Panel Start / -->
        <div class="col-xl-12 m-t-20">
            <div class="tab-content m-0 p-0">
                <asp:MultiView ID="MainView" runat="server">
                    <asp:View ID="View1" runat="server">

                        <%--   <div class="tab-pane active" id="Tab-1">--%>
                        <div id="Tab-1">
                            <!-- Tab Panel Start / -->

                            <div class="tab-pane active" id="FBP">
                                <div class="tab-content m-0 p-0">

                                    <div id="DivSec80" runat="server">

                                        <div class="tab-pane " id="Section-80">
                                            <div class="table-responsive card-box">
                                                <h5 class="pb-2">IT Declaration Section 80: From April 1<sup>st</sup>
                                                    <asp:Label ID="LblFromDate" runat="server"></asp:Label>
                                                    To March 31<sup>st</sup>
                                                    <asp:Label ID="LblToDate" runat="server"></asp:Label></h5>

                                                <asp:Label ID="LblLockSts" runat="server" CssClass="msgboard"></asp:Label>
                                                <h5 style="text-align: left; color: black;" class="hidden">
                                                    <asp:CheckBox ID="CB_ConsAct" runat="server" Text="Consider Actuals" OnCheckedChanged="CB_ConsAct_CheckedChanged" AutoPostBack="true" TabIndex="1" CssClass="checkbox checkbox-info" /></h5>

                                                <asp:GridView ID="GVITSec80" runat="server" CssClass="gridviewNew" GridLines="None" AutoGenerateColumns="False" BorderStyle="None" DataKeyNames="SBSEC,SBDIV,ID,LID"
                                                    OnRowCommand="GVITSec80_RowCommand" OnRowDeleting="GVITSec80_RowDeleting" TabIndex="2">
                                                    <Columns>
                                                        <%-- //select t8.SBSEC,t8.SBDIV,t8.SBDDS, t9.SDVLT,t9.TXEXM--%>
                                                        <asp:TemplateField HeaderText="No." HeaderStyle-Width="5%">
                                                            <ItemTemplate>
                                                                <%# Container.DataItemIndex+1 %>
                                                            </ItemTemplate>

                                                        </asp:TemplateField>
                                                        <asp:BoundField HeaderText="Contribution" DataField="SBDDS" HeaderStyle-Width="25%"></asp:BoundField>
                                                        <asp:BoundField HeaderText="Limit" DataField="SDVLT" DataFormatString="{0:#,##0.00}" HeaderStyle-CssClass="right" ItemStyle-CssClass="right"></asp:BoundField>
                                                        <asp:BoundField HeaderText="Tax EXEM%" DataField="TXEXM"></asp:BoundField>

                                                        <asp:TemplateField HeaderText="Prop. Contr. (INR)" HeaderStyle-CssClass="right" ItemStyle-CssClass="right">

                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtPropContr" runat="server" Text='<%# Eval("PROPCONTR") %>' CssClass="form-control  text-right"></asp:TextBox>
                                                                <cc1:FilteredTextBoxExtender ID="FTB_txtPropContr" runat="server" FilterMode="ValidChars" FilterType="Custom,Numbers"
                                                                    TargetControlID="txtPropContr" ValidChars=".">
                                                                </cc1:FilteredTextBoxExtender>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Act. Contr. (INR)" HeaderStyle-CssClass="right" ItemStyle-CssClass="right">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtActContr" runat="server" CssClass="form-control text-right" Text='<%# Eval("ACTCONTR") %>'></asp:TextBox>
                                                                <cc1:FilteredTextBoxExtender ID="FTB_txtActContr" runat="server" FilterMode="ValidChars" FilterType="Custom,Numbers"
                                                                    TargetControlID="txtActContr" ValidChars=".">
                                                                </cc1:FilteredTextBoxExtender>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <%-- <asp:BoundField HeaderText="Curr"  DataField="CURR">
                        <ItemStyle HorizontalAlign="right"/>
                    </asp:BoundField>--%>

                                                        <asp:TemplateField HeaderText="Attachments" Visible="false">
                                                            <ItemTemplate>
                                                                <div id="divFile" runat="server" style="overflow: hidden">
                                                                    <asp:FileUpload ID="fuAttachments" runat="server" ForeColor="Black" Visible='<%# bool.Parse(string.Format("{0}", Eval("RECEIPT_FID").ToString()=="" ? "true" : "false"))%>' /><br />
                                                                    <asp:Label ID="fuAttachmentsfname" runat="server" Visible='<%# bool.Parse(string.Format("{0}", Eval("RECEIPT_FID").ToString()=="" ? "true" : "false"))%>'></asp:Label>
                                                                    <asp:LinkButton ID="Lbtndownload" runat="server" Text='<%#Eval("RECEIPT_FID") %>' Font-Bold="True" CommandName="download" CommandArgument='<%# Eval("RECEIPT_FPATH") %>' CausesValidation="false" Visible='<%# bool.Parse(string.Format("{0}", Eval("RECEIPT_FID").ToString()=="" ? "false" : "true"))%>' />

                                                                    <%-- <asp:LinkButton ID="LbtnUpload" runat="server" Text="Upload" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'
                            CommandName="Upload" CausesValidation="false" Visible='<%# bool.Parse(string.Format("{0}", Eval("RECEIPT_FID").ToString()=="" ? "true" : "false"))%>'></asp:LinkButton>
                                                                    --%>
                                                                    <asp:LinkButton ID="LbtnDelete" runat="server" Text="Delete" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'
                                                                        CommandName="Delete" CausesValidation="false" Visible='<%# bool.Parse(string.Format("{0}", Eval("RECEIPT_FID").ToString()=="" ? "false" : "true"))%>'></asp:LinkButton>

                                                                </div>

                                                            </ItemTemplate>
                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="Remarks">
                                                            <ItemStyle HorizontalAlign="left" />
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtRemarks" runat="server" Height="25" TextMode="MultiLine" CssClass="form-control" Text='<%# Eval("EMPCOMMENTS") %>'></asp:TextBox>
                                                            </ItemTemplate>

                                                        </asp:TemplateField>
                                                    </Columns>

                                                </asp:GridView>
                                            </div>

                                            <div class="mb-3">
                                                <asp:Button ID="btnSubmitClaims" runat="server" Text="Save & Next" OnClick="btnSubmitITSec80_Click" TabIndex="3" CssClass="btn bg-brand-btn waves-effect waves-light btn-std" />
                                                <asp:Button ID="BtnUpdate" runat="server" Text="Save & Next" OnClick="BtnUpdate_Click" TabIndex="4" CssClass="btn bg-brand-btn waves-effect waves-light btn-std" />
                                                <asp:Button ID="BtnEdit" runat="server" Text="Edit" OnClick="BtnEdit_Click" TabIndex="5" CssClass="btn bg-brand-btn waves-effect waves-light btn-std" />
                                                <asp:Button ID="BtnCancel" runat="server" Text="Cancel" OnClick="BtnCancel_Click" TabIndex="6" CssClass="btn bg-brand-btn waves-effect waves-light btn-std" />
                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </div>

                            <!-- end Tab Panel-->

                        </div>
                        <!-- end row -->

                    </asp:View>


                    <!-- =========== Tab-2 Tab Panel ==================-->
                    <asp:View ID="View2" runat="server">
                        <div id="Tab-2">
                            <!-- Tab Panel Start / -->

                            <div class="tab-pane active" id="Div1">
                                <div class="tab-content m-0 p-0">

                                    <div id="DivSec80C" runat="server">

                                        <div class="tab-pane " id="Div3">
                                            <div class="table-responsive card-box">
                                                <h5 class="pb-2">IT Declaration Section 80C: From April 1<sup>st</sup>
                                                    <asp:Label ID="LblFromDate2" runat="server"></asp:Label>
                                                    To March 31<sup>st</sup>
                                                    <asp:Label ID="LblToDate2" runat="server"></asp:Label></h5>

                                                <asp:Label ID="LblLockSts2" runat="server" CssClass="msgboard"></asp:Label>
                                                <h5 style="text-align: left; color: black;" class="hidden">
                                                    <asp:CheckBox ID="CB_ConsAct2" runat="server" Text="Consider Actuals" OnCheckedChanged="CB_ConsAct2_CheckedChanged" AutoPostBack="true" TabIndex="1" CssClass="checkbox checkbox-info" /></h5>

                                                <asp:GridView ID="GVITSec80C" runat="server" CssClass="gridviewNew" GridLines="None" AutoGenerateColumns="False" BorderStyle="None" DataKeyNames="ICODE,ID,LID"
                                                    OnRowCommand="GVITSec80C_RowCommand" OnRowDeleting="GVITSec80C_RowDeleting" TabIndex="2">
                                                    <Columns>

                                                        <asp:TemplateField HeaderText="No." HeaderStyle-Width="5%">
                                                            <ItemTemplate>
                                                                <%# Container.DataItemIndex+1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField HeaderText="Investment / Contribution" DataField="ITTXT" HeaderStyle-Width="35%"></asp:BoundField>
                                                        <asp:BoundField HeaderText="Limit" DataField="ITLMT" HeaderStyle-CssClass="right" ItemStyle-CssClass="right" DataFormatString="{0:#,##0.00}"></asp:BoundField>

                                                        <asp:TemplateField HeaderText="Prop. Invst. (INR)" HeaderStyle-CssClass="right" ItemStyle-CssClass="right">

                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtPropContr" runat="server" Style="text-align: right" CssClass="form-control" Text='<%# Eval("PROPINVST") %>'></asp:TextBox>
                                                                <cc1:FilteredTextBoxExtender ID="FTB_txtPropContr" runat="server" FilterMode="ValidChars" FilterType="Custom,Numbers"
                                                                    TargetControlID="txtPropContr" ValidChars=".">
                                                                </cc1:FilteredTextBoxExtender>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Act. Invst. (INR)" HeaderStyle-CssClass="right" ItemStyle-CssClass="right">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtActContr" runat="server" Style="text-align: right" CssClass="form-control" Text='<%# Eval("ACTINVST") %>'></asp:TextBox>
                                                                <cc1:FilteredTextBoxExtender ID="FTB_txtActContr" runat="server" FilterMode="ValidChars" FilterType="Custom,Numbers"
                                                                    TargetControlID="txtActContr" ValidChars=".">
                                                                </cc1:FilteredTextBoxExtender>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="Attachments" Visible="false">
                                                            <ItemTemplate>
                                                                <div id="divFile" runat="server" style="overflow: hidden">
                                                                    <asp:FileUpload ID="fuAttachments" runat="server" ForeColor="Black" Visible='<%# bool.Parse(string.Format("{0}", Eval("RECEIPT_FID").ToString()=="" ? "true" : "false"))%>' /><br />
                                                                    <asp:Label ID="fuAttachmentsfname" runat="server" Visible='<%# bool.Parse(string.Format("{0}", Eval("RECEIPT_FID").ToString()=="" ? "true" : "false"))%>'></asp:Label>



                                                                    <asp:LinkButton ID="Lbtndownload" runat="server" Text='<%#Eval("RECEIPT_FID") %>' Font-Bold="True" CommandName="download" CommandArgument='<%# Eval("RECEIPT_FPATH") %>' CausesValidation="false" Visible='<%# bool.Parse(string.Format("{0}", Eval("RECEIPT_FID").ToString()=="" ? "false" : "true"))%>' />

                                                                    <asp:LinkButton ID="LbtnDelete" runat="server" Text="Delete" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'
                                                                        CommandName="Delete" CausesValidation="false" Visible='<%# bool.Parse(string.Format("{0}", Eval("RECEIPT_FID").ToString()=="" ? "false" : "true"))%>'></asp:LinkButton>



                                                                </div>

                                                            </ItemTemplate>
                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="Remarks">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtRemarks" runat="server" Height="25" CssClass="form-control" TextMode="MultiLine" Text='<%# Eval("EMPCOMMENTS") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>

                                                </asp:GridView>

                                                <%--<asp:GridView ID="GridView1" runat="server"></asp:GridView>--%>
                                            </div>

                                            <div class="mb-3">
                                                <asp:Button ID="btnSubmitClaims2" runat="server" Text="Save & Next" OnClick="btnSubmitITSec802_Click" TabIndex="3" CssClass="btn bg-brand-btn waves-effect waves-light btn-std" />
                                                <asp:Button ID="BtnUpdate2" runat="server" Text="Save & Next" OnClick="BtnUpdate2_Click" TabIndex="4" CssClass="btn bg-brand-btn waves-effect waves-light btn-std" />
                                                <asp:Button ID="BtnEdit2" runat="server" Text="Edit" OnClick="BtnEdit2_Click" TabIndex="5" CssClass="btn bg-brand-btn waves-effect waves-light btn-std" />
                                                <asp:Button ID="BtnCancel2" runat="server" Text="Cancel" OnClick="BtnCancel2_Click" TabIndex="6" CssClass="btn bg-brand-btn waves-effect waves-light btn-std" />
                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </div>

                            <!-- end Tab Panel-->
                    </asp:View>
                    <!-- =========== end Payslip Tab Panel ===========-->

                    <!-- =========== Tab-3 Tab Panel ==================-->
                    <asp:View ID="View3" runat="server">

                        <div id="Tab-3">
                            <!-- Tab Panel Start / -->

                            <div class="tab-pane active" id="Div2">
                                <div class="tab-content m-0 p-0">

                                    <div id="DivHosuing" runat="server">

                                        <div class="tab-pane " id="Div5">
                                            <div class="table-responsive card-box">
                                                <h5 class="pb-2">IT Declaration Housing: From April 1<sup>st</sup>
                                                    <asp:Label ID="LblFromDate3" runat="server"></asp:Label>
                                                    To March 31<sup>st</sup>
                                                    <asp:Label ID="LblToDate3" runat="server"></asp:Label></h5>
                                                <asp:Label ID="LblLockSts3" runat="server" CssClass="msgboard"></asp:Label>


                                                <div class="row">
                                                    <div class="col-sm-2">
                                                        City Category :
                                                    </div>

                                                    <div class="col-sm-2">
                                                        <asp:DropDownList ID="DDL_CityCat" runat="server" CssClass="form-control" TabIndex="2">
                                                            <asp:ListItem Text="SELECT" Value="-1" Selected="True"></asp:ListItem>
                                                            <asp:ListItem Text="Metro" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="Non-Metro" Value="0"></asp:ListItem>
                                                        </asp:DropDownList>

                                                    </div>
                                                    <div class="col-sm-2">
                                                        <asp:RequiredFieldValidator ID="RF_DDL_CityCat" runat="server" ErrorMessage="Please Select City Category" ForeColor="Red"
                                                            ControlToValidate="DDL_CityCat" ValidationGroup="VG1" InitialValue="-1"></asp:RequiredFieldValidator>
                                                    </div>


                                                </div>

                                                <br />
                                                <b>HRA-RENT PAYMENT DETAILS:</b>
                                                <%-- <div class="col-sm-9 text-right">--%>
                                                <h5 class="text-warning">Update the monthly Rent amount which will be paying in the current FY between Apr - Mar!</h5>
                                                <%--</div>--%>
                                                <%--<br />
                                                <br />--%>
                                                <asp:GridView ID="gvHRA" runat="server" CssClass="gridviewNew" GridLines="None" AutoGenerateColumns="false" DataKeyNames="ID"
                                                    EmptyDataText="No records has been added." OnRowDataBound="OnRowDataBound" OnRowEditing="OnRowEditing" OnRowCancelingEdit="OnRowCancelingEdit"
                                                    OnRowUpdating="OnRowUpdating" OnRowDeleting="OnRowDeleting">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="From Date">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblFromDate" runat="server" Text='<%# Eval("BEGDA", "{0:dd/MM/yyyy}") %>' class="form-control-file"></asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtFromDate" runat="server" Text='<%# Eval("BEGDA", "{0:dd/MM/yyyy}") %>' class="form-control"></asp:TextBox>
                                                                <cc1:MaskedEditExtender ID="MEE_txtFromDate" runat="server" AcceptNegative="Left"
                                                                    CultureName="en-GB" DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999"
                                                                    MaskType="Date" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                                                                    OnInvalidCssClass="MaskedEditError" TargetControlID="txtFromDate" />
                                                                <cc1:CalendarExtender ID="CE_txtFromDate" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                                    TargetControlID="txtFromDate">
                                                                </cc1:CalendarExtender>
                                                                <asp:RequiredFieldValidator ID="RFV_txtFromDate" ValidationGroup="VGEdit" runat="server" ControlToValidate="txtFromDate" ErrorMessage="Please select From Date"
                                                                    ForeColor="Red"></asp:RequiredFieldValidator>
                                                                <asp:RangeValidator ID="RV_txtFromDate" runat="server" ControlToValidate="txtFromDate" Display="Dynamic"
                                                                    ErrorMessage="" ValidationGroup="VGEdit"
                                                                    Type="Date" ForeColor="Red"></asp:RangeValidator>
                                                                <asp:CompareValidator ID="CPV_txtFromDate" ControlToValidate="txtFromDate" runat="server" Display="Dynamic" SetFocusOnError="true" Operator="GreaterThanEqual"
                                                                    Type="Date" ErrorMessage="Invalid! From date must be greater than date of joining" ForeColor="Red" ValidationGroup="VGEdit">
                                                                </asp:CompareValidator>

                                                            </EditItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="To Date">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblToDate" runat="server" Text='<%# Eval("ENDDA", "{0:dd/MM/yyyy}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtToDate" runat="server" Text='<%# Eval("ENDDA", "{0:dd/MM/yyyy}") %>' class="form-control"></asp:TextBox>
                                                                <cc1:MaskedEditExtender ID="MEE_txtToDate" runat="server" AcceptNegative="Left"
                                                                    CultureName="en-GB" DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999"
                                                                    MaskType="Date" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                                                                    OnInvalidCssClass="MaskedEditError" TargetControlID="txtToDate" />
                                                                <cc1:CalendarExtender ID="CE_txtToDate" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                                    TargetControlID="txtToDate">
                                                                </cc1:CalendarExtender>
                                                                <asp:RequiredFieldValidator ID="RFV_txtToDate" ValidationGroup="VGEdit" runat="server" ControlToValidate="txtToDate" ErrorMessage="Please select To Date"
                                                                    ForeColor="Red"></asp:RequiredFieldValidator>
                                                                <asp:RangeValidator ID="RV_txtToDate" runat="server" ControlToValidate="txtToDate" Display="Dynamic"
                                                                    ErrorMessage="" ValidationGroup="VGEdit"
                                                                    Type="Date" ForeColor="Red"></asp:RangeValidator>
                                                                <asp:CompareValidator ID="CPV_txtToDate" ControlToValidate="txtToDate" runat="server" ValidationGroup="VGEdit" Display="Dynamic" SetFocusOnError="true" Operator="GreaterThan"
                                                                    Type="Date" ErrorMessage="Invalid! To date must be greater than date of joining" ForeColor="Red">
                                                                </asp:CompareValidator>
                                                            </EditItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Rent Per Month" HeaderStyle-CssClass="right" ItemStyle-CssClass="right" HeaderStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRentPerMonth" runat="server" Text='<%# Eval("RTAMT","{0:#,##0.00 }") %>'></asp:Label>
                                                                / Month
                                                                <asp:Label ID="lblRentPeryear" runat="server" Text='<%# ((((Convert.ToDateTime(Eval("ENDDA")).Year - Convert.ToDateTime(Eval("BEGDA")).Year)*12)+(Convert.ToDateTime(Eval("ENDDA")).Month - Convert.ToDateTime(Eval("BEGDA")).Month))+1)*Convert.ToDecimal(Eval("RTAMT","{0:#,##0.00 }")) %>'></asp:Label>
                                                                / Annum
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtRentPerMonth" runat="server" Text='<%# Eval("RTAMT") %>' class="form-control right" OnTextChanged="OnTextChanged" AutoPostBack="true"></asp:TextBox>
                                                                <asp:TextBox ID="txtRentPeryear" runat="server" Width="110" Enabled="false" Style="text-align: right"></asp:TextBox>
                                                                <div style="text-align: right">
                                                                    <asp:Label ID="Label2" runat="server" Width="80" Text="/ Annum" />
                                                                </div>
                                                            </EditItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Period Rent" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPeriodRent" runat="server" Text="" class="form-control-file"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Address" ItemStyle-Width="150">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblAddress" runat="server" Text='<%# Eval("Address") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtAddress" runat="server" MaxLength="35" class="form-control" Text='<%# Eval("Address") %>' TextMode="MultiLine"></asp:TextBox>
                                                                <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtAddress" ID="RegularExpressionValidator6" ValidationGroup="VGEdit"
                                                                    ValidationExpression="^[\s\S]{0,35}$" runat="server" ErrorMessage="Maximum 35 characters allowed." ForeColor="Red"></asp:RegularExpressionValidator>
                                                            </EditItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="State">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblState" runat="server" Text='<%# Eval("BEZEI") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <%--<asp:TextBox ID="txtState" runat="server" Text='<%# Eval("State") %>'></asp:TextBox>--%>
                                                                <asp:DropDownList ID="drpdwnState" runat="server" class="form-control"></asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RFVdrpdwnState" runat="server" ErrorMessage="Please select State" ForeColor="Red" ControlToValidate="drpdwnState" InitialValue="0"></asp:RequiredFieldValidator>
                                                            </EditItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="City">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCity" runat="server" Text='<%# Eval("City") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtCity" runat="server" Text='<%# Eval("City") %>' class="form-control"></asp:TextBox>
                                                            </EditItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Landlord's Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblLandLordsName" runat="server" Text='<%# Eval("LDNAM") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtLandLordsName" runat="server" Text='<%# Eval("LDNAM") %>' class="form-control"></asp:TextBox>

                                                            </EditItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Landlord's Address">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblLandLordsAddress" runat="server" Text='<%# Eval("LDAD1") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtLandLordsAddress" MaxLength="35" runat="server" Text='<%# Eval("LDAD1") %>' class="form-control" TextMode="MultiLine"></asp:TextBox>
                                                                <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtLandLordsAddress" ID="RegularExpressionValidator3" ValidationGroup="VGEdit"
                                                                    ValidationExpression="^[\s\S]{0,35}$" runat="server" ErrorMessage="Maximum 35 characters allowed." ForeColor="Red"></asp:RegularExpressionValidator>
                                                            </EditItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="PAN of Landlord" ItemStyle-Width="80">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPANofLandLord" runat="server" Text='<%# Eval("LDAID") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtPANofLandLord" runat="server" Text='<%# Eval("LDAID") %>' class="form-control" ToolTip="1. Is a combination of letters and numbers having a length of 10.
                                                                            &#013;2. All letters are in upper case.
                                                                            &#013;3. A PAN card number always starts and ends with letters.
                                                                            &#013;4. Does not contain special characters.
                                                                            &#013;5. The PAN card number starts with five letters then has four digits and the last one is a letter."></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="VGEdit" runat="server" ValidationExpression="[A-Z]{3}P[A-Z]{1}\d{4}[A-Z]{1}$" Display="Dynamic" SetFocusOnError="true"
                                                                    ControlToValidate="txtPANofLandLord" ErrorMessage="Invalid PAN Number" ForeColor="Red"></asp:RegularExpressionValidator><%--[A-Z]{3}P[A-Z]{1}\d{4}[A-Z]{1}$--%>
                                                            </EditItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:CommandField ButtonType="Link" ShowEditButton="true" ShowDeleteButton="true" ItemStyle-Width="80" ValidationGroup="VGEdit" />
                                                    </Columns>
                                                </asp:GridView>

                                                <table class="gridviewNew">
                                                    <tr>
                                                        <td style="width: 100px">
                                                            <%-- From Date:<br />--%>
                                                            <asp:TextBox ID="txtFromDate" runat="server" Width="110" ValidationGroup="VGAdd" CssClass="form-control" placeholder="From Date" />
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
                                                            <asp:CompareValidator ID="CPV_txtFromDate" ControlToValidate="txtFromDate" runat="server" Display="Dynamic" SetFocusOnError="true" Operator="GreaterThanEqual"
                                                                Type="Date" ErrorMessage="Invalid! From date must be greater than date of joining" ForeColor="Red">
                                                            </asp:CompareValidator>
                                                        </td>
                                                        <td style="width: 80px">
                                                            <%-- To Date:<br />--%>
                                                            <asp:TextBox ID="txtToDate" runat="server" Width="80" ValidationGroup="VGAdd" CssClass="form-control" placeholder="To Date" />
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
                                                            <asp:CompareValidator ID="CPV_txtToDate" ControlToValidate="txtToDate" runat="server" Display="Dynamic" SetFocusOnError="true" Operator="GreaterThan"
                                                                Type="Date" ErrorMessage="Invalid! From date must be greater than date of joining" ForeColor="Red">
                                                            </asp:CompareValidator>
                                                            <asp:CompareValidator ID="CPV_FRM_TO" ControlToValidate="txtToDate" ControlToCompare="txtFromDate" runat="server" Display="Dynamic" SetFocusOnError="true" Operator="GreaterThan"
                                                                Type="Date" ErrorMessage="Invalid! From date must be less than To date" ForeColor="Red">
                                                            </asp:CompareValidator>
                                                        </td>
                                                        <td style="width: 110px">
                                                            <%--Rent/Month:<br />--%>
                                                            <asp:TextBox ID="TXT_ActAmount" runat="server" Width="110" AutoPostBack="True" OnTextChanged="TXT_ActAmount_TextChanged" ValidationGroup="VGAdd" CssClass="form-control right" placeholder="Rent/Month" />
                                                            <%--<div style="text-align: right">--%>
                                                            <%--<asp:Label ID="lblRentPeryear" runat="server" Width="80" />--%>

                                                            <asp:TextBox ID="lblRentPeryear" runat="server" Width="110" Enabled="false" Style="text-align: right"></asp:TextBox>
                                                            <div style="text-align: right">
                                                                <asp:Label ID="Label2" runat="server" Width="80" Text="/ Annum" />
                                                            </div>
                                                            <%--</div>--%>
                                                            <asp:RequiredFieldValidator ID="RFV_TXT_ActAmount" runat="server" ErrorMessage="Please Enter Rent"
                                                                ForeColor="Red" ControlToValidate="TXT_ActAmount" ValidationGroup="VGAdd"></asp:RequiredFieldValidator>
                                                            <cc1:FilteredTextBoxExtender ID="FTB_TXT_ActAmount" runat="server" FilterMode="ValidChars" FilterType="Custom,Numbers"
                                                                TargetControlID="TXT_ActAmount" ValidChars=".">
                                                            </cc1:FilteredTextBoxExtender>

                                                            <asp:RegularExpressionValidator ID="REVTXT_ActAmount" runat="server" ControlToValidate="TXT_ActAmount"
                                                                ErrorMessage="> 0" ForeColor="Red" ValidationExpression="^[1-9][0-9]*(\.[0-9]+)?|0+\.[0-9]*[1-9][0-9]*$"
                                                                ValidationGroup="VGAdd"></asp:RegularExpressionValidator>
                                                        </td>
                                                        <td style="width: 80px; display: none;">
                                                            <%--Period Rent:<br />--%>
                                                            <asp:Label ID="lblPeriodRent" runat="server" Width="80" />
                                                            <br />
                                                            <br />
                                                        </td>
                                                        <td style="width: 150px">
                                                            <%--Address:<br />--%>
                                                            <asp:TextBox ID="txtAddress" runat="server" Width="140" MaxLength="35" TextMode="MultiLine" ValidationGroup="VGAdd" CssClass="form-control" placeholder="Address" />
                                                            <asp:RequiredFieldValidator ID="RFVtxtAddress" runat="server" ErrorMessage="Please Enter Address"
                                                                Display="Dynamic" ForeColor="Red" ControlToValidate="txtAddress" ValidationGroup="VGAdd"></asp:RequiredFieldValidator>
                                                            <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtAddress" ID="RegularExpressionValidator2" ValidationGroup="VGAdd"
                                                                ValidationExpression="^[\s\S]{0,35}$" runat="server" ErrorMessage="Maximum 35 characters allowed." ForeColor="Red"></asp:RegularExpressionValidator>
                                                            <br />
                                                            <br />
                                                        </td>
                                                        <td style="width: 130px">
                                                            <%--State:<br />--%>
                                                            <%--<asp:TextBox ID="txtState" runat="server" Width="140" />--%>
                                                            <%--<asp:DropDownList ID="DropDownList1" runat="server" Width="130px" ValidationGroup="VGAdd" CssClass="form-control-file"></asp:DropDownList>--%>
                                                            <asp:DropDownList ID="drpdwnState" runat="server" Width="130px" ValidationGroup="VGAdd" CssClass="form-control" placeholder="State"></asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RFVdrpdwnState" runat="server" ErrorMessage="Please select State" ForeColor="Red" ControlToValidate="drpdwnState" InitialValue="0" ValidationGroup="VGAdd"></asp:RequiredFieldValidator>
                                                            <br />
                                                            <br />
                                                        </td>
                                                        <td style="width: 150px">
                                                            <%--City:<br />--%>
                                                            <asp:TextBox ID="txtCity" runat="server" Width="140" ValidationGroup="vg1" CssClass="form-control" placeholder="City" />
                                                            <asp:RequiredFieldValidator ID="RFVCity" runat="server" ErrorMessage="Please Enter City"
                                                                ForeColor="Red" ControlToValidate="txtCity" ValidationGroup="VGAdd"></asp:RequiredFieldValidator>
                                                            <br />
                                                            <br />
                                                        </td>
                                                        <td style="width: 150px">
                                                            <%--Landlord's(LL) Name:<br />--%>
                                                            <asp:TextBox ID="TXTLandLordName" runat="server" Width="140" ValidationGroup="VGAdd" CssClass="form-control" placeholder="Landlord's(LL) Name" />
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please Enter Name"
                                                                ForeColor="Red" ControlToValidate="TXTLandLordName" ValidationGroup="VGAdd"></asp:RequiredFieldValidator>
                                                        </td>
                                                        <td style="width: 150px">
                                                            <%--LL Address:<br />--%>
                                                            <asp:TextBox ID="TXTLandLordAddr" runat="server" Width="140" TextMode="MultiLine" ValidationGroup="VGAdd" CssClass="form-control" placeholder="LL Address" />
                                                            <asp:RequiredFieldValidator ID="RFV_TXTLandLordAddr" runat="server" ErrorMessage="Please Enter Address"
                                                                ForeColor="Red" ControlToValidate="TXTLandLordAddr" ValidationGroup="VGAdd"></asp:RequiredFieldValidator>
                                                            <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="TXTLandLordAddr" ID="RegularExpressionValidator3" ValidationGroup="VGAdd"
                                                                ValidationExpression="^[\s\S]{0,35}$" runat="server" ErrorMessage="Maximum 35 characters allowed." ForeColor="Red"></asp:RegularExpressionValidator>
                                                        </td>
                                                        <td style="width: 80px">
                                                            <%--LL PAN:<br />--%>
                                                            <asp:TextBox ID="TXTPANLAndLord" runat="server" Width="80" ValidationGroup="VGAdd" CssClass="form-control" ToolTip="1. Is a combination of letters and numbers having a length of 10.
                                                                            &#013;2. All letters are in upper case.
                                                                            &#013;3. A PAN card number always starts and ends with letters.
                                                                            &#013;4. Does not contain special characters.
                                                                            &#013;5. The PAN card number starts with five letters then has four digits and the last one is a letter."
                                                                placeholder="LL PAN" />
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ValidationExpression="[A-Z]{3}P[A-Z]{1}\d{4}[A-Z]{1}$" Display="Dynamic" SetFocusOnError="true"
                                                                ControlToValidate="TXTPANLAndLord" ErrorMessage="Invalid PAN Number" ForeColor="Red" ValidationGroup="VGAdd"></asp:RegularExpressionValidator>
                                                            <asp:RequiredFieldValidator ID="RFV_TXTPANLAndLord" runat="server" ErrorMessage="Please Enter PAN"
                                                                ForeColor="Red" ControlToValidate="TXTPANLAndLord" ValidationGroup="VGAdd" Enabled="false"></asp:RequiredFieldValidator>

                                                        </td>
                                                      <%--  <td style="width: 50px" align="center">
                                                            <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="Insert" ValidationGroup="VGAdd" CssClass="btn-xs btn-secondary" />
                                                        </td>--%>
                                                    </tr>
                                                    <tr>
                                                          <td>
                                                            <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="Insert" ValidationGroup="VGAdd" CssClass="btn-xs btn-secondary" />
                                                        </td>
                                                    </tr>
                                                </table>

                                                <asp:Button ID="btnHousingNext" runat="server" Text="Save & Next" OnClick="btnHousingNext_Click" TabIndex="19" CssClass="btn bg-brand-btn waves-effect waves-light btn-std"/>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </div>

                            <!-- end Tab Panel-->
                        </div>
                    </asp:View>

                    <!-- =========== Tab-4 Tab Panel ==================-->
                    <asp:View ID="View4" runat="server">

                        <div id="Tab-4">
                            <asp:Label ID="LblLockSts4" runat="server" CssClass="msgboard"></asp:Label>
                            <div id="DivOthers" runat="server">
                                <div class="tab-pane" id="Income-Other-Sources">
                                    <div class="table-responsive card-box">

                                        <div id="Div_Proptyp" runat="server">
                                            <h5 class="pb-2">Income from Other Sources </h5>

                                            <table class="table">
                                                <thead>
                                                    <tr>
                                                        <th colspan="3">
                                                            <span class="text-blue">SELECT :</span>
                                                            <asp:DropDownList ID="DDl_TYPE" runat="server" Font-Size="12px" CssClass="textbox" OnSelectedIndexChanged="DDl_TYPE_SelectedIndexChanged" AutoPostBack="true" TabIndex="1">
                                                                <asp:ListItem Text="SELECT" Value="0" Selected="True"></asp:ListItem>
                                                                <asp:ListItem Text="House Property" Value="1"></asp:ListItem>
                                                                <asp:ListItem Text="Other Sources" Value="2"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </th>
                                                    </tr>
                                                </thead>
                                            </table>


                                        </div>


                                        <div id="DIVINCOMETYP" runat="server">
                                            <asp:MultiView ID="MV_IncomeSources" runat="server">
                                                <asp:View ID="ViewHousing" runat="server">
                                                    <table class="table">
                                                        <tbody>
                                                            <tr>
                                                                <td class="alert-blue" colspan="3">Income from House Property :  From April 1<sup>st</sup>
                                                                    <asp:Label ID="LblFromDate4" runat="server"></asp:Label>
                                                                    To March 31<sup>st</sup>
                                                                    <asp:Label ID="LblToDate4" runat="server"></asp:Label></td>
                                                            </tr>

                                                            <asp:Label ID="Label3" runat="server" CssClass="msgboard"></asp:Label>
                                                        </tbody>
                                                    </table>
                                                    <div>
                                                        <table class="table">


                                                            <tr>
                                                                <td class="text-blue2" colspan="3">Property Type </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2">
                                                                    <asp:RadioButtonList ID="RB_PropTyp" runat="server" CssClass="radio" Font-Size="14px" OnSelectedIndexChanged="RB_PropTyp_SelectedIndexChanged" AutoPostBack="true" TabIndex="2" RepeatDirection="Horizontal">
                                                                        <asp:ListItem Value="1" Text=" Self Occupied House Property " Selected="True"></asp:ListItem>
                                                                        <%--<asp:ListItem Value="2" Text=" Partly Let Out House Property "></asp:ListItem>--%>
                                                                        <asp:ListItem Value="3" Text=" Let Out House Property "></asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="Td1"></td>
                                                            </tr>

                                                        </table>
                                                    </div>


                                                    <div id="Div6" runat="server" visible="true">
                                                        <fieldset>
                                                            <legend style="font-size: 17px;"><b>&nbsp;Deduction Details&nbsp;</b></legend>
                                                            <asp:GridView ID="grdSelfOccDetails" runat="server" EmptyDataText="No Record Found!" EmptyDataRowStyle-CssClass="emptygridstyle"
                                                                OnRowCommand="grdSelfOccDetails_RowCommand" OnRowDataBound="grdSelfOccDetails_RowDataBound" DataKeyNames="ID"
                                                                AutoGenerateColumns="false" GridLines="None" CssClass="gridviewNew">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Sl.No." ControlStyle-CssClass="col-center">
                                                                        <ItemTemplate>
                                                                            <%# Container.DataItemIndex+1 %>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Led. Name">
                                                                        <ItemTemplate>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Led. Addr.">
                                                                        <ItemTemplate>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Led. PAN">
                                                                        <ItemTemplate>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Addr. of Property">
                                                                        <ItemTemplate>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="State">
                                                                        <ItemTemplate>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="City">
                                                                        <ItemTemplate>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Loan Sanc. Date">
                                                                        <ItemTemplate>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Purpose of Housing loan">
                                                                        <ItemTemplate>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Carpet Area in Sq. Ft.">
                                                                        <ItemTemplate>
                                                                            <%#Eval("TDSAT") %>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Loan Sanc. Amt." HeaderStyle-CssClass="right" ItemStyle-CssClass="right">
                                                                        <ItemTemplate>
                                                                            <%#Eval("INT24","{0:#,##0.00}") %>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Value of the Property">
                                                                        <ItemTemplate>
                                                                            <%#Eval("REP24") %>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Stamp Duty/Reg. charge" HeaderStyle-CssClass="right" ItemStyle-CssClass="right">
                                                                        <ItemTemplate>
                                                                            <%#Eval("OTH24","{0:#,##0.00}") %>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Stamp Duty/Reg. charge date">
                                                                        <ItemTemplate>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Any other house Property">
                                                                        <ItemTemplate>
                                                                            <%#Eval("LETVL").ToString()=="0.00"?"No":"Yes" %>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Loan Type">
                                                                        <ItemTemplate>
                                                                            <%#Eval("BSPFT").ToString()=="1.00"?"Single":"Joint" %>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Total Interest Paid" HeaderStyle-CssClass="right" ItemStyle-CssClass="right">
                                                                        <ItemTemplate>
                                                                            <%#Eval("CPGLN","{0:#,##0.00}") %>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Total Principal Paid " HeaderStyle-CssClass="right" ItemStyle-CssClass="right">
                                                                        <ItemTemplate>
                                                                            <%#Eval("CPGLS","{0:#,##0.00}") %>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Total Interest" HeaderStyle-CssClass="right" ItemStyle-CssClass="right">
                                                                        <ItemTemplate>
                                                                            <%#Eval("CPGSS","{0:#,##0.00}") %>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Total Principal" HeaderStyle-CssClass="right" ItemStyle-CssClass="right">
                                                                        <ItemTemplate>
                                                                            <%#Eval("INTRS","{0:#,##0.00}") %>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Benefit Under Section 80 EEA">
                                                                        <ItemTemplate>
                                                                            <%#Eval("DVDND").ToString()=="0.00"?"No":"Yes" %>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Action">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="LbtnView" runat="server" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'
                                                                                CommandName="ViewR" CausesValidation="false"><i class="dripicons-article"></i></asp:LinkButton>
                                                                            <asp:LinkButton ID="LbtnDelete" runat="server" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'
                                                                                CommandName="DeleteR" CausesValidation="false"><i class="fe-trash-2"></i></asp:LinkButton>
                                                                            <asp:LinkButton ID="LbtnEdit" runat="server" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'
                                                                                CommandName="EditR" CausesValidation="false"><i class="fe-edit-1"></i></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>

                                                            <asp:GridView ID="grdLetout" runat="server" EmptyDataText="No Record Found!" EmptyDataRowStyle-CssClass="emptygridstyle"
                                                                OnRowCommand="grdLetout_RowCommand" OnRowDataBound="grdLetout_RowDataBound" DataKeyNames="ID,EMPCOMMENTS2"
                                                                AutoGenerateColumns="false" GridLines="None" CssClass="gridviewNew">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Sl.No." ControlStyle-CssClass="col-center">
                                                                        <ItemTemplate>
                                                                            <%# Container.DataItemIndex+1 %>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Led. Name">
                                                                        <ItemTemplate>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Led. Addr.">
                                                                        <ItemTemplate>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Led. PAN">
                                                                        <ItemTemplate>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Addr. of Property">
                                                                        <ItemTemplate>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="State">
                                                                        <ItemTemplate>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="City">
                                                                        <ItemTemplate>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Loan Sanc. Date">
                                                                        <ItemTemplate>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Purpose of Housing loan">
                                                                        <ItemTemplate>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <%--  <asp:TemplateField HeaderText="Carpet Area in Sq. Ft.">
                                                                        <ItemTemplate>
                                                                            <%#Eval("TDSAT") %>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Loan Sanc. Amt.">
                                                                        <ItemTemplate>
                                                                            <%#Eval("INT24") %>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Value of the Property">
                                                                        <ItemTemplate>
                                                                            <%#Eval("REP24") %>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Stamp Duty/Reg. charge">
                                                                        <ItemTemplate>
                                                                            <%#Eval("OTH24") %>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Stamp Duty/Reg. charge date">
                                                                        <ItemTemplate>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Any other house Property">
                                                                        <ItemTemplate>
                                                                            <%#Eval("LETVL").ToString()=="0.00"?"No":"Yes" %>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Loan Type">
                                                                        <ItemTemplate>
                                                                            <%#Eval("BSPFT").ToString()=="1.00"?"Single":"Joint" %>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                 <asp:TemplateField HeaderText="Total Interest Paid">
                                                                        <ItemTemplate>
                                                                            <%#Eval("CPGLN") %>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Total Principal Paid ">
                                                                        <ItemTemplate>
                                                                            <%#Eval("CPGLS") %>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>--%>
                                                                    <%-- <asp:TemplateField HeaderText="Total Interest">
                                                                        <ItemTemplate>
                                                                            <%#Eval("CPGSS") %>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Total Principal">
                                                                        <ItemTemplate>
                                                                            <%#Eval("INTRS") %>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Benefit Under Section 80 EEA">
                                                                        <ItemTemplate>
                                                                            <%#Eval("DVDND").ToString()=="0.00"?"No":"Yes" %>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>--%>
                                                                    <asp:TemplateField HeaderText="Action">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="LbtnView" runat="server" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'
                                                                                CommandName="ViewR" CausesValidation="false"><i class="dripicons-article"></i></asp:LinkButton>
                                                                            <asp:LinkButton ID="LbtnDelete" runat="server" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'
                                                                                CommandName="DeleteR" CausesValidation="false"><i class="fe-trash-2"></i></asp:LinkButton>
                                                                            <asp:LinkButton ID="LbtnEdit" runat="server" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'
                                                                                CommandName="EditR" CausesValidation="false"><i class="fe-edit-1"></i></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>

                                                            <br />
                                                            <table class="table" id="Table1" runat="server">
                                                                <tr>
                                                                    <td style="width: 20%">Lenders Name </td>
                                                                    <td style="width: 30%">
                                                                        <asp:TextBox ID="txtLendrName" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtLendrName_TextChanged"></asp:TextBox></td>
                                                                    <td style="width: 20%">Address of Property for which loan is taken		
                                                                    </td>
                                                                    <td style="width: 30%">
                                                                        <asp:TextBox ID="txtAddrPropty" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>Lenders Address </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtLendrAdd" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox></td>
                                                                    <td>State </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtITSlfState" runat="server" CssClass="form-control"></asp:TextBox></td>
                                                                </tr>

                                                                <tr>
                                                                    <td>Pan of the Lender		
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtLendrPAN" runat="server" CssClass="form-control" ToolTip="1. Is a combination of letters and numbers having a length of 10.
                                                                            &#013;2. All letters are in upper case.
                                                                            &#013;3. Does not contain special characters.
                                                                            &#013;4. The PAN card number starts with five letters then has four digits and the last one is a letter."></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="FTBE_txtLendrPAN" runat="server" FilterMode="ValidChars" FilterType="Numbers,UppercaseLetters"
                                                                            TargetControlID="txtLendrPAN">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                        <asp:RegularExpressionValidator ID="rgxPANCard" runat="server" ValidationExpression="[A-Z]{5}\d{4}[A-Z]{1}$" Display="Dynamic" SetFocusOnError="true"
                                                                            ControlToValidate="txtLendrPAN" ErrorMessage="Invalid PAN Number" ForeColor="Red"></asp:RegularExpressionValidator>
                                                                    </td>

                                                                    <td>City 		
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtITSlfCity" runat="server" CssClass="form-control"></asp:TextBox></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>Loan Sanction Date </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtLoanSacDt" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <cc1:MaskedEditExtender ID="MEEtxtLoanSacDt" runat="server" AcceptNegative="Left"
                                                                            CultureName="en-GB" DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999"
                                                                            MaskType="Date" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                                                                            OnInvalidCssClass="MaskedEditError" TargetControlID="txtLoanSacDt" />
                                                                        <cc1:CalendarExtender ID="CEtxtLoanSacDt" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                                            TargetControlID="txtLoanSacDt">
                                                                        </cc1:CalendarExtender>
                                                                    </td>
                                                                    <td>Purpose of Housing loan 		
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtPurofHsLoan" MaxLength="69" runat="server" CssClass="form-control"></asp:TextBox></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>Loan Sanctioned Amount </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtLoanSacAmt" runat="server" CssClass="form-control right"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="FTBEtxtLoanSacAmt" runat="server" FilterMode="ValidChars" FilterType="Custom,Numbers"
                                                                            TargetControlID="txtLoanSacAmt" ValidChars=".">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                    </td>
                                                                    <td>Carpet Area in Sq. Ft. </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtCaptSqFt" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="FTBEtxtCaptSqFt" runat="server" FilterMode="ValidChars" FilterType="Custom,Numbers"
                                                                            TargetControlID="txtCaptSqFt" ValidChars=".">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                    </td>

                                                                </tr>
                                                                <tr>
                                                                    <td>Stamp Duty/Registration charge if paid in current Financial year  		
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtStampDuty" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="FTBEtxtStampDuty" runat="server" FilterMode="ValidChars" FilterType="Custom,Numbers"
                                                                            TargetControlID="txtStampDuty" ValidChars=".">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                    </td>
                                                                    <td>Value of the Property </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtValPropty" runat="server" CssClass="form-control right"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="FTBEtxtValPropty" runat="server" FilterMode="ValidChars" FilterType="Custom,Numbers"
                                                                            TargetControlID="txtValPropty" ValidChars=".">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                    </td>

                                                                </tr>
                                                                <tr>

                                                                    <td>Do you own any other house Property 		
                                                                    </td>
                                                                    <td>
                                                                        <asp:CheckBox ID="chkOwnhsPropty" runat="server" CssClass="form-control-file" Text="(if Yes check the Box)"></asp:CheckBox>

                                                                    </td>
                                                                    <td>Stamp Duty/Registration charge date </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtStampChgrDt" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <cc1:MaskedEditExtender ID="MEEtxtStampChgrDt" runat="server" AcceptNegative="Left"
                                                                            CultureName="en-GB" DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999"
                                                                            MaskType="Date" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                                                                            OnInvalidCssClass="MaskedEditError" TargetControlID="txtStampChgrDt" />
                                                                        <cc1:CalendarExtender ID="CEtxtStampChgrDt" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                                            TargetControlID="txtStampChgrDt">
                                                                        </cc1:CalendarExtender>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>Loan Type </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="ddlSlfLoanType" runat="server" CssClass="form-control">
                                                                            <asp:ListItem Value="1.00" Selected="True">Single</asp:ListItem>
                                                                            <asp:ListItem Value="2.00">Joint</asp:ListItem>
                                                                        </asp:DropDownList></td>
                                                                    <td colspan="2">


                                                                        <table class="gridviewNew">
                                                                            <%-- <tr class="text-center font-weight-bold">
                                                                                    <td colspan="4">
                                                                                        <h5>Borrowers Details</h5>
                                                                                    </td>
                                                                                </tr>--%>
                                                                            <tr>
                                                                                <th>Sl. No.</th>
                                                                                <th>Borrowers Name</th>
                                                                                <th>Percentange % of Contribution</th>
                                                                                <th>Action</th>
                                                                            </tr>
                                                                            <tr>
                                                                                <td></td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtBorwName" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                    <cc1:FilteredTextBoxExtender ID="FTBEtxtBorwName" runat="server" FilterMode="ValidChars" FilterType="LowercaseLetters,Custom,UppercaseLetters"
                                                                                        TargetControlID="txtBorwName" ValidChars=" .">
                                                                                    </cc1:FilteredTextBoxExtender>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtBorwPerct" runat="server" CssClass="form-control" ValidationGroup="borw"></asp:TextBox>
                                                                                    <cc1:FilteredTextBoxExtender ID="FTBEtxtBorwPerct" runat="server" FilterMode="ValidChars" FilterType="Custom,Numbers"
                                                                                        TargetControlID="txtBorwPerct" ValidChars=".">
                                                                                    </cc1:FilteredTextBoxExtender>
                                                                                    <asp:RangeValidator ID="RangeValidator1" runat="server" ValidationGroup="borw"
                                                                                        ControlToValidate="txtBorwPerct" Display="Dynamic" ForeColor="Red"
                                                                                        ErrorMessage="Invalid Percentage" MaximumValue="100.00" MinimumValue="0.00"
                                                                                        Type="Double"></asp:RangeValidator>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:LinkButton ID="lnkborAdd" runat="server" ValidationGroup="borw" CssClass="btn-xs btn-warning" OnClick="lnkborAdd_Click"><i class="fe-plus"></i></asp:LinkButton></td>
                                                                            </tr>
                                                                        </table>
                                                                        <asp:GridView ID="grdBorwDetails" runat="server" OnRowCommand="grdBorwDetails_RowCommand" EmptyDataText="No Record Found!" EmptyDataRowStyle-CssClass="emptygridstyle" DataKeyNames="ID" AutoGenerateColumns="false" GridLines="None" CssClass="gridviewNew">
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="" ControlStyle-CssClass="col-center">
                                                                                    <ItemTemplate>
                                                                                        <%# Container.DataItemIndex+1 %>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField>
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblGrdBorwName" runat="server" Text='<%#Eval("Name") %>'></asp:Label>
                                                                                        <%-- <asp:TextBox ID="txtGrdBorwName" runat="server" CssClass="form-control-file"></asp:TextBox>
                                                                                        <cc1:FilteredTextBoxExtender ID="FTBEtxtGrdBorwName" runat="server" FilterMode="ValidChars" FilterType="LowercaseLetters,Custom,UppercaseLetters"
                                                                                            TargetControlID="txtGrdBorwName" ValidChars=".">
                                                                                        </cc1:FilteredTextBoxExtender>--%>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField>
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblGrdBorwPernct" runat="server" Text='<%#Eval("PERCNT") %>'></asp:Label>
                                                                                        <%-- <asp:TextBox ID="txtGrdBorwPernct" runat="server" CssClass="form-control-file"></asp:TextBox>
                                                                                        <cc1:FilteredTextBoxExtender ID="FTBEtxtGrdBorwPernct" runat="server" FilterMode="ValidChars" FilterType="Custom,Numbers"
                                                                                            TargetControlID="txtGrdBorwPernct" ValidChars=".">
                                                                                        </cc1:FilteredTextBoxExtender>--%>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField>
                                                                                    <ItemTemplate>
                                                                                        <asp:LinkButton ID="LbtnDelete" runat="server" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'
                                                                                            CommandName="DeleteR" CausesValidation="false"><i class="fe-trash-2"></i></asp:LinkButton>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                        </asp:GridView>

                                                                    </td>
                                                                </tr>
                                                                <%--<tr>
                                                                    <td>Total Interest Paid	 		
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control right" AutoPostBack="true" OnTextChanged="txtTotInsPaid_TextChanged"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterMode="ValidChars" FilterType="Custom,Numbers"
                                                                            TargetControlID="txtTotInsPaid" ValidChars=".">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                    </td>
                                                                    <td>Total Principal Paid </td>
                                                                    <td>
                                                                        <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control right" AutoPostBack="true" OnTextChanged="txtTotPrinPaid_TextChanged"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" FilterMode="ValidChars" FilterType="Custom,Numbers"
                                                                            TargetControlID="txtTotPrinPaid" ValidChars=".">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                    </td>

                                                                </tr>--%>
                                                                <tr>
                                                                    <td>Total Interest Paid <code>*</code>	
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtTotInsPaid" runat="server" CssClass="form-control right" AutoPostBack="true" OnTextChanged="txtTotInsPaid_TextChanged"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="FTBEtxtTotInsPaid" runat="server" FilterMode="ValidChars" FilterType="Custom,Numbers"
                                                                            TargetControlID="txtTotInsPaid" ValidChars=".">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                        <asp:RequiredFieldValidator ID="RFV_txtTotInsPaid" runat="server" ControlToValidate="txtTotInsPaid" ErrorMessage="Please Enter Total Interest Paid" ForeColor="Red"></asp:RequiredFieldValidator>
                                                                        <asp:CompareValidator ID="CV_txtTotInsPaid" runat="server" ControlToValidate="txtTotInsPaid" ErrorMessage="Must be &gt; 0" Operator="GreaterThan" Type="Double" ValueToCompare="0"  ForeColor="Red"/>
                                                                    </td>
                                                                    <td>Total Principal Paid <code>*</code></td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtTotPrinPaid" runat="server" CssClass="form-control right" AutoPostBack="true" OnTextChanged="txtTotPrinPaid_TextChanged"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="FTBEtxtTotPrinPaid" runat="server" FilterMode="ValidChars" FilterType="Custom,Numbers"
                                                                            TargetControlID="txtTotPrinPaid" ValidChars=".">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                        <asp:RequiredFieldValidator ID="RFV_txtTotPrinPaid" runat="server" ControlToValidate="txtTotPrinPaid" ErrorMessage="Please Enter Total Principal Paid" ForeColor="Red"></asp:RequiredFieldValidator>
                                                                        <asp:CompareValidator ID="CV_txtTotPrinPaid" runat="server" ControlToValidate="txtTotPrinPaid" ErrorMessage="Must be &gt; 0" Operator="GreaterThan" Type="Double" ValueToCompare="0"  ForeColor="Red"/>
                                                                    </td>

                                                                </tr>
                                                                <tr class="text-center font-weight-bold">
                                                                    <td colspan="4">
                                                                        <h5>Housing Loan Interest & Principal considered for Exemption</h5>
                                                                    </td>
                                                                </tr>
                                                                <tr>

                                                                    <td>
                                                                        <div runat="server" id="DIVins1" visible="false">Total Interest	 </div>
                                                                    </td>
                                                                    <td>
                                                                        <div runat="server" id="DIVins" visible="false">
                                                                            <asp:TextBox ID="txtTotIns" runat="server" CssClass="form-control right" Enabled="false"></asp:TextBox>
                                                                            <cc1:FilteredTextBoxExtender ID="FTBEtxtTotIns" runat="server" FilterMode="ValidChars" FilterType="Custom,Numbers"
                                                                                TargetControlID="txtTotIns" ValidChars=".">
                                                                            </cc1:FilteredTextBoxExtender>
                                                                        </div>
                                                                    </td>
                                                                    <td></td>
                                                                    <td></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>Total Principal		 		
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtTotPrincpl" runat="server" CssClass="form-control right" Enabled="false"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="FTBEtxtTotPrincpl" runat="server" FilterMode="ValidChars" FilterType="Custom,Numbers"
                                                                            TargetControlID="txtTotPrincpl" ValidChars=".">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                    </td>
                                                                    <td>Benefit Under Section 80 EEA </td>
                                                                    <td>
                                                                        <asp:CheckBox ID="chkBenftsSec80EE" runat="server" CssClass="form-control-file" Text="(if Yes check the Box)"></asp:CheckBox>

                                                                        <%--<asp:TextBox ID="txtBenftsSec80EE" runat="server" CssClass="form-control-file"></asp:TextBox>--%>
                                                                        <%-- <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterMode="ValidChars" FilterType="Custom,Numbers"
                                                                            TargetControlID="txtTotPrinPaid" ValidChars=".">
                                                                        </cc1:FilteredTextBoxExtender>--%>
                                                                    </td>

                                                                </tr>

                                                            </table>

                                                        </fieldset>

                                                    </div>

                                                    <div id="Div7" runat="server" visible="false">
                                                        <fieldset>
                                                            <legend style="font-size: 17px;"><b>&nbsp;Deduction Details&nbsp;</b></legend>
                                                            <table class="table" id="Table2" runat="server">
                                                                <tr>
                                                                    <td style="width: 25%">Rental Income received in the current from Apr to Mar in current Financial year </td>
                                                                    <td>
                                                                        <asp:TextBox ID="TXT_FNLLETVALUE" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="TXT_FNLLETVALUE_TextChanged1" Text="0.0" TabIndex="4"></asp:TextBox></td>
                                                                    <td>INR
                                                                        <cc1:FilteredTextBoxExtender ID="FTB_TXT_FNLLETVALUE" runat="server" FilterMode="ValidChars" FilterType="Custom,Numbers"
                                                                            TargetControlID="TXT_FNLLETVALUE" ValidChars=".">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="TXT_FNLLETVALUE" ErrorMessage="Must be &gt; 0" Operator="GreaterThan" Type="Double" ValueToCompare="0"  ForeColor="Red"/>
                                                                    </td>

                                                                </tr>
                                                                <tr>
                                                                    <td>Municipal Tax paid for the year	 </td>
                                                                    <td>
                                                                        <asp:Label ID="Lbl_DEDREPAIR" runat="server" CssClass="hidden" Text="0"></asp:Label>
                                                                        <asp:TextBox ID="txtMunicipaltax" runat="server" CssClass="form-control" Text="0.0" AutoPostBack="true" OnTextChanged="txtMunicipaltax_TextChanged" TabIndex="6"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterMode="ValidChars" FilterType="Custom,Numbers"
                                                                            TargetControlID="txtMunicipaltax" ValidChars=".">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                        <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="txtMunicipaltax" ErrorMessage="Must be &gt; 0" Operator="GreaterThan" Type="Double" ValueToCompare="0"  ForeColor="Red"/>
                                                                    </td>
                                                                    <td>INR</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>Net Annual Value</td>
                                                                    <td>
                                                                        <asp:TextBox ID="TXT_DEDINT" runat="server" CssClass="form-control right" TabIndex="5" Enabled="false" Text="0.0"></asp:TextBox></td>
                                                                    <td>INR   
                                                                        <cc1:FilteredTextBoxExtender ID="FTB_TXT_DEDINT" runat="server" FilterMode="ValidChars" FilterType="Custom,Numbers"
                                                                            TargetControlID="TXT_DEDINT" ValidChars=".">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                    </td>

                                                                </tr>
                                                                <tr>
                                                                    <td>Repairs & Maintenance charge(30%)</td>
                                                                    <td>
                                                                        <asp:TextBox ID="TXTDECOTHERS" runat="server" CssClass="form-control right" Text="0.0" Enabled="false" TabIndex="6"></asp:TextBox>
                                                                    </td>
                                                                    <td>INR   
                                                                        <cc1:FilteredTextBoxExtender ID="FTB_TXTDECOTHERS" runat="server" FilterMode="ValidChars" FilterType="Custom,Numbers"
                                                                            TargetControlID="TXTDECOTHERS" ValidChars=".">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>Loan Type</td>
                                                                    <td>
                                                                        <asp:DropDownList ID="ddlLoanType" runat="server" CssClass="form-control">
                                                                            <asp:ListItem Value="1.00" Selected="True">Single</asp:ListItem>
                                                                            <asp:ListItem Value="2.00">Joint</asp:ListItem>
                                                                        </asp:DropDownList></td>
                                                                    <td></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>Self Percentange of Loan</td>
                                                                    <td>
                                                                        <asp:TextBox ID="TXTTDS" runat="server" CssClass="form-control" TabIndex="7"></asp:TextBox></td>
                                                                    <td>% 
                                                                        <cc1:FilteredTextBoxExtender ID="FTB_TXTTDS" runat="server" FilterMode="ValidChars" FilterType="Custom,Numbers"
                                                                            TargetControlID="TXTTDS" ValidChars=".">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                        <asp:RangeValidator ID="percentageRangeValidator" runat="server"
                                                                            ControlToValidate="TXTTDS" Display="Dynamic" ForeColor="Red"
                                                                            ErrorMessage="Invalid Percentage" MaximumValue="100.00" MinimumValue="0.00"
                                                                            Type="Double"></asp:RangeValidator>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>Total Interest Portion	</td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtTotintPort" runat="server" CssClass="form-control right" AutoPostBack="true" OnTextChanged="txtTotintPort_TextChanged" TabIndex="7"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterMode="ValidChars" FilterType="Custom,Numbers"
                                                                            TargetControlID="txtTotintPort" ValidChars=".">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                        <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="txtTotintPort" ErrorMessage="Must be &gt; 0" Operator="GreaterThan" Type="Double" ValueToCompare="0"  ForeColor="Red"/>
                                                                    </td>
                                                                    <td>INR</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>Income /Loss on House Property considered for Exemption	 </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtIncm_loss" runat="server" CssClass="form-control right" Enabled="false" TabIndex="7"></asp:TextBox></td>
                                                                    <td>INR 
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterMode="ValidChars" FilterType="Custom,Numbers"
                                                                            TargetControlID="txtIncm_loss" ValidChars=".">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                    </td>
                                                                </tr>
                                                            </table>

                                                        </fieldset>

                                                    </div>

                                                    <div runat="server" id="DIVCOMMENTS">

                                                        <table class="table">

                                                            <tr>
                                                                <td width="20%">Comments</td>
                                                                <td width="40%">
                                                                    <asp:TextBox ID="TXTCOMMENTS" runat="server" TextMode="MultiLine" CssClass="form-control"
                                                                        TabIndex="8"></asp:TextBox></td>
                                                                <td>&nbsp;</td>
                                                                <td>&nbsp;</td>
                                                                <td>&nbsp;</td>
                                                            </tr>

                                                        </table>
                                                    </div>


                                                </asp:View>

                                                <asp:View ID="ViewOtherSources" runat="server">
                                                    <h4>Income from Other Sources :  From April 1<sup>st</sup>
                                                        <asp:Label ID="LblFrom" runat="server"></asp:Label>
                                                        To March 31<sup>st</sup>
                                                        <asp:Label ID="LblTo" runat="server"></asp:Label></h4>
                                                    <asp:Label ID="LblMsg" runat="server" CssClass="msgboard"></asp:Label>
                                                    <div>
                                                        <table class="table">
                                                            <tr class="hidden">
                                                                <td width="20%">1. Business Profits </td>
                                                                <td>
                                                                    <asp:TextBox ID="TXT_BusnProfits" runat="server" CssClass="form-control right" TabIndex="9"></asp:TextBox></td>
                                                                <td>INR
                                                                    <cc1:FilteredTextBoxExtender ID="FTB_TXT_BusnProfits" runat="server" FilterMode="ValidChars" FilterType="Custom,Numbers"
                                                                        TargetControlID="TXT_BusnProfits" ValidChars=".">
                                                                    </cc1:FilteredTextBoxExtender>
                                                                </td>
                                                            </tr>


                                                            <tr class="hidden">
                                                                <td>2. Long-Term Capital Gains </td>
                                                                <td>
                                                                    <asp:TextBox ID="TXT_LTCG" runat="server" CssClass="form-control" TabIndex="10"></asp:TextBox></td>
                                                                <td>INR  (Normal Rate) 
                                                                    <cc1:FilteredTextBoxExtender ID="FTB_TXT_LTCG" runat="server" FilterMode="ValidChars" FilterType="Custom,Numbers"
                                                                        TargetControlID="TXT_LTCG" ValidChars=".">
                                                                    </cc1:FilteredTextBoxExtender>
                                                                </td>
                                                            </tr>

                                                            <tr class="hidden">
                                                                <td>3. Long-Term Capital Gains </td>
                                                                <td>
                                                                    <asp:TextBox ID="TXT_LTCGS" runat="server" CssClass="form-control" TabIndex="11"></asp:TextBox></td>
                                                                <td>INR  (Special Rate) 
                                                                    <cc1:FilteredTextBoxExtender ID="FTB_TXT_LTCGS" runat="server" FilterMode="ValidChars" FilterType="Custom,Numbers"
                                                                        TargetControlID="TXT_LTCGS" ValidChars=".">
                                                                    </cc1:FilteredTextBoxExtender>
                                                                </td>
                                                            </tr>

                                                            <tr class="hidden">
                                                                <td>4. Short-Term Capital Gains </td>
                                                                <td>
                                                                    <asp:TextBox ID="TXT_STCG" runat="server" CssClass="form-control" TabIndex="12"></asp:TextBox></td>
                                                                <td>INR  
                                                                    <cc1:FilteredTextBoxExtender ID="FTB_TXT_STCG" runat="server" FilterMode="ValidChars" FilterType="Custom,Numbers"
                                                                        TargetControlID="TXT_STCG" ValidChars=".">
                                                                    </cc1:FilteredTextBoxExtender>
                                                                </td>
                                                            </tr>

                                                            <tr class="hidden">
                                                                <td>5. Short-Term Capital Gains</td>
                                                                <td>
                                                                    <asp:TextBox ID="TXT_STCGLS" runat="server" CssClass="form-control" TabIndex="13"></asp:TextBox></td>
                                                                <td>INR  (Listed Securities)
                                                                    <cc1:FilteredTextBoxExtender ID="FTB_TXT_STCGLS" runat="server" FilterMode="ValidChars" FilterType="Custom,Numbers"
                                                                        TargetControlID="TXT_STCGLS" ValidChars=".">
                                                                    </cc1:FilteredTextBoxExtender>
                                                                </td>
                                                            </tr>

                                                            <tr class="hidden">
                                                                <td>6. Income From Dividend </td>
                                                                <td>
                                                                    <asp:TextBox ID="TXT_IFD" runat="server" CssClass="form-control" TabIndex="14"></asp:TextBox></td>
                                                                <td>INR
                                                                    <cc1:FilteredTextBoxExtender ID="FTB_TXT_IFD" runat="server" FilterMode="ValidChars" FilterType="Custom,Numbers"
                                                                        TargetControlID="TXT_IFD" ValidChars=".">
                                                                    </cc1:FilteredTextBoxExtender>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <%--<td>1. Income from Interest</td>--%>
                                                                <td>1. Income from Savings Bank interest</td>
                                                                <td>
                                                                    <asp:TextBox ID="TXT_IFI" runat="server" CssClass="form-control right" TabIndex="15"></asp:TextBox></td>
                                                                <td>INR 
                                                                    <cc1:FilteredTextBoxExtender ID="FTB_TXT_IFI" runat="server" FilterMode="ValidChars" FilterType="Custom,Numbers"
                                                                        TargetControlID="TXT_IFI" ValidChars=".">
                                                                    </cc1:FilteredTextBoxExtender>
                                                                </td>
                                                            </tr>

                                                            <tr class="hidden">
                                                                <td>2. Other Income Unspecified</td>
                                                                <td>
                                                                    <asp:TextBox ID="TXT_OI" runat="server" CssClass="form-control" TabIndex="16"></asp:TextBox></td>
                                                                <td>INR 
                                                                    <cc1:FilteredTextBoxExtender ID="FTB_TXT_OI" runat="server" FilterMode="ValidChars" FilterType="Custom,Numbers"
                                                                        TargetControlID="TXT_OI" ValidChars=".">
                                                                    </cc1:FilteredTextBoxExtender>
                                                                </td>
                                                            </tr>

                                                            <tr class="hidden">
                                                                <td>9. TDS On Other Income </td>
                                                                <td>
                                                                    <asp:TextBox ID="TXT_TDSI" runat="server" CssClass="form-control" TabIndex="17"></asp:TextBox></td>
                                                                <td>INR
                                                                    <cc1:FilteredTextBoxExtender ID="FTB_TXT_TDSI" runat="server" FilterMode="ValidChars" FilterType="Custom,Numbers"
                                                                        TargetControlID="TXT_TDSI" ValidChars=".">
                                                                    </cc1:FilteredTextBoxExtender>
                                                                </td>
                                                            </tr>

                                                            <tr>
                                                                <td>Comments</td>
                                                                <td colspan="2">
                                                                    <asp:TextBox ID="TXTCOMMENTS2" runat="server" CssClass="form-control" TextMode="MultiLine"
                                                                        TabIndex="18"></asp:TextBox></td>
                                                            </tr>

                                                        </table>
                                                    </div>


                                                </asp:View>
                                            </asp:MultiView>
                                        </div>




                                        <div class="mb-3">
                                            <asp:Button ID="BTNSubmitHousingOthers" runat="server" Text="Save" OnClick="BTNSubmitHousingOthers_Click" TabIndex="19" CssClass="btn bg-brand-btn waves-effect waves-light btn-std" />
                                            <asp:Button ID="BtnEDIT4" runat="server" Text="Edit" OnClick="BtnEDIT4_Click" TabIndex="20" CssClass="btn bg-brand-btn waves-effect waves-light btn-std" />
                                            <asp:Button ID="btnUpdate4" runat="server" Text="Save" OnClick="btnUpdateITHousingOthers_Click" CausesValidation="true" TabIndex="21" CssClass="btn bg-brand-btn waves-effect waves-light btn-std" />
                                            <asp:Button ID="BtnCancel4" runat="server" Text="Cancel" OnClick="BtnCancel4_Click" TabIndex="22" CssClass="btn bg-brand-btn waves-effect waves-light btn-std" />
                                        </div>

                                    </div>
                                </div>


                                <%--   <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                    <ContentTemplate>
                                        <div class="row">
                                            <div class="col-sm-2">Year End Only : Upload IT file </div>
                                            <div class="col-sm-8">
                                                <asp:FileUpload ID="FU_ItAllFiles" runat="server" CssClass="form-control" ToolTip="Upload files related to IT current financial year (if multiple files add to compress file and upload)" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ErrorMessage="Please select file..!" ControlToValidate="FU_ItAllFiles"
                                                    runat="server" Display="Dynamic" ForeColor="Red" SetFocusOnError="true" ValidationGroup="VF" Enabled="false" />
                                                <asp:RegularExpressionValidator ID="REV_FU_ItAllFiles" ValidationExpression="([a-zA-Z0-9\s_\\.\-:])+(.zip|.rar|.pdf)$"
                                                    ControlToValidate="FU_ItAllFiles" runat="server" ForeColor="Red" ErrorMessage="Please select a valid (.rar/.zip/.pdf) file..!"
                                                    Display="Dynamic" ValidationGroup="VF" Enabled="false" />
                                            </div>
                                        </div>

                                        <br />
                                        <div class="row">

                                            <div class="col-sm-2">
                                                <asp:Button ID="btnView12b" runat="server" ToolTip="Enables details of 12 B" Text="Download 12 B" OnClick="btnView12b_Click" CssClass="btn bg-brand-btn waves-effect waves-light " />
                                            </div>
                                            <div class="col-sm-1">
                                                <asp:Button ID="btnSubmitAll" runat="server" ValidationGroup="VF" ToolTip="All details of IT will be submitted to approval" Text="Submit All" OnClick="btnSubmitAll_Click" CssClass="btn bg-brand-btn waves-effect waves-light btn-std" />


                                            </div>

                                        </div>

                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="btnSubmitAll" />

                                        <asp:PostBackTrigger ControlID="btnView12b" />

                                    </Triggers>
                                </asp:UpdatePanel>--%>
                            </div>
                        </div>

                        <%--<asp:Button ID="btnSubmitAll" runat="server" Text="Submit All" OnClick="btnSubmitAll_Click" CssClass="btn bg-brand-btn waves-effect waves-light btn-std" />--%>
                    </asp:View>

                    <!-- =========== Tab-5 Tab Panel ==================-->
                    <asp:View ID="View5" runat="server">
                        <div class="table-responsive card-box">
                            <div id="Tab-5">
                                <h4>Income Tax History </h4>
                                <%--from April 1<sup>st</sup> <asp:Label ID="LblFromDate" runat="server"></asp:Label> To March 31<sup>st</sup> <asp:Label ID="LblToDate" runat="server"></asp:Label></h2>
                                --%>

                                <div style="width: 99%" class="cb">

                                    <asp:Button Text="Section 80C" BorderStyle="None" ID="Tab2H" CssClass="Initial" runat="server"
                                        OnClick="Tab2H_Click" />
                                    <asp:Button Text="Section 80" BorderStyle="None" ID="Tab1H" CssClass="Initial" runat="server"
                                        OnClick="Tab1H_Click" />
                                    <asp:Button Text="Housing" BorderStyle="None" ID="Tab3H" CssClass="Initial" runat="server"
                                        OnClick="Tab3H_Click" />
                                    <asp:Button Text="Previous Employment Income" BorderStyle="None" CssClass="Initial" runat="server"
                                        OnClick="Tab5H_Click" ID="Tab5H" />
                                    <asp:Button Text="Income from Other Sources" BorderStyle="None" ID="Tab4H" CssClass="Initial" runat="server"
                                        OnClick="Tab4H_Click" />

                                </div>
                                <br />
                                <div class="row">

                                    <div class="col-sm-2">

                                        <asp:DropDownList ID="ddlSeachSelect" runat="server" CssClass="form-control" TabIndex="1">
                                            <asp:ListItem Text="-SELECT-" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="IT ID" Value="3"></asp:ListItem>
                                            <asp:ListItem Text="Status" Value="4"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-2">
                                        <asp:TextBox ID="txtsearch" runat="server" CssClass="form-control" placeholder="Enter Text" TabIndex="2"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-2">
                                        <asp:TextBox ID="txtFromDateH" runat="server" TabIndex="3" placeholder="Select From Date" CssClass="form-control"></asp:TextBox>
                                        <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" AcceptNegative="Left"
                                            CultureName="en-GB" DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999"
                                            MaskType="Date" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                                            OnInvalidCssClass="MaskedEditError" TargetControlID="txtFromDateH" />
                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                            TargetControlID="txtFromDateH">
                                        </cc1:CalendarExtender>
                                    </div>
                                    <div class="col-sm-2">
                                        <asp:TextBox ID="txtTodateH" runat="server" TabIndex="4" placeholder="Select To Date " CssClass="form-control"></asp:TextBox>
                                        <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" AcceptNegative="Left"
                                            CultureName="en-GB" DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999"
                                            MaskType="Date" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                                            OnInvalidCssClass="MaskedEditError" TargetControlID="txtTodateH" />
                                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                            TargetControlID="txtTodateH">
                                        </cc1:CalendarExtender>
                                    </div>
                                    <div class="col-sm-2">
                                        <asp:Button ID="btnsearch" runat="server" CssClass="btn btn-xs btn-secondary" CausesValidation="false" OnClick="btnsearch_Click" TabIndex="5" Text="Search" />

                                        <asp:Button ID="btnclear" runat="server" CssClass="btn btn-xs btn-secondary" OnClick="btnclear_Click" TabIndex="5" Text="Clear" />
                                        <asp:HiddenField ID="HFTabID" runat="server" />

                                    </div>

                                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                        <ContentTemplate>
                                            <div class="col-sm-2 ">



                                                <asp:LinkButton runat="server" CausesValidation="false" ToolTip="Download Submitted File" ID="lnkDwnlITFile" class="btn btn-xs btn-secondary" OnClick="lnkDwnlITFile_Click"><i class="fe-download"></i><%--Style="background: #6c757d !important; color: #FFF !important;"--%>
                                                </asp:LinkButton>
                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:PostBackTrigger ControlID="lnkDwnlITFile" />
                                        </Triggers>
                                    </asp:UpdatePanel>

                                </div>
                                <br />
                                <div class="DivSpacer01"></div>

                                <div class="cb">
                                    <asp:MultiView ID="MultiViewH" runat="server">
                                        <asp:View ID="View1H" runat="server">


                                            <asp:Label ID="Label1" runat="server" CssClass="msgboard"></asp:Label>
                                            <asp:GridView ID="GVSec80Header" runat="server" CssClass="gridviewNew" GridLines="None" AutoGenerateColumns="False" BorderStyle="None" DataKeyNames="ID,BEGDA,ENDDA,CREATED_ON,CONACTPROP,APPROVEDON,REMARKS,STATUS"
                                                Width="85%" OnRowCommand="GVSec80Header_RowCommand" AllowPaging="true" PageSize="15" OnPageIndexChanging="GVSec80Header_PageIndexChanging"
                                                AllowSorting="true" OnSorting="GVSec80Header_Sorting" TabIndex="7">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="No.">
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex+1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField HeaderText="ID" DataField="ID"></asp:BoundField>
                                                    <asp:TemplateField HeaderText="IT Type">
                                                        <ItemTemplate>
                                                            Section 80 
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="BEGDA" HeaderText="From Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                                    <asp:BoundField DataField="ENDDA" HeaderText="To Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                                    <%-- <asp:BoundField DataField="CONACTPROP" HeaderText="Consider Actuals" />--%>

                                                    <asp:TemplateField HeaderText="Consider Actuals">
                                                        <ItemTemplate>

                                                            <%#(Eval("CONACTPROP").ToString().Trim()=="0") ? "No" : (Eval("CONACTPROP").ToString().Trim()=="1")? "Yes": "-"%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:BoundField DataField="CREATED_ON" HeaderText="Submitted On" DataFormatString="{0:dd-MMM-yyyy}" />
                                                    <%--  <asp:BoundField DataField="APPROVEDON" HeaderText="Approved On" DataFormatString="{0:dd-MMM-yyyy}" />--%>

                                                    <asp:TemplateField HeaderText="Approved On">
                                                        <ItemTemplate>
                                                            <%#(Eval("APPROVEDON","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APPROVEDON","{0:dd-MM-yyyy}")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>


                                                    <%--<td><%#(Eval("APPROVED_ON2","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APPROVED_ON2","{0:dd-MM-yyyy}")%></td>--%>
                                                    <asp:BoundField DataField="REMARKS" HeaderText="Remarks" />

                                                    <%--<asp:BoundField HeaderText="Status" DataField="STATUS"></asp:BoundField>--%>
                                                    <asp:TemplateField HeaderText="Status">
                                                        <ItemTemplate>

                                                            <asp:LinkButton ID="lbtnStatus" runat="server" CssClass='<%#Eval("STATUS").ToString() == "Approved"? "btn btn-xs btn-success waves-effect waves-light" :Eval("Status").ToString() == "Rejected" ? "btn btn-xs btn-danger waves-effect waves-light btn-block": "btn btn-xs btn-blue waves-effect waves-light" %>' CausesValidation="False" Text='<%#  Eval("STATUS").ToString() %>' Enabled="false" ForeColor="White"></asp:LinkButton>
                                                        </ItemTemplate>

                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="LbtnSec80View" runat="server" CausesValidation="False" CommandName="View" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' CssClass="btn-xs btn-warning" ForeColor="White"><i class="fe-eye"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>
                                                <PagerStyle CssClass="GridPager" HorizontalAlign="Right" />
                                                <PagerSettings Mode="NumericFirstLast" FirstPageText="<<" PreviousPageText="<" NextPageText=">" LastPageText=">>" />
                                                <SelectedRowStyle BackColor="Silver" />
                                            </asp:GridView>

                                            <br />

                                            <asp:GridView ID="GVITSec80H" runat="server" CssClass="gridviewNew" GridLines="None" AutoGenerateColumns="False" BorderStyle="None" Width="99%" Visible="false">
                                                <Columns>
                                                    <%--//select t8.SBSEC,t8.SBDIV,t8.SBDDS, t9.SDVLT,t9.TXEXM--%>
                                                    <%-- <asp:TemplateField HeaderText="No.">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex+1 %>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"/>
                    </asp:TemplateField>--%>
                                                    <asp:BoundField HeaderText="ID" DataField="ID"></asp:BoundField>
                                                    <asp:BoundField HeaderText="Contribution" DataField="SBDDS"></asp:BoundField>
                                                    <asp:BoundField HeaderText="Limit" DataField="SDVLT" HeaderStyle-CssClass="right" ItemStyle-CssClass="right" DataFormatString="{0:#,##0.00}"></asp:BoundField>
                                                    <asp:BoundField HeaderText="Tax EXEM%" DataField="TXEXM"></asp:BoundField>
                                                    <asp:BoundField HeaderText="Prop. Contr. (INR)" DataField="PROPCONTR" HeaderStyle-CssClass="right" ItemStyle-CssClass="right" DataFormatString="{0:#,##0.00}"></asp:BoundField>
                                                    <asp:BoundField HeaderText="Act. Contr. (INR)" DataField="ACTCONTR" HeaderStyle-CssClass="right" ItemStyle-CssClass="right" DataFormatString="{0:#,##0.00}"></asp:BoundField>
                                                    <asp:BoundField HeaderText="Curr" DataField="CURR" Visible="false"></asp:BoundField>
                                                    <asp:BoundField HeaderText="Attachments" DataField="RECEIPT_FID"></asp:BoundField>
                                                    <asp:BoundField HeaderText="Comments" DataField="EMPCOMMENTS"></asp:BoundField>

                                                </Columns>

                                            </asp:GridView>

                                            <div class="DivSpacer01">
                                                <br />
                                            </div>
                                            <asp:UpdatePanel ID="UPITSec80" runat="server">
                                                <ContentTemplate>
                                                    <div id="ExportbtnSec80" runat="server">
                                                        <asp:Button ID="BtnExporttoXlSec80" runat="server" Text="Export To Excel" OnClick="BtnExporttoXlSec80_Click" CausesValidation="false" TabIndex="8" CssClass="btn bg-dark waves-effect waves-light btn-std" />
                                                        &nbsp;&nbsp;
                                                        <asp:Button ID="BtnExporttoPDFSec80" runat="server" Text="Export To PDF" OnClick="BtnExporttoPDFSec80_Click" TabIndex="9" CssClass="btn bg-dark waves-effect waves-light btn-std" />

                                                    </div>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:PostBackTrigger ControlID="BtnExporttoXlSec80" />
                                                    <asp:PostBackTrigger ControlID="BtnExporttoPDFSec80" />
                                                </Triggers>
                                            </asp:UpdatePanel>

                                            <asp:HiddenField ID="viewcheckSec80" runat="server" />

                                        </asp:View>

                                        <asp:View ID="View2H" runat="server">
                                            <asp:Label ID="LblSec80c" runat="server" CssClass="msgboard"></asp:Label>
                                            <asp:GridView ID="GVSec80CHeader" runat="server" CssClass="gridviewNew" GridLines="None" AutoGenerateColumns="False" BorderStyle="None" DataKeyNames="ID,BEGDA,ENDDA,CREATED_ON,CONACTPROP,APPROVEDON,REMARKS,STATUS" Width="85%"
                                                OnRowCommand="GVSec80CHeader_RowCommand" AllowPaging="true" PageSize="10" OnPageIndexChanging="GVSec80CHeader_PageIndexChanging"
                                                AllowSorting="true" OnSorting="GVSec80CHeader_Sorting">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="No.">
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex+1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField HeaderText="ID" DataField="ID"></asp:BoundField>
                                                    <asp:TemplateField HeaderText="IT Type">
                                                        <ItemTemplate>
                                                            Section 80 C
                                                        </ItemTemplate>

                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="BEGDA" HeaderText="From Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                                    <asp:BoundField DataField="ENDDA" HeaderText="To Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                                    <%-- <asp:BoundField DataField="CONACTPROP" HeaderText="Consider Actuals" />--%>

                                                    <asp:TemplateField HeaderText="Consider Actuals">
                                                        <ItemTemplate>
                                                            <%#(Eval("CONACTPROP").ToString().Trim()=="0") ? "No" : (Eval("CONACTPROP").ToString().Trim()=="1")? "Yes": "-"%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:BoundField DataField="CREATED_ON" HeaderText="Submitted On" DataFormatString="{0:dd-MMM-yyyy}" />
                                                    <%-- <asp:BoundField DataField="APPROVEDON" HeaderText="Approved On" DataFormatString="{0:dd-MMM-yyyy}" />--%>
                                                    <asp:TemplateField HeaderText="Approved On">
                                                        <ItemTemplate>
                                                            <%#(Eval("APPROVEDON","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APPROVEDON","{0:dd-MM-yyyy}")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="REMARKS" HeaderText="Remarks" />

                                                    <%--<asp:BoundField HeaderText="Status" DataField="STATUS"></asp:BoundField>--%>
                                                    <asp:TemplateField HeaderText="Status">
                                                        <ItemTemplate>

                                                            <asp:LinkButton ID="lbtnStatus" runat="server" CssClass='<%#Eval("STATUS").ToString() == "Approved"? "btn btn-xs btn-success waves-effect waves-light" :Eval("Status").ToString() == "Rejected" ?"btn btn-xs btn-danger waves-effect waves-light btn-block": "btn btn-xs btn-blue waves-effect waves-light" %>' CausesValidation="False" Text='<%#  Eval("STATUS").ToString() %>' Enabled="false" ForeColor="White"></asp:LinkButton>
                                                        </ItemTemplate>

                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="LbtnSec80CView" runat="server" CausesValidation="False" CommandName="View" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' CssClass="btn-xs btn-warning" ForeColor="White"><i class="fe-eye"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>
                                                <PagerStyle CssClass="GridPager" HorizontalAlign="Right" />
                                                <PagerSettings Mode="NumericFirstLast" FirstPageText="<<" PreviousPageText="<" NextPageText=">" LastPageText=">>" />
                                                <SelectedRowStyle BackColor="Silver" />
                                            </asp:GridView>

                                            <br />

                                            <asp:GridView ID="GVITSec80CH" runat="server" CssClass="gridviewNew" GridLines="None" AutoGenerateColumns="False" BorderStyle="None" Width="99%">
                                                <Columns>
                                                    <%-- //select t8.SBSEC,t8.SBDIV,t8.SBDDS, t9.SDVLT,t9.TXEXM--%>
                                                    <%-- <asp:TemplateField HeaderText="No.">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex+1 %>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"/>
                    </asp:TemplateField>--%>
                                                    <asp:BoundField HeaderText="ID" DataField="ID"></asp:BoundField>
                                                    <asp:BoundField HeaderText="Investment / Contribution" DataField="ITTXT"></asp:BoundField>
                                                    <asp:BoundField HeaderText="Limit" DataField="ITLMT" HeaderStyle-CssClass="right" ItemStyle-CssClass="right" DataFormatString="{0:#,##0.00}"></asp:BoundField>

                                                    <asp:BoundField HeaderText="Prop. Invst. (INR)" DataField="PROPINVST" HeaderStyle-CssClass="right" ItemStyle-CssClass="right" DataFormatString="{0:#,##0.00}"></asp:BoundField>
                                                    <asp:BoundField HeaderText="Act. Invst. (INR)" DataField="ACTINVST" HeaderStyle-CssClass="right" ItemStyle-CssClass="right" DataFormatString="{0:#,##0.00}"></asp:BoundField>

                                                    <asp:BoundField HeaderText="Curr" DataField="CURR" Visible="false"></asp:BoundField>

                                                    <asp:BoundField HeaderText="Attachments" DataField="RECEIPT_FID"></asp:BoundField>
                                                    <asp:BoundField HeaderText="Comments" DataField="EMPCOMMENTS"></asp:BoundField>
                                                </Columns>

                                            </asp:GridView>


                                            <div class="DivSpacer01">
                                                <br />
                                            </div>
                                            <asp:UpdatePanel ID="UPITSec80C" runat="server">
                                                <ContentTemplate>
                                                    <div id="ExportbtnSec80C" runat="server">
                                                        <asp:Button ID="BtnExptoXLSEC80C" runat="server" Text="Export To Excel" OnClick="BtnExptoXLSEC80C_Click" CausesValidation="false" TabIndex="10" CssClass="btn bg-dark waves-effect waves-light btn-std" />
                                                        &nbsp;&nbsp;
                    <asp:Button ID="BtnExptoPdfSec80C" runat="server" Text="Export To PDF" OnClick="BtnExptoPdfSec80C_Click" TabIndex="11" CssClass="btn bg-dark waves-effect waves-light btn-std" />

                                                    </div>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:PostBackTrigger ControlID="BtnExptoXLSEC80C" />
                                                    <asp:PostBackTrigger ControlID="BtnExptoPdfSec80C" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                            <asp:HiddenField ID="viewcheckSec80C" runat="server" />
                                        </asp:View>

                                        <asp:View ID="View3H" runat="server">
                                            <asp:Label ID="LblHousing" runat="server" CssClass="msgboard"></asp:Label>
                                            <asp:GridView ID="GVHousingHeader" runat="server" CssClass="gridviewNew" GridLines="None" AutoGenerateColumns="False" BorderStyle="None" Width="85%" DataKeyNames="ID,BEGDA,ENDDA,CREATED_ON,CONACTPROP,APPROVEDON,REMARKS,STATUS"
                                                OnRowCommand="GVHousingHeader_RowCommand" AllowPaging="true" PageSize="10" OnPageIndexChanging="GVHousingHeader_PageIndexChanging"
                                                AllowSorting="true" OnSorting="GVHousingHeader_Sorting">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="No.">
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex+1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField HeaderText="ID" DataField="ID"></asp:BoundField>
                                                    <asp:TemplateField HeaderText="IT Type">
                                                        <ItemTemplate>
                                                            Housing
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="BEGDA" HeaderText="From Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                                    <asp:BoundField DataField="ENDDA" HeaderText="To Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                                    <%--<asp:BoundField DataField="CONACTPROP" HeaderText="Consider Actuals"/>--%>

                                                    <asp:BoundField DataField="CREATED_ON" HeaderText="Submitted On" DataFormatString="{0:dd-MMM-yyyy}" />
                                                    <%-- <asp:BoundField DataField="APPROVEDON" HeaderText="Approved On" DataFormatString="{0:dd-MMM-yyyy}" />--%>
                                                    <asp:TemplateField HeaderText="Approved On">
                                                        <ItemTemplate>
                                                            <%#(Eval("APPROVEDON","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APPROVEDON","{0:dd-MM-yyyy}")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="REMARKS" HeaderText="Remarks" />

                                                    <%-- <asp:BoundField HeaderText="Status" DataField="STATUS"></asp:BoundField>--%>
                                                    <asp:TemplateField HeaderText="Status">
                                                        <ItemTemplate>

                                                            <asp:LinkButton ID="lbtnStatus" runat="server" CssClass='<%#Eval("STATUS").ToString() == "Approved"? "btn btn-xs btn-success waves-effect waves-light" :Eval("Status").ToString() == "Rejected" ?"btn btn-xs btn-danger waves-effect waves-light btn-block": "btn btn-xs btn-blue waves-effect waves-light" %>' CausesValidation="False" Text='<%#  Eval("STATUS").ToString() %>' Enabled="false" ForeColor="White"></asp:LinkButton>
                                                        </ItemTemplate>

                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="LbtnHousingView" runat="server" CausesValidation="False" CommandName="View" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' CssClass="btn-xs btn-warning" ForeColor="White"><i class="fe-eye"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <PagerStyle CssClass="GridPager" HorizontalAlign="Right" />
                                                <PagerSettings Mode="NumericFirstLast" FirstPageText="<<" PreviousPageText="<" NextPageText=">" LastPageText=">>" />
                                                <SelectedRowStyle BackColor="Silver" />
                                            </asp:GridView>

                                            <br />

                                            <asp:GridView ID="GVHousing" runat="server" CssClass="gridviewNew" GridLines="None" AutoGenerateColumns="False" BorderStyle="None" Width="99%">
                                                <Columns>
                                                    <%--  //ACCOM,METRO,RTAMT,HRTXE,LDAD1,LDAID,LDADE,EMPCOMMENTS,STATUS--%>
                                                    <%-- <asp:TemplateField HeaderText="No.">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex+1 %>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"/>
                    </asp:TemplateField>--%>
                                                    <asp:BoundField HeaderText="ID" DataField="ID"></asp:BoundField>
                                                    <%-- <asp:BoundField HeaderText="Accomodation Type" DataField="">
                        <ItemStyle HorizontalAlign="left" Width="100px"/>
                    </asp:BoundField>--%>
                                                    <asp:TemplateField HeaderText="Accomodation Type">
                                                        <ItemTemplate>

                                                            <%#(Eval("ACCOM").ToString().Trim()=="1") ? "Rentded" : (Eval("ACCOM").ToString().Trim()=="4")? "Own": "-"%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%-- <asp:BoundField HeaderText="City Category"  DataField="">
                        <ItemStyle HorizontalAlign="left" />
                    </asp:BoundField>--%>

                                                    <asp:TemplateField HeaderText="City Category">
                                                        <ItemTemplate>

                                                            <%#(Eval("METRO").ToString().Trim()=="1") ? "Metro" : (Eval("METRO").ToString().Trim()=="0")? "Non-Metro": "-"%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:BoundField HeaderText="Actual Amount" DataField="RTAMT" DataFormatString="{0:#,##0.00}" HeaderStyle-CssClass="right" ItemStyle-CssClass="right"></asp:BoundField>
                                                    <%-- <asp:BoundField HeaderText="HRA to be Excempt"  DataField="">
                        <ItemStyle HorizontalAlign="left" />
                    </asp:BoundField>--%>

                                                    <asp:TemplateField HeaderText="HRA to be Excempt">
                                                        <ItemTemplate>

                                                            <%#(Eval("HRTXE").ToString().Trim()=="1") ? "Yes" : (Eval("HRTXE").ToString().Trim()=="0")? "No" : "-"%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>


                                                    <asp:BoundField HeaderText="Landlord's Address" DataField="LDAD1"></asp:BoundField>

                                                    <asp:BoundField HeaderText="PAN of Landlord" DataField="LDAID"></asp:BoundField>

                                                    <%--<asp:BoundField HeaderText="Declaration Provided by LandLord"  DataField="">
                        <ItemStyle HorizontalAlign="left"/>
                    </asp:BoundField>--%>

                                                    <asp:TemplateField HeaderText="Declaration Provided by Landlord">
                                                        <ItemTemplate>

                                                            <%#(Eval("LDADE").ToString().Trim()=="1") ? "Yes" : (Eval("LDADE").ToString().Trim()=="0")? "No" : "-"%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>



                                                    <asp:BoundField HeaderText="Comments" DataField="EMPCOMMENTS"></asp:BoundField>
                                                </Columns>

                                            </asp:GridView>


                                            <div class="DivSpacer01">
                                                <br />
                                            </div>
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                                    <div id="ExportbtnHousing" runat="server">
                                                        <asp:Button ID="BtnExptoXLHousing" runat="server" Text="Export To Excel" OnClick="BtnExptoXLHousing_Click" CausesValidation="false" TabIndex="12" CssClass="btn bg-dark waves-effect waves-light btn-std" />
                                                        &nbsp;&nbsp;
                    <asp:Button ID="BtnExptopdfHousing" runat="server" Text="Export To PDF" OnClick="BtnExptopdfHousing_Click" TabIndex="13" CssClass="btn bg-dark waves-effect waves-light btn-std" />

                                                    </div>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:PostBackTrigger ControlID="BtnExptoXLHousing" />
                                                    <asp:PostBackTrigger ControlID="BtnExptopdfHousing" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                            <asp:HiddenField ID="viewcheckHousing" runat="server" />

                                        </asp:View>

                                        <asp:View ID="View4H" runat="server">
                                            <asp:Label ID="LblOthers" runat="server" CssClass="msgboard"></asp:Label><br />
                                            <asp:GridView ID="GVOthersHeader" runat="server" CssClass="gridviewNew" GridLines="None" AutoGenerateColumns="False" BorderStyle="None" DataKeyNames="ID,ITTYP,BEGDA,ENDDA,CREATED_ON,CONACTPROP,APPROVEDON,REMARKS,STATUS"
                                                OnRowCommand="GVOthersHeader_RowCommand" Width="85%" AllowPaging="true" PageSize="10" OnPageIndexChanging="GVOthersHeader_PageIndexChanging"
                                                AllowSorting="true" OnSorting="GVOthersHeader_Sorting">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="No.">
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex+1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField HeaderText="ID" DataField="ID"></asp:BoundField>
                                                    <%--   <asp:TemplateField HeaderText="IT Type">
                                <ItemTemplate>
                                    Other Sources
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"/>
                    </asp:TemplateField>--%>
                                                    <%-- <asp:BoundField HeaderText="IT Type" DataField="ITTYP"></asp:BoundField>--%>

                                                    <asp:TemplateField HeaderText="IT Type">
                                                        <ItemTemplate>

                                                            <%#(Eval("ITTYP").ToString().Trim()=="1") ? "Housing Property" :  "Other Sources"%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="BEGDA" HeaderText="From Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                                    <asp:BoundField DataField="ENDDA" HeaderText="To Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                                    <%--          <asp:BoundField DataField="CONACTPROP" HeaderText="Consider Actuals"/>--%>

                                                    <asp:BoundField DataField="CREATED_ON" HeaderText="Submitted On" DataFormatString="{0:dd-MMM-yyyy}" />
                                                    <%-- <asp:BoundField DataField="APPROVEDON" HeaderText="Approved On" DataFormatString="{0:dd-MMM-yyyy}" />--%>
                                                    <asp:TemplateField HeaderText="Approved On">
                                                        <ItemTemplate>
                                                            <%#(Eval("APPROVEDON","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APPROVEDON","{0:dd-MM-yyyy}")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="REMARKS" HeaderText="Remarks" />

                                                    <%--<asp:BoundField HeaderText="Status" DataField="STATUS"></asp:BoundField>--%>
                                                    <asp:TemplateField HeaderText="Status">
                                                        <ItemTemplate>

                                                            <asp:LinkButton ID="lbtnStatus" runat="server" CssClass='<%#Eval("STATUS").ToString() == "Approved"? "btn btn-xs btn-success waves-effect waves-light" :Eval("Status").ToString() == "Rejected" ?"btn btn-xs btn-danger waves-effect waves-light btn-block": "btn btn-xs btn-blue waves-effect waves-light" %>' CausesValidation="False" Text='<%#  Eval("STATUS").ToString() %>' Enabled="false" ForeColor="White"></asp:LinkButton>
                                                        </ItemTemplate>

                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="LbtnOthersView" runat="server" CausesValidation="False" CommandName="View" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' CssClass="btn-xs btn-warning" ForeColor="White"><i class="fe-eye"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>
                                                <PagerStyle CssClass="GridPager" HorizontalAlign="Right" />
                                                <PagerSettings Mode="NumericFirstLast" FirstPageText="<<" PreviousPageText="<" NextPageText=">" LastPageText=">>" />
                                                <SelectedRowStyle BackColor="Silver" />
                                            </asp:GridView>
                                            <br />
                                            <asp:GridView ID="grdHistySelf" runat="server" EmptyDataText="No Record Found!" EmptyDataRowStyle-CssClass="emptygridstyle"
                                                OnRowDataBound="grdHistySelf_RowDataBound" DataKeyNames="ID"
                                                AutoGenerateColumns="false" GridLines="None" CssClass="gridviewNew">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl.No." ControlStyle-CssClass="col-center">
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex+1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Led. Name">
                                                        <ItemTemplate>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Led. Addr.">
                                                        <ItemTemplate>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Led. PAN">
                                                        <ItemTemplate>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Addr. of Property">
                                                        <ItemTemplate>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="State">
                                                        <ItemTemplate>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="City">
                                                        <ItemTemplate>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Loan Sanc. Date">
                                                        <ItemTemplate>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Purpose of Housing loan">
                                                        <ItemTemplate>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Carpet Area in Sq. Ft.">
                                                        <ItemTemplate>
                                                            <%#Eval("TDSAT") %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Loan Sanc. Amt." ItemStyle-CssClass="right" HeaderStyle-CssClass="right">
                                                        <ItemTemplate>
                                                            <%#Eval("INT24","{0:#,##0.00}") %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Value of the Property" ItemStyle-CssClass="right" HeaderStyle-CssClass="right">
                                                        <ItemTemplate>
                                                            <%#Eval("REP24","{0:#,##0.00}") %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Stamp Duty/Reg. charge" ItemStyle-CssClass="right" HeaderStyle-CssClass="right">
                                                        <ItemTemplate>
                                                            <%#Eval("OTH24","{0:#,##0.00}") %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Stamp Duty/Reg. charge date" ItemStyle-CssClass="right" HeaderStyle-CssClass="right">
                                                        <ItemTemplate>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Any other house Property" ItemStyle-CssClass="right" HeaderStyle-CssClass="right">
                                                        <ItemTemplate>
                                                            <%#Eval("LETVL").ToString()=="0.00"?"No":"Yes" %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Loan Type">
                                                        <ItemTemplate>
                                                            <%#Eval("BSPFT").ToString()=="1.00"?"Single":"Joint" %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Total Interest Paid" ItemStyle-CssClass="right" HeaderStyle-CssClass="right">
                                                        <ItemTemplate>
                                                            <%#Eval("CPGLN","{0:#,##0.00}") %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Total Principal Paid " ItemStyle-CssClass="right" HeaderStyle-CssClass="right">
                                                        <ItemTemplate>
                                                            <%#Eval("CPGLS","{0:#,##0.00}") %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Total Interest" ItemStyle-CssClass="right" HeaderStyle-CssClass="right">
                                                        <ItemTemplate>
                                                            <%#Eval("CPGSS","{0:#,##0.00}") %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Total Principal" ItemStyle-CssClass="right" HeaderStyle-CssClass="right">
                                                        <ItemTemplate>
                                                            <%#Eval("INTRS","{0:#,##0.00}") %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Benefit Under Section 80EE">
                                                        <ItemTemplate>
                                                            <%#Eval("DVDND").ToString()=="0.00"?"No":"Yes" %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField HeaderText="Comments" DataField="EMPCOMMENTS">
                                                        <ItemStyle HorizontalAlign="left" />
                                                    </asp:BoundField>

                                                </Columns>
                                            </asp:GridView>
                                            <br />
                                            <asp:GridView ID="GVOthers1" runat="server" CssClass="gridviewNew" GridLines="None" AutoGenerateColumns="False" BorderStyle="None" Width="85%">
                                                <Columns>
                                                    <%--    //ID,PROPTYP,RENTO,INT24,LETVL,REP24,OTH24,TDSOT,
                // BSPFT,CPGLN,CPGLS,CPGNS,CPGSS,DVDND,INTRS,UNSPI,TDSAT, EMPCOMMENTS,EMPCOMMENTS2,STATUS  -%>
                   <%-- <asp:TemplateField HeaderText="No.">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex+1 %>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"/>
                    </asp:TemplateField>--%>
                                                    <asp:BoundField HeaderText="ID" DataField="ID">
                                                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                                                    </asp:BoundField>
                                                    <%-- <asp:BoundField HeaderText="Accomodation Type" DataField="">
                        <ItemStyle HorizontalAlign="left" Width="100px"/>
                    </asp:BoundField>--%>
                                                    <asp:TemplateField HeaderText="Income Type">
                                                        <ItemTemplate>



                                                            <%#(Eval("PROPTYP").ToString().Trim()=="1") ? "House Property" : (Eval("PROPTYP").ToString().Trim()=="2")? "Other Sources": "-"%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>



                                                    <asp:TemplateField HeaderText="Property Type">
                                                        <ItemTemplate>



                                                            <%#(Eval("RENTO").ToString().Trim()=="1") ? "Self Occupied House Property" : (Eval("RENTO").ToString().Trim()=="2")? "Partly Let Out House Property": (Eval("RENTO").ToString().Trim()=="3")? "Let Out House Property": "-"%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>



                                                    <asp:BoundField HeaderText="Rental Income received in the Financial year" DataField="LETVL" HeaderStyle-CssClass="right" ItemStyle-CssClass="right" DataFormatString="{0:#,##0.00}"></asp:BoundField>
                                                    <asp:BoundField HeaderText="Municipal Tax paid for the year" DataField="REP24" HeaderStyle-CssClass="right" ItemStyle-CssClass="right" DataFormatString="{0:#,##0.00}"></asp:BoundField>



                                                    <%--  <asp:BoundField HeaderText="Deduce - Interest u/s 24" DataField="LETVL">
                            <ItemStyle HorizontalAlign="right" />
                        </asp:BoundField>--%>



                                                    <asp:BoundField HeaderText="Net Annual Value" DataField="INT24" HeaderStyle-CssClass="right" ItemStyle-CssClass="right" DataFormatString="{0:#,##0.00}">
                                                        <ItemStyle HorizontalAlign="right" />
                                                    </asp:BoundField>



                                                    <asp:BoundField HeaderText="Repairs & Maintenance charge(30%)" DataField="OTH24" HeaderStyle-CssClass="right" ItemStyle-CssClass="right" DataFormatString="{0:#,##0.00}">
                                                        <ItemStyle HorizontalAlign="right" />
                                                    </asp:BoundField>



                                                    <asp:TemplateField HeaderText="Loan Type">
                                                        <ItemTemplate>
                                                            <%#(Eval("BSPFT").ToString().Trim()=="1.00") ? "Single" :  "Joint"%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField HeaderText="Self Percentange" DataField="TDSOT">
                                                        <ItemStyle HorizontalAlign="right" />
                                                    </asp:BoundField>



                                                    <asp:BoundField HeaderText="Total Interest Portion" DataField="CPGLN" HeaderStyle-CssClass="right" ItemStyle-CssClass="right" DataFormatString="{0:#,##0.00}">
                                                        <ItemStyle HorizontalAlign="right" />
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderText="Income /Loss on House Property considered for Exemption" DataField="CPGLS" HeaderStyle-CssClass="right" ItemStyle-CssClass="right" DataFormatString="{0:#,##0.00}">
                                                        <ItemStyle HorizontalAlign="right" />
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderText="Comments" DataField="EMPCOMMENTS">
                                                        <ItemStyle HorizontalAlign="left" />
                                                    </asp:BoundField>
                                                </Columns>



                                            </asp:GridView>
                                            <br />
                                            <asp:GridView ID="grdHisBorw" runat="server" EmptyDataText="No Record Found!" EmptyDataRowStyle-CssClass="emptygridstyle" DataKeyNames="ID" AutoGenerateColumns="false" GridLines="None" CssClass="gridviewNew">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl. No." ControlStyle-CssClass="col-center">
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex+1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Borrowers Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblGrdBorwName" runat="server" Text='<%#Eval("Name") %>'></asp:Label>
                                                            <%-- <asp:TextBox ID="txtGrdBorwName" runat="server" CssClass="form-control-file"></asp:TextBox>
                                                                                        <cc1:FilteredTextBoxExtender ID="FTBEtxtGrdBorwName" runat="server" FilterMode="ValidChars" FilterType="LowercaseLetters,Custom,UppercaseLetters"
                                                                                            TargetControlID="txtGrdBorwName" ValidChars=".">
                                                                                        </cc1:FilteredTextBoxExtender>--%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Percentange(%)">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblGrdBorwPernct" runat="server" Text='<%#Eval("PERCNT") %>'></asp:Label>
                                                            <%-- <asp:TextBox ID="txtGrdBorwPernct" runat="server" CssClass="form-control-file"></asp:TextBox>
                                                                                        <cc1:FilteredTextBoxExtender ID="FTBEtxtGrdBorwPernct" runat="server" FilterMode="ValidChars" FilterType="Custom,Numbers"
                                                                                            TargetControlID="txtGrdBorwPernct" ValidChars=".">
                                                                                        </cc1:FilteredTextBoxExtender>--%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>
                                            </asp:GridView>
                                            <br />
                                            <asp:GridView ID="GVOthers2" runat="server" CssClass="gridviewNew" GridLines="None" AutoGenerateColumns="False" BorderStyle="None" Width="85%">
                                                <Columns>
                                                    <%--    //ID,PROPTYP,RENTO,INT24,LETVL,REP24,OTH24,TDSOT,
                // BSPFT,CPGLN,CPGLS,CPGNS,CPGSS,DVDND,INTRS,UNSPI,TDSAT, EMPCOMMENTS,EMPCOMMENTS2,STATUS  -%>
                   <%-- <asp:TemplateField HeaderText="No.">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex+1 %>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"/>
                    </asp:TemplateField>--%>
                                                    <asp:BoundField HeaderText="ID" DataField="ID"></asp:BoundField>

                                                    <asp:TemplateField HeaderText="Income Type">
                                                        <ItemTemplate>

                                                            <%#(Eval("PROPTYP").ToString().Trim()=="1") ? "House Property" : (Eval("PROPTYP").ToString().Trim()=="2")? "Other Sources": "-"%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>


                                                    <%--  <asp:BoundField HeaderText="Business Profits" DataField="BSPFT"></asp:BoundField>
                                                    <asp:BoundField HeaderText="Long-Term Capital Gains (Normal Rate)" DataField="CPGLN"></asp:BoundField>
                                                    <asp:BoundField HeaderText="Long-Term Capital Gains (Special Rate)" DataField="CPGLS"></asp:BoundField>
                                                    <asp:BoundField HeaderText="Short-Term Capital Gains" DataField="CPGNS"></asp:BoundField>
                                                    <asp:BoundField HeaderText="Short-Term Capital Gains (Listed Securities)" DataField="CPGSS"></asp:BoundField>
                                                    <asp:BoundField HeaderText="Income From Dividend" DataField="DVDND"></asp:BoundField>--%>
                                                    <asp:BoundField HeaderText="Income From Interest" DataField="INTRS" HeaderStyle-CssClass="right" ItemStyle-CssClass="right" DataFormatString="{0:#,##0.00}"></asp:BoundField>
                                                    <%-- <asp:BoundField HeaderText="Other Income Unspecified" DataField="UNSPI"></asp:BoundField>
                                                    <asp:BoundField HeaderText="TDS On Other Income" DataField="TDSAT"></asp:BoundField>--%>


                                                    <asp:BoundField HeaderText="Comments" DataField="EMPCOMMENTS2"></asp:BoundField>



                                                </Columns>

                                            </asp:GridView>

                                            <div class="DivSpacer01">
                                                <br />
                                            </div>
                                            <br />
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                <ContentTemplate>
                                                    <div id="ExportbtnOthers" runat="server">
                                                        <asp:Button ID="BtnExptoXLOthers" runat="server" Text="Export To Excel" OnClick="BtnExptoXLOthers_Click" CausesValidation="false" TabIndex="14" CssClass="btn bg-dark waves-effect waves-light btn-std" />
                                                        &nbsp;&nbsp;
                                                        <asp:Button ID="BtnExptopdfOthers" runat="server" Text="Export To PDF" OnClick="BtnExptopdfOthers_Click" TabIndex="15" CssClass="btn bg-dark waves-effect waves-light btn-std" />

                                                    </div>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:PostBackTrigger ControlID="BtnExptopdfOthers" />
                                                    <asp:PostBackTrigger ControlID="BtnExptoXLOthers" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                            <asp:HiddenField ID="viewcheckOthers" runat="server" />

                                        </asp:View>

                                        <asp:View ID="View5H" runat="server">
                                            <asp:Label ID="lblPreE" runat="server"></asp:Label>
                                            <asp:GridView ID="grdPreEmptIncHead" runat="server" CssClass="gridviewNew" GridLines="None" AutoGenerateColumns="False"
                                                BorderStyle="None" DataKeyNames="ID,BEGDA,ENDDA,STATUS" Width="85%" AllowPaging="true" PageSize="10" OnRowCommand="grdPreEmptIncHead_RowCommand"
                                                OnPageIndexChanging="grdPreEmptIncHead_PageIndexChanging">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="No.">
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex+1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField HeaderText="ID" DataField="ID"></asp:BoundField>
                                                    <%--   <asp:TemplateField HeaderText="IT Type">
                                <ItemTemplate>
                                    Other Sources
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"/>
                    </asp:TemplateField>--%>
                                                    <%-- <asp:BoundField HeaderText="IT Type" DataField="ITTYP"></asp:BoundField>--%>

                                                    <asp:TemplateField HeaderText="IT Type">
                                                        <ItemTemplate>
                                                            Previous Employment Income
                                                           <%-- <%#(Eval("ITTYP").ToString().Trim()=="1") ? "Housing Property" :  "Other Sources"%>--%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="BEGDA" HeaderText="From Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                                    <asp:BoundField DataField="ENDDA" HeaderText="To Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                                    <%--          <asp:BoundField DataField="CONACTPROP" HeaderText="Consider Actuals"/>--%>

                                                    <asp:BoundField DataField="CREATED_ON" HeaderText="Submitted On" DataFormatString="{0:dd-MMM-yyyy}" />
                                                    <%--  <asp:BoundField DataField="APPROVEDON" HeaderText="Approved On" DataFormatString="{0:dd-MMM-yyyy}" />--%>
                                                    <asp:TemplateField HeaderText="Approved On">
                                                        <ItemTemplate>
                                                            <%#(Eval("APPROVEDON","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APPROVEDON","{0:dd-MM-yyyy}")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="REMARKS" HeaderText="Remarks" />

                                                    <%--   <asp:BoundField HeaderText="Status" DataField="STATUS"></asp:BoundField>--%>
                                                    <asp:TemplateField HeaderText="Status">
                                                        <ItemTemplate>

                                                            <asp:LinkButton ID="lbtnStatus" runat="server" CssClass='<%#Eval("STATUS").ToString() == "Approved"? "btn btn-xs btn-success waves-effect waves-light" :Eval("Status").ToString() == "Rejected" ?"btn btn-xs btn-danger waves-effect waves-light btn-block": "btn btn-xs btn-blue waves-effect waves-light" %>' CausesValidation="False" Text='<%#  Eval("STATUS").ToString() %>' Enabled="false" ForeColor="White"></asp:LinkButton>
                                                        </ItemTemplate>

                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="LbtnOthersView" runat="server" CausesValidation="False" CommandName="View" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' CssClass="btn-xs btn-warning" ForeColor="White"><i class="fe-eye"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>
                                                <PagerStyle CssClass="GridPager" HorizontalAlign="Right" />
                                                <PagerSettings Mode="NumericFirstLast" FirstPageText="<<" PreviousPageText="<" NextPageText=">" LastPageText=">>" />
                                                <SelectedRowStyle BackColor="Silver" />
                                            </asp:GridView>
                                            <asp:GridView ID="grdPreEmptInc" runat="server" CssClass="gridviewNew" GridLines="None" AutoGenerateColumns="False"
                                                BorderStyle="None" Visible="false">
                                                <Columns>
                                                    <asp:BoundField HeaderText="Employer" DataField="PreEmpr" />
                                                    <asp:BoundField HeaderText="PAN" DataField="PreEmprPAN" />
                                                    <asp:BoundField HeaderText="TAN" DataField="PreEmprTAN" />
                                                    <asp:BoundField HeaderText="From Date" DataField="BEGDA" DataFormatString="{0:dd-MMM-yyyy}" />
                                                    <asp:BoundField HeaderText="To Date" DataField="ENDDA" DataFormatString="{0:dd-MMM-yyyy}" />
                                                    <asp:BoundField HeaderText="Salary Income After Exemptions" DataField="GRSAL" HeaderStyle-CssClass="right" ItemStyle-CssClass="right" DataFormatString="{0:#,##0.00}" />
                                                    <%--                                                    <asp:BoundField HeaderText="Value of Perquisites u/s 17(2)" DataField="VPRQS" />
                                                    <asp:BoundField HeaderText="Profits in lieu of salary u/s 17(3)" DataField="PRSAL" />
                                                    <asp:BoundField HeaderText="Exemptions under Section (10)" DataField="EXS10" />--%>
                                                    <asp:BoundField HeaderText="PT deducted" DataField="PRTAX" HeaderStyle-CssClass="right" ItemStyle-CssClass="right" DataFormatString="{0:#,##0.00}" />
                                                    <asp:BoundField HeaderText="PF deducted" DataField="PRFND" HeaderStyle-CssClass="right" ItemStyle-CssClass="right" DataFormatString="{0:#,##0.00}" />
                                                    <asp:BoundField HeaderText="IT Deducted" DataField="TXDED" HeaderStyle-CssClass="right" ItemStyle-CssClass="right" DataFormatString="{0:#,##0.00}" />
                                                    <asp:BoundField HeaderText="Surcharge Deducted" DataField="SCDED" HeaderStyle-CssClass="right" ItemStyle-CssClass="right" DataFormatString="{0:#,##0.00}" />
                                                    <asp:BoundField HeaderText="Cess Deducted" DataField="ECDED" HeaderStyle-CssClass="right" ItemStyle-CssClass="right" DataFormatString="{0:#,##0.00}" />
                                                </Columns>
                                            </asp:GridView>
                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                <ContentTemplate>
                                                    <div id="divPreEmptIncExprt" runat="server">
                                                        <br />
                                                        <asp:Button ID="btnPreEmptIncXL" runat="server" Text="Export To Excel" OnClick="btnPreEmptIncXL_Click" CausesValidation="false" TabIndex="14" CssClass="btn bg-dark waves-effect waves-light btn-std" />
                                                        &nbsp;&nbsp;
                                                        <asp:Button ID="btnPreEmptIncPDF" runat="server" Text="Export To PDF" OnClick="btnPreEmptIncPDF_Click" TabIndex="15" CssClass="btn bg-dark waves-effect waves-light btn-std" />

                                                    </div>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:PostBackTrigger ControlID="btnPreEmptIncXL" />
                                                    <asp:PostBackTrigger ControlID="btnPreEmptIncPDF" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                            <asp:HiddenField ID="HFPreEmptInc" runat="server" />
                                        </asp:View>
                                    </asp:MultiView>

                                </div>
                            </div>
                        </div>
                    </asp:View>

                    <!-- =========== Tab-6 Tab Panel ==================-->
                    <asp:View ID="View6" runat="server">
                        <div class="table-responsive card-box">
                            <div id="Tab-6">
                                <h4>Previous Employment Income</h4>
                                <hr />
                                <div class="form-group">
                                    <div class="row margin5rem">
                                        <div class="col-sm-3">Name of the Previous Employer</div>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="txtITPreEmprNm" runat="server" CssClass="form-control" MaxLength="99" AutoPostBack="true" OnTextChanged="txtITPreEmprNm_TextChanged"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-4">
                                            <asp:RequiredFieldValidator ID="RFVtxtITPreEmprNm" runat="server" ErrorMessage="Please Enter Previous Employer Name"
                                                ForeColor="Red" ControlToValidate="txtITPreEmprNm" ValidationGroup="PEIC" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="row margin5rem">
                                        <div class="col-sm-3">Company PAN</div>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="txtITPreEmprPAN" runat="server" CssClass="form-control" MaxLength="15"></asp:TextBox>

                                        </div>
                                        <div class="col-sm-4">
                                            <asp:RequiredFieldValidator ID="RFVtxtITPreEmprPAN" runat="server" ErrorMessage="Please Enter Company PAN" Enabled="false"
                                                ForeColor="Red" ControlToValidate="txtITPreEmprPAN" ValidationGroup="PEIC" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="row margin5rem">
                                        <div class="col-sm-3">
                                            Company TAN
                                        </div>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="txtITPreEmprTAN" runat="server" CssClass="form-control" MaxLength="15"></asp:TextBox>

                                        </div>
                                        <div class="col-sm-4">
                                            <asp:RequiredFieldValidator ID="RFVtxtITPreEmprTAN" runat="server" ErrorMessage="Please Enter Company TAN" Enabled="false"
                                                ForeColor="Red" ControlToValidate="txtITPreEmprTAN" ValidationGroup="PEIC" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        </div>

                                    </div>
                                    <div class="row margin5rem">
                                        <div class="col-sm-3">
                                            From Date
                                        </div>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="txtITPreEmprFrmDt" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>
                                            <cc1:MaskedEditExtender ID="MEE_txtITPreEmprFrmDt" runat="server" AcceptNegative="Left"
                                                CultureName="en-GB" DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999"
                                                MaskType="Date" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                                                OnInvalidCssClass="MaskedEditError" TargetControlID="txtITPreEmprFrmDt" />
                                            <cc1:CalendarExtender ID="CE_txtITPreEmprFrmDt" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                TargetControlID="txtITPreEmprFrmDt">
                                            </cc1:CalendarExtender>

                                        </div>
                                        <div class="col-sm-4">
                                            <%--<asp:RequiredFieldValidator ID="RFVtxtITPreEmprFrmDt" runat="server" ErrorMessage="Please Enter From Date"
                                                ForeColor="Red" ControlToValidate="txtITPreEmprFrmDt" ValidationGroup="PEIC" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                                            <asp:RangeValidator ID="RV_txtITPreEmprFrmDt" runat="server" ControlToValidate="txtITPreEmprFrmDt" Display="Dynamic"
                                                ErrorMessage="" ValidationGroup="PEIC"
                                                Type="Date" ForeColor="Red"></asp:RangeValidator>
                                        </div>
                                    </div>
                                    <div class="row margin5rem">
                                        <div class="col-sm-3">
                                            To Date
                                        </div>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="txtITPreEmprToDt" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>
                                            <cc1:MaskedEditExtender ID="MEE_txtITPreEmprToDt" runat="server" AcceptNegative="Left"
                                                CultureName="en-GB" DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999"
                                                MaskType="Date" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                                                OnInvalidCssClass="MaskedEditError" TargetControlID="txtITPreEmprToDt" />
                                            <cc1:CalendarExtender ID="CE_txtITPreEmprToDt" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                TargetControlID="txtITPreEmprToDt">
                                            </cc1:CalendarExtender>

                                        </div>
                                        <div class="col-sm-4">
                                            <%--<asp:RequiredFieldValidator ID="RFVtxtITPreEmprToDt" runat="server" ErrorMessage="Please Enter To Date"
                                                ForeColor="Red" ControlToValidate="txtITPreEmprToDt" ValidationGroup="PEIC" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                                            <asp:RangeValidator ID="RVtxtITPreEmprToDt" runat="server" ControlToValidate="txtITPreEmprToDt" Display="Dynamic"
                                                ErrorMessage="" ValidationGroup="PEIC"
                                                Type="Date" ForeColor="Red"></asp:RangeValidator>
                                        </div>

                                    </div>
                                    <div class="row margin5rem">
                                        <div class="col-sm-3">
                                            Salary Income After Exemptions
                                        </div>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="txtITPreEmprSal171" runat="server" CssClass="form-control right" Text="0.0"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="FTE_txtITPreEmprSal171" runat="server" FilterMode="ValidChars" FilterType="Custom,Numbers"
                                                TargetControlID="txtITPreEmprSal171" ValidChars=".">
                                            </cc1:FilteredTextBoxExtender>

                                        </div>
                                        <div class="col-sm-4">
                                            <%--<asp:RequiredFieldValidator ID="RFVtxtITPreEmprSal171" runat="server" ErrorMessage="Please Enter Salary as per provisions u/s 17(1)"
                                                ForeColor="Red" ControlToValidate="txtITPreEmprSal171" ValidationGroup="PEIC" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                                        </div>
                                    </div>
                                    <div class="row margin5rem hidden">
                                        <div class="col-sm-3">
                                            Value of Perquisites u/s 17(2)
                                        </div>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="txtITPreEmprVal172" runat="server" CssClass="form-control right" Text="0.0"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="FTE_txtITPreEmprVal172" runat="server" FilterMode="ValidChars" FilterType="Custom,Numbers"
                                                TargetControlID="txtITPreEmprVal172" ValidChars=".">
                                            </cc1:FilteredTextBoxExtender>

                                        </div>
                                        <div class="col-sm-4">
                                            <%--<asp:RequiredFieldValidator ID="RFVtxtITPreEmprVal172" runat="server" ErrorMessage="Please Enter Value of Perquisites u/s 17(2)"
                                                ForeColor="Red" ControlToValidate="txtITPreEmprVal172" ValidationGroup="PEIC" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                                        </div>

                                    </div>
                                    <div class="row margin5rem hidden">
                                        <div class="col-sm-3">
                                            Profits in lieu of salary u/s 17(3)
                                        </div>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="txtITPreEmprPro173" runat="server" CssClass="form-control right" Text="0.0"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="FTE_txtITPreEmprPro173" runat="server" FilterMode="ValidChars" FilterType="Custom,Numbers"
                                                TargetControlID="txtITPreEmprPro173" ValidChars=".">
                                            </cc1:FilteredTextBoxExtender>

                                        </div>
                                        <div class="col-sm-4">
                                            <%--<asp:RequiredFieldValidator ID="RFVtxtITPreEmprPro173" runat="server" ErrorMessage="Please Enter Profits in lieu of salary u/s 17(3)"
                                                ForeColor="Red" ControlToValidate="txtITPreEmprPro173" ValidationGroup="PEIC" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                                        </div>
                                    </div>
                                    <div class="row margin5rem hidden">
                                        <div class="col-sm-3">
                                            Exemptions under Section (10)
                                        </div>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="txtITPreEmprExpSec" runat="server" CssClass="form-control right" Text="0.0"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="FTE_txtITPreEmprExpSec" runat="server" FilterMode="ValidChars" FilterType="Custom,Numbers"
                                                TargetControlID="txtITPreEmprExpSec" ValidChars=".">
                                            </cc1:FilteredTextBoxExtender>

                                        </div>
                                        <div class="col-sm-4">
                                            <%--<asp:RequiredFieldValidator ID="RFVtxtITPreEmprExpSec" runat="server" ErrorMessage="Please Enter Exemptions under Section (10)"
                                                ForeColor="Red" ControlToValidate="txtITPreEmprExpSec" ValidationGroup="PEIC" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                                        </div>

                                    </div>
                                    <div class="row margin5rem ">
                                        <div class="col-sm-3">
                                            Professional Tax deducted
                                        </div>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="txtITPreEmprProTax" runat="server" CssClass="form-control right" Text="0.0"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="FTE_txtITPreEmprProTax" runat="server" FilterMode="ValidChars" FilterType="Custom,Numbers"
                                                TargetControlID="txtITPreEmprProTax" ValidChars=".">
                                            </cc1:FilteredTextBoxExtender>

                                        </div>
                                        <div class="col-sm-4">
                                            <%--<asp:RequiredFieldValidator ID="RFVtxtITPreEmprProTax" runat="server" ErrorMessage="Please Enter Professional Tax deducted"
                                                ForeColor="Red" ControlToValidate="txtITPreEmprProTax" ValidationGroup="PEIC" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                                        </div>
                                    </div>
                                    <div class="row margin5rem">
                                        <div class="col-sm-3">
                                            Provident Fund deducted
                                        </div>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="txtITPreEmprPF" runat="server" CssClass="form-control right" Text="0.0"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="FTE_txtITPreEmprPF" runat="server" FilterMode="ValidChars" FilterType="Custom,Numbers"
                                                TargetControlID="txtITPreEmprPF" ValidChars=".">
                                            </cc1:FilteredTextBoxExtender>

                                        </div>
                                        <div class="col-sm-4">
                                            <%--<asp:RequiredFieldValidator ID="RFVtxtITPreEmprPF" runat="server" ErrorMessage="Please Enter Provident Fund deducted"
                                                ForeColor="Red" ControlToValidate="txtITPreEmprPF" ValidationGroup="PEIC" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                                        </div>

                                    </div>
                                    <div class="row margin5rem">
                                        <div class="col-sm-3">
                                            Income tax Deducted
                                        </div>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="txtITPreEmprIT" runat="server" CssClass="form-control right" onchange="cal()" Text="0.0"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="FTE_txtITPreEmprIT" runat="server" FilterMode="ValidChars" FilterType="Custom,Numbers"
                                                TargetControlID="txtITPreEmprIT" ValidChars=".">
                                            </cc1:FilteredTextBoxExtender>

                                        </div>
                                        <div class="col-sm-4">
                                            <%--<asp:RequiredFieldValidator ID="RFVtxtITPreEmprIT" runat="server" ErrorMessage="Please Enter Income tax Deducted"
                                                ForeColor="Red" ControlToValidate="txtITPreEmprIT" ValidationGroup="PEIC" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                                        </div>
                                    </div>
                                    <div class="row margin5rem">
                                        <div class="col-sm-3">
                                            Surcharge Deducted 
                                        </div>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="txtITPreEmprSurD" runat="server" CssClass="form-control right" onchange="cal()" Text="0.0"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="FTE_txtITPreEmprSurD" runat="server" FilterMode="ValidChars" FilterType="Custom,Numbers"
                                                TargetControlID="txtITPreEmprSurD" ValidChars=".">
                                            </cc1:FilteredTextBoxExtender>

                                        </div>
                                        <div class="col-sm-4">
                                            <%--<asp:RequiredFieldValidator ID="RFVtxtITPreEmprSurD" runat="server" ErrorMessage="Please Enter Surcharge Deducted"
                                                ForeColor="Red" ControlToValidate="txtITPreEmprSurD" ValidationGroup="PEIC" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                                        </div>

                                    </div>
                                    <div class="row margin5rem">
                                        <div class="col-sm-3">
                                            Cess Deducted
                                        </div>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="txtITPreEmprCess" runat="server" CssClass="form-control right" onchange="cal()" Text="0.0"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="FTE_txtITPreEmprCess" runat="server" FilterMode="ValidChars" FilterType="Custom,Numbers"
                                                TargetControlID="txtITPreEmprCess" ValidChars=".">
                                            </cc1:FilteredTextBoxExtender>

                                        </div>
                                        <div class="col-sm-4">
                                            <%--<asp:RequiredFieldValidator ID="RFVtxtITPreEmprCess" runat="server" ErrorMessage="Please Enter Cess Deducted"
                                                ForeColor="Red" ControlToValidate="txtITPreEmprCess" ValidationGroup="PEIC" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                                        </div>
                                    </div>
                                    <div class="row margin5rem">
                                        <div class="col-sm-3">
                                            Income tax Ded incl. of Cess & Surcharge
                                        </div>
                                        <div class="col-sm-4">
                                            <asp:Label ID="lblPretot" runat="server" Text="0.0"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-6">
                                            <asp:Button ID="btnITPreEmprSub" runat="server" Text="Save & Next" Enabled="false" OnClick="btnITPreEmprSub_Click" CssClass="btn bg-brand-btn waves-effect waves-light btn-std" ValidationGroup="PEIC" />
                                            <asp:Button ID="btnITPreEmprClear" runat="server" Text="Clear" OnClick="btnITPreEmprClear_Click" CssClass="btn bg-brand-btn waves-effect waves-light btn-std" CausesValidation="false" />
                                            <asp:Button ID="btnITPreEmprUpd" runat="server" Text="Save & Next" OnClick="btnITPreEmprUpd_Click" CssClass="btn bg-brand-btn waves-effect waves-light btn-std" ValidationGroup="PEIC" Visible="false" />
                                            <asp:Button ID="btnITPreEmprCancel" runat="server" Text="Cancel" OnClick="btnITPreEmprCancel_Click" CssClass="btn bg-brand-btn waves-effect waves-light btn-std" CausesValidation="false" Visible="false" />
                                        </div>
                                    </div>
                                </div>

                                <asp:GridView ID="grdPreEmpDetails" runat="server" CssClass="gridviewNew" GridLines="None" AutoGenerateColumns="False" DataKeyNames="ID"
                                    BorderStyle="None" Visible="false" OnRowCommand="grdPreEmpDetails_RowCommand">
                                    <Columns>
                                        <asp:BoundField HeaderText="ID" DataField="ID" />
                                        <asp:BoundField HeaderText="Employer" DataField="PreEmpr" />
                                        <asp:BoundField HeaderText="PAN" DataField="PreEmprPAN" />
                                        <asp:BoundField HeaderText="TAN" DataField="PreEmprTAN" />
                                        <asp:BoundField HeaderText="From Date" DataField="BEGDA" DataFormatString="{0:dd-MMM-yyyy}" />
                                        <asp:BoundField HeaderText="To Date" DataField="ENDDA" DataFormatString="{0:dd-MMM-yyyy}" />
                                        <asp:BoundField HeaderText="Salary Income After Exemptions" DataField="GRSAL" HeaderStyle-CssClass="right" ItemStyle-CssClass="right" DataFormatString="{0:#,##0.00}" />
                                        <%-- <asp:BoundField HeaderText="Value of Perquisites u/s 17(2)" DataField="VPRQS" />
                                        <asp:BoundField HeaderText="Profits in lieu of salary u/s 17(3)" DataField="PRSAL" />
                                        <asp:BoundField HeaderText="Exemptions under Section (10)" DataField="EXS10" />--%>
                                        <asp:BoundField HeaderText="PT deducted" DataField="PRTAX" HeaderStyle-CssClass="right" ItemStyle-CssClass="right" DataFormatString="{0:#,##0.00}" />
                                        <asp:BoundField HeaderText="PF deducted" DataField="PRFND" HeaderStyle-CssClass="right" ItemStyle-CssClass="right" DataFormatString="{0:#,##0.00}" />
                                        <asp:BoundField HeaderText="IT Deducted" DataField="TXDED" HeaderStyle-CssClass="right" ItemStyle-CssClass="right" DataFormatString="{0:#,##0.00}" />
                                        <asp:BoundField HeaderText="Surcharge Deducted" DataField="SCDED" HeaderStyle-CssClass="right" ItemStyle-CssClass="right" DataFormatString="{0:#,##0.00}" />
                                        <asp:BoundField HeaderText="Cess Deducted" DataField="ECDED" HeaderStyle-CssClass="right" ItemStyle-CssClass="right" DataFormatString="{0:#,##0.00}" />
                                        <%--<asp:BoundField HeaderText="Status" DataField="STATUS" />--%>
                                        <asp:TemplateField HeaderText="Status">
                                            <ItemTemplate>

                                                <asp:LinkButton ID="lbtnStatus" runat="server" CssClass='<%#Eval("STATUS").ToString() == "Approved"? "btn btn-xs btn-success waves-effect waves-light" :Eval("Status").ToString() == "Rejected" ?"btn btn-xs btn-warning waves-effect waves-light btn-block": "btn btn-xs btn-blue waves-effect waves-light" %>' CausesValidation="False" Text='<%#  Eval("STATUS").ToString() %>' Enabled="false" ForeColor="White"></asp:LinkButton>
                                            </ItemTemplate>

                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LbtnDelete" runat="server" Text="Delete" ForeColor="White" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'
                                                    CommandName="DeleteR" CausesValidation="false" CssClass="btn-xs btn-warning"><i class="fe-trash-2"></i></asp:LinkButton>
                                                <asp:LinkButton ID="lnkEdit" runat="server" Text="Delete" ForeColor="White" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'
                                                    CommandName="EditR" CausesValidation="false" CssClass="btn-xs btn-warning"><i class="fe-edit-1"></i></asp:LinkButton>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </asp:View>

                    <!-- =========== Tab-7 Tab Panel ==================-->
                    <asp:View ID="View7" runat="server">
                        <div class="table-responsive card-box" style="display:none">
                            <div id="Tab-7">
                                <h4>IT Submit All</h4>
                                <h5 class="text-warning">Download the Form 12BB, attach the IT Proofs along with Form 12BB in Zip format</h5>
                                <hr />
                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                    <ContentTemplate>
                                        <div class="row">
                                            <div class="col-sm-2">Year End Only : Upload IT file </div>
                                            <div class="col-sm-8">
                                                <asp:FileUpload ID="FU_ItAllFiles" runat="server" CssClass="form-control" ToolTip="Upload files related to IT current financial year (if multiple files add to compress file and upload)" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ErrorMessage="Please select file..!" ControlToValidate="FU_ItAllFiles"
                                                    runat="server" Display="Dynamic" ForeColor="Red" SetFocusOnError="true" ValidationGroup="VF"/><%-- Enabled="false" --%>
                                                <asp:RegularExpressionValidator ID="REV_FU_ItAllFiles" ValidationExpression="([a-zA-Z0-9\s_\\.\-:])+(.zip|.rar|.pdf)$"
                                                    ControlToValidate="FU_ItAllFiles" runat="server" ForeColor="Red" ErrorMessage="Please select a valid (.rar/.zip/.pdf) file..!"
                                                    Display="Dynamic" ValidationGroup="VF" Enabled="false" />
                                            </div>
                                        </div>

                                        <br />
                                        <div class="row">

                                            <div class="col-sm-2">
                                                <asp:Button ID="btnView12b" runat="server" ToolTip="Enables details of 12 B" Text="Download 12 BB" OnClick="btnView12b_Click" CssClass="btn bg-brand-btn waves-effect waves-light " />
                                            </div>
                                            <div class="col-sm-1">
                                                <asp:Button ID="btnSubmitAll" runat="server" ValidationGroup="VF" ToolTip="All details of IT will be submitted to approval" Text="Submit All" OnClick="btnSubmitAll_Click" CssClass="btn bg-brand-btn waves-effect waves-light btn-std" />


                                            </div>

                                        </div>

                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="btnSubmitAll" />

                                        <asp:PostBackTrigger ControlID="btnView12b" />

                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </asp:View>
                </asp:MultiView>
            </div>
        </div>
        <!-- end Tab Panel-->

    </div>
    <!-- end row -->


    <!--  Modal content for the above example -->
    <div id="dvAppHistory" runat="server" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" style="display: none;" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title" id="myLargeModalLabel">iExpense Approval History </h4>
                    <%--  <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>--%>
                    <asp:Button ID="btnClose" runat="server" Text="x" CssClass="close" data-dismiss="modal" aria-hidden="true" CausesValidation="false" />
                </div>
                <div class="modal-body">
                    <%--	<table class="table table-sm table-bordered table_font_sm">
	<thead>
	<tr>
	<th>#</th>
	<th>Approver</th>
	<th>Date</th>	
	<th>Comments</th>
	<th>Approved</th>	
	</tr>
	</thead>	
	<tbody>
	<tr>
	<td>2</td>
	<td>00002632 - Shifaz K Mohammed </td>
	<td>24-08-2016 </td>	
	<td>OK</td>
	<td><span class="badge badge-success">Approved</span></td>
	</tr>
	<tr>
	<td>2</td>
	<td>00002632 - Shifaz K Mohammed </td>
	<td>24-08-2016 </td>	
	<td>OK</td>
	<td><span class="badge badge-success">Approved</span></td>
	</tr>	
	<tr>
	<td>3</td>
	<td>00002632 - Neville Collins </td>
	<td>24-08-2016 </td>	
	<td>OK</td>
	<td><span class="badge badge-warning">Pending</span></td>
	</tr>
	</tbody>
	</table>--%>
                    <asp:GridView ID="grdAppRejHistory" runat="server" AutoGenerateColumns="False" CssClass="gridviewNew" GridLines="None" ShowHeader="False">

                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <table class="table table-sm table-bordered table_font_sm">
                                        <tr>
                                            <th>Id</th>
                                            <th>Approver</th>

                                            <th>Date</th>
                                            <th>Comments</th>
                                            <th>Approved</th>
                                        </tr>
                                        <asp:Panel ID="pnlAPPROVEDBY1" runat="server" Visible='<%# (Eval("APPROVED_BY1")).ToString()==""?false:true %>'>
                                            <tr>
                                                <td><%# Eval("APPROVED_BY1") %> </td>
                                                <td><%#(Eval("APPROVED_BY1").ToString().StartsWith("fiad")) ? "Finance" : Eval("APPROVED_BY1N")%></td>

                                                <%--   <td class="Tbltd"><%# (Eval("STATUS").ToString()=="Requested")?"Pending": ""%>--%>
                                                <%-- <td class="Tbltd"><%#(Eval("APP_ON1","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APP_ON1","{0:dd-MM-yyyy}")%></td>--%>

                                                <td><%#(Eval("APPROVED_ON1","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APPROVED_ON1","{0:dd-MM-yyyy}")%></td>

                                                <td><%# Eval("REMARKS1") %></td>
                                                <td><span class='<%# Eval("STATUS").ToString()=="Requested"?"badge badge-warning":"badge badge-success" %>'><%# (Eval("STATUS").ToString()=="Requested")?"Pending": (Eval("STATUS").ToString()=="Rejected1")?"Rejected": (Eval("STATUS").ToString()=="Approved1")?"Approved":(Eval("STATUS").ToString()=="HOLD1")?"Hold":(Eval("STATUS").ToString()=="RELEASED1")?"Released":(Eval("STATUS").ToString()=="Cancelled")?"Cancelled":(Eval("STATUS").ToString()=="Saved")?"Saved":"Approved"%></span></td>
                                                <%--<td><span class="{!IF(<%# Eval("STATUS").ToString()=="Approved1")%>,"badge badge-warning","badge badge-success")}"><%# (Eval("STATUS").ToString()=="Requested")?"Pending": (Eval("STATUS").ToString()=="Rejected1")?"Rejected": (Eval("STATUS").ToString()=="Approved1")?"Approved":(Eval("STATUS").ToString()=="HOLD1")?"Hold":(Eval("STATUS").ToString()=="RELEASED1")?"Released":(Eval("STATUS").ToString()=="Cancelled")?"Cancelled":(Eval("STATUS").ToString()=="Saved")?"Saved":"Approved"%></span> </td>--%>
                                            </tr>
                                        </asp:Panel>
                                        <asp:Panel ID="pnlAPPROVEDBY2" runat="server" Visible='<%# (Eval("APPROVED_BY2")).ToString()==""?false:true %>'>
                                            <tr>
                                                <td><%# Eval("APPROVED_BY2") %></td>
                                                <td><%#(Eval("APPROVED_BY2").ToString().StartsWith("fiad")) ? "Finance" : Eval("APPROVED_BY2N") %></td>

                                                <%-- <td class="Tbltd"><%#(Eval("APP_ON2","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APP_ON2","{0:dd-MM-yyyy}")%></td>--%>


                                                <td><%#(Eval("APPROVED_ON2","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APPROVED_ON2","{0:dd-MM-yyyy}")%></td>

                                                <td><%# Eval("REMARKS2") %></td>
                                                <%--<td><span class="badge badge-success"><%# (Eval("STATUS").ToString()=="Approved1")?"Pending": (Eval("STATUS").ToString()=="Rejected2")?"Rejected": (Eval("STATUS").ToString()=="Approved2")?"Approved":(Eval("STATUS").ToString()=="HOLD2")?"Hold":(Eval("STATUS").ToString()=="RELEASED2")?"Released":(Eval("STATUS").ToString()=="Cancelled")?"Cancelled":(Eval("STATUS").ToString()=="Saved")?"Saved":"Approved"%></span> </td>--%>
                                                <td><span class='<%# Eval("STATUS").ToString()=="Requested"||Eval("STATUS").ToString()=="Approved1"?"badge badge-warning":"badge badge-success" %>'><%# (Eval("STATUS").ToString()=="Approved1")?"Pending": (Eval("STATUS").ToString()=="Rejected2")?"Rejected": (Eval("STATUS").ToString()=="Approved2")?"Approved":(Eval("STATUS").ToString()=="HOLD2")?"Hold":(Eval("STATUS").ToString()=="RELEASED2")?"Released":(Eval("STATUS").ToString()=="Cancelled")?"Cancelled":(Eval("STATUS").ToString()=="Saved")?"Saved":"Approved"%></span> </td>
                                            </tr>
                                        </asp:Panel>
                                        <asp:Panel ID="Panel1" runat="server" Visible='<%# (Eval("APPROVED_BY3")).ToString()==""?false:true %>'>

                                            <tr>
                                                <td><%# Eval("APPROVED_BY3") %></td>
                                                <td><%# (Eval("APPROVED_BY3").ToString().StartsWith("fiad")) ? "Finance" : Eval("APPROVED_BY3N") %></td>

                                                <%-- <td class="Tbltd"><%#(Eval("APP_ON3","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APP_ON3","{0:dd-MM-yyyy}")%></td>--%>
                                                <td><%#(Eval("APPROVED_ON3","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APPROVED_ON3","{0:dd-MM-yyyy}")%></td>


                                                <td><%# Eval("REMARKS3") %></td>
                                                <td><span class='<%# Eval("STATUS").ToString()=="Requested"||Eval("STATUS").ToString()=="Approved1"||Eval("STATUS").ToString()=="Approved2"?"badge badge-warning":"badge badge-success" %>'><%# (Eval("STATUS").ToString()=="Approved2")||(Eval("STATUS").ToString()=="Approved1")?"Pending": (Eval("STATUS").ToString()=="Rejected3")?"Rejected": (Eval("STATUS").ToString()=="Rejected2"||(Eval("STATUS").ToString()=="Rejected1"))?"": (Eval("STATUS").ToString()=="Approved3")?"Approved":(Eval("STATUS").ToString()=="HOLD3")?"Hold":(Eval("STATUS").ToString()=="HOLD2")?"":(Eval("STATUS").ToString()=="RELEASED3")?"Released":(Eval("STATUS").ToString()=="RELEASED2")||(Eval("STATUS").ToString()=="RELEASED1")?"":(Eval("STATUS").ToString()=="Cancelled")?"Cancelled":(Eval("STATUS").ToString()=="Saved")?"Saved":"Approved"%></span> </td>
                                            </tr>
                                        </asp:Panel>
                                        <asp:Panel ID="Panel2" runat="server" Visible='<%# (Eval("APPROVED_BY4")).ToString()==""?false:true %>'>
                                            <tr>
                                                <td><%# Eval("APPROVED_BY4") %></td>
                                                <td><%# (Eval("APPROVED_BY4").ToString().StartsWith("fiad")) ? "Finance" : Eval("APPROVED_BY4N") %></td>

                                                <%-- <td class="Tbltd"><%#(Eval("APP_ON4","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APP_ON4","{0:dd-MM-yyyy}")%></td>--%>
                                                <td><%#(Eval("APPROVED_ON4","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APPROVED_ON4","{0:dd-MM-yyyy}")%></td>

                                                <td><%# Eval("REMARKS4") %></td>
                                                <td><span class='<%# Eval("STATUS").ToString()=="Requested"||Eval("STATUS").ToString()=="Approved1"||Eval("STATUS").ToString()=="Approved2"||Eval("STATUS").ToString()=="Approved3"?"badge badge-warning":"badge badge-success" %>'><%# (Eval("STATUS").ToString()=="Approved3")||(Eval("STATUS").ToString()=="Approved2")||(Eval("STATUS").ToString()=="Approved1")?"Pending": (Eval("STATUS").ToString()=="Rejected4")?"Rejected": (Eval("STATUS").ToString()=="Rejected3")||(Eval("STATUS").ToString()=="Rejected2"||(Eval("STATUS").ToString()=="Rejected1"))?"": (Eval("STATUS").ToString()=="Approved4")?"Approved":(Eval("STATUS").ToString()=="HOLD4")?"Hold":(Eval("STATUS").ToString()=="HOLD3")||(Eval("STATUS").ToString()=="HOLD2")?"":(Eval("STATUS").ToString()=="RELEASED4")?"Released":(Eval("STATUS").ToString()=="RELEASED3")||(Eval("STATUS").ToString()=="RELEASED2")||(Eval("STATUS").ToString()=="RELEASED1")?"":(Eval("STATUS").ToString()=="Cancelled")?"Cancelled":(Eval("STATUS").ToString()=="Saved")?"Saved":"Approved"%></span> </td>
                                            </tr>
                                        </asp:Panel>
                                        <asp:Panel ID="Panel3" runat="server" Visible='<%# (Eval("APPROVED_BY5")).ToString()==""?false:true %>'>
                                            <tr>
                                                <td><%# Eval("APPROVED_BY5") %></td>
                                                <td><%# (Eval("APPROVED_BY5").ToString().StartsWith("fiad")) ? "Finance" : Eval("APPROVED_BY5N") %></td>

                                                <%--<td class="Tbltd"><%#(Eval("APP_ON5","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APP_ON5","{0:dd-MM-yyyy}")%></td>--%>
                                                <td><%#(Eval("APPROVED_ON5","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APPROVED_ON5","{0:dd-MM-yyyy}")%></td>

                                                <td><%# Eval("REMARKS5") %></td>
                                                <td><span class='<%# Eval("STATUS").ToString()=="Requested"||Eval("STATUS").ToString()=="Approved1"||Eval("STATUS").ToString()=="Approved2"||Eval("STATUS").ToString()=="Approved3"||Eval("STATUS").ToString()=="Approved4"?"badge badge-warning":"badge badge-success" %>'><%# (Eval("STATUS").ToString()=="Approved4")||(Eval("STATUS").ToString()=="Approved3")||(Eval("STATUS").ToString()=="Approved2")||(Eval("STATUS").ToString()=="Approved1")?"Pending": (Eval("STATUS").ToString()=="Rejected5")?"Rejected": (Eval("STATUS").ToString()=="Rejected4")||(Eval("STATUS").ToString()=="Rejected3")||(Eval("STATUS").ToString()=="Rejected2"||(Eval("STATUS").ToString()=="Rejected1"))?"": (Eval("STATUS").ToString()=="Approved5")?"Approved":(Eval("STATUS").ToString()=="HOLD5")?"Hold":(Eval("STATUS").ToString()=="HOLD4")||(Eval("STATUS").ToString()=="HOLD3")||(Eval("STATUS").ToString()=="HOLD2")?"":(Eval("STATUS").ToString()=="RELEASED5")?"Released":(Eval("STATUS").ToString()=="RELEASED4")||(Eval("STATUS").ToString()=="RELEASED3")||(Eval("STATUS").ToString()=="RELEASED2")||(Eval("STATUS").ToString()=="RELEASED1")?"":(Eval("STATUS").ToString()=="Cancelled")?"Cancelled":(Eval("STATUS").ToString()=="Saved")?"Saved":"Approved"%></span> </td>
                                            </tr>
                                        </asp:Panel>
                                        <asp:Panel ID="Panel4" runat="server" Visible='<%# (Eval("APPROVED_BY6")).ToString()==""?false:true %>'>
                                            <tr>
                                                <td><%# Eval("APPROVED_BY6") %></td>
                                                <td><%# (Eval("APPROVED_BY6").ToString().StartsWith("fiad")) ? "Finance" : Eval("APPROVED_BY6N") %></td>

                                                <%--  <td class="Tbltd"><%#(Eval("APP_ON6","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APP_ON6","{0:dd-MM-yyyy}")%></td>--%>
                                                <td><%#(Eval("APPROVED_ON6","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APPROVED_ON6","{0:dd-MM-yyyy}")%></td>

                                                <td><%# Eval("REMARKS6") %></td>
                                                <td><span class='<%# Eval("STATUS").ToString()=="Requested"||Eval("STATUS").ToString()=="Approved1"||Eval("STATUS").ToString()=="Approved2"||Eval("STATUS").ToString()=="Approved3"||Eval("STATUS").ToString()=="Approved4"||Eval("STATUS").ToString()=="Approved5"?"badge badge-warning":"badge badge-success" %>'><%# (Eval("STATUS").ToString()=="Approved5")||(Eval("STATUS").ToString()=="Approved4")||(Eval("STATUS").ToString()=="Approved3")||(Eval("STATUS").ToString()=="Approved2")||(Eval("STATUS").ToString()=="Approved1")?"Pending": (Eval("STATUS").ToString()=="Rejected6")?"Rejected": (Eval("STATUS").ToString()=="Rejected5")||(Eval("STATUS").ToString()=="Rejected4")||(Eval("STATUS").ToString()=="Rejected3")||(Eval("STATUS").ToString()=="Rejected2")||(Eval("STATUS").ToString()=="Rejected1")?"": (Eval("STATUS").ToString()=="Approved6")?"Approved":(Eval("STATUS").ToString()=="HOLD6")?"Hold":(Eval("STATUS").ToString()=="HOLD5")||(Eval("STATUS").ToString()=="HOLD4")||(Eval("STATUS").ToString()=="HOLD3")||(Eval("STATUS").ToString()=="HOLD2")?"":(Eval("STATUS").ToString()=="RELEASED6")?"Released":(Eval("STATUS").ToString()=="RELEASED5")||(Eval("STATUS").ToString()=="RELEASED4")||(Eval("STATUS").ToString()=="RELEASED3")||(Eval("STATUS").ToString()=="RELEASED2")||(Eval("STATUS").ToString()=="RELEASED1")?"":(Eval("STATUS").ToString()=="Cancelled")?"Cancelled":(Eval("STATUS").ToString()=="Saved")?"Saved":"Approved"%></span> </td>
                                            </tr>
                                        </asp:Panel>
                                        <asp:Panel ID="Panel5" runat="server" Visible='<%# (Eval("APPROVED_BY7")).ToString()==""?false:true %>'>
                                            <tr>
                                                <td><%# Eval("APPROVED_BY7") %></td>
                                                <td><%# (Eval("APPROVED_BY7").ToString().StartsWith("fiad")) ? "Finance" : Eval("APPROVED_BY7N") %></td>

                                                <td><%#(Eval("APPROVED_ON7","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APPROVED_ON7","{0:dd-MM-yyyy}")%></td>
                                                <%--<td class="Tbltd"><%# (Eval("STATUS").ToString()=="Approved6")?Eval("APP_ON6","{0:dd-MM-yyyy}").ToString(): (Eval("STATUS").ToString()=="HOLD6")?Eval("HOLD_ON6","{0:dd-MM-yyyy}").ToString(): (Eval("STATUS").ToString()=="RELEASED6")?Eval("RELEASED_ON6","{0:dd-MM-yyyy}").ToString():(Eval("STATUS").ToString()=="Rejected6")?Eval("APP_ON6","{0:dd-MM-yyyy}").ToString():(Eval("APP_ON6","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APP_ON6","{0:dd-MM-yyyy}")%> </td>--%>

                                                <td><%# Eval("REMARKS7") %></td>
                                                <td><span class='<%# Eval("STATUS").ToString()=="Requested"||Eval("STATUS").ToString()=="Approved1"||Eval("STATUS").ToString()=="Approved2"||Eval("STATUS").ToString()=="Approved3"||Eval("STATUS").ToString()=="Approved4"||Eval("STATUS").ToString()=="Approved5"||Eval("STATUS").ToString()=="Approved6"?"badge badge-warning":"badge badge-success" %>'><%# (Eval("STATUS").ToString()=="Requested")||(Eval("STATUS").ToString()=="Approved5")||(Eval("STATUS").ToString()=="Approved4")||(Eval("STATUS").ToString()=="Approved3")||(Eval("STATUS").ToString()=="Approved2")||(Eval("STATUS").ToString()=="Approved1")?"Pending": (Eval("STATUS").ToString()=="Rejected6")?"Rejected": (Eval("STATUS").ToString()=="Rejected5")||(Eval("STATUS").ToString()=="Rejected4")||(Eval("STATUS").ToString()=="Rejected3")||(Eval("STATUS").ToString()=="Rejected2")||(Eval("STATUS").ToString()=="Rejected1")?"": (Eval("STATUS").ToString()=="Approved6")?"Approved":"Approved"%></span> </td>
                                            </tr>
                                        </asp:Panel>
                                        <asp:Panel ID="Panel6" runat="server" Visible='<%# (Eval("APPROVED_BY8")).ToString()==""?false:true %>'>
                                            <tr>
                                                <td><%# Eval("APPROVED_BY8") %></td>
                                                <td><%# (Eval("APPROVED_BY8").ToString().StartsWith("fiad")) ? "Finance" : Eval("APPROVED_BY8") %></td>

                                                <td><%#(Eval("APPROVED_ON8","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APPROVED_ON8","{0:dd-MM-yyyy}")%></td>
                                                <%--<td class="Tbltd"><%# (Eval("STATUS").ToString()=="Approved6")?Eval("APP_ON6","{0:dd-MM-yyyy}").ToString(): (Eval("STATUS").ToString()=="HOLD6")?Eval("HOLD_ON6","{0:dd-MM-yyyy}").ToString(): (Eval("STATUS").ToString()=="RELEASED6")?Eval("RELEASED_ON6","{0:dd-MM-yyyy}").ToString():(Eval("STATUS").ToString()=="Rejected6")?Eval("APP_ON6","{0:dd-MM-yyyy}").ToString():(Eval("APP_ON6","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APP_ON6","{0:dd-MM-yyyy}")%> </td>--%>

                                                <td><%# Eval("REMARKS8") %></td>
                                                <td><span class='<%# Eval("STATUS").ToString()=="Requested"||Eval("STATUS").ToString()=="Approved1"||Eval("STATUS").ToString()=="Approved2"||Eval("STATUS").ToString()=="Approved3"||Eval("STATUS").ToString()=="Approved4"||Eval("STATUS").ToString()=="Approved5"||Eval("STATUS").ToString()=="Approved6"||Eval("STATUS").ToString()=="Approved7"?"badge badge-warning":"badge badge-success" %>'><%# (Eval("STATUS").ToString()=="Requested")||(Eval("STATUS").ToString()=="Approved5")||(Eval("STATUS").ToString()=="Approved4")||(Eval("STATUS").ToString()=="Approved3")||(Eval("STATUS").ToString()=="Approved2")||(Eval("STATUS").ToString()=="Approved1")?"Pending": (Eval("STATUS").ToString()=="Rejected6")?"Rejected": (Eval("STATUS").ToString()=="Rejected5")||(Eval("STATUS").ToString()=="Rejected4")||(Eval("STATUS").ToString()=="Rejected3")||(Eval("STATUS").ToString()=="Rejected2")||(Eval("STATUS").ToString()=="Rejected1")?"": (Eval("STATUS").ToString()=="Approved6")?"Approved":"Approved"%></span> </td>
                                            </tr>
                                        </asp:Panel>
                                        <asp:Panel ID="Panel7" runat="server" Visible='<%# (Eval("APPROVED_BY9")).ToString()==""?false:true %>'>
                                            <tr>
                                                <td><%# Eval("APPROVED_BY9") %></td>
                                                <td><%# (Eval("APPROVED_BY9").ToString().StartsWith("fiad")) ? "Finance" : Eval("APPROVED_BY9") %></td>

                                                <td><%#(Eval("APPROVED_ON9","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APPROVED_ON9","{0:dd-MM-yyyy}")%></td>
                                                <%--<td class="Tbltd"><%# (Eval("STATUS").ToString()=="Approved6")?Eval("APP_ON6","{0:dd-MM-yyyy}").ToString(): (Eval("STATUS").ToString()=="HOLD6")?Eval("HOLD_ON6","{0:dd-MM-yyyy}").ToString(): (Eval("STATUS").ToString()=="RELEASED6")?Eval("RELEASED_ON6","{0:dd-MM-yyyy}").ToString():(Eval("STATUS").ToString()=="Rejected6")?Eval("APP_ON6","{0:dd-MM-yyyy}").ToString():(Eval("APP_ON6","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APP_ON6","{0:dd-MM-yyyy}")%> </td>--%>

                                                <td><%# Eval("REMARKS9") %></td>
                                                <td><span class='<%# Eval("STATUS").ToString()=="Requested"||Eval("STATUS").ToString()=="Approved1"||Eval("STATUS").ToString()=="Approved2"||Eval("STATUS").ToString()=="Approved3"||Eval("STATUS").ToString()=="Approved4"||Eval("STATUS").ToString()=="Approved5"||Eval("STATUS").ToString()=="Approved6"||Eval("STATUS").ToString()=="Approved7"||Eval("STATUS").ToString()=="Approved8"?"badge badge-warning":"badge badge-success" %>'><%# (Eval("STATUS").ToString()=="Requested")||(Eval("STATUS").ToString()=="Approved5")||(Eval("STATUS").ToString()=="Approved4")||(Eval("STATUS").ToString()=="Approved3")||(Eval("STATUS").ToString()=="Approved2")||(Eval("STATUS").ToString()=="Approved1")?"Pending": (Eval("STATUS").ToString()=="Rejected6")?"Rejected": (Eval("STATUS").ToString()=="Rejected5")||(Eval("STATUS").ToString()=="Rejected4")||(Eval("STATUS").ToString()=="Rejected3")||(Eval("STATUS").ToString()=="Rejected2")||(Eval("STATUS").ToString()=="Rejected1")?"": (Eval("STATUS").ToString()=="Approved6")?"Approved":"Approved"%></span> </td>
                                            </tr>
                                        </asp:Panel>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--<asp:BoundField DataField="APPROVEDBY1" HeaderText="Id" />
                                    <asp:BoundField DataField="APPROVEDBY1" HeaderText="Name" />
                                    <asp:BoundField DataField="APPROVEDBY1" HeaderText="Action" />
                                    <asp:BoundField DataField="APPROVEDBY1" HeaderText="Action Date" />
                                    <asp:BoundField DataField="APPROVEDBY1" HeaderText="Comments" />--%>
                        </Columns>

                    </asp:GridView>
                </div>
                <div class="modal-footer">
                    <%--<button type="button" class="btn btn-light waves-effect" data-dismiss="modal">Close</button>--%>
                    <asp:Button ID="Button1" runat="server" Text="Close" CssClass="btn btn-light waves-effect" data-dismiss="modal" CausesValidation="false" />
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

    <!-- /.modal -->

    <!--  Modal content for the above example -->
    <div class="modal fade Approved" tabindex="-1" role="dialog" aria-labelledby=".Approved" style="display: none;" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title" id="Approved">PR Approved View </h4>
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                </div>
                <div class="modal-body">
                    <table class="table table-sm table-bordered table_font_sm">
                        <thead>
                            <tr>
                                <th>#</th>
                                <th>Approver</th>
                                <th>Date</th>
                                <th>Comments</th>
                                <th>Approved</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td>2</td>
                                <td>00002632 - Shifaz K Mohammed </td>
                                <td>24-08-2016 </td>
                                <td>OK</td>
                                <td><span class="badge badge-success">Approved</span></td>
                            </tr>
                            <tr>
                                <td>2</td>
                                <td>00002632 - Shifaz K Mohammed </td>
                                <td>24-08-2016 </td>
                                <td>OK</td>
                                <td><span class="badge badge-success">Approved</span></td>
                            </tr>
                            <tr>
                                <td>3</td>
                                <td>00002632 - Neville Collins </td>
                                <td>24-08-2016 </td>
                                <td>OK</td>
                                <td><span class="badge badge-success">Approved</span></td>
                            </tr>
                        </tbody>
                    </table>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-light waves-effect" data-dismiss="modal">Close</button>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

    <!-- /.modal -->
    <asp:Panel ID="pnl12b" runat="server" CssClass="hidden">


        <div id="exprt" runat="server">

            <div class="" style="margin: 0 5% !important">

                <table width="100%" id="tbl">
                    <tbody>
                        <tr>
                            <td class="text-center" colspan="4">
                                <br />
                                <h3><b>FORM NO. 12BB</b></h3>
                                <h4>(See rule 26C)</h4>
                                <h4><b>Statement showing particulars of claims by an employee for deduction of tax under section 192</b><br />
                                    <br />
                                </h4>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2"><b>1. Name and address of the employee</b></td>
                            <%-- <td>:</td>--%>
                            <td colspan="2" class="text-left">:&nbsp;
                                <asp:Label ID="lblEmpName" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td colspan="2"><b>2. Permanent Account Number of the employee</b></td>
                            <%-- <td>:</td>--%>
                            <td colspan="2" class="text-left">:&nbsp;
                                <asp:Label ID="lblEmpPAN" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td colspan="2" class="text-l"><b>3. Financial year</b></td>
                            <%--  <td>:</td>--%>
                            <td colspan="2" class="text-left">:&nbsp;
                                <asp:Label ID="lblCurntYear" runat="server"></asp:Label></td>
                        </tr>
                        <tr class="border border-dark text-center">
                            <td colspan="4"><b>Details of claims and evidence there of</b></td>
                        </tr>
                        <tr style="text-align: center">
                            <th>Sl. No.
                            </th>
                            <th>Nature of claim</th>
                            <th>Amount
                        (Rs.)
                            </th>
                            <th>Evidence / particulars</th>
                        </tr>
                        <tr style="font-weight: bold; text-align: center">
                            <td>(1)</td>
                            <td>(2)</td>
                            <td>(3)</td>
                            <td>(4)</td>
                        </tr>
                        <%-- Value starts --%>
                        <tr>
                            <td style="text-align: right">1.</td>
                            <td>House Rent Allowance:</td>
                            <td style="text-align: right">
                                <asp:Label ID="lblHRA" runat="server"></asp:Label></td>
                            <td>House Rent Receipts</td>
                        </tr>
                        <asp:Literal ID="ltHsdtls" runat="server"></asp:Literal>
                        <tr>
                            <td></td>
                            <td>Note: Permanent Account Number shall be furnished if the aggregate rent paid during the
                        previous year exceeds one lakh rupees
                            </td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td style="text-align: right">2.</td>
                            <td>Leave travel concessions or assistance</td>
                            <td style="text-align: right">
                                <asp:Label ID="lblLTA" runat="server"></asp:Label></td>
                            <td>Travel Receipts/Tickets</td>
                        </tr>
                        <tr>
                            <td style="text-align: right">3.</td>
                            <td>Deduction of interest on borrowing</td>
                            <td style="text-align: right">
                                <asp:Label ID="lblDedIOB" runat="server"></asp:Label></td>
                            <td>Provisional Certificate from Bank/Financial Institution/Lender</td>
                        </tr>
                        <tr>
                            <td style="text-align: right"></td>
                            <td>
                                <h4>(i)	Interest payable/paid to the lender</h4>
                                <asp:Literal ID="ltDedIOBSelfIntpayable" runat="server"></asp:Literal><br />
                                <asp:Literal ID="ltDedIOBLOIntpayable" runat="server"></asp:Literal><br />
                                <h4>(ii) Name of the lender</h4>
                                <asp:Literal ID="ltDedIOBSelfltLenderName" runat="server"></asp:Literal><br />
                                <asp:Literal ID="ltDedIOBLOltLenderName" runat="server"></asp:Literal><br />
                                <h4>(iii) Address of the lender</h4>
                                <asp:Literal ID="ltDedIOBSelfLenderAddress" runat="server"></asp:Literal><br />
                                <asp:Literal ID="ltDedIOBLOLenderAddress" runat="server"></asp:Literal><br />
                                <h4>(iv) Permanent Account Number of the lender</h4>
                                <asp:Literal ID="ltDedIOBSelfLenderPAN" runat="server"></asp:Literal><br />
                                <asp:Literal ID="ltDedIOBLOfLenderPAN" runat="server"></asp:Literal><br />
                            </td>
                            <td style="text-align: right"></td>
                            <td></td>
                        </tr>
                        <asp:Literal ID="ltDedins" runat="server"></asp:Literal>

                        <tr>
                            <td style="text-align: right">4.</td>
                            <td>Deduction under Chapter VI-A</td>
                            <td style="text-align: right">
                                <asp:Label ID="lblDedUC6A" runat="server" Text="0" Visible="false"></asp:Label></td>
                            <td>Photocopy of the investment proofs</td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>(A) Section 80C,80CCC and 80CCD</td>
                            <td style="text-align: right">
                                <asp:Label ID="lblSec80CCCCCCD" runat="server" Text="0" Visible="false"></asp:Label></td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>(i) Section 80C</td>
                            <%--586-Sec80C--%>
                            <td style="text-align: right">
                                <asp:Label ID="lblSection80C" runat="server" Text="0"></asp:Label></td>
                            <td></td>
                            <td></td>
                        </tr>
                        <asp:Literal ID="lt80c" runat="server"></asp:Literal>
                        <tr>
                            <td></td>
                            <td>(ii) Section 80CCC</td>
                            <td style="text-align: right">
                                <asp:Label ID="lblSec80CCC" runat="server" Text="0"></asp:Label></td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>(iii) Section 80CCD</td>
                            <td style="text-align: right">
                                <asp:Label ID="lblSec80CCD" runat="server" Text="0" Visible="false"></asp:Label></td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>(B) Other sections (e.g. 80E, 80G, 80TTA, etc.) under Chapter VI-A.</td>
                            <td style="text-align: right">
                                <asp:Label ID="lblOtherSecs" runat="server" Text="0"></asp:Label></td>
                            <td></td>
                            <td></td>
                        </tr>
                        <%-- <tr>
                            <td style="text-align: right">5.</td>
                            <td>Deduction under Section 24</td>
                            <td></td>
                            <td></td>
                        </tr>--%>
                        <%-- Value ends --%>
                        <tr>
                            <td colspan="4" style="text-align: center"><b>Verification</b></td>
                        </tr>
                        <tr>
                            <td colspan="4" style="text-align: left">I, <span id="spnNAme" runat="server"></span>hereby certify that the information given above is complete and correct.</td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align: left">Place :</td>
                            <td colspan="2" style="text-align: left; border-bottom: none !important"></td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align: left">Date : <%=DateTime.Now.Date.ToString("yyyy-MM-dd") %></td>
                            <td colspan="2" style="text-align: center; border-top: none !important">(Signature of the employee)</td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align: left">Designation : <span id="spnEmpDesg" runat="server"></span></td>
                            <td colspan="2" style="text-align: left">Full Name : <span id="spnEmpName" runat="server"></span></td>
                        </tr>
                    </tbody>
                </table>

            </div>

            <br />
            <hr />
            <br />

            <div style="margin: 0 5% !important; page-break-before: always">

                <table width="100%">
                    <tr>
                        <td class="text-center" colspan="8">
                            <br />
                            <h3><b>Subex</b></h3>
                            <h4><b>POI for the period of Apr 2021 To Mar 2022</b><br />
                                <br />
                            </h4>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" class="font-weight-bold" style="border: 1px solid black">Name :</td>
                        <td colspan="2" style="border: 1px solid black">
                            <asp:Label ID="lblEmpName2" runat="server"></asp:Label></td>
                        <td colspan="2" style="border: 1px solid black" class="font-weight-bold">Employee No :</td>
                        <td colspan="2" style="border: 1px solid black">
                            <asp:Label ID="lblEmpID" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td colspan="2" style="border: 1px solid black" class="font-weight-bold">Date Of Join :</td>
                        <td colspan="2" style="border: 1px solid black">
                            <asp:Label ID="lblDOJ" runat="server"></asp:Label></td>
                        <td colspan="2" style="border: 1px solid black" class="font-weight-bold">Permanent Account Number :</td>
                        <td colspan="2" style="border: 1px solid black">
                            <asp:Label ID="lblEMPPAN2" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td colspan="2" style="border: 1px solid black" class="font-weight-bold">Approved Date :</td>
                        <td colspan="6" style="border: 1px solid black"></td>
                    </tr>
                    <tr>
                        <td colspan="8"></td>
                    </tr>
                </table>
                <table id="tbl2" width="100%">
                    <tr id="trM" class="bg-soft-primary font-weight-bold text-center">
                        <td style="width: 5% !important; border: 1px solid black">Sl. No.</td>
                        <td style="width: 14% !important; border: 1px solid black">Month</td>
                        <td style="width: 14% !important; border: 1px solid black">Location Indicator</td>
                        <td style="width: 14% !important; border: 1px solid black">Rent per month (Rs.)</td>
                        <td style="width: 14% !important; border: 1px solid black">Approved Amount</td>
                        <td style="width: 14% !important; border: 1px solid black">Status</td>
                        <td style="width: 25% !important; border: 1px solid black" colspan="2">Remarks</td>
                    </tr>

                    <%-- <tr class="bg-soft-primary font-weight-bold text-center">
                <td>Sl. No.</td>
                <td>Particulars</td>
                <td>Amount(Rs.)</td>
                <td>Approved Amount</td>
                <td>Status</td>
                <td>No. of Doc</td>
                <td>Proof</td>
                <td>Remarks</td>
            </tr>--%>
                </table>

                <asp:Literal ID="ltMonths" runat="server"></asp:Literal>

                <br />

                <table width="100%" id="tbl4">
                    <tr id="trM1" class="bg-soft-primary font-weight-bold text-center">
                        <td style="width: 5% !important;">Sl. No.</td>
                        <td style="width: 25% !important;">Particulars</td>
                        <td style="width: 13% !important;">Amount(Rs.)</td>
                        <td style="width: 13% !important;">Approved Amount</td>
                        <td style="width: 9% !important;">Status</td>
                        <td style="width: 5% !important;">No. of Doc</td>
                        <td style="width: 5% !important;">Proof</td>
                        <td style="width: 25% !important;">Remarks</td>
                    </tr>
                    <tr>
                        <td colspan="8">
                            <h4>House Rent Paid</h4>
                        </td>
                    </tr>
                    <tr>
                        <td class="text-right" style="width: 5% !important;">1</td>
                        <td style="width: 25% !important;">House Rent Paid
                        </td>
                        <td class="text-right" style="width: 13% !important;">
                            <asp:Label ID="lblrntAmt" runat="server"></asp:Label></td>
                        <td class="text-right" style="width: 13% !important;">
                            <asp:Label ID="lblAppAmt" runat="server"></asp:Label></td>
                        <td class="text-center" style="width: 9% !important;">
                            <asp:Label ID="lblSt" runat="server"></asp:Label></td>
                        <td style="width: 5% !important;" class="text-right">0</td>
                        <td style="width: 5% !important;">No</td>
                        <td style="width: 25% !important;"></td>
                    </tr>
                    <%-- <tr>
                        <td colspan="8">
                            <h4>Deduction Under Section 24</h4>
                        </td>
                    </tr>--%>


                    <tr>
                        <td colspan="8">
                            <h4>Deduction of interest on borrowing:</h4>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="8">
                            <h4>(i)	Interest payable/paid to the lender</h4>
                            <asp:Literal ID="ltSelfIntpayable" runat="server"></asp:Literal><br />
                            <asp:Literal ID="ltLOIntpayable" runat="server"></asp:Literal><br />
                            <h4>(ii) Name of the lender</h4>
                            <asp:Literal ID="ltSelfltLenderName" runat="server"></asp:Literal><br />
                            <asp:Literal ID="ltLOltLenderName" runat="server"></asp:Literal><br />
                            <h4>(iii) Address of the lender</h4>
                            <asp:Literal ID="ltSelfLenderAddress" runat="server"></asp:Literal><br />
                            <asp:Literal ID="ltLOLenderAddress" runat="server"></asp:Literal><br />
                            <h4>(iv) Permanent Account Number of the lender</h4>
                            <asp:Literal ID="ltSelfLenderPAN" runat="server"></asp:Literal><br />
                            <asp:Literal ID="ltLOfLenderPAN" runat="server"></asp:Literal><br />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="8">
                            <h4>Deduction Under Chapter VI A</h4>
                            <%-- <h6>(A)	Section 80C</h6>
                            <asp:Literal ID="ltSec80C12BB" runat="server"></asp:Literal>
                            <h6>(B)	Other sections (e.g., 80E, 80G, 80TTA, etc.) under Chapter VI-A.</h6>
                            <asp:Literal ID="ltSec8012BB" runat="server"></asp:Literal>--%>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="8">
                            <h4>(A)	Section 80C</h4>
                            <asp:Literal ID="ltSec80C12BB" runat="server"></asp:Literal><br />
                            <h4>(B)	Other sections (e.g., 80E, 80G, 80TTA, etc.) under Chapter VI-A.</h4>
                            <asp:Literal ID="ltSec8012BB" runat="server"></asp:Literal>
                        </td>
                    </tr>

                </table>

                <asp:Literal ID="ltOtherDetls" runat="server"></asp:Literal>

                <h4><b>Note :</b></h4>
                <p>The information/details above, as required for deduction of tax u/s 192 of the Income Tax Act, has been entered by the employee through an authorized login on the portal.</p>
            </div>

        </div>

    </asp:Panel>

    <script>
        function createPDF() {



            var sTable = document.getElementById('ContentPlaceHolder1_MainContent_exprt').innerHTML;



            var style = " <link href='../../NewAssets/css/bootstrap.css' rel='stylesheet' /> <style>     table {            color: black !important;        }        .text-left {            text-align: left !important;        }        #tbl tr:nth-child(n+6) td, th, #tbl2 tr:nth-child(n+2) td, th, #tabl3 tr:nth-child(n) td, th, #tbl4 tr:nth-child(n) td, th, #tabl4 tr:nth-child(n) td, th {            border: 1px solid black;        }            #tbl tr:nth-child(n+6) td:nth-child(4), th:nth-child(4) {                width: 25% !important;            }            #tbl tr:nth-child(n+6) td:nth-child(3), th:nth-child(3) {                width: 25% !important;            }            #tbl tr:nth-child(n+6) td:nth-child(2), th :nth-child(2) {                width: 50% !important;            }            #tbl tr:nth-child(n+6) td:nth-child(1), th:nth-child(1) {                width: 5% !important;            }        #tabl3 tr td, #trM td {            width: 14.3%;        }        #tabl4 td:nth-child(1), #tbl4 td:nth-child(1) {            width: 5% !important;        }        #tabl4 td:nth-child(2), #tbl4 td:nth-child(2) {            width: 25% !important;        }        #tabl4 td:nth-child(n+3), #tbl4 td:nth-child(n+3) {            width: 10% !important;        }                #tabl4 td:nth-child(8), #tbl4 td:nth-child(8) {            width: 20% !important;        }";
            style = style + "</style>";



            // CREATE A WINDOW OBJECT.
            var win = window.open('', '', 'height=700,width=700');



            win.document.write('<html><head>');
            win.document.write('<title>IT 12 B</title>');   // <title> FOR PDF HEADER.
            win.document.write(style);          // ADD STYLE INSIDE THE HEAD TAG.
            win.document.write('</head>');
            win.document.write('<body style="background-color: white !important">');
            win.document.write(sTable);         // THE TABLE CONTENTS INSIDE THE BODY TAG.
            win.document.write('</body></html>');



            win.document.close(); // CLOSE THE CURRENT WINDOW.



            win.print();    // PRINT THE CONTENTS.



        }
    </script>

</asp:Content>
