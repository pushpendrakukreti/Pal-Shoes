<%@ Page Title="PayU BOLT" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="payu_bolt._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    
    <script src="http://code.jquery.com/jquery-latest.min.js" type="text/javascript"></script>
    <!-- this meta viewport is required for BOLT //-->
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" >
    <!-- BOLT Sandbox/test //-->
    <script id="bolt" src="https://sboxcheckout-static.citruspay.com/bolt/run/bolt.min.js" bolt-color="e34524" bolt-logo="http://boltiswatching.com/wp-content/uploads/2015/09/Bolt-Logo-e14421724859591.png"></script>
    <!-- BOLT Production/Live //-->
    <!--// script id="bolt" src="https://checkout-static.citruspay.com/bolt/run/bolt.min.js" bolt-color="e34524" bolt-logo="http://boltiswatching.com/wp-content/uploads/2015/09/Bolt-Logo-e14421724859591.png"></script //-->

</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
<style>
span.text 
{
	float:left;
	width:180px;
}

div.dv
{
	margin-bottom:5px;
}

</style>
 <div>
    
    <input type="hidden" id="udf5" name="udf5" value="BOLT_KIT_ASP.NET" />
    <input type="hidden" id="surl" name="surl" value="<%= Session["surl"]%>" />
    <div class="dv">
    <span class="text"><label>Merchant Key:</label></span>
    <span>
        <%--<input type="text" id="key" name="key" placeholder="Merchant Key" value="" />--%>
        <asp:TextBox ID="merchant_key" runat="server" value="178503"></asp:TextBox>
    </span>
    </div>
    
    <div class="dv">
    <span class="text"><label>Merchant Salt:</label></span>
    <span>
        <%--<input type="text" id="salt" name="salt" placeholder="Merchant Salt" value="" />--%>
        <asp:TextBox ID="merchant_salt" runat="server" value="178503"></asp:TextBox>
    </span>
    </div>
    
    <div class="dv">
    <span class="text"><label>Transaction/Order ID:</label></span>
    <span>
        <%--<input type="text" id="txnid" name="txnid" placeholder="Transaction ID" value="<%=Session["txnid"] %>" />--%>
        <input type="text" runat="server" name="tid" id="tid" readonly />
    </span>
    </div>
    
    <div class="dv">
    <span class="text"><label>Amount:</label></span>
    <span>
        <%--<input type="text" id="amount" name="amount" placeholder="Amount" value="6.00" /> --%>  
            
        <asp:TextBox ID="amount" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="amount" Display="Dynamic" runat="server" ErrorMessage="" Font-Size="Large" ForeColor="Red" ValidationGroup="g1">*</asp:RequiredFieldValidator>
    </span> 
    </div>
    
    <div class="dv">
    <span class="text"><label>Product Info:</label></span>
    <span>
        <%--<input type="text" id="pinfo" name="pinfo" placeholder="Product Info" value="P01,P02" />--%>
        <asp:TextBox ID="txtProductInfo" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtProductInfo" Display="Dynamic" runat="server" 
                ErrorMessage="" Font-Size="Large" ForeColor="Red" ValidationGroup="g1">*</asp:RequiredFieldValidator>
    </span>
    </div>
    
    <div class="dv">
    <span class="text"><label>First Name:</label></span>
    <span>
        <%--<input type="text" id="fname" name="fname" placeholder="First Name" value="" />--%>
        <asp:TextBox ID="billing_name" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="billing_name" Display="Dynamic" runat="server" ErrorMessage="" Font-Size="Large" ForeColor="Red" ValidationGroup="g1">*</asp:RequiredFieldValidator>
    </span>
    </div>
    
    <div class="dv">
    <span class="text"><label>Email ID:</label></span>
    <span>
       <%-- <input type="text" id="email" name="email" placeholder="Email ID" value="" />--%>
        <asp:TextBox ID="billing_email" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" ControlToValidate="billing_email" Display="Dynamic" runat="server" ErrorMessage="" Font-Size="Large" ForeColor="Red" ValidationGroup="g1">*</asp:RequiredFieldValidator>
    </span>
    </div>
    
    <div class="dv">
    <span class="text"><label>Mobile/Cell Number:</label></span>
    <span>
        <%--<input type="text" id="mobile" name="mobile" placeholder="Mobile/Cell Number" value="" />--%>
        <asp:TextBox ID="billing_tel" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" ControlToValidate="billing_tel" Display="Dynamic" runat="server" ErrorMessage="" Font-Size="Large" ForeColor="Red" ValidationGroup="g1">*</asp:RequiredFieldValidator>
    </span>
    </div>
    
    <div class="dv">
    <span class="text"><label>Hash:</label></span>
    <span>
       <%-- <input type="text" id="hash" name="hash" placeholder="Hash" value="" />--%>
    </span>
    </div>
    
    
    <div>
       <%-- <input type="submit" value="Pay" onclick="launchBOLT(); return false;" />--%>
        <asp:Button ID="btnSubmit" runat="server" Text="Checkout" OnClick="btnSubmit_Click" />
  </div>
	
