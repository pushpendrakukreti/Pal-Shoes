<%@ Page Title="" Language="C#" MasterPageFile="~/admin/AdminPage.master" AutoEventWireup="true" CodeFile="RecomendedProduct.aspx.cs" Inherits="admin_RecomendedProduct" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<style type="text/css">
.mGrid { width: 100%; background-color: #fff; margin:0px 1px 2px 1px; border: solid 1px #525252; border-collapse:collapse; }
.mGrid td { padding: 7px; border: solid 1px #c1c1c1; color: #717171;font-size: 14px;}
.mGrid th { padding: 7px; color: #fff; background: #424242 url(/images/grd_head.png) repeat-x top; border-left: solid 1px #525252; font-size:14px; }
.mGrid .alt { }
.mGrid .pgr {background: #424242 url(/images/grd_pgr.png) repeat-x top; height:32px;}
.mGrid .pgr table { margin: 5px 0; }
.mGrid .pgr td { border-width: 0; padding: 0 6px; border-left: solid 1px #666;  font-weight: bold; color: #fff; line-height: 12px; font-size:1.1em; }

.mGrid td a{ text-decoration:none; color:#0000FF;}
.mGrid td:nth-child(1){width:10%;}
.mGrid td:nth-child(2){width:25%;}
.mGrid td:nth-child(3){width:15%;}
.mGrid td:nth-child(4){width:20%;}
.mGrid td:nth-child(5){width:20%;}
.mGrid td:nth-child(6){width:10%;}

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
    
    .minheight{ min-height:440px;}
</style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="minheight">
<h2 class="text-center">Recomended Products</h2>
<p class="text-center"><asp:Label ID="lblMessage" runat="server" Text=""></asp:Label></p>

    <asp:GridView ID="gvWishlist" runat="server" GridLines="None" 
        CssClass="mGrid" PagerStyle-CssClass="pgr" DataKeyNames="WishId"
        AlternatingRowStyle-CssClass="alt" PageSize="30" 
        AutoGenerateColumns="False" AllowPaging="True" 
        onpageindexchanging="gvWishlist_PageIndexChanging" 
        onrowdeleting="gvWishlist_RowDeleting">
        <Columns>
            <asp:TemplateField HeaderText="S.No.">
                <ItemTemplate>
                <%# Container.DataItemIndex+1 %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="UserIdentifyNo" HeaderText="User ID" />
            <asp:BoundField DataField="Name" HeaderText="Product Name" />
            <asp:BoundField DataField="fullname" HeaderText="Cutomer Name" />
            <asp:BoundField DataField="Email" HeaderText="Email ID" />
            <asp:TemplateField>
             <ItemTemplate>
                <asp:LinkButton ID = "lnkDelete" CommandArgument= '<%# Eval("WishId") %>' OnClick = "DeleteRecord" runat = "server" Text = "Delete"></asp:LinkButton>
                <cc1:ConfirmButtonExtender ID="cbe" runat="server" DisplayModalPopupID="mpe" TargetControlID="lnkDelete">
                </cc1:ConfirmButtonExtender>
                <cc1:ModalPopupExtender ID="mpe" runat="server" PopupControlID="pnlPopup" TargetControlID="lnkDelete" OkControlID = "btnYes"
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
</div>
</asp:Content>

