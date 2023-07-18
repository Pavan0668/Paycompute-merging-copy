<%@ Page Title="Directory" Language="C#" MasterPageFile="~/UI/SubSiteMaster.master" AutoEventWireup="true" EnableEventValidation="false"
    Inherits="UI_Manager_Self_Service_who_swho" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" Theme="SkinFile" CodeBehind="who'swho.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <link rel="stylesheet" href="https://bootswatch.com/slate/bootstrap.min.css">
    <link href="../../ProgressBar/plugin.css" rel="stylesheet" />
    <link href="http://www.jqueryscript.net/css/jquerysctipttop.css" rel="stylesheet" type="text/css">



    <div class="row">
        <div class="col-12">
            <div class="page-title-box">
                <div class="page-title-right">
                    <ol class="breadcrumb m-0">
                        <li class="breadcrumb-item"><a href="../Default.aspx">Home</a></li>
                        <li class="breadcrumb-item active">Directory</li>
                    </ol>
                </div>
                <h4 class="page-title">Directory
                    <asp:Label ID="Label1" runat="server" CssClass="msgboard"></asp:Label>
                </h4>
            </div>
        </div>
    </div>

    <div class=" card-box">
        <div id="real_time_chart" class="dashboard-flot-chart">

            <div class="row">
                    <asp:Label ID="lblMessageBoard" runat="server" CssClass="lblMsg"></asp:Label><br />
               
                
                <div class="">
                    <div class=" col-sm-offset-8" style="visibility: hidden">
                        <div class="row">
                            <div class="col-sm-3 ">
                                <div id="divprog1" class="img my-progress-bar"></div>
                            </div>
                            <div class="col-sm-3 ">
                                <div id="divprog2" class="img1 my-progress-bar"></div>
                            </div>
                            <div class="col-sm-3 ">
                                <div id="divprog3" class="img2 my-progress-bar"></div>
                            </div>
                        </div>
                    </div>
                    <script src="../../ProgressBar/plugin.js"></script>
                    <script src="../../ProgressBar/script.js"></script>
                    <script type="text/javascript">
                        var _gaq = _gaq || [];
                        _gaq.push(['_setAccount', 'UA-36251023-1']);
                        _gaq.push(['_setDomainName', 'jqueryscript.net']);
                        _gaq.push(['_trackPageview']);

                        (function () {
                            var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
                            ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';
                            var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);
                        })();
                    </script>
                </div>
            </div>
             <asp:UpdatePanel ID="b" runat="server">
              <ContentTemplate>
            <div class="form-group">
                <div class="row">
                    <div class="col-md-2">
                        <asp:TextBox ID="txtEmployeNmae" runat="server" CssClass="txtDropDownwidth" Width="100%" placeholder="Enter Employee Name"
                            meta:resourcekey="txtEmployeNmaeResource1" TabIndex="1"></asp:TextBox>
                    </div>
                   <%-- <div class="col-md-1 htCr" style="width: 105px; display: none;">
                        Designation<b>:</b>
                    </div>
                    <div class="col-md-2">
                        <asp:TextBox ID="txtDesignation" Visible="false" runat="server" CssClass="txtDropDownwidth" placeholder="Enter Designation"
                            TabIndex="2"></asp:TextBox>
                    </div>--%>
                    <div class="col-md-3">
                        <asp:Button ID="btnSearch" runat="server" Width="80px" Text="Search" OnClick="btnSearch_Click" meta:resourcekey="btnSearchResource1" TabIndex="3" />
                   
                        <asp:Button ID="btnReset" runat="server" Width="80px" Text="Reset" OnClick="btnReset_Click" TabIndex="4" />
                   
                        <asp:Button ID="BtnExporttoXl" runat="server" Width="80px" Text="Export" OnClick="BtnExporttoXl_Click" CausesValidation="false" TabIndex="5" />
                    </div>

                    <div class="col-md-7 text-right" id="divcnt" runat="server"></div>
                </div>
            </div>
                   </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnSearch" />
                      <asp:PostBackTrigger ControlID="btnReset" />
                      <asp:PostBackTrigger ControlID="BtnExporttoXl" />
                </Triggers>
                </asp:UpdatePanel>

            <div class="respovrflw">               
                <asp:GridView ID="grdWhoIsWho" runat="server" AutoGenerateColumns="False"
                    AllowPaging="True" OnPageIndexChanging="grdWhoIsWho_PageIndexChanging"
                    meta:resourcekey="grdWhoIsWhoResource1" Width="100%" OnRowDataBound="grdWhoIsWho_RowDataBound">
                    <Columns>
                         <asp:TemplateField HeaderText="Sl No.">
                            <ItemTemplate>
                                <asp:Label ID="lblRowNumber" runat="server" Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Photo" meta:resourcekey="TemplateFieldResource1">
                            <ItemTemplate>
                                <asp:Image ID="Image1" runat="server"
                                    ImageUrl='<%# Eval("EMPLOYEE_PHOTO_PATH") %>' alt="Image"
                                    Height="35px" Width="35px" class="rounded-circle setdatainput" />
                            </ItemTemplate>
                            <ItemStyle Wrap="False" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="PERNR" HeaderText="Emp ID" />
                        <asp:BoundField DataField="EMPLOYEE_ID" HeaderText="Emp Name" />
                        <asp:BoundField DataField="JOININGDATE" HeaderText="DOJ" />

                        <asp:BoundField DataField="DESIGNATION" HeaderText="Designation"
                            meta:resourcekey="BoundFieldResource2" />
                        <asp:BoundField DataField="EMAIL" HeaderText="Email ID" />
                        <asp:BoundField DataField="ALTEMAILID" HeaderText="Alternate Email ID" Visible="false" />
                        <asp:BoundField DataField="MPHN" HeaderText="Mobile No" Visible="false" />

                        <asp:BoundField DataField="MNGRNAME" HeaderText="Reporting Manager" />
                        <asp:BoundField DataField="DEPDETAILS" HeaderText="Department" />
                        <asp:BoundField DataField="MODULE" HeaderText="Module" />
                        <asp:BoundField DataField="PERAREADESC" HeaderText="Personal Area" />





                    </Columns>
                    <RowStyle CssClass="ROWHGHT" />
                </asp:GridView>
            </div>
           
               
            <div class="DivSpacer01"></div>
            <div class="DivSpacer01"></div>

            <asp:HiddenField ID="HFM" runat="server" />
            <asp:HiddenField ID="HFF" runat="server" />
        </div>
    </div>
</asp:Content>

