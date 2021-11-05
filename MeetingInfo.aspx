<%@ Page Title="" Language="C#" MasterPageFile="~/user.Master" AutoEventWireup="true" CodeBehind="MeetingInfo.aspx.cs" Inherits="KPMAMS.MeetingInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
        <style type="text/css">
     .hidden
     {
         display:none;
     }
</style>
    <div class="panel panel-default">
               <div class="panel-heading">Meeting Info</div>
               <div class="panel-body">
                  <form method="get" action="/" class="form-horizontal">
                     <fieldset>
                        <div class="form-group">
                           <label class="col-sm-1 control-label">Meeting For Class:</label>
                           <div class="col-sm-5">
                               <asp:TextBox ID="txtClass" runat="server" CssClass="form-control form-control-rounded" Enabled="False"></asp:TextBox>
                           </div>
                           <label class="col-sm-1 control-label">Meeting For:</label>
                           <div class="col-sm-5">
                               <asp:TextBox ID="txtTopic" runat="server" CssClass="form-control form-control-rounded" Enabled="False"></asp:TextBox>
                           </div>
                        </div>
                     </fieldset>
                      <br />
                      <fieldset>
                        <div class="form-group">
                           <label class="col-sm-1 control-label">Meeting Room ID:</label>
                           <div class="col-sm-5">
                               <asp:TextBox ID="txtMeetingID" runat="server" CssClass="form-control form-control-rounded" Enabled="False"></asp:TextBox>
                           </div>
                           <label class="col-sm-1 control-label">Meeting Room Passcode:</label>
                           <div class="col-sm-5">
                               <asp:TextBox ID="txtMeetingPass" runat="server" CssClass="form-control form-control-rounded" Enabled="False"></asp:TextBox>
                           </div>
                        </div>
                     </fieldset>
                      <br />
                      <fieldset>
                        <div class="form-group">
                           <label class="col-sm-1 control-label">Meeting Duration:</label>
                           <div class="col-sm-5">
                               <asp:TextBox ID="txtDuration" runat="server" CssClass="form-control form-control-rounded" Enabled="False"></asp:TextBox>
                           </div>
                           <label class="col-sm-1 control-label">Created On:</label>
                           <div class="col-sm-5">
                               <asp:TextBox ID="txtDate" runat="server" CssClass="form-control form-control-rounded" Enabled="False"></asp:TextBox>
                           </div>
                        </div>
                     </fieldset>
                      <br />
                      <fieldset>
                        <div class="form-group">
                            <div style="float:right">
                                 <button type="submit" class="btn btn-primary" id="join_meeting">Join</button>
                                <button type="submit" class="btn btn-primary" id="back" runat="server" OnClick="btnBack_Click">Back</button>
<%--                                <asp:Button ID="btnDelete" runat="server" CssClass="btn-danger" Text="Delete" OnClick="btnDelete_Click" Width="100px"/>--%>
<%--                            <asp:Button ID="btnBack" runat="server" CssClass="btn-default" Text="Back" OnClick="btnBack_Click" Width="100px"/>--%>
                            </div>
                           

                        </div>
                     </fieldset>
                      <asp:TextBox ID="txtRole" runat="server" Class="hidden"></asp:TextBox>
                      <asp:TextBox ID="txtName" runat="server" Class="hidden"></asp:TextBox>


                      <input type="text" name="meeting_role" id="meeting_role" value="" Class="hidden">
                      <input type="text" name="meeting_china" id="meeting_china" value="0" Class="hidden" >
                      <input type="text" name="meeting_lang" id="meeting_lang" value="en-US" Class="hidden">

                        <input type="text" name="display_name" id="display_name" value="2.0.1#CDN" maxlength="100" placeholder="Name" required="" Class="hidden">
                        <input type="text" name="meeting_number" id="meeting_number" value="" maxlength="200" style="width:150px" placeholder="Meeting Number"  required=""  >
                        <input type="text" name="meeting_pwd" id="meeting_pwd" value="" style="width:150px" maxlength="32" placeholder="Meeting Password" >
                        <input type="text" name="meeting_email" id="meeting_email" value="" style="width:150px" maxlength="32" placeholder="Email option" Class="hidden" >
                  </form>
               </div>
            </div>
                <script src="Scripts/tool.js"></script>
            <script src="Scripts/vconsole.min.js"></script>
            <script src="Scripts/index.js"></script>



        	<!-- For either view: import Web Meeting SDK JS dependencies -->
	<script src="https://source.zoom.us/2.0.1/lib/vendor/react.min.js"></script>
	<script src="https://source.zoom.us/2.0.1/lib/vendor/react-dom.min.js"></script>
	<script src="https://source.zoom.us/2.0.1/lib/vendor/redux.min.js"></script>
	<script src="https://source.zoom.us/2.0.1/lib/vendor/redux-thunk.min.js"></script>
	<script src="https://source.zoom.us/2.0.1/lib/vendor/lodash.min.js"></script>
<%--            <script src="https://source.zoom.us/2.0.1/lib/vendor/jquery.min.js"></script>--%>

	
	<!-- For Client View -->
	<!-- For Client View -->
	<script src="https://source.zoom.us/zoom-meeting-2.0.1.min.js"></script>

        <script>


            var meetingnum = document.getElementById('<%=txtMeetingID.ClientID%>').value;
            var meetingpwd = document.getElementById('<%=txtMeetingPass.ClientID%>').value;
        var role = document.getElementById('<%=txtRole.ClientID%>').value;
        var name = document.getElementById('<%=txtName.ClientID%>').value;

            $('#meeting_number').val(meetingnum);
            $('#meeting_pwd').val(meetingpwd);
/*            document.getElementById('meeting_number').value = meetingnum;*/
/*            document.getElementById('meeting_pwd').value = meetingpwd;*/
            document.getElementById('meeting_role').value = role;
            document.getElementById('display_name').value = name;
        </script>
</asp:Content>
