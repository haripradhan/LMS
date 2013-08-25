<%@ Page Title="" Language="C#" MasterPageFile="~/MyCourse.master" AutoEventWireup="true"
    CodeBehind="formManageLectureNote.aspx.cs" Inherits="LMS.InstructorSite.formManageLectureNote" %>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyHeaderContentPlaceholder" runat="server">
    Manage Lecture Note
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="BodyContentPlaceholder" runat="server">
    
        <asp:DataList ID="dlLectureNotes" runat="server" 
            OnItemCommand="dlCommandItem" Width="100%">
           
            <ItemTemplate>
                <div id="bodyContainer">
                <div class="leftC">
                    <asp:Label ID="lblLFileLocation" runat="server" Text='<%# Eval("LFileLocation") %>' Visible="False" />
                    <asp:Label ID="lblLectureNoteID" runat="server" Text='<%# Eval("LectureNoteID") %>' Visible="False" />
                    <asp:LinkButton ID="lnkbtnLectureNote" Height="29px" CssClass="lbtnLecture" runat="server" Text='<%# Eval("LectureNote") %>' CommandName="View" />
                </div>
                <div class="rightC">
                    <asp:Button ID="btnDelete" Height="29px" width="25px" BackColor="Transparent" BorderStyle="None" CssClass="btnDelete" runat="server" Tooltip="Delete" CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete the lecture note?')"/>
                </div>
                </div>
            </ItemTemplate>
            <SeparatorTemplate></SeparatorTemplate>
            
        </asp:DataList>
        <br/>
        <div class="bottomC">
            <asp:Button ID="btnAdd" Height="29px" width="30px" BackColor="Transparent" BorderStyle="None" CssClass="btnAdd" runat="server" Tooltip="Add" onclick="btnAdd_Click" />
        </div>
    
</asp:Content>
