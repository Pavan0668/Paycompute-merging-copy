<%@ Page Title="" Language="C#" MasterPageFile="~/UI/SubSiteMaster.master" AutoEventWireup="true" CodeBehind="Payslip_Emp.aspx.cs" MaintainScrollPositionOnPostback="true"
    Inherits="iEmpPower.UI.Payroll_Management.Payslip_Emp" 
    Culture="en-GB" UICulture="auto" Theme="SkinFile" EnableEventValidation="false"%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajx" %>

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
                        <li class="breadcrumb-item active">Payslip</li>
                    </ol>
                </div>
                <h4 class="page-title">Payslip
                    <asp:Label ID="Label1" runat="server" CssClass="msgboard"></asp:Label>
                </h4>
            </div>
        </div>
    </div>
    <div class="header">
        <asp:Label ID="LblMsg" runat="server" CssClass="lblValidation"></asp:Label>
    </div>

    <div class="card-box">
        <div id="real_time_chart" class="dashboard-flot-chart">

            <div id="divbrdr" class="divfr">
                <div class="search-section">

                        <div>

                            <asp:Label ID="lblMessageBoard" runat="server" CssClass="lblMsg" meta:resourcekey="lblMessageBoardResource1"></asp:Label><br />

                            <div>

                                <div class="form-inline">
                                    <div class="form-group">
                                        
                                        <div class="col-sm-2 htCr"> Year &nbsp;<b>:</b></div>
                                        <div class="col-sm-3">
                               
                                    <asp:DropDownList ID="drpdwnYear" runat="server" CssClass="txtDropDownwidth" TabIndex="1">
                                    </asp:DropDownList>
                                   <%-- <Ajx:CascadingDropDown ID="CCD_drpdwnYear" runat="server" ServicePath="~/WebService/Service.asmx" ServiceMethod="GetYearPayslip" Category="QYear"
                                        UseContextKey="false" TargetControlID="drpdwnYear">
                                    </Ajx:CascadingDropDown>--%>
                                </div>
                                        <div class="col-sm-2 htCr">Month &nbsp;<b>:</b></div>
                                        <div class="col-sm-3">
                               
                                    <asp:DropDownList ID="drpdwnMonth" runat="server" CssClass="txtDropDownwidth" TabIndex="2"
                                        meta:resourcekey="drpdwnMonthResource1">
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
                                 <div class="col-sm-2" style="width: 90px">
                                     <asp:Button ID="btnGeneratePaySlip" runat="server" Text="View Payslip" OnClientClick="clearDirty();" OnClick="btnGeneratePaySlip_Click" />
                                <%--<asp:Button ID="btnPrint" runat="server" Text="Print" OnClientClick="return CallPrint(ifrm);" />--%>
                             </div>
                            </div>
                        </div>
                       
                         <div class="col-sm-12 row" style="height:800px;margin-top:35px">
                          <iframe id="ifrm" name="ifrm" runat="server" class="frame" scrolling="auto" width="90%" height="95%" frameborder="0" marginheight="0" marginwidth="0" visible="False"></iframe>
                               </div>     
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

</asp:Content>
