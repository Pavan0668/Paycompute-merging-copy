<%@ Page Title="Ticketing Tool" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CreateIssueTicket.aspx.cs"
    Inherits="iEmpPower.UI.Ticketing_Tool.CreateIssueTicket" EnableEventValidation="false" Culture="en-GB" UICulture="auto" Theme="SkinFile"
    MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        .Td07
        {
            color: #004080;
            font-size: 13px;
            width: 10px;
            padding: 8px;
            text-align: center;
            line-height: 20px !important;
        }

        .Td07Task
        {
            color: #fff;
            font-size: 13px;
            width: 10px;
            padding: 8px;
            text-align: center;
            line-height: 20px !important;
        }

        .Td10
        {
            width: 300px !important;
            padding: 3px !important;
            text-align: left !important;
            line-height: 20px !important;
        }

        .Td06
        {
            color: #004080 !important;
            font-size: 14px !important;
            width: 180px !important;
            padding: 3px !important;
            text-align: left !important;
            line-height: 20px !important;
        }

        .Td06Task
        {
            color: #fff !important;
            font-size: 14px !important;
            width: 180px !important;
            padding: 3px !important;
            text-align: left !important;
            line-height: 20px !important;
        }

        .resize
        {
            resize: none !important;
        }
        /*AutoComplete flyout */
        .completionList
        {
            border: solid 1px #444444;
            margin: 0px;
            padding: 2px;
            height: 100px;
            overflow: auto;
            background-color: #FFFFFF;
            font-size: xx-small;
        }

        .listItem
        {
            color: #1C1C1C;
        }

        .itemHighlighted
        {
            background-color: #dbf2ff;
        }

        .accordionContent
        {
            background-color: #D3DEEF;
            border-color: -moz-use-text-color #2F4F4F #2F4F4F;
            border-right: 1px dashed #2F4F4F;
            border-style: none dashed dashed;
            border-width: medium 1px 1px;
            padding: 10px 5px 5px;
            width: 100%;
        }

        .accordionHeaderSelected
        {
            background-color: #00617c;
            border: 1px solid #2F4F4F;
            color: white !important;
            cursor: pointer;
            font-family: Arial,Sans-Serif;
            font-size: 12px;
            font-weight: bold;
            margin-top: 5px;
            padding: 5px;
            width: 100%;
        }

        .accordionHeader
        {
            background-color: #00617c;
            border: 1px solid #2F4F4F;
            color: white !important;
            cursor: pointer;
            font-family: Arial,Sans-Serif;
            font-size: 12px;
            font-weight: bold;
            margin-top: 5px;
            padding: 5px;
            width: 100%;
        }

        .href
        {
            color: White !important;
            font-weight: bold;
            text-decoration: none;
        }

        .PromptCSS
        {
            color: Orchid;
            font-size: large;
            font-style: italic;
            font-weight: bold;
            background-color: Snow;
            border: solid 1px Orchid;
            height: 30px;
        }

        .modalBackground
        {
            background-color: black;
            filter: alpha(opacity=90);
            opacity: 0.8;
            /*width: 100% !important;*/
        }

        .modalPopup
        {
            background-color: #FFFFFF;
            border-width: 3px;
            border-style: solid;
            border-color: black;
            width: 50% !important;
            height: 500px;
        }

        #MainContent_UPTASKCREATIONBTN
        {
            width: 95% !important;
            left: 20px !important;
            top: 12px !important;
            overflow: auto !important;
        }

        .paddingstyl
        {
            padding-left: 10px !important;
        }

        .txtdec
        {
            text-decoration: underline !important;
        }

        @media (max-width:600px)
        {
            .accordionHeader
            {
                width: 100%;
            }

            .accordionHeaderSelected
            {
                width: 100%;
            }

            .accordionContent
            {
                width: 100%;
            }
        }

        .PnlPadding
        {
            padding-left: 10px;
        }

        .PnlPaddingRating
        {
            padding-left: 25%;
        }

        #MainContent_UPCustRating
        {
            border-color: black;
            border-style: solid;
            background-color: white !important;
            width: 50% !important;
            /*right:25px !important;*/
        }

        .textchart
        {
            width: 22vh;
            height: 6vh;
            display: inline;
        }

        .textchart1
        {
            width: 15vh;
            height: 15vh;
            /*vertical-align:top !important;*/
            /*margin-bottom:0px !important;*/
            position: absolute;
        }


        .modal-content
        {
            /*display:none;*/
            z-index: 0;
            background-color: rgba(232,232,232,0.9);
            /*margin: 15% auto;  /*15% from the top and centered*/
            /*padding: 20px;*/
            border: 2px solid #888;
            width: 67%; /*Could be more or less, depending on screen size*/
            position: fixed;
            background-size: cover;
        }

        .modal-content1
        {
            /*display:none;*/
            z-index: 1000;
            background-color: rgba(225,225,225,0.9);
            /*margin: 15% auto;  /*15% from the top and centered*/
            /*padding: 20px;*/
            border: 2px outset #888;
            width: 40%; /*Could be more or less, depending on screen size*/
            position: fixed;
            background-size: cover;
            height: 84%;
            border-radius: 8px;
            box-shadow: 0 5px 15px rgba(0, 0, 0, .5);
        }

        .flowchart
        {
            width: 75vh;
            height: 75vh;
            position: absolute;
        }

        .lnkclass
        {
            float: right;
            padding-right: 5px;
            display: inline;
            padding-top: 3px;
        }

        .lnkclass1
        {
            float: right;
            padding-right: 5px;
            display: inline;
            padding-bottom: 20px !important;
        }
    </style>

    <script type="text/javascript">

        function DisplayName() {
            //alert(document.getElementById("DDLIssueStatus").);
            //alert(document.getElementById("<z%= DDLIssueStatus.ClientID %>").value);

            if (Page_ClientValidate()) {
                if (document.getElementById("<%= DDLIssueStatus.ClientID %>").value == "7") {
                    $find("mpe").show();
                }
            }
            return false;
        }
        function HideModalPopup() {
            $find("mpe").hide();
            $find("RatingBehavior1").set_Rating(3);
            $find("RatingBehavior2").set_Rating(3);
            $find("RatingBehavior3").set_Rating(3);
            $find("RatingBehavior4").set_Rating(3);
            $find("RatingBehavior5").set_Rating(3);
            var textcontrol = document.getElementById("<%= txtsug.ClientID %>");
            textcontrol.value = "";
            return false;
        }

        function HideModalPopupc() {
            $find("mpe").hide();
            alert("Ticket Updated Successfully !!..")
        }

        function showpop() {
            document.getElementById("<%= divtxtflow.ClientID %>").style.display = "";
            document.getElementById("<%= divfnlflowchart.ClientID %>").style.display = "";
            document.getElementById("<%= divpanelnotcheckedflow.ClientID %>").style.display = "";
        }
    </script>




