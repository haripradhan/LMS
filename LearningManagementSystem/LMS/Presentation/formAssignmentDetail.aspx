<%@ Page Title="" Language="C#" MasterPageFile="~/MyCourse.master" AutoEventWireup="true" CodeBehind="formAssignmentDetail.aspx.cs" Inherits="LMS.Presentation.formAssignmentDetail" %>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyHeaderContentPlaceholder" runat="server">
    Assignment
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="BodyContentPlaceholder" runat="server">
<div>
Submit your assignement by uploading your file.
<div>
    <asp:FileUpload CssClass="fuManageLecture" ID="fileuploadSubmission" runat="server" />
</div>
<div>
    <asp:Button CssClass="ubtnManageLecture" ID="btnSubmit" runat="server" Text="Submit" 
        onclick="btnSubmit_Click" />
</div>
<br/>
<div>
    <asp:Table ID="tblSubmission" runat="server" BorderStyle="Double" 
        GridLines="Both">
    <asp:TableRow>
    <asp:TableHeaderCell>Assignment</asp:TableHeaderCell>
    <asp:TableHeaderCell>Submitted Date</asp:TableHeaderCell>
    <asp:TableHeaderCell>Grade</asp:TableHeaderCell>
    </asp:TableRow>
    <asp:TableRow>
    <asp:TableCell>
        <asp:Label ID="lblFileLocation" runat="server" Text="" Visible="False"></asp:Label>
        <asp:LinkButton ID="lbtnAssignment" runat="server" Text="" OnClick="ViewDownloadFile"></asp:LinkButton>
    </asp:TableCell>
    <asp:TableCell> 
        <asp:Label ID="lblSubmittedDate" runat="server" Text=""></asp:Label></asp:TableCell>
    <asp:TableCell>
        <asp:Label ID="lblGrade" runat="server" Text=""></asp:Label></asp:TableCell>
    </asp:TableRow>
    </asp:Table>
</div>
</div>
</asp:Content>
