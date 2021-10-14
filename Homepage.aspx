<%@ Page Language="C#" MasterPageFile="~/user.Master" AutoEventWireup="true" CodeBehind="Homepage.aspx.cs" Inherits="KPMAMS.Homepage" EnableEventValidation="false" %>


<asp:Content ID="Content1" ContentPlaceHolderID="BodyContent" runat="server">
<div class="row">
                     <div class="col-lg-12">
                        <!-- START panel-->
                        <div class="panel panel-default">
                           <div class="panel-heading">Announcements
<%--                              <a href="javascript:void(0);" data-perform="panel-dismiss" data-toggle="tooltip" title="" class="pull-right" data-original-title="Close Panel">
                                 <em class="fa fa-times"></em>
                              </a>--%>
<%--                              <a href="javascript:void(0);" data-perform="panel-collapse" data-toggle="tooltip" title="" class="pull-right" data-original-title="Collapse Panel">
                                 <em class="fa fa-minus"></em>
                              </a>--%>
                           </div>
                            <asp:GridView ID="GridView1" runat="server" AllowSorting="True" CssClass="table table-bordered table-condensed table-responsive table-hover" Enabled="False" Width="100%" AutoGenerateColumns="False" DataKeyNames="AnnouncementGUID" OnRowDataBound="GridView1_RowDataBound" > 
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:HyperLink ID="hlView" Text="View" runat="server" ></asp:HyperLink>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="AnnouncementGUID" HeaderText="AnnouncementGUID" ReadOnly="True" SortExpression="AnnouncementGUID" Visible="false"/>
                                    <asp:BoundField DataField="AnnouncementTitle" HeaderText="Title" SortExpression="AnnouncementTitle" />
                                    <asp:BoundField DataField="CreateDate" HeaderText="Date" SortExpression="CreateDate" />
                                </Columns>
                            </asp:GridView>
                           
                        </div>
                        <!-- END panel-->
                     </div>
                  </div>
    </asp:Content>
