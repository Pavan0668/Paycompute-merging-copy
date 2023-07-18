<%@ Page Title="" Language="C#" MasterPageFile="~/UI/SubSiteMaster.master" AutoEventWireup="true" CodeBehind="Statutory.aspx.cs" MaintainScrollPositionOnPostback="true"
    Inherits="iEmpPower.UI.Payroll_Management.Statutory" Culture="en-GB" UICulture="auto" Theme="SkinFile" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function clearDirty() {
            document.body.onbeforeunload = "";
            document.getElementById("DirtyLabel").className = "hide";
        }


        document.body.onclick = setControlChange;
        document.body.onkeyup = setControlChange;
    </script>
    <span class="hidden">
                <asp:Button ID="btnEntryKey" runat="server" Text="" /></span>
            <div id="DirtyLabel" style="color: Red;" class="hide"></div>
            <span class="hidden">
                <asp:Button ID="Button1" runat="server" Text="" /></span>

     <div class="row">
        <div class="col-12">
            <div class="page-title-box">
                <div class="page-title-right">
                    <ol class="breadcrumb m-0">
                        <li class="breadcrumb-item"><a href="../Default.aspx">Home</a></li>
                        <li class="breadcrumb-item active">Statutory Reports</li>
                    </ol>
                </div>
                <h4 class="page-title">Statutory Reports
                   <asp:Label ID="Label1" runat="server" CssClass="msgboard"></asp:Label>
                </h4>
            </div>
        </div>
    </div>

    <div class="header">
        <asp:Label ID="LblMsg" runat="server" CssClass="lblValidation"></asp:Label>
    </div>

             <div class=" card-box">
            <div id="real_time_chart" class="dashboard-flot-chart">
               
                            <asp:Label ID="lblMessageBoard" runat="server" CssClass="lblMsg"></asp:Label><br />

                            <div>
                 <div class="row">
                    <div class="col-sm-2 htCr"> Select Type of Report</div>
                     <div class="col-sm-6">
                                <asp:DropDownList ID="ddlReportType" runat="server" CssClass="txtDropDownwidth">
                                <asp:ListItem>ESI</asp:ListItem>
                                <asp:ListItem>PF</asp:ListItem>
                                <asp:ListItem>PT</asp:ListItem>
                            </asp:DropDownList>
                            </div>
                           </div>
                    <br />
                    <div class="row">
                    <div class="col-sm-2 htCr">Year</div>
                        <div class="col-sm-6">
                        <asp:DropDownList ID="drpdwnYear" runat="server" CssClass="txtDropDownwidth">
                                <asp:ListItem Value="1" meta:resourcekey="ListItemResource1">2010</asp:ListItem>
                                <asp:ListItem Value="2" meta:resourcekey="ListItemResource2">2011</asp:ListItem>
                                <asp:ListItem Value="3" meta:resourcekey="ListItemResource3">2012</asp:ListItem>
                                <asp:ListItem Value="4">2013</asp:ListItem>
                                <asp:ListItem Value="5">2014</asp:ListItem>
                                <asp:ListItem Value="6">2015</asp:ListItem>
                                <asp:ListItem Value="7">2016</asp:ListItem>
                                <asp:ListItem Value="8">2017</asp:ListItem>
                                <asp:ListItem Value="9">2018</asp:ListItem>
                                <asp:ListItem Value="10">2019</asp:ListItem>
                                <asp:ListItem Value="11">2020</asp:ListItem>
                                <asp:ListItem Value="12">2021</asp:ListItem>
                                <asp:ListItem Value="13">2022</asp:ListItem>
                                <asp:ListItem Value="14">2023</asp:ListItem>
                                <asp:ListItem Value="15">2024</asp:ListItem>
                                <asp:ListItem Value="16">2025</asp:ListItem>
                                <asp:ListItem Value="17">2026</asp:ListItem>
                                <asp:ListItem Value="18">2027</asp:ListItem>
                                <asp:ListItem Value="19">2028</asp:ListItem>
                                <asp:ListItem Value="20">2029</asp:ListItem>
                                <asp:ListItem Value="21">2030</asp:ListItem>
                            </asp:DropDownList>
                          </div>
                          </div>

                    <br />
                    <div class="row">
                    <div class="col-sm-2 htCr">Month</div>
                        <div class="col-sm-6">
                            <asp:DropDownList ID="drpdwnMonth" runat="server" CssClass="txtDropDownwidth">
                                <asp:ListItem Value="01" meta:resourcekey="ListItemResource4">January</asp:ListItem>
                                <asp:ListItem Value="02" meta:resourcekey="ListItemResource5">February</asp:ListItem>
                                <asp:ListItem Value="03" meta:resourcekey="ListItemResource6">March</asp:ListItem>
                                <asp:ListItem Value="04" meta:resourcekey="ListItemResource7">April</asp:ListItem>
                                <asp:ListItem Value="05" meta:resourcekey="ListItemResource8">May</asp:ListItem>
                                <asp:ListItem Value="06" meta:resourcekey="ListItemResource9">June</asp:ListItem>
                                <asp:ListItem Value="07" meta:resourcekey="ListItemResource10">July</asp:ListItem>
                                <asp:ListItem Value="08" meta:resourcekey="ListItemResource11">August</asp:ListItem>
                                <asp:ListItem Value="09" meta:resourcekey="ListItemResource12">September</asp:ListItem>
                                <asp:ListItem Value="10" meta:resourcekey="ListItemResource13">October</asp:ListItem>
                                <asp:ListItem Value="11" meta:resourcekey="ListItemResource14">November</asp:ListItem>
                                <asp:ListItem Value="12" meta:resourcekey="ListItemResource15">December</asp:ListItem>
                            </asp:DropDownList>
                              </div>
                        </div>
                    <br />
                    <br />
                 <div class="row">
                    <div class="col-sm-12 htCr">
                                <asp:Button ID="btnGeneratePaySlip" runat="server" Text="View Report" OnClientClick="clearDirty();"
                                    OnClick="btnGeneratePaySlip_Click" meta:resourcekey="btnGeneratePaySlipResource1" />
                                <%--<asp:Button ID="btnPrint" runat="server" Text="Print" OnClientClick="return CallPrint(ifrm);" />--%>
                            </div>
                       </div>
                   
                <div class="col-sm-12 row" style="height:800px;margin-top:35px">
                    <iframe id="ifrm" name="ifrm" runat="server" width="90%" height="95%" scrolling="no" frameborder="0" marginheight="0" marginwidth="0" visible="False"></iframe>
               </div>
        </div>
      </div>
    
 </div>

</asp:Content>
