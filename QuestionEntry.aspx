<%@ Page Title="" Language="C#" MasterPageFile="~/user.Master" AutoEventWireup="true" CodeBehind="QuestionEntry.aspx.cs" Inherits="KPMAMS.QuestionEntry" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <div class="panel panel-default">
               <div class="panel-heading"><h3>Create Quiz</h3></div>
               <div class="panel-body">
                  <fieldset>
                        <div class="form-group">
                           <label class="col-sm-1 control-label">Question</label>
                           <div class="col-lg-12">
                               <asp:TextBox ID="txtQuestion" runat="server" CssClass="form-control form-control-rounded"></asp:TextBox>
                           </div>
                        </div>
                     </fieldset>
                   <br />
                   <fieldset>
                        <div class="form-group">
                           <label class="col-sm-1 control-label">Option 1</label>
                            <asp:RadioButton ID="rbAns1" GroupName="Answer" runat="server" class="col-sm-1"/>
                           <div class="col-sm-10">
                               <asp:TextBox ID="txtAnswer1" runat="server" CssClass="form-control form-control-rounded"></asp:TextBox>
                           </div>
                        </div>
                     </fieldset>
                   <br />
                   <fieldset>
                        <div class="form-group">
                            <label class="col-sm-1 control-label">Option 2</label>
                            <asp:RadioButton ID="rbAns2" GroupName="Answer" runat="server" class="col-sm-1"/>
                           <div class="col-sm-10">
                               <asp:TextBox ID="txtAnswer2" runat="server" CssClass="form-control form-control-rounded"></asp:TextBox>
                           </div>
                        </div>
                     </fieldset>
                   <br />
                   <fieldset>
                        <div class="form-group">
                            <label class="col-sm-1 control-label">Option 3</label>
                            <asp:RadioButton ID="rbAns3" GroupName="Answer" runat="server" class="col-sm-1"/>
                           <div class="col-sm-10">
                               <asp:TextBox ID="txtAnswer3" runat="server" CssClass="form-control form-control-rounded"></asp:TextBox>
                           </div>
                        </div>
                     </fieldset>
                   <br />
                   <fieldset>
                        <div class="form-group">
                            <label class="col-sm-1 control-label">Option 4</label>
                            <asp:RadioButton ID="rbAns4" GroupName="Answer" runat="server" class="col-sm-1"/>
                           <div class="col-sm-10">
                               <asp:TextBox ID="txtAnswer4" runat="server" CssClass="form-control form-control-rounded"></asp:TextBox>
                           </div>
                        </div>
                     </fieldset>
                   <br />
                    <fieldset>
                    
                        <div class="form-group">
                            <div style="float:right">
                                <asp:Button ID="btnUpdate" CssClass="btn btn-primary" runat="server" Text="Update" OnClick="btnUpdate_Click" Visible="false"/>
                                <asp:Button ID="btnDelete" CssClass="btn btn-primary" runat="server" Text="Delete" OnClick="btnDelete_Click" Visible="false" />
                               <asp:Button ID="btnAdd" CssClass="btn btn-primary" runat="server" Text="Add" OnClick="btnAdd_Click" />
                                <asp:Button ID="btnBack" CssClass="btn btn-primary" runat="server" Text="Back" OnClick="btnBack_Click" />
                            </div>
                           

                        </div>
                     </fieldset>


               </div>
            </div>
</asp:Content>
