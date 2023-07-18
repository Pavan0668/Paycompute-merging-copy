<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Kpi_Docs.aspx.cs" Inherits="iEmpPower.UI.Document_Management_System.Kpi_Docs"
    EnableEventValidation="false" UICulture="en-GB" Theme="SkinFile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


    <div class="header">
        <div class="row clearfix">
            <div class="col-xs-12 col-sm-6">

                <span class="HeadFontSize">&nbsp;KPI Document</span><br />

            </div>
        </div>


    </div>

    <div>
        <div class="body">
            <asp:Label ID="lblMessageBoard" runat="server" CssClass="lblMsg" meta:resourcekey="lblMessageBoardResource1"></asp:Label>
            <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>--%>
            <div class="row">
                <div class="col-md-2 htCr">Select Employee : </div>
                <div class="col-md-6">
                    <asp:DropDownList ID="ddlEmp" CssClass="txtDropDownwidth" AutoPostBack="true" OnSelectedIndexChanged="ddlEmp_SelectedIndexChanged" runat="server"></asp:DropDownList>
                </div>
            </div>


            <iframe id="ifrm" name="ifrm" runat="server" class="frame" scrolling="auto" frameborder="0" marginheight="0" marginwidth="0" visible="False"></iframe>
            <%--</ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="ddlEmp" />
                </Triggers>
            </asp:UpdatePanel>--%>
        </div>


      
    </div>

</asp:Content>
