﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="checkout.aspx.cs" Inherits="checkout" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="keywords" content="" />
    <meta name="distribution" content="global" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1" />
    <title>Pal Shoes</title>
    <link href="https://fonts.googleapis.com/css?family=Open+Sans:400,400i,600,700,800" rel="stylesheet" />
    <link rel="stylesheet" type="text/css" href="css/font-awesome.min.css" />
    <link rel="stylesheet" type="text/css" href="css/bootstrap.css" />
    <link rel="stylesheet" type="text/css" href="css/jquery-ui.css" />
    <link rel="stylesheet" type="text/css" href="css/owl.carousel.css" />
    <link rel="stylesheet" type="text/css" href="css/fotorama.css" />
    <link rel="stylesheet" type="text/css" href="css/custom.css" />
    <link rel="stylesheet" type="text/css" href="css/responsive.css" />

    <link rel="shortcut icon" href="images/favicon.png" />
    <%--<link rel="apple-touch-icon" href="images/apple-touch-icon.html"/>
<link rel="apple-touch-icon" sizes="72x72" href="images/apple-touch-icon-72x72.html"/>
<link rel="apple-touch-icon" sizes="114x114" href="images/apple-touch-icon-114x114.html"/>--%>
    <style type="text/css">
        .logo1 {
            padding-top: 2px;
        }

        #rdbilling {
            background: #fff;
            padding: 0px;
            width: 15px;
            border: 1px solid #eaeaea;
            min-height: 15px;
        }

        #rdshipping {
            background: #fff;
            padding: 0px;
            width: 15px;
            border: 1px solid #eaeaea;
            min-height: 15px;
        }
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
            <!-- HEADER START -->
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
                    <div class="page-title">Checkout</div>
                    <div class="bread-crumb-inner right-side float-none-xs">
                        <ul>
                            <li><a href="Default.aspx">Home</a><i class="fa fa-angle-right"></i></li>
                            <li><a href="cart.aspx">Cart</a><i class="fa fa-angle-right"></i></li>
                            <li><span>Checkout</span></li>
                        </ul>
                    </div>
                </div>
            </div>
            <!-- Bread Crumb END -->

            <!-- CONTAIN START -->
            <section class="checkout-section pb-95">
    <div class="container">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <Triggers>
    <asp:PostBackTrigger ControlID="lnkNext" />
    </Triggers>
    <ContentTemplate>
       <div class="row">
        <div class="col-xs-12">
          <div class="checkout-step mb-40">
            <ul>
              <li class="active"> 
                <a href="checkout.aspx">
                  <div class="step">
                    <div class="line"></div>
                    <div class="circle">1</div>
                  </div>
                  <span>Shipping</span>
                </a>
              </li>
              <li> 
                <a href="order-overview.aspx">
                  <div class="step">
                    <div class="line"></div>
                    <div class="circle">2</div>
                  </div>
                  <span>Order Overview</span>
                </a>
              </li>
              <li>
                <a href="">
                  <div class="step">
                    <div class="line"></div>
                    <div class="circle">3</div>
                  </div>
                  <span>Payment</span>
                </a>
              </li>
              <li>
                <a href="order-complete.aspx">
                  <div class="step">
                    <div class="line"></div>
                    <div class="circle">4</div>
                  </div>
                  <span>Order Complete</span>
                </a>
              </li>
            </ul>
            <hr />
          </div>
          <div class="checkout-content" >
            <div class="row">
              <div class="col-xs-12">
                <div class="heading-part align-center">
                    <h2 class="heading">Please fill up your Shipping details</h2>
                    <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                </div>
              </div>
            </div>
            <div class="row">
              <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                <div class="main-form full">
                  <div class="mb-20">
                    <div class="row">

                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 list-group-item">
                      <div class="col-xs-12 mb-20">
                        <div class="heading-part">
                          <h3 class="sub-heading">Billing Address</h3>
                        </div>
                        <hr />
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
                            ErrorMessage="Billing Email Address" ControlToValidate="txtEmailAddressBill" Display="Dynamic" 
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
                          <asp:TextBox ID="txtPostcodezipBill" placeholder="Postcode/zip" runat="server"></asp:TextBox>
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" 
                            ErrorMessage="Postcode" ControlToValidate="txtPostcodezipBill" Display="Dynamic" 
                            ValidationGroup="g1" ForeColor="Red"></asp:RequiredFieldValidator>
                        </div>
                      </div>
                      <div class="col-sm-6">
                        <div class="input-box">
                         <asp:DropDownList ID="ddCountryBill" runat="server" style="display:none">
                         </asp:DropDownList>
                         <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                            ErrorMessage="Country" ControlToValidate="ddCountryBill" Display="Dynamic" 
                            ValidationGroup="g1" ForeColor="Red" InitialValue="Select Country"></asp:RequiredFieldValidator>--%>
                        </div>
                      </div>

                    </div>

                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                      <div class="col-sm-12 col-md-12">
                        <div class="check-box">
                        <asp:RadioButton ID="rdbilling" runat="server" Checked="true" GroupName="g3" 
                                oncheckedchanged="rdbilling_CheckedChanged" AutoPostBack="true"></asp:RadioButton>
                        <label for="chk-billing-address"><big>Use my billing address as my shipping address.</big></label>
                        </div>
                        <div class="check-box mt-10">
                          <asp:RadioButton ID="rdshipping" runat="server" GroupName="g3"
                                oncheckedchanged="rdshipping_CheckedChanged" AutoPostBack="true"></asp:RadioButton>
                          <label for="chk-billing-address"><big>Delivery to a different address?</big></label>
                        </div>
                      </div>

                    <div class="col-sm-12 col-md-12 list-group-item mt-10">
                    <asp:Panel ID="pnlShipping" Visible="false" runat="server">
                      <div class="col-xs-12">
                        <div class="heading-part">
                          <h4 class="sub-heading">Shipping Address</h4>
                        </div>
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
                            ValidationGroup="g1" ForeColor="Red" InitialValue=""></asp:RequiredFieldValidator>
                        </div>
                      </div>
                      <div class="col-sm-6">
                        <div class="input-box">
                          <asp:TextBox ID="txtPostcodezip" placeholder="Postcode/zip" runat="server"></asp:TextBox>
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" 
                            ErrorMessage="Postcode" ControlToValidate="txtPostcodezip" Display="Dynamic" 
                            ValidationGroup="g1" ForeColor="Red"></asp:RequiredFieldValidator>
                        </div>
                      </div>
                    <div class="col-sm-6">
                        <div class="input-box">
                         <asp:DropDownList ID="ddCountry" runat="server" style="display:none">
                         </asp:DropDownList>
                         <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" 
                            ErrorMessage="Country" ControlToValidate="ddCountry" Display="Dynamic" 
                            ValidationGroup="g1" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>--%>
                        </div>
                      </div>
                      </asp:Panel>
                      <asp:Panel ID="pnlInfomation" Visible="true" runat="server">
                      <br />
                      <br />
                      <hr />
                      <br />
                      <div class="brand">
                        <h3 class="text-center">Related Products</h3>
                        <div id="brand-logo" class="owl-carousel align_center">
                         <asp:Repeater ID="rptImages" runat="server">
                            <ItemTemplate>
                            <div class="item">
                            <a href='product-detail.aspx?product=<%# Eval("ProductId") %>'>
                                <img src='<%# Eval("Thumbnail","Upload/thumbnails/{0}") %>' alt="#">
                            </a>
                            &nbsp;&nbsp;&nbsp;</div>
                            </ItemTemplate>
                          </asp:Repeater>
                        </div>
                      </div>
                      <p><br /></p>
                      </asp:Panel>
                      </div>

                    </div>
                  </div>
                  <div class="row">
                    <div class="col-sm-6">
                    <br />
                    <asp:LinkButton ID="lnkNext" ValidationGroup="g1" runat="server" class="btn btn-black right-side" 
                            onclick="lnkNext_Click">Next</asp:LinkButton>
                     </div>
                  </div>

                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </ContentTemplate>
    </asp:UpdatePanel>
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
                <div id="txtEmail Here..."></div>
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
