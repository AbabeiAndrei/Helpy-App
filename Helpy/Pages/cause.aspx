<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="cause.aspx.cs" Inherits="Helpy.Pages.cause" %>
<%@ Import Namespace="Helpy.Code" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(function () {
            $('.dtp').datetimepicker();
        });
    </script>
    
    <style>
        .row {
            margin-top: 10px;
            padding: 5px
        }
        .right {
            margin-right: 5px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-md-2"></div>
            <div class="col-md-8">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <h3 class="panel-title">Help people</h3>
                        Thank you for helping all us!
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-md-8">
                                <div class="input-group">
                                    <span class="input-group-addon">Full name</span>
                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtName"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class='input-group date dtp' id='datetimepicker1' data-provider="datepicker" weekStart="0">
                                    <span class="input-group-addon">BirthDate</span>
                                    <asp:TextBox runat="server" ID="txtBirthDate" CssClass="form-control" />
                                    <span class="input-group-addon">
                                        <span class="glyphicon glyphicon-calendar"></span>
                                    </span>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="input-group">
                                    <span class="input-group-addon">Address</span>
                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtAddress"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="input-group">
                                    <span class="input-group-addon">Short story</span>
                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtShortStory"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="input-group">
                                    <span class="input-group-addon">Need</span>
                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtNeed" TextMode="Number"></asp:TextBox>
                                    <span class="input-group-addon"><%=Constants.CURRENCY %></span>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class='input-group date dtp' id='dtpUntil' data-provider="datepicker" weekStart="0">
                                    <span class="input-group-addon">Until</span>
                                    <asp:TextBox runat="server" ID="txtUntil" CssClass="form-control" />
                                    <span class="input-group-addon">
                                        <span class="glyphicon glyphicon-calendar"></span>
                                    </span>
                                </div>
                            </div>
                        </div>
                        <div class="form-horizontal list-causes">
                            <div class="form-group">
                                <div class="col-md-12">
                                    <textarea class="form-control no-resizable " rows="3" placeholder="Full story" runat="server" ID="txtMessage"></textarea>
                                </div>
                            </div>
                        </div>
                        <asp:Button runat="server" ID="btnOk" Text="Create" CssClass="btn btn-primary right" OnClick="btnOk_OnClick"/>
                    </div>
                </div>
            </div>
            <div class="col-md-2"></div>
        </div>
    </div>
</asp:Content>
