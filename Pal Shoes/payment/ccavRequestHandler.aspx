<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ccavRequestHandler.aspx.cs" Inherits="SubmitData" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.10.2/jquery.min.js"></script>  
        <script type="text/javascript">
            $(document).ready(function () {
                jQuery("#btnSubmit").click();
                //$("#nonseamless").submit();
            });
       </script>
    <title>Custom Form Kit</title>
</head>
<body>
    <%--<form id="nonseamless" method="post" name="redirect" action="https://secure.ccavenue.com/transaction/transaction.do?command=initiateTransaction"> 
        <input type="hidden" id="encRequest" name="encRequest" value="<%=strEncRequest%>"/>
        <input type="hidden" name="access_code" id="Hidden1" value="<%=strAccessCode%>"/>
    </form>--%>
    <form id="form1" runat="server">
    <table style="display: none;">
        <tr>
            <td>
                TID:
            </td>
            <td>
                <asp:Label ID="lbltid" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                MerchantId:
            </td>
            <td>
                <asp:Label ID="lblMerchantId" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                OrderId:
            </td>
            <td>
                <asp:Label ID="lblOrderId" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                Amount:
            </td>
            <td>
                <asp:Label ID="lblAmount" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                Currency:
            </td>
            <td>
                <asp:Label ID="lblcurrency" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                RedirectURL:
            </td>
            <td>
                <asp:Label ID="lblRedirectUrl" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                CancelURL:
            </td>
            <td>
                <asp:Label ID="lblCancelUrl" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                BillingCustomerName:
            </td>
            <td>
                <asp:Label ID="lblCustomerName" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                BillingCustomerAddress:
            </td>
            <td>
                <asp:Label ID="lblCustAddr" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                BillingCustomerCity:
            </td>
            <td>
                <asp:Label ID="lblCustCity" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                BillingCustomerState:
            </td>
            <td>
                <asp:Label ID="lblCustState" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                ZipCode:
            </td>
            <td>
                <asp:Label ID="lblZipCode" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                BillingCustomerCountry:
            </td>
            <td>
                <asp:Label ID="lblCustCountry" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                BillingCustomerTelephone:
            </td>
            <td>
                <asp:Label ID="lblCustPhone" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                BillingCustomerEmail:
            </td>
            <td>
                <asp:Label ID="lblCustEmail" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Button ID="btnSubmit" runat="server" Text="Submit" PostBackUrl="https://secure.ccavenue.com/transaction/transaction.do?command=initiateTransaction" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:HiddenField ID="encRequest" runat="server" />
                <asp:HiddenField ID="access_code" runat="server" />
            </td>
        </tr>
    </table>
    <center>
        <asp:Label ID="lb_Message" Style="color: Red; display: block; margin-top: 35px;"
            runat="server" Text=""></asp:Label>
    </center>
    </form>
</body>
</html>
