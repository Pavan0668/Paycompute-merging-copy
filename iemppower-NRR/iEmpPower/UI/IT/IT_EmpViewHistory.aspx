<%@ Page Title="" Language="C#" MasterPageFile="~/UI/SubSiteMaster.master" AutoEventWireup="true" CodeBehind="IT_EmpViewHistory.aspx.cs"
    Inherits="iEmpPower.UI.IT.IT_EmpViewHistory" Theme="SkinFile" EnableEventValidation="false" Culture="en-GB"
    MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%--<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

    <style type="text/css">
        .Initial {
            display: inline;
            padding: 7px 16px;
            margin: 0;
            margin-right: 3px;
            border-radius: 3px 3px 0 0;
            outline: none;
            color: black;
        }

        .Clicked {
            display: inline;
            padding: 7px 16px;
            margin: 0;
            margin-right: 3px;
            border-radius: 3px 3px 0 0;
            outline: none;
            background: white;
            color: black;
        }
    </style>
</asp:Content>--%>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


    <h2>Income Tax History </h2>
    <%--from April 1<sup>st</sup> <asp:Label ID="LblFromDate" runat="server"></asp:Label> To March 31<sup>st</sup> <asp:Label ID="LblToDate" runat="server"></asp:Label></h2>
    --%>

    <div style="width: 99%" class="cb">
        <asp:Button Text="Section 80" BorderStyle="None" ID="Tab1" CssClass="Initial" runat="server"
            OnClick="Tab1_Click" />
        <asp:Button Text="Section 80C" BorderStyle="None" ID="Tab2" CssClass="Initial" runat="server"
            OnClick="Tab2_Click" />
        <asp:Button Text="Housing" BorderStyle="None" ID="Tab3" CssClass="Initial" runat="server"
            OnClick="Tab3_Click" />
        <asp:Button Text="Other Sources" BorderStyle="None" ID="Tab4" CssClass="Initial" runat="server"
            OnClick="Tab4_Click" />

    </div>

     <div style="width:70%; float: left">
            <fieldset style="float: left"">
                <legend><b>&nbsp;Search&nbsp;</b></legend>
                <table>

                    <tr>
                        <td>&nbsp;Search&nbsp;
            </td>
                        <td>
                            <asp:DropDownList ID="ddlSeachSelect" runat="server" CssClass="textbox" TabIndex="1">
                                <asp:ListItem Text="-SELECT-" Value="0"></asp:ListItem>
                                <asp:ListItem Text="IT ID" Value="3"></asp:ListItem>
                                <asp:ListItem Text="Status" Value="4"></asp:ListItem>
                            </asp:DropDownList>
                        </td>


                        <td>
                            <asp:TextBox ID="txtsearch" runat="server" CssClass="textbox" placeholder="Enter Text" TabIndex="2"></asp:TextBox>
                        </td></tr>
                        <tr>
                            <td>&nbsp;From&nbsp;
            </td>
            <td>
                <asp:TextBox ID="txtFromDate" runat="server" TabIndex="3"  placeholder="Select From Date" Width="199px" Style="letter-spacing: 1px; background: #ffffff url('../../images/CalenderIMG.png') no-repeat 99% 55% !important; border: 1px solid #666666; margin-bottom: 4px; margin-left: 2px; padding: 2px;"></asp:TextBox>
                <cc1:MaskedEditExtender ID="MEE_txtFromDate" runat="server" AcceptNegative="Left"
                    CultureName="en-GB" DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999"
                    MaskType="Date" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                    OnInvalidCssClass="MaskedEditError" TargetControlID="txtFromDate" />
                <cc1:CalendarExtender ID="CE_txtFromDate" runat="server" Enabled="True" Format="dd/MM/yyyy"
                    TargetControlID="txtFromDate">
                </cc1:CalendarExtender>
            </td>
                             <td>&nbsp;To&nbsp;&nbsp;
           <%-- </td>
            <td>--%>
                <asp:TextBox ID="txtTodate" runat="server" TabIndex="4"  placeholder="Select To Date" Width="199px" Style="letter-spacing: 1px; background: #ffffff url('../../images/CalenderIMG.png') no-repeat 99% 55% !important; border: 1px solid #666666; margin-bottom: 4px; margin-left: 2px; padding: 2px;"></asp:TextBox>
                <cc1:MaskedEditExtender ID="MEE_txtTodate" runat="server" AcceptNegative="Left"
                    CultureName="en-GB" DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999"
                    MaskType="Date" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                    OnInvalidCssClass="MaskedEditError" TargetControlID="txtTodate" />
                <cc1:CalendarExtender ID="CE_txtTodate" runat="server" Enabled="True" Format="dd/MM/yyyy"
                    TargetControlID="txtTodate">
                </cc1:CalendarExtender>
            </td>
                        <td>
                            <asp:Button ID="btnsearch" runat="server" CausesValidation="false" OnClick="btnsearch_Click" TabIndex="5" Text="Search" />
                            &nbsp;&nbsp;
                            <asp:Button ID="btnclear" runat="server" OnClick="btnclear_Click" TabIndex="5" Text="Clear" />
                            <asp:HiddenField ID="HFTabID" runat="server"/>
                        </td>



                    </tr>
                </table>
            </fieldset>
            </div>
    
    <div class="DivSpacer01"></div>

    <div class="cb">
        <asp:MultiView ID="MainView" runat="server">
            <asp:View ID="View1" runat="server">


                <asp:Label ID="lblMessageBoard" runat="server" CssClass="msgboard"></asp:Label>

                <asp:GridView ID="GVSec80Header" runat="server"  CssClass="gridviewNew" GridLines="None" AutoGenerateColumns="False" BorderStyle="None" DataKeyNames="ID,BEGDA,ENDDA,CREATED_ON,CONACTPROP,APPROVEDON,REMARKS,STATUS"
                    OnRowCommand="GVSec80Header_RowCommand" AllowPaging="true" PageSize="10" OnPageIndexChanging="GVSec80Header_PageIndexChanging"
                    AllowSorting="true" OnSorting="GVSec80Header_Sorting" TabIndex="7" >
                    <Columns>
                        <asp:TemplateField HeaderText="No." HeaderStyle-Width="20%">
                            <ItemTemplate>
                                <%# Container.DataItemIndex+1 %>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="ID" DataField="ID" SortExpression="ID" HeaderStyle-Width="20%"></asp:BoundField>
                        <asp:TemplateField HeaderText="IT Type" HeaderStyle-Width="30%">
                            <ItemTemplate>
                                Section 80 
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="BEGDA" HeaderText="From Date" DataFormatString="{0:dd-MMM-yyyy}" SortExpression="BEGDA" HeaderStyle-Width="30%"/>
                        <asp:BoundField DataField="ENDDA" HeaderText="To Date" DataFormatString="{0:dd-MMM-yyyy}" SortExpression="ENDDA" HeaderStyle-Width="30%"/>
                       <%-- <asp:BoundField DataField="CONACTPROP" HeaderText="Consider Actuals" />--%>

                          <asp:TemplateField HeaderText="Consider Actuals" HeaderStyle-Width="10%">
                            <ItemTemplate>

                                <%#(Eval("CONACTPROP").ToString().Trim()=="0") ? "No" : (Eval("CONACTPROP").ToString().Trim()=="1")? "Yes": "-"%>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:BoundField DataField="CREATED_ON" HeaderText="Created On" DataFormatString="{0:dd-MMM-yyyy}" SortExpression="CREATED_ON" HeaderStyle-Width="30%"/>
                        <asp:BoundField DataField="APPROVEDON" HeaderText="Approved On" DataFormatString="{0:dd-MMM-yyyy}" SortExpression="APPROVEDON" HeaderStyle-Width="30%"/>
                        <asp:BoundField DataField="REMARKS" HeaderText="Remarks" SortExpression="REMARKS" HeaderStyle-Width="10%"/>

                        <asp:BoundField HeaderText="Status" DataField="STATUS" SortExpression="STATUS" HeaderStyle-Width="10%"></asp:BoundField>

                        <asp:TemplateField HeaderStyle-Width="10%">
                            <ItemTemplate>
                                <asp:LinkButton ID="LbtnSec80View" runat="server" CausesValidation="False" CommandName="View" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'>View</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>

                </asp:GridView>

                <br />
                <asp:GridView ID="GVITSec80" runat="server" AutoGenerateColumns="False" BorderStyle="None" Width="99%" Visible="false">
                    <Columns>
                        <%--//select t8.SBSEC,t8.SBDIV,t8.SBDDS, t9.SDVLT,t9.TXEXM--%>
                        <%-- <asp:TemplateField HeaderText="No.">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex+1 %>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"/>
                    </asp:TemplateField>--%>
                        <asp:BoundField HeaderText="ID" DataField="ID">
                            <ItemStyle HorizontalAlign="Center" Width="50px" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Contribution" DataField="SBDDS">
                            <ItemStyle HorizontalAlign="left" Width="450px" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Limit" DataField="SDVLT">
                            <ItemStyle HorizontalAlign="right" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Tax EXEM%" DataField="TXEXM">
                            <ItemStyle HorizontalAlign="right" />
                        </asp:BoundField>

                        <asp:BoundField HeaderText="Prop. Contr. (INR)" DataField="PROPCONTR">
                            <ItemStyle HorizontalAlign="right" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Act. Contr. (INR)" DataField="ACTCONTR">
                            <ItemStyle HorizontalAlign="right" />
                        </asp:BoundField>


                        <asp:BoundField HeaderText="Curr" DataField="CURR" Visible="false">
                            <ItemStyle HorizontalAlign="right" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Attachments" DataField="RECEIPT_FID">
                            <ItemStyle HorizontalAlign="left" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Comments" DataField="EMPCOMMENTS">
                            <ItemStyle HorizontalAlign="left" />
                        </asp:BoundField>

                    </Columns>

                </asp:GridView>
        <div><br /></div>
                        <div id="ExportbtnSec80" runat="server" style="margin-top: 25px;">
                                <asp:Button ID="BtnExporttoXlSec80" runat="server" CausesValidation="false" OnClick="BtnExporttoXlSec80_Click" TabIndex="8" Text="Export To Excel" />
                                &nbsp;&nbsp;
                                <asp:Button ID="BtnExporttoPDFSec80" runat="server" OnClick="BtnExporttoPDFSec80_Click" TabIndex="9" Text="Export To PDF" />
                        </div>

  
    <asp:HiddenField ID="viewcheckSec80" runat="server"/>

            </asp:View>

            <asp:View ID="View2" runat="server">
                <asp:Label ID="LblSec80c" runat="server" CssClass="msgboard"></asp:Label>
                <asp:GridView ID="GVSec80CHeader" runat="server" AutoGenerateColumns="False" BorderStyle="None" DataKeyNames="ID,BEGDA,ENDDA,CREATED_ON,CONACTPROP,APPROVEDON,REMARKS,STATUS" Width="85%"
                    OnRowCommand="GVSec80CHeader_RowCommand" AllowPaging="true" PageSize="15" OnPageIndexChanging="GVSec80CHeader_PageIndexChanging"
                    AllowSorting="true" OnSorting="GVSec80CHeader_Sorting">
                    <Columns>
                        <asp:TemplateField HeaderText="No.">
                            <ItemTemplate>
                                <%# Container.DataItemIndex+1 %>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="ID" DataField="ID" SortExpression="ID"></asp:BoundField>
                        <asp:TemplateField HeaderText="IT Type">
                            <ItemTemplate>
                                Section 80 C
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="BEGDA" HeaderText="From Date" DataFormatString="{0:dd-MMM-yyyy}" SortExpression="BEGDA"/>
                        <asp:BoundField DataField="ENDDA" HeaderText="To Date" DataFormatString="{0:dd-MMM-yyyy}" SortExpression="ENDDA"/>
                       <%-- <asp:BoundField DataField="CONACTPROP" HeaderText="Consider Actuals" />--%>

                          <asp:TemplateField HeaderText="Consider Actuals">
                            <ItemTemplate>

                                <%#(Eval("CONACTPROP").ToString().Trim()=="0") ? "No" : (Eval("CONACTPROP").ToString().Trim()=="1")? "Yes": "-"%>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:BoundField DataField="CREATED_ON" HeaderText="Created On" DataFormatString="{0:dd-MMM-yyyy}" SortExpression="CREATED_ON"/>
                        <asp:BoundField DataField="APPROVEDON" HeaderText="Approved On" DataFormatString="{0:dd-MMM-yyyy}" SortExpression="APPROVEDON"/>
                        <asp:BoundField DataField="REMARKS" HeaderText="Remarks" SortExpression="REMARKS"/>

                        <asp:BoundField HeaderText="Status" DataField="STATUS" SortExpression="STATUS"></asp:BoundField>

                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="LbtnSec80CView" runat="server" CausesValidation="False" CommandName="View" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'>View</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>

                </asp:GridView>

                <br />

                <asp:GridView ID="GVITSec80C" runat="server" AutoGenerateColumns="False" BorderStyle="None" Width="99%">
                    <Columns>
                        <%-- //select t8.SBSEC,t8.SBDIV,t8.SBDDS, t9.SDVLT,t9.TXEXM--%>
                        <%-- <asp:TemplateField HeaderText="No.">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex+1 %>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"/>
                    </asp:TemplateField>--%>
                        <asp:BoundField HeaderText="ID" DataField="ID">
                            <ItemStyle HorizontalAlign="Center" Width="50px" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Investment / Contribution" DataField="ITTXT">
                            <ItemStyle HorizontalAlign="left" Width="450px" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Limit" DataField="ITLMT">
                            <ItemStyle HorizontalAlign="right" />
                        </asp:BoundField>

                        <asp:BoundField HeaderText="Prop. Invst. (INR)" DataField="PROPINVST">
                            <ItemStyle HorizontalAlign="right" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Act. Invst. (INR)" DataField="ACTINVST">
                            <ItemStyle HorizontalAlign="right" />
                        </asp:BoundField>

                        <asp:BoundField HeaderText="Curr" DataField="CURR" Visible="false">
                            <ItemStyle HorizontalAlign="right" />
                        </asp:BoundField>

                        <asp:BoundField HeaderText="Attachments" DataField="RECEIPT_FID">
                            <ItemStyle HorizontalAlign="left" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Comments" DataField="EMPCOMMENTS">
                            <ItemStyle HorizontalAlign="left" />
                        </asp:BoundField>
                    </Columns>

                </asp:GridView>

                
                   <div class="DivSpacer01"></div><br />
    <div id="ExportbtnSec80C" runat="server" >
        <asp:Button ID="BtnExptoXLSEC80C" runat="server" Text="Export To Excel" OnClick="BtnExptoXLSEC80C_Click" CausesValidation="false" TabIndex="10" />
        &nbsp;&nbsp;
                    <asp:Button ID="BtnExptoPdfSec80C" runat="server" Text="Export To PDF" OnClick="BtnExptoPdfSec80C_Click" TabIndex="11" />

    </div>
                    <asp:HiddenField ID="viewcheckSec80C" runat="server"/>
            </asp:View>

            <asp:View ID="View3" runat="server">
                <asp:Label ID="LblHousing" runat="server" CssClass="msgboard"></asp:Label>
                <asp:GridView ID="GVHousingHeader" runat="server" AutoGenerateColumns="False" BorderStyle="None" Width="85%" DataKeyNames="ID,BEGDA,ENDDA,CREATED_ON,CONACTPROP,APPROVEDON,REMARKS,STATUS" 
                    OnRowCommand="GVHousingHeader_RowCommand" AllowPaging="true" PageSize="15" OnPageIndexChanging="GVHousingHeader_PageIndexChanging"
                    AllowSorting="true" OnSorting="GVHousingHeader_Sorting">
                    <Columns>
                        <asp:TemplateField HeaderText="No.">
                            <ItemTemplate>
                                <%# Container.DataItemIndex+1 %>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="ID" DataField="ID" SortExpression="ID"></asp:BoundField>
                        <asp:TemplateField HeaderText="IT Type">
                            <ItemTemplate>
                                Housing
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="BEGDA" HeaderText="From Date" DataFormatString="{0:dd-MMM-yyyy}" SortExpression="BEGDA"/>
                        <asp:BoundField DataField="ENDDA" HeaderText="To Date" DataFormatString="{0:dd-MMM-yyyy}" SortExpression="ENDDA"/>
                        <%--<asp:BoundField DataField="CONACTPROP" HeaderText="Consider Actuals"/>--%>

                        <asp:BoundField DataField="CREATED_ON" HeaderText="Created On" DataFormatString="{0:dd-MMM-yyyy}" SortExpression="CREATED_ON"/>
                        <asp:BoundField DataField="APPROVEDON" HeaderText="Approved On" DataFormatString="{0:dd-MMM-yyyy}" SortExpression="APPROVEDON"/>
                        <asp:BoundField DataField="REMARKS" HeaderText="Remarks" SortExpression="REMARKS"/>

                        <asp:BoundField HeaderText="Status" DataField="STATUS" SortExpression="STATUS"></asp:BoundField>

                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="LbtnHousingView" runat="server" CausesValidation="False" CommandName="View" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'>View</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>

                </asp:GridView>

                <br />

                <asp:GridView ID="GVHousing" runat="server" AutoGenerateColumns="False" BorderStyle="None" Width="99%">
                    <Columns>
                        <%--  //ACCOM,METRO,RTAMT,HRTXE,LDAD1,LDAID,LDADE,EMPCOMMENTS,STATUS--%>
                        <%-- <asp:TemplateField HeaderText="No.">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex+1 %>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"/>
                    </asp:TemplateField>--%>
                        <asp:BoundField HeaderText="ID" DataField="ID">
                            <ItemStyle HorizontalAlign="Center" Width="50px" />
                        </asp:BoundField>
                        <%-- <asp:BoundField HeaderText="Accomodation Type" DataField="">
                        <ItemStyle HorizontalAlign="left" Width="100px"/>
                    </asp:BoundField>--%>
                        <asp:TemplateField HeaderText="Accomodation Type">
                            <ItemTemplate>

                                <%#(Eval("ACCOM").ToString().Trim()=="1") ? "Rentded" : (Eval("ACCOM").ToString().Trim()=="4")? "Own": "-"%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%-- <asp:BoundField HeaderText="City Category"  DataField="">
                        <ItemStyle HorizontalAlign="left" />
                    </asp:BoundField>--%>

                        <asp:TemplateField HeaderText="City Category">
                            <ItemTemplate>

                                <%#(Eval("METRO").ToString().Trim()=="1") ? "Metro" : (Eval("METRO").ToString().Trim()=="0")? "Non-Metro": "-"%>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:BoundField HeaderText="Actual Amount" DataField="RTAMT">
                            <ItemStyle HorizontalAlign="right" />
                        </asp:BoundField>
                        <%-- <asp:BoundField HeaderText="HRA to be Excempt"  DataField="">
                        <ItemStyle HorizontalAlign="left" />
                    </asp:BoundField>--%>

                        <asp:TemplateField HeaderText="HRA to be Excempt">
                            <ItemTemplate>

                                <%#(Eval("HRTXE").ToString().Trim()=="1") ? "Yes" : (Eval("HRTXE").ToString().Trim()=="0")? "No" : "-"%>
                            </ItemTemplate>
                        </asp:TemplateField>


                        <asp:BoundField HeaderText="LandLord's Address" DataField="LDAD1">
                            <ItemStyle HorizontalAlign="left" />
                        </asp:BoundField>

                        <asp:BoundField HeaderText="PAN of LandLord" DataField="LDAID">
                            <ItemStyle HorizontalAlign="left" />
                        </asp:BoundField>

                        <%--<asp:BoundField HeaderText="Declaration Provided by LandLord"  DataField="">
                        <ItemStyle HorizontalAlign="left"/>
                    </asp:BoundField>--%>

                        <asp:TemplateField HeaderText="Declaration Provided by LandLord">
                            <ItemTemplate>

                                <%#(Eval("LDADE").ToString().Trim()=="1") ? "Yes" : (Eval("LDADE").ToString().Trim()=="0")? "No" : "-"%>
                            </ItemTemplate>
                        </asp:TemplateField>



                        <asp:BoundField HeaderText="Comments" DataField="EMPCOMMENTS">
                            <ItemStyle HorizontalAlign="left" />
                        </asp:BoundField>
                    </Columns>

                </asp:GridView>
               

                  <div class="DivSpacer01"></div> <br />
    <div id="ExportbtnHousing" runat="server" >
        <asp:Button ID="BtnExptoXLHousing" runat="server" Text="Export To Excel" OnClick="BtnExptoXLHousing_Click" CausesValidation="false" TabIndex="12" />
        &nbsp;&nbsp;
                    <asp:Button ID="BtnExptopdfHousing" runat="server" Text="Export To PDF" OnClick="BtnExptopdfHousing_Click" TabIndex="13" />

    </div>
    <asp:HiddenField ID="viewcheckHousing" runat="server"/>

            </asp:View>

            <asp:View ID="View4" runat="server">
                <asp:Label ID="LblOthers" runat="server" CssClass="msgboard"></asp:Label>
                <asp:GridView ID="GVOthersHeader" runat="server" AutoGenerateColumns="False" BorderStyle="None" DataKeyNames="ID,ITTYP,BEGDA,ENDDA,CREATED_ON,CONACTPROP,APPROVEDON,REMARKS,STATUS"
                    OnRowCommand="GVOthersHeader_RowCommand" Width="85%" AllowPaging="true" PageSize="15" OnPageIndexChanging="GVOthersHeader_PageIndexChanging"
                    AllowSorting="true" OnSorting="GVOthersHeader_Sorting">
                    <Columns>
                        <asp:TemplateField HeaderText="No.">
                            <ItemTemplate>
                                <%# Container.DataItemIndex+1 %>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="ID" DataField="ID" SortExpression="ID"></asp:BoundField>
                        <%--   <asp:TemplateField HeaderText="IT Type">
                                <ItemTemplate>
                                    Other Sources
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"/>
                    </asp:TemplateField>--%>
                       <%-- <asp:BoundField HeaderText="IT Type" DataField="ITTYP"></asp:BoundField>--%>

                        <asp:TemplateField HeaderText="IT Type" SortExpression="ITTYP">
                            <ItemTemplate>

                                <%#(Eval("ITTYP").ToString().Trim()=="1") ? "Housing Property" :  "Other Sources"%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="BEGDA" HeaderText="From Date" DataFormatString="{0:dd-MMM-yyyy}" SortExpression="BEGDA"/>
                        <asp:BoundField DataField="ENDDA" HeaderText="To Date" DataFormatString="{0:dd-MMM-yyyy}" SortExpression="ENDDA"/>
                        <%--          <asp:BoundField DataField="CONACTPROP" HeaderText="Consider Actuals"/>--%>

                        <asp:BoundField DataField="CREATED_ON" HeaderText="Created On" DataFormatString="{0:dd-MMM-yyyy}" SortExpression="CREATED_ON"/>
                        <asp:BoundField DataField="APPROVEDON" HeaderText="Approved On" DataFormatString="{0:dd-MMM-yyyy}" SortExpression="APPROVEDON"/>
                        <asp:BoundField DataField="REMARKS" HeaderText="Remarks"  SortExpression="REMARKS"/>

                        <asp:BoundField HeaderText="Status" DataField="STATUS" SortExpression="STATUS"></asp:BoundField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="LbtnOthersView" runat="server" CausesValidation="False" CommandName="View" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'>View</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>

                </asp:GridView>

                <br />
                <asp:GridView ID="GVOthers1" runat="server" AutoGenerateColumns="False" BorderStyle="None" Width="85%">
                    <Columns>
                        <%--    //ID,PROPTYP,RENTO,INT24,LETVL,REP24,OTH24,TDSOT,
                // BSPFT,CPGLN,CPGLS,CPGNS,CPGSS,DVDND,INTRS,UNSPI,TDSAT, EMPCOMMENTS,EMPCOMMENTS2,STATUS  -%>
                   <%-- <asp:TemplateField HeaderText="No.">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex+1 %>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"/>
                    </asp:TemplateField>--%>
                        <asp:BoundField HeaderText="ID" DataField="ID">
                            <ItemStyle HorizontalAlign="Center" Width="50px" />
                        </asp:BoundField>
                        <%-- <asp:BoundField HeaderText="Accomodation Type" DataField="">
                        <ItemStyle HorizontalAlign="left" Width="100px"/>
                    </asp:BoundField>--%>
                        <asp:TemplateField HeaderText="Income Type">
                            <ItemTemplate>

                                <%#(Eval("PROPTYP").ToString().Trim()=="1") ? "House Property" : (Eval("PROPTYP").ToString().Trim()=="2")? "Other Sources": "-"%>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Property Type">
                            <ItemTemplate>

                                <%#(Eval("RENTO").ToString().Trim()=="1") ? "Self-Occupied/Deemed Self Occupied House Property" : (Eval("RENTO").ToString().Trim()=="2")? "Partly Let Out House Property": (Eval("RENTO").ToString().Trim()=="3")? "Wholly Let Out House Property": "-"%>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:BoundField HeaderText="Deduce - Interest u/s 24" DataField="INT24">
                            <ItemStyle HorizontalAlign="right" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Final Lettable Value" DataField="LETVL">
                            <ItemStyle HorizontalAlign="right" />
                        </asp:BoundField>

                        <%--  <asp:BoundField HeaderText="Deduce - Interest u/s 24" DataField="LETVL">
                            <ItemStyle HorizontalAlign="right" />
                        </asp:BoundField>--%>

                        <asp:BoundField HeaderText="TDS On Other Income" DataField="TDSOT">
                            <ItemStyle HorizontalAlign="right" />
                        </asp:BoundField>

                        <asp:BoundField HeaderText="Deduce - Repair u/s 24" DataField="REP24">
                            <ItemStyle HorizontalAlign="right" />
                        </asp:BoundField>

                        <asp:BoundField HeaderText="Deduce - Others u/s 24 " DataField="OTH24">
                            <ItemStyle HorizontalAlign="right" />
                        </asp:BoundField>

                          <asp:BoundField HeaderText="Comments" DataField="EMPCOMMENTS">
                            <ItemStyle HorizontalAlign="left" />
                        </asp:BoundField>
                    </Columns>

                </asp:GridView>



                <asp:GridView ID="GVOthers2" runat="server" AutoGenerateColumns="False" BorderStyle="None" Width="85%"> 
                    <Columns>
                        <%--    //ID,PROPTYP,RENTO,INT24,LETVL,REP24,OTH24,TDSOT,
                // BSPFT,CPGLN,CPGLS,CPGNS,CPGSS,DVDND,INTRS,UNSPI,TDSAT, EMPCOMMENTS,EMPCOMMENTS2,STATUS  -%>
                   <%-- <asp:TemplateField HeaderText="No.">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex+1 %>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"/>
                    </asp:TemplateField>--%>
                        <asp:BoundField HeaderText="ID" DataField="ID">
                            <ItemStyle HorizontalAlign="Center" Width="50px" />
                        </asp:BoundField>

                        <asp:TemplateField HeaderText="Income Type">
                            <ItemTemplate>

                                <%#(Eval("PROPTYP").ToString().Trim()=="1") ? "House Property" : (Eval("PROPTYP").ToString().Trim()=="2")? "Other Sources": "-"%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        

                           <asp:BoundField HeaderText="Business Profits" DataField="BSPFT">
                            <ItemStyle HorizontalAlign="right" />
                        </asp:BoundField>
                           <asp:BoundField HeaderText="Long-Term Capital Gains (Normal Rate)" DataField="CPGLN">
                            <ItemStyle HorizontalAlign="right" />
                        </asp:BoundField>
                           <asp:BoundField HeaderText="Long-Term Capital Gains (Special Rate)" DataField="CPGLS">
                            <ItemStyle HorizontalAlign="right" />
                        </asp:BoundField>
                           <asp:BoundField HeaderText="Short-Term Capital Gains" DataField="CPGNS">
                            <ItemStyle HorizontalAlign="right" />
                        </asp:BoundField>
                           <asp:BoundField HeaderText="Short-Term Capital Gains (Listed Securities)" DataField="CPGSS">
                            <ItemStyle HorizontalAlign="right" />
                        </asp:BoundField>
                           <asp:BoundField HeaderText="Income From Dividend" DataField="DVDND">
                            <ItemStyle HorizontalAlign="right" />
                        </asp:BoundField>
                            <asp:BoundField HeaderText="Income From Interest" DataField="INTRS">
                            <ItemStyle HorizontalAlign="right" />
                        </asp:BoundField>
                           <asp:BoundField HeaderText="Other Income Unspecified" DataField="UNSPI">
                            <ItemStyle HorizontalAlign="right" />
                        </asp:BoundField>
                           <asp:BoundField HeaderText="TDS On Other Income" DataField="TDSAT">
                            <ItemStyle HorizontalAlign="right" />
                        </asp:BoundField>
                       
                
                          <asp:BoundField HeaderText="Comments" DataField="EMPCOMMENTS2">
                            <ItemStyle HorizontalAlign="left" />
                        </asp:BoundField>



                    </Columns>

                </asp:GridView>
              
                  <div class="DivSpacer01"></div>  <br />
    <div id="ExportbtnOthers" runat="server" >
        <asp:Button ID="BtnExptoXLOthers" runat="server" Text="Export To Excel" OnClick="BtnExptoXLOthers_Click" CausesValidation="false" TabIndex="14" />
        &nbsp;&nbsp;
                    <asp:Button ID="BtnExptopdfOthers" runat="server" Text="Export To PDF" OnClick="BtnExptopdfOthers_Click" TabIndex="15" />

    </div>
    <asp:HiddenField ID="viewcheckOthers" runat="server"/>

            </asp:View>

        </asp:MultiView>

    </div>

</asp:Content>
