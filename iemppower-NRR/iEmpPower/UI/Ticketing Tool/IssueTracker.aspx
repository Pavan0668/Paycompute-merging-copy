<%@ Page Title="Issue Tracker" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="IssueTracker.aspx.cs"
    Inherits="iEmpPower.UI.Ticketing_Tool.IssueTracker" EnableEventValidation="false" Culture="en-GB" UICulture="auto" Theme="SkinFile" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        .modal-content
        {
            /*display:none;*/
            z-index: -1;
            background-color: rgba(0,94,124,0.2);
            /*margin: 15% auto;*/ /* 15% from the top and centered */
            padding: 20px;
            border: 2px solid #888;
            width: 50%; /*Could be more or less, depending on screen size*/
        }

        .hidden
        {
            visibility: hidden;
        }

         .modalBackground {
           background-color: black;
           filter: alpha(opacity=90);
           opacity: 0.8;
       }

       .modalPopup {
           background-color: #FFFFFF;
           border-width: 3px;
           border-style: solid;
           border-color: black;
           width: 50% !important;
           height: 500px;
       }

        .Td07
        {
            color: #004080;
            font-size: 13px;
            width: 10px;
            padding: 8px;
            text-align: center;
            line-height: 20px !important;
        }

        .txtGrdSerch
        {
            position: relative;
            width: 50px !important;
            border-width: 2px;
            border-color: lightblue;
            border-bottom-style: solid;
            box-sizing: border-box;
            background-color: #00617C;
            bottom: 1px !important;
            margin-bottom: 1px !important;
            padding-bottom: 1px !important;
        }

        .RedCls
        {
            width: 23px;
            height: 16px;
            background-color: #f75f0b;
            border: 1px solid #ccc;
            padding: 0;
            margin: 0 4px;
        }

        .darkRed
        {
            width: 23px;
            height: 16px;
            background-color: #ea0d14;
            border: 1px solid #ccc;
            padding: 0;
            margin: 0 4px;
        }

        .GreenCls
        {
            width: 23px;
            height: 16px;
            background-color: #008000;
            border: 1px solid #ccc;
            padding: 0;
            margin: 0 4px;
        }

        .AmberCls
        {
            width: 23px;
            height: 16px;
            background-color: #FFBF00;
            border: 1px solid #ccc;
            padding: 0;
            margin: 0 4px;
        }

        .Brd01
        {
            border: 1px solid #666;
        }

        .Tdlegend1
        {
            color: #004080;
            font-size: 13px;
            line-height: 12px !important;
            padding: 3px;
        }

        .Tdlegend2
        {
            color: #004080;
            font-size: 13px;
            text-align: center;
            line-height: 12px !important;
            width: 10px;
            padding: 8px;
        }

        .Ul01, .Ul02
        {
            list-style: none;
            margin: 0;
            padding: 0;
        }

            .Ul01 li
            {
                padding: 5px 0 5px 10px;
                margin: 3px;
            }

            .Ul02 li
            {
                padding: 3px 10px 3px 10px;
                margin: 3px;
            }

        .TxtC
        {
            text-align: center !important;
        }

        .FntPIPO
        {
            border-bottom: 1px solid #ccc !important;
        }

        .lgdpos
        {
            float: right;
            right: 145px;
            position: absolute;
            top: 40px;
        }

        /*#MainContent_pnlgrid
        {
            overflow-x: scroll !important;
        }*/

        #MainContent_Pnltask
        {
            overflow-x: scroll !important;
        }

        .rowheight
        {
            height: 90px !important;
        }

        .rowwidth
        {
            width: 100px !important;
        }

        .rowwidthtitle
        {
            width: 180px !important;
        }

        .gridview td
        {
            padding: 5px 10px 2px 0px !important;
        }

        .gridview th
        {
            padding: 5px 10px 2px 0px !important;
        }


        .txtdec
        {
            text-decoration: underline !important;
        }

        #MainContent_UPCustRating {
           border-color: black;
           border-style: solid;
           background-color: white !important;
           width: 50% !important;
           /*right:25px !important;*/
       }

       .PnlPaddingRating {
           padding-left: 25%;
       }
    </style>
    <%--  <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript" src="../../js/quicksearch.js"></script>
    <script type="text/javascript">
        $(function () {
            $('.search_textbox').each(function (i) {
                $(this).quicksearch("[id*=GV_Tickets] tr:not(:has(th))", {
                    'testQuery': function (query, txt, row) {
                        return $(row).children(":eq(" + i + ")").text().toLowerCase().indexOf(query[0].toLowerCase()) != -1;
                    }
                });
            });
        });
    </script>--%>

    <script type="text/javascript">
        function searchTable(obj, index) {

            var filter = obj.value.toUpperCase();
            var grid = document.getElementById('<%= GV_Tickets.ClientID%>');
            for (var i = 1; i <= grid.rows.length; i++)
                grid.rows[i].style.display = grid.rows[i].cells[index].innerText.toUpperCase().indexOf(filter) > -1 ? '' : 'none';

        }

        function DisplayName() {
            $find("mpe").show();
            return false;
        }
        function HideModalPopup() {
            $find("mpe").hide();
            return false;
        }

        function HideModalPopupc() {
            $find("mpe").hide();
            alert("Ticket Updated Successfully !!..")
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="header">
        <div class="row clearfix">
            <div class="col-xs-12 col-sm-6">
                <span class="HeadFontSize">Issue Tracker</span>

            </div>
        </div>
    </div>
    <div class="body">



        <%--<div class="DivSpacer01"></div>--%>
        <%--<asp:Button ID="BtnNewTicket" runat="server" Text="New Ticket" OnClick="BtnNewTicket_Click" />--%>

        <%--   <table>
            <tr>
                <x%-- <td>
                    <asp:DropDownList runat="server" ID="DDLStatusSearch" TabIndex="1" CssClass="txtDropDownwidth" OnSelectedIndexChanged="DDLSearch_SelectedIndexChanged" AutoPostBack="true">
                    </asp:DropDownList></td>--%x>
                <td>&nbsp;&nbsp;</td>
                <td>
                    <asp:Button ID="BtnNewTicket" runat="server" TabIndex="2" Text="New Ticket" OnClick="BtnNewTicket_Click" /></td>
                <x%-- <td>&nbsp;&nbsp; <asp:Button ID="BTnBackdashboard" runat="server" TabIndex="2" Text="Back" OnClick="BTnBackdashboard_Click" /></td>--%x>
            </tr>
        </table>--%>
        <%--<div class="DivSpacer01"></div>--%>

        <div class="divfr" id="divbrdr">
            <asp:Label ID="LblMsg" runat="server" CssClass="lblValidation"></asp:Label>
            <div class="col-sm-12">
                <div class="Fr"></div>
                <div class="col-sm-8 Brd01">
                    <%--   <fieldset style="width:99%; border: 1px solid grey; padding-bottom: 10px; padding-left: 5px; padding-right: 5px;">
            <legend style="width: 50px; border: 0px; font-size: 15px;">Search</legend>--%>

                    <div class="form-inline">
                        <div class="col-sm-12 ">
                            <h4 class="FntPIPO" style="padding: 2px;"><b>Search</b></h4>
                        </div>
                        <div class="form-group">
                            <div class="col-sm-2 htCr" style="width: 100px">Customer <b>:</b></div>
                            <%--<td class="Td07"><b>:</b> </td>--%>
                            <div class="col-sm-4" style="width: 165px">
                                <asp:DropDownList ID="DDLCustomerList" runat="server" TabIndex="1" CssClass="txtDropDownwidth" OnSelectedIndexChanged="DDLCustomerList_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                            </div>

                            <%-- <td>&nbsp;&nbsp;&nbsp;</td>
                                <td>&nbsp;&nbsp;&nbsp;</td>--%>
                            <div class="col-sm-2 htCr" style="width: 100px">Status <b>:</b></div>
                            <%--<td class="Td07"><b>:</b> </td>--%>
                            <div class="col-sm-3" style="width: 165px">
                                <asp:DropDownList runat="server" ID="DDLStatusSearch" TabIndex="2" CssClass="txtDropDownwidth" OnSelectedIndexChanged="DDLSearch_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>


                        </div>
                        <div class="form-group">
                            <div class="col-sm-2 htCr" style="width: 100px">Begin date <b>:</b></div>
                            <%-- <td class="Td07"><b>:</b> </td>--%>
                            <div class="col-sm-4" style="width: 165px">
                                <asp:TextBox ID="TxtFromDate" runat="server" PlaceHolder="Begin date" TabIndex="3" CssClass="txtDropDownwidth"
                                    OnTextChanged="TxtFromDate_TextChanged" AutoPostBack="true"></asp:TextBox>
                                <Ajx:MaskedEditExtender ID="MEE_TxtFromDate" runat="server" AcceptNegative="Left"
                                    CultureName="en-GB" DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999"
                                    MaskType="Date" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                                    OnInvalidCssClass="MaskedEditError" TargetControlID="TxtFromDate" />
                                <Ajx:CalendarExtender ID="CE_TxtFromDate" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                    TargetControlID="TxtFromDate">
                                </Ajx:CalendarExtender>
                                <asp:RegularExpressionValidator ID="REV_TxtFromDate" runat="server" Display="Dynamic"
                                    CssClass="lblValidation" ErrorMessage="Invalid Date" ControlToValidate="TxtFromDate"
                                    SetFocusOnError="True" ValidationExpression="^((((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00)))))$" ForeColor="Red"></asp:RegularExpressionValidator>
                            </div>
                            <%-- <td>&nbsp;&nbsp;&nbsp;</td>
                                <td>&nbsp;&nbsp;&nbsp;</td>--%>
                            <%-- </tr>
            <tr>--%>
                            <div class="col-sm-2 htCr" style="width: 100px">End date <b>:</b></div>
                            <%-- <td class="Td07"><b>:</b> </td>--%>
                            <div class="col-sm-3" style="width: 165px">
                                <asp:TextBox ID="TxtToDate" runat="server" TabIndex="4" PlaceHolder="End date" CssClass="txtDropDownwidth"
                                    OnTextChanged="TxtToDate_TextChanged" AutoPostBack="true"></asp:TextBox>
                                <Ajx:MaskedEditExtender ID="MaskedEditExtender1" runat="server" AcceptNegative="Left"
                                    CultureName="en-GB" DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999"
                                    MaskType="Date" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                                    OnInvalidCssClass="MaskedEditError" TargetControlID="TxtToDate" />
                                <Ajx:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                    TargetControlID="TxtToDate">
                                </Ajx:CalendarExtender>

                                <asp:CompareValidator ID="CV_TxtToDate" runat="server" ControlToCompare="TxtFromDate"
                                    CssClass="lblValidation" ControlToValidate="TxtToDate" Display="Dynamic" ErrorMessage="From date should be less than to date"
                                    Operator="GreaterThanEqual" Type="Date" ForeColor="Red"></asp:CompareValidator>
                                <asp:RegularExpressionValidator ID="REV_TxtToDate" runat="server" Display="Dynamic"
                                    CssClass="lblValidation" ErrorMessage="Invalid Date" ControlToValidate="TxtToDate"
                                    SetFocusOnError="True" ValidationExpression="^((((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00)))))$" ForeColor="Red"></asp:RegularExpressionValidator>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-sm-2 htCr" style="width: 100px">Ticket ID <b>:</b></div>
                            <%-- <td class="Td07"><b>:</b> </td>--%>
                            <div class="col-sm-4" style="width: 165px">
                                <asp:TextBox ID="TxtTID" runat="server" TabIndex="5" PlaceHolder="Ticket ID" CssClass="txtDropDownwidth"
                                    OnTextChanged="TxtTID_TextChanged" AutoPostBack="true"></asp:TextBox>
                                <Ajx:FilteredTextBoxExtender ID="FTBE_TxtTID" runat="server" TargetControlID="TxtTID"
                                    FilterType="Numbers" ValidChars="">
                                </Ajx:FilteredTextBoxExtender>
                            </div>
                            <%--<tr>--%>
                            <div class="col-sm-2">
                                <asp:Button runat="server" ID="btnclear" CausesValidation="false" OnClick="btnclear_Click" Text="Clear" TabIndex="5" />
                            </div>
                            <%-- </tr>--%>
                        </div>

                    </div>
                    <%-- </fieldset>--%>
                </div>
                <div class="col-sm-4">
                    <asp:LinkButton ID="BTnBackdashboard" runat="server" TabIndex="9" CssClass="Fr  linkbtn" CausesValidation="false" Text="Back" OnClick="BTnBackdashboard_Click"></asp:LinkButton>
                    <div style="padding-left: 3px;">

                        <asp:Button ID="BtnNewTicket" runat="server" TabIndex="6" Text="New Ticket" OnClick="BtnNewTicket_Click" />
                        <div style="margin-bottom:15px"></div>
                    </div>

                    <%--<div runat="server" style="width: 30%; float: left; padding-top: 15px;">--%>
                    <div>
                        <div id="divcont" runat="server" style="width: 100%">
                            <ul class="Ul02">

                                <li class="Fl Brd01" style="padding-top: 0 !important; margin-top: 0 !important">
                                    <ul class="Ul01">
                                        <li class="TxtC FntPIPO">

                                            <b id="bsla1" runat="server">SLA Time Progress Legend</b>

                                            <%--<Ajx:ModalPopupExtender ID="mp1" runat="server" PopupControlID="pnlSLADetls" TargetControlID="lnkbtn"
                                                                        BackgroundCssClass="modalBackground" CancelControlID="btnADvanceClose">
                                                                    </Ajx:ModalPopupExtender>--%>
                                            <Ajx:HoverMenuExtender ID="HoverMenuExtender1" runat="server" TargetControlID="bsla1"
                                                PopupControlID="pnlSLADetls" PopupPosition="Left" HoverCssClass="modalBackground">
                                            </Ajx:HoverMenuExtender>
                                            <%-- <Ajx:HoverMenuExtender ID="HoverMenuExtender2" runat="server" TargetControlID="sla2"
                                                PopupControlID="pnlSLADetls" PopupPosition="Left" HoverCssClass="modalBackground">
                                            </Ajx:HoverMenuExtender>
                                            <Ajx:HoverMenuExtender ID="HoverMenuExtender3" runat="server" TargetControlID="sla3"
                                                PopupControlID="pnlSLADetls" PopupPosition="Left" HoverCssClass="modalBackground">
                                            </Ajx:HoverMenuExtender>
                                            <Ajx:HoverMenuExtender ID="HoverMenuExtender4" runat="server" TargetControlID="sla4"
                                                PopupControlID="pnlSLADetls" PopupPosition="Left" HoverCssClass="modalBackground">
                                            </Ajx:HoverMenuExtender>--%>
                                            <%--<asp:Button ID="Button1" runat="server" Style="Display: none;" Text="Button" />--%>
                                        </li>
                                        <li>
                                            <table>
                                                <tr>
                                                    <td class="Tdlegend1">
                                                        <div class="GreenCls"></div>
                                                    </td>
                                                    <td class="Tdlegend2">- </td>
                                                    <td><b><%--<a style="color: #6d5555 !important" id="sla1" runat="server">SLA Time</a> 0-40%--%>
                                                        <asp:LinkButton ID="lnkBtnSLA1" OnClick="lnkBtnSLA1_Click" CssClass="linkbtn" runat="server">SLA Time 0-40%</asp:LinkButton>
                                                    </b></td>
                                                </tr>
                                                <tr>
                                                    <td class="Tdlegend1">
                                                        <div class="AmberCls"></div>
                                                    </td>
                                                    <td class="Tdlegend2">- </td>
                                                    <td><b><%--<a style="color: #6d5555 !important" id="sla2" runat="server">SLA Time</a> 40-80%--%>
                                                        <asp:LinkButton ID="lnkBtnSLA2" OnClick="lnkBtnSLA2_Click" CssClass="linkbtn" runat="server">SLA Time 40-80%</asp:LinkButton>
                                                    </b></td>
                                                </tr>
                                                <tr>
                                                    <td class="Tdlegend1">
                                                        <div class="RedCls"></div>
                                                    </td>
                                                    <td class="Tdlegend2">- </td>
                                                    <td><b><%--<a style="color: #6d5555 !important" id="sla3" runat="server">SLA Time</a> Above 80%--%>
                                                        <asp:LinkButton ID="lnkBtnSLA3" OnClick="lnkBtnSLA3_Click" CssClass="linkbtn" runat="server">SLA Time Above 80%</asp:LinkButton>
                                                    </b></td>
                                                </tr>
                                                <tr>
                                                    <td class="Tdlegend1">
                                                        <div class="darkRed"></div>
                                                    </td>
                                                    <td class="Tdlegend2">- </td>
                                                    <td><b><%--<a style="color: #6d5555 !important" id="sla4" runat="server">SLA Time</a> Breached--%>
                                                        <asp:LinkButton ID="lnkBtnSLA4" OnClick="lnkBtnSLA4_Click" CssClass="linkbtn" runat="server">SLA Time Breached</asp:LinkButton>
                                                    </b></td>
                                                </tr>
                                            </table>

                                        </li>
                                    </ul>
                                </li>
                            </ul>
                        </div>

                    </div>
                    <%--</div>--%>
                </div>
            </div>

            <div class="DivSpacer03">
                <hr />
            </div>
            <%--  <fieldset style="width: 100%; border: 1px solid grey">
                <legend style="width: 70px; border: 0px;">Tickets</legend>--%>


            <div id="ticketdiv" runat="server" style="width: 100%; margin: 0 auto; padding: 2px;">
                <h4><b>Tickets</b></h4>
                <asp:Label ID="LblTicket" runat="server" CssClass="lblValidation"></asp:Label>

                <asp:Panel ID="pnlgrid" runat="server" Width="99%">
                    <div class="respovrflw" style="max-height:350px;">
                        <asp:GridView ID="GV_Tickets" runat="server" AutoGenerateColumns="False" Width="100%" CssClass="Grid" GridLines="Both" PagerStyle-CssClass="cssPager"
                            DataKeyNames="TID,PERCENTAGE,STATUS,CATEGORY,SecSLA" OnRowCommand="GV_Tickets_RowCommand" OnRowDataBound="GV_Tickets_RowDataBound"
                            OnSorting="GV_Tickets_Sorting" AllowSorting="true" CellPadding="100"
                            CellSpacing="5"  OnSelectedIndexChanged="GV_Tickets_SelectedIndexChanged">
                            <Columns>

                                <%-- AllowPaging="true" OnPageIndexChanging="GV_Tickets_PageIndexChanging"  PageSize="5" OnDataBound="OnDataBound" <asp:BoundField DataField="TID" HeaderText="Ticket ID" />
                        <asp:BoundField DataField="TITLE" HeaderText="Title" />--%>

                                <asp:TemplateField HeaderText="Slno" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="LblSlno" runat="server" Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:BoundField DataField="TID" HeaderText="Ticket ID" SortExpression="TID" />
                                <asp:BoundField DataField="TITLE" HeaderText="Title" SortExpression="TITLE" />
                                <asp:TemplateField HeaderText="Client" SortExpression="CLIENT">
                                    <ItemTemplate>
                                        <%#Eval("CLIENT") + "-"+Eval("CLIENTNNAME")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="FRMUSR" HeaderText="From User" SortExpression="FRMUSR" />
                                <asp:TemplateField HeaderText="Assignee" SortExpression="ASSIGNEE">
                                    <ItemTemplate>
                                        <%#Eval("ASSIGNEE") + "-"+ Eval("TOASSIGNEENNAME")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="PriorityTxt" HeaderText="Priority" SortExpression="PriorityTxt" />
                                <asp:BoundField DataField="CategoryTxt" HeaderText="Category" SortExpression="CategoryTxt" />
                                    <asp:BoundField DataField="IssueTypeTxt" HeaderText="Issue Type" SortExpression="IssueType" />
                                <asp:TemplateField HeaderText="SLA">
                                    <ItemTemplate>
                                        <%#((Eval("CATEGORY").ToString().Trim() =="2" || Eval("CATEGORY").ToString().Trim() =="3") ? "Yes" :"No") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Agent" SortExpression="AGENT">
                                    <ItemTemplate>
                                        <%#Eval("AGENT") + "-"+ Eval("AGENTNAME")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Ticket Ref. To" SortExpression="TIDREF">
                                    <ItemTemplate>
                                        <%#((string.IsNullOrEmpty(Eval("TIDREF").ToString()) || Eval("TIDREF").ToString().Trim()=="0") ? "-"  : Eval("TIDREF"))%>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:BoundField DataField="StatusTxt" HeaderText="Status" SortExpression="StatusTxt" />

                                <asp:BoundField DataField="CREATED_ON" DataFormatString="{0:dd-MMM-yyyy HH:mm:ss}" HeaderText="Created On" SortExpression="CREATED_ON" />

                                <asp:TemplateField HeaderText="Last Modified By" SortExpression="LASTMODIFIED_BY">
                                    <ItemTemplate>
                                        <%#(Eval("LASTMODIFIED_BY")=="" ?Eval("CLIENT") +"-"+ Eval("CLIENTNNAME")  : Eval("LASTMODIFIED_BY") + "-" + Eval("MODIFIEDONNAME"))%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Last Modified On" SortExpression="LASTMODIFIED_ON">
                                    <ItemTemplate>
                                        <%#(Eval("LASTMODIFIED_ON","{0:dd-MM-yyyy}").ToString()=="01-01-1900") ? Eval("CREATED_ON" ,"{0:dd-MMM-yyyy HH:mm:ss} ") : Eval("LASTMODIFIED_ON","{0:dd-MMM-yyyy HH:mm:ss} ")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Breached?">
                                    <ItemTemplate>
                                        <%#Eval("SecSLA")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Breached Time (Days.hh:mm:ss)">
                                    <ItemTemplate>
                                        <%#Eval("SecSLA")%>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField>
                                   <ItemTemplate>
                                       <asp:LinkButton ID="LbtnFeedbackView" runat="server" CausesValidation="false" CommandName="FBVIEW" TabIndex="23"
                                           Visible='<%# bool.Parse(string.Format("{0}", Eval("STATUS").ToString().Trim()=="8" || Eval("STATUS").ToString().Trim()=="9"  ? "true" : "false"))%>' CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CssClass="Fnt02" Text="View Feedback"></asp:LinkButton>
                                       &nbsp;
                                   </ItemTemplate>
                               </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LbtnTicketView" runat="server" CausesValidation="false" CommandName="VIEW"
                                            CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CssClass="Fnt02" Text="View"></asp:LinkButton>
                                        &nbsp;
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                            <RowStyle CssClass="rowheight" />


                        </asp:GridView>
                    </div>
                    <div class="DivSpacer01 Div02">
                        <div class="col-sm-9">
                            <asp:Repeater ID="RptrPendingPager" runat="server">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkPage" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>' Enabled='<%# Eval("Enabled") %>' OnClick="PendingPage_Changed" CssClass='<%# Convert.ToBoolean(Eval("Enabled")) ? "page_enabled linkbtn" : "page_disabled linkbtn" %>'></asp:LinkButton>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                        <div class="col-sm-2 htCr" style="width: 95px">Page Size <b>:</b></div>
                        <div class="col-sm-1">
                            <asp:DropDownList ID="ddlPagesize" runat="server" CssClass="txtDropDownwidth" AutoPostBack="true" OnSelectedIndexChanged="ddlPagesize_SelectedIndexChanged">
                                <asp:ListItem Text="10" Value="10" ></asp:ListItem>
                                <asp:ListItem Text="15" Value="15"></asp:ListItem>
                                <asp:ListItem Text="20" Value="20"></asp:ListItem>
                                <asp:ListItem Text="25" Value="25" Selected="True"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>

                </asp:Panel>
            </div>
            <%--</fieldset>--%>
            <div class="DivSpacer03">
                <hr />
            </div>
            <%-- <div class="DivSpacer01"></div>
            <div class="DivSpacer01"></div>--%>

            <%-- <fieldset id="Taskfs" runat="server" style="width: 100%; border: 1px solid grey">
                <legend style="width: 70px; border: 0px;">Task</legend>--%>
            <div id="Taskfs" runat="server">
                <div id="taskdiv" runat="server" style="width: 99%; margin: 0 auto; padding: 2px;">
                    <h4><b>Tasks</b></h4>
                    <asp:Label ID="LblTask" runat="server" CssClass="lblValidation"></asp:Label>
                    <asp:Panel ID="Pnltask" runat="server" Width="100%">
                        <div class="respovrflw" style="max-height:350px;">
                            <asp:GridView ID="GV_Task" runat="server" AutoGenerateColumns="False" Width="100%" CssClass="Grid" GridLines="Both" AllowSorting="true" PagerStyle-CssClass="cssPager"
                                DataKeyNames="TICKETID,TASKID,TASKLINEID" OnRowCommand="GV_Task_RowCommand" OnSorting="GV_Task_Sorting" OnSelectedIndexChanged="GV_Task_SelectedIndexChanged" OnRowDataBound="GV_Task_RowDataBound">
                                <Columns>
                                    <%--AllowPaging="true" PageSize="5" OnPageIndexChanging="GV_Task_PageIndexChanging"--%>
                                    <asp:TemplateField HeaderText="Slno" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="LblSlno" runat="server" Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:BoundField DataField="TICKETID" HeaderText="Ticket ID" SortExpression="TICKETID" />
                                    <asp:BoundField DataField="TASKLINEID" HeaderText="Task ID" SortExpression="TASKID" />
                                    <asp:BoundField DataField="TASKTITLE" HeaderText="Title" SortExpression="TASKTITLE" />
                                    <%--<asp:BoundField DataField="TASKAGENT" HeaderText="Agent" />--%>
                                    <asp:TemplateField HeaderText="Assigned To" SortExpression="TASKAGENT">
                                        <ItemTemplate>
                                            <%#Eval("TASKAGENT") + " - " + Eval("AGENTNAME") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Agent" SortExpression="TASKACTUALAGENT">
                                        <ItemTemplate>
                                            <%#Eval("TASKACTUALAGENT") + " - " + Eval("TaskActAgentname") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Task Assigned By" SortExpression="TASKCREATED_BY">
                                        <ItemTemplate>
                                            <%#  Eval("TASKCREATED_BY") + " - " + Eval("CREATEDONNAME") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="StatusTxt" HeaderText="Status" SortExpression="StatusTxt" />
                                    <asp:BoundField DataField="TASKCREATED_ON" DataFormatString="{0:dd-MMM-yyyy HH:mm:ss}" HeaderText="Created On" SortExpression="TASKCREATED_ON" />
                                    <%-- <asp:BoundField DataField="LASTMODIFIED_ON" DataFormatString="{0:dd-MMM-yyyy}" HeaderText="Last Modified On" />--%>

                                    <asp:TemplateField HeaderText="Last Modified By" SortExpression="TASKMODIFIED_BY">
                                        <ItemTemplate>
                                            <%#(Eval("TASKMODIFIED_BY")=="" ? Eval("TASKCREATED_BY") + " - " + Eval("CREATEDONNAME") : Eval("TASKMODIFIED_BY") + " - " + Eval("MODIFIEDONNAME"))%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Last Modified On" SortExpression="TASKMODIFIED_ON">
                                        <ItemTemplate>
                                            <%#(Eval("TASKMODIFIED_ON","{0:dd-MM-yyyy}").ToString()=="01-01-1900") ? Eval("TASKCREATED_ON" ,"{0:dd-MMM-yyyy HH:mm:ss} ") : Eval("TASKMODIFIED_ON","{0:dd-MMM-yyyy HH:mm:ss} ")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%-- <asp:BoundField DataField="LASTMODIFIED_BY" HeaderText="Last Modified By" />--%>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LbtnTicketView" runat="server" CausesValidation="false" CommandName="VIEW"
                                                CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CssClass="Fnt02" Text="View"></asp:LinkButton>
                                            &nbsp;
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <RowStyle CssClass="rowheight" />
                            </asp:GridView>
                        </div>
                        <div class="DivSpacer01 Div02">
                            <div class="col-sm-9">
                                <asp:Repeater ID="RepeaterTask" runat="server">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkPage" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>' Enabled='<%# Eval("Enabled") %>' OnClick="lnkPage_Click" CssClass='<%# Convert.ToBoolean(Eval("Enabled")) ? "page_enabled" : "page_disabled" %>'></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                            <div class="col-sm-2 htCr" style="width: 95px">Page Size <b>:</b></div>
                            <div class="col-sm-1">
                                <asp:DropDownList ID="ddlPaseSizeTAsk" TabIndex="8" runat="server" CssClass="txtDropDownwidth" AutoPostBack="true" OnSelectedIndexChanged="ddlPaseSizeTAsk_SelectedIndexChanged">
                                    <asp:ListItem Text="10" Value="10" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="5" Value="5"></asp:ListItem>
                                    <asp:ListItem Text="30" Value="30"></asp:ListItem>
                                    <asp:ListItem Text="40" Value="40"></asp:ListItem>
                                    <asp:ListItem Text="50" Value="50"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </asp:Panel>
                </div>

                <br />

            </div>
        </div>
    </div>
    <asp:LinkButton ID="lnkDummy" runat="server"></asp:LinkButton>
   <Ajx:ModalPopupExtender ID="mp1" runat="server" PopupControlID="UPCustRating" TargetControlID="lnkDummy" BehaviorID="mpe"
       BackgroundCssClass="modalBackground">
   </Ajx:ModalPopupExtender>

    <asp:UpdatePanel ID="UPCustRating" runat="server">
        <ContentTemplate>
            <asp:Panel runat="server" ID="PnlRating" Width="99%" Height="600px">
                <div runat="server" id="RatingDiv">
                    <div id="CustRatings" runat="server" class="col-sm-12">
                        <div>
                            <h3 style="color: black;">Customer feedback for Ticket ID <asp:Label id="RTID" runat="server"></asp:Label></h3>
                            <div style="width: 99%">
                                <b style="color: black;">Please take a few minutes to give us feedback about our service by filling in this short Customer Feedback Form. We are conducting this research in order
                                        to measure your level of satisfaction with the quality of our service. We thank you for your participation.
                                </b>
                            </div>
                            <div class="DivSpacer01"></div>
                            <div class="DivSpacer01"></div>
                            <div class="form-inline">

                                <div class="form-group">
                                    <div class="htCrrating Cb">
                                        <b>Processing time for this message - How satisfied are you?</b>
                                    </div>

                                </div>
                                <div class="DivSpacer01"></div>
                                <div class="DivSpacer01"></div>
                                <div class="form-group">
                                    <div class="col-sm-6 htCrrating">
                                        <p>With the initial reaction time from ITChamps:</p>
                                    </div>
                                    <div class="col-sm-3">
                                        <Ajx:Rating runat="server" ID="Rating1" Enabled="false" BehaviorID="RatingBehavior1" MaxRating="5" CurrentRating="3" CssClass="ratingStar" StarCssClass="ratingItem"
                                            WaitingStarCssClass="Saved" FilledStarCssClass="Filled" EmptyStarCssClass="Empty">
                                        </Ajx:Rating>
                                    </div>
                                </div>
                                <div class="DivSpacer01"></div>
                                <div class="DivSpacer01"></div>
                                <div class="form-group">
                                    <div class="col-sm-6 htCrrating">
                                        <p>With the total time used by ITChamps for solving this issue:</p>
                                    </div>
                                    <div class="col-sm-3">
                                        <Ajx:Rating runat="server" ID="Rating2" Enabled="false" BehaviorID="RatingBehavior2" MaxRating="5" CurrentRating="3" CssClass="ratingStar" StarCssClass="ratingItem"
                                            WaitingStarCssClass="Saved" FilledStarCssClass="Filled" EmptyStarCssClass="Empty">
                                        </Ajx:Rating>
                                    </div>
                                </div>
                            </div>
                            <div class="DivSpacer01"></div>
                            <div class="DivSpacer01"></div>
                            <div class="form-inline">
                                <div class="form-group">
                                    <div class="htCrrating Cb">
                                        <b>ITChamps Employee who solved your issue - How satisfied are you?</b>
                                    </div>

                                </div>
                                <div class="DivSpacer01"></div>
                                <div class="DivSpacer01"></div>
                                <div class="form-group">
                                    <div class="col-sm-6 htCrrating">
                                        <p>With his/her experience and knowledge for the job:</p>
                                    </div>
                                    <div class="col-sm-3">
                                        <Ajx:Rating runat="server" ID="Rating3" Enabled="false" BehaviorID="RatingBehavior3" MaxRating="5" CurrentRating="3" CssClass="ratingStar" StarCssClass="ratingItem"
                                            WaitingStarCssClass="Saved" FilledStarCssClass="Filled" EmptyStarCssClass="Empty">
                                        </Ajx:Rating>
                                    </div>
                                </div>
                                <div class="DivSpacer01"></div>
                                <div class="DivSpacer01"></div>
                                <div class="form-group">
                                    <div class="col-sm-6 htCrrating">
                                        <p>With his/her friendliness and behaviour:</p>
                                    </div>
                                    <div class="col-sm-3">
                                        <Ajx:Rating runat="server" ID="Rating4" Enabled="false" BehaviorID="RatingBehavior4" MaxRating="5" CurrentRating="3" CssClass="ratingStar" StarCssClass="ratingItem"
                                            WaitingStarCssClass="Saved" FilledStarCssClass="Filled" EmptyStarCssClass="Empty">
                                        </Ajx:Rating>
                                    </div>
                                </div>
                                <div class="DivSpacer01"></div>
                                <div class="DivSpacer01"></div>
                                <div class="form-group">
                                    <div class="col-sm-6 htCrrating">
                                        <p>How satisfied are you with Quality of Solution provided:</p>
                                    </div>
                                    <div class="col-sm-3">
                                        <Ajx:Rating runat="server" ID="Rating5" Enabled="false" BehaviorID="RatingBehavior5" MaxRating="5" CurrentRating="3" CssClass="ratingStar" StarCssClass="ratingItem"
                                            WaitingStarCssClass="Saved" FilledStarCssClass="Filled" EmptyStarCssClass="Empty">
                                        </Ajx:Rating>
                                    </div>
                                </div>


                            </div>
                            <div class="DivSpacer01"></div>
                            <div class="DivSpacer01"></div>
                            <div class="form-inline">
                                <div class="form-group">
                                    <div class="col-sm-6 htCrrating">
                                        <p>Suggestions / Comments</p>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <asp:Label runat="server" ID="RatingSug"></asp:Label>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="DivSpacer01"></div>
                    <div class="DivSpacer01"></div>
                    <div class="DivSpacer01" style="padding-left: 20px">
                        <asp:Button ID="btnClose" runat="server" Text="Close" OnClientClick="return HideModalPopup()" />
                    </div>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:Panel ID="pnlSLADetls" runat="server" CssClass="modal-content">
        <div>
           <div style="float: left; top: 0px;">
                <h4>SLA Time Details</h4>
                <%--<asp:Button ID="btnADvanceClose" runat="server" Text="&times; Close"  />--%>
            </div>
            <div class="respovrflw">
                <asp:GridView ID="GV_SLADetls" runat="server" AutoGenerateColumns="False" Width="99%" CssClass="Grid1" AllowSorting="True" PagerStyle-CssClass="cssPager">

                    <Columns>
                        <asp:BoundField DataField="Trouble" HeaderText="Trouble" />
                        <%--<asp:BoundField DataField="priority" HeaderText="Priority"/>--%>
                        <asp:BoundField DataField="priority_type" HeaderText="Priority Type" />
                        <asp:BoundField DataField="Resp_time" HeaderText="Response Time" />
                        <asp:BoundField DataField="Resol_time" HeaderText="Resolution Time" />
                        <asp:BoundField DataField="Resol_days" HeaderText="Resolution Days" />
                    </Columns>
                    <PagerStyle CssClass="cssPager" />

                </asp:GridView>
            </div>
        </div>

    </asp:Panel>

    <script type="text/javascript" language="javascript">
        function pageLoad() {
            //change its title

            var RatingID1 = "<%=Rating1.ClientID %>";
            for (i = 1; i <= $find("RatingBehavior1").get_MaxRating() ; i++) {
                switch (i) {
                    case 1: $get(RatingID1 + "_Star_" + (i).toString()).title = "Very Dissatisfied"; break;
                    case 2: $get(RatingID1 + "_Star_" + (i).toString()).title = "Dissatisfied"; break;
                    case 3: $get(RatingID1 + "_Star_" + (i).toString()).title = "Acceptable"; break;
                    case 4: $get(RatingID1 + "_Star_" + (i).toString()).title = "Satisfied"; break;
                    case 5: $get(RatingID1 + "_Star_" + (i).toString()).title = "Very Satisfied"; break;
                    default: break;
                }
            }

            var RatingID2 = "<%=Rating2.ClientID %>";
            for (j = 1; j <= $find("RatingBehavior2").get_MaxRating() ; j++) {
                switch (j) {
                    case 1: $get(RatingID2 + "_Star_" + (j).toString()).title = "Very Dissatisfied"; break;
                    case 2: $get(RatingID2 + "_Star_" + (j).toString()).title = "Dissatisfied"; break;
                    case 3: $get(RatingID2 + "_Star_" + (j).toString()).title = "Acceptable"; break;
                    case 4: $get(RatingID2 + "_Star_" + (j).toString()).title = "Satisfied"; break;
                    case 5: $get(RatingID2 + "_Star_" + (j).toString()).title = "Very Satisfied"; break;
                    default: break;
                }
            }


            var RatingID3 = "<%=Rating3.ClientID %>";
             for (k = 1; k <= $find("RatingBehavior3").get_MaxRating() ; k++) {
                 switch (k) {
                     case 1: $get(RatingID3 + "_Star_" + (k).toString()).title = "Very Dissatisfied"; break;
                     case 2: $get(RatingID3 + "_Star_" + (k).toString()).title = "Dissatisfied"; break;
                     case 3: $get(RatingID3 + "_Star_" + (k).toString()).title = "Acceptable"; break;
                     case 4: $get(RatingID3 + "_Star_" + (k).toString()).title = "Satisfied"; break;
                     case 5: $get(RatingID3 + "_Star_" + (k).toString()).title = "Very Satisfied"; break;
                     default: break;
                 }
             }

             var RatingID4 = "<%=Rating4.ClientID %>";
            for (m = 1; m <= $find("RatingBehavior4").get_MaxRating() ; m++) {
                switch (m) {
                    case 1: $get(RatingID4 + "_Star_" + (m).toString()).title = "Very Dissatisfied"; break;
                    case 2: $get(RatingID4 + "_Star_" + (m).toString()).title = "Dissatisfied"; break;
                    case 3: $get(RatingID4 + "_Star_" + (m).toString()).title = "Acceptable"; break;
                    case 4: $get(RatingID4 + "_Star_" + (m).toString()).title = "Satisfied"; break;
                    case 5: $get(RatingID4 + "_Star_" + (m).toString()).title = "Very Satisfied"; break;
                    default: break;
                }
            }

            var RatingID5 = "<%=Rating5.ClientID %>";
            for (n = 1; n <= $find("RatingBehavior5").get_MaxRating() ; n++) {
                switch (n) {
                    case 1: $get(RatingID5 + "_Star_" + (n).toString()).title = "Very Dissatisfied"; break;
                    case 2: $get(RatingID5 + "_Star_" + (n).toString()).title = "Dissatisfied"; break;
                    case 3: $get(RatingID5 + "_Star_" + (n).toString()).title = "Acceptable"; break;
                    case 4: $get(RatingID5 + "_Star_" + (n).toString()).title = "Satisfied"; break;
                    case 5: $get(RatingID5 + "_Star_" + (n).toString()).title = "Very Satisfied"; break;
                    default: break;
                }
            }

        }
    </script>

    <%--<div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
     <asp:Timer id="Timer1" runat="server" interval="1000" ontick="Timer1_Tick"></asp:Timer>
<asp:Label id="Lable1" runat="server" />
                    </ContentTemplate></asp:UpdatePanel></div>--%>
    <%-- <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript" src="../../js/quicksearch.js"></script>
    <script type="text/javascript">
        $(function () {
            $('.search_textbox').each(function (i) {
                $(this).quicksearch("[id*=GV_Tickets] tr:not(:has(th))", {
                    'testQuery': function (query, txt, row) {
                        return $(row).children(":eq(" + i + ")").text().toLowerCase().indexOf(query[0].toLowerCase()) != -1;
                    }
                });
            });
        });
    </script>--%>
</asp:Content>
