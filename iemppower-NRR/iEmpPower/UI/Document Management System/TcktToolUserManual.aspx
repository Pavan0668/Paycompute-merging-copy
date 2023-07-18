<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TcktToolUserManual.aspx.cs"
    Inherits="iEmpPower.UI.Document_Management_System.TcktToolUserManual" EnableEventValidation="false" UICulture="en-GB" Theme="SkinFile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="header">
        <div class="row clearfix">
            <div class="col-xs-12 col-sm-6">

                <span class="HeadFontSize">&nbsp;Ticketing Tool User Manual</span><br />

            </div>
        </div>

        <asp:Label ID="lblMessageBoard" runat="server" CssClass="lblMsg" meta:resourcekey="lblMessageBoardResource1"></asp:Label>
    </div>

    <div>

        <div class="body">


            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <iframe id="ifrm" name="ifrm" runat="server" class="frame" scrolling="auto" frameborder="0" marginheight="0" marginwidth="0" visible="False"></iframe>

                </ContentTemplate>
            </asp:UpdatePanel>

        </div>
    </div>
</asp:Content>
