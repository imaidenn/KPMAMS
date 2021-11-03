<%@ Page Title="" Language="C#" MasterPageFile="~/user.Master" AutoEventWireup="true" CodeBehind="CreateAssessment.aspx.cs" Inherits="KPMAMS.CreateAssessment" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/moment.js/2.15.1/moment.min.js"></script>
    <link rel="stylesheet" type="text/css" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datetimepicker/4.17.43/css/bootstrap-datetimepicker.min.css"> 
    <link rel="stylesheet" type="text/css" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datetimepicker/4.17.43/css/bootstrap-datetimepicker-standalone.css"> 
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datetimepicker/4.17.43/js/bootstrap-datetimepicker.min.js"></script>
    <script type="text/javascript">
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
    <div class="container">
        <div class="row">
            <div class="col-md-8 mx-auto">
                <div class="card pt-2 pb-2">
                     <div class="card-body">
                         <div class="row">
                             <div class="col">
                                 <h3>Create Assessment</h3>
                             </div>
                         </div>
                         <div class="row">
                             <div class="col">
                                 <hr>
                             </div>
                         </div>
                         <div class="row">
                             <div class="col-md-8">
                                 <label>Assessment Title:</label>
                                 <div class="form-group">
                                    <asp:TextBox class="form-control" ID="tbTitle" runat="server" placeholder="Title of assessment" AutoCompleteType="Disabled"></asp:TextBox>
                                     <asp:RequiredFieldValidator runat="server" id="reqTitle" controltovalidate="tbTitle" errormessage="Please enter title" ForeColor="Red" />
                                 </div>
                             </div>
                             <div class="col-md-4">
                                 <label>Class:</label>
                                 <div class="from-group">
                                    <asp:DropDownList class="form-control" ID="dlClassList" runat="server">
                                    </asp:DropDownList>
                                 </div>
                             </div>
                         </div>
                         <div class="row">
                             <div class="col-md-6">
                                <label>Due Date</label>
                                <div class="datetimepicker input-group date mb-lg">
                                    <asp:TextBox ID="tbDueDate" CssClass="form-control" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                    <span class="input-group-addon">
                                        <asp:LinkButton ID="lbClear" runat="server" OnClick="lbClear_Click"><i class="fas fa-minus-circle"></i></asp:LinkButton>
                                    </span>
                                 </div>
                             </div>
                             <div class="col-md-6">
                                <label>File:</label>
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <ajaxToolkit:AsyncFileUpload runat="server" ID="AsyncFileUpload1" Mode="Auto" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                             </div>
                         </div>
                         <div class="row">
                             <div class="col-md-12">
                                 <label>Description :</label>
                                 <div class="form-group">
                                    <asp:TextBox CssClass="form-control" ID="tbDesc" runat="server" TextMode="MultiLine" placeholder="Description of assessment" Rows="5" AutoCompleteType="Disabled"></asp:TextBox>
                                     <asp:RequiredFieldValidator runat="server" id="reqDesc" controltovalidate="tbDesc" errormessage="Please enter description" ForeColor="Red"/>
                                 </div>
                                 <div class="from-group">
                                     <asp:Button ID="btnCreate" runat="server" class="btn btn-block btn-info btn-lg" Text="Create Assessment" OnClick="btnCreate_Click" />
                                 </div>
                             </div>
                         </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
