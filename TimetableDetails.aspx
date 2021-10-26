<%@ Page Title="" Language="C#" MasterPageFile="~/user.Master" AutoEventWireup="true" CodeBehind="TimetableDetails.aspx.cs" Inherits="KPMAMS.TimetableDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <div class="container-fluid" runat="server" >
        <div class="row">
            <div class="col mx-auto">
                <div class="panel">
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-md-12">
                                <h3>
                                    <asp:Label ID="lbClass" runat="server"></asp:Label>
                                </h3>
                            </div>
                            <div class="col-md-12">
                                <hr />
                            </div>
                            <div class="col-md-3">
                                <asp:Label ID="lbCreateDate" runat="server"></asp:Label>
                            </div>
                            <div class="col-md-3">
                                <asp:Label ID="lbLastUpdateDate" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12" style="margin-top:10px;">
                                <asp:GridView class="" ID="GvTimetable" CssClass="table table-responsive-md" runat="server" AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:BoundField DataField="TimetableGUID" HeaderText="Timetable GUID" ReadOnly="True" SortExpression="TimetabletGUID" Visible="false"/>
                                        <asp:BoundField DataField="Day2" HeaderText="Days/Time" ReadOnly="True" />
                                        <asp:BoundField DataField="TimeSlot1" HeaderText="8.30am-9.00am" ReadOnly="True" ItemStyle-HorizontalAlign="Center"/>
                                        <asp:BoundField DataField="TimeSlot2" HeaderText="9.00am-9.30am" ReadOnly="True" ItemStyle-HorizontalAlign="Center"/>
                                        <asp:BoundField DataField="TimeSlot3" HeaderText="9.30am-10.00am" ReadOnly="True" ItemStyle-HorizontalAlign="Center"/>
                                        <asp:BoundField DataField="TimeSlot4" HeaderText="10.00am-10.30am" ReadOnly="True" ItemStyle-HorizontalAlign="Center"/>
                                        <asp:BoundField DataField="TimeSlot5" HeaderText="10.30am-11.00am" ReadOnly="True" ItemStyle-HorizontalAlign="Center"/>
                                        <asp:BoundField DataField="TimeSlot6" HeaderText="11.00am-11.30am" ReadOnly="True" ItemStyle-HorizontalAlign="Center"/>
                                        <asp:BoundField DataField="TimeSlot7" HeaderText="11.30am-12.00pm" ReadOnly="True" ItemStyle-HorizontalAlign="Center"/>
                                        <asp:BoundField DataField="TimeSlot8" HeaderText="12.00am-12.30am" ReadOnly="True" ItemStyle-HorizontalAlign="Center"/>
                                        <asp:BoundField DataField="TimeSlot9" HeaderText="12.30am-1.00am" ReadOnly="True" ItemStyle-HorizontalAlign="Center"/>
                                        <asp:BoundField DataField="TimeSlot10" HeaderText="1.00am-1.30am" ReadOnly="True" ItemStyle-HorizontalAlign="Center"/>
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
    <script src="script/jquery/jquery.min.js"></script>
</asp:Content>
