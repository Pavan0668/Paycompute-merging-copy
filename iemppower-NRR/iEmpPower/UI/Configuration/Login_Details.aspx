<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"  Inherits="UI_Configuration_Login_Details"  theme="SkinFile" Codebehind="Login_Details.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <div>
     <h2>Employee Login Details</h2>
 <asp:Label ID="lblMessageBoard" runat="server" CssClass="msgboard" meta:resourcekey="lblMessageBoardResource1"></asp:Label><br />
    <div class="gridview">
    <br />
    <asp:GridView ID="grdLastLogin" runat="server" AutoGenerateColumns="False" onpageindexchanging="grdLastLogin_PageIndexChanging" AllowPaging="False" Width="100%"  >
        <Columns>
        <asp:BoundField DataField="USER_NAME" HeaderText="ID"/>
        <asp:BoundField DataField="NAME" HeaderText="Name" />
        <asp:BoundField DataField="LASTLOGINDATE" HeaderText="Last Login" />
        </Columns>        
    </asp:GridView>
    </div>
    </div>
</asp:Content>

