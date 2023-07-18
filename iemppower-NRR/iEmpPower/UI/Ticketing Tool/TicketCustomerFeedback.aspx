<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TicketCustomerFeedback.aspx.cs"
    Inherits="iEmpPower.UI.Ticketing_Tool.TicketCustomerFeedback" EnableEventValidation="false" Culture="en-GB" UICulture="auto" Theme="SkinFile" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

    <style>
        .modal-content {
            /*display:none;*/
            z-index: -1;
            background-color: rgba(0,94,124,0.2);
            /*margin: 15% auto;*/ /* 15% from the top and centered */
            padding: 20px;
            border: 2px solid #888;
            width: 50%; /*Could be more or less, depending on screen size*/
        }

        .hidden {
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


        .Td07 {
            color: #004080;
            font-size: 13px;
            width: 10px;
            padding: 8px;
            text-align: center;
            line-height: 20px !important;
        }

        .Td07Task {
            color: #fff;
            font-size: 13px;
            width: 10px;
            padding: 8px;
            text-align: center;
            line-height: 20px !important;
        }

        .Td10 {
            width: 300px !important;
            padding: 3px !important;
            text-align: left !important;
            line-height: 20px !important;
        }

        .Td06 {
            color: #004080 !important;
            font-size: 14px !important;
            width: 100px !important;
            padding: 3px !important;
            text-align: left !important;
            line-height: 20px !important;
        }

        .Td06Task {
            color: #fff !important;
            font-size: 14px !important;
            width: 180px !important;
            padding: 3px !important;
            text-align: left !important;
            line-height: 20px !important;
        }

        .resize {
            resize: none !important;
        }
        /*AutoComplete flyout */
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

        .accordionContent {
            background-color: #D3DEEF;
            border-color: -moz-use-text-color #2F4F4F #2F4F4F;
            border-right: 1px dashed #2F4F4F;
            border-style: none dashed dashed;
            border-width: medium 1px 1px;
            padding: 10px 5px 5px;
            width: 70%;
        }

        .accordionHeaderSelected {
            background-color: #00617c;
            border: 1px solid #2F4F4F;
            color: white !important;
            cursor: pointer;
            font-family: Arial,Sans-Serif;
            font-size: 12px;
            font-weight: bold;
            margin-top: 5px;
            padding: 5px;
            width: 70%;
        }

        .accordionHeader {
            background-color: #00617c;
            border: 1px solid #2F4F4F;
            color: white !important;
            cursor: pointer;
            font-family: Arial,Sans-Serif;
            font-size: 12px;
            font-weight: bold;
            margin-top: 5px;
            padding: 5px;
            width: 70%;
        }

        .href {
            color: White !important;
            font-weight: bold;
            text-decoration: none;
        }

        .PromptCSS {
            color: Orchid;
            font-size: large;
            font-style: italic;
            font-weight: bold;
            background-color: Snow;
            border: solid 1px Orchid;
            height: 30px;
        }

        .RedCls {
            width: 23px;
            height: 16px;
            background-color: #f75f0b;
            border: 1px solid #ccc;
            padding: 0;
            margin: 0 4px;
        }

        .darkRed {
            width: 23px;
            height: 16px;
            background-color: #ea0d14;
            border: 1px solid #ccc;
            padding: 0;
            margin: 0 4px;
        }

        .GreenCls {
            width: 23px;
            height: 16px;
            background-color: #008000;
            border: 1px solid #ccc;
            padding: 0;
            margin: 0 4px;
        }

        .AmberCls {
            width: 23px;
            height: 16px;
            background-color: #FFBF00;
            border: 1px solid #ccc;
            padding: 0;
            margin: 0 4px;
        }

        .Brd01 {
            border: 1px solid #666;
        }

        .Tdlegend1 {
            color: #004080;
            font-size: 13px;
            line-height: 12px !important;
            padding: 3px;
        }

        .Tdlegend2 {
            color: #004080;
            font-size: 13px;
            text-align: center;
            line-height: 12px !important;
            width: 10px;
            padding: 8px;
        }

        .Ul01, .Ul02 {
            list-style: none;
            margin: 0;
            padding: 0;
        }

            .Ul01 li {
                padding: 5px 0 5px 10px;
                margin: 3px;
            }

            .Ul02 li {
                padding: 3px 10px 3px 10px;
                margin: 3px;
            }

        .TxtC {
            text-align: center !important;
        }

        .FntPIPO {
            border-bottom: 1px solid #ccc !important;
        }

        .lgdpos {
            float: right;
            right: 145px;
            position: absolute;
            top: 138px;
        }

        /*#MainContent_pnlgrid
        {
            overflow-x: scroll !important;
        }*/

        .rowheight {
            height: 90px !important;
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
    <script type="text/javascript">

        function DisplayName() {
            $find("mpe").show();
            return false;
        }

        function HideModalPopup() {
            $find("mpe").hide();
            return false;
        }

    </script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="header">
        <div class="row clearfix">
            <div class="col-xs-12 col-sm-6">
                <span class="HeadFontSize">Customer Feedback</span>

            </div>
        </div>
    </div>
    <div class="body">
        <div class="divfr" id="divbrdr">

            <asp:Label ID="LblMsg" runat="server" CssClass="lblValidation"></asp:Label>
            <div class="col-sm-12">

                <div class="col-sm-8 Brd01">
                    <div class="form-inline">
                        <div class="col-sm-12 ">
                            <h4 class="FntPIPO" style="padding: 2px;"><b>Search</b></h4>
                        </div>
                        <div class="form-group">
                            <div class="col-sm-2 htCr" style="width: 100px">Customer <b>:</b></div>
                            <div class="col-sm-4" style="width: 165px">
                                <asp:DropDownList ID="DDLCustomerList" runat="server" TabIndex="1" CssClass="txtDropDownwidth" OnSelectedIndexChanged="DDLCustomerList_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                            </div>
                            <div class="col-sm-2 htCr" style="width: 100px">Ticket ID <b>:</b></div>
                            <div class="col-sm-4" style="width: 165px">
                                <asp:TextBox ID="TxtTID" runat="server" TabIndex="2" PlaceHolder="Ticket ID" CssClass="txtDropDownwidth"
                                    OnTextChanged="TxtTID_TextChanged" AutoPostBack="true"></asp:TextBox>
                                <Ajx:FilteredTextBoxExtender ID="FTBE_TxtTID" runat="server" TargetControlID="TxtTID"
                                    FilterType="Numbers" ValidChars="">
                                </Ajx:FilteredTextBoxExtender>
                            </div>

                        </div>
                        <asp:Panel runat="server" ID="DDEmpPnl">
                            <div class="form-group">

                                <div class="col-sm-2 htCr" style="width: 100px">Employee <b>:</b></div>
                                <div class="col-sm-3" style="width: 165px">
                                    <asp:DropDownList runat="server" ID="DDLEmployee" TabIndex="3" CssClass="txtDropDownwidth" OnSelectedIndexChanged="DDLEmployee_SelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>
                                </div>
                                <%--<div class="col-sm-2">
                                <asp:Button runat="server" ID="btnclear" CausesValidation="false" OnClick="btnclear_Click" Text="Clear" TabIndex="5" />
                            </div>--%>
                            </div>
                        </asp:Panel>
                        <div class="form-group">
                            <div class="col-sm-2">
                                <asp:Button runat="server" ID="btnclear" CausesValidation="false" OnClick="btnclear_Click" Text="Clear" TabIndex="4" />
                            </div>
                        </div>

                    </div>
                </div>
                <div class="col-sm-4">
                    <asp:LinkButton ID="BTnBackdashboard" runat="server" CssClass="Fr linkbtn" CausesValidation="false" Text="Back" OnClick="BTnBackdashboard_Click"></asp:LinkButton>
                    <br />

                </div>
            </div>

            <div class="DivSpacer03">
                <hr />
            </div>


            <div id="ticketdiv" runat="server" style="width: 99%; margin: 0 auto; padding: 2px;">
                <asp:Label ID="LblTicket" runat="server" CssClass="lblValidation"></asp:Label>
                <asp:Panel ID="pnlgrid" runat="server" Width="99%">
                    <div class="respovrflw">
                        <asp:GridView runat="server" ID="GV_TicketsRating" AutoGenerateColumns="False" Width="99%" CssClass="Grid" GridLines="Both" PagerStyle-CssClass="cssPager"
                            DataKeyNames="TID" OnSorting="GV_TicketsRating_Sorting" AllowSorting="true">
                            <%--  TicketingboObj.RID = vRow.RID;
            TicketingboObj.TID = vRow.TID;
            TicketingboObj.CLIENT = vRow.CLIENT;
            TicketingboObj.AGENT = string.IsNullOrEmpty(vRow.Agent) ? "" : vRow.Agent;
            TicketingboObj.AGENTNAME = vRow.Agentname;
            TicketingboObj.Q1 = vRow.Q1;
            TicketingboObj.Q2 = vRow.Q2;
            TicketingboObj.Q3 = vRow.Q3;
            TicketingboObj.Q4 = vRow.Q4;
            TicketingboObj.Q5 = vRow.Q5;
            TicketingboObj.RATINGCOMMENTS = vRow.RATINGCOMMENTS;
            TicketingboObj.SUMRATING = vRow.SUMRATING;
            TicketingboObj.TRAVERAGE = vRow.AVGRATING;--%>
                            <Columns>
                                <asp:TemplateField HeaderText="Slno" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="LblSlno" runat="server" Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="TID" HeaderText="Ticket ID" SortExpression="TID" />
                                <asp:TemplateField HeaderText="Client" SortExpression="CLIENT">
                                    <ItemTemplate>
                                        <%#Eval("CLIENT") + "-"+Eval("CLIENTNNAME")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Agent" SortExpression="AGENT">
                                    <ItemTemplate>
                                        <%#Eval("AGENT") + "-"+ Eval("AGENTNAME")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Initial Reaction Time" SortExpression="Q1"> 
                                    <ItemTemplate>
                                        <%#Eval("Q1")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Time for solving the issue" SortExpression="Q2">
                                    <ItemTemplate>
                                        <%#Eval("Q2")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Experience and Knowledge" SortExpression="Q3">
                                    <ItemTemplate>
                                        <%#Eval("Q3")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Friendliness and Behaviour" SortExpression="Q4">
                                    <ItemTemplate>
                                        <%#Eval("Q4")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Quality of Solution Provided" SortExpression="Q5">
                                    <ItemTemplate>
                                        <%#Eval("Q5")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Suggestions / Comments">
                                    <ItemTemplate>
                                        <%#Eval("RATINGCOMMENTS")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Rating" SortExpression="TRATING">
                                    <ItemTemplate>
                                        <%#Eval("SUMRATING")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Average Rating" SortExpression="AVGATING">
                                    <ItemTemplate>
                                        <%#(decimal.Parse(Eval("TRAVERAGE").ToString().Trim()) <=0 ? " - " : Eval("TRAVERAGE"))%>
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
                        <div class="col-sm-2 htCr" style="width: 105px">Page Size <b>:</b></div>
                        <div class="col-sm-1">
                            <asp:DropDownList ID="ddlPagesize" runat="server" CssClass="txtDropDownwidth" AutoPostBack="true" OnSelectedIndexChanged="ddlPagesize_SelectedIndexChanged" Style="float: right; width: 60px;">
                                <asp:ListItem Text="10" Value="10" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="15" Value="15"></asp:ListItem>
                                <asp:ListItem Text="20" Value="20"></asp:ListItem>
                                <asp:ListItem Text="25" Value="25"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </asp:Panel>
            </div>
            <div class="DivSpacer01"></div>



            <div runat="server" id="TicketExports">
                <div class="form-inline">
                    <div class="btn-group-lg">
                        <div class="col-sm-2">
                            <asp:Button ID="BtnExporttoXl" Width="120px" runat="server" TabIndex="5" Text="Export To Excel" OnClick="BtnExporttoXl_Click" CausesValidation="false" />
                        </div>
                        <div class="col-sm-2">
                            <asp:Button ID="BtnExporttoPDF" Width="120px" runat="server" TabIndex="6" Text="Export To PDF" OnClick="BtnExporttoPDF_Click" />
                        </div>
                        <br />
                    </div>
                </div>
            </div>

        </div>
    </div>

</asp:Content>
