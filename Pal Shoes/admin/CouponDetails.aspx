<%@ Page Title="" Language="C#" MasterPageFile="~/admin/AdminPage.master" AutoEventWireup="true" CodeFile="CouponDetails.aspx.cs" Inherits="admin_CouponDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<style type="text/css">
table.category{ margin:0px 0px 0px 0px; padding:0; border:none; width:90%;}
.category td
{
    padding:5px; vertical-align:top; margin:0;
}
.category td:nth-child(1){ width:40%;}
.category td:nth-child(2){ width:60%;}

.mGrid { width: 100%; background-color: #fff; margin:0px 1px 2px 1px; border: solid 1px #525252; border-collapse:collapse; }
.mGrid td { padding: 5px; border:solid 1px #c1c1c1; color: #717171; font-size: 14px; }
.mGrid th { padding: 7px; color: #fff; font-size:14px; background: #424242 url(/images/grd_head.png) repeat-x top; border-left: solid 1px #525252;}
.mGrid .alt { }
.mGrid .pgr {background: #424242 url(/images/grd_pgr.png) repeat-x top; height:32px;}
.mGrid .pgr table { margin: 5px 0; }
.mGrid .pgr td { border-width: 0; padding:0 6px; border-left: solid 1px #666;  font-weight: bold; color: #fff; line-height: 12px; font-size:1.1em;}

.mGrid td a{ text-decoration:none; color:#0000FF;}
</style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<br />
    <p class="text-center">
    <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label></p>
    <h2 style="text-align:center"> Coupon List</h2>
    <asp:GridView ID="gvCoupon" runat="server" GridLines="None"
            AllowPaging="False" CssClass="mGrid" PagerStyle-CssClass="pgr" 
            AlternatingRowStyle-CssClass="alt" onrowcommand="gvCoupon_RowCommand" 
            AutoGenerateColumns="False" onrowdeleting="gvCoupon_RowDeleting">
        <Columns>
        <asp:TemplateField HeaderText="Id">
            <ItemTemplate>
            <%# Container.DataItemIndex + 1 %>
            </ItemTemplate>
        </asp:TemplateField>
            <%--<asp:BoundField HeaderText="User ID" DataField="useremailid" />--%>
            <asp:BoundField HeaderText="Coupon-Code" DataField="couponcode" />
            <asp:BoundField HeaderText="Discount(%)" DataField="discount" />
            <asp:BoundField HeaderText="Price" DataField="price" />
            <asp:BoundField HeaderText="Issue Date" DataFormatString="{0:dd/MM/yyyy}" DataField="CouponDate" />
            <%--<asp:TemplateField ShowHeader="False">
                <ItemTemplate>
                    <asp:Button ID="btnEdit" runat="server" Text="Edit" CommandName="Edit" CommandArgument='<%#Eval("SubCategoryId") %>'></asp:Button>
                </ItemTemplate>
            </asp:TemplateField>--%>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:ImageButton ID="BtnDelete" CommandArgument='<%# Eval("cid") %>' ImageUrl="~/images/Trash_d.jpg" CommandName="Delete" ToolTip="Delete" Width="25px" Height="25px" runat="server"></asp:ImageButton>
                </ItemTemplate>
            </asp:TemplateField>
    </Columns>
    <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
    <HeaderStyle Height="32px" />
    <PagerStyle CssClass="pgr"></PagerStyle>
    </asp:GridView>

<hr />
    <br />
    <p class="text-center">
    <asp:Label ID="Label1" runat="server" Text=""></asp:Label></p>
    <h2 style="text-align:center"> Special Coupon List</h2>
    <asp:GridView ID="gvSpecialCoupon" runat="server" GridLines="None"
            AllowPaging="False" CssClass="mGrid" PagerStyle-CssClass="pgr" 
            AlternatingRowStyle-CssClass="alt" onrowcommand="gvSpecialCoupon_RowCommand" 
            AutoGenerateColumns="False" onrowdeleting="gvSpecialCoupon_RowDeleting">
        <Columns>
        <asp:TemplateField HeaderText="Id">
            <ItemTemplate>
            <%# Container.DataItemIndex + 1 %>
            </ItemTemplate>
        </asp:TemplateField>
            <asp:BoundField HeaderText="Coupon-Code" DataField="couponcode" />
            <asp:BoundField HeaderText="Discount(%)" DataField="discount" />
            <asp:BoundField HeaderText="Price" DataField="price" />
            <asp:BoundField HeaderText="Issue Date" DataFormatString="{0:dd/MM/yyyy}" DataField="CouponDate" />
           
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:ImageButton ID="BtnDelete" CommandArgument='<%# Eval("cid") %>' ImageUrl="~/images/Trash_d.jpg" CommandName="Delete" ToolTip="Delete" Width="25px" Height="25px" runat="server"></asp:ImageButton>
                </ItemTemplate>
            </asp:TemplateField>
    </Columns>
    <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
    <HeaderStyle Height="32px" />
    <PagerStyle CssClass="pgr"></PagerStyle>
    </asp:GridView>
</asp:Content>

