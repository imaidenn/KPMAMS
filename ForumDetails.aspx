<%@ Page Title="" Language="C#" MasterPageFile="~/user.Master" AutoEventWireup="true" CodeBehind="ForumDetails.aspx.cs" Inherits="KPMAMS.ForumDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/ForumDetails.css" rel="stylesheet" />
    <script>
        $(document).ready(function () {
            $.noConflict();
            $(".table").prepend($("<thead></thead>").html($(this).find("tr:first"))).dataTable({
                "searching": true,
                "pageLength": 10,
                "order": [[1, 'acs']],
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
    <div class="container pt-2 pb-2">
        <div class="row">
            <div class="col mx-auto">
                <div class="panel">
                    <div class="panel-body">
                        <div class="row d-flex">
                            <div class="col-md-10 form-group">
                                <h3>
                                    <asp:Label ID="lbTitle" runat="server" Text=" "></asp:Label>
                                </h3>
                                <asp:TextBox ID="tbTitle" style="font-size: 1.17em; font-weight: bolder" class="form-control" runat="server" Visible="false" placeholder="Title of forum" ></asp:TextBox>
                            </div>
                            <div class="col-md-2">
                                <div class="dropdown">
                                    <asp:LinkButton ID="lbMenu" CssClass="btn btn-info btn-block dropdown-toggle" role="button" type="button" data-toggle="dropdown" Visible="false" runat="server">More<i style="margin-left:10px;" class="fas fa-caret-down"></i></asp:LinkButton>
                                    <div class="dropdown-menu text-center">
                                        <asp:LinkButton ID="lbModify" CssClass="dropdown-item" runat="server" OnClick="lbModify_Click" >Modify</asp:LinkButton>
                                        <asp:LinkButton ID="lbDelete" CssClass="dropdown-item" style="color:red; font-weight: bold" runat="server" OnClick="lbDelete_Click">Delete</asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                                <asp:Label ID="lbCreated" runat="server" Text=" "></asp:Label>
                            </div>
                            <div class="col-md-3">
                                <asp:Label ID="lbLastUpdate" runat="server" Text=" "></asp:Label>
                            </div>
                            <div class="col-md-2">
                                <asp:Label ID="lbClass" runat="server" Text=" "></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <hr />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <p>
                                    <asp:Label ID="lbContent" runat="server" Text=" "></asp:Label>
                                    <div class="form-group">
                                        <asp:TextBox ID="tbContent" CssClass="form-control" Visible="false" TextMode="MultiLine" placeholder="Contents of forum" Rows="5" runat="server"></asp:TextBox>
                                    </div>
                                </p>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <hr />
                            </div>
                        </div>
                        <div class="row d-flex justify-content-end">
                            <div class="col-md-2 mr-auto">
                                <asp:Button ID="btnUpdate" CssClass="btn btn-block btn-warning" Visible="false" runat="server" Text="Update" OnClick="btnUpdate_Click" />
                            </div>
                            <div class="col-md-2">
                                <asp:Button ID="btnCancel" CssClass="btn btn-block btn-danger" Visible="false" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                            </div>
                            <div class="col-md-2">
                            </div>
                            <div class="col-md-3 text-right ">
                                <asp:Label ID="lbCreatedDate" runat="server" ></asp:Label>
                            </div>
                            <div class="col-md-3">
                                <div class="box-comments">
                                    <asp:Image ID="ImgProfilePic" runat="server" class="img-circle img-sm" />
                                    <div class="comment-text">
                                        <asp:Label ID="lbUserName" CssClass="username" runat="server" Text=""></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="container" id="divComment" runat="server" >
        <div class="row">
            <div class="col mx-auto">
                <div class="panel">
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-md-12">
                                <h3>Comment</h3>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <hr>
                            </div>
                        </div>
                        <div class="row d-flex">
                            <div class="col-md-12">
                                <h3>
                                    <asp:Label ID="lblNoData" runat="server" Text="No comment yet" Visible="false"></asp:Label>
                                </h3>
                                <asp:GridView class="table table-striped table-bordered table-responsive-md" ID="GvCommentList" runat="server" AutoGenerateColumns="False" DataKeyNames="CommentGUID" OnRowDataBound="GvCommentList_RowDataBound" OnRowCommand="GvCommentList_RowCommand" >
                                    <Columns>
                                        <asp:BoundField DataField="CommentGUID" HeaderText="Comment GUID" ReadOnly="True" SortExpression="CommentGUID" Visible="true"/>
                                        <asp:TemplateField HeaderText="Comment">
                                            <ItemTemplate>
                                                <div class="container-fluid">
                                                    <div class="row">
                                                        <div class="col-md-12">
                                                            <asp:Label ID="lblCommentBy" runat="server" Text='<%# Eval("CommentBy") %>' Font-Bold="True" ForeColor="#3333FF"></asp:Label>
                                                            comment on
                                                            <asp:Label ID="lblCommentDate" runat="server" Text='<%# Eval("CreateDate") %>'></asp:Label>      
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-md-12">
                                                            <asp:Label ID="lblCommentContent" runat="server" Text='<%# Eval("Content") %>'></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                            <asp:Button ID="btnModifyComment" runat="server" class="btn btn-block btn-info btn-sm" Text="Modify" CommandName="selectModify" CommandArgument="<%# Container.DataItemIndex %>" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <label>Comment :</label>
                                <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="tbComment" runat="server" TextMode="MultiLine" placeholder="Contents of comment" Rows="5"></asp:TextBox>
                                </div>
                                <div class="from-group">
                                    <asp:Button ID="btnSubmit" runat="server" class="btn btn-block btn-info btn-lg" Text="Post comment" OnClick="btnSubmit_Click"/>
                                    <asp:Button ID="btnDeleteComment" runat="server" class="btn btn-block btn-danger btn-lg" Text="Delete" Visible="false" OnClick="btnDeleteComment_Click" />
                                    <asp:Button ID="btnCancelModify" runat="server" class="btn btn-block btn-light btn-lg" Text="Cancel" Visible="false" OnClick="btnCancelModify_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script src="script/jquery/jquery.min.js"></script>
</asp:Content>
