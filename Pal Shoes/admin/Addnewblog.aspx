<%@ Page Title="" Language="C#" MasterPageFile="~/admin/AdminPage.master" AutoEventWireup="true" CodeFile="Addnewblog.aspx.cs" Inherits="admin_Addnewblog" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<style type="text/css">
table.product{ margin:0px 0px 0px 0px; padding:0; border:none; width:90%;}
.product td
{
    padding:7px; vertical-align:top; margin:0;
}
.product td:nth-child(1){ width:40%;}
.product td:nth-child(2){ width:60%;}

</style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">--%>
    <%--<Triggers>
        <asp:PostBackTrigger ControlID="btnAddProduct" />
        <asp:AsyncPostBackTrigger ControlID="ddCategory" />
    </Triggers>--%>
   <%--<ContentTemplate>--%>
    <div>
   <center>
<div style="width:90%; padding:10px; margin:0 auto 0 auto; border:solid 1px #fc9e2c;">
<table class="product">
<tr>
     <td colspan="2" align="center">
         <h4 class="text-center">Add New Blog</h4>
         <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
     </td>
    </tr>

 <tr>
   <td>Blog Title: :</td>
   <td>
       <asp:TextBox ID="txtBlogtitle" runat="server" Width="220px"></asp:TextBox>&nbsp;
       <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
           ErrorMessage="Blog Title*" ControlToValidate="txtBlogtitle" Display="Dynamic" 
           ValidationGroup="g1" ForeColor="Red"></asp:RequiredFieldValidator>
   </td>
 </tr>

 <tr>
 <td>Blog Photo : 
 </td>
 <td>
      <img id="imgprvw" src="../images/no-image.jpg" width="90" height="90" class="" alt="" />
     <p><b>Image Size:width: 950px Height: 350px</b></p> 
      <asp:FileUpload ID="UploadImage" onchange="showimagepreview(this)" Width="200" runat="server" />
     <asp:Label ID="lblImage" runat="server" Text=""></asp:Label><br />
 </td>  
 </tr>

 <tr>
   <td colspan="2">
        Blog Description :&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
       <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
           ErrorMessage="Enter Description" ControlToValidate="CKEditor1" Display="Dynamic" 
           ValidationGroup="g1" ForeColor="Red"></asp:RequiredFieldValidator>
           <br />
        <%--<CKEditor:CKEditorControl ID="CKEditorControl1" runat="server"></CKEditor:CKEditorControl>--%>
        <CKEditor:CKEditorControl ID="CKEditor1" BasePath="~/ckeditor" runat="server" UIColor="#BFEE62" Language="de" EnterMode="BR">
        </CKEditor:CKEditorControl>
   </td>
 </tr>
 <tr>
 <td></td>
 <td>
     <asp:Button ID="btnAddBlog" runat="server" Text="Add Blog" OnClick="btnAddBlog_Click" ValidationGroup="g1" />
         &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click"/>
 </td>
 </tr>
</table>
</div>
</center>
</div>
<%--</ContentTemplate>
</asp:UpdatePanel>--%>
<script type="text/javascript">    jQuery.noConflict();</script>
<script type="text/javascript" src="http://code.jquery.com/jquery-1.8.2.js"></script>
    <script type="text/javascript">
        function showimagepreview(input) {
            if (input.files && input.files[0]) {
                var filerdr = new FileReader();
                filerdr.onload = function (e) {
                    $('#imgprvw').attr('src', e.target.result);
                }
                filerdr.readAsDataURL(input.files[0]);
            }
        }
    </script>
</asp:Content>

