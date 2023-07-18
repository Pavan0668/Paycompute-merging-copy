<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Policys.aspx.cs" Inherits="iEmpPower.UI.Document_Management_System.Policys" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="header">
        <div class="row clearfix">
            <div class="col-xs-12 col-sm-6">
                <span class="HeadFontSize">&nbsp;Work from Home Policy</span><br />
            </div>
        </div>
    </div>

    <div>
        <div class="body">
            <asp:Label ID="lblMessageBoard" runat="server" CssClass="lblMsg" meta:resourcekey="lblMessageBoardResource1"></asp:Label><br />
            <iframe id="ifrm" name="ifrm" runat="server" class="frame" scrolling="auto" frameborder="0" marginheight="0" marginwidth="0" visible="true" src="~/UI/Document Management System/Documents/Work from Home Policy @ ITChamps.pdf"></iframe>
        </div>
    </div>
</asp:Content>
