<%@ Page Title="" Language="C#" MasterPageFile="~/user.Master" AutoEventWireup="true" CodeBehind="AssessmentList.aspx.cs" Inherits="KPMAMS.AssessmentList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <div class="p-4 col">
        <div class="row">
            <div class="col">
                <center>
                    <h3>Assessment List</h3>
                </center>
            </div>
        </div>
        <div class="row">
            <div class="col">
                <hr>
            </div>
        </div>
        <div class="row">
            <div class="col-md-7">
                <h4>
                    <asp:Label ID="lbClass" runat="server" Text=""></asp:Label>
                </h4>
            </div>
            <div class="col-md-2">
                <asp:Button ID="btnCreateAssessment" class="btn btn-info btn-block btn-sm" Visible="false" runat="server" Text="Create" OnClick="btnCreateAssessment_Click" />
            </div>
            <div class="col-md-3">
                <div class="from-group">
                    <asp:DropDownList class="form-control" ID="dlClassList" runat="server" Visible="false" OnSelectedIndexChanged ="dlClassList_SelectedIndexChanged" AutoPostBack="true" >
                    </asp:DropDownList>
                    <asp:DropDownList class="form-control" ID="dlStatus" runat="server" Visible="true" OnSelectedIndexChanged ="dlStatus_SelectedIndexChanged" AutoPostBack="true" >
                        <asp:ListItem Text="Assign" Value="Assign" />
                        <asp:ListItem Text="Submitted" Value="Submitted" />
                        <asp:ListItem Text="Missing" Value="Missing" />
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12 pt-3">
                <center>
                    <h3>
                        <asp:Label ID="lblNoData" runat="server" Visible="false" Text="No Data Found"></asp:Label>
                    </h3> 
                </center>
                <asp:GridView class="" ID="GvAssessmentList" CssClass="table table-striped table-responsive-md" runat="server" AutoGenerateColumns="False" OnRowDataBound="GvAssessmentList_RowDataBound" >
                    <Columns>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:HyperLink ID="hlView" Text="View" runat="server" ></asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="AssessmentGUID" HeaderText="Asessment GUID" ReadOnly="True" SortExpression="AsessmentGUID" Visible="false"/>
                        <asp:BoundField DataField="CreateBy" HeaderText="Create By" ReadOnly="True" SortExpression="CreateDate" HtmlEncode="false" />
                        <asp:BoundField DataField="LastUpdateDate" HeaderText="Last Update Date" ReadOnly="True" SortExpression="LastUpdateDate" />
                        <asp:BoundField DataField="Title" HeaderText="Title" ReadOnly="True" SortExpression="Title"/>
                        <asp:BoundField DataField="DueDate" HeaderText="Due Date" ReadOnly="True" SortExpression="DueDate"/>
                    </Columns>
                </asp:GridView>
            </div>
        </div>        
    </div>
    <script src="script/jquery/jquery.min.js"></script>
</asp:Content>
