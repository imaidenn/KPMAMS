<%@ Page Title="" Language="C#" MasterPageFile="~/TSPSite.Master" AutoEventWireup="true" CodeBehind="ForumList.aspx.cs" Inherits="KPMAMS.ForumList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="BodyContent" runat="server">
    <div class="p-4 col">
        <div class="row">
            <div class="col">
                <center>
                    <h3>ForumList</h3>
                </center>
            </div>
        </div>
        <div class="row">
            <div class="col">
                <hr>
            </div>
        </div>
        <div class="row">
            <div class="col">
                <%--<asp:GridView class="table table-striped table-bordered" ID="GridView1" runat="server" OnSelectedIndexChanged="GridView1_SelectedIndexChanged"></asp:GridView>--%>
                <asp:GridView class="table table-striped table-bordered" ID="GvForumList" runat="server"></asp:GridView>
            </div>
        </div>        
    </div>
</asp:Content>