</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="body">
        <div id="TicketDiv" runat="server">
            <div class="header">
                <div class="row clearfix">
                    <div class="col-xs-12 col-sm-6">
                        <span class="HeadFontSize">Tickets</span>

                    </div>
                </div>
            </div>
            <Ajx:ModalPopupExtender ID="Modalpop_statusFlow" CancelControlID="btncl1" runat="server" TargetControlID="linkstatusflow"
                PopupControlID="txtflow">
            </Ajx:ModalPopupExtender>
            <%-- <Ajx:HoverMenuExtender ID="hoverstatusflow" runat="server" TargetControlID="linkstatusflow"
                PopupControlID="txtflow" PopupPosition="left">
            </Ajx:HoverMenuExtender>--%>
            <div id="divtxtflow" runat="server" style="display: none">
                <asp:Panel runat="server" ID="txtflow" CssClass="modal-content">
                    <div class="Fr">
                        <asp:Button ID="btncl1" runat="server" Text="&times;" />
                    </div>
                    <br />
                    <br />
                    <div style="margin-left: 8px">

                        <div style="display: inline">
                            <p style="font-weight: bold; float: left">IN REVIEW CHECKED</p>
                            <asp:LinkButton ID="linkflowchart" runat="server" CssClass="lnkclass" CausesValidation="false" Enabled="false" Text="View Flow chart"></asp:LinkButton>
                        </div>
                        <br />
                        <br />
                        <img src="../../images/Picture11.PNG" class="textchart" />
                        <img src="../../images/Picture2.PNG" class="textchart" />
                        <img src="../../images/Picture3.PNG" class="textchart" />
                        <img src="../../images/Picture4.PNG" class="textchart" />
                        <img src="../../images/Picture5.PNG" class="textchart" />

                        <img src="../../images/Picture12.PNG" class="textchart1" />
                        <br />
                        <br />

                        <img src="../../images/Picture10.PNG" class="textchart" />
                        <img src="../../images/Picture9.PNG" class="textchart" />
                        <img src="../../images/Picture8.PNG" class="textchart" />
                        <img src="../../images/Picture7.PNG" class="textchart" />
                        <img src="../../images/Picture6.PNG" class="textchart" />
                    </div>
                    <br />
                    <br />
                    <br />
                    <div>
                        <div style="display: inline">
                            <p style="font-weight: bold; float: left">IN REVIEW NOT CHECKED</p>
                            <asp:LinkButton ID="lnkbtnflownotcheck" runat="server" CssClass="lnkclass1" CausesValidation="false" Enabled="false" Text="View Flow chart"></asp:LinkButton>
                        </div>
                        <br />
                        <br />
                        <img src="../../images/Picture11.PNG" class="textchart" />
                        <img src="../../images/Picture2.PNG" class="textchart" />
                        <img src="../../images/Picture3.PNG" class="textchart" />
                        <img src="../../images/Withouprd.png" class="textchart" />
                        <img src="../../images/Withouprd9.png" class="textchart" />
                        <img src="../../images/Withouprd10.png" class="textchart" />
                    </div>
                </asp:Panel>
            </div>

            <%--  <Ajx:HoverMenuExtender ID="hvrflowchart1" runat="server" TargetControlID="linkflowchart"
                PopupControlID="fnlflowchart" PopupPosition="left">
            </Ajx:HoverMenuExtender>--%>
            <Ajx:ModalPopupExtender ID="ModalPopupExtender1" CancelControlID="btncl2" runat="server" TargetControlID="linkflowchart" PopupControlID="fnlflowchart"></Ajx:ModalPopupExtender>
            <div id="divfnlflowchart" runat="server" style="display: none">
                <asp:Panel runat="server" ID="fnlflowchart" CssClass="modal-content1">
                    <div style="margin-left: 22px">
                        <div class="Fr">
                            <asp:Button ID="btncl2" runat="server" Text="&times;" />
                        </div>
                        <p style="font-weight: bold; text-align: center; margin-top: 2px">InReview Checked Flow chart</p>

                        <img src="../../images/flowchart.PNG" class="flowchart" />
                    </div>
                </asp:Panel>
            </div>

            <%--            <Ajx:HoverMenuExtender ID="hvrflowchart2" runat="server" TargetControlID="lnkbtnflownotcheck"
                PopupControlID="panelnotcheckedflow" PopupPosition="left">
            </Ajx:HoverMenuExtender>--%>
            <Ajx:ModalPopupExtender ID="ModalPopupExtender2" CancelControlID="btncl3" runat="server" TargetControlID="lnkbtnflownotcheck" PopupControlID="panelnotcheckedflow"></Ajx:ModalPopupExtender>
            <div id="divpanelnotcheckedflow" runat="server" style="display: none">
                <asp:Panel runat="server" ID="panelnotcheckedflow" CssClass="modal-content1">
                    <div style="margin-left: 22px">
                        <div class="Fr">
                            <asp:Button ID="btncl3" runat="server" Text="&times;" />
                        </div>
                        <p style="font-weight: bold; text-align: center; margin-top: 2px">InReview not checked Flow chart</p>

                        <img src="../../images/flownotchecked.PNG" class="flowchart" />
                    </div>
                </asp:Panel>
            </div>
            <asp:Label ID="LblMsg" runat="server" CssClass="lblValidation"></asp:Label>

            <div class="Fr">


                <table>
                    <tr>
                        <td>
                            <asp:LinkButton ID="LblCreateTask" runat="server" CssClass="Fr paddingstyl linkbtn" CausesValidation="false" Text="Task" OnClick="LblCreateTask_Click"></asp:LinkButton>
                        </td>
                        <td>
                            <p class="paddingstyl">| </p>
                        </td>
                        <td>

                            <asp:LinkButton ID="LinkBtnBack" runat="server" CssClass="Fr paddingstyl linkbtn" CausesValidation="false" Text="Back" OnClick="LinkBtnBack_Click"></asp:LinkButton>
                        </td>
                        <td>
                            <p class="paddingstyl">| </p>
                        </td>
                        <td>
                            <asp:LinkButton ID="linkstatusflow" runat="server" CssClass="Fr paddingstyl linkbtn" CausesValidation="false" Enabled="false" Text="Status flow"></asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </div>


            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="form-inline">
                        <div class="col-sm-12">
                            <asp:Timer ID="Timer1" runat="server" Interval="1000" OnTick="Timer1_Tick">
                            </asp:Timer>
                            <h4>
                                <div class="col-sm-5">
                                    SLA - Time Remaining (Days.hh:mm:ss) -
                                </div>
                                <div class="col-sm-5">
                                    <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                                </div>
                            </h4>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>


            <div>
                <%--<asp:FormView runat="server" ID="FormTicket" Width="99%">

                    <ItemTemplate>

                        <div class="DivSpacer01"></div>
                        <div id="div1" style="float: left; width: 50%">
                            <table style="border-collapse: collapse; width: 100%;">



                                <tr>
                                    <td class="Td06">Ticket ID</td>
                                    <td class="Td07"><b>:</b> </td>
                                    <td><%#(Eval("TID"))%> 
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Td06">Title</td>
                                    <td class="Td07"><b>:</b> </td>
                                    <td><%#(Eval("TITLE"))%> 
                                    </td>
                                </tr>

                                <tr>
                                    <td class="Td06">Issue Description</td>
                                    <td class="Td07"><b>:</b> </td>
                                    <td><%#(Eval("ISSDESC"))%> 
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Td06">Client</td>
                                    <td class="Td07"><b>:</b> </td>
                                    <td><%#(Eval("CLIENT")) +"-"+ (Eval("CLIENTNNAME"))%> 
                                    </td>
                                </tr>

                                <tr>
                                    <td class="Td06">Raised By</td>
                                    <td class="Td07"><b>:</b> </td>
                                    <td><%#(Eval("FRMUSR"))%> 
                                    </td>
                                </tr>


                            </table>
                        </div>
                        <div id="div2" style="float: right; width: 50%">
                            <table style="border-collapse: collapse; width: 100%;">


                                <tr>
                                    <td class="Td06">Current Status</td>
                                    <td class="Td07"><b>:</b> </td>
                                    <td><%#(Eval("StatusTxt"))%> 
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Td06">Currently With</td>
                                    <td class="Td07"><b>:</b> </td>
                                    <td><%#(Eval("ASSIGNEE")) + " - " +(Eval("TOASSIGNEENNAME"))%> 
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Td06">Last Modified By</td>
                                    <td class="Td07"><b>:</b> </td>
                                    <td><%#(Eval("LASTMODIFIED_BY")=="" ?Eval("CLIENT") + "-"+Eval("CLIENTNNAME") : Eval("LASTMODIFIED_BY") +"-"+Eval("MODIFIEDONNAME"))%>  
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Td06">Last Modified On</td>
                                    <td class="Td07"><b>:</b> </td>

                                    <td>
                                        <%#(Eval("LASTMODIFIED_ON","{0:dd-MM-yyyy}").ToString()=="01-01-1900") ? Eval("CREATED_ON" ,"{0:dd-MMM-yyyy HH:mm:ss} ") : Eval("LASTMODIFIED_ON","{0:dd-MMM-yyyy HH:mm:ss} ")%>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Td06">Agent</td>
                                    <td class="Td07"><b>:</b> </td>
                                    <td><%#(Eval("AGENT")) + "-"+(Eval("AGENTNAME"))%> 
                                    </td>
                                </tr>

                            </table>
                        </div>
                    </ItemTemplate>
                </asp:FormView>--%>

                <asp:FormView runat="server" ID="FormTicket" Width="99%">

                    <ItemTemplate>
                        <div class="DivSpacer01"></div>
                        <div class="row col-sm-12">
                            <div id="div1" class="col-sm-6">
                                <div class="form-inline">
                                    <div class="form-group">
                                        <div class="col-sm-5 htCr">Ticket ID <b>:</b> </div>
                                        <%--<td class="Td07"><b>:</b> </td>--%>
                                        <div class="col-sm-6">
                                            <%#(Eval("TID"))%>
                                        </div>
                                    </div>
                                    <div class="form-inline">
                                        <div class="col-sm-5 htCr">Title <b>:</b></div>
                                        <%--<td class="Td07"><b>:</b> </td>--%>
                                        <div class="col-sm-6">
                                            <%#(Eval("TITLE"))%>
                                        </div>
                                    </div>

                                    <div class="form-inline">
                                        <div class="col-sm-5 htCr">Issue Description <b>:</b></div>
                                        <%--<td class="Td07"><b>:</b> </td>--%>
                                        <div class="col-sm-6">
                                            <%#(Eval("ISSDESC"))%>
                                        </div>
                                    </div>
                                    <div class="form-inline">
                                        <div class="col-sm-5 htCr">Client <b>:</b></div>
                                        <%--<td class="Td07"><b>:</b> </td>--%>
                                        <div class="col-sm-6">
                                            <%#(Eval("CLIENT")) +"-"+ (Eval("CLIENTNNAME"))%>
                                            <asp:HiddenField ID="HF_clint" runat="server" Value='<%#(Eval("CLIENT"))%>' />
                                        </div>
                                    </div>

                                    <div class="form-inline">
                                        <div class="col-sm-5 htCr">Raised By <b>:</b></div>
                                        <%--<td class="Td07"><b>:</b> </td>--%>
                                        <div class="col-sm-6">
                                            <%#(Eval("FRMUSR"))%>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div id="div2" class="col-sm-6">
                                <div class="form-inline">
                                    <div class="form-group">
                                        <div class="col-sm-5 htCr">Current Status <b>:</b></div>
                                        <%--<td class="Td07"><b>:</b> </td>--%>
                                        <div class="col-sm-6">
                                            <%#(Eval("StatusTxt"))%>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-sm-5 htCr">Currently With <b>:</b></div>
                                        <%--<td class="Td07"><b>:</b> </td>--%>
                                        <div class="col-sm-6">
                                            <%#(Eval("ASSIGNEE")) + " - " +(Eval("TOASSIGNEENNAME"))%>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-sm-5 htCr">Last Modified By <b>:</b></div>
                                        <%--<td class="Td07"><b>:</b> </td>--%>
                                        <div class="col-sm-6">
                                            <%#(Eval("LASTMODIFIED_BY")=="" ?Eval("CLIENT") + "-"+Eval("CLIENTNNAME") : Eval("LASTMODIFIED_BY") +"-"+Eval("MODIFIEDONNAME"))%>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-sm-5 htCr">Last Modified On <b>:</b></div>
                                        <%--<td class="Td07"><b>:</b> </td>--%>

                                        <div class="col-sm-6">
                                            <%#(Eval("LASTMODIFIED_ON","{0:dd-MM-yyyy}").ToString()=="01-01-1900") ? Eval("CREATED_ON" ,"{0:dd-MMM-yyyy HH:mm:ss} ") : Eval("LASTMODIFIED_ON","{0:dd-MMM-yyyy HH:mm:ss} ")%>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-sm-5 htCr">Agent <b>:</b></div>
                                        <%--<td class="Td07"><b>:</b> </td>--%>
                                        <div class="col-sm-6">
                                            <%#(Eval("AGENT")) + "-"+(Eval("AGENTNAME"))%>
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>

                    </ItemTemplate>
                </asp:FormView>

            </div>

            <div class="DivSpacer01"></div>

            <div id="DivTicketForm" runat="server" visible="false">
                <div id="divTblTicket1" runat="server" class="col-sm-12">
                    <div class="form-inline">
                        <div class="form-group">
                            <div class="col-sm-2 htCr">
                                <span class="rcls"></span>Title <b>:</b>
                            </div>
                            <%--<td class="Td07"><b>:</b> </td>--%>
                            <div class="col-sm-8">
                                <asp:TextBox ID="TxtTite" runat="server" placeholder="Title of Ticket" MaxLength="200" CssClass="txtDropDownwidth" TabIndex="1"></asp:TextBox>

                                <%--<td></td>--%>
                                <asp:RequiredFieldValidator ID="RFV_Title" runat="server" ControlToValidate="TxtTite"
                                    Display="Dynamic" ErrorMessage="Please Enter the Title" ForeColor="Red" ValidationGroup="VGSubmit"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-sm-2 htCr">
                                <span class="rcls"></span>Issue Description <b>:</b>
                            </div>
                            <%--<td class="Td07"><b>:</b> </td>--%>
                            <div class="col-sm-8">
                                <asp:TextBox ID="TxtIssDesc" runat="server" placeholder="Description of Ticket" TextMode="MultiLine" CssClass="txtDropDownwidth" TabIndex="2"></asp:TextBox>

                                <%--<td></td>--%>
                                <asp:RequiredFieldValidator ID="RFV_TxtIssDesc" runat="server" ControlToValidate="TxtIssDesc"
                                    Display="Dynamic" ErrorMessage="Please Enter the Issue Desc" ForeColor="Red" ValidationGroup="VGSubmit"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-sm-2 htCr">
                                <span class="rcls"></span>Client <b>:</b>
                            </div>
                            <%--<td class="Td07"><b>:</b> </td>--%>
                            <div class="col-sm-8">
                                <asp:TextBox ID="TxtRaisedby" runat="server" placeholder="Client Name" MaxLength="200" CssClass="txtDropDownwidth" Enabled="false" TabIndex="3"></asp:TextBox>
                            </div>
                            <%--</td> <td>--%>
                        </div>
                        <div class="form-group">
                            <div class="col-sm-2 htCr"><span class="rcls"></span>Raised By <b>:</b></div>
                            <%--<td class="Td07"><b>:</b> </td>--%>
                            <div class="col-sm-8">
                                <asp:TextBox ID="TxtFrmUser" runat="server" placeholder="Ticket Raising By" MaxLength="200" CssClass="txtDropDownwidth" TabIndex="4"></asp:TextBox>
                                       <Ajx:FilteredTextBoxExtender ID="FTE_raisedby" runat="server" TargetControlID="TxtFrmUser" ValidChars=" " FilterType="Custom,LowercaseLetters,UppercaseLetters"></Ajx:FilteredTextBoxExtender>
                                <%--<td></td>--%>
                                <asp:RequiredFieldValidator ID="RFV_TxtFrmUser" runat="server" ControlToValidate="TxtFrmUser"
                                    Display="Dynamic" ErrorMessage="Please Enter Raised By" ForeColor="Red" ValidationGroup="VGSubmit"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                </div>
                <div id="divTblticket2" runat="server" class="col-sm-12">
                    <div class="form-inline">
                        <div class="form-group">
                            <div class="col-sm-2 htCr">
                                <span class="rcls"></span>Requestor Mail-ID <b>:</b>
                            </div>
                            <%--<td class="Td07"><b>:</b> </td>--%>
                            <div class="col-sm-8">
                                <asp:TextBox ID="TxtUsrMailID" placeholder="Raising By Mail-ID" runat="server" MaxLength="200" CssClass="txtDropDownwidth" TabIndex="5"></asp:TextBox>

                                <%--<td></td>--%>
                                <asp:RequiredFieldValidator ID="RFV_TxtUsrMailID" runat="server" ControlToValidate="TxtUsrMailID"
                                    Display="Dynamic" ErrorMessage="Please Enter User Mail ID" ForeColor="Red" ValidationGroup="VGSubmit"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="REV_TxtUsrMailID" runat="server" Display="Dynamic"
                                    ErrorMessage="Invalid E-mail" ControlToValidate="TxtUsrMailID" ValidationGroup="VGSubmit"
                                    SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                    ForeColor="Red"></asp:RegularExpressionValidator>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-sm-2 htCr">
                                CC MailID's <b>:</b>
                            </div>
                            <%--<td class="Td07"><b>:</b> </td>--%>
                            <div class="col-sm-8">
                                <asp:TextBox ID="TxtCCMailID" runat="server" placeholder="CC Mail-ID's" CssClass="txtDropDownwidth" TabIndex="6"></asp:TextBox>
                                <Ajx:AutoCompleteExtender ID="EmpMailIDAutoCompleteExtender" runat="server" TargetControlID="TxtCCMailID" MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="5" CompletionInterval="1" FirstRowSelected="True"
                                    ServiceMethod="GetEmployeeMailId" ServicePath="~/WebService/Service.asmx"
                                    CompletionListCssClass="completionList"
                                    CompletionListHighlightedItemCssClass="itemHighlighted"
                                    CompletionListItemCssClass="listItem" DelimiterCharacters=","
                                    ShowOnlyCurrentWordInCompletionListItem="true">
                                </Ajx:AutoCompleteExtender>
                                <span>(Separated by comma and no space)</span>

                                <%--<td> </td>--%>
                                <asp:RegularExpressionValidator ID="REV_TxtCCMailID" runat="server" Display="Dynamic"
                                    ErrorMessage="Invalid E-mail" ControlToValidate="TxtCCMailID" ValidationGroup="VGSubmit"
                                    SetFocusOnError="True" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*([,]\s*\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)*"
                                    ForeColor="Red"></asp:RegularExpressionValidator>

                            </div>
                            <%--<td></td>--%>
                        </div>
                        <%--   <asp:Panel runat="server" ID="PnlAssindTo">--%>
                        <div runat="server" class="form-group" id="divAssindTotr">
                            <%--<tr runat="server" id="AssindTotr">--%>
                            <div class="col-sm-2 htCr">
                                <span class="rcls"></span>Assigned To <b>:</b>
                            </div>
                            <%--<td class="Td07"><b>:</b> </td>--%>
                            <div class="col-sm-8">
                                <%-- <asp:TextBox ID="TxtAssignee" runat="server" MaxLength="200" CssClass="txtDropDownwidth1"></asp:TextBox>--%>
                                <%--  <div></div>--%><%--onclick="this.size=1;" onMouseOut="this.size=1;" Style="position: absolute;"--%>
                                <asp:DropDownList ID="ddlAssignee" runat="server" TabIndex="7" CssClass="txtDropDownwidth"></asp:DropDownList>
                                <Ajx:ListSearchExtender ID="LSE_ddlAssignee" TargetControlID="ddlAssignee" PromptText="Search Assignee" PromptPosition="Top" QueryPattern="Contains" IsSorted="true" runat="server"></Ajx:ListSearchExtender>
                            </div>
                            <%--<td></td>--%>
                            <%--<asp:RequiredFieldValidator ID="RFV_ddlAssignee" runat="server" ControlToValidate="ddlAssignee" InitialValue="0" 
                            Display="Dynamic" ErrorMessage="Please Select Agssignee" ForeColor="Red" ValidationGroup="VGSubmit"></asp:RequiredFieldValidator>--%>
                        </div>
                        <%--</asp:Panel>--%>
                        <div class="form-group">
                            <div class="col-sm-2 htCr">
                                <span class="rcls"></span>Priority <b>:</b>
                            </div>
                            <%--<td class="Td07"><b>:</b> </td>--%>
                            <div class="col-sm-8">
                                <asp:DropDownList runat="server" ID="DDLIssuePriority" CssClass="txtDropDownwidth" TabIndex="8">
                                </asp:DropDownList>

                                <%--<td></td>--%>
                                <asp:RequiredFieldValidator ID="RFV_DDLIssuePriority" runat="server" ControlToValidate="DDLIssuePriority" InitialValue="0"
                                    Display="Dynamic" ErrorMessage="Please Select Priority" ForeColor="Red" ValidationGroup="VGSubmit"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div id="divtrCategory" class="form-group" runat="server">
                            <%--<tr id="trCategory" runat="server">--%>
                            <div class="col-sm-2 htCr">
                                <span class="rcls"></span>Category <b>:</b>
                            </div>
                            <%--<td class="Td07"><b>:</b> </td>--%>
                            <div class="col-sm-8">
                                <asp:DropDownList runat="server" ID="DDLIssueCategory" CssClass="txtDropDownwidth" TabIndex="9">
                                </asp:DropDownList>

                                <%--<td></td>--%>
                                <asp:RequiredFieldValidator ID="RFV_DDLIssueCategory" runat="server" ControlToValidate="DDLIssueCategory" InitialValue="0"
                                    Display="Dynamic" ErrorMessage="Please select Category" ForeColor="Red" ValidationGroup="VGSubmit"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div id="divtrIssueCatCss" class="form-group" runat="server">
                            <%-- <tr id="trIssueCatCss" runat="server">--%>
                            <div class="col-sm-2 htCr">
                                <span class="rcls"></span>Module <b>:</b>
                            </div>
                            <%--<td class="Td07"><b>:</b> </td>--%>
                            <div class="col-sm-8">
                                <asp:DropDownList runat="server" ID="DDLIssueCategoryCSS" CssClass="txtDropDownwidth" TabIndex="10">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RFV_DDLIssueCategoryCSS" runat="server" ControlToValidate="DDLIssueCategoryCSS" InitialValue="0"
                                    Display="Dynamic" ErrorMessage="Please Select Module" ForeColor="Red" ValidationGroup="VGSubmit" Enabled="false"></asp:RequiredFieldValidator>
                            </div>
                            <%--<td></td>--%>
                        </div>



                        <div id="divtrIssueType" class="form-group" runat="server">
                            <%-- <tr id="trIssueCatCss" runat="server">--%>
                            <div class="col-sm-2 htCr">
                                Issue Type <b>:</b>
                            </div>
                            <%--<td class="Td07"><b>:</b> </td>--%>
                            <div class="col-sm-8">
                                <asp:DropDownList runat="server" ID="DDLIssueType" CssClass="txtDropDownwidth" TabIndex="10">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RFV_DDLIssueType" runat="server" ControlToValidate="DDLIssueType" InitialValue="0"
                                    Display="Dynamic" ErrorMessage="Please select Issue Type" ForeColor="Red" ValidationGroup="VGSubmit" Enabled="false"></asp:RequiredFieldValidator>
                            </div>
                            <%--<td></td>--%>
                        </div>



                        <div class="form-group">
                            <div class="col-sm-2 htCr">
                                <span class="rcls"></span>Comments <b>:</b>
                            </div>
                            <%--<td class="Td07"><b>:</b> </td>--%>
                            <div class="col-sm-8">
                                <asp:TextBox ID="TxtComments" runat="server" placeholder="Comments to Assignee" TextMode="MultiLine" CssClass="txtDropDownwidth" TabIndex="11"></asp:TextBox>

                                <%--<td></td>--%>
                                <asp:RequiredFieldValidator ID="RFV_TxtComments" runat="server" ControlToValidate="TxtComments"
                                    Display="Dynamic" ErrorMessage="Please enter comments" ForeColor="Red" ValidationGroup="VGSubmit"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-sm-2 htCr">
                                Attachments <b>:</b>
                            </div>
                            <%--<td class="Td07"><b>:</b> </td>--%>
                            <div class="col-sm-8">
                                <asp:FileUpload ID="FU_Isssue" runat="server" AllowMultiple="true" TabIndex="12" />

                                <%--<td></td>--%>
                                <asp:Label ID="FU_IsssueName" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div id="divCbtr" class="form-group" runat="server">
                            <%--<tr id="Cbtr" runat="server">--%>
                            <div class="col-sm-2 htCr">Review Required? <b>:</b></div>
                            <%--<td class="Td07"><b>:</b> </td>--%>
                            <div class="col-sm-8">
                                <asp:CheckBox ID="Cb_InReviewNeed" runat="server" Text=" " OnCheckedChanged="Cb_InReviewNeed_CheckedChanged" AutoPostBack="true" TabIndex="13" />
                            </div>
                            <%--<td></td>--%>
                        </div>
                        <div class="form-group">
                            <div class="col-sm-2 htCr">
                                Status <b>:</b>
                            </div>
                            <%--<td class="Td07"><b>:</b> </td>--%>
                            <div class="col-sm-8">
                                <asp:DropDownList runat="server" ID="DDLIssueStatus" TabIndex="14" CssClass="txtDropDownwidth" OnSelectedIndexChanged="DDLIssueStatus_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>

                                <%--<td></td>--%>
                                <asp:RequiredFieldValidator ID="RFV_DDLIssueStatus" runat="server" ControlToValidate="DDLIssueStatus" InitialValue="0"
                                    Display="Dynamic" ErrorMessage="Please select Status" ForeColor="Red" ValidationGroup="VGSubmit"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div id="divTrPlndhrs" runat="server" class="form-group">
                            <%-- <tr id="TrPlndhrs" runat="server">--%>
                            <div class="col-sm-2 htCr">Planned Hours <b>:</b></div>
                            <%--<td class="Td07"><b>:</b> </td>--%>
                            <div class="col-sm-8">
                                <asp:TextBox ID="TxtPlndHrs" runat="server" placeholder="Planned Hours" CssClass="txtDropDownwidth" TabIndex="15"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" Enabled="false" ID="RF_plannedHr" ControlToValidate="TxtPlndHrs" ForeColor="Red"
                                    SetFocusOnError="true" Display="Dynamic" CssClass="lblValidation" ValidationGroup="VGSubmit" ErrorMessage="Please Enter Planned Hour..!"></asp:RequiredFieldValidator>
                                <asp:RangeValidator ID="RV_TxtPlndHrs" Enabled="false" runat="server" ControlToValidate="TxtPlndHrs" ForeColor="Red" ValidationGroup="VGSubmit"
                                    SetFocusOnError="true" Display="Dynamic" CssClass="lblValidation" ErrorMessage="Invalid Entry..!"
                                    MinimumValue="0.10" MaximumValue="999"></asp:RangeValidator>
                                <%--<td></td>--%>
                                <Ajx:FilteredTextBoxExtender ID="FTBE_TxtPlndHrs" runat="server" TargetControlID="TxtPlndHrs"
                                    FilterType="Numbers,Custom" ValidChars=".">
                                </Ajx:FilteredTextBoxExtender>

                            </div>
                        </div>
                        <div id="divTrActhrs" runat="server" class="form-group">
                            <%--<tr id="TrActhrs" runat="server">--%>
                            <div class="col-sm-2 htCr">Actual Hours <b>:</b></div>
                            <%--<td class="Td07"><b>:</b> </td>--%>
                            <div class="col-sm-8">
                                <asp:TextBox ID="TxtTrActhrs" runat="server" placeholder="Actual Hours" CssClass="txtDropDownwidth" TabIndex="16"></asp:TextBox>

                                <%--<td></td>--%>
                                <Ajx:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="TxtTrActhrs"
                                    FilterType="Numbers,Custom" ValidChars=".">
                                </Ajx:FilteredTextBoxExtender>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-sm-12">
                <div class="" id="divTblButtons" runat="server">
                    <%--TblButtons--%>
                    <div class="form-inline">
                        <div class="form-group">
                            <div class="col-sm-12">
                                <%-- <div></div>--%><asp:Button ID="BtnBack" Width="85px" runat="server" Text="Back" OnClick="BtnBack_Click" TabIndex="17" CausesValidation="false" />
                                <%--<div></div>--%>
                                <asp:Button ID="BtnSubmit" runat="server" Text="Submit" Width="85px" OnClick="BtnSubmit_Click" CausesValidation="true" ValidationGroup="VGSubmit" TabIndex="18" />
                                <%--<div></div>--%>
                                <asp:Button ID="BtnEdit" runat="server" Text="Edit" Width="85px" OnClick="BtnEdit_Click" TabIndex="19" CausesValidation="false" />
                                <%--<div></div>--%>
                                <asp:Button ID="BtnUpdate" runat="server" Text="Update" Width="85px" OnClick="BtnUpdate_Click" ValidationGroup="VGSubmit" CausesValidation="true" TabIndex="20" />
                                <%--<div></div>--%>
                                <asp:Button ID="BtnCancel" runat="server" Text="Cancel" Width="85px" OnClick="BtnCancel_Click" ValidationGroup="VGSubmit" CausesValidation="true" TabIndex="21" />
                                <%--<div></div>--%>
                                <%--<asp:Button ID="BtnPending" runat="server" Text="Pending" OnClick="BtnPending_Click" ValidationGroup="VGSubmit" CausesValidation="true" TabIndex="20"/></td>
                    <td>&nbsp;&nbsp;--%>
                                <asp:Button ID="BtnCompleted" runat="server" Width="85px" Text="Update" OnClick="BtnCompleted_Click" ValidationGroup="VGSubmit" CausesValidation="true" TabIndex="22" />
                                <%--               <td>&nbsp;&nbsp;
                    <asp:Button ID="BtnApprove" runat="server" Text="Approve" onclick="BtnApprove_Click"/></td>
                <td>&nbsp;&nbsp;
                    <asp:Button ID="BtnReject" runat="server" Text="Reject" OnClick="BtnReject_Click"/></td>--%>
                                <%--<div></div>--%>
                                <%--<asp:Button ID="BtnConfirm" runat="server" Width="85px" Text="Confirm" OnClick="BtnConfirm_Click" ValidationGroup="VGSubmit" CausesValidation="true" TabIndex="23" />--%>
                                <%--<div></div>--%>

                                <asp:Button ID="BtnConfirm" runat="server" Width="85px" Text="Confirm" CausesValidation="false" OnClientClick="return DisplayName()" TabIndex="23" />
                                <asp:Button ID="BtnConfirmUAT" runat="server" Width="85px" Text="Confirm" CausesValidation="true" OnClick="BtnConfirmUAT_Click" TabIndex="23" />
                                <asp:Button ID="BtnDeny" runat="server" Text="Deny" Width="85px" OnClick="BtnDeny_Click" ValidationGroup="VGSubmit" CausesValidation="true" TabIndex="24" />
                                <%--<div></div>--%>
                                <asp:Button ID="BtnRework" runat="server" Width="85px" Text="Rework" OnClick="BtnRework_Click" ValidationGroup="VGSubmit" CausesValidation="true" TabIndex="25" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-sm-12">
                    <Ajx:Accordion ID="UserAccordion" runat="server" HeaderCssClass="accordionHeader"
                        HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent" FadeTransitions="true" SuppressHeaderPostbacks="true" TransitionDuration="250" FramesPerSecond="40" RequireOpenedPane="false" AutoSize="None">
                        <Panes>


                            <Ajx:AccordionPane ID="AccordionPane2" runat="server">
                                <Header><a href="#" class="href">Comments</a></Header>
                                <Content>
                                    <div class="respovrflw">
                                        <asp:GridView ID="grdTicketsComments" runat="server" AutoGenerateColumns="False" CssClass="Grid" GridLines="Both" PagerStyle-CssClass="cssPager" Width="99%">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Slno">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblSlno" runat="server" Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="TID" HeaderText="Ticket ID" />
                                                <asp:BoundField DataField="COMMENTS" HeaderText="Comments" />
                                                <%--<asp:BoundField DataField="MODIFIED_BY" HeaderText="Commented By" />--%>
                                                <asp:TemplateField HeaderText="Commented By">
                                                    <ItemTemplate>
                                                        <%# Eval("MODIFIED_BY") + " - " + Eval("MODIFIEDONNAME")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="MODIFIED_ON" HeaderText="Commented On" DataFormatString="{0:dd-MMM-yyyy HH:mm:ss}" />

                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </Content>
                            </Ajx:AccordionPane>

                            <Ajx:AccordionPane ID="AccordionPane1" runat="server">
                                <Header><a href="#" class="href">Attachments</a></Header>
                                <Content>
                                    <div class="respovrflw">
                                        <asp:GridView ID="GrdTicketsAttachments" runat="server" AutoGenerateColumns="False" CssClass="Grid" GridLines="Both" PagerStyle-CssClass="cssPager" Width="99%"
                                            OnRowCommand="GrdTicketsAttachments_RowCommand">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Slno">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblSlno" runat="server" Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="TID" HeaderText="Ticket ID" />
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        Attachments
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="LinkButton1" CssClass="linkbtn" runat="server" Text='<%#Eval("ATTACHEMENT_FID") %>' Font-Bold="True" CommandName="download" CommandArgument='<%# Eval("ATTACHEMENT_FPATH") %>' CausesValidation="false" />
                                                    </ItemTemplate>
                                                    <ItemStyle Width="100" />
                                                </asp:TemplateField>
                                                <%--<asp:BoundField DataField="MODIFIED_BY" HeaderText="Uploaded By" />--%>
                                                <asp:TemplateField HeaderText="Uploaded By">
                                                    <ItemTemplate>
                                                        <%# Eval("MODIFIED_BY") + " - " + Eval("MODIFIEDONNAME")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="MODIFIED_ON" HeaderText="Uploaded On" DataFormatString="{0:dd-MMM-yyyy HH:mm:ss}" />
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </Content>
                            </Ajx:AccordionPane>

                            <Ajx:AccordionPane ID="AccordionPane3" runat="server">
                                <Header><a href="#" class="href">Status</a> </Header>
                                <Content>
                                    <div class="respovrflw">
                                        <asp:GridView ID="GrdTicketStatus" runat="server" AutoGenerateColumns="False" CssClass="Grid" GridLines="Both" PagerStyle-CssClass="cssPager" Width="99%">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Slno">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblSlno" runat="server" Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="TID" HeaderText="Ticket ID" />
                                                <asp:TemplateField HeaderText="From User">
                                                    <ItemTemplate>
                                                        <%#Eval("FRMASSIGNEE") + "-"+Eval("FRMASSIGNEENNAME") %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="To User">
                                                    <ItemTemplate>
                                                        <%#Eval("TOASSIGNEE") + "-"+Eval("TOASSIGNEENNAME") %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="From Status">
                                                    <ItemTemplate>
                                                        <%#Eval("FRMSTATUSTXT") %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="To Status">
                                                    <ItemTemplate>
                                                        <%#Eval("TOSTATUSTXT") %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:BoundField DataField="MODIFIED_BY" HeaderText="By" />--%>
                                                <asp:TemplateField HeaderText="By">
                                                    <ItemTemplate>
                                                        <%# Eval("MODIFIED_BY") + " - " + Eval("MODIFIEDONNAME")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="MODIFIED_ON" HeaderText="On" DataFormatString="{0:dd-MMM-yyyy HH:mm:ss}" />
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </Content>
                            </Ajx:AccordionPane>
                        </Panes>
                    </Ajx:Accordion>

                </div>

            </div>
            <div class="DivSpacer01"></div>

            <%--<asp:HiddenField ID="HFPrevStsID" runat="server" />--%>
            <asp:LinkButton ID="lnkDummy" runat="server"></asp:LinkButton>
            <Ajx:ModalPopupExtender ID="mp1" runat="server" PopupControlID="UPCustRating" TargetControlID="lnkDummy" BehaviorID="mpe"
                BackgroundCssClass="modalBackground">
            </Ajx:ModalPopupExtender>


            <asp:UpdatePanel ID="UPCustRating" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Panel runat="server" ID="PnlRating" Width="99%" Height="600px">
                        <div runat="server" id="RatingDiv">
                            <div id="CustRatings" runat="server" class="col-sm-12">
                                <div>
                                    <h3 style="color: black;">Customer feedback for Ticket ID
                                        <asp:Label ID="RTID" runat="server"></asp:Label></h3>
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
                                                <Ajx:Rating runat="server" ID="Rating1" BehaviorID="RatingBehavior1" MaxRating="5" CurrentRating="3" CssClass="ratingStar" StarCssClass="ratingItem"
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
                                                <Ajx:Rating runat="server" ID="Rating2" BehaviorID="RatingBehavior2" MaxRating="5" CurrentRating="3" CssClass="ratingStar" StarCssClass="ratingItem"
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
                                                <b>ITCHamps Employee who solved your issue - How satisfied are you?</b>
                                            </div>

                                        </div>
                                        <div class="DivSpacer01"></div>
                                        <div class="DivSpacer01"></div>
                                        <div class="form-group">
                                            <div class="col-sm-6 htCrrating">
                                                <p>With his/her experience and knowledge for the job:</p>
                                            </div>
                                            <div class="col-sm-3">
                                                <Ajx:Rating runat="server" ID="Rating3" BehaviorID="RatingBehavior3" MaxRating="5" CurrentRating="3" CssClass="ratingStar" StarCssClass="ratingItem"
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
                                                <Ajx:Rating runat="server" ID="Rating4" BehaviorID="RatingBehavior4" MaxRating="5" CurrentRating="3" CssClass="ratingStar" StarCssClass="ratingItem"
                                                    WaitingStarCssClass="Saved" FilledStarCssClass="Filled" EmptyStarCssClass="Empty">
                                                </Ajx:Rating>
                                            </div>
                                        </div>
                                        <div class="DivSpacer01"></div>
                                        <div class="DivSpacer01"></div>
                                        <div class="form-group">
                                            <div class="col-sm-6 htCrrating">
                                                <p>How satisfied are you with Quality of Solution Provided:</p>
                                            </div>
                                            <div class="col-sm-3">
                                                <Ajx:Rating runat="server" ID="Rating5" BehaviorID="RatingBehavior5" MaxRating="5" CurrentRating="3" CssClass="ratingStar" StarCssClass="ratingItem"
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

                                                <asp:TextBox ID="txtsug" runat="server" TextMode="MultiLine"></asp:TextBox>



                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="DivSpacer01" style="padding-left: 20px">
                                <asp:Button ID="BtnRatingConfirm" runat="server" Text="Submit" OnClick="BtnRatingConfirm_Click" OnClientClick="HideModalPopupc()" />
                                <asp:Button ID="btnClose" runat="server" Text="Close" OnClientClick="return HideModalPopup()" />
                            </div>
                        </div>
                    </asp:Panel>
                </ContentTemplate>

                <Triggers>
                    <asp:PostBackTrigger ControlID="BtnRatingConfirm" />
                </Triggers>
            </asp:UpdatePanel>

        </div>
        <div runat="server" id="TaskOuterDiv">

            <%--<asp:Panel runat="server" ID="PnlTASK" ScrollBars="Vertical" Width="100%" Height="630px">--%>
            <table id="Table2" runat="server">
                <tr>
                    <td colspan="4">
                        <h2>Task </h2>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div class="DivSpacer01"></div>
                    </td>
                </tr>
            </table>
            <div runat="server" id="TaskDiv">
                <div id="divTaskLoad" runat="server">
                    <div>
                        <asp:Button ID="BtnTaskCreate" runat="server" Text="Create" OnClick="BtnTaskCreate_Click" TabIndex="26" />
                        <asp:Button ID="BtnTaskBAck" runat="server" Text="Back" OnClick="BtnTaskBAck_Click" TabIndex="27" />
                        <div class="DivSpacer01"></div>
                    </div>
                    <asp:Label ID="LblPopUp" runat="server" CssClass="lblMsg"></asp:Label><br />
                    <div class="respovrflw">
                        <asp:GridView ID="GV_Task" runat="server" AutoGenerateColumns="False" Width="100%" CssClass="Grid" GridLines="Both"
                            DataKeyNames="TICKETID,TASKID,TASKLINEID" OnRowCommand="GV_Task_RowCommand">
                            <Columns>
                                <asp:TemplateField HeaderText="Slno">
                                    <ItemTemplate>
                                        <asp:Label ID="LblSlno" runat="server" Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="TICKETID" HeaderText="Ticket ID" />
                                <asp:BoundField DataField="TASKLINEID" HeaderText="Task ID" />
                                <asp:BoundField DataField="TASKTITLE" HeaderText="Title" />
                                <%-- <asp:BoundField DataField="TASKAGENT" HeaderText="Agent" />--%>
                                <asp:TemplateField HeaderText="Assigned To">
                                    <ItemTemplate>
                                        <%#Eval("TASKAGENT") + " - " + Eval("AGENTNAME") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Agent">
                                    <ItemTemplate>
                                        <%#Eval("TASKACTUALAGENT") + " - " + Eval("TaskActAgentname") %>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Task Assigned By">
                                    <ItemTemplate>
                                        <%#  Eval("TASKCREATED_BY") + " - " + Eval("CREATEDONNAME") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="StatusTxt" HeaderText="Status" />
                                <asp:TemplateField HeaderText="Last Modified By">
                                    <ItemTemplate>
                                        <%#(Eval("TASKMODIFIED_BY")=="" ? Eval("TASKCREATED_BY") + " - " + Eval("CREATEDONNAME") : Eval("TASKMODIFIED_BY") + " - " + Eval("MODIFIEDONNAME"))%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Last Modified On">
                                    <ItemTemplate>
                                        <%#(Eval("TASKMODIFIED_ON","{0:dd-MM-yyyy}").ToString()=="01-01-1900") ? Eval("TASKCREATED_ON" ,"{0:dd-MMM-yyyy HH:mm:ss} ") : Eval("TASKMODIFIED_ON","{0:dd-MMM-yyyy HH:mm:ss} ")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LbtnTaskView" runat="server" CausesValidation="false" CommandName="VIEW"
                                            CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CssClass="Fnt02 linkbtn" Text="View"></asp:LinkButton>
                                        &nbsp;
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>


                <div id="TaskCreateUpdate" runat="server">

                    <%--<div class="DivSpacer01"></div>--%>
                    <div class="DivSpacer01">
                        <asp:LinkButton ID="LnkTaskBack" runat="server" CssClass="Fr paddingstyl linkbtn" CausesValidation="false" Text="Back" OnClick="LnkTaskBack_Click"></asp:LinkButton>&nbsp;&nbsp; 
                    </div>
                    <div id="TaskTicket" runat="server">

                        <div>
                            <asp:FormView runat="server" ID="FVTICKETTASK">
                                <ItemTemplate>
                                    <%--<div class="DivSpacer01"></div>--%>
                                    <div class="col-sm-12">
                                        <h4>Ticket details</h4>
                                        <div id="div1" class="col-sm-6">
                                            <div class="form-inline">
                                                <div class="form-group">
                                                    <div class="col-sm-4 htCr">Ticket ID <b>:</b></div>
                                                    <%--<td class="Td07"><b>:</b> </td>--%>
                                                    <div class="col-sm-4">
                                                        <%#(Eval("TID"))%>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="col-sm-4 htCr">Title <b>:</b></div>
                                                    <%--<td class="Td07"><b>:</b> </td>--%>
                                                    <div class="col-sm-4">
                                                        <%#(Eval("TITLE"))%>
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <div class="col-sm-4 htCr">Issue Description <b>:</b></div>
                                                    <%--<td class="Td07"><b>:</b> </td>--%>
                                                    <div class="col-sm-7">
                                                        <%#(Eval("ISSDESC"))%>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div id="div2" class="col-sm-6">
                                            <div class="form-inline">
                                                <div class="form-group">
                                                    <div class="col-sm-3 htCr">Client <b>:</b></div>
                                                    <%--<td class="Td07"><b>:</b> </td>--%>
                                                    <div class="col-sm-7">
                                                        <%#(Eval("CLIENT")) +"-"+ (Eval("CLIENTNNAME"))%>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="col-sm-3 htCr">Priority <b>:</b></div>
                                                    <%--<td class="Td07"><b>:</b> </td>--%>
                                                    <div class="col-sm-7">
                                                        <%#(Eval("PriorityTxt"))%>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="col-sm-3 htCr">Category <b>:</b></div>
                                                    <%--<td class="Td07"><b>:</b> </td>--%>
                                                    <div class="col-sm-7">
                                                        <%#(Eval("CategoryTxt"))%>
                                                    </div>
                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:FormView>
                            <div class="col-sm-12">
                                <Ajx:Accordion ID="Accordion1" runat="server" HeaderCssClass="accordionHeader"
                                    HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent" FadeTransitions="true" SuppressHeaderPostbacks="true" TransitionDuration="250" FramesPerSecond="40" RequireOpenedPane="false" AutoSize="None">
                                    <Panes>

                                        <Ajx:AccordionPane ID="AccordionPane8" runat="server">
                                            <Header><a href="#" class="href">Attachments</a></Header>
                                            <Content>
                                                <div class="respovrflw">
                                                    <asp:GridView ID="GrdTaskTickAtt" runat="server" AutoGenerateColumns="False" CssClass="Grid" GridLines="Both" PagerStyle-CssClass="cssPager" Width="99%"
                                                        OnRowCommand="GrdTaskTickAtt_RowCommand">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Slno">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="LblSlno" runat="server" Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="TID" HeaderText="Ticket ID" />
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Attachments
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="LinkButton1" runat="server" CssClass="linkbtn" Text='<%#Eval("ATTACHEMENT_FID") %>' Font-Bold="True" CommandName="download" CommandArgument='<%# Eval("ATTACHEMENT_FPATH") %>' CausesValidation="false" />
                                                                </ItemTemplate>
                                                                <ItemStyle Width="100" />
                                                            </asp:TemplateField>
                                                            <%--<asp:BoundField DataField="MODIFIED_BY" HeaderText="Uploaded By" />--%>
                                                            <asp:TemplateField HeaderText="Uploaded By">
                                                                <ItemTemplate>
                                                                    <%# Eval("MODIFIED_BY") + " - " + Eval("MODIFIEDONNAME")%>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="MODIFIED_ON" HeaderText="Uploaded On" DataFormatString="{0:dd-MMM-yyyy HH:mm:ss}" />
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </Content>
                                        </Ajx:AccordionPane>


                                    </Panes>
                                </Ajx:Accordion>
                            </div>
                        </div>
                    </div>
                    <div class="DivSpacer01">
                        <hr />
                    </div>
                    <div class="DivSpacer01"></div>
                    <div id="Taskfieldset" runat="server" class="form-group ">
                        <h4>Task details</h4>

                        <div id="divTable1" runat="server">
                            <div class="form-inline">
                                <%--<tr runat="server" id="TaskTicketID">
                            <td class="Td06">Ticket ID</td>
                            <td class="Td07"><b>:</b> </td>
                            <td class="Td10">
                                <asp:Label runat="server" ID="lblTaskTicketID"> </asp:Label></td>

                        </tr>--%>
                                <div runat="server" id="TaskID" class="form-group">
                                    <div class="col-sm-2 htCr">Task ID <b>:</b></div>
                                    <%--<td class="Td07"><b>:</b> </td>--%>
                                    <div class="col-sm-8">
                                        <asp:Label runat="server" ID="lblTaskID"></asp:Label>
                                    </div>

                                </div>


                                <div class="form-group">
                                    <div class="col-sm-2 htCr">Title <b>:</b></div>
                                    <%--<td class="Td07"><b>:</b> </td>--%>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="TxtTaskTitle" runat="server" MaxLength="200" CssClass="txtDropDownwidth" TabIndex="28"></asp:TextBox>

                                        <%--<td></td>--%>
                                        <asp:RequiredFieldValidator ID="RFV_TxtTaskTitle" runat="server" ControlToValidate="TxtTaskTitle"
                                            Display="Dynamic" ErrorMessage="Please Enter the Title" ForeColor="Red" ValidationGroup="VGTaskSubmit"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-sm-2 htCr">
                                        Issue Description <b>:</b>
                                    </div>
                                    <%--<td class="Td07"><b>:</b> </td>--%>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="TxtTaskIssDesc" runat="server" TextMode="MultiLine" CssClass="txtDropDownwidth" TabIndex="29"></asp:TextBox>

                                        <%--<td></td>--%>
                                        <asp:RequiredFieldValidator ID="RFV_TxtTaskIssDesc" runat="server" ControlToValidate="TxtTaskIssDesc"
                                            Display="Dynamic" ErrorMessage="Please Enter the Issue Desc" ForeColor="Red" ValidationGroup="VGTaskSubmit"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-sm-2 htCr">
                                        Assigned To <b>:</b>
                                    </div>
                                    <%--<td class="Td07"><b>:</b> </td>--%>
                                    <div class="col-sm-8">

                                        <asp:DropDownList ID="DDLTaskAssignee" runat="server" TabIndex="30" CssClass="txtDropDownwidth"></asp:DropDownList>
                                        <Ajx:ListSearchExtender ID="LSE_DDLTaskAssignee" TargetControlID="DDLTaskAssignee" PromptText="Search Assignee" PromptPosition="Top" QueryPattern="Contains" IsSorted="true" runat="server">
                                        </Ajx:ListSearchExtender>

                                        <%--<td></td>--%>
                                        <asp:RequiredFieldValidator ID="RFV_DDLTaskAssignee" runat="server" ControlToValidate="DDLTaskAssignee" InitialValue="0"
                                            Display="Dynamic" ErrorMessage="Please Select Agssignee" ForeColor="Red" ValidationGroup="VGTaskSubmit"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-sm-2 htCr">
                                        CC MailID's <b>:</b>
                                    </div>
                                    <%--<td class="Td07"><b>:</b> </td>--%>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="TXTTaskCCMAILID" runat="server" CssClass="txtDropDownwidth" TabIndex="31"></asp:TextBox>
                                        <Ajx:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="TXTTaskCCMAILID" MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="5" CompletionInterval="1" FirstRowSelected="True"
                                            ServiceMethod="GetEmployeeMailId" ServicePath="~/WebService/Service.asmx"
                                            CompletionListCssClass="completionList"
                                            CompletionListHighlightedItemCssClass="itemHighlighted"
                                            CompletionListItemCssClass="listItem" DelimiterCharacters=","
                                            ShowOnlyCurrentWordInCompletionListItem="true">
                                        </Ajx:AutoCompleteExtender>
                                        <span>(Separated by comma and no space)</span>
                                    </div>
                                    <%--<td></td>--%>
                                </div>
                                <%-- <tr>
                                    <td>
                                        <div class="DivSpacer01"></div>
                                    </td>
                                </tr>--%>

                                <div class="form-group">
                                    <div class="col-sm-2 htCr">
                                        Comments <b>:</b>
                                    </div>
                                    <%--<td class="Td07"><b>:</b> </td>--%>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="TxtTaskComments" runat="server" TextMode="MultiLine" CssClass="txtDropDownwidth" TabIndex="32"></asp:TextBox>
                                    </div>
                                    <%--<td></td>--%>
                                </div>
                                <div class="form-group">
                                    <div class="col-sm-2 htCr">
                                        Attachments <b>:</b>
                                    </div>
                                    <%--<td class="Td07"><b>:</b> </td>--%>
                                    <div class="col-sm-8">
                                        <asp:FileUpload ID="FU_TaskAttachments" runat="server" AllowMultiple="true" TabIndex="33" />



                                        <%--<td></td>--%>
                                        <asp:Label ID="fuAttachmentsfname" runat="server" TabIndex="34"></asp:Label>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <div class="col-sm-2 htCr">
                                        Status <b>:</b>
                                    </div>
                                    <%--<td class="Td07"><b>:</b> </td>--%>
                                    <div class="col-sm-8">
                                        <asp:DropDownList runat="server" ID="DDLTaskStatus" CssClass="txtDropDownwidth" TabIndex="35" AutoPostBack="true" OnSelectedIndexChanged="DDLTaskStatus_SelectedIndexChanged">
                                        </asp:DropDownList>

                                        <%--<td></td>--%>
                                        <asp:RequiredFieldValidator ID="RFV_DDLTaskStatus" runat="server" ControlToValidate="DDLTaskStatus" InitialValue="0"
                                            Display="Dynamic" ErrorMessage="Please select Status" ForeColor="Red" ValidationGroup="VGTaskSubmit"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div id="TrTaskPlnhrs" runat="server" class="form-group">
                                    <div class="col-sm-2 htCr">Planned Hours <b>:</b></div>
                                    <%--<td class="Td07"><b>:</b> </td>--%>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="TxtTrTaskPlnhrs" runat="server" CssClass="txtDropDownwidth" TabIndex="36"></asp:TextBox>

                                        <%--<td> </td>--%>
                                        <Ajx:FilteredTextBoxExtender ID="FTBETrTaskPlnhrs" runat="server" TargetControlID="TxtTrTaskPlnhrs"
                                            FilterType="Numbers,Custom" ValidChars=".">
                                        </Ajx:FilteredTextBoxExtender>
                                        <asp:RequiredFieldValidator ID="RF_TxtTrTaskPlnhrs" runat="server" ForeColor="Red" ErrorMessage="Please Enter Planned Hour..!" ValidationGroup="VGTaskSubmit" SetFocusOnError="true"
                                            Display="Dynamic" ControlToValidate="TxtTrTaskPlnhrs"></asp:RequiredFieldValidator>
                                        <asp:RangeValidator ID="RV_TxtTrTaskPlnhrs" runat="server" ErrorMessage="Invalid Entry..!" ForeColor="Red"
                                            ValidationGroup="VGTaskSubmit" SetFocusOnError="true" MaximumValue="999" MinimumValue="0.1" Type="Double"
                                            Display="Dynamic" ControlToValidate="TxtTrTaskPlnhrs"></asp:RangeValidator>
                                    </div>
                                </div>
                                <div id="TrTaskActhrs" runat="server">
                                    <div class="form-group">
                                        <div class="col-sm-2 htCr">Actual Hours <b>:</b></div>
                                        <%--<td class="Td07"><b>:</b> </td>--%>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="TxtTrTaskActhrs" runat="server" CssClass="txtDropDownwidth" TabIndex="37"></asp:TextBox>

                                            <%--<td></td>--%>
                                            <Ajx:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="TxtTrTaskActhrs"
                                                FilterType="Numbers,Custom" ValidChars=".">
                                            </Ajx:FilteredTextBoxExtender>
                                        </div>
                                    </div>
                                    <%--</td>--%>
                                    <%--  <tr>
                                    <td>
                                        <div class="DivSpacer01"></div>
                                    </td>
                                </tr>--%>
                                    <div class="col-sm-8">
                                        <div class="btn-group-sm">

                                            <asp:Button ID="TASKBack" Width="70px" Text="Back" runat="server" OnClick="TASKBack_Click" CausesValidation="false" TabIndex="38" />
                                            <asp:Button ID="TASKADD" Width="70px" Text="Add" runat="server" OnClick="TASKADD_Click" ValidationGroup="VGTaskSubmit" CausesValidation="true" TabIndex="39" />
                                            <asp:Button ID="TaskUpdate" Width="70px" Text="Update" runat="server" OnClick="TaskUpdate_Click" ValidationGroup="VGTaskSubmit" CausesValidation="true" TabIndex="40" />
                                            <asp:Button ID="TaskEdit" Width="70px" Text="Edit" runat="server" OnClick="TaskEdit_Click" TabIndex="41" />
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="DivSpacer01"></div>
                            <Ajx:Accordion ID="AccordionTask" runat="server" HeaderCssClass="accordionHeader"
                                HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent" FadeTransitions="true" SuppressHeaderPostbacks="true" TransitionDuration="250" FramesPerSecond="40" RequireOpenedPane="false" AutoSize="None">
                                <Panes>


                                    <Ajx:AccordionPane ID="AccordionPane4" runat="server">
                                        <Header><a href="#" class="href">Comments</a></Header>
                                        <Content>
                                            <div class="respovrflw">

                                                <asp:GridView ID="Grd_TaskComments" runat="server" AutoGenerateColumns="False" CssClass="Grid" GridLines="Both" PagerStyle-CssClass="cssPager" Width="99%">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Slno">
                                                            <ItemTemplate>
                                                                <asp:Label ID="LblSlno" runat="server" Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="TICKETID" HeaderText="Ticket ID" />
                                                        <asp:BoundField DataField="TASKLINEID" HeaderText="Task ID" />
                                                        <asp:BoundField DataField="TASKCCOMMENTS" HeaderText="Comments" />
                                                        <%--<asp:BoundField DataField="MODIFIED_BY" HeaderText="Commented By" />--%>
                                                        <asp:TemplateField HeaderText="Commented By">
                                                            <ItemTemplate>
                                                                <%#Eval("MODIFIED_BY") + " - " + Eval("MODIFIEDONNAME") %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="MODIFIED_ON" HeaderText="Commented On" DataFormatString="{0:dd-MMM-yyyy HH:mm:ss}" />

                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </Content>
                                    </Ajx:AccordionPane>

                                    <Ajx:AccordionPane ID="AccordionPane5" runat="server">
                                        <Header><a href="#" class="href">Attachments</a></Header>
                                        <Content>
                                            <div class="respovrflw">
                                                <asp:GridView ID="Grd_TaskAttachments" runat="server" AutoGenerateColumns="False" CssClass="Grid" GridLines="Both" PagerStyle-CssClass="cssPager" Width="99%" OnRowCommand="Grd_TaskAttachments_RowCommand">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Slno">
                                                            <ItemTemplate>
                                                                <asp:Label ID="LblSlno" runat="server" Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="TICKETID" HeaderText="Ticket ID" />
                                                        <asp:BoundField DataField="TASKLINEID" HeaderText="Task ID" />
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Attachments
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="LinkButton1" runat="server" Text='<%#Eval("TASKATTACHEMENT_FID") %>' Font-Bold="True" CssClass="linkbtn" CommandName="download" CommandArgument='<%# Eval("TASKATTACHEMENT_FPATH") %>' CausesValidation="false" />
                                                            </ItemTemplate>
                                                            <ItemStyle Width="100" />
                                                        </asp:TemplateField>
                                                        <%-- <asp:BoundField DataField="MODIFIED_BY" HeaderText="Uploaded By" />--%>
                                                        <asp:TemplateField HeaderText="Uploaded By">
                                                            <ItemTemplate>
                                                                <%#Eval("MODIFIED_BY") + " - " + Eval("MODIFIEDONNAME") %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="MODIFIED_ON" HeaderText="Uploaded On" DataFormatString="{0:dd-MMM-yyyy HH:mm:ss}" />
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </Content>
                                    </Ajx:AccordionPane>
                                    <Ajx:AccordionPane ID="AccordionPane6" runat="server">
                                        <Header><a href="#" class="href">Status</a> </Header>
                                        <Content>
                                            <div class="respovrflw">
                                                <asp:GridView ID="Grd_TaskStatus" runat="server" AutoGenerateColumns="False" CssClass="Grid" GridLines="Both" PagerStyle-CssClass="cssPager" Width="99%">
                                                    <Columns>

                                                        <asp:TemplateField HeaderText="Slno">
                                                            <ItemTemplate>
                                                                <asp:Label ID="LblSlno" runat="server" Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="TICKETID" HeaderText="Ticket ID" />
                                                        <asp:BoundField DataField="TASKLINEID" HeaderText="Task ID" />
                                                        <asp:TemplateField HeaderText="From User">
                                                            <ItemTemplate>
                                                                <%#Eval("FRMASSIGNEE")  + "-"+Eval("FRMASSIGNEENNAME")%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="To User">
                                                            <ItemTemplate>
                                                                <%#Eval("TOASSIGNEE")  + "-"+Eval("TOASSIGNEENNAME") %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="From Status">
                                                            <ItemTemplate>
                                                                <%#Eval("FRMSTATUSTXT") %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="To Status">
                                                            <ItemTemplate>
                                                                <%#Eval("TOSTATUSTXT") %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <%-- <asp:BoundField DataField="MODIFIED_BY" HeaderText="By" />--%>
                                                        <asp:TemplateField HeaderText="By">
                                                            <ItemTemplate>
                                                                <%#Eval("MODIFIED_BY") + " - " + Eval("MODIFIEDONNAME") %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="MODIFIED_ON" HeaderText="On" DataFormatString="{0:dd-MMM-yyyy HH:mm:ss}" />
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </Content>
                                    </Ajx:AccordionPane>
                                </Panes>
                            </Ajx:Accordion>
                        </div>
                    </div>
                </div>


            </div>
            <%--  </asp:Panel>--%>
        </div>

    </div>

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
</asp:Content>
