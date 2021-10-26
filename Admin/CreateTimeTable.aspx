<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/admin.Master" AutoEventWireup="true" CodeBehind="CreateTimeTable.aspx.cs" Inherits="KPMAMS.CreateTimeTable" %>

<asp:Content ID="Content1" ContentPlaceHolderID="BodyContent" runat="server">
    <script>
        $(document).ready(function () {
            $.noConflict();
            $("#<%= GvSubject.ClientID%>").prepend($("<thead></thead>").append($("#<%= GvSubject.ClientID%>").find("tr:first"))).DataTable({
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
    <div class="container-fluid" runat="server" >
        <div class="row">
            <div class="col mx-auto">
                <div class="panel">
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-md-12">
                                <h3>Create/Update timetable</h3>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <hr>
                            </div>
                            <div class="col-md-9">
                            </div>
                            <div class="col-md-3">
                                <asp:DropDownList ID="dlClassList" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="dlClassList_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="row d-flex">
                            <div class="col-md-12">
                                <table class="table table-bordered table-responsive-md">
                                    <thead>
                                    <tr>
                                        <th scope="col">Days/Time</th>
                                        <th scope="col">8.30am-9.00am</th>
                                        <th scope="col">9.00am-9.30am</th>
                                        <th scope="col">9.30am-10.00am</th>
                                        <th scope="col">10.00am-10.30am</th>
                                        <th scope="col">10.30am-11.00am</th>
                                        <th scope="col">11.00am-11.30am</th>
                                        <th scope="col">11.30am-12.00pm</th>
                                        <th scope="col">12.00am-12.30am</th>
                                        <th scope="col">12.30am-1.00am</th>
                                        <th scope="col">1.00am-1.30am</th>
                                    </tr>
                                    </thead>
                                    <tbody>
                                    <tr>
                                        <th scope="row">Monday</th>
                                        <td>
                                            <asp:DropDownList ID="d1t1" runat="server"></asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="d1t2" runat="server"></asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="d1t3" runat="server"></asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="d1t4" runat="server"></asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="d1t5" runat="server"></asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="d1t6" runat="server"></asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="d1t7" runat="server"></asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="d1t8" runat="server"></asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="d1t9" runat="server"></asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="d1t10" runat="server"></asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th scope="row">Tuesday</th>
                                        <td>
                                            <asp:DropDownList ID="d2t1" runat="server"></asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="d2t2" runat="server"></asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="d2t3" runat="server"></asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="d2t4" runat="server"></asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="d2t5" runat="server"></asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="d2t6" runat="server"></asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="d2t7" runat="server"></asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="d2t8" runat="server"></asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="d2t9" runat="server"></asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="d2t10" runat="server"></asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th scope="row">Wednesday</th>
                                        <td>
                                            <asp:DropDownList ID="d3t1" runat="server"></asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="d3t2" runat="server"></asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="d3t3" runat="server"></asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="d3t4" runat="server"></asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="d3t5" runat="server"></asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="d3t6" runat="server"></asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="d3t7" runat="server"></asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="d3t8" runat="server"></asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="d3t9" runat="server"></asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="d3t10" runat="server"></asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th scope="row">Thursday</th>
                                        <td>
                                            <asp:DropDownList ID="d4t1" runat="server"></asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="d4t2" runat="server"></asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="d4t3" runat="server"></asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="d4t4" runat="server"></asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="d4t5" runat="server"></asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="d4t6" runat="server"></asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="d4t7" runat="server"></asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="d4t8" runat="server"></asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="d4t9" runat="server"></asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="d4t10" runat="server"></asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th scope="row">Friday</th>
                                        <td>
                                            <asp:DropDownList ID="d5t1" runat="server"></asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="d5t2" runat="server"></asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="d5t3" runat="server"></asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="d5t4" runat="server"></asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="d5t5" runat="server"></asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="d5t6" runat="server"></asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="d5t7" runat="server"></asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="d5t8" runat="server"></asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="d5t9" runat="server"></asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="d5t10" runat="server"></asp:DropDownList>
                                        </td>
                                    </tr>
                                    </tbody>
                                </table>
                                <div class="col-md-5 text-right">
                                    <asp:Button ID="btnCreate" runat="server" Class="btn btn-primary" style="margin-top:20px" Text="Create" OnClick="btnCreate_Click" />
                                </div>
                                <div class="col-md-2 text-center">
                                    <asp:Button ID="btnReset" runat="server" Class="btn btn-warning" style="margin-top:20px" Text="Reset" OnClick="btnReset_Click" />
                                </div>
                                <div class="col-md-5 text-left">
                                    <asp:Button ID="btnCancel" runat="server" Class="btn btn-danger" style="margin-top:20px" Text="Cancel" OnClick="btnCancel_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-6 mx-auto">
                <div class="panel">
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-md-12">
                                <asp:GridView class="" ID="GvSubject" CssClass="table table-striped table-responsive-md" runat="server" AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:BoundField DataField="SubjectGUID" HeaderText="Subject GUID" ReadOnly="True" SortExpression="SubjectGUID" Visible="false"/>
                                        <asp:BoundField DataField="SubjectName" HeaderText="Subject" ReadOnly="True" SortExpression="SubjectName" />
                                        <asp:BoundField DataField="Shortform" HeaderText="Shortform" ReadOnly="True" SortExpression="Shortform" />
                                    </Columns>
                                </asp:GridView>                 
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-6 mx-auto">
                <div class="panel">
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-md-12">
                                <asp:GridView class="" ID="GvSubjectTeach" CssClass="table table-striped table-responsive-md" runat="server" AutoGenerateColumns="False">
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
        </div>
    </div>
    
    <script src="../script/jquery/jquery.min.js"></script>
</asp:Content>
