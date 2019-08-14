<%@ Page Title="" Language="C#" MasterPageFile="~/admin/AdminPage.master" AutoEventWireup="true" CodeFile="Order1.aspx.cs" Inherits="admin_Order" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<style type="text/css">
.textbox { 		
		padding: 4px; 
		margin: 4px 0 5px 0; 
		background: #fff; 
		width: 220px; 
		font-size: 14px;
		color: #555; 
		border: 1px #ddd solid;
		
		-webkit-box-shadow: 0px 0px 4px #aaa;
		-moz-box-shadow: 0px 0px 4px #aaa; 
		box-shadow: 0px 0px 4px #aaa;		
		-webkit-transition: background 0.3s linear;							
	}
#ContentPlaceHolder1_DataList1{ width:100%;}
table.product{ margin:0; padding:0; width:100%;}
.product td{ padding:0px; vertical-align:middle; text-align:left;}
.product td:nth-child(1){ width:10%;}
.product td:nth-child(2){ width:50%;}
.product td:nth-child(3){ width:25%;}
.product td:nth-child(4){ width:4%;}
.product td:nth-child(5){ width:4%;}
.product td:nth-child(6){ width:6%;}

table.billingAdd{ margin:0; padding:0; width:98%; border-collapse:collapse;}
.billingAdd td{ padding:4px; vertical-align:top; text-align:left;}

</style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 
<div style=" margin:0 auto; padding:0; width:95%;">
    <div style="text-align:right; margin:0 5% 0 0;">
    <a href="OrderDetails.aspx" style="text-decoration:none; color:Blue; font-weight:bold;">Back</a>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:ImageButton ID="lnkprint" ToolTip="Print" Style="vertical-align:bottom;" runat="server" ImageUrl="~/img/print.gif" Width="25px" onclick="lnkprint_Click" />
    </div>
 <div style="border: 1px solid #D1D1D1; margin:10px 20px 20px 20px; background-color:white; padding:10px 50px 30px 50px">
 <h2 class="text-center" style="margin-bottom:0px;">TAX INVOICE</h2>
     <h2 class="text-center"><a href="paulshoes-TABLETENNIS.COM">paulshoes-TABLETENNIS.COM</a></h2>
     <h5 class="text-center">GLOBAL ENTERPRISES</h5>
     <h5 class="text-center">GSTIN: 07DE0PK2284FIZA</h5>
     <style>
         .product td:nth-child(2){
             width:30% !important;
         }
         .product td:nth-child(4){
             width:10% !important;
         }
     </style>
  <table class="billingAdd">
   <tr>
    <td colspan="4">
        <asp:Panel ID="Panel1" runat="server" Visible="false">
        <div style="height:20px; font-size:17px; padding:10px; font-weight:bold; background-color: #CCCCCC;">
            <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
        </div>
        </asp:Panel>

   <div style="height:30px; margin-bottom:10px; border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color:#7C7C7C;">
    <b>Billing Details :</b></div>
   </td>
   </tr>
   <tr>
     <td>Customer Name :</td>
      <td><asp:Label ID="lblBFname" runat="server"></asp:Label> <asp:Label ID="lblBLname" runat="server"></asp:Label></td>
      <td>Contact No. :</td>
      <td><asp:Label ID="lblBContact" runat="server"></asp:Label></td>
   </tr>
   <tr>
     <td>Billing Address :</td>
      <td><asp:Label ID="lblBillingAddress" runat="server"></asp:Label></td>
      <td>Email ID :</td>
      <td><asp:Label ID="lblBEmailid" runat="server"></asp:Label></td>
   </tr>
   <tr>
     <td>Land Mark :</td>
      <td><asp:Label ID="lblBNearby" runat="server"></asp:Label></td>
       <td>City :</td>
       <td><asp:Label ID="lblBCity" runat="server"></asp:Label> -
       <asp:Label ID="lblBPincode" runat="server"></asp:Label></td>
   </tr>
   <tr>
     <td>State :</td>
     <td><asp:Label ID="lblBState" runat="server"></asp:Label></td>
     <td>Country :</td>
     <td><asp:Label ID="lblBCountry" runat="server"></asp:Label></td>
   </tr>
   <tr>
    <td colspan="4">
    <div style="height:30px; margin:30px 0 10px 0; border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #7C7C7C;">
    <b>Product Details</b></div>
   </td>
   </tr>
   <tr>
    <td colspan="4">
        <asp:DataList ID="DataList1" runat="server"
         GridLines="None" BorderWidth="0" ItemStyle-VerticalAlign="Top" 
            onitemdatabound="DataList1_ItemDataBound">
            <ItemTemplate>
                <table class="product">
                <tr>
                <td>
                <img src="../Upload/thumbnails/<%#DataBinder.Eval(Container.DataItem,"thumbnail") %>" width="60px" height="60px" alt="" />
                </td>
               <td><%# Eval("Name") %></td>
               <td>
                   <asp:Literal ID="lblvariant" runat="server" Text='<%# Bind("Variantname") %>'></asp:Literal>
               </td>
               <td>
               <asp:Literal ID="lblprice" runat="server" Text='<%# Bind("ItemPrice") %>'></asp:Literal>
               <asp:Literal ID="lblusdprice" runat="server" Text='<%# Bind("ItemPrice") %>'></asp:Literal>
               </td>
               <td>X</td>
               <td><%# Eval("Quantity") %></td>
               <td align="right">= 
               <asp:Literal ID="lbltotal" runat="server" Text='<%# Bind("ItemSubTotal") %>'></asp:Literal>
               <asp:Literal ID="lblusdtotal" runat="server" Text='<%# Bind("ItemSubTotal") %>'></asp:Literal>
               </td>
              </tr>
              </table>
            </ItemTemplate>
        </asp:DataList>
        <br />
        <hr />
        <p class="text-center">Shipping Charges : <asp:Label ID="lblShipping" runat="server" Text=""></asp:Label></p>
        <div style="text-align:right; color:Black; padding-right:10%; height:30px; padding:5px;">
        Total = <asp:Label ID="lblTotal" runat="server" Text=""></asp:Label>
        </div>
   </td>
   </tr>
   <tr>
    <td colspan="4">
    <div style="height:30px; margin:10px 0 10px 0; border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #7C7C7C;">
    <b>Shipping Details</b></div>
   </td>
   </tr>
   <tr>
     <td>Customer Name :</td>
      <td><asp:Label ID="txtSFName" runat="server"></asp:Label> <asp:Label ID="txtSLName" runat="server"></asp:Label></td>
      <td>Contact No. :</td>
      <td><asp:Label ID="txtSPhoneno" runat="server"></asp:Label></td>
   </tr>
   <tr>
     <td>Billing Address :</td>
      <td><asp:Label ID="txtShipAddress" runat="server"></asp:Label></td>
      <td>Email ID :</td>
      <td><asp:Label ID="txtSEmailid" runat="server"></asp:Label></td>
   </tr>
   <tr>
     <td>Land Mark :</td>
      <td><asp:Label ID="txtSNearBy" runat="server"></asp:Label></td>
       <td>City :</td>
       <td><asp:Label ID="txtSCity" runat="server"></asp:Label> -
       <asp:Label ID="txtSPincode" runat="server"></asp:Label></td>
   </tr>
   <tr>
     <td>State :</td>
     <td><asp:Label ID="txtSState" runat="server"></asp:Label></td>
     <td>Country :</td>
     <td><asp:Label ID="txtSCountry" runat="server"></asp:Label></td>
   </tr>
   <tr>
   <td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td>
   </tr>
  </table>
 </div>
</div>
</asp:Content>

