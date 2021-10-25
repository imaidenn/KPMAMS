<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/admin.Master" AutoEventWireup="true" CodeBehind="ResultEntry.aspx.cs" Inherits="KPMAMS.Admin.ResultEntry" %>
<asp:Content ID="Content1" ContentPlaceHolderID="BodyContent" runat="server">
    <div class="panel panel-default">
               <div class="panel-heading">Result</div>
               <div class="panel-body">
                   <br />
                    <div class="row">
                           <div class="col-md-12">
                              <div class="box-placeholder">
                                 <div class="row">
                                    <div class="col-md-6">
                                       <label class="col-sm-3 control-label">Name :</label>
                                    <div class="col-sm-5">
                                    <asp:Label ID="lblName" runat="server"></asp:Label>
                           </div>
                                    </div>
                                    <div class="col-md-6">
                                          <label class="col-sm-3 control-label">Class :</label>
                                    <div class="col-sm-5">
                                    <asp:Label ID="lblClass" runat="server"></asp:Label>
                           </div>
                                    </div>
                                     <br />
                                     <div class="col-md-6">
                                          <label class="col-sm-3 control-label">IC No:</label>
                                    <div class="col-sm-5">
                                    <asp:Label ID="lblIC" runat="server"></asp:Label>
                           </div>
                                    </div>
                                     <div class="col-md-6">
                                          <label class="col-sm-3 control-label">Gender :</label>
                                    <div class="col-sm-5">
                                    <asp:Label ID="lblGender" runat="server"></asp:Label>
                           </div>
                                    </div>
                                 </div>
                              </div>
                           </div>
                        <br />

                   <div class="panel-body">
                        <asp:GridView ID="GridView1" runat="server" AllowSorting="True" CssClass="table table-bordered table-condensed table-responsive table-hover" Enabled="False" Width="100%" AutoGenerateColumns="False" DataKeyNames="SubjectGUID"  > 
                                <Columns>                              

                                    <asp:BoundField DataField="SubjectGUID" HeaderText="SubjectGUID" SortExpression="SubjectGUID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                    <asp:BoundField DataField="SubjectName" HeaderText="Subject Name" SortExpression="SubjectName" />
                                       <asp:BoundField DataField="Mark" HeaderText="Mark" SortExpression="Mark" />
                                       <asp:BoundField DataField="Grade" HeaderText="Grade" SortExpression="Grade" />                   

                                </Columns>
                            </asp:GridView>
                            <asp:Label id="lblNoData" runat="server" Visible="false" Text="No data to Display"></asp:Label>
                     </div>
                   <br />

                   <div class="panel-body">
                            <div class="row">
                           <div class="col-md-12">
                              <div class="box-placeholder">
                                 <div class="row">
                                    <div class="col-md-6">
                                       <label class="col-sm-3 control-label">Place in Class :</label>
                                    <div class="col-sm-5">
                                    <asp:Label ID="lblplclass" runat="server"></asp:Label>
                           </div>
                                    </div>
                                    <div class="col-md-6">
                                          <label class="col-sm-3 control-label">Place in Form :</label>
                                    <div class="col-sm-5">
                                    <asp:Label ID="lblplform" runat="server"></asp:Label>
                           </div>
                                    </div>
                                     <br />
                                     <div class="col-md-6">
                                          <label class="col-sm-3 control-label">GPA :</label>
                                    <div class="col-sm-5">
                                    <asp:Label ID="lblgpa" runat="server"></asp:Label>
                           </div>
                                    </div>
                                     <div class="col-md-6">
                                          <label class="col-sm-3 control-label">CGPA :</label>
                                    <div class="col-sm-5">
                                    <asp:Label ID="lblcgpa" runat="server"></asp:Label>
                           </div>
                                    </div>
                                     <br />
                                     <div class="col-md-6">
                                          <label class="col-sm-3 control-label">Average Mark :</label>
                                    <div class="col-sm-5">
                                    <asp:Label ID="lblAvgMark" runat="server"></asp:Label>
                           </div>
                                    </div>
                                 </div>
                              </div>
                           </div>
                        </div>

                        </div>

                     </div>
                                </div>
</asp:Content>
