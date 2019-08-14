<%@ Page Title="" Language="C#" MasterPageFile="~/admin/AdminPage.master" AutoEventWireup="true" CodeFile="BackImage.aspx.cs" Inherits="admin_BackImage" %>

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
    <script type="text/javascript">
        function showimagepreview2(input) {
            if (input.files && input.files[0]) {
                var filerdr = new FileReader();
                filerdr.onload = function (e) {
                    $('#imgprvw2').attr('src', e.target.result);
                }
                filerdr.readAsDataURL(input.files[0]);
            }
        }
    </script>
    <script type="text/javascript">
        function showimagepreview3(input) {
            if (input.files && input.files[0]) {
                var filerdr = new FileReader();
                filerdr.onload = function (e) {
                    $('#imgprvw3').attr('src', e.target.result);
                }
                filerdr.readAsDataURL(input.files[0]);
            }
        }
    </script>

    <style type="text/css">
table.category{ margin:0px 0px 0px 0px; padding:0; border:none; width:100%;}
.category td
{
    padding:7px; vertical-align:top; margin:0;
}
.category td:nth-child(1){ width:40%;}
.category td:nth-child(2){ width:60%;}

.mGrid { width: 90%; background-color: #fff; margin:0px 1px 2px 1px; border: solid 1px #525252; border-collapse:collapse; }
.mGrid td { padding: 7px; border: solid 1px #c1c1c1; color: #717171;font-size: 14px;}
.mGrid th { padding: 7px; color: #fff; background: #424242 url(/images/grd_head.png) repeat-x top; border-left: solid 1px #525252; font-size:14px; }
.mGrid .alt { }
.mGrid .pgr {background: #424242 url(/images/grd_pgr.png) repeat-x top; height:32px;}
.mGrid .pgr table { margin: 5px 0; }
.mGrid .pgr td { border-width: 0; padding: 0 6px; border-left: solid 1px #666;  font-weight: bold; color: #fff; line-height: 12px; font-size:1.1em; }

.mGrid td a{ text-decoration:none; color:#0000FF;}

</style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="row">
    <div class="col-lg-6 col-md-8 col-sm-8 col-lg-offset-3 col-sm-offset-2">
        <div class="main-form full">
        <div class="row">
            <div class="col-xs-12 mb-20">
            <div class="heading-part heading-bg">
                <h2 class="heading">Product Back/Left/Right Images</h2>
            </div>
            <asp:Label ID="lblMessage" CssClass="message2" runat="server" Text=""></asp:Label>
            </div>
            <div class="col-xs-12">
            <div class="input-box">
                <label for="f-name">Back Image &nbsp;&nbsp;&nbsp;</label>
                <img id="imgprvw" src="../images/no-image.jpg" width="90" height="90" class="" alt="" />
                <asp:FileUpload ID="UploadImage" onchange="showimagepreview(this)" runat="server" />
                <asp:Label ID="lblImage" runat="server" Text=""></asp:Label>
            </div>
            </div>
            <div class="col-xs-12">
            <br />
            <div class="input-box">
                <label for="login-email">Left Image &nbsp;&nbsp;&nbsp;</label>
                <img id="imgprvw2" src="../images/no-image.jpg" width="90" height="90" class="" alt="" />
                <asp:FileUpload ID="UploadImage2" onchange="showimagepreview2(this)" runat="server" />
                <asp:Label ID="lblImage2" runat="server" Text=""></asp:Label>
            </div>
            </div>
            <div class="col-xs-12">
            <br />
            <div class="input-box">
                <label for="login-email">Right Image &nbsp;&nbsp;&nbsp;</label>
                <img id="imgprvw3" src="../images/no-image.jpg" width="90" height="90" class="" alt="" />
                <asp:FileUpload ID="UploadImage3" onchange="showimagepreview3(this)" runat="server" />
                <asp:Label ID="lblImage3" runat="server" Text=""></asp:Label>
            </div>
            </div>
            <div class="col-xs-12">
            <br />
            <asp:Button ID="btnBackImage" runat="server" Text="Add Images" class="btn-black" 
                    ValidationGroup="g1" onclick="btnBackImage_Click" />
            <asp:Button ID="btnCancel" runat="server" Text="Back to Product" 
                    class="btn-black right-side" onclick="btnCancel_Click"></asp:Button>
            </div>
        </div>
    </div>
    </div>
    </div>
    <div class="row">
        <div class="col-md-12 mt-40">
        <asp:GridView ID="gvBackImage" runat="server" GridLines="None"
            AllowPaging="False" CssClass="mGrid" PagerStyle-CssClass="pgr" 
            AlternatingRowStyle-CssClass="alt" 
            AutoGenerateColumns="False">
        <Columns>
        <asp:TemplateField HeaderText="S.No.">
            <ItemTemplate>
            <%# Container.DataItemIndex + 1 %>
            </ItemTemplate>
         </asp:TemplateField>
            <asp:TemplateField HeaderText="Back Image">
                <ItemTemplate>
                    <asp:Image ID="imgBack" ImageUrl='<%# Eval("BackImage","../Upload/thumbnails/{0}") %>' Height="80px" Width="80px" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Left Image">
                <ItemTemplate>
                    <asp:Image ID="imgLeft" ImageUrl='<%# Eval("LeftImage","../Upload/thumbnails/{0}") %>' Height="80px" Width="80px" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Right Image">
                <ItemTemplate>
                    <asp:Image ID="imgRight" ImageUrl='<%# Eval("RightImage","../Upload/thumbnails/{0}") %>' Height="80px" Width="80px" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
        <HeaderStyle Height="32px" />
        <PagerStyle CssClass="pgr"></PagerStyle>
        </asp:GridView>
        </div>
        </div>
</asp:Content>

