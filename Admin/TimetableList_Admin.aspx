<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/admin.Master" AutoEventWireup="true" CodeBehind="TimetableList_Admin.aspx.cs" Inherits="KPMAMS.Admin.TimetableList_Admin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="BodyContent" runat="server">
    <script>
        $(document).ready(function () {
            $.noConflict();
            $(".table").prepend($("<thead></thead>").html($(this).find("tr:first"))).dataTable({
                "searching": true,
                "pageLength": 10,
                "order": [[1, 'asc']],
                "lengthMenu": [[1, 5, 10, 25, 50, -1], [1, 5, 10, 25, 50, "All"]],
                columnDefs: [{
                    'targets': [0], /* column index [0,1,2,3]*/
                    'orderable': false, /* true or false */
                }]
            });
        });
    </script>
    <div class="p-4 col">
        <div class="row">
            <div class="col">
                <center>
                    <h3>Timetable List</h3>
                </center>
            </div>
        </div>
        <div class="row">
            <div class="col">
                <hr />
            </div>
        </div>
        <div class="row">
            <div class="col-md-10">
            </div>
            <div class="col-md-2">
                <asp:Button ID="btnCreateTimetable" class="btn btn-info btn-block btn-sm" runat="server" Text="Create" OnClick="btnCreateTimetable_Click" />
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <center>
                    <h3>
                        <asp:Label ID="lblNoData" runat="server" Visible="false" Text="No Data Found"></asp:Label>
                    </h3> 
                </center>
                <asp:GridView class="" ID="GvTimetableList" CssClass="table table-striped table-responsive-md" runat="server" AutoGenerateColumns="False" OnRowDataBound="GvTimetableList_RowDataBound" >
                    <Columns>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:HyperLink ID="hlView" Text="View" runat="server" ></asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="TimetableGUID" HeaderText="Timetable GUID" ReadOnly="True" SortExpression="TimetableGUID" Visible="false"/>
                        <asp:BoundField DataField="Class" HeaderText="Class" ReadOnly="True" SortExpression="Class"/>
                        <asp:BoundField DataField="CreateDate" HeaderText="Create Date" ReadOnly="True" SortExpression="CreateDate" />
                        <asp:BoundField DataField="LastUpdateDate" HeaderText="Last Update Date" ReadOnly="True" SortExpression="LastUpdateDate" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>        
    </div>
    <script src="script/jquery/jquery.min.js"></script>
</asp:Content>
