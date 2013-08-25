<%@ Page Title="" Language="C#" MasterPageFile="~/MyCourse.master" AutoEventWireup="true" CodeBehind="formAbout.aspx.cs" Inherits="LMS.Presentation.formAbout" %>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyHeaderContentPlaceholder" runat="server">
    About
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="BodyContentPlaceholder" runat="server">
<div><asp:Label CssClass="lblManageLecture" ID="Label1" runat="server">Course Name:</asp:Label>
<asp:Label CssClass="lblManageLecture" ID="lblCourseName" runat="server"></asp:Label></div>
<div><asp:Label CssClass="lblManageLecture" ID="Label2" runat="server">Instructor Name:</asp:Label>
<asp:Label CssClass="lblManageLecture" ID="lblInstructorName" runat="server"></asp:Label></div>
<div><asp:Label CssClass="lblManageLecture" ID="Label3" runat="server">Introduction:</asp:Label>
<asp:Label CssClass="lblManageLecture" ID="lblIntroduction" runat="server"></asp:Label></div>
<div><asp:Label CssClass="lblManageLecture" ID="Label4" runat="server">Session ID:</asp:Label>
<asp:Label CssClass="lblManageLecture" ID="lblSessionID" runat="server"></asp:Label></div>
<div><asp:Label CssClass="lblManageLecture" ID="Label5" runat="server">About the course:</asp:Label>
<asp:Label CssClass="lblManageLecture" ID="lblAbtCourse" runat="server"></asp:Label></div>
<div><asp:Label CssClass="lblManageLecture" ID="Label6" runat="server">Pre-requisites:</asp:Label>
<asp:Label CssClass="lblManageLecture" ID="lblPreRequisites" runat="server"></asp:Label></div>
<div><asp:Label CssClass="lblManageLecture" ID="Label7" runat="server">Syllabus:</asp:Label>
<asp:Label CssClass="lblManageLecture" ID="lblSyllabus" runat="server"></asp:Label></div>  
</asp:Content>
