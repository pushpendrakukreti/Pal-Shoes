<%@ Page Title="" Language="C#" MasterPageFile="~/admin/AdminPage.master" AutoEventWireup="true" CodeFile="AddBrandLogo.aspx.cs" Inherits="admin_AddBrandLogo" %>

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
.category td:nth-child(1){ width:45%;}
.category td:nth-child(2){ width:55%;}

.mGrid { width: 100%; background-color: #fff; margin:0px 1px 2px 1px; border: solid 1px #525252; border-collapse:collapse; }
.mGrid td { padding: 5px; border:solid 1px #c1c1c1; color: #717171; font-size: 14px; }
.mGrid th { padding: 7px; color: #fff; font-size:14px; background: #424242 url(/images/grd_head.png) repeat-x top; border-left: solid 1px #525252;}
.mGrid .alt { }
.mGrid .pgr {background: #424242 url(/images/grd_pgr.png) repeat-x top; height:32px;}
.mGrid .pgr table { margin: 5px 0; }
.mGrid .pgr td { border-width: 0; padding:0 6px; border-left: solid 1px #666;  font-weight: bold; color: #fff; line-height: 12px; font-size:1.1em; }

.mGrid td a{ text-decoration:none; color:#0000FF;}
.mGrid td:nth-child(1){width:10%;}
.mGrid td:nth-child(2){width:25%;}
.mGrid td:nth-child(3){width:30%;}
.mGrid td:nth-child(4){width:35%;}

</style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div style="width:90%; padding:10px; margin:0 auto 20px auto; border:solid 1px #fc9e2c;">
        <table class="category">
            <tr>
                <td colspan="2">
                    <h4 class="text-center">Add Product Brand Image</h4>
                    <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
            <td>
                Select Product Brand :
            </td>
            <td>
                <asp:DropDownList ID="ddBrand" runat="server" AutoPostBack="true" 
                    onselectedindexchanged="ddBrand_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ErrorMessage="Select Brand" InitialValue="--Select Brand--" ForeColor="Red" 
                    ControlToValidate="ddBrand" Display="Dynamic" ValidationGroup="g1"></asp:RequiredFieldValidator>
            </td>
            </tr>
            <tr>
            <td>Select Category :</td>
            <td>
                <asp:DropDownList ID="ddCategory" runat="server">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                    ErrorMessage="Select Category" InitialValue="--Select Category--" ForeColor="Red" 
                    ControlToValidate="ddCategory" Display="Dynamic" ValidationGroup="g1"></asp:RequiredFieldValidator>
            </td>
            </tr>
            <tr>
            <td>Sub Category Image :</td>
            <td>
                <img id="imgprvw" src="../img/no-image.jpg" width="90" height="90" class="" alt="" />
                <asp:FileUpload ID="UploadImage" onchange="showimagepreview(this)" runat="server" />
                <br /><asp:Label ID="lblImage" runat="server" Text=""></asp:Label>
            </td>
            </tr>
            <tr>
            <td></td>
            <td>
                <asp:Button ID="btnAddBrand" runat="server" Text="Add Brand Image" 
                    onclick="btnAddBrand_Click" ValidationGroup="g1" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" 
                    onclick="btnCancel_Click"/>
              </td>
            </tr>
        </table>
        </div>
       <asp:GridView ID="gvBrands" runat="server" GridLines="None" 
        CssClass="mGrid" AllowPaging="true" PagerStyle-CssClass="pgr" 
            AlternatingRowStyle-CssClass="alt" PageSize="20" 
            AutoGenerateColumns="False" 
        onpageindexchanging="gvBrands_PageIndexChanging" 
        onrowcommand="gvBrands_RowCommand" onrowdeleting="gvBrands_RowDeleting">
        <Columns>
        <asp:TemplateField HeaderText="S.No.">
            <ItemTemplate>
            <%# Container.DataItemIndex + 1 %>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Brand">
            <ItemTemplate>
                <asp:Image ID="Image2" ImageUrl='<%# Eval("BrandImage","~/Upload/brand/{0}") %>' Height="60px" runat="server" />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Brand Name">
            <ItemTemplate>
                <asp:Label ID="lblBrandName" runat="server" Text='<%# Bind("Brandname") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Category">
            <ItemTemplate>
                <asp:Label ID="lblCategory" runat="server" Text='<%# Bind("Category") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField ShowHeader="False">
            <ItemTemplate>
                <asp:Button ID="btnDelete" runat="server" Text="Delete" CommandName="Delete" CommandArgument='<%#Eval("BrandId") %>'
                    OnClientClick="return confirm('Do you want delete it?')"></asp:Button>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
    <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
    <HeaderStyle Height="32px" />
    <PagerStyle CssClass="pgr"></PagerStyle>
    </asp:GridView>
    <br />
</asp:Content>

