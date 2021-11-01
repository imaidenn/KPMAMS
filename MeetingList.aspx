<%@ Page Title="" Language="C#" MasterPageFile="~/user.Master" AutoEventWireup="true" CodeBehind="MeetingList.aspx.cs" Inherits="KPMAMS.MeetingList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
        <div class="panel panel-default">
               <div class="panel-heading">Meeting</div>
               <div class="panel-body">
    <div class="col-lg-12">
                  <div class="panel panel-default">
                     <div class="panel-heading">
                         <asp:Label ID="lblTotalQty" runat="server"></asp:Label>
                     </div>
                     <div class="panel-body">
                        <asp:GridView ID="GridView1" runat="server" AllowSorting="True" CssClass="table table-bordered table-condensed table-responsive table-hover" Enabled="False" Width="100%" AutoGenerateColumns="False" DataKeyNames="MeetingGUID" OnRowDataBound="GridView1_RowDataBound" > 
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:HyperLink ID="hlView" Text="View" runat="server" ></asp:HyperLink>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="MeetingGUID" HeaderText="MeetingGUID" ReadOnly="True" SortExpression="MeetingGUID" Visible="false"/>
                                    <asp:BoundField DataField="MeetingTopic" HeaderText="MeetingTopic" SortExpression="MeetingTopic"/>
                                    <asp:BoundField DataField="MeetingTime" HeaderText="MeetingTime" SortExpression="MeetingTime"/>
                                    <asp:BoundField DataField="Duration" HeaderText="Duration" SortExpression="Duration"/>
                                    <asp:BoundField DataField="Class" HeaderText="Class" SortExpression="Class"/>

                                </Columns>
                            </asp:GridView>
                            <asp:Label id="lblNoData" runat="server" Visible="false" Text="No data to Display"></asp:Label>
                     </div>
                  </div>
               </div>
                   </div>
            </div>

</asp:Content>
