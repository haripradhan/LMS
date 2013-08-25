<%@ Page Title="" Language="C#" MasterPageFile="~/MyCourse.master" AutoEventWireup="true"
    CodeBehind="formLectureNote.aspx.cs" Inherits="LMS.Presentation.formLectureNote" %>
    
<asp:Content ID="Content2" ContentPlaceHolderID="BodyHeaderContentPlaceholder" runat="server">
    Lecture Note
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="BodyContentPlaceholder" runat="server">
    <asp:DataList ID="dlistLectureNote" runat="server" Width="100%" 
        OnItemCommand="ViewDownloadLectureNote">
        <ItemTemplate>
            <div id="bodyContainer">
                <div class="leftC">
                    <asp:Label ID="lblLectureNoteID" runat="server" Text='<%# Eval("LectureNoteID") %>' Visible="False" />
                    <asp:Label ID="lblLFileLocation" runat="server" Text='<%# Eval("LFileLocation") %>' Visible="False"/>                    
                    <asp:LinkButton ID="lnkbtnLectureNote" Height="29px" CssClass="lbtnLecture" runat="server" Text='<%# Bind("LectureNote") %>' CommandName="View"/>
                </div>
                <div class="rightC">
                <%--<asp:ImageButton ID="imgbtnDownload" CssClass="btnDownload" runat="server" ToolTip="Download" ImageUrl="~/Images/lbtnDownloadNote.png"/>--%>
                    <asp:Button ID="btnDownload" Height="29px" width="40px" BackColor="Transparent" BorderStyle="None" CssClass="btnDownload" runat="server" ToolTip="Download" CommandName="Download" />
                </div>
            </div>
        </ItemTemplate>
        <SeparatorTemplate>
        </SeparatorTemplate>
    </asp:DataList>
</asp:Content>
