<%@ Page Title="" Language="C#" MasterPageFile="~/user.Master" AutoEventWireup="true" CodeBehind="ChatList.aspx.cs" Inherits="KPMAMS.ChatList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        $(document).ready(function () {
            $.noConflict();
            $("#<%= GvTeacherList.ClientID%>").prepend($("<thead></thead>").append($("#<%= GvTeacherList.ClientID%>").find("tr:first"))).DataTable({
                "searching": true,
                "pageLength": 5,
                "order": [[1, 'asc']],
                "lengthMenu": [[1, 5, 10, 25, 50, -1], [1, 5, 10, 25, 50, "All"]],
                columnDefs: [{
                    'targets': [0], /* column index [0,1,2,3]*/
                    'orderable': false, /* true or false */
                }]
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-md-12">
                <div class="row">
                    <div class="col-md-10">
                        <h3>Chat list</h3>
                    </div>
                    <div class="col-md-2">
                        <asp:Button ID="btnGroupChat" href="LiveChat.aspx" runat="server" class="btn btn-outline-secondary" Onclick="btnGroupChat_Click" Text="GroupChat" />
                    </div>
                    <div class="col-md-12">
                        <hr />
                    </div>
                </div>
            </div>
            <div class="col-md-6">
                <div class="panel">
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-md-12">
                                <label>Teacher list</label>
                                <asp:GridView class="" ID="GvTeacherList" CssClass="table table-striped table-responsive-md" runat="server" AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:BoundField DataField="FullName" HeaderText="Teacher" ReadOnly="True" SortExpression="FullName" />
                                        <asp:BoundField DataField="SubjectName" HeaderText="Subject" ReadOnly="True" SortExpression="SubjectName" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-6">
                <div class="panel">
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-md-12">
                                <label>Current chat</label>
                                <asp:GridView class="" ID="GvCurrentChat" CssClass="table table-striped table-responsive-md" runat="server" AutoGenerateColumns="False" OnRowDataBound="GvCurrentChat_RowDataBound" >
                                    <Columns>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="hlChat" Text="Chat" runat="server" ></asp:HyperLink>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="ChatGUID" HeaderText="ChatGUID" Visible="false" ReadOnly="True" SortExpression="FullName" />
                                        <asp:BoundField DataField="FullName" HeaderText="Teacher" ReadOnly="True" SortExpression="FullName" />
                                        <asp:BoundField DataField="SubjectName" HeaderText="Subject" ReadOnly="True" SortExpression="SubjectName" />
                                        <asp:BoundField DataField="CreateDate" HeaderText="Create date" ReadOnly="True" SortExpression="CreateDate" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


</asp:Content>
