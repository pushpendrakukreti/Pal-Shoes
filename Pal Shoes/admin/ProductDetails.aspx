<%@ Page Title="" Language="C#" MasterPageFile="~/admin/AdminPage.master" AutoEventWireup="true" CodeFile="ProductDetails.aspx.cs" Inherits="admin_ProductDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<style type="text/css">
table.product{ margin:0 auto 0 auto; padding:0; border:solid 1px #00B1F0; width:95%;}
.product td
{
    padding:10px; vertical-align:top; margin:0;
    border-bottom: solid 1px #00B1F0;
}
#ContentPlaceHolder1_dtlProductDetails{ width: 100%;}
.product td:nth-child(1){ width:20%;}
.product td:nth-child(2){ width:5%;}
.product td:nth-child(3){ width:25%;}
.product td:nth-child(4){ width:20%; border-left: solid 1px #00B1F0;}
.product td:nth-child(5){ width:5%;}
.product td:nth-child(6){ width:25%;}
.bor1{ border-left: solid 1px #00B1F0;}
.back{ margin:0; text-align:right; padding:2px 40px 10px 40px;}
</style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
       <h4 class="text-center">Product Details</h4>
       <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
    <div class="back"><asp:LinkButton ID="LinkButton1" runat="server" onclick="LinkButton1_Click">Back</asp:LinkButton></div>
    <asp:DataList ID="dtlProductDetails" runat="server">
    <ItemTemplate>
    <table class="product">
    <tr>
    <td>Product Name</td><td>:</td><td><%# Eval("Name")%></td>
    <td>Product SKU</td><td>:</td><td><%# Eval("SKUCode")%></td>
    </tr>
    <tr>
    <td>Brand Name</td><td>:</td><td><%# Eval("Brand")%></td>
    <td></td><td></td>
    <td rowspan="4" class="bor1">
        <asp:Image ID="Image1" ImageUrl='<%# Eval("Thumbnail","../Upload/thumbnails/{0}") %>' Width="140px" runat="server" /></td>
    </tr>
    <tr>
    <td>Weight (lbs)</td><td>:</td><td><%# Eval("Capacity")%></td>
    <td>Product Image</td><td>:</td>
    </tr>
    <tr>
    <%--<td>Flavour</td><td>:</td><td><%# Eval("Producttype")%></td>--%>
    <%--<td></td><td></td>--%>
    </tr>
    <tr>
    <td>Price</td><td>:</td><td><%# Eval("Price")%></td>
    <td></td><td></td>
    </tr>
    <tr>
    <td>Discount</td><td>:</td><td><%# Eval("Discount")%>%</td>
    <td>Shipping Charges</td><td>:</td><td><%# Eval("ShippingCharges")%> rupees in India</td>
    </tr>
    <tr>
    <td>After Discout Price</td><td>:</td><td><%# Eval("discoutprice")%></td>
    <td>Added Date</td><td>:</td><td><%# Eval("DateAdded")%></td>
    </tr>
    <tr>
    <td>Description</td><td>:</td><td colspan="4"><%# Eval("Description")%></td>
    </tr>
    <tr>
    <td>Stock Quantity</td><td>:</td><td><%# Eval("Quantity")%></td>
    <td>Status</td><td>:</td><td><%# Eval("Active")%></td>
    </tr>
    </table>
    </ItemTemplate>
    </asp:DataList>
    <br />
</asp:Content>

