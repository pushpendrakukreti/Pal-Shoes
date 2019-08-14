<%@ Page Title="" Language="C#" MasterPageFile="~/admin/AdminPage.master" AutoEventWireup="true" CodeFile="CreateSubcategory.aspx.cs" Inherits="admin_CreateSubcategory" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
<style type="text/css">
table.category{ margin:0px 0px 0px 0px; padding:0; border:none; width:90%;}
.category td
{
    padding:5px; vertical-align:top; margin:0;
}
.category td:nth-child(1){ width:40%;}
.category td:nth-child(2){ width:60%;}

.mGrid { width: 100%; background-color: #fff; margin:0px 1px 2px 1px; border: solid 1px #525252; border-collapse:collapse; }
.mGrid td { padding: 5px; border:solid 1px #c1c1c1; color: #424242; font-size: 14px; }
.mGrid th { padding: 7px; color: #fff; font-size:14px; background: #424242 url(/images/grd_head.png) repeat-x top; border-left: solid 1px #525252;}
.mGrid .alt { }
.mGrid .pgr {background: #424242 url(/images/grd_pgr.png) repeat-x top; height:32px;}
.mGrid .pgr table { margin: 5px 0; }
.mGrid .pgr td { border-width: 0; padding:0 6px; border-left: solid 1px #666;  font-weight: bold; color: #fff; line-height: 12px; font-size:1.1em;}

.mGrid td a{ text-decoration:none; color:#0000FF;}
.mGrid td:nth-child(1){width:10%;}
.mGrid td:nth-child(2){width:30%;}
.mGrid td:nth-child(3){width:40%;}
.mGrid td:nth-child(4){width:10%;}
.mGrid td:nth-child(5){width:10%;}

.modalBackground
    {
        background-color: Black;
        filter: alpha(opacity=40);
        opacity: 0.4;
    }
    .modalPopup
    {
        background-color: #FFFFFF;
        width: 300px;
        border: 3px solid #0DA9D0;
    }
    .modalPopup .header
    {
        background-color: #2FBDF1;
        height: 30px;
        color: White;
        line-height: 30px;
        text-align: center;
        font-weight: bold;
    }
    .modalPopup .body
    {
        min-height: 50px;
        line-height: 30px;
        text-align: center;
        font-weight: bold;
    }
    .modalPopup .footer
    {
        padding: 3px;
    }
    .modalPopup .yes, .modalPopup .no
    {
        height: 23px;
        color: White;
        line-height: 23px;
        text-align: center;
        font-weight: bold;
        cursor: pointer;
    }
    .modalPopup .yes
    {
        background-color: #2FBDF1;
        border: 1px solid #0DA9D0;
    }
    .modalPopup .no
    {
        background-color: #9F9F9F;
        border: 1px solid #5C5C5C;
    }
    .footer1 { margin:0; padding:5px; text-align:center; color:Black; background-color:#FFFF80;}
</style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="width:90%; padding:10px; margin:0 auto 20px auto; border:solid 1px #fc9e2c;">
        <table class="category">
            <tr>
                <td colspan="2">
                    <h4 class="text-center">Create Sub Category</h4>
                    <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
            <td>Select Category :</td>
            <td><asp:DropDownList ID="ddCategory" runat="server">
                </asp:DropDownList>
            </td>
            </tr>
            <tr>
                <td>
                    Sub Category Name :
                </td>
                <td>
                    <asp:TextBox ID="txtSubCategory" runat="server" Width="230"></asp:TextBox>
                </td>
            </tr>
            <tr>
            <td>Sub Category Description : (Optional)</td>
            <td>
                <asp:TextBox ID="txtDescription" runat="server" Width="230" Columns="20" 
                    Rows="3" TextMode="MultiLine"></asp:TextBox>
            </td>
            </tr>
            <tr>
            <td>Sub Category Image : (Optional)</td>
            <td>
                <img id="imgprvw" src="no-image.jpg" width="90" height="90" class="" alt="" />
                <asp:FileUpload ID="UploadImage" onchange="showimagepreview(this)" runat="server" />
                <br /><asp:Label ID="lblImage" runat="server" Text=""></asp:Label>
            </td>
            </tr>
            <tr>
            <td></td>
            <td>
                <asp:Button ID="btnCreateSubCategory" runat="server" Text="Create Sub Category" 
                    onclick="btnCreateSubCategory_Click"/>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" 
                    onclick="btnCancel_Click"/>      
              </td>
            </tr>
        </table>
        </div>
        <asp:GridView ID="gvSubCategory" runat="server" GridLines="None"
            AllowPaging="False" CssClass="mGrid" PagerStyle-CssClass="pgr" 
            AlternatingRowStyle-CssClass="alt" 
            AutoGenerateColumns="False" onrowcommand="gvSubCategory_RowCommand" 
        onrowdeleting="gvSubCategory_RowDeleting">
        <Columns>
        <asp:TemplateField HeaderText="S.No.">
            <ItemTemplate>
            <%# Container.DataItemIndex + 1 %>
            </ItemTemplate>
        </asp:TemplateField>
            <asp:BoundField HeaderText="Category Name" DataField="Category" />
            <asp:BoundField HeaderText="Sub Category Name" DataField="SubName" />
            <%--<asp:BoundField HeaderText="Description" DataField="SubDescription" />
            <asp:TemplateField HeaderText="Image">
                <ItemTemplate>
                    <asp:Image ID="Image1" ImageUrl='<%# Eval("Thumbnail","../Upload/thumbnails/{0}") %>' Height="80px" Width="80px" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>--%>
            <asp:TemplateField ShowHeader="False">
                <ItemTemplate>
                    <asp:Button ID="btnEdit" runat="server" Text="Edit" CommandName="Edit" CommandArgument='<%#Eval("SubCategoryId") %>'></asp:Button>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ShowHeader="False">
                <ItemTemplate>
                    <asp:Button ID="btnDelete" CommandArgument= '<%# Eval("SubCategoryId") %>' OnClick ="DeleteRecord" runat="server" Text="Delete" />
                <cc1:ConfirmButtonExtender ID="cbe" runat="server" DisplayModalPopupID="mpe" TargetControlID="btnDelete">
                </cc1:ConfirmButtonExtender>
                <cc1:ModalPopupExtender ID="mpe" runat="server" PopupControlID="pnlPopup" TargetControlID="btnDelete" OkControlID = "btnYes"
                    CancelControlID="btnNo" BackgroundCssClass="modalBackground">
                </cc1:ModalPopupExtender>
                <asp:Panel ID="pnlPopup" runat="server" CssClass="modalPopup" Style="display: none">
                    <div class="header">
                        Confirmation
                    </div>
                    <div class="body">
                        Do you want to delete this record?
                    </div>
                    <div class="footer1" align="right">
                        <asp:Button ID="btnYes" runat="server" Text="Yes" />
                        <asp:Button ID="btnNo" runat="server" Text="No" />
                    </div>
                </asp:Panel>
                </ItemTemplate>
            </asp:TemplateField>
    </Columns>
    <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
    <HeaderStyle Height="32px" />
    <PagerStyle CssClass="pgr"></PagerStyle>
    </asp:GridView>
    <br />
</asp:Content>

