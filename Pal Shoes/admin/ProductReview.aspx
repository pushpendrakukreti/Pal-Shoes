<%@ Page Title="" Language="C#" Culture="en-GB" MasterPageFile="~/admin/AdminPage.master" AutoEventWireup="true" CodeFile="ProductReview.aspx.cs" Inherits="admin_ProductReview" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<style type="text/css">
table.category{ margin:0px 0px 0px 0px; padding:0; border:none; width:100%;}
.category td
{
    padding:7px; vertical-align:top; margin:0;
}
.minheight{ min-height:440px;}

.mGrid { width: 100%; background-color: #fff; margin:0px 1px 2px 1px; border: solid 1px #525252; border-collapse:collapse; }
.mGrid td { padding: 7px; border: solid 1px #c1c1c1; color: #717171;font-size: 14px;}
.mGrid th { padding: 7px; color: #fff; background: #424242 url(/images/grd_head.png) repeat-x top; border-left: solid 1px #525252; font-size:14px; }
.mGrid .alt { }
.mGrid .pgr {background: #424242 url(/images/grd_pgr.png) repeat-x top; height:32px;}
.mGrid .pgr table { margin: 5px 0; }
.mGrid .pgr td { border-width: 0; padding: 0 6px; border-left: solid 1px #666;  font-weight: bold; color: #fff; line-height: 12px; font-size:1.1em;}

.mGrid td a{ text-decoration:none; color:#0000FF;}

table.ord{ width:auto; margin:0 auto 0 auto;}
.ord td{ }
</style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="minheight">
<h3 class="text-center">Product Review</h3>
<table class="ord">
    <tr>
    <td>
        Date: <asp:TextBox ID="txtDate1" runat="server" Width="150px"></asp:TextBox>
        <img src="~/img/Calendar.gif" id="img_date1" runat="server" alt="" />
        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="img_date1"
            TargetControlID="txtDate1" Format="dd/MM/yyyy">
        </cc1:CalendarExtender>
        <asp:CustomValidator ID="CustomValidator1" runat="server" ClientValidationFunction="ValidateDate" 
        ControlToValidate="txtDate1" ErrorMessage="dd/mm/yyyy" ValidationGroup="g1" ForeColor="Red" />
    </td>
    <td>
        To Date: <asp:TextBox ID="txtDate2" runat="server" Width="150px"></asp:TextBox>
        <img src="~/img/Calendar.gif" id="img_date2" runat="server" alt="" />
        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" PopupButtonID="img_date2"
            TargetControlID="txtDate2" Format="dd/MM/yyyy">
        </cc1:CalendarExtender>
        <asp:CustomValidator ID="CustomValidator2" runat="server" ClientValidationFunction="ValidateDate" 
        ControlToValidate="txtDate2" ErrorMessage="dd/mm/yyyy" ValidationGroup="g1" ForeColor="Red" />
    </td>
    <td>
        <asp:Button ID="btnGetDetails" CssClass="getbutton" runat="server" ValidationGroup="g1" 
            Text="Get Detail" onclick="btnGetDetails_Click" />
    </td>
    </tr>
    </table>
    <p class="text-center">
        <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label></p>
    <asp:GridView ID="gvRegistration" runat="server" GridLines="None" 
        CssClass="mGrid" PagerStyle-CssClass="pgr" 
            AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False" 
        onrowcommand="gvRegistration_RowCommand" PageSize="25" 
        onrowdatabound="gvRegistration_RowDataBound" 
        onpageindexchanging="gvRegistration_PageIndexChanging">
        <Columns>
            <asp:TemplateField HeaderText="S.No.">
                <ItemTemplate>
                    <%# Container.DataItemIndex + 1 %>
                    <asp:Label ID="lblId" Visible="false" runat="server" Text='<%# Eval("RatingId") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Name" HeaderText="Name" />
            <asp:BoundField DataField="Emailid" HeaderText="Email ID" />
            <asp:BoundField DataField="Review" HeaderText="Review/Comments" />
            <asp:BoundField DataField="ReviewDate" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Date" />
            <asp:TemplateField HeaderText="Status">
                <ItemTemplate>
                    <asp:Label ID="lblActive" runat="server" Text='<%# Bind("active") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:ImageButton ID="BtnActive" ImageUrl='<%# Eval("active") %>' OnClick="BtnActive_Click" 
                    CommandArgument='<%# Eval("RatingId") %>' Width="21px" Height="21px" runat="server"></asp:ImageButton>
                </ItemTemplate>
            </asp:TemplateField>
            <%--<asp:TemplateField>
                <ItemTemplate>
                    <asp:ImageButton ID="BtnDelete" CommandArgument='<%# Eval("RatingId") %>' ImageUrl="~/img/Trash_d.png"
                     CommandName="Delete" ToolTip="Delete" Width="25px" Height="25px" runat="server"></asp:ImageButton>
                </ItemTemplate>
            </asp:TemplateField>--%>
        </Columns>
        <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
        <HeaderStyle Height="32px" />
        <PagerStyle CssClass="pgr"></PagerStyle>
    </asp:GridView>
    </div>
</asp:Content>

