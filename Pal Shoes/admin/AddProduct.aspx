<%@ Page Title="" Language="C#" MasterPageFile="~/admin/AdminPage.master" AutoEventWireup="true" CodeFile="AddProduct.aspx.cs" Inherits="admin_AddProduct" %>

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
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <Triggers>
        <asp:PostBackTrigger ControlID="btnAddProduct" />
        <asp:AsyncPostBackTrigger ControlID="ddCategory" />
    </Triggers>
    <ContentTemplate>
    <div>
   <center>
<div style="width:90%; padding:10px; margin:0 auto 0 auto; border:solid 1px #fc9e2c;">
<table class="product">
<tr>
     <td colspan="2" align="center">
         <h4 class="text-center">Add Products</h4>
         <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
     </td>
    </tr>
 <tr>
   <td>Select Category :</td>
   <td>
       <asp:DropDownList ID="ddCategory" runat="server" AutoPostBack="true" 
           onselectedindexchanged="ddCategory_SelectedIndexChanged">
       </asp:DropDownList>&nbsp;
       <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
           ErrorMessage="Select Category" InitialValue="--Select Category--" ForeColor="Red" 
           ControlToValidate="ddCategory" Display="Dynamic" ValidationGroup="g1"></asp:RequiredFieldValidator>
   </td>
 </tr>
 <tr>
   <td>Select Sub Category :</td>
   <td>
       <asp:DropDownList ID="ddSubCategory" runat="server">
       </asp:DropDownList>
   </td>
 </tr>
<tr>
   <td>Product Url :</td>
   <td>
       <asp:TextBox ID="txtProducturl" runat="server" Width="220px"></asp:TextBox>&nbsp;
       <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
           ErrorMessage="Product Url" ControlToValidate="txtProducturl" Display="Dynamic" 
           ValidationGroup="g1" ForeColor="Red"></asp:RequiredFieldValidator>
   </td>
 </tr>
<tr>
   <td>Product Title :</td>
   <td>
       <asp:TextBox ID="txtProductTitle" runat="server" Width="220px"></asp:TextBox>&nbsp;
       <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
           ErrorMessage="Product Title" ControlToValidate="txtProductTitle" Display="Dynamic" 
           ValidationGroup="g1" ForeColor="Red"></asp:RequiredFieldValidator>
   </td>
 </tr>
<tr>
   <td>Product Keywords :</td>
   <td>
       <asp:TextBox ID="txtProductKeywords" runat="server" Width="220px"></asp:TextBox>&nbsp;
       <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
           ErrorMessage="Product Keywords" ControlToValidate="txtProductKeywords" Display="Dynamic" 
           ValidationGroup="g1" ForeColor="Red"></asp:RequiredFieldValidator>
   </td>
 </tr>
<tr>
   <td>Short Description :</td>
   <td>
       <asp:TextBox ID="txtProductShortDes" runat="server" Width="220px"></asp:TextBox>&nbsp;
       <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
           ErrorMessage="Product ShortDes" ControlToValidate="txtProductShortDes" Display="Dynamic" 
           ValidationGroup="g1" ForeColor="Red"></asp:RequiredFieldValidator>
   </td>
 </tr>
 <tr>
   <td>Product Name :</td>
   <td>
       <asp:TextBox ID="txtProductName" runat="server" Width="220px"></asp:TextBox>&nbsp;
       <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
           ErrorMessage="Product Name" ControlToValidate="txtProductName" Display="Dynamic" 
           ValidationGroup="g1" ForeColor="Red"></asp:RequiredFieldValidator>
   </td>
 </tr>
 <tr>
   <td>Product Brand :</td>
   <td>
       <asp:TextBox ID="txtBrand" runat="server" Width="220px"></asp:TextBox>&nbsp;
       <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" 
           ErrorMessage="Brand Name" ControlToValidate="txtBrand" Display="Dynamic" 
           ValidationGroup="g1" ForeColor="Red"></asp:RequiredFieldValidator>
   </td>
 </tr>
 <tr>
   <td>Weight :</td>
   <td>
       <asp:TextBox ID="txtProductCapacity" runat="server" Width="220px"></asp:TextBox>&nbsp;
      <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" 
           ErrorMessage="Product Capacity" ControlToValidate="txtProductCapacity" Display="Dynamic" 
           ValidationGroup="g1" ForeColor="Red"></asp:RequiredFieldValidator>--%>
   </td>
 </tr>

 <tr>
   <td>Variant Names :</td>
   <td>
       <asp:TextBox ID="txtVariant" runat="server" Width="220px"></asp:TextBox>&nbsp;
       <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" 
           ErrorMessage="Product Variant Names" ControlToValidate="txtVariant" Display="Dynamic" 
           ValidationGroup="g1" ForeColor="Red"></asp:RequiredFieldValidator>
   </td>
 </tr>

