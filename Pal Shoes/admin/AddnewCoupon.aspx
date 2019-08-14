<%@ Page Title="" Language="C#" MasterPageFile="~/admin/AdminPage.master" AutoEventWireup="true" CodeFile="AddnewCoupon.aspx.cs" Inherits="admin_AddnewCoupon" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<style type="text/css">
table.category{ margin:0px 0px 0px 0px; padding:0; border:none; width:90%;}
.category td
{
    padding:5px; vertical-align:top; margin:0;
}
.category td:nth-child(1){ width:40%;}
.category td:nth-child(2){ width:60%;}

</style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="row">
<div class="col-md-10 col-md-offset-1 col-sm-12">
    <table class="category">
            <tr>
                <td colspan="2">
                    <h4 class="text-center">Add New Coupon</h4>
                   <center> <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label></center>
                </td>
            </tr>
            <tr>
            <td>Coupon :</td>
            <td><asp:DropDownList ID="ddSpecialCoupon" runat="server" Width="200px" Height="40px">
                <asp:ListItem Value="0" text="Select a Value">Select Value</asp:ListItem>
                <asp:ListItem Value="1" text="Select a Coupon">Special Coupon</asp:ListItem>
                </asp:DropDownList>
            </td>
            </tr>
            <tr>
                <td>
                    Coupon Code :
                </td>
                <td>
                   <asp:TextBox ID="txtCouponCode" runat="server" Width="200"></asp:TextBox>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                   ErrorMessage="Coupon Code" ControlToValidate="txtCouponCode" Display="Dynamic" 
                   ValidationGroup="g1" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    Discount in percentage (%) :
                </td>
                <td>
                    <asp:TextBox ID="txtDiscountpercent" runat="server" Width="200"></asp:TextBox>
                    <cc1:FilteredTextBoxExtender runat="server" ID="FilteredTextBoxExtender1" TargetControlID="txtDiscountpercent"
                    FilterType="Custom" Enabled="true" FilterMode="ValidChars" ValidChars="1234567890">
               </cc1:FilteredTextBoxExtender>
               <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                   ErrorMessage="Discount percentage" ControlToValidate="txtDiscountpercent" Display="Dynamic" 
                   ValidationGroup="g1" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    Product Price :
                </td>
                <td>
                    <asp:TextBox ID="txtPrice" runat="server" Width="200"></asp:TextBox>
                    <cc1:FilteredTextBoxExtender runat="server" ID="FilteredTextBoxExtender6" TargetControlID="txtPrice"
                        FilterType="Custom" Enabled="true" FilterMode="ValidChars" ValidChars="1234567890.">
                   </cc1:FilteredTextBoxExtender>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                   ErrorMessage="Product Price" ControlToValidate="txtPrice" Display="Dynamic" 
                   ValidationGroup="g1" ForeColor="Red"></asp:RequiredFieldValidator>
                   <label><font color="red">* Minimum Price for Coupon Valid.</font></label>
                </td>
            </tr>
            <tr>
            <td></td>
            <td>
                <asp:Button ID="btnCreateSubCategory" runat="server" Text="Add New Coupon" 
                    ValidationGroup="g1" onclick="btnCreateSubCategory_Click" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" />      
              </td>
            </tr>
        </table>
    </div>
</div>
</asp:Content>

