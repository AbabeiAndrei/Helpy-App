<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="donate.aspx.cs" Inherits="Helpy.Pages.donate" %>
<%@ Import Namespace="Helpy.Classes" %>
<%@ Import Namespace="Helpy.Code" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .container {
            margin-top: 20px;
        }
    </style>
    <script type="text/javascript">
        $(function () {
            $('#datetimepicker1').datetimepicker();
        });
    </script>
    <style>
        td {
            border-color: white;
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
                        <h3 class="panel-title">Donate</h3>
                    </div>
                    <div class="panel-body">
                        <p>Complete the form below in order to donate</p>
                        <div class="nav nav-tabs nav-tabs-justified">
                            <ul class="nav nav-tabs">
                                <li class="<%= _childId == 0 ? "active" : string.Empty%> "><a href="#tab-1" data-toggle="tab">Step 1. Who you want to help?</a></li>
                                <li class="<%= _childId != 0 ? "active" : string.Empty%> "><a href="#tab-2" data-toggle="tab">Step 2. Who are you?</a></li>
                                <li><a href="#tab-3" data-toggle="tab">Step 3. How much you would like to offer?</a></li>
                            </ul>
                            <div class="tab-content">
                                <div class="tab-pane <%= _childId == 0 ? "active" : string.Empty%>" id="tab-1">
                                    <div class="list-causes">
                                        <div class="list-group">
                                            <asp:ObjectDataSource ID="childDs" runat="server" SelectMethod="GetAll" TypeName="Helpy.Classes.Child">
                                                <SelectParameters>
                                                    <asp:Parameter DefaultValue="0" Name="page" Type="Int32" />
                                                    <asp:Parameter DefaultValue="10000" Name="limit" Type="Int32" />
                                                </SelectParameters>
                                            </asp:ObjectDataSource>
                                        </div>
                                        <%
                                            var mlkids = GetChilds(0, Constants.ITEMS_PER_PAGE).ToList();

                                            if(mlkids.Count == 0)
                                                Response.Write("<div class=\"alert alert-success\" role=\"alert\">Nothing to donate, all people are happy!!</div>");
                                        %>
                                        <ul class="list-group">
                                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="childDs" ShowHeader="False" Width="100%">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Id" SortExpression="Id">
                                                        <ItemTemplate>
                                                            <li class="list-group-item">
                                                                <span class="badge right">
                                                                    <%#((decimal)Eval("Amount")).ToString("N2")%> <%=Constants.CURRENCY  %>
                                                                </span>
                                                                <h4 class="list-group-item-heading">
                                                                    <%# Eval("FullName") %> 
                                                                </h4>
                                                                <p class="list-group-item-text">
                                                                    <%# Eval("ShortStory") %>
                                                                </p>
                                                                <div class="row">
                                                                    <div class="col-md-10">
                                                                        <div class="progress" style="margin-top:10px">
                                                                            <div class="progress-bar" 
                                                                                    role="progressbar" 
                                                                                    aria-valuenow="60"
                                                                                    aria-valuemin="0" 
                                                                                    aria-valuemax="100" 
                                                                                    style="width: <%#((float)Eval("Procent")).ToString("N2")%>%;">
                                                                                    <%# ((decimal)Eval("Total")).ToString("N2") %> <%=Constants.CURRENCY%>
                                                                            </div>
                                                                        </div>  
                                                                    </div>
                                                                    <div class="col-md-2">
                                                                        <asp:Button runat="server"
                                                                                    Text="Donate" 
                                                                                    CssClass="btn btn-primary donate_child_click right" 
                                                                                    OnClick="OnNextClick" 
                                                                                    CommandArgument='<%# Eval("Id") %>'
                                                                                    ID="btnNext" /> 
                                                                    </div>
                                                                </div>
                                                            </li>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <RowStyle BorderStyle="None" />
                                            </asp:GridView>
                                        </ul>
                                    </div> 
                                    <%

                                        var mltotalKids = TotalNumberOfKids();
                                        if (mltotalKids > Constants.ITEMS_PER_PAGE)
                                        {
                                            Response.Write(@"
                                            <nav aria-label=""Page navigation"">
                                                <ul class=""pagination"">
                                                    <li class=""disabled"">
                                                        <a href=""#"" aria-label=""Previous"">
                                                            <span aria-hidden=""true"">&laquo;</span>
                                                        </a>
                                                    </li>
                                                    <li class=""active""><a href=""#"">1<span class=""sr-only"">(current)</span></a></li>");

                                                for (mltotalKids -= Constants.ITEMS_PER_PAGE; mltotalKids > 0; mltotalKids -= Constants.ITEMS_PER_PAGE)
                                                    Response.Write(@"<li><a href=""#"">2</a></li>");

                                            Response.Write(@"
                                                <li>
                                                    <a href=""#"" aria-label=""Next"">
                                                        <span aria-hidden=""true"">&raquo;</span>
                                                    </a>
                                                </li>
                                            </ul>
                                            </nav>");
                                        }
                                    %>                    
                                </div>
                                <div class="tab-pane <%= _childId != 0 ? "active" : string.Empty%>" id="tab-2">
                                    <div class="row list-causes">
                                        <div class="col-lg-8">
                                            <div class="input-group">
                                                <div class="input-group-btn">
                                                <button type="button" 
                                                        class="btn btn-default dropdown-toggle" 
                                                        data-toggle="dropdown" 
                                                        aria-haspopup="true" 
                                                        aria-expanded="false" 
                                                        id="btn_title_donation">
                                                    Mr.
                                                    <span class="caret"></span>
                                                </button>
                                                <ul class="dropdown-menu">
                                                      <li><a href="#" class="btn_title_donation_item">Mr.</a></li>
                                                      <li><a href="#" class="btn_title_donation_item">Mrs.</a></li>
                                                      <li><a href="#" class="btn_title_donation_item">Miss</a></li>
                                                </ul>
                                                </div>
                                                <asp:TextBox runat="server" CssClass="form-control" ID="txtName"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-lg-4">
                                            <div class="input-group">
                                                <span class="input-group-addon">Mobile</span>
                                                <asp:TextBox runat="server" CssClass="form-control" ID="txtPhone" TextMode="Phone"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row list-causes">
                                        <div class="col-lg-12">
                                            <div class="input-group">
                                                <span class="input-group-addon">Address</span>
                                                <asp:TextBox runat="server" CssClass="form-control" ID="txtAddress"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="tab-pane" id="tab-3">
                                    <div class="row list-causes">
                                        <div class="col-lg-2">
                                            <h5>I want to donate </h5>
                                        </div>
                                        <div class="col-lg-4">
                                            <div class="input-group">
                                                <asp:TextBox runat="server" CssClass="form-control" ID="txtAmount" TextMode="Number"></asp:TextBox>
                                                <span class="input-group-addon"> <%= Constants.CURRENCY %></span>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <h5 id="donate_to_name">for </h5>
                                            <asp:Label runat="server" ID="lblDonateToId" CssClass="invisible">for </asp:Label>
                                        </div>
                                    </div>
                                    <div class="row list-causes">
                                        <div class="col-lg-2">
                                            <h5>Using my </h5>
                                        </div>
                                        <div class="col-lg-10">
                                            <div class="input-group">
                                                <div class="input-group-btn">
                                                <button type="button" 
                                                        class="btn btn-default dropdown-toggle" 
                                                        data-toggle="dropdown" 
                                                        aria-haspopup="true" 
                                                        aria-expanded="false" 
                                                        id="btn_card_donation">
                                                    Visa
                                                    <span class="caret"></span>
                                                </button>
                                                <ul class="dropdown-menu">
                                                      <li><a href="#" class="btn_card_donation_item">Visa</a></li>
                                                      <li><a href="#" class="btn_card_donation_item">Mastercard</a></li>
                                                </ul>
                                                </div>
                                                <asp:TextBox runat="server" CssClass="form-control" ID="txtCard"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row list-causes">
                                        <div class="col-lg-6">
                                            <div class="input-group">
                                                <span class="input-group-addon">Card Holder</span>
                                                <asp:TextBox runat="server" CssClass="form-control" ID="txtCardHolder"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-lg-6">
                                            <div class='input-group'>
                                                <span class="input-group-addon">Expire date</span>
                                                <asp:TextBox runat="server" ID="txtCardExpire" CssClass="form-control" TextMode="Date" />
                                                <span class="input-group-addon">
                                                    <span class="glyphicon glyphicon-calendar"></span>
                                                </span>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-horizontal list-causes">
                                        <div class="form-group">
                                            <div class="col-md-12">
                                                <textarea class="form-control no-resizable " rows="3" placeholder="A short message?" runat="server" ID="txtStory"></textarea>
                                            </div>
                                        </div>
                                    </div>
                                    <asp:Button runat="server" CssClass="btn btn-primary" Text="Donate!" OnClick="OnClickDonate" />           
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-2"></div>
        </div>
    </div>
</asp:Content>
