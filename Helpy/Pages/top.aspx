<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="top.aspx.cs" Inherits="Helpy.Pages.top" %>
<%@ Import Namespace="Helpy.Code" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        td {
            border-color: white;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row" style="padding-top: 10px">
            <div class="col-md-2"></div>
            <div class="col-md-8">
                <div class="page-header">
                    <h1>Thank you all of you</h1>
                </div>
                <div class="list-group">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="dsTop" ShowHeader="False" CssClass="full-widht">
                        <Columns>
                            <asp:TemplateField HeaderText="Id" SortExpression="Amount">
                                <ItemTemplate>
                                    <a href="#" class="list-group-item list-group-item-action">
                                        <h4 class="list-group-item-heading"><%#Eval("Name") %> donated 
                                                                                <strong>
                                                                                    <%# ((decimal)Eval("Amount")).ToString("N2") + " " + Constants.CURRENCY%>
                                                                                </strong>
                                                                            for <%#Eval("ChildName") %></h4>
                                        <p class="list-group-item-text"><%#Eval("Message") %></p>
                                    </a>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                <asp:ObjectDataSource ID="dsTop" runat="server" SelectMethod="TopDonor" TypeName="Helpy.Classes.Donor">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="20" Name="limit" Type="Int32" />
                        <asp:Parameter DefaultValue="" Name="date" Type="DateTime" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </div>
            <div class="col-md-2"></div>
        </div>
    </div>
</asp:Content>
