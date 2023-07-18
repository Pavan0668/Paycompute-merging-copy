<%@ Page Title="" Language="C#" MasterPageFile="~/UI/SubSiteMaster.master" AutoEventWireup="true" CodeBehind="UserManual.aspx.cs"
    Inherits="iEmpPower.UI.Document_Management_System.UserManual" Culture="auto"
    EnableEventValidation="false" UICulture="en-GB" Theme="SkinFile" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
   <div class="row">
        <div class="col-12">
            <div class="page-title-box">
                <div class="page-title-right">
                    <ol class="breadcrumb m-0">
                        <li class="breadcrumb-item"><a href="../Default.aspx">Home</a></li>
                        <li class="breadcrumb-item active">IEmpPower Paycompute User Manual</li>
                    </ol>
                </div>
                <h4 class="page-title">IEmpPower Paycompute User Manual
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
                <div class="col-sm-12 row" style="height:800px;margin-left:50px;margin-top:20px">
             <iframe id="ifrm" name="ifrm" runat="server" class="frame" scrolling="auto" width="90%" height="95%" frameborder="0" marginheight="0" marginwidth="0" visible="False"></iframe>
                    </div>
        </div>
    </div>

</asp:Content>
