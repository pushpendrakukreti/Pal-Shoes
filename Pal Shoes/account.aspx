<%@ Page Language="C#" AutoEventWireup="true" Culture="en-GB" CodeFile="account.aspx.cs" Inherits="account" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="X-UA-Compatible" content="IE=edge" />
<meta name="keywords" content=""/>
<meta name="distribution" content="global"/>
<meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1"/>
   <title>Pal Shoes</title>
<link rel="stylesheet" type="text/css" href="css/font-awesome.min.css"/>
<link rel="stylesheet" type="text/css" href="css/bootstrap.css"/>
<link rel="stylesheet" type="text/css" href="css/jquery-ui.css"/>
<link rel="stylesheet" type="text/css" href="css/owl.carousel.css"/>
<link rel="stylesheet" type="text/css" href="css/fotorama.css"/>
<link rel="stylesheet" type="text/css" href="css/custom.css"/>
<link rel="stylesheet" type="text/css" href="css/responsive.css"/>

<link rel="shortcut icon" href="images/favicon.png"/>
<%--<link rel="apple-touch-icon" href="images/apple-touch-icon.html"/>
<link rel="apple-touch-icon" sizes="72x72" href="images/apple-touch-icon-72x72.html"/>
<link rel="apple-touch-icon" sizes="114x114" href="images/apple-touch-icon-114x114.html"/>--%>
<style type="text/css">
.mGrid { width: 100%; background-color: #fff; margin:0px 1px 2px 1px; border: solid 1px #525252; border-collapse:collapse; }
.mGrid td { padding: 7px; border: solid 1px #c1c1c1; color: #717171;font-size: 14px;}
.mGrid th { padding: 7px; color: #fff; background: #424242 url(/images/grd_head.png) repeat-x top; border-left: solid 1px #525252; font-size:14px; }
.mGrid .alt { }
.mGrid .pgr {background: #424242 url(/images/grd_pgr.png) repeat-x top; height:32px;}
.mGrid .pgr table { margin: 5px 0; }
.mGrid .pgr td { border-width: 0; padding: 0 6px; border-left: solid 1px #666;  font-weight: bold; color: #fff; line-height: 12px; font-size:1.1em;}

.mGrid td a{ text-decoration:none; color:#0000FF;}

.red{ color:Red; font-size:14px;}
.red a{ color:Blue; }
.logo1{ padding-top:2px;}

.addw{ margin:-60px 0 10px 0; vertical-align:middle; padding-right:10px;}
.addw h3{ margin:20px 20px 0 auto; float:left; }


    .modalBackground
    {
        background-color: Black;
        filter: alpha(opacity=40);
        opacity: 0.4;
    }
    .modalPopup
    {
        background-color: #FFFFFF;
        width: 300px;
        border: 2px solid #0DA9D0;
    }
    .modalPopup .body
    {
        min-height: 50px;
        line-height: 25px;
        text-align: center;
        font-weight: bold;
        padding:15px 5px 5px 5px;
    }
    .modalPopup .footer
    {
        padding: 1px;
    }
    .modalPopup .yes, .modalPopup .no
    {
        height: 22px;
        color: White;
        line-height: 20px;
        text-align: center;
        font-weight: bold;
        cursor: pointer;
    }
    .footer1 { margin:0; padding:2px 2px 15px 2px; text-align:center; color:Black;}

    .commun-table th ul li {
        margin-right: 95px
    }
    label { margin-bottom: 0px }
</style>
<link href="autocomplete/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="autocomplete/jquery-ui.min.js"></script>
    <script src="autocomplete/jquery.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            SearchText();
        });
        function SearchText() {
            $("#txtSearch").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "Default.aspx/GetSerchResult",
                        data: "{'searchdata':'" + document.getElementById('txtSearch').value + "'}",
                        dataType: "json",
                        success: function (data) {
                            response(data.d);
                        },
                        error: function (result) {
                            alert("No Match");
                        }
                    });
                }
            });
        }  
    </script>
