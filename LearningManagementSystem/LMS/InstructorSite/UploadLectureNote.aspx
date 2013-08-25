<%@ Page Title="" Language="C#" MasterPageFile="~/MyCourse.master" AutoEventWireup="true"
    CodeBehind="UploadLectureNote.aspx.cs" Inherits="LMS.InstructorSite.UploadLectureNote" %>
<%@ Register TagPrefix="ajaxToolkit" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=4.1.7.123, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyHeaderContentPlaceholder" runat="server">
   Manage Lecture Note
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="BodyContentPlaceholder" runat="server">
        <ajaxToolkit:ToolkitScriptManager runat="Server" EnablePartialRendering="true" ID="ScriptManager1" />
    <style>
        .ajax__fileupload_button
        {
            background-color: green;
        }
    </style>
    <script type="text/javascript">


        function onClientUploadComplete(sender, e) {
            var id = e.get_fileId();
            onImageValidated("TRUE", e);
        }

        function onImageValidated(arg, context) {

            var test = document.getElementById("testuploaded");
            test.style.display = 'block';

            var fileList = document.getElementById("fileList");
            var item = document.createElement('div');
            item.style.padding = '4px';

            if (arg == "TRUE") {
                var url = context.get_postedUrl();
                url = url.replace('&amp;', '&');
                item.appendChild(createThumbnail(context, url));
            } else {
                item.appendChild(createFileInfo(context));
            }

            fileList.appendChild(item);

        }

        function createFileInfo(e) {
            var holder = document.createElement('div');
            holder.appendChild(document.createTextNode(e.get_fileName() + ' with size ' + e.get_fileSize() + ' bytes'));

            return holder;
        }

        function createThumbnail(e, url) {
            var holder = document.createElement('div');
            var img = document.createElement("img");
            img.style.width = '80px';
            img.style.height = '80px';
            img.setAttribute("src", url);

            holder.appendChild(createFileInfo(e));
            holder.appendChild(img);

            return holder;
        }
    
    </script>

    <!--useform with width="100%"-->
    <div id="uploadContainer">
        <div>
            <asp:Label ID="lblLectureTitle" CssClass="lblManageLecture" runat="server" Text="Title"
                Visible="True" />
            <asp:TextBox ID="txtboxLectureTitle" CssClass="tbManageLecture" runat="server"></asp:TextBox>
        </div>
        <div>
            <asp:Label ID="lblLecture" CssClass="lblManageLecture" runat="server" Text="Upload Lecture"
                Visible="True" />
            <asp:FileUpload CssClass="fuManageLecture" ID="fileuploadLectureNote" runat="server" Visible="True"/>
        </div>
        
        <%--<div>
        <asp:Label runat="server" ID="myThrobber" Style="display: none;"><img align="absmiddle" alt="" src="~/Images/uploading.gif"/></asp:Label>
        <ajaxToolkit:AjaxFileUpload ID="AjaxFileUpload1" runat="server" Padding-Bottom="4"
            Padding-Left="2" Padding-Right="1" Padding-Top="4" ThrobberID="myThrobber" OnClientUploadComplete="onClientUploadComplete"
            OnUploadComplete="AjaxFileUpload1_OnUploadComplete" MaximumNumberOfFiles="1"
            AllowedFileTypes="pdf,docx,doc" />
    </div>--%>
        
        

        <%--<div>
            <asp:Label ID="lblLectureImage" CssClass="lblManageLecture" runat="server" Text="Lecture Image"
                Visible="True" />
            <asp:FileUpload CssClass="fuManageLecture" ID="fileuploadImage" Width="400px" runat="server" />
        </div>--%>
        <div>
            <asp:Button ID="btnUpload" CssClass="ubtnManageLecture" runat="server" Text="Upload"
                OnClick="btnUpload_Click" />
        </div>
        <%-- <asp:RequiredFieldValidator ID="reqFieldValidatorTitleTxtBox" runat="server" ErrorMessage="Please provide the title of the lecture!!!"
            ControlToValidate="txtboxLectureTitle"></asp:RequiredFieldValidator>
        <asp:RequiredFieldValidator ID="reqFieldValidatorUploadTxtBox" runat="server" ErrorMessage="Please specify the file path of the lecture note!!!"
            ControlToValidate="fileuploadLectureNote"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="regExpressionValidatorUploadTxtBox" runat="server"
            ErrorMessage="File format error: Only pdf or ppt files are allowed!!!" ControlToValidate="fileuploadLectureNote"
            ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))
    +(.pdf|.PDF|.ppt|.PPT|.pptx|.PPTX)$"></asp:RegularExpressionValidator>--%>
    </div>
</asp:Content>
