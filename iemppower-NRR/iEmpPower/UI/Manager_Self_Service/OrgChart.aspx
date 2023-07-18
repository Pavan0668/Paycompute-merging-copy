<%@ Page Title="Org Chart" Language="C#" MasterPageFile="~/UI/SubSiteMaster.master" AutoEventWireup="true" CodeBehind="OrgChart.aspx.cs" Inherits="iEmpPower.UI.Manager_Self_Service.OrgChart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <link href="../../getorgchart/getorgchart.css" rel="stylesheet" />
    <script src="../../getorgchart/getorgchart.js"></script>
    <style type="text/css">
        html, body
        {
            margin: 0px;
            padding: 0px;
            width: 100%;
            height: 100%;
            overflow: hidden;
        }

        #people
        {
            width: 100%;
            height: 100%;
        }
       
    </style>

    <div class="row">
        <div class="col-12">
            <div class="page-title-box">
                <div class="page-title-right">
                    <ol class="breadcrumb m-0">
                        <li class="breadcrumb-item"><a href="../Default.aspx">Home</a></li>
                        <li class="breadcrumb-item active">Org Chart</li>
                    </ol>
                </div>
                <h5 class="page-title">Organization Chart
                  
                </h5>
            </div>
        </div>
    </div>
   

    <div style="display:none">
        <asp:GridView ID="gv_chartData" runat="server" AutoGenerateColumns="false">
            <Columns>
                <asp:BoundField DataField="PERNR" HeaderText="PERNR" />
                <asp:BoundField DataField="Manager" HeaderText="Manager" />
                <asp:BoundField DataField="ENAME" HeaderText="ENAME" />
                <asp:BoundField DataField="DESIG" HeaderText="DESIG" />
                <asp:BoundField DataField="Image" HeaderText="Image" />
               
            </Columns>
        </asp:GridView>
    </div>
    <div id="people" style="height: 70vh"></div>

    <script type="text/javascript">
        function onload() {
            session
            Storage.setItem("themes", "ula");
            sessionStorage.setItem("org", "getOrgChart.RO_TOP");
        }
        window.onload = onload;

        function theme(xyz) {
            sessionStorage.setItem("themes", xyz.value);
            document.location.reload();
        }

        function org(xyz) {
            sessionStorage.setItem("org", xyz.value);
            document.location.reload();
        }

        var x = sessionStorage.getItem("themes") == null ? "ula" : sessionStorage.getItem("themes");
        var y = eval(sessionStorage.getItem("org") == null ? "getOrgChart.RO_TOP" : sessionStorage.getItem("org"));
        var t = sessionStorage.getItem("org") == null ? "getOrgChart.RO_TOP" : sessionStorage.getItem("org");

        var peopleElement = document.getElementById("people");
        var orgChart = new getOrgChart(peopleElement, {

            linkType: "M",
            layout: getOrgChart.MIXED_HIERARCHY_RIGHT_LINKS,
            expandTolevel: 1,
            primaryFields: ["ENAME", "DESIG", "PERNR"],
            photoFields: ["Image"],
            enableGridView: false,

            enableSearch: true,
            enableEdit: false,
            enableDetailsView: false,
            orientation: y,
            theme: x,
            dataSource: document.getElementById("<%=gv_chartData.ClientID%>"),
            customize: {
                "itch1001": { color: "teal" }
               
            }

        });


        document.getElementById("drpdwlsttheme").value = x;
        document.getElementById("drpdwnlstortn").value = t;
    </script>




</asp:Content>
