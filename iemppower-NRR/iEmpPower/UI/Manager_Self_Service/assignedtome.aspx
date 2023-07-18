<%@ Page Title="Assigned to me" Language="C#" MasterPageFile="~/UI/SubSiteMaster.master" AutoEventWireup="true" EnableEventValidation="false"  MaintainScrollPositionOnPostback="true"
    Inherits="UI_Manager_Self_Service_assignedtome" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" Theme="SkinFile" CodeBehind="assignedtome.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="MainContent">
    <style type="text/css">
        #MainContent_grdRecordTime
        {
            overflow: scroll !important;
        }

        #MainContent_pnlRecordWorking
        {
            overflow: scroll !important;
        }

        #__tab_MainContent_tcAssignToMe_tabPending, #__tab_MainContent_tcAssignToMe_TabPanel2
        {
            height: 18.5px !important;
        }

        .Td06
        {
            color: #004080;
            font-size: 13px;
            width: 120px;
            padding: 3px;
            text-align: justify !important;
        }

        .Td10
        {
            font-size: 13px;
            width: 120px;
            text-align: justify !important;
        }


        .GvCls01
        {
            border-collapse: collapse;
            font: normal normal normal 11px/22px opensans,Verdana,Arial,Helvetica,sans-serif;
            border: 1px solid #ddd !important;
            background-color: #fafafa;
            margin-left: 8px;
        }

            .GvCls01 td
            {
                border-collapse: collapse !important;
                font: normal normal normal 11px/13px opensans,Verdana,Arial,Helvetica,sans-serif;
                border: 0px solid #fff !important;
                margin: 3px 5px 3px 10px !important;
            }
    </style>


    <script src="../../Utilities/ValidationMessages.js" type="text/javascript"></script>
    <script src="../../Utilities/Validations.js" type="text/javascript"></script>

    <script type="text/javascript">
        function ValidateControls(txtValue) {
            clearDirty();
            document.getElementById("<%= lblMessageBoard.ClientID %>").style.color = "red";
            var txtRemarks = document.getElementById(txtValue);
            if (!TextBoxEmpty(txtRemarks)) {
                document.getElementById("<%= lblMessageBoard.ClientID %>").innerText = msgRemarksEmpty;
                document.getElementById(txtValue).focus();
                return false;
            }
        }
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
        function Validate(sender, args) {
            var gridView = document.getElementById("<%=grdPending.ClientID %>");
            var checkBoxes = gridView.getElementsByTagName("input");
            for (var i = 0; i < checkBoxes.length; i++) {
                if (checkBoxes[i].type == "checkbox" && checkBoxes[i].checked) {
                    args.IsValid = true;
                    return;
                }
            }
            args.IsValid = false;
        }
    </script>  

        <div class="row">
        <div class="col-12">
            <div class="page-title-box">
                <div class="page-title-right">
                    <ol class="breadcrumb m-0">
                        <li class="breadcrumb-item"><a href="../Default.aspx">Home</a></li>
                        <li class="breadcrumb-item active">Assigned to me</li>
                    </ol>
                </div>
                <h4 class="page-title">Assigned to me
                    <asp:Label ID="Label1" runat="server" CssClass="msgboard"></asp:Label>
                </h4>
            </div>
        </div>
    </div>

     <span class="hidden">
        <asp:Button ID="btnEntryKey" runat="server" Text="" /></span>
    <div id="DirtyLabel" style="color: Red;" class="hide"></div>
    <div>
        <span id="bold" runat="server" style="font-weight: bold"></span>
    </div>


    <div>
        <br />
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
               <div class="card-box">
                    <div id="real_time_chart" class="dashboard-flot-chart">
                        

                        <div id="DivAppRej" runat="server">
                            <div>
                                <div class="DivSpacer01"></div>
                                <div class="clear">
                                    <asp:Label ID="lblMessageBoard" runat="server" CssClass="lblMsg"
                                        meta:resourcekey="lblMessageBoardResource1"></asp:Label>
                                </div>
                                <br />
                                <div>
                                    <div class="row">
                                        <div class="col-sm-6">
                                           <ul class="nav nav-pills navtab-bg">
                                        <li class="nav-item font-12">
                                            <asp:LinkButton runat="server" ID="Tab1" class="nav-link p-2" OnClick="Tab1_Click" CausesValidation="false"><i class="fe-alert-circle"></i>
                                            Pending </asp:LinkButton></li>
                                        <li class="nav-item font-12">
                                            <asp:LinkButton runat="server" ID="Tab2" class="nav-link  p-2" OnClick="Tab2_Click" CausesValidation="false"><i class="fe-check-circle"></i>
                                             Completed </asp:LinkButton></li>
                                    </ul>
                                        </div>
                                        <div class="col-sm-6" id="HRTabSel" runat="server">
                                            <div class="row text-right">
                                                <div class="col-sm-12">
                                                    <asp:DropDownList ID="DDL_HRTabSel" CssClass="txtDropDownwidth" runat="server" OnSelectedIndexChanged="DDL_HRTabSel_SelectedIndexChanged" AutoPostBack="true">
                                                    </asp:DropDownList>
                                                    <asp:DropDownList runat="server" ID="DDLYear" CssClass="txtDropDownwidth" OnSelectedIndexChanged="DDLYear_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <br />

                                    <div runat="server" id="divView1">                                      
                                            <div class="respovrflw">
                                                <asp:GridView ID="grdPending" runat="server" DataKeyNames="PERNR,PKEY,ENAME,PLSXT,ID,TableTyp,REVIEW,CHANGE_APPROVAL,LAST_ACTIVITY_DATE,MMODON" OnPageIndexChanging="grdPending_PageIndexChanging"
                                                    OnSorting="grdPending_Sorting" AutoGenerateColumns="False" OnRowDataBound="grdPending_RowDataBound"
                                                    AllowSorting="True" OnRowCommand="grdPending_RowCommand">
                                                    <Columns>
                                                        <asp:BoundField DataField="RowNumber" HeaderText="Sl No." />
                                                            <asp:BoundField DataField="" HeaderText="Employee ID" ReadOnly="True"/>
                                                        <asp:BoundField DataField="ENAME" HeaderText="Employee Name"
                                                            SortExpression="ENAME" />

                                                        <asp:BoundField DataField="CHANGE_APPROVAL" HeaderText="Type of Request" ReadOnly="True"
                                                            SortExpression="CHANGE_APPROVAL" />
                                                        <asp:BoundField DataField="Subtype" HeaderText="Request Subtype" ReadOnly="True"
                                                            SortExpression="Subtype" />
                                                        <asp:BoundField DataField="REVIEW" HeaderText="Review" ReadOnly="True"
                                                            SortExpression="REVIEW" meta:resourcekey="BoundFieldResource5" />
                                                        <asp:BoundField DataField="LAST_ACTIVITY_DATE" HeaderText="Last Activity Date" ReadOnly="True"
                                                            SortExpression="LAST_ACTIVITY_DATE" meta:resourcekey="BoundFieldResource6" />
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Action
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="grdPendingView" runat="server" CausesValidation="false" CommandName="VIEW"
                                                                    CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CssClass="btn btn-xs btn-warning"><i class="fe-eye"></i></asp:LinkButton>

                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                <div style="margin-left:-3px">
                                                                    <asp:CheckBox runat="server" ID="chkboxSelectAll" Text=" " CssClass="chkboxStyl" AutoPostBack="true" OnCheckedChanged="chkboxSelectAll_CheckedChanged" />                                                                
                                                                    </div>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkEmp" runat="server" Text=" " AutoPostBack="true" OnCheckedChanged="chkEmp_CheckedChanged" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                         <asp:TemplateField HeaderText="" Visible="false">
                                                            <ItemTemplate>
                                                            <asp:Label ID="LBL_empid" runat="server" Text='<%# Eval("PERNR") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField> 
                                                    </Columns>
                                                </asp:GridView>

                                            <div class="row col-md-12" id="divpageNcnt" runat="server">
                                                <div class="col-md-3" style="margin-top: 5px" id="divpendingrecordcount" runat="server"></div>
                                                <div class="col-md-9 DivSpacer01 Div02 repeater text-xl-right">
                                                    <asp:Repeater ID="RptrPendingPager" runat="server">
                                                        <ItemTemplate>
                                                            <ul class="pagination pagination-rounded" style="display: inline-block">
                                                                <li class='paginate_button page-item <%# Convert.ToBoolean(Eval("Enabled")) ? "" : "active" %>'>
                                                                    <asp:LinkButton ID="lnkPage" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>' Enabled='<%# Eval("Enabled") %>' OnClick="LbtnPendingPage_Changed" CausesValidation="false" CssClass='page-link <%# Convert.ToBoolean(Eval("Enabled")) ? "page_enabled" : "page_disabled" %>'></asp:LinkButton>
                                                                </li>
                                                            </ul>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </div>
                                            </div>
                                        </div>
                                            <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="Please select at least one record."
                                                ClientValidationFunction="Validate" ForeColor="Red"></asp:CustomValidator><br />
                                            <div class="row">
                                                <div class="col-sm-6" id="divMassBtn" runat="server" visible="False">
                                                        <asp:Button ID="btnLRApp" runat="server" Width="80px" Text="Approve" OnClick="btnLRAppApprove_Click" />                                                   
                                                        <asp:Button ID="btnLRRej" runat="server" Width="80px" Text="Reject" OnClick="btnLRRejReject_Click" />

                                                </div>
                                            </div>
                                        </div>
                                        <div id="divView2" runat="server">
                                            <div class="respovrflw">
                                                <asp:GridView ID="grdCompleted" runat="server" AutoGenerateColumns="False" DataKeyNames="PERNR,PKEY,ENAME,PLSXT,ID,TableTyp,REVIEW,LAST_ACTIVITY_DATE,MODIFIEDON,CHANGE_APPROVAL"
                                                    OnPageIndexChanging="grdCompleted_PageIndexChanging" OnSorting="grdCompleted_Sorting" AllowSorting="True" AllowPaging="True" OnRowDataBound="grdCompleted_RowDataBound"
                                                   OnRowCommand="grdCompleted_RowCommand" PageSize="10" Width="100%" Visible="true">
                                                    <Columns>
                                                        <asp:BoundField DataField="RowNumber" HeaderText="Sl No." />
                                                        <asp:BoundField DataField="" HeaderText="Employee ID" ReadOnly="true" SortExpression="PERNR" />
                                                        <asp:BoundField DataField="ENAME" HeaderText="Employee Name" SortExpression="ENAME" />
                                                        <asp:BoundField DataField="CHANGE_APPROVAL" HeaderText="Type of Request" ReadOnly="True" SortExpression="CHANGE_APPROVAL" />
                                                        <asp:BoundField DataField="Subtype" HeaderText="Request Subtype" ReadOnly="True" SortExpression="Subtype" />
                                                        <asp:BoundField DataField="REVIEW" HeaderText="Review" ReadOnly="True" SortExpression="REVIEW" meta:resourcekey="BoundFieldResource11" />
                                                        <asp:BoundField DataField="AppByName" HeaderText="Approved By" ReadOnly="true" SortExpression="AppByName" />
                                                        <asp:BoundField DataField="LAST_ACTIVITY_DATE" HeaderText="Last Activity Date" ReadOnly="True" SortExpression="LAST_ACTIVITY_DATE" meta:resourcekey="BoundFieldResource12" />
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="grdCompletedView" runat="server" CausesValidation="false" CommandName="VIEW"
                                                                    CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CssClass="btn btn-xs btn-warning"><i class="fe-eye"></i></asp:LinkButton>
                                                                &nbsp;
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="" Visible="false">
                                            <ItemTemplate>
                                                 <asp:Label ID="LBL_empid" runat="server" Text='<%# Eval("PERNR") %>'></asp:Label>
                                                 </ItemTemplate>
                                         </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>

                                                 <div class="row col-md-12" id="divpageNcnt2" runat="server">
                                                <div class="col-md-3" style="margin-top: 5px" id="divcntshwingcomp" runat="server"></div>
                                                <div class="col-md-9 DivSpacer01 Div02 repeater text-xl-right">
                                                    <asp:Repeater ID="RptrCompleatedPager" runat="server">
                                                        <ItemTemplate>
                                                            <ul class="pagination pagination-rounded" style="display: inline-block">
                                                                <li class='paginate_button page-item <%# Convert.ToBoolean(Eval("Enabled")) ? "" : "active" %>'>
                                                                    <asp:LinkButton ID="lnkPage" runat="server" CausesValidation="false" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>' Enabled='<%# Eval("Enabled") %>' OnClick="LbtnCompleatedPage_Changed" CssClass='page-link <%# Convert.ToBoolean(Eval("Enabled")) ? "page_enabled" : "page_disabled" %>'></asp:LinkButton>
                                                                </li>
                                                            </ul>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </div>
                                            </div>


                                        </div>
                                    </div>
                                    </div>
                                <div>
                                <asp:Panel ID="pnlRecordWorking" runat="server">
                                    <asp:GridView ID="grdRecordTime" runat="server" ShowFooter="True" AutoGenerateColumns="False">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Project" HeaderStyle-CssClass="hd-small">
                                                <ItemTemplate>
                                                    <asp:Label ID="drpdwnCostCenter" runat="server"> 
                                                    </asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle CssClass="hd-small"></HeaderStyle>
                                                <ItemStyle Width="50px"></ItemStyle>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="WBS" HeaderStyle-CssClass="hd-small">
                                                <ItemTemplate>
                                                    <asp:Label ID="drpdwnOrder" runat="server"> 
                                                    </asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle CssClass="hd-small"></HeaderStyle>
                                                <ItemStyle Width="30px"></ItemStyle>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Activity" HeaderStyle-CssClass="hd-small">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtStaffGrade" runat="server"></asp:Label>
                                                </ItemTemplate>
                                                 <HeaderStyle CssClass="hd-small"></HeaderStyle>
                                                <ItemStyle Width="30px"></ItemStyle>
                                                <FooterStyle HorizontalAlign="Center" />
                                                <FooterTemplate>
                                                    <div>
                                                        <asp:Label ID="lblStaffGrade" runat="server" Text="Hours" CssClass="label" Width="40px"></asp:Label>
                                                    </div>
                                                </FooterTemplate>
                                                <HeaderStyle CssClass="hd-small"></HeaderStyle>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Attendance Type" ItemStyle-Width="30" HeaderStyle-CssClass="hd-small">
                                                <ItemTemplate>
                                                    <asp:Label ID="drpdwnAttabsType" runat="server"> 
                                                    </asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle CssClass="hd-small"></HeaderStyle>
                                                <ItemStyle Width="30px"></ItemStyle>
                                            </asp:TemplateField>
                                            
                                            <asp:TemplateField HeaderText="Hours" HeaderStyle-CssClass="hd-small" HeaderStyle-Width="30">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtTotal" runat="server" Width="30px" ReadOnly="True"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                                <FooterStyle HorizontalAlign="Center" />
                                                <FooterTemplate>
                                                    <div>
                                                        <asp:Label ID="lblHours" runat="server" CssClass="label" Width="40px" Style="text-align: center;"></asp:Label>
                                                    </div>
                                                </FooterTemplate>
                                                <HeaderStyle CssClass="hd-small"></HeaderStyle>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="SUN" HeaderStyle-CssClass="hd-small">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtSUN" runat="server" Width="30px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                                <FooterStyle HorizontalAlign="Center" />
                                                <FooterTemplate>
                                                    <div>
                                                        <asp:Label ID="lblSun" runat="server" CssClass="label" Width="40px" Style="text-align: center;"></asp:Label>
                                                    </div>
                                                </FooterTemplate>
                                                <HeaderStyle CssClass="hd-small"></HeaderStyle>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="MON" HeaderStyle-CssClass="hd-small">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtMON" runat="server" Width="30px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                                <FooterStyle HorizontalAlign="Center" />
                                                <FooterTemplate>
                                                    <div>
                                                        <asp:Label ID="lblMon" runat="server" CssClass="label" Width="40px" Style="text-align: center;"></asp:Label>
                                                    </div>
                                                </FooterTemplate>
                                                <HeaderStyle CssClass="hd-small"></HeaderStyle>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="TUE" HeaderStyle-CssClass="hd-small">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtTUE" runat="server" Width="30px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                                <FooterStyle HorizontalAlign="Center" />
                                                <FooterTemplate>
                                                    <div>
                                                        <asp:Label ID="lblTues" runat="server" CssClass="label" Width="40px" Style="text-align: center;"></asp:Label>
                                                    </div>
                                                </FooterTemplate>
                                                <HeaderStyle CssClass="hd-small"></HeaderStyle>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="WED" HeaderStyle-CssClass="hd-small">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtWED" runat="server" Width="30px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                                <FooterStyle HorizontalAlign="Center" />
                                                <FooterTemplate>
                                                    <div>
                                                        <asp:Label ID="lblWed" runat="server" CssClass="label" Width="40px" Style="text-align: center;"></asp:Label>
                                                    </div>
                                                </FooterTemplate>
                                                <HeaderStyle CssClass="hd-small"></HeaderStyle>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="THU" HeaderStyle-CssClass="hd-small">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtTHU" runat="server" Width="30px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                                <FooterStyle HorizontalAlign="Center" />
                                                <FooterTemplate>
                                                    <div>
                                                        <asp:Label ID="lblThu" runat="server" CssClass="label" Width="40px" Style="text-align: center;"></asp:Label>
                                                    </div>
                                                </FooterTemplate>
                                                <HeaderStyle CssClass="hd-small"></HeaderStyle>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="FRI" HeaderStyle-CssClass="hd-small">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtFRI" runat="server" Width="30px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                                <FooterStyle HorizontalAlign="Center" />
                                                <FooterTemplate>
                                                    <div>
                                                        <asp:Label ID="lblFri" runat="server" CssClass="label" Width="40px" Style="text-align: center;"></asp:Label>
                                                    </div>
                                                </FooterTemplate>
                                                <HeaderStyle CssClass="hd-small"></HeaderStyle>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="SAT" HeaderStyle-CssClass="hd-small">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtSAT" runat="server" Width="30px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                                <FooterStyle HorizontalAlign="Center" />
                                                <FooterTemplate>
                                                    <div>
                                                        <asp:Label ID="lblSAt" runat="server" CssClass="label" Width="40px"></asp:Label>
                                                    </div>
                                                </FooterTemplate>
                                                <HeaderStyle CssClass="hd-small"></HeaderStyle>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="REMARKS" HeaderStyle-CssClass="hd-small">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtREMARKS" runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                                <FooterStyle HorizontalAlign="Center" />
                                                <FooterTemplate>
                                                    <div>
                                                        <asp:Label ID="lblREMARKS" runat="server" CssClass="label" Visible="false"></asp:Label>
                                                    </div>
                                                </FooterTemplate>
                                                <HeaderStyle CssClass="hd-small"></HeaderStyle>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    <br />


                                    <div class="form-group" id="RWTdiv" runat="server">

                                        <div class="row">
                                            <div class="col-sm-1">Remarks&nbsp;<b>:</b></div>
                                            <div class="col-sm-6">
                                                <asp:TextBox ID="txtRWComments" runat="server" CssClass="txtDropDownwidth" TextMode="MultiLine"></asp:TextBox>

                                                <asp:Label ID="lblValidateRWCommnets" runat="server" Text="" CssClass="lblValidation"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <table id="TblRemarks" runat="server">
                                        <tr>
                                            <td>&nbsp;Remarks&nbsp;:&nbsp;</td>
                                            <td>
                                                <asp:Label ID="lblRemarksRWT" runat="server" Text="" CssClass="lblValidation"></asp:Label></td>
                                        </tr>
                                    </table>

                                     <div class="row">
                                        <div class="col-sm-1" style="width: 90px;">
                                            <asp:Button ID="btnRWApprove" runat="server" Text="Approve" Width="80px" OnClick="btnRWApprove_Click" />
                                        </div>
                                        <div class="col-sm-1">
                                            <asp:Button ID="btnRWReject" runat="server" Text="Reject" Width="80px" OnClick="btnRWReject_Click" />
                                        </div>
                                    </div>
                                </asp:Panel>

                            </div>


                            <asp:HiddenField runat="server" ID="HF_TBLTYPE" />
                            <asp:HiddenField runat="server" ID="HF_ID" />
                            <asp:HiddenField runat="server" ID="HF_PKEY" />
                            <asp:HiddenField runat="server" ID="HF_STS" />
                            <div class="respovrflw">


                                <asp:GridView ID="GridViewDetails" runat="server" DataKeyNames="PKEY,ID,CHANGE_APPROVAL,STATUS,MODON,MODIFIEDON,CREATED_BY" Visible="false" AutoGenerateColumns="false"
                                 OnRowCommand="GridViewDetails_RowCommand" OnRowDataBound="GridViewDetails_RowDataBound">
                                    <Columns>
                                        <asp:BoundField DataField="" HeaderText="Employee ID" ReadOnly="True" />
                                        <asp:BoundField DataField="ENAME" HeaderText="Employee Name" />

                                        <asp:BoundField DataField="PARTICULARS" HeaderText="Particulars" />
                                        <asp:BoundField DataField="MODIFIEDON" HeaderText="Modified On" ReadOnly="True" />
                                        <asp:BoundField DataField="STATUS" HeaderText="Status" ReadOnly="True" />
                                        <asp:TemplateField HeaderText="View">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="GridViewDetailsView" runat="server" CausesValidation="false" CommandName="VIEW"
                                                    CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CssClass="btn btn-xs btn-warning"><i class="fe-eye"></i></asp:LinkButton>                                               
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="" Visible="false">
                                            <ItemTemplate>
                                                 <asp:Label ID="LBL_empid" runat="server" Text='<%# Eval("CREATED_BY") %>'></asp:Label>
                                                 </ItemTemplate>
                                         </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>

                            </div>

                            <div class="respovrflw">
                                <br />
                                <div style="margin: 20px 0 0 0;">
                                    <asp:GridView ID="GV_DashboardDetails" runat="server" AutoGenerateColumns="false" ShowHeader="false" AllowPaging="false" ShowFooter="true" AllowSorting="false"
                                        OnRowCommand="GV_DashboardDetails_RowCommand" OnRowCancelingEdit="GV_DashboardDetails_RowCancelingEdit"
                                        OnRowDataBound="GV_DashboardDetails_RowDataBound" OnRowDeleting="GV_DashboardDetails_RowDeleting"
                                        OnRowEditing="GV_DashboardDetails_RowEditing" OnRowUpdating="GV_DashboardDetails_RowUpdating">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Particulars">
                                                <ItemTemplate>
                                                    <%# Eval("TEXT") %>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    Approver Remarks
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <b>:</b>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <b>:</b>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Details">
                                                <ItemTemplate>

                                                    <div class=""><%# Eval("VALUES") %></div>

                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <div class="form-group">
                                                        <div>
                                                            <asp:TextBox ID="TxtGvRemarks" runat="server" Width="180px" CssClass="" TextMode="MultiLine" Columns="6" MaxLength="150" autocomplete="off" placeholder="Enter Remarks"
                                                                Style="resize: none;"></asp:TextBox>

                                                            <asp:RequiredFieldValidator ID="RFV_TxtGvRemarks" runat="server" ControlToValidate="TxtGvRemarks" ValidationGroup="VgReq"
                                                                ErrorMessage="Enter Remarks" SetFocusOnError="true" ForeColor="Red" CssClass="lblValidation"></asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="row">
                                                          <div class="col-sm-6">
                                                                <asp:Button ID="BtnGvReqApprove" runat="server" Width="70px" Text="Approve" CommandName="APPROVE" ValidationGroup="VgReq" />
                                                            &nbsp;
                                                                <asp:Button ID="BtnGvReqReject" runat="server" Width="70px" Text="Reject" CommandName="REJECT" ValidationGroup="VgReq" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </FooterTemplate>
                                                <FooterStyle VerticalAlign="Top" ForeColor="#333333" Font-Bold="false" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                                <div style="margin: 20px 0 0 0;">
                                    <asp:GridView ID="GV_DashboardCompleatedDetails" runat="server" AutoGenerateColumns="false" ShowHeader="false" AllowPaging="false"
                                        ShowFooter="false" AllowSorting="false" 
                                        OnRowCommand="GV_DashboardCompleatedDetails_RowCommand" OnRowCancelingEdit="GV_DashboardCompleatedDetails_RowCancelingEdit"
                                        OnRowDataBound="GV_DashboardCompleatedDetails_RowDataBound" OnRowDeleting="GV_DashboardCompleatedDetails_RowDeleting"
                                        OnRowEditing="GV_DashboardCompleatedDetails_RowEditing" OnRowUpdating="GV_DashboardCompleatedDetails_RowUpdating">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Particulars" ItemStyle-Width="150">
                                                <ItemTemplate>
                                                    <%# Eval("TEXT") %>
                                                </ItemTemplate>
                                                <ItemStyle Font-Bold="true" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="" ItemStyle-Width="60">
                                                <ItemTemplate>
                                                    <b>:</b>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Details">
                                                <ItemTemplate>
                                                    <%# Eval("VALUES") %>
                                                </ItemTemplate>
                                                <ItemStyle />
                                            </asp:TemplateField>
                                        </Columns>
                                     </asp:GridView>
                                    </div>
                                </div>


                            </div>
                        </div>
                    </div>
                  
                    <asp:HiddenField ID="HF_TABID" runat="server" />
            </ContentTemplate>          
        </asp:UpdatePanel>
    </div>
</asp:Content>



