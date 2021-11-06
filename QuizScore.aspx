<%@ Page Title="" Language="C#" MasterPageFile="~/user.Master" AutoEventWireup="true" CodeBehind="QuizScore.aspx.cs" Inherits="KPMAMS.QuizScore" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <div class="panel panel-default">
               <div class="panel-heading">Scoreboard</div>
               <div class="panel-body">
               <asp:Repeater ID="Repeater1" runat="server" >
                   <ItemTemplate>
            <div style="float:left" class="col-lg-12">
                <asp:Label ID="lblAnswerGUID" runat="server" Visible="false" Text='<%#Eval("AnswerGUID")%>' ></asp:Label>
                <asp:Label ID="lblPosition" runat="server" class="col-sm-1 control-label" Text='<%#Eval("Position")%>' Font-Bold="true" Font-Size="X-Large"></asp:Label>&nbsp
            <asp:Label ID="lblStudentName" runat="server" class="col-sm-6 control-label" Text='<%#Eval("FullName")%>' Font-Bold="true" Font-Size="X-Large"></asp:Label>&nbsp
                <asp:Label ID="lblScore" runat="server" class="col-sm-3 control-label" Text='<%#Eval("TotalScore")%>' Font-Bold="true" Font-Size="X-Large"></asp:Label>&nbsp
            </div>
                       <br />

            <hr></hr>
        </ItemTemplate>
               </asp:Repeater>   
                   <fieldset>
                    
                        <div class="form-group">
                            <div style="float:right">
                                 <asp:Button ID="btnBack" CssClass="btn btn-primary" runat="server" Text="Back" OnClick="btnBack_Click" />
                            </div>
                           

                        </div>
                     </fieldset>

               


               </div>
            </div>
</asp:Content>