<tr>
   <td>Variant Quantity :</td>
   <td>
       <asp:TextBox ID="txtVariantquantity" runat="server" Width="220px"></asp:TextBox>&nbsp;
       <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" 
           ErrorMessage="Product Variant Quantity" ControlToValidate="txtVariantquantity" Display="Dynamic" ValidationGroup="g1" ForeColor="Red"></asp:RequiredFieldValidator>
   </td>
 </tr>

 <%--<tr>
   <td>Variant :</td>
   <td>
       <asp:DropDownList ID="ddFlavour" runat="server">
       </asp:DropDownList>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" 
           ErrorMessage="Product Flavour" ControlToValidate="ddFlavour" Display="Dynamic" 
           ValidationGroup="g1" ForeColor="Red" InitialValue="Select Variant"></asp:RequiredFieldValidator>&nbsp;&nbsp;&nbsp;
       <asp:Button ID="btnNewFlavour" runat="server" Text="Add New Variant" 
           onclick="btnNewFlavour_Click" />
   </td>
 </tr>--%>
 <tr>
   <td>Price :</td>
   <td>
       <asp:TextBox ID="txtPrice" runat="server" Width="220px"></asp:TextBox>
       <cc1:FilteredTextBoxExtender runat="server" ID="FilteredTextBoxExtender6" TargetControlID="txtPrice"
           FilterType="Custom" Enabled="true" FilterMode="ValidChars" ValidChars="1234567890.">
           </cc1:FilteredTextBoxExtender>
       <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" 
           ErrorMessage="Product Price" ControlToValidate="txtPrice" Display="Dynamic" 
           ValidationGroup="g1" ForeColor="Red"></asp:RequiredFieldValidator>
   </td>
 </tr> 
 <tr>
 <td>Discount % :
 </td>
 <td>
     <asp:TextBox ID="txtDiscount" runat="server" Width="220px"></asp:TextBox>
     <cc1:FilteredTextBoxExtender runat="server" ID="FilteredTextBoxExtender1" TargetControlID="txtDiscount"
           FilterType="Custom" Enabled="true" FilterMode="ValidChars" ValidChars="1234567890.">
     </cc1:FilteredTextBoxExtender>
 </td>
 </tr>
 <tr>
   <td>Shipping Charges :</td>
   <td>
       <asp:TextBox ID="txtshippingcharges" runat="server" Width="220px"></asp:TextBox>
       <cc1:FilteredTextBoxExtender runat="server" ID="FilteredTextBoxExtender2" TargetControlID="txtshippingcharges" FilterType="Custom" Enabled="true" FilterMode="ValidChars" ValidChars="1234567890.">
     </cc1:FilteredTextBoxExtender>
   </td>
 </tr>
 <tr>
 <td>SKU Code :</td>
 <td>
     <asp:TextBox ID="txtProductCode" runat="server" Width="220px" Enabled="false"></asp:TextBox>
 </td>
 </tr>
 <tr>
 <td>Select (Home page) Product Type :</td>
 <td>
     <font color="red"><asp:RadioButton ID="rdOfferProduct" runat="server" Text="Offer Product" GroupName="rdprdtype" /></font> &nbsp;&nbsp;
     <asp:RadioButton ID="rdNewproduct" runat="server" Text="New Product" GroupName="rdprdtype" Checked="true" /> &nbsp;&nbsp;
     <asp:RadioButton ID="rdBestSeller" runat="server" Text="Best Seller Product" GroupName="rdprdtype" /> &nbsp;&nbsp;
     <asp:RadioButton ID="rdFeature" runat="server" Text="Feature Product" GroupName="rdprdtype" />
 </td>
 </tr>
 <tr>
 <td> Is Active Product : </td>
 <td>
     <asp:CheckBox ID="CheckBox1" runat="server" Checked="true" />&nbsp;Active</td>
 </tr>
 <tr>
 <td>Display Image : 
 </td>
 <td>
      <img id="imgprvw" src="../images/no-image.jpg" width="90" height="90" class="" alt="" />
      <asp:FileUpload ID="UploadImage" onchange="showimagepreview(this)" Width="200" runat="server" />
     <asp:Label ID="lblImage" runat="server" Text=""></asp:Label><br />
 </td>  
 </tr>
 <%--<tr>
   <td colspan="2" style="border:1px solid">
   <asp:Panel ID="Panel1" runat="server">
   <div>
    <div style="float:left; width:48%">Quantity :</div>
    <div style="float:left; width:48%"><asp:TextBox ID="txtQuantity" runat="server">1</asp:TextBox></div>
   </div>
   </asp:Panel>
   </td>
 </tr>--%>
 <tr>
   <td colspan="2">
        Description :&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
       <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" 
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
     <asp:Button ID="btnAddProduct" runat="server" Text="Add Product" 
         onclick="btnAddProduct_Click" ValidationGroup="g1" />
         &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button ID="btnCancel" runat="server" Text="Cancel" 
         onclick="btnCancel_Click" />
 </td>
 </tr>
</table>
</div>
</center>
</div>
</ContentTemplate>
</asp:UpdatePanel>
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

