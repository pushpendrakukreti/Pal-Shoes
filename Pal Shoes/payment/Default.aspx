<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="ccAvenue_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ccAvenue Payment</title>
    <script type="text/javascript">
        window.onload = function () {
            var d = new Date().getTime();
            document.getElementById("tid").value = d;
        };
</script>
<style type="text/css">
table.paynow
{
    margin:10px auto 10px auto; padding:0 20px 0 10%; width:80%; border:1px solid #bcbcbc;
    font-family: Arial Baltic, Sans-Serif; font-size:13px;
}
.paynow td{ padding:2px; vertical-align:top; text-align:left;}
.paynow td:nth-child(1){ width:45%;}
.paynow td:nth-child(2){ width:55%;}
.paynow td h2{ margin:0 auto 0 0; padding:0; font-size:1.5em; font-weight:bold;}
</style>
</head>
<body>
    <form id="form1" runat="server">
    <table class="paynow">
         <tr>
            <td colspan="2">
            <img alt="" src="cc-avenue-payment-gateway-integration.jpg" width="250px" />
            <h2>Compulsory Information</h2>
            </td>
         </tr>
	    <tr>
		<td>TID	:</td><td><input type="text" runat="server" name="tid" id="tid" readonly /></td>
	    </tr>
         <tr>
            <td>Merchant Id :</td>
            <td><asp:TextBox ID="merchant_id" runat="server" value="178503"></asp:TextBox>
            </td>
         </tr>
         <tr>
            <td>Order Id :</td>
            <td><asp:TextBox ID="order_id" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="order_id" Display="Dynamic" runat="server" 
                ErrorMessage="" Font-Size="Large" ForeColor="Red" ValidationGroup="g1">*</asp:RequiredFieldValidator>
            </td>
         </tr>
         <tr>
            <td>Amount :</td>
            <td><asp:TextBox ID="amount" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="amount" Display="Dynamic" runat="server" 
                ErrorMessage="" Font-Size="Large" ForeColor="Red" ValidationGroup="g1">*</asp:RequiredFieldValidator>
            </td>
         </tr>
         <tr>
            <td>Currency :</td>
            <td><asp:TextBox ID="currency" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="currency" Display="Dynamic" runat="server" 
                ErrorMessage="" Font-Size="Large" ForeColor="Red" ValidationGroup="g1">*</asp:RequiredFieldValidator>
            </td>
         </tr>
         <tr>
            <td>Redirect URL :</td>
            <td><asp:TextBox ID="redirect_url" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="redirect_url" Display="Dynamic" runat="server" 
                ErrorMessage="" Font-Size="Large" ForeColor="Red" ValidationGroup="g1">*</asp:RequiredFieldValidator>
            </td>
         </tr>
	     <tr>
            <td>Cancel URL :</td>
            <td><asp:TextBox ID="cancel_url" runat="server"></asp:TextBox>
            </td>
         </tr>
         <tr>
            <td colspan="2"><b>Billing information(optional):</b></td>
         </tr>
         <tr>
            <td>Billing Name :</td>
            <td><asp:TextBox ID="billing_name" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="billing_name" Display="Dynamic" runat="server" 
                ErrorMessage="" Font-Size="Large" ForeColor="Red" ValidationGroup="g1">*</asp:RequiredFieldValidator>
            </td>
         </tr>
         <tr>
            <td>Billing Address :</td>
            <td><asp:TextBox ID="billing_address" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ControlToValidate="billing_address" Display="Dynamic" runat="server" 
                ErrorMessage="" Font-Size="Large" ForeColor="Red" ValidationGroup="g1">*</asp:RequiredFieldValidator>
            </td>
         </tr>
         <tr>
            <td>Billing City :</td>
            <td><asp:TextBox ID="billing_city" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ControlToValidate="billing_city" Display="Dynamic" runat="server" 
                ErrorMessage="" Font-Size="Large" ForeColor="Red" ValidationGroup="g1">*</asp:RequiredFieldValidator>
            </td>
         </tr>
         <tr>
            <td>Billing State :</td>
            <td><asp:TextBox ID="billing_state" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" ControlToValidate="billing_state" Display="Dynamic" runat="server" 
                ErrorMessage="" Font-Size="Large" ForeColor="Red" ValidationGroup="g1">*</asp:RequiredFieldValidator>
            </td>
         </tr>
         <tr>
            <td>Billing Zip :</td>
            <td><asp:TextBox ID="billing_zip" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" ControlToValidate="billing_zip" Display="Dynamic" runat="server" 
                ErrorMessage="" Font-Size="Large" ForeColor="Red" ValidationGroup="g1">*</asp:RequiredFieldValidator>
            </td>
         </tr>
         <tr>
            <td>Billing Country:</td>
            <td><asp:TextBox ID="billing_country" runat="server"></asp:TextBox>
            </td>
         </tr>
         <tr>
            <td>Billing Phone :</td>
            <td><asp:TextBox ID="billing_tel" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" ControlToValidate="billing_tel" Display="Dynamic" runat="server" 
                ErrorMessage="" Font-Size="Large" ForeColor="Red" ValidationGroup="g1">*</asp:RequiredFieldValidator>
            </td>
         </tr>
         <tr>
            <td>Billing Email :</td>
            <td><asp:TextBox ID="billing_email" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" ControlToValidate="billing_email" Display="Dynamic" runat="server" 
                ErrorMessage="" Font-Size="Large" ForeColor="Red" ValidationGroup="g1">*</asp:RequiredFieldValidator>
            </td>
         </tr>
         <tr>
          	<td><img alt="" src="ccavenue_logo.png" width="250px" /></td>
			<td>
                <asp:Button ID="btnSubmit" runat="server" Text="Checkout" 
                    onclick="btnSubmit_Click" />
            </td>
         </tr>
    </table>
    </form>
</body>
</html>