</head>
<body>
  <form id="form1" runat="server">
  <asp:ScriptManager ID="ScriptManager1" runat="server">
  </asp:ScriptManager>
    <div class="main">
  <header class="navbar navbar-custom" id="header">
    <div class="header-top">
      <div class="container">
        <div class="header-top-inner">
          <div class="row">
           <div class="col-md-3">
			<div class="navbar-header float-none-sm">
                <a class="navbar-brand page-scroll" href="http://palshoes.com">
                  <img alt="Palshoes" class="logo1" src="images/palshoes-logo.png" />
                </a>
				<button data-target=".navbar-collapse" data-toggle="collapse" class="navbar-toggle" type="button">
				<big><big><i class="fa fa-bars"></i></big></big></button>
            </div>
            </div>
            <div class="col-md-9">
              <div class="top-link right-side">
                <ul>
                  <li><a style="padding-right: 100px;border-right: none"> </a></li>

                  <li class="account-icon"><a href="account.aspx" title="My Account"><span></span> My Account</a></li>
                  <li class="wishlist-icon">
                  <asp:Panel ID="pnlwishlist" runat="server" Visible="false">
                  <a href="wishlist.aspx" title="Wishlist"><span></span>My Wishlist
                  <asp:Repeater ID="Rpwishlist" runat="server">
                        <ItemTemplate>
                        (<%# Eval("wishNum")%>)
                        </ItemTemplate>
                   </asp:Repeater>
                  </a>
                  </asp:Panel>
                  </li>
                  <li class="Compare-icon"><a href="compare.aspx" title="Compare"><span></span>Compare
                  <asp:Repeater ID="rpCompare" runat="server">
                        <ItemTemplate>
                        (<%# Eval("compare")%>)
                        </ItemTemplate>
                   </asp:Repeater>
                  </a></li>
                  <li><a href="register.aspx">
                  <asp:LinkButton ID="lnkLogin" runat="server" onclick="lnkLogin_Click"></asp:LinkButton></a>
                  </li>
                </ul>
               </div>
			  

			   <div class="header-right-part right-side float-none-xs">
                <ul>
				 <li class="mobile-view-search visible-sm visible-xs visible-md visible-lg">
                    <div class="mobile-view">
                      <div>
                        <div class="search-box">
                       <asp:TextBox ID="txtSearch" runat="server" placeholder="Search product..." class="input-text"></asp:TextBox>
                       <asp:ImageButton ID="Searchbtn" OnClick="Searchbtn_Click" CssClass="search-btn" ImageUrl="~/img/search.png" runat="server"></asp:ImageButton>
                        </div>
                      </div>
                    </div>
                  </li>
                  <li class="shipping-icon visible-lg visible-md visible-sm visible-xs">
                    <a href="#">
                     <span>
                      </span>
                      <div class="header-right-text">&nbsp;</div>
                    </a>
                  </li>
                  <li class="cart-icon">
                    <a href="cart.aspx">
                      <span>
                      <small class="cart-notification">
                        <asp:Repeater ID="rpnotification" runat="server">
                        <ItemTemplate>
                          <%# Eval("Items")%>
                        </ItemTemplate>
                        </asp:Repeater>
                        </small>
                      </span>
                      <div class="header-right-text">Shopping Cart</div>
                      <div class="header-price">
                      <asp:Repeater ID="rpItempPrice" runat="server">
                        <ItemTemplate>
                          <%# Eval("itmeprice")%>
                        </ItemTemplate>
                        </asp:Repeater>
                      </div>
                    </a>
                  </li>
                    <li class="shipping-icon visible-lg visible-md visible-sm visible-xs">
                    
                  </li>
                </ul>
              </div>

            <div id="menu" class="navbar-collapse collapse left-side" >
			
          <ul class="nav navbar-nav navbar-left">
            <li class="level"><a href="Default.aspx" class="page-scroll">Home</a></li>
            <li class="level"><a href="about.aspx" class="page-scroll">About Us</a></li>
            
            <li class="level">
              <span class="opener plus"></span>
              <a href="#" class="page-scroll">Products</a>
			  <div class="megamenu mobile-sub-menu">
                  <div class="megamenu-inner-top">
                   <ul class="sub-menu-level1">
                    <asp:Repeater ID="rpMenuCategory" runat="server" OnItemDataBound="Menu_ItemDataBound">
                    <ItemTemplate>
                    <li class="level2">
                    <a href="product.aspx?category=<%# Eval("CategoryId") %>"><span><%# Eval("CategoryName") %></span></a>
                        <ul class="sub-menu-level2 ">
                            <asp:Repeater ID="rpSubCategory" runat="server">
                            <ItemTemplate>
                                <li class="level3"><a href='product.aspx?subcategory=<%# Eval("SubCategoryId") %>'><%# Eval("SubName") %></a></li>
                            </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                    </li>
                    </ItemTemplate>
                    </asp:Repeater>
                    </ul>
                  </div>
              </div>
			</li>
            <li class="level"><a href="blog_list.aspx" class="page-scroll">Blog</a></li>
            <li class="level"><a href="event_list.aspx" class="page-scroll">Events</a></li>
          </ul>
		 
        </div>
   </div>
          
    </div>
    </div>
    </div>
</div>
</header>
  <!-- HEADER END -->


  <!-- Bread Crumb STRAT -->
  <div class="container">
    <div class="bread-crumb mtb-30 center-xs">
      <div class="page-title">Account</div>
      <div class="bread-crumb-inner right-side float-none-xs">
        <ul>
          <li><a href="Default.aspx">Home</a><i class="fa fa-angle-right"></i></li>
          <li><span>Account Settings</span></li>
        </ul>
      </div>
    </div>
  </div>
  <!-- Bread Crumb END -->
  

            <asp:HiddenField ID="HiddenField1" runat="server"></asp:HiddenField>
                <cc1:ModalPopupExtender ID="mpe" runat="server" PopupControlID="pnlPopup" TargetControlID="HiddenField1" 
                    CancelControlID="btnNo" BackgroundCssClass="modalBackground">
                </cc1:ModalPopupExtender>
                <asp:Panel ID="pnlPopup" runat="server" CssClass="modalPopup" Style="display: none">
                    <div class="body">
                       <p class="text-center"><asp:Label ID="lblpopup" runat="server" Text=""></asp:Label></p>
                       <label id="lbla" runat="server">Amount :</label>&nbsp;
                       <asp:TextBox ID="txtwalletamount" Width="110px" Height="25px" runat="server"></asp:TextBox>
                    </div>
                    <div class="footer1" align="right">
                        <asp:Button ID="Addwallet" runat="server" OnClick="Addwallet_Click" Text="Add to Wallet" />
                        &nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnNo" runat="server" Text="Close" />
                    </div>
                </asp:Panel>

  <!-- CONTAIN START -->
  <section class="checkout-section pb-95">
    <div class="container">
      <div class="row">
        <div class="col-md-3 col-sm-4">
          <div class="account-sidebar account-tab mb-xs-30">
            <div class="tab-title-bg">
              <div class="heading-part">
                <div class="sub-title"><span></span> My Account</div>
              </div>
            </div>
            <div class="account-tab-inner">
              <ul class="account-tab-stap">
                <li id="step1" class="active">
                  <a href="javascript:void(0)">My Dashboard<i class="fa fa-angle-right"></i>
                  </a>
                </li>
                <li id="step3">
                  <a href="javascript:void(0)">My Order List<i class="fa fa-angle-right"></i>
                  </a>
                </li>
                <li id="step2">
                  <a href="javascript:void(0)">Account Details<i class="fa fa-angle-right"></i>
                  </a>
                </li>
                <%--<li id="step6">
                  <a href="javascript:void(0)">Withdrawal Request<i class="fa fa-angle-right"></i>
                  </a>
                </li>
                <li id="step5">
                  <a href="javascript:void(0)">Fix Amount<i class="fa fa-angle-right"></i>
                  </a>
                </li>--%>
                <li id="step4">
                  <a href="javascript:void(0)">Change Password<i class="fa fa-angle-right"></i>
                  </a>
                </li>
              </ul>
            </div>
          </div>
        </div>
        <div class="col-md-9 col-sm-8">
          <div id="data-step1" class="account-content" data-temp="tabdata">
            <div class="row">
              <div class="col-xs-12">
                <div class="heading-part heading-bg mb-30">
                  <h2 class="heading m-0">Account Dashboard</h2>
                </div>
              </div>
            </div>
            <div class="mb-30">
              <div class="row">
                <div class="col-xs-12">
                  <div class="heading-part">
                    <h3 class="sub-heading">Hello, <asp:Label ID="lblUserName" runat="server" Text=""></asp:Label></h3>
                    <%--<div class="addw right-side">
                    <h3>Add To Wallet</h3>
                    <asp:ImageButton ID="imgAddtowallet" Width="70px" 
                            ImageUrl="~/img/addtowallet2.png" runat="server" onclick="imgAddtowallet_Click"></asp:ImageButton>
                    </div>--%>
                    
                  </div>
                  <asp:Label ID="lblMessage" Width="95%" runat="server" Text=""></asp:Label>
                <hr/>
                </div>
              </div>
            </div>
            <div class="m-0">
              <div class="row">
                <div class="col-sm-6">
                  <div class="cart-total-table address-box commun-table">
                    <div class="table-responsive">
                      <table class="table">
                        <thead>
                          <tr>
                            <th>Shipping Address</th>
                          </tr>
                        </thead>
                        <tbody>
                          <tr>
                            <td>
                              <ul>
                              <asp:Repeater ID="rpShippingAddress" runat="server">
                              <ItemTemplate>
                              <li class="inner-heading">
                                <b><%# Eval("Shippingname")%></b>
                              </li>
                              <li>
                                <p><%# Eval("ShipAddress")%>,<br /><%# Eval("ShipNearby")%></p>
                              </li>
                              <li>
                                <p><%# Eval("ShipCity")%> - <%# Eval("ShipZip")%>, <%# Eval("State")%></p>
                              </li>
                              <li>
                                <p><%# Eval("Country")%></p>
                              </li>
                              </ItemTemplate>
                              </asp:Repeater>
                              </ul>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                    </div>
                  </div>
                </div>
                <div class="col-sm-6">
                  <div class="cart-total-table address-box commun-table">
                    <div class="table-responsive">
                      <table class="table">
                        <thead>
                          <tr>
                            <th>Billing Address</th>
                          </tr>
                        </thead>
                        <tbody>
                          <tr>
                            <td>
                              <ul>
                             <asp:Repeater ID="rpBillingAddress" runat="server">
                             <ItemTemplate>
                              <li class="inner-heading">
                                <b><%# Eval("Billingname")%></b>
                              </li>
                              <li>
                                <p><%# Eval("BillAddress")%>,<br /><%# Eval("BillNearby")%></p>
                              </li>
                              <li>
                                <p><%# Eval("BillCity")%> - <%# Eval("BillZip")%>, <%# Eval("State")%></p>
                              </li>
                              <li>
                                <p><%# Eval("Country")%></p>
                              </li>
                              </ItemTemplate>
                             </asp:Repeater>
                              </ul>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                    </div>
                  </div>
                </div>
              </div>

        <%--<div class="row">
        <div class="col-xs-12 mb-xs-30">
        <br />
            <div class="heading-part heading-bg mtb-20">
                <h2 class="heading m-0">Wallet Details</h2>
            </div>
            <table class="ord">
        <tr>
        <td>
            Date: <asp:TextBox ID="txtDate1" runat="server" Width="150px"></asp:TextBox>
            <img src="~/img/Calendar.gif" id="img_date1" runat="server" alt="" />
            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="img_date1"
                TargetControlID="txtDate1" Format="dd/MM/yyyy">
            </cc1:CalendarExtender>
            <asp:CustomValidator ID="CustomValidator1" runat="server" ClientValidationFunction="ValidateDate" 
            ControlToValidate="txtDate1" ErrorMessage="dd/mm/yyyy" ValidationGroup="w1" ForeColor="Red" />
        </td>
        <td>
            To Date: <asp:TextBox ID="txtDate2" runat="server" Width="150px"></asp:TextBox>
            <img src="~/img/Calendar.gif" id="img_date2" runat="server" alt="" />
            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" PopupButtonID="img_date2"
                TargetControlID="txtDate2" Format="dd/MM/yyyy">
            </cc1:CalendarExtender>
            <asp:CustomValidator ID="CustomValidator2" runat="server" ClientValidationFunction="ValidateDate" 
            ControlToValidate="txtDate2" ErrorMessage="dd/mm/yyyy" ValidationGroup="w1" ForeColor="Red" />
        </td>
        <td><asp:Button ID="btnGetDetails" CssClass="getbutton" runat="server" ValidationGroup="w1" 
                Text="Get Detail" onclick="btnGetDetails_Click" />
        </td>
        </tr>
        </table>
        <asp:GridView ID="gvWalletDetails" runat="server" GridLines="None" 
            CssClass="mGrid" PagerStyle-CssClass="pgr" 
            AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False" 
            PageSize="20" onpageindexchanging="gvWalletDetails_PageIndexChanging">
        <Columns>
            <asp:TemplateField HeaderText="S.N.">
                <ItemTemplate>
                    <%# Container.DataItemIndex + 1 %>
                </ItemTemplate>
            </asp:TemplateField>
            <%--<asp:BoundField DataField="Customername" HeaderText="Name" />
            <asp:BoundField DataField="totamount" HeaderText="Balance" />
            <asp:BoundField DataField="AddedAmount" HeaderText="Added" />
            <asp:BoundField DataField="DeductedAmount" HeaderText="Deduct" />
            <asp:BoundField DataField="AddedDate" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Date" />
            <asp:BoundField DataField="TransactionNo" HeaderText="Trans. No" />
            <asp:BoundField DataField="Transactiontype" HeaderText="Payment" />
            <%--<asp:BoundField DataField="WalletStatus" HeaderText="Remarks" />
        </Columns>
        <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
        <HeaderStyle Height="32px" />
        <PagerStyle CssClass="pgr"></PagerStyle>
        </asp:GridView>
              </div>
            </div>--%>

        </div>
        </div>
          
          <%--<div id="data-step6" class="account-content" data-temp="tabdata" style="display:none">
           <div class="row">
              <div class="col-xs-12">
                <div class="heading-part heading-bg mb-30">
                  <h2 class="heading m-0">Amount Request</h2>
                </div>
              </div>
            </div>
            <div class="m-0">
              <div class="main-form full">
                <div class="mb-20">
                  <div class="row">
                    <div class="col-xs-12">
                      <div class="heading-part">
                        <h3 class="sub-heading">Amount Withdrawal Request</h3>
                      </div>
                      <hr>
                    </div>
                    <div class="col-sm-12">
                      <p class="red">If you want to refund you money from wallet you can just send a request to the admin department of Palshoes.com. 
                      It will be take 24 hours to refund by "term and condition". <br />
                      To send amount request click <a href="fundtransferrequest.aspx"><u>Fund Transfer Request</u>.</a></p>
                      <br />
                    </div>
                    </div>
                    <div class="row">
                <div class="col-xs-12">
                
          <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
          <Triggers>
          <asp:PostBackTrigger  ControlID="btnAddAccount" />
          </Triggers>
          <ContentTemplate>
            <div class="m-0">
              <div class="main-form full">
                <div class="mb-20">
                  <div class="row">
                    <div class="col-xs-12 mb-20">
                      <div class="heading-part">
                        <h4 class="sub-heading">If you are requesting first time, you have to update your bank details below.
                        <br /><b>Update Your Bank Details</b></h4>
                      </div>
                      <hr>
                    </div>
                    <div class="col-sm-6">
                      <div class="input-box">
                        <asp:TextBox ID="txtAcFullname" placeholder="Full Name (Bank Account)" runat="server"></asp:TextBox>
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" 
                            ErrorMessage="Full Name (Bank Account)" ControlToValidate="txtAcFullname" Display="Dynamic" 
                            ValidationGroup="g2" ForeColor="Red"></asp:RequiredFieldValidator>
                      </div>
                    </div>
                    <div class="col-sm-6">
                      <div class="input-box">
                        <asp:TextBox ID="txtMobileNo" placeholder="Contact Number" runat="server"></asp:TextBox>
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" 
                            ErrorMessage="Mobile Number" ControlToValidate="txtMobileNo" Display="Dynamic" 
                            ValidationGroup="g2" ForeColor="Red"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtMobileNo"
                            ErrorMessage="Invalid Contact No" ValidationExpression="^[56789]\d{9}$" ValidationGroup="g1"
                            ForeColor="Red"></asp:RegularExpressionValidator>
                      </div>
                    </div>
                   <div class="col-sm-6">
                      <div class="input-box">
                        <asp:TextBox ID="txtBankName" placeholder="Bank Name" runat="server"></asp:TextBox>
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator25" runat="server" 
                            ErrorMessage="Bank Name" ControlToValidate="txtBankName" Display="Dynamic" 
                            ValidationGroup="g2" ForeColor="Red"></asp:RequiredFieldValidator>
                      </div>
                    </div>
                    <div class="col-sm-6">
                      <div class="input-box">
                        <asp:TextBox ID="txtAccountNumber" placeholder="Acount Number" runat="server"></asp:TextBox>
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" 
                            ErrorMessage="Bank Account Number" ControlToValidate="txtAccountNumber" Display="Dynamic" 
                            ValidationGroup="g2" ForeColor="Red"></asp:RequiredFieldValidator>
                        <cc1:FilteredTextBoxExtender runat="server" ID="FilteredTextBoxExtender9" TargetControlID="txtAccountNumber"
                            FilterType="Custom" Enabled="true" FilterMode="ValidChars" ValidChars="1234567890"></cc1:FilteredTextBoxExtender>
                      </div>
                    </div>
                    <div class="col-sm-6">
                      <div class="input-box">
                        <asp:TextBox ID="txtIFSCCode" placeholder="IFSC Code" runat="server"></asp:TextBox>
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" 
                            ErrorMessage="IFSC Code" ControlToValidate="txtIFSCCode" Display="Dynamic" 
                            ValidationGroup="g2" ForeColor="Red"></asp:RequiredFieldValidator>
                      </div>
                    </div>
                    <div class="col-sm-12">
                      <asp:Button ID="btnAddAccount" runat="server" OnClick="btnAddAccount_Click" Text="Update Bank Details" 
                        class="btn-black" ValidationGroup="g2"></asp:Button>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </ContentTemplate>
          </asp:UpdatePanel>
          </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>--%>

          <div id="data-step2" class="account-content" data-temp="tabdata" style="display:none">
          <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
          <Triggers>
          <asp:PostBackTrigger ControlID="lnkSubmit" />
          </Triggers>
          <ContentTemplate>
            <div class="row">
              <div class="col-xs-12">
                <div class="heading-part heading-bg mb-30">
                  <h2 class="heading m-0">Account Details</h2>
                </div>
              </div>
            </div>
            <div class="m-0">
              <div class="main-form full">
                <div class="mb-20">
                  <div class="row">
                    <div class="col-xs-12 mb-20">
                      <div class="heading-part">
                        <h3 class="sub-heading">Billing Address</h3>
                      </div>
                      <hr>
                    </div>
                    <div class="col-sm-6">
                      <div class="input-box">
                        <asp:TextBox ID="txtFullNameBill" placeholder="Full Name" runat="server"></asp:TextBox>
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                            ErrorMessage="Full Name" ControlToValidate="txtFullNameBill" Display="Dynamic" 
                            ValidationGroup="g1" ForeColor="Red"></asp:RequiredFieldValidator>
                      </div>
                    </div>
                    <div class="col-sm-6">
                      <div class="input-box">
                        <asp:TextBox ID="txtEmailAddressBill" placeholder="Email Address" runat="server"></asp:TextBox>
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                            ErrorMessage="Email Address" ControlToValidate="txtEmailAddressBill" Display="Dynamic" 
                            ValidationGroup="g1" ForeColor="Red"></asp:RequiredFieldValidator>
                           <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Invalid Email ID"
                            ControlToValidate="txtEmailAddressBill" Display="Dynamic" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                            ValidationGroup="g1" ForeColor="Red"></asp:RegularExpressionValidator>
                      </div>
                    </div>
                    <div class="col-sm-6">
                      <div class="input-box">
                        <asp:TextBox ID="txtCompanyBill" placeholder="Company" runat="server"></asp:TextBox>
                      </div>
                    </div>
                    <div class="col-sm-6">
                      <div class="input-box">
                        <asp:TextBox ID="txtContactNumberBill" placeholder="Contact Number" runat="server"></asp:TextBox>
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                            ErrorMessage="Contact Number" ControlToValidate="txtContactNumberBill" Display="Dynamic" 
                            ValidationGroup="g1" ForeColor="Red"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtContactNumberBill"
                            ErrorMessage="Invalid Contact No" ValidationExpression="^[56789]\d{9}$" ValidationGroup="g1"
                            ForeColor="Red"></asp:RegularExpressionValidator>
                      </div>
                    </div>
                    <div class="col-sm-6">
                      <div class="input-box">
                        <asp:TextBox ID="txtBillingAddressBill" placeholder="Address" runat="server"></asp:TextBox>
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                            ErrorMessage="Address" ControlToValidate="txtBillingAddressBill" Display="Dynamic" 
                            ValidationGroup="g1" ForeColor="Red"></asp:RequiredFieldValidator>
                      </div>
                    </div>
                    <div class="col-sm-6">
                      <div class="input-box">
                        <asp:TextBox ID="txtBillingLandmarkBill" placeholder="Landmark" runat="server"></asp:TextBox>
                      </div>
                    </div>
                    <div class="col-sm-6">
                      <div class="input-box">
                        <asp:TextBox ID="txtSelectCityBill" placeholder="City" runat="server"></asp:TextBox>
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" 
                            ErrorMessage="City" ControlToValidate="txtSelectCityBill" Display="Dynamic" 
                            ValidationGroup="g1" ForeColor="Red"></asp:RequiredFieldValidator>
                      </div>
                    </div>
                    <div class="col-sm-6">
                      <div class="input-box">
                        <asp:DropDownList ID="ddStateBill" runat="server">
                         </asp:DropDownList>
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                            ErrorMessage="State" ControlToValidate="ddStateBill" Display="Dynamic" 
                            ValidationGroup="g1" ForeColor="Red" InitialValue="Select State"></asp:RequiredFieldValidator>
                      </div>
                    </div>
                    <div class="col-sm-6">
                      <div class="input-box">
                        <asp:DropDownList ID="ddCountryBill" runat="server">
                         </asp:DropDownList>
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                            ErrorMessage="Country" ControlToValidate="ddCountryBill" Display="Dynamic" 
                            ValidationGroup="g1" ForeColor="Red" InitialValue="Select Country"></asp:RequiredFieldValidator>
                      </div>
                    </div>
                    <div class="col-sm-6">
                      <div class="input-box">
                        <asp:TextBox ID="txtPostcodezipBill" placeholder="Postcode/zip" runat="server"></asp:TextBox>
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" 
                            ErrorMessage="Postcode/zip" ControlToValidate="txtPostcodezipBill" Display="Dynamic" 
                            ValidationGroup="g1" ForeColor="Red"></asp:RequiredFieldValidator>
                      </div>
                    </div>
                    <div class="col-sm-12">
                    <div class="check-box">
                        <span>
                        <asp:CheckBox ID="CheckBox1" class="checkbox" AutoPostBack="true" 
                            runat="server" oncheckedchanged="CheckBox1_CheckedChanged"></asp:CheckBox>
                        </span>
                        <label for="chk-billing-address"><big>Use my billing address as my shipping address.</big></label>
                    </div>
                    </div>
                  </div>
                </div>
                <div class="">
                  <div class="row">
                    <div class="col-xs-12 mb-20">
                      <div class="heading-part">
                        <h3 class="sub-heading">Shipping Address</h3>
                      </div>
                      <hr>
                    </div>
                    <div class="col-sm-6">
                      <div class="input-box">
                        <asp:TextBox ID="txtFullName" placeholder="Full Name" runat="server"></asp:TextBox>
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" 
                            ErrorMessage="Full Name" ControlToValidate="txtFullName" Display="Dynamic" 
                            ValidationGroup="g1" ForeColor="Red"></asp:RequiredFieldValidator>
                      </div>
                    </div>
                    <div class="col-sm-6">
                      <div class="input-box">
                        <asp:TextBox ID="txtEmailAddress" placeholder="Email Address" runat="server"></asp:TextBox>
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" 
                            ErrorMessage="Email Address" ControlToValidate="txtEmailAddress" Display="Dynamic" 
                            ValidationGroup="g1" ForeColor="Red"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Invalid Email ID"
                            ControlToValidate="txtEmailAddress" Display="Dynamic" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                            ValidationGroup="g1" ForeColor="Red"></asp:RegularExpressionValidator>
                      </div>
                    </div>
                    <div class="col-sm-6">
                      <div class="input-box">
                        <asp:TextBox ID="txtCompany" placeholder="Company" runat="server"></asp:TextBox>
                      </div>
                    </div>
                    <div class="col-sm-6">
                      <div class="input-box">
                        <asp:TextBox ID="txtContactNumber" placeholder="Contact Number" runat="server"></asp:TextBox>
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" 
                            ErrorMessage="Contact Number" ControlToValidate="txtContactNumber" Display="Dynamic" 
                            ValidationGroup="g1" ForeColor="Red"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtContactNumber"
                            ErrorMessage="Invalid Contact No" ValidationExpression="^[56789]\d{9}$" ValidationGroup="g1"
                            ForeColor="Red"></asp:RegularExpressionValidator>
                      </div>
                    </div>
                    <div class="col-sm-6">
                      <div class="input-box">
                        <asp:TextBox ID="txtShippingAddress" placeholder="Address" runat="server"></asp:TextBox>
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" 
                            ErrorMessage="Address" ControlToValidate="txtShippingAddress" Display="Dynamic" 
                            ValidationGroup="g1" ForeColor="Red"></asp:RequiredFieldValidator>
                      </div>
                    </div>
                    <div class="col-sm-6">
                      <div class="input-box">
                        <asp:TextBox ID="txtShippingLandmark" placeholder="Landmark" runat="server"></asp:TextBox>
                      </div>
                    </div>
                    <div class="col-sm-6">
                      <div class="input-box">
                        <asp:TextBox ID="txtSelectCity" placeholder="City" runat="server"></asp:TextBox>
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" 
                            ErrorMessage="City" ControlToValidate="txtSelectCity" Display="Dynamic" 
                            ValidationGroup="g1" ForeColor="Red"></asp:RequiredFieldValidator>
                      </div>
                    </div>
                    <div class="col-sm-6">
                      <div class="input-box">
                        <asp:DropDownList ID="ddState" runat="server">
                         </asp:DropDownList>
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" 
                            ErrorMessage="State" ControlToValidate="ddState" Display="Dynamic" 
                            ValidationGroup="g1" ForeColor="Red" InitialValue="Select State"></asp:RequiredFieldValidator>
                      </div>
                    </div>
                    <div class="col-sm-6">
                      <div class="input-box">
                        <asp:DropDownList ID="ddCountry" runat="server">
                         </asp:DropDownList>
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" 
                            ErrorMessage="Country" ControlToValidate="ddCountry" Display="Dynamic" 
                            ValidationGroup="g1" ForeColor="Red" InitialValue="Select Country"></asp:RequiredFieldValidator>
                      </div>
                    </div>
                    <div class="col-sm-6">
                      <div class="input-box">
                        <asp:TextBox ID="txtPostcodezip" placeholder="Postcode/zip" runat="server"></asp:TextBox>
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" 
                            ErrorMessage="Postcode/zip" ControlToValidate="txtPostcodezip" Display="Dynamic" 
                            ValidationGroup="g1" ForeColor="Red"></asp:RequiredFieldValidator>
                      </div>
                    </div>

                    <div class="col-sm-12">
                      <asp:LinkButton ID="lnkSubmit" OnClick="lnkSubmit_Click" ValidationGroup="g1" runat="server" class="btn btn-black">Submit</asp:LinkButton>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </ContentTemplate>
          </asp:UpdatePanel>
          </div>
          <div id="data-step3" class="account-content" data-temp="tabdata" style="display:none">
            <div class="row">
              <div class="col-xs-12">
                <div class="heading-part heading-bg mb-30">
                  <h2 class="heading m-0">My Orders</h2>
                </div>
              </div>
            </div>
            <div class="row">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
              <div class="col-xs-12 mb-xs-30">
                <div class="cart-item-table commun-table">
                  <div class="table-responsive">
                    <table class="table">
                      <thead>
                        <tr>
                          <th colspan="4">
                            <ul>
                              <li><span>Order Placed</span> <asp:Label ID="lblOrderdate" runat="server" Text=""></asp:Label></li>
                              <li class="price-box"><span>Total</span> <asp:Label ID="lblTotalamount" class="price" runat="server" Text=""></asp:Label></li>
                              <li><span>Order No.</span> <asp:Label ID="lblOrderNo" runat="server" Text=""></asp:Label></li>
                            </ul>
                          </th>
                        </tr>
                      </thead>
                      <tbody>
                      <asp:Repeater ID="rpOrderlist" runat="server" 
                              onitemcommand="rpOrderlist_ItemCommand" 
                              onitemdatabound="rpOrderlist_ItemDataBound">
                      <ItemTemplate>
                        <tr>
                          <td>
                            
                              <div class="product-image"><img src='<%# Eval("Thumbnail","Upload/thumbnails/{0}") %>' alt=""/></div>
                           
                          </td>
                          <td>
                            <div class="product-title">
                              Product Name: <%# Eval("Name")%>
                            </div>
                            <div class="product-info-stock-sku m-0">
                              <div>
                                <label>Quantity: </label>
                                <span class="info-deta"><%# Eval("Quantity")%></span>
                              </div>
                            </div>
                            <div class="product-info-stock-sku m-0">
                              <div>
                                <label>Variant Name: </label>
                                <span class="info-deta"><%# Eval("VariantName")%></span>
                              </div>
                            </div>
                            
                          </td>
                          <td>
                            <div class="base-price price-box">
                              <span class="price">
                                <asp:Literal ID="lbltotal" runat="server" Text='<%# Bind("ItemSubTotal") %>'></asp:Literal>
                                <asp:Literal ID="lblusdtotal" runat="server" Text='<%# Bind("ItemSubTotal") %>'></asp:Literal>
                              </span>
                            </div>
                          </td>
                          <td>
                            <asp:LinkButton ID="LinkButton1" runat="server" 
                            CommandArgument='<%#DataBinder.Eval(Container.DataItem, "OrderId")%>' 
                            CommandName="Delete">
                            <i title="Cancel Order" data-id="100" class="fa fa-trash cart-remove-item"></i>
                            </asp:LinkButton>
                            <cc1:ConfirmButtonExtender ID="cbe" runat="server" DisplayModalPopupID="mpe2" TargetControlID="LinkButton1">
                        </cc1:ConfirmButtonExtender>
                        <cc1:ModalPopupExtender ID="mpe2" runat="server" PopupControlID="pnlPopup" TargetControlID="LinkButton1" OkControlID = "btnYes"
                            CancelControlID="btnNo" BackgroundCssClass="modalBackground">
                        </cc1:ModalPopupExtender>
                        <asp:Panel ID="pnlPopup" runat="server" CssClass="modalPopup" Style="display: none">
                            <div class="header">
                                Confirmation
                            </div>
                            <div class="body">
                                Do you want to cancel order of this product?
                            </div>
                            <div class="footer1" align="right">
                                <asp:Button ID="btnYes" runat="server" Text="Yes" />&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnNo" runat="server" Text="No" />
                            </div>
                        </asp:Panel>
                          </td>
                        </tr>
                      </ItemTemplate>
                      </asp:Repeater>
                      </tbody>
                    </table>
                  </div>
                </div>
                <br />

                <center><big><big><asp:LinkButton ID="lnkOldOrderList" CssClass="text-center" OnClick="lnkOldOrderList_Click"
                 runat="server">Old Order List</asp:LinkButton></big></big></center>
                <asp:Panel ID="Panel1" runat="server">
                <div class="cart-item-table commun-table">
                  <div class="table-responsive">
                    <table class="table">
                      <thead>
                        <tr>
                          <th colspan="4">
                            <ul>
                              <li><span>Order Item</span></li>
                              <li><span>Product Details</span></li>
                              <%--<li><span>Order Placed On</span></li>--%>
                              <li class="price-box"><span>Item Price</span></li>
                            </ul>
                          </th>
                        </tr>
                      </thead>
                      <tbody>
                      <asp:Repeater ID="rpOldOrderlist" runat="server"
                        onitemdatabound="rpOldOrderlist_ItemDataBound">
                      <ItemTemplate>
                        <tr>
                          <td>
                            <a href="#">
                              <div class="product-image"><img src='<%# Eval("Thumbnail","Upload/thumbnails/{0}") %>' alt=""/></div>
                            </a>
                          </td>
                          <td>
                            <div class="product-title">
                              Product Name: <%# Eval("Name")%>
                            </div>
                            <div class="product-info-stock-sku m-0">
                              <div>
                                <label>Quantity: </label>
                                <span class="info-deta"><%# Eval("Quantity")%></span>
                              </div>
                            </div>
                            <div class="product-info-stock-sku m-0">
                              <div>
                                <label>Variant Name: </label>
                                <span class="info-deta"><%# Eval("VariantName")%></span>
                              </div>
                            </div>
                             <div class="product-info-stock-sku m-0">
                              <div>
                                <label>Order Placed On: </label>
                                <span class="info-deta"><%# Eval("Order Placed")%></span>
                              </div>
                            </div>
                            
                          </td>
                          <%--<td>
                              <div class="product-title">
                              <%# Eval("Order Placed")%>
                            </div>
                          </td>--%>
                          <td>
                            <div class="base-price price-box">
                              <span class="price">
                                <asp:Literal ID="lbltotal" runat="server" Text='<%# Bind("ItemSubTotal") %>'></asp:Literal>
                                <asp:Literal ID="lblusdtotal" runat="server" Text='<%# Bind("ItemSubTotal") %>'></asp:Literal>
                              </span>
                            </div>
                          </td>
                        </tr>
                      </ItemTemplate>
                      </asp:Repeater>
                      </tbody>
                    </table>
                  </div>
                </div>
                </asp:Panel>
              </div>
            </ContentTemplate>
            </asp:UpdatePanel>
            </div>
          </div>
          <div id="data-step4" class="account-content" data-temp="tabdata" style="display:none">
            <div class="row">
              <div class="col-xs-12">
                <div class="heading-part heading-bg mb-30">
                  <h2 class="heading m-0">Change Password</h2>
                </div>
              </div>
            </div>
            <div class="main-form full">
              <div class="row">
                <div class="col-sm-6">
                  <div class="input-box">
                    <label for="login-pass">Email ID / Mobile No</label>
                    <asp:TextBox ID="txtUsername" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator21" ControlToValidate="txtUsername" runat="server" 
                        ErrorMessage="Mobile/Email ID" ValidationGroup="pass" ForeColor="Red" Display="Dynamic">
                    </asp:RequiredFieldValidator>
                  </div>
                </div>
                <div class="col-sm-6">
                  <div class="input-box">
                    <label for="old-pass">Old-Password</label>
                    <asp:TextBox ID="txtOldPassword" runat="server" TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator23" ControlToValidate="txtOldPassword" runat="server" 
                        ErrorMessage="Current Password" ValidationGroup="pass" ForeColor="Red" Display="Dynamic">
                    </asp:RequiredFieldValidator>
                  </div>
                </div>
                <div class="col-sm-6">
                  <div class="input-box">
                    <label for="login-pass">New Password</label>
                    <asp:TextBox ID="txtNewPassword" runat="server" TextMode="Password"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="Regex2" runat="server" ControlToValidate="txtNewPassword"
                        ValidationExpression="^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$"
                        ErrorMessage="Minimum 8 Character, 1 Alpha and Number" ForeColor="Red" ValidationGroup="g1" />
                  </div>
                </div>
                <div class="col-sm-6">
                  <div class="input-box">
                    <label for="re-enter-pass">Re-enter Password</label>
                    <asp:TextBox ID="txtConfirmpass" runat="server" TextMode="Password"></asp:TextBox>
                     <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="No Match" ValidationGroup="pass"
                        ControlToValidate="txtConfirmpass" ControlToCompare="txtNewPassword" ForeColor="Red"></asp:CompareValidator>
                  </div>
                </div>
                <div class="col-xs-12 text-right">
                  <%--<button class="btn-black" type="submit" name="submit">Change Password</button>--%>
                  <asp:Button ID="btnSave" runat="server" Text="Change Password" ValidationGroup="pass" class="btn-black" onclick="btnSave_Click"></asp:Button>
                </div>
              </div>
            </div>
          </div>

          <div id="data-step5" class="account-content" data-temp="tabdata" style="display:none">
          <div class="row">
              <div class="col-xs-12">
                <div class="heading-part heading-bg mb-30">
                  <h2 class="heading m-0">Fix Amount</h2>
                </div>
              </div>
            </div>
            <div class="m-0">
              <div class="main-form full">
                <div class="mb-20">
                  <div class="row">
                    <div class="col-xs-12 mb-20">
                      <div class="heading-part">
                        <h3 class="sub-heading">Fix Your Wallet Amount</h3>
                      </div>
                      <hr>
                    </div>
                    </div>
            <div class="col-sm-12">
                <p class="red">If you want to fix your amount in wallet for few days, so first you have to add amount in your wallet then you can fix you amount in wallet. 
                To fix amount in your wallet<br /> click to <a href="fixamount.aspx"><u>Fix Amount into Wallet </u>.</a></p>
            </div>
            </div>

            </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </section>
  <!-- CONTAINER END --> 

   <!-- FOOTER START -->
  <div class="footer">
                <div class="container">
                    <div class="footer-inner">
                        <div class="footer-top">
                            <div class="row">
                                <div class="col-md-5 f-col">
                                    <div class="footer-static-block">
                                        <span class="opener plus"></span>
                                        <div class="f-logo">
                                            <a href="Default.aspx" class="">
                                                <img src="images/footer-logo.png" class="flogos" alt="" />
                                            </a>
                                        </div>
                                        <ul class="footer-block-contant address-footer">
                                            <li class="item">
                                                <i class="fa fa-text">EMAIL :&nbsp;&nbsp;</i>
                                                <p>
                                                    <a>info@palshoes.com</a>
                                                </p>
                                            </li>
                                            <li class="item">
                                                <i class="fa fa-text">BRANCH OFFICE :&nbsp;&nbsp;</i>
                                                <p>
                                                    Pal Boot House, Main Market Sohna-122103 Gurgaon, Haryana
                                                </p>
                                            </li>
                                            <li class="item">
                                                <a href="https://api.whatsapp.com/send?phone=918168126969" target="blank"><img src="images/watsapp.png" alt=""/>: &nbsp;&nbsp;(+91) 8168126969 </a>
                                               
                                            </li>
                                        </ul>
                                    </div>
                                </div>
                                <div class="col-md-7">
                                    <div class="row">
                                        <div class="col-md-5 f-col">
                                            <div class="footer-static-block">
                                                <span class="opener plus"></span>
                                                <h3 class="title"><span></span>Quick Link</h3>
                                                <ul class="footer-block-contant link">
                                                    <li><a href="about.aspx"> Who We Are</a></li>
                                                    <li><a href="deliverypolicy.aspx"> Delivery Policy</a></li>
                                                    <li><a href="cancellation.aspx"> Cancellation / Refund Policy</a></li>
                                                    <li><a href="term-condition.aspx"> Term & Condition</a></li>
                                                    <li><a href="disclaimer.aspx"> Disclaimer</a></li>
                                                </ul>
                                            </div>
                                        </div>
                                        <div class="col-md-7 f-col">
                                            <div class="footer-static-block">
                                                <span class="opener plus"></span>
                                                <h3 class="title"><span></span>About Pal Shoes Shop</h3>
                                                <div class="footer-block-contant">
                                                    <p>
                                                        Pal Shoes shop has the Authorised Distributorship & PAN India Online Selling Rights of brand shoes and Accessories.
                                                    </p>
                                                    <p>
                                                        Assurance of 100% Original & Authentic products.
                                                    </p>
                                                                  
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <hr />
                        <div class="footer-middle center-sm">
                            <div class="row">
                                <div class="col-md-7">
                                    <div class="site-link mt-30 ">
                                        <ul>
                                            <li style="font-size: 13pt;">
                                                <a href="http://palshoes.com">Palshoes.com</a> provides its customers a Simple and Authorised Online Portal to Purchase Authentic Sports Products. 
                                            </li>
                                        </ul>
                                    </div>
                                </div>
                                <div class="col-md-5 border-l">
                                    <div class="newsletter mtb-30">
                                        <div class="newsletter-inner">
                                            <div class="newsletter-title">
                                                <h3 class="title"><span></span>Sign Up For Our Newsletter</h3>
                                            </div>
                                            <div class="newsletter-box">
                                                <asp:Label ID="lblMessageSubscribe" runat="server"></asp:Label>
                                                <asp:TextBox ID="txtemail" runat="server" Text="" placeholder="Email Here..."></asp:TextBox>
                                                    <center>
                                                    <asp:Button ID="btnSubscribe" runat="server" Text="Subscribe Now" OnClick="btnSubscribe_Click" Style="width: 100px; background-color: #ffee00; color: #000; margin-top: 15px" /></center>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <hr />
                        <div class="footer-bottom mtb-30">
                            <div class="row">
                                <div class="col-sm-4">
                                    <div class="footer_social pt-xs-15 center-xs mt-xs-15">
                                        <ul class="social-icon">
                                            <li><a title="Facebook" class="facebook"><i class="fa fa-facebook"></i></a></li>
                                            <li><a title="Twitter" class="twitter"><i class="fa fa-twitter"></i></a></li>
                                            <li><a title="Youtube" class="youtube"><i class="fa fa-youtube"></i></a></li>
                                        </ul>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="copy-right text-center center-xs">© 2019 Pal Shoes, All Rights Reserved.</div>
                                    <div class="text-center" style="font-size: 10px; color: #8A8A8A">Design & Developed by <a href="http://rssindia.com" target="_blank">RSS Infotech Pvt. Ltd.</a></div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="payment right-side float-none-xs center-xs">
                                        <ul class="payment_icon">
                                            <li class="discover"><a></a></li>
                                            <li class="visa"><a></a></li>
                                            <li class="mastro"><a></a></li>
                                            <li class="paypal"><a></a></li>
                                        </ul>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
  <div class="scroll-top">
    <div id="scrollup"></div>
  </div>
  <!-- FOOTER END -->
 </div>
</form>
<script src="js/jquery-1.12.3.min.js" type="text/javascript"></script>
<script src="js/bootstrap.min.js" type="text/javascript"></script>
<script src="js/jquery-ui.js" type="text/javascript"></script>
<script src="js/fotorama.js" type="text/javascript"></script>
<script src="js/jquery.magnific-popup.js" type="text/javascript"></script>
<script src="js/owl.carousel.min.js" type="text/javascript"></script>
<script src="js/custom.js" type="text/javascript"></script>

    <!-- all js here -->
        <script type="text/javascript">
    var blink_speed = 500;
    var t = setInterval(function () {
    var ele = document.getElementById('blinker');
    ele.style.visibility = (ele.style.visibility == 'hidden' ? '' : 'hidden');
    }, blink_speed);
        </script>
</body>
</html>
