<%@ Page Title="" Language="C#" MasterPageFile="~/UI/SubSiteMaster.master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="iEmpPower.UI.PR.Index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <!-- start page title -->
    <div class="row">
        <div class="col-6">
            <div class="page-title-box">
                <h4 class="page-title">Dashboard</h4>
            </div>
        </div>
        <div class="col-6">
            <div class="page-title-box page-title text-left">

                <label class='toggle-label'>
                    <input type='checkbox' />
                    <span class='back'>
                        <span class='toggle'></span>
                        <span class='label on'>Me as User</span>
                        <span class='label off'>Me as Approver</span>
                    </span>
                </label>

            </div>
        </div>
    </div>
    <!-- end page title -->

    <div class="row">
        <div class="col-md-6 col-xl-3">
            <div class="card-box">
                <div class="row">
                    <div class="col-5">
                        <div class="avatar-lg rounded-circle">
                            <img src="../../NewAssets/images/icon-01.png" height="86" alt="" />
                        </div>
                    </div>
                    <div class="col-7">
                        <div class="text-right">
                            <a href="#"><i class="fa fa-heart float-right fa-1x text-danger"></i></a>
                            <div class="clearfix"></div>
                            <div class="text-dark mt-1">
                                <h2><span data-plugin="counterup">14</span></h2>
                            </div>
                            <p class="text-muted mb-1 "><a href="Purchase_Request.html">Purchase Request</a></p>
                        </div>
                    </div>
                </div>
                <!-- end row-->
            </div>
            <!-- end widget-rounded-circle-->

        </div>
        <!-- end col-->

        <div class="col-md-6 col-xl-3">
            <div class="card-box">
                <div class="row">
                    <div class="col-5">
                        <div class="avatar-lg rounded-circle">
                            <img src="../../NewAssets/images/icon-02.png" height="86" alt="" />
                        </div>
                    </div>
                    <div class="col-7">
                        <div class="text-right">
                            <a href="#"><i class="fa fa-heart float-right fa-1x text-danger"></i></a>
                            <div class="clearfix"></div>
                            <div class="text-dark mt-1">
                                <h2><span data-plugin="counterup">05</span></h2>
                            </div>
                            <p class="text-muted mb-1"><a href="Benefits_Payment.html">Travel Management</a></p>
                        </div>
                    </div>
                </div>
                <!-- end row-->
            </div>
            <!-- end widget-rounded-circle-->
        </div>
        <!-- end col-->

        <div class="col-md-6 col-xl-3">
            <div class="card-box">
                <div class="row">
                    <div class="col-5">
                        <div class="avatar-lg rounded-circle">
                            <img src="../../NewAssets/images/icon-04.png" height="86" alt="" />
                        </div>
                    </div>
                    <div class="col-7">
                        <div class="text-right">
                            <a href="#"><i class="fa fa-heart text-danger float-right fa-1x"></i></a>
                            <div class="clearfix"></div>
                            <div class="text-dark mt-1">
                                <h2><span data-plugin="counterup">14</span></h2>
                            </div>
                            <p class="text-muted mb-1"><a href="Benefits_Payment.html">IExpense</a></p>
                        </div>
                    </div>
                </div>
                <!-- end row-->
            </div>
            <!-- end widget-rounded-circle-->
        </div>
        <!-- end col-->

        <div class="col-md-6 col-xl-3">
            <div class="card-box">
                <div class="row">
                    <div class="col-5">
                        <div class="avatar-lg rounded-circle">
                            <img src="../../NewAssets/images/icon-03.png" height="86" alt="" />
                        </div>
                    </div>
                    <div class="col-7">
                        <a href="Benefits_Payment.html">
                            <div class="text-right">
                                <i class="fa fa-heart text-danger float-right fa-1x"></i>
                                <div class="clearfix"></div>
                                <div class="text-dark mt-1">
                                    <h2><span data-plugin="counterup">02</span></h2>
                                </div>
                                <p class="text-muted mb-1">Benefits & Payment</p>
                            </div>
                        </a>
                    </div>
                </div>
                <!-- end row-->
            </div>
            <!-- end widget-rounded-circle-->
        </div>
        <!-- end col-->
    </div>
</asp:Content>
