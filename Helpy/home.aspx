<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="home.aspx.cs" Inherits="Helpy.index" %>
<%@ Import Namespace="Helpy.Code" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="jumbotron">
        <div class="container"> 
            <h1 class="display-3">Welcome to <%=Constants.PROJECT_NAME %> </h1>
            <p class="lead">The world need our help.</p>
        </div>
    </div>
    <div class="container">
        <% 
            var md = GetFistDonor();
            if(md == null)
                
                Response.Write("<div class=\"alert alert-warning\" role=\"alert\">No donation today, be the first who <a href='Pages/donate.aspx' class=\"alert-link\">donate</a>.</div>");
            else
                Response.Write("<div class=\"alert alert-info\" role=\"alert\">" +
                                    "Donor of the day " +
                                    "<a href=\"#\" class=\"alert-link\">" +
                                        $"{md.Name} {md.Amount.ToString("N2")} {Constants.CURRENCY}" +
                                    "</a>" +
                                "</div>");
        %>
        <div class="alert alert-success" role="alert" style='<%=!_fromDonation ? "display:none" : string.Empty%>'>
            Thank you for donation, we like people like you!!
        </div>
        <div class="row">
            <div class="col-md-4">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                            <h3 class="panel-title">Donate</h3>
                    </div>
                    <div class="panel-body">
                        <p>Help kids all arround the world</p>
                        <a href="Pages/donate.aspx" class="btn btn-primary">
                            Donate now
                        </a>
                    </div>
                </div>
            </div>
            <div class="col-md-4 panel">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <h3 class="panel-title">Help</h3>
                    </div>
                    <div class="panel-body">
                        <p>Add a cause you want to donate</p>
                        <a href="Pages/cause.aspx" class="btn btn-primary">
                            Add cause
                        </a>
                    </div>
                </div>
            </div>
            <div class="col-md-4">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <h3 class="panel-title">Top donors</h3>
                    </div>
                    <div class="panel-body">
                        <p>Who are the best donors of the mouth</p>
                        <a href="Pages/top.aspx" class="btn btn-primary">
                            View top
                        </a>
                    </div>
                </div>
            </div>
        </div>
        <hr/>
        <footer>
            <p>© <%=Constants.CREATOR %> 2016</p>
        </footer>
    </div>
</asp:Content>
