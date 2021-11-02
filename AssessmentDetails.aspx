<%@ Page Title="" Language="C#" MasterPageFile="~/user.Master" AutoEventWireup="true" CodeBehind="AssessmentDetails.aspx.cs" Inherits="KPMAMS.AssessmentDetails" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/ForumDetails.css" rel="stylesheet" />
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/moment.js/2.15.1/moment.min.js"></script>
    <link rel="stylesheet" type="text/css" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datetimepicker/4.17.43/css/bootstrap-datetimepicker.min.css"> 
    <%--<link rel="stylesheet" type="text/css" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datetimepicker/4.17.43/css/bootstrap-datetimepicker-standalone.css">--%> 
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datetimepicker/4.17.43/js/bootstrap-datetimepicker.min.js"></script>
    <script type="text/javascript">
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
        $(function () {
            $('[id*=tbDueDate]').datetimepicker({
                format: 'DD/MM/YYYY hh:mm A',
                icons: {
                    time: "fa fa-clock-o",
                    date: "fa fa-calendar",
                    up: "fa fa-chevron-up",
                    down: "fa fa-chevron-down",
                    previous: 'fa fa-chevron-left',
                    next: 'fa fa-chevron-right',
                    today: 'fa fa-screenshot',
                    clear: 'fa fa-trash',
                    close: 'fa fa-remove'
                },
                minDate: moment()
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" ></asp:ScriptManager>
    <div class="container-fluid pt-2 pb-2">
        <div class="row">
            <div id="divAssessmentDetails" runat="server" class="col-md-8 mx-auto">
                <div class="panel">
                    <div class="panel-body">
                        <div class="row d-flex">
                            <div class="col-md-10 form-group">
                                <h3>
                                    <asp:Label ID="lbTitle" runat="server" Text=" "></asp:Label>
                                </h3>
                                <asp:TextBox ID="tbTitle" style="font-size: 1.17em; font-weight: bolder" class="form-control" runat="server" Visible="false" placeholder="Title of assessment" ></asp:TextBox>
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
                            <div class="col-md-3">
                            <asp:Label ID="lbDueDate" runat="server" Text=" "></asp:Label>
                                <div id="divDueDate" class="datetimepicker input-group date mb-lg" runat="server">
                                    <asp:TextBox ID="tbDueDate" CssClass="form-control" runat="server"></asp:TextBox>
                                    <span class="input-group-addon">
                                        <asp:LinkButton ID="lbClear" runat="server" OnClick="lbClear_Click"><i class="fas fa-minus-circle"></i></asp:LinkButton>
                                    </span>
                                </div>
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
                                    <asp:Label ID="lbDesc" runat="server" Text=" "></asp:Label>
                                    <div class="form-group">
                                        <asp:TextBox ID="tbDesc" CssClass="form-control" Visible="false" TextMode="MultiLine" placeholder="Description of aseessment" Rows="5" runat="server"></asp:TextBox>
                                    </div>
                                </p>
                                <div id="divFile" style="margin-top:30px;" runat="server">
                                    <label>Additional file</label><br />
                                    <asp:HyperLink ID="hlFile" target="_blank" runat="server">
                                    </asp:HyperLink>
                                    <span id="spanlb" class="badge" runat="server">
                                        <asp:LinkButton ID="lbClearFile" runat="server" Onclick="lbClearFile_Click"><i class="fas fa-minus-circle"></i></asp:LinkButton>
                                        <asp:LinkButton ID="lbDownload" target="_blank" runat="server" ><i class="fas fa-download"></i></asp:LinkButton>
                                    </span>
                                    <asp:UpdatePanel ID="UpdatePanel" runat="server">
                                        <ContentTemplate>
                                            <ajaxToolkit:AsyncFileUpload runat="server" ID="AsyncFileUpload1" Mode="Auto" Visible="false" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
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
                            <div class="col-md-3">
                                <asp:Label ID="lbCreatedDate" runat="server" Text=""></asp:Label>
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
            <div id="divSubmission" class="col-md-4 mx-auto" runat="server">
                <div class="panel">
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-md-12">
                                <h3>Submission</h3>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <hr>
                            </div>
                        </div>
                        <div class="row d-flex">
                            <div class="col-md-12">
                                <div class="row">
                                    <div class="col-md-12 text-right">
                                        <asp:Label ID="lbSubmitDate" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12" style="margin-top:10px;">
                                        <asp:HyperLink ID="hlSubmitFile" target="_blank" runat="server"></asp:HyperLink>
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <ajaxToolkit:AsyncFileUpload runat="server" ID="AsyncFileUpload2" Visible="false" />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <hr>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="from-group">
                                    <asp:Button ID="btnSubmit" runat="server" class="btn btn-block btn-info btn-sm" Text="Submit" Onclick="btnSubmit_Click"/>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div id="divSubmissionList" runat="server" class="col-12 mx-auto">
                <div class="panel">
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-md-12">
                                <h3>Submission List</h3>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <hr>
                            </div>
                            <div class="col-md-9">
                            </div>
                            <div class="col-md-3" >
                                <asp:DropDownList class="form-control" ID="dlStatus" runat="server" Visible="true" OnSelectedIndexChanged ="dlStatus_SelectedIndexChanged" AutoPostBack="true" >
                                    <asp:ListItem Text="Submitted" Value="Submitted" />
                                    <asp:ListItem Text="Pending" Value="Pending" />
                                    <asp:ListItem Text="Late submit" Value="lateSubmit" />
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="row d-flex">
                            <div class="col-md-12">
                                <h3>
                                    <asp:Label ID="lblNoData" runat="server" Text="No data yet" Visible="false"></asp:Label>
                                </h3>
                                <asp:GridView class="table table-striped table-bordered table-responsive-md" ID="GvSubmissionList" runat="server" AutoGenerateColumns="False" DataKeyNames="SubmissionGUID" OnRowDataBound="GvSubmissionList_RowDataBound" >
                                    <Columns>
                                        <asp:BoundField DataField="SubmissionGUID" HeaderText="Submission GUID" ReadOnly="True" SortExpression="SubmissionGUID" Visible="false"/>
                                        <asp:BoundField DataField="FullName" HeaderText="Student Name" ReadOnly="True" SortExpression="FullName"/>
                                        <asp:BoundField DataField="LastUpdateDate" HeaderText="Submit Date" ReadOnly="True" SortExpression="LastUpdateDate" />
                                        <asp:BoundField DataField="File" HeaderText="File" ReadOnly="True" SortExpression="File"/>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <%--<asp:Button ID="btnView" class="btn btn-block btn-info btn-sm" runat="server" Text="View"/>--%>
                                                <asp:LinkButton ID="lbView" target="_blank" class="btn btn-block btn-info btn-sm" runat="server">View</asp:LinkButton>
                                                <%--<asp:HyperLink ID="hlView" Text="View" target="_blank" runat="server" ></asp:HyperLink>--%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbDownload" target="_blank" class="btn btn-block btn-info btn-sm" runat="server">Download</asp:LinkButton>
                                                <%--<asp:Button ID="btnDownload" class="btn btn-block btn-info btn-sm" runat="server" Text="Download" />--%>
                                                <%--<asp:HyperLink ID="hlDownload" Text="Download" target="_blank" runat="server" ></asp:HyperLink>--%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
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
