<%@ Page Title="" Language="C#" MasterPageFile="~/user.Master" AutoEventWireup="true" CodeBehind="ParentDetails.aspx.cs" Inherits="KPMAMS.ParentDetails"  EnableEventValidation="false"%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <div class="panel panel-default">
            <div class="panel-heading">Profile Info</div>
            <div class="panel-body">
                <form method="get" action="/" class="form-horizontal">
                    <fieldset>
<%--                        <legend>Classic inputs</legend>--%>
                    <div class="form-group">
                        <label class="col-sm-1 control-label">Parent ID</label>
                        <div class="col-sm-5">
                            <asp:TextBox ID="txtParentID" runat="server" CssClass="form-control form-control-rounded" Enabled="False"></asp:TextBox>
                        </div>
                        <label class="col-sm-1 control-label">Full Name</label>
                        <div class="col-sm-5">
                            <asp:TextBox ID="txtName" runat="server" CssClass="form-control form-control-rounded" Enabled="False"></asp:TextBox>
                        </div>
                    </div>
                    </fieldset>
                    <br />
                    <fieldset>
                    <div class="form-group">
                        <label class="col-sm-1 control-label">IC Number</label>
                        <div class="col-sm-5">
                            <asp:TextBox ID="txtICno" runat="server" CssClass="form-control form-control-rounded" Enabled="False"></asp:TextBox>
                        </div>
                    </div>
                    </fieldset>
                    

                    <fieldset>
                    <div class="form-group">
                        <label class="col-sm-5 control-label">Student list</label>
                        <div class="col-sm-12">
                            <asp:GridView class="" ID="GvStudentList" CssClass="table table-striped table-responsive-md" runat="server" AutoGenerateColumns="False">
                                <Columns>
                                    <asp:BoundField DataField="StudentUserID" HeaderText="Student ID" ReadOnly="True" SortExpression="StudentUserID" />
                                    <asp:BoundField DataField="fullName" HeaderText="Student Name" ReadOnly="True" SortExpression="fullName"/>
                                    <asp:BoundField DataField="ICNo" HeaderText="IcNo" ReadOnly="True" SortExpression="ICNo"/>
                                    <asp:BoundField DataField="Class" HeaderText="Class" ReadOnly="True" SortExpression="Class" />
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                    </fieldset>    
                </form>
            </div>
        </div>
</asp:Content>
