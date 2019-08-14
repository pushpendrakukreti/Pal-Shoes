<%@ Page Title="" Language="C#" MasterPageFile="~/admin/AdminPage.master" Culture="en-GB" AutoEventWireup="true" CodeFile="OrderDetails.aspx.cs" 
Inherits="admin_OrderDetails" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<style type="text/css">
.mGrid { width: 100%; background-color: #fff; margin:0px 1px 2px 1px; border: solid 1px #525252; border-collapse:collapse; }
.mGrid td { padding: 7px; border: solid 1px #c1c1c1; color: #717171;font-size: 14px; }
.mGrid th { padding: 7px; color: #fff; background: #424242 url(/images/grd_head.png) repeat-x top; border-left: solid 1px #525252; font-size:14px; }
.mGrid .alt { }
.mGrid .pgr {background: #424242 url(/images/grd_pgr.png) repeat-x top; height:32px;}
.mGrid .pgr table { margin: 5px 0; }
.mGrid .pgr td { border-width: 0; padding: 0 6px; border-left: solid 1px #666;  font-weight: bold; color: #fff; line-height: 12px; font-size:1.1em;}

.mGrid td a{ text-decoration:none; color:#0000FF;}
.mGrid td:nth-child(1){width:6%;}
.mGrid td:nth-child(2){width:10%;}
.mGrid td:nth-child(3){width:10%;}
.mGrid td:nth-child(4){width:12%;}
.mGrid td:nth-child(5){width:10%;}
.mGrid td:nth-child(6){width:10%;}
.mGrid td:nth-child(7){width:12%;}
.mGrid td:nth-child(8){width:8%;}
.mGrid td:nth-child(9){width:8%;}
.mGrid td:nth-child(10){width:14%;}


table.ord{ width:auto; margin:0 auto 0 auto;}
.ord td{ }
.minheight{ min-height:440px;}
</style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="minheight">
    <h2 class="text-center">Order Details</h2>
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
        Order-No: <asp:TextBox ID="txtOrderId" runat="server" Width="150px"></asp:TextBox>
    </td>
    <td>
        <asp:Button ID="btnGetDetails" CssClass="getbutton" runat="server" ValidationGroup="g1" 
            Text="Get Details" onclick="btnGetDetails_Click" />
    </td>
    <td>
        &nbsp;&nbsp;&nbsp;<asp:ImageButton ID="ImageButton1" ImageUrl="~/img/excel.png" 
            runat="server" onclick="ImageButton1_Click" Width="29px" Height="29px" /></td>
    </tr>
    </table>
    <p class="text-center">
        <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label></p>
    <asp:GridView ID="gvOrderDetails" runat="server" GridLines="None" 
        CssClass="mGrid" PagerStyle-CssClass="pgr" DataKeyNames="OrderId"
        AlternatingRowStyle-CssClass="alt" PageSize="30" 
        AutoGenerateColumns="False" AllowPaging="True" 
        onpageindexchanging="gvOrderDetails_PageIndexChanging" 
        onrowcommand="gvOrderDetails_RowCommand" 
        onrowdeleting="gvOrderDetails_RowDeleting" 
        onrowdatabound="gvOrderDetails_RowDataBound">
        <Columns>
            <asp:TemplateField HeaderText="S.N.">
                <ItemTemplate>
                <%# Container.DataItemIndex+1 %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="UseridentifyNo" HeaderText="User ID" />
            <asp:BoundField DataField="Orderdetailid" HeaderText="Order-NO" />
            <asp:BoundField DataField="Name" HeaderText="Customer Name" />
            <asp:BoundField DataField="BillContact" HeaderText="Contact No" />
            <asp:BoundField DataField="Orderdate" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Order Date" />
            <%--<asp:BoundField DataField="BillCity" HeaderText="City" />--%>
            <asp:BoundField DataField="Paymentmode" HeaderText="Payment" />
            <asp:TemplateField  HeaderText="Order">
            <ItemTemplate>
                <asp:LinkButton ID="LinkButton1" CommandName="Details" CommandArgument='<%# Eval("TransId") %>' runat="server">Details</asp:LinkButton>
            </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField  HeaderText="Order">
            <ItemTemplate>
                <asp:LinkButton ID="lnkUpdateorder" CommandName="UpdateOrder" CommandArgument='<%# Eval("OrderId") %>' runat="server">Update</asp:LinkButton>
            </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Status">
                <ItemTemplate>
                    <asp:Label ID="lblOrderStatus" runat="server" Text='<%# Bind("Orderstatus") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
    <HeaderStyle Height="32px" />
    <PagerStyle CssClass="pgr"></PagerStyle>
    </asp:GridView>
</div>
</asp:Content>
