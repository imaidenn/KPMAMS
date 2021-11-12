<%@ Page Title="" Language="C#" MasterPageFile="~/user.Master" AutoEventWireup="true" CodeBehind="ResultDetails.aspx.cs" Inherits="KPMAMS.ResultDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <script>
        function printpage() {

            var value = document.getElementById("<%=ddlSem.ClientID%>");
            var gettext = value.options[value.selectedIndex].text;

            var getpanel = document.getElementById("<%= panel1.ClientID%>");
    var MainWindow = window.open('', '', 'height=600,width=1000');
            MainWindow.document.write('<html><head><title>Result For ' + gettext+'</title>');
    MainWindow.document.write('</head><body>');
    MainWindow.document.write(getpanel.innerHTML);
    MainWindow.document.write('</body></html>');
    MainWindow.document.close();
    setTimeout(function () {
        MainWindow.print();
    }, 500);
    return false;

}
    </script>
    <div class="panel panel-default">
               <div class="panel-heading"><h3>Result</h3></div>
               <div class="panel-body">
                   <div class="form-group">
                           <label class="col-sm-2 control-label">Exam Semester For:</label>
                           <div class="col-sm-5">
                               <asp:DropDownList ID="ddlSem" runat="server" OnSelectedIndexChanged="ddlSem_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                           </div>

                        </div>
                   <br />
                   

                   
                   <asp:Panel ID="panel1" runat="server">

                       <div class="row">
                                              <div class="panel-body">
                            <div class="row">
                           <div class="col-md-12">
                              <div class="box-placeholder">
                           <table style="width: 100%;">
    <tr>
     <td><b>Name :</b></td>
     <td><asp:Label ID="lblName" runat="server"></asp:Label> </td>
     <td><b>Class :</b></td>
     <td><asp:Label ID="lblClass" runat="server"></asp:Label></td>
    </tr>
<tr>
     <td><b>IC No :</b></td>
     <td><asp:Label ID="lblIC" runat="server"></asp:Label></td>
     <td><b>Gender :</b></td>
     <td><asp:Label ID="lblGender" runat="server"></asp:Label></td>
    </tr>
   </table>
                                  </div>
                               </div>
                                </div>
                                                  </div>

                        <br />

                   <div class="panel-body">
                        <asp:GridView ID="GridView1" runat="server" AllowSorting="True" CssClass="table table-bordered table-condensed table-responsive table-hover" Enabled="False" Width="100%" AutoGenerateColumns="False" DataKeyNames="SubjectGUID"  > 
                                <Columns>                              

                                    <asp:BoundField DataField="SubjectGUID" HeaderText="SubjectGUID" SortExpression="SubjectGUID" Visible="false" />
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
                           <table style="width: 100%;">
    <tr>
     <td><b>Place in Class :</b></td>
     <td><asp:Label ID="lblplclass" runat="server"></asp:Label> </td>
     <td><b>Place in Form :</b></td>
     <td><asp:Label ID="lblplform" runat="server"></asp:Label></td>
    </tr>
<tr>
     <td><b>GPA :</b></td>
     <td><asp:Label ID="lblgpa" runat="server"></asp:Label></td>
     <td><b>CGPA :</b></td>
     <td><asp:Label ID="lblcgpa" runat="server"></asp:Label></td>
    </tr>
                               <tr>
     <td><b>Average Mark :</b></td>
     <td><asp:Label ID="lblAvgMark" runat="server"></asp:Label></td>
    </tr>
   </table>
                                  </div>
                               </div>
                                </div>
                                                  </div>

                     </div>
                   </asp:Panel>
                    <br />
                                                        <fieldset>
                        <div class="form-group">
                            <div style="float:right">
                            <asp:Button ID="btnPrint" runat="server" Text="Print" OnClientClick="return printpage();" CssClass="btn btn-secondary btn-lg"/>&nbsp
                                        <asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click" CssClass="btn btn-secondary btn-lg"/>&nbsp
                            </div>
                           

                        </div>
                     </fieldset>
                                </div>
   
</asp:Content>
