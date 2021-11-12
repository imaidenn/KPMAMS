<%@ Page Language="C#" MasterPageFile="~/Admin/admin.Master" ValidateRequest="false" AutoEventWireup="true" CodeBehind="AnnouncementEntry.aspx.cs" Inherits="KPMAMS.Admin.AnnouncementEntry" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="BodyContent" runat="server">
    <!-- include libraries(jQuery, bootstrap) -->
<link href="https://stackpath.bootstrapcdn.com/bootstrap/3.4.1/css/bootstrap.min.css" rel="stylesheet">
<script src="https://code.jquery.com/jquery-3.5.1.min.js"></script>
<script src="https://stackpath.bootstrapcdn.com/bootstrap/3.4.1/js/bootstrap.min.js"></script>

<!-- include summernote css/js -->
<link href="https://cdn.jsdelivr.net/npm/summernote@0.8.18/dist/summernote.min.css" rel="stylesheet">
<script src="https://cdn.jsdelivr.net/npm/summernote@0.8.18/dist/summernote.min.js"></script>
<%--     <script>

         $(document).ready(function () {
             $('#summernote').summernote
             var markupStr = 'hello world';
             $('#summernote').summernote('code', markupStr);
             $("#summernote").on('summernote.blur', function () {
/*                 $('#summernote').html($('#summernote').summernote('code'));*/
                 var html = $('#summernote').summernote('code');
             });

         });
<%--         $('#btnSave').click(function () {
             var markupStr = $('#summernote').summernote('code');
             var hiddenControl = '<%= hiddenControl.ClientID %>';
                          document.getElementById(hiddenControl).value = markupStr;
                          //or some other code to get your value
                      }  

     </script>--%>

    <script src="/Content/SummerNote/summernote.js"></script>
    <script>
        $(function () {
            // Set up your summernote instance
            $("#<%= txtSummernote.ClientID %>").summernote();
            focus: true
            // When the summernote instance loses focus, update the content of your <textarea>
            $("#<%= txtSummernote.ClientID %>").on('summernote.blur', function () {
                $('#<%= txtSummernote.ClientID %>').html($('#<%= txtSummernote.ClientID %>').summernote('code'));
           });
        });
    </script>
    <script type="text/javascript">
        function funcMyHtml() {
            debugger;
            document.getElementById("#<%= txtSummernote.ClientID %>").value = $('#<%= txtSummernote.ClientID %>').summernote('code');
<%--            var test = document.getElementById("#<%= txtSummernote.ClientID %>").value
            $('#summernote').summernote('code', test);--%>
        }
    </script>


    <div class="panel panel-default">
               <div class="panel-heading"><h3>Announcement Details</h3></div>
               <div class="panel-body">
                  <form method="get" action="/" class="form-horizontal">
                     <fieldset>
<%--                        <legend>Classic inputs</legend>--%>
                        <div class="form-group">
                           <label class="col-sm-2 control-label">Title</label>
                           <div class="col-sm-10">
                               <asp:TextBox ID="txtTitle" placeholder="Enter Title" runat="server" CssClass="form-control form-control-rounded" MaxLength="100"></asp:TextBox>
                           </div>
                        </div>
                     </fieldset>
                      <br />
                      <fieldset>
<div class="form-group">
    <asp:Label ID="lblSummernote" runat="server" Text="Description" AssociatedControlID="txtSummernote" CssClass="control-label col-md-2"></asp:Label>
    <br /><br />
        <div class="col-md-15">
            <asp:TextBox ID="txtSummernote" runat="server" TextMode="MultiLine" Rows="2" MaxLength="500"></asp:TextBox>

      </div>
 </div> 
 
                     </fieldset>
                      <br />
                      
             <fieldset>
                        <div class="form-group">
                            <div style="float:right">
                                <asp:Button ID="btnSave" runat="server" CssClass="btn-success" Text="Save" OnClick="btnSave_Click" Width="100px"/>
                            <asp:Button ID="btnUpdate" runat="server" CssClass="btn-success" Text="Update" OnClick="btnUpdate_Click" Width="100px" Visible="false"/>
                            <asp:Button ID="btnRemove" runat="server" CssClass="btn-danger" Text="Remove" OnClick="btnRemove_Click" Width="100px" Visible="false"/>
                            </div>
                           

                        </div>
                     </fieldset>

                     
                  </form>
               </div>
            </div>
    </asp:Content>

