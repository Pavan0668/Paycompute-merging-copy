<%@ Page Title="Ticketing Tool Dashboard" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TicketingTDashBoard.aspx.cs"
    EnableEventValidation="false" Culture="en-GB" Theme="SkinFile" Inherits="iEmpPower.UI.Ticketing_Tool.TicketingTDashBoard" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajx" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

    <style type="text/css">
        .chartsz
        {
            width: 100% !important;
            height: 92% !important;
        }

        #MainContent_GrdMandteDocs
        {
            border-width: 0 !important;
            border-style: none !important;
        }

        .bkgrdclr
        {
            background-color: #09576c;
        }

        .c14
        {
            background-color: #B03A2E !important;
            color: #fff !important;
        }

        .c15
        {
            background-color: #f06291 !important;
            color: #fff !important;
        }

        .c16
        {
            background-color: #656464 !important;
            color: #fff !important;
        }

        .c17
        {
            background-color: #9A7D0A !important;
            color: #fff !important;
        }

        .c18
        {
            background-color: #ff6347 !important;
            color: #fff !important;
        }

        .chrt_backgrd
        {
            box-shadow: 0 2px 10px rgba(0, 0, 0, 0.2);
            height: auto;
            width: auto;
            display: flex;
            cursor: default;
            background-color: #fff;
            position: relative;
            overflow: hidden;
            margin: 15px 0;
        }

            .chrt_backgrd .content .text
            {
                font-size: 15px;
                margin-top: 11px;
                color: #555;
            }

        .style_prevu_kit
        {
            display: inline-block;
            border: 0;
            width: auto;
            height: auto;
            position: relative;
            -webkit-transition: all 200ms ease-in;
            -webkit-transform: scale(1);
            -ms-transition: all 200ms ease-in;
            -ms-transform: scale(1);
            -moz-transition: all 200ms ease-in;
            -moz-transform: scale(1);
            transition: all 200ms ease-in;
            transform: scale(1);
        }

            .style_prevu_kit:hover
            {
                box-shadow: 0px 0px 10px #000;
                z-index: 121;
                -webkit-transition: all 200ms ease-in;
                -webkit-transform: scale(1.5);
                -ms-transition: all 200ms ease-in;
                -ms-transform: scale(1.5);
                -moz-transition: all 200ms ease-in;
                -moz-transform: scale(1.5);
                transition: all 200ms ease-in;
                transform: scale(1.5);
            }


        .TextBx
        {
            box-sizing: border-box;
            border: 2px solid #ccc;
            -webkit-transition: 0.5s;
            transition: 0.5s;
            outline: none;
            min-width: 80px;
            max-width: 200px;
            padding: 5px;
        }

            .TextBx:hover
            {
                border: 2px solid #00617C;
            }

            .TextBx:focus
            {
                border: 2px solid #00617C;
            }

        .modal-content
        {
            /*display:none;*/
            z-index: -1;
            background-color: rgba(0,97,124,0.8);
            /*margin: 15% auto;*/ /* 15% from the top and centered */
            padding: 18px;
            border: 2px solid #888;
            width: 25%; /*Could be more or less, depending on screen size*/
        }

        .hidden
        {
            visibility: hidden;
        }

        .modalBackground
        {
            /*visibility:hidden;
            position:absolute;
            top:25vh;*/
            background-color: black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div style="margin-left: 12px;">
        <div class="row " style="padding-top: 10px; padding-bottom: 0; margin-bottom: 0;">
            <asp:Panel ID="pnlIssue" runat="server">
                <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                    <%-- <a href="../../UI/Ticketing Tool/CreateIssueTicket.aspx?Type=New">--%>
                    <asp:LinkButton ID="lnk" OnClick="Button1_Click" runat="server">
                        <div class="info-box hover-zoom-effect PointerHover" style="margin-bottom: 10px !important;">
                            <div class="icon c15">
                                <i class="material-icons">report_problem</i>
                            </div>
                            <div class="content">
                                <div class="text">Issue Ticket</div>
                                <%-- <a href="../../UI/Working_Time/leaverequest_new.aspx">--%>
                                <div class="number count-to" data-from="0" data-to="125" data-speed="1000" data-fresh-interval="20">Raise</div>
                                <%--  </a>--%>
                            </div>

                        </div></asp:LinkButton>
                    <%--</a>--%>
                </div>
            </asp:Panel>

            <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                <a href="../../UI/Ticketing Tool/IssueTracker.aspx">
                    <div class="info-box hover-zoom-effect PointerHover" style="margin-bottom: 10px !important;">
                        <div class="icon c16">
                            <i class="material-icons">find_in_page</i>
                        </div>
                        <div class="content">
                            <div class="text">Raised Tickets</div>
                            <div class="numberPending" id="TTMYQUEUE" runat="server"></div>
                            <div class="numberPending" id="TTMYQUEUETask" runat="server"></div>
                            <%-- <a href="../../UI/Working_Time/leaverequest_new.aspx">--%>
                            <%--<div class="number count-to" data-from="0" data-to="125" data-speed="1000" data-fresh-interval="20">Check</div>--%>
                            <%--  </a>--%>
                        </div>
                    </div>
                </a>
            </div>
            <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                <a href="../../UI/Ticketing Tool/TicketingToolReport.aspx">
                    <div class="info-box hover-zoom-effect PointerHover" style="margin-bottom: 10px !important;">
                        <div class="icon bg-blue">
                            <i class="material-icons"> description</i>
                        </div>
                        <div class="content">
                            <div class="text">Ticket Reports</div>
                            <%-- <a href="../../UI/Working_Time/leaverequest_new.aspx">--%>
                            <div class="number count-to" data-from="0" data-to="125" data-speed="1000" data-fresh-interval="20">Export</div>
                            <%--  </a>--%>
                        </div>

                    </div>
                </a>
            </div>
            <asp:Panel runat="server" ID="pnlTiManDocs">
                <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                    <a id="mandateDocs" runat="server">
                        <div class="info-box hover-zoom-effect PointerHover" style="margin-bottom: 10px !important;">
                            <div class="icon c17">
                                <i class="material-icons">assignment_late</i>
                            </div>
                            <div class="content">
                                <div class="text">Mandatory</div>
                                <%-- <a href="../../UI/Working_Time/leaverequest_new.aspx">--%>
                                <div class="number count-to" data-from="0" data-to="125" data-speed="1000" data-fresh-interval="20">Templates</div>
                                <%--  </a>--%>
                            </div>

                        </div>
                    </a>
                    <Ajx:ModalPopupExtender ID="mp1" runat="server" PopupControlID="pnlMandateDocs" TargetControlID="mandateDocs"
                        BackgroundCssClass="modalBackground" CancelControlID="btnADvanceClose">
                    </Ajx:ModalPopupExtender>
                    <%-- <Ajx:HoverMenuExtender ID="HoverMenuExtender1" runat="server" TargetControlID="mandateDocs"
                    PopupControlID="pnlMandateDocs" PopupPosition="Left" HoverCssClass="modalBackground">
                </Ajx:HoverMenuExtender>--%>
                </div>
            </asp:Panel>

            <asp:Panel runat="server" ID="pnlUSerManual">
                <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                    <a href="../../UI/Document Management System/TcktToolUserManual.aspx">
                        <div class="info-box hover-zoom-effect PointerHover" style="margin-bottom: 10px !important;">
                            <div class="icon bg-yellow">
                                <i class="material-icons">import_contacts</i>
                            </div>
                            <div class="content">
                                <div class="text">User Manual</div>
                                <%-- <a href="../../UI/Working_Time/leaverequest_new.aspx">--%>
                                <div class="number count-to" data-from="0" data-to="125" data-speed="1000" data-fresh-interval="20">View</div>
                                <%--  </a>--%>
                            </div>

                        </div>
                    </a>
                    <Ajx:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="pnlMandateDocs" TargetControlID="mandateDocs"
                        BackgroundCssClass="modalBackground" CancelControlID="btnADvanceClose">
                    </Ajx:ModalPopupExtender>
                    <%-- <Ajx:HoverMenuExtender ID="HoverMenuExtender1" runat="server" TargetControlID="mandateDocs"
                    PopupControlID="pnlMandateDocs" PopupPosition="Left" HoverCssClass="modalBackground">
                </Ajx:HoverMenuExtender>--%>
                </div>
            </asp:Panel>
        </div>

        <%--   <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>--%>



        <div class="row " style="padding-top: 10px; padding-bottom: 0; margin-bottom: 0;">
            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="">
                <div class="info-box" style="margin-bottom: 10px !important; margin-right: 10px !important; height: auto;">
                    <div class="content">
                        <div class="" style="color: #555;">
                            <div class="form-inline">
                                <div class="form-group">
                                    <%--<div class="col-sm-1" style="width: 75px;">
                                        <h5>Select </h5>
                                    </div>--%>
                                    <div class="col-sm-2" style="width: 115px;">
                                        <h5>Select From : </h5>
                                    </div>
                                    <div class="col-sm-3" style="width: 210px;">
                                        <asp:TextBox ID="txtTTFromDate" AutoPostBack="true" CssClass="TextBx" runat="server" OnTextChanged="txtTTFromDate_TextChanged"></asp:TextBox>
                                        <Ajx:CalendarExtender ID="CE_TxtFromDate" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                            TargetControlID="txtTTFromDate">
                                        </Ajx:CalendarExtender>
                                        <Ajx:MaskedEditExtender ID="MEE_TxtFromDate" runat="server" AcceptNegative="Left"
                                            CultureName="en-GB" DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999"
                                            MaskType="Date" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                                            OnInvalidCssClass="MaskedEditError" TargetControlID="txtTTFromDate" />
                                    </div>
                                    <div class="col-sm-1" style="width: 55px;">
                                        <h5>To : </h5>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtTTToDate" AutoPostBack="true" CssClass="TextBx" runat="server" OnTextChanged="txtTTToDate_TextChanged"></asp:TextBox>
                                        <Ajx:CalendarExtender ID="CE_TxtToDate" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                            TargetControlID="txtTTToDate">
                                        </Ajx:CalendarExtender>
                                        <Ajx:MaskedEditExtender ID="MEE_TxtToDate" runat="server" AcceptNegative="Left"
                                            CultureName="en-GB" DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999"
                                            MaskType="Date" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                                            OnInvalidCssClass="MaskedEditError" TargetControlID="txtTTToDate" />
                                        <%--  <asp:CompareValidator ID="CV_TxtToDate" runat="server" ControlToCompare="txtTTFromDate" CssClass="lblValidation"
                                                ControlToValidate="txtTTToDate" Display="Dynamic" ErrorMessage="From date should be less than to date"
                                                Operator="GreaterThanEqual" Type="Date" ValidationGroup="" ForeColor="Red"></asp:CompareValidator>--%>
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="block-header" style="margin-top: 15px">
            <h2><b>ASSIGNED TO ME</b></h2>
        </div>
        <div class="">
            <div class="row clearfix ">
                <a>
                    <div class="style_prevu_kit col-lg-4 col-md-4 col-sm-8 col-xs-12 ">
                        <div class="chrt_backgrd ">
                            <div class="content ">
                                <div class="text font-bold">Ticket Status</div>
                                <asp:Chart runat="server" ID="ctl00" OnClick="Chart1_Click" CssClass="chartsz">
                                    <%--<Size ></Size>--%>
                                    <Titles>
                                        <asp:Title ShadowOffset="3" Name="Items" />
                                    </Titles>
                                    <%--<Legends>
        <asp:Legend Alignment="Center" Docking="Left" IsTextAutoFit="true" Name="Default"
            LegendStyle="Column" />
    </Legends>--%>
                                    <Series>
                                        <asp:Series Name="" />
                                    </Series>

                                    <ChartAreas>
                                        <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
                                    </ChartAreas>
                                </asp:Chart>
                            </div>
                        </div>
                    </div>
                </a>
                <%-- </div>

     <div class="row clearfix">--%>


                <a>
                    <div class="style_prevu_kit col-lg-4 col-md-4 col-sm-8 col-xs-12">
                        <div class="chrt_backgrd">
                            <div class="content ">
                                <div class="text font-bold">Priority Based Tickets</div>
                                <asp:Chart runat="server" ID="ctpriority" CssClass="chartsz" OnClick="ctpriority_Click">
                                    <%--<Size ></Size>--%>
                                    <Titles>
                                        <asp:Title ShadowOffset="3" Name="Items" />
                                    </Titles>
                                    <%--<Legends>
        <asp:Legend Alignment="Center" Docking="Left" IsTextAutoFit="true" Name="Default"
            LegendStyle="Column" />
    </Legends>--%>

                                    <Series>
                                        <asp:Series Name="" />
                                    </Series>

                                    <ChartAreas>
                                        <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
                                    </ChartAreas>
                                </asp:Chart>
                            </div>
                        </div>
                    </div>
                </a>

                <a>
                    <div class="style_prevu_kit col-lg-4 col-md-4 col-sm-8 col-xs-12">
                        <div class="chrt_backgrd">
                            <div class="content ">
                                <div class="text font-bold">Tickets In Progress</div>
                                <asp:Chart runat="server" ID="Cttckprogress" CssClass="chartsz">
                                    <%--<Size ></Size>--%>
                                    <Titles>
                                        <asp:Title ShadowOffset="3" Name="Items" />
                                    </Titles>
                                    <%--<Legends>
        <asp:Legend Alignment="Center" Docking="Left" IsTextAutoFit="true" Name="Default"
            LegendStyle="Column" />
    </Legends>--%>

                                    <Series>
                                        <asp:Series Name="" />
                                    </Series>

                                    <ChartAreas>
                                        <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
                                    </ChartAreas>
                                </asp:Chart>
                            </div>
                        </div>
                    </div>
                </a>

            </div>
            <asp:Panel ID="pnlclient" runat="server">
                <div class="row clearfix">
                    <a>
                        <div class="style_prevu_kit col-lg-4 col-md-4 col-sm-8 col-xs-12">
                            <div class="chrt_backgrd">
                                <div class="content ">
                                    <div class="text font-bold">Request Type Tickets</div>
                                    <asp:Chart runat="server" ID="ctreqType" CssClass="chartsz" OnClick="ctreqType_Click">
                                        <%--<Size ></Size>--%>
                                        <Titles>
                                            <asp:Title ShadowOffset="3" Name="Items" />
                                        </Titles>
                                        <%--<Legends>
        <asp:Legend Alignment="Center" Docking="Left" IsTextAutoFit="true" Name="Default"
            LegendStyle="Column" />
    </Legends>--%>

                                        <Series>
                                            <asp:Series Name="" />
                                        </Series>

                                        <ChartAreas>
                                            <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
                                        </ChartAreas>
                                    </asp:Chart>
                                </div>
                            </div>
                        </div>
                    </a>
                    <%-- </div>

    <div class="row clearfix">--%>



                    <a>
                        <div class="style_prevu_kit col-lg-4 col-md-4 col-sm-8 col-xs-12 ">
                            <div class="chrt_backgrd">
                                <div class="content ">
                                    <div class="text font-bold">Clients Raised Tickets</div>
                                    <asp:Chart runat="server" ID="ctclients" CssClass="chartsz" OnClick="ctclients_Click">
                                        <%--<Size ></Size>--%>
                                        <Titles>
                                            <asp:Title ShadowOffset="3" Name="Items" />
                                        </Titles>
                                        <%--<Legends>
        <asp:Legend Alignment="Center" Docking="Left" IsTextAutoFit="true" Name="Default"
            LegendStyle="Column" />
    </Legends>--%>
                                        <Series>
                                            <asp:Series Name="" />
                                        </Series>

                                        <ChartAreas>
                                            <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
                                        </ChartAreas>
                                    </asp:Chart>
                                </div>
                            </div>
                        </div>
                    </a>

                    <a>
                        <div class="style_prevu_kit col-lg-4 col-md-4 col-sm-8 col-xs-12 ">
                            <div class="chrt_backgrd">
                                <div class="content ">
                                    <div class="text font-bold">Issue Type Tickets</div>
                                    <asp:Chart runat="server" ID="ctcissueType" CssClass="chartsz" OnClick="ctcissueType_Click">
                                        <%--<Size ></Size>--%>
                                        <Titles>
                                            <asp:Title ShadowOffset="3" Name="Items" />
                                        </Titles>
                                        <%--  <Legends>
        <asp:Legend Alignment="Center" Docking="Left" IsTextAutoFit="true" Name="Default"
            LegendStyle="Column" />
    </Legends>--%>
                                        <Series>
                                            <asp:Series Name="" />
                                        </Series>

                                        <ChartAreas>

                                            <asp:ChartArea Name="ChartArea1">
                                            </asp:ChartArea>
                                        </ChartAreas>
                                    </asp:Chart>
                                </div>
                            </div>
                        </div>
                    </a>
                    <asp:Panel ID="pnlmytckts" runat="server">
                        <a>
                            <div class="style_prevu_kit col-lg-4 col-md-4 col-sm-8 col-xs-12">
                                <div class="chrt_backgrd">
                                    <div class="content ">
                                        <div class="text font-bold">My Tickets</div>
                                        <asp:Chart runat="server" ID="ctagent" CssClass="chartsz" OnClick="ctagent_Click">
                                            <%--<Size ></Size>--%>
                                            <Titles>
                                                <asp:Title ShadowOffset="3" Name="Items" />
                                            </Titles>
                                            <%--<Legends>
        <asp:Legend Alignment="Center" Docking="Left" IsTextAutoFit="true" Name="Default"
            LegendStyle="Column" />
    </Legends>--%>

                                            <Series>
                                                <asp:Series Name="" />
                                            </Series>

                                            <ChartAreas>
                                                <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
                                            </ChartAreas>
                                        </asp:Chart>
                                    </div>
                                </div>
                            </div>
                        </a>
                    </asp:Panel>
                </div>
            </asp:Panel>

            <asp:Panel ID="pnlAsMSS" runat="server">

                <div class="block-header" style="margin-top: 15px">
                    <h2><b>
                        <asp:Label ID="lblheadassgTeam" runat="server"></asp:Label></b></h2>
                </div>
                <div class="row clearfix ">
                    <a>
                        <div class="style_prevu_kit col-lg-4 col-md-4 col-sm-8 col-xs-12">
                            <div class="chrt_backgrd">
                                <div class="content ">
                                    <div class="text font-bold">Ticket Status</div>
                                    <asp:Chart runat="server" ID="ctMSSTckStats" OnClick="ctMSSTckStats_Click" CssClass="chartsz">
                                        <%--<Size ></Size>--%>
                                        <Titles>
                                            <asp:Title ShadowOffset="3" Name="Items" />
                                        </Titles>
                                        <%--<Legends>
        <asp:Legend Alignment="Center" Docking="Left" IsTextAutoFit="true" Name="Default"
            LegendStyle="Column" />
    </Legends>--%>
                                        <Series>
                                            <asp:Series Name="" />
                                        </Series>

                                        <ChartAreas>
                                            <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
                                        </ChartAreas>
                                    </asp:Chart>
                                </div>
                            </div>
                        </div>
                    </a>
                    <%-- </div>

     <div class="row clearfix">--%>


                    <a>
                        <div class="style_prevu_kit col-lg-4 col-md-4 col-sm-8 col-xs-12">
                            <div class="chrt_backgrd">
                                <div class="content ">
                                    <div class="text font-bold">Priority Based Tickets</div>
                                    <asp:Chart runat="server" ID="ctMSStckPrio" CssClass="chartsz" OnClick="ctMSStckPrio_Click">
                                        <%--<Size ></Size>--%>
                                        <Titles>
                                            <asp:Title ShadowOffset="3" Name="Items" />
                                        </Titles>
                                        <%--<Legends>
        <asp:Legend Alignment="Center" Docking="Left" IsTextAutoFit="true" Name="Default"
            LegendStyle="Column" />
    </Legends>--%>

                                        <Series>
                                            <asp:Series Name="" />
                                        </Series>

                                        <ChartAreas>
                                            <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
                                        </ChartAreas>
                                    </asp:Chart>
                                </div>
                            </div>
                        </div>
                    </a>

                    <a>
                        <div class="style_prevu_kit col-lg-4 col-md-4 col-sm-8 col-xs-12">
                            <div class="chrt_backgrd">
                                <div class="content ">
                                    <div class="text font-bold">Tickets In Progress</div>
                                    <asp:Chart runat="server" ID="ctMSStckProg" CssClass="chartsz">
                                        <%--<Size ></Size>--%>
                                        <Titles>
                                            <asp:Title ShadowOffset="3" Name="Items" />
                                        </Titles>
                                        <%--<Legends>
        <asp:Legend Alignment="Center" Docking="Left" IsTextAutoFit="true" Name="Default"
            LegendStyle="Column" />
    </Legends>--%>

                                        <Series>
                                            <asp:Series Name="" />
                                        </Series>

                                        <ChartAreas>
                                            <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
                                        </ChartAreas>
                                    </asp:Chart>
                                </div>
                            </div>
                        </div>
                    </a>
                </div>
                <div class="row clearfix">
                    <a>
                        <div class="style_prevu_kit col-lg-4 col-md-4 col-sm-8 col-xs-12">
                            <div class="chrt_backgrd">
                                <div class="content ">
                                    <div class="text font-bold">Request Type Tickets</div>
                                    <asp:Chart runat="server" ID="ctMSSreqtytcks" CssClass="chartsz" OnClick="ctMSSreqtytcks_Click">
                                        <%--<Size ></Size>--%>
                                        <Titles>
                                            <asp:Title ShadowOffset="3" Name="Items" />
                                        </Titles>
                                        <%--<Legends>
        <asp:Legend Alignment="Center" Docking="Left" IsTextAutoFit="true" Name="Default"
            LegendStyle="Column" />
    </Legends>--%>

                                        <Series>
                                            <asp:Series Name="" />
                                        </Series>

                                        <ChartAreas>
                                            <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
                                        </ChartAreas>
                                    </asp:Chart>
                                </div>
                            </div>
                        </div>
                    </a>
                    <%-- </div>

    <div class="row clearfix">--%>


                    <a>
                        <div class="style_prevu_kit col-lg-4 col-md-4 col-sm-8 col-xs-12 ">
                            <div class="chrt_backgrd">
                                <div class="content ">
                                    <div class="text font-bold">Clients Tickets Assigned</div>
                                    <asp:Chart runat="server" ID="ctMSSClintRTcks" CssClass="chartsz" OnClick="ctMSSClintRTcks_Click">
                                        <%--<Size ></Size>--%>
                                        <Titles>
                                            <asp:Title ShadowOffset="3" Name="Items" />
                                        </Titles>
                                        <%--<Legends>
        <asp:Legend Alignment="Center" Docking="Left" IsTextAutoFit="true" Name="Default"
            LegendStyle="Column" />
    </Legends>--%>
                                        <Series>
                                            <asp:Series Name="" />
                                        </Series>

                                        <ChartAreas>
                                            <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
                                        </ChartAreas>
                                    </asp:Chart>
                                </div>
                            </div>
                        </div>
                    </a>

                    <a>
                        <div class="style_prevu_kit col-lg-4 col-md-4 col-sm-8 col-xs-12 ">
                            <div class="chrt_backgrd">
                                <div class="content ">
                                    <div class="text font-bold">Issue Type Tickets</div>
                                    <asp:Chart runat="server" ID="ctMssSubIssueType" CssClass="chartsz" OnClick="ctMssSubIssueType_Click">
                                        <%--<Size ></Size>--%>
                                        <Titles>
                                            <asp:Title ShadowOffset="3" Name="Items" />
                                        </Titles>
                                        <%--<Legends>
        <asp:Legend Alignment="Center" Docking="Left" IsTextAutoFit="true" Name="Default"
            LegendStyle="Column" />
    </Legends>--%>
                                        <Series>
                                            <asp:Series Name="" />
                                        </Series>

                                        <ChartAreas>
                                            <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
                                        </ChartAreas>
                                    </asp:Chart>
                                </div>
                            </div>
                        </div>
                    </a>
                    <a>
                        <div class="style_prevu_kit col-lg-4 col-md-4 col-sm-8 col-xs-12">
                            <div class="chrt_backgrd">
                                <div class="content ">
                                    <div class="text font-bold">
                                        <asp:Label ID="lblIntnlTck" runat="server" Text=""></asp:Label>
                                    </div>
                                    <asp:Chart runat="server" ID="ctMSSSubAgent" CssClass="chartsz" OnClick="ctMSSSubAgent_Click">

                                        <Titles>
                                            <asp:Title ShadowOffset="3" Name="Items" />
                                        </Titles>


                                        <Series>
                                            <asp:Series Name="" />
                                        </Series>

                                        <ChartAreas>
                                            <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
                                        </ChartAreas>
                                    </asp:Chart>
                                </div>
                            </div>
                        </div>
                    </a>

                </div>
            </asp:Panel>

        </div>
        <%--   </ContentTemplate>
            <Triggers >
                <asp:AsyncPostBackTrigger ControlID="ctl00" />
                 <asp:AsyncPostBackTrigger ControlID="ctpriority" />
                 <asp:AsyncPostBackTrigger ControlID="ctclients" />
                 <asp:AsyncPostBackTrigger ControlID="ctagent" />
                 <asp:AsyncPostBackTrigger ControlID="ctreqType" />
                 <asp:AsyncPostBackTrigger ControlID="Cttckprogress" />
                 <asp:AsyncPostBackTrigger ControlID="ctMSStckProg" />
                 <asp:AsyncPostBackTrigger ControlID="ctMSSTckStats" />
                 <asp:AsyncPostBackTrigger ControlID="ctMSStckPrio" />
                 <asp:AsyncPostBackTrigger ControlID="ctMSSClintRTcks" />
                 <asp:AsyncPostBackTrigger ControlID="ctMSSreqtytcks" />
                 <asp:AsyncPostBackTrigger ControlID="ctMSSSubAgent" />
            </Triggers>
        </asp:UpdatePanel>--%>
    </div>
    <asp:Panel ID="pnlMandateDocs" runat="server" CssClass="modal-content">
        <div class="Fr">
            <asp:Button ID="btnADvanceClose" runat="server" Text=" &times; " />
        </div>
        <h5 style="color: white"><b>Mandate Document Templates</b></h5>
        <div class="DivSpacer03"></div>
        <asp:GridView ID="GrdMandteDocs" GridLines="None" runat="server" Width="100%" ShowHeader="false" ShowFooter="false" AutoGenerateColumns="false" OnRowCommand="GrdMandteDocs_RowCommand" CssClass="bkgrdclr">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <ul style="color: white">
                            <li>
                                <asp:LinkButton ID="LinkButton1" runat="server" Text='<%#Eval("FileName") %>' Font-Bold="True" ForeColor="White" CommandName="download" CommandArgument='<%# Eval("FileURL") %>' CausesValidation="false" />
                            </li>
                        </ul>

                    </ItemTemplate>
                    <ItemStyle CssClass="bkgrdclr" />
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <%-- <ul style="color:white">
            <li>
                <asp:LinkButton ID="hypDocFS" ForeColor="White" runat="server" OnClick="hypDocFS_Click">Functional Specification</asp:LinkButton></li>
            <li>
                <asp:LinkButton ID="hypDocTS" ForeColor="White" runat="server" OnClick="hypDocTS_Click">Technical Specification</asp:LinkButton></li>
            <li>
                <asp:LinkButton ID="hypCodeRw" ForeColor="White" runat="server" OnClick="hypCodeRw_Click">Code Review Check List</asp:LinkButton></li>
            <li>
                <asp:LinkButton ID="hypTstD" ForeColor="White" runat="server" OnClick="hypTstD_Click">Code Review Test Defect Log</asp:LinkButton></li>
         <li>
                <asp:LinkButton ID="hypTRF" ForeColor="White" runat="server" OnClick="hypTRF_Click">TR Form</asp:LinkButton></li>
          
        
        </ul>--%>
    </asp:Panel>
</asp:Content>
