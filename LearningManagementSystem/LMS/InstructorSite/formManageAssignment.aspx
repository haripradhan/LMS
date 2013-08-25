<%@ Page Title="" Language="C#" MasterPageFile="~/MyCourse.master" AutoEventWireup="true"
    CodeBehind="formManageAssignment.aspx.cs" Inherits="LMS.InstructorSite.formManageAssignment" %>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyHeaderContentPlaceholder" runat="server">
    Manage Assignment
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="BodyContentPlaceholder" runat="server">
    <asp:DataList ID="dlistAssignment" runat="server" OnItemCommand="dlCommandItem" Width="100%">
        <ItemTemplate>
            <div id="bodyContainer" style="height: 50px;">
                <div class="leftC" style="height: 50px;">
                    <div>
                        <asp:Label ID="lblAFileLocation" runat="server" Text='<%# Eval("AFileLocation") %>'
                            Visible="False" />
                        <asp:Label ID="lblAssignmentID" runat="server" Text='<%# Eval("AssignmentID") %>'
                            Visible="False" />
                        <asp:LinkButton ID="lnkbtnAssignmentTitle" Height="29px" CssClass="lbtnLecture" runat="server"
                            Text='<%# Eval("AssignmentTitle") %>' CommandName="View" />
                    </div>
                    <div>
                        <asp:Label ID="Label1" CssClass="lblManageAssignment" runat="server" Text="Due Date: " />
                        <asp:Label ID="lblDueDate" CssClass="lblManageAssignment" runat="server" Text='<%# Eval("DueDate") %>' />
                    </div>
                    <div>
                        <asp:Label ID="lblAssignmentDate" CssClass="lblManageAssignment" runat="server" Text='<%# Eval("AssignmentDate") %>'
                            Visible="False" />
                    </div>
                </div>
                <div class="rightC" style="margin-top:10px;">
                    <asp:Button ID="btnDelete" runat="server" ToolTip="Delete" Height="29px" Width="25px"
                        BackColor="Transparent" BorderStyle="None" CssClass="btnDelete" CommandName="Delete"
                        OnClientClick="return confirm('Are you sure you want to delete the assignment?')" />
                </div>
            </div>
        </ItemTemplate>
        <SeparatorTemplate>
        </SeparatorTemplate>
    </asp:DataList>
    <br />
    <div class="bottomC">
        <asp:Button ID="btnAdd" Height="29px" Width="30px" BackColor="Transparent" BorderStyle="None"
            CssClass="btnAdd" runat="server" ToolTip="Add" OnClick="btnAdd_Click" />
    </div>
</asp:Content>