</div>

<script type="text/javascript"><!--
    $(document).ready(function () {        
        $('#payment_form').keyup(function () {            
            $.ajax({
                url: 'Hash.aspx',
                type: 'post',
                data: JSON.stringify({
                    key: $('#key').val(),
                    salt: $('#salt').val(),
                    txnid: $('#txnid').val(),
                    amount: $('#amount').val(),
                    pinfo: $('#pinfo').val(),
                    fname: $('#fname').val(),
                    email: $('#email').val(),
                    mobile: $('#mobile').val(),
                    udf5: $('#udf5').val()
                }),
                contentType: "application/json",
                dataType: 'json',
                success: function (json) {
                    if (json['error']) {
                        $('#alertinfo').html('<i class="fa fa-info-circle"></i>' + json['error']);
                    }
                    else if (json['success']) {
                        $('#hash').val(json['success']);
                    }
                }
            });
        });
    });
//-->
</script>
<script type="text/javascript"><!--
    function launchBOLT() {
        bolt.launch({
            key: $('#key').val(),
            txnid: $('#txnid').val(),
            hash: $('#hash').val(),
            amount: $('#amount').val(),
            firstname: $('#fname').val(),
            email: $('#email').val(),
            phone: $('#mobile').val(),
            productinfo: $('#pinfo').val(),
            udf5: $('#udf5').val(),
            surl: $('#surl').val(),
            furl: $('#surl').val()
        }, { responseHandler: function (BOLT) {
            console.log(BOLT.response.txnStatus);
            if (BOLT.response.txnStatus != 'CANCEL') {
                //Salt is passd here for demo purpose only. For practical use keep salt at server side only.
                var fr = '<form action=\"' + $('#surl').val() + '\" method=\"post\">' +
		'<input type=\"hidden\" name=\"key\" value=\"' + BOLT.response.key + '\" />' +
		'<input type=\"hidden\" name=\"salt\" value=\"' + $('#salt').val() + '\" />' +
		'<input type=\"hidden\" name=\"txnid\" value=\"' + BOLT.response.txnid + '\" />' +
		'<input type=\"hidden\" name=\"amount\" value=\"' + BOLT.response.amount + '\" />' +
		'<input type=\"hidden\" name=\"productinfo\" value=\"' + BOLT.response.productinfo + '\" />' +
		'<input type=\"hidden\" name=\"firstname\" value=\"' + BOLT.response.firstname + '\" />' +
		'<input type=\"hidden\" name=\"email\" value=\"' + BOLT.response.email + '\" />' +
		'<input type=\"hidden\" name=\"udf5\" value=\"' + BOLT.response.udf5 + '\" />' +
		'<input type=\"hidden\" name=\"mihpayid\" value=\"' + BOLT.response.mihpayid + '\" />' +
		'<input type=\"hidden\" name=\"status\" value=\"' + BOLT.response.status + '\" />' +
		'<input type=\"hidden\" name=\"hash\" value=\"' + BOLT.response.hash + '\" />' +
		'</form>';
                var form = jQuery(fr);
                jQuery('body').append(form);
                form.submit();
            }
        },
            catchException: function (BOLT) {
                alert(BOLT.message);
            }
        });
    }
    //--
</script>	

</asp:Content>