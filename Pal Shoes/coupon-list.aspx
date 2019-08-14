<%@ Page Language="C#" AutoEventWireup="true" CodeFile="coupon-list.aspx.cs" Inherits="coupon_list" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Pal Shoes</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="description" content="" />
    <meta name="keywords" content="" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1" />

    <link rel="stylesheet" type="text/css" href="css/font-awesome.min.css" />
    <link rel="stylesheet" type="text/css" href="css/bootstrap.css" />
    <link rel="stylesheet" type="text/css" href="css/jquery-ui.css" />
    <link rel="stylesheet" type="text/css" href="css/owl.carousel.css" />
    <link rel="stylesheet" type="text/css" href="css/fotorama.css" />
    <link rel="stylesheet" type="text/css" href="css/magnific-popup.css" />
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

    <style>
        /*Style form coupon list*/
        #coupon
{
    width: 106px;
	height: 88px;
	background: #23ad2a;
	-moz-border-radius: 50px;
	-webkit-border-radius: 50px;
	border-radius: 50px;
    /*float:left;*/
    margin:5px;
}
.count
{
  line-height: 85px;
  color:white;
  margin-left:4px;
  font-size:25px;
}
#talkbubble {
   width: 120px;
   height: 80px;
   background: red;
   position: relative;
   -moz-border-radius:    10px;
   -webkit-border-radius: 10px;
   border-radius:         10px;
  float:left;
  margin:20px;
}
#talkbubble:before {
   content:"";
   position: absolute;
   right: 100%;
   top: 26px;
   width: 0;
   height: 0;
   border-top: 13px solid transparent;
   border-right: 26px solid red;
   border-bottom: 13px solid transparent;
}

.linker
{
  font-size : 20px;
  font-color: black;
}
		
    @media only screen and (min-width: 0px) and (max-device-width:520px) {
            .col-sm-3 {  padding-left: 220px; margin-top: -440px; }
		     #coupon { margin-top: 5px;}
		    h2.heading { font-size: 20px }

   </style>

</head>
<body>
    <form id="form1" runat="server">
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
                                            <big><big><i class="fa fa-bars"></i></big></big>
                                        </button>
                                    </div>
                                </div>
                                <div class="col-md-9">
                                    <div class="top-link right-side">
                                        <ul>
                                            <li><a style="padding-right: 100px; border-right: none"></a></li>

                                            <li class="account-icon"><a href="account.aspx" title="My Account"><span></span>My Account</a></li>
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
                                                <asp:LinkButton ID="lnkLogin" runat="server" OnClick="lnkLogin_Click"></asp:LinkButton></a>
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
                                                    <span></span>
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
                                            <li class="shipping-icon visible-lg visible-md visible-sm visible-xs"></li>
                                        </ul>
                                    </div>

                                    <div id="menu" class="navbar-collapse collapse left-side">

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
                    <div class="page-title">BUY MORE, SAVE MORE</div>
                    <div class="bread-crumb-inner right-side float-none-xs">
                        <ul>
                            <li><a href="Default.aspx">Home</a><i class="fa fa-angle-right"></i></li>
                            <%--<li><span>WHO WE ARE</span></li>--%>
                        </ul>
                    </div>
                </div>
            </div>
            <!-- Bread Crumb END -->


            <!-- CONTAIN START pb-95-->
            <section class="pb-95">
                <div class="container">
                    <div class="about-detail mt-30">
                        <div class="row">
                            <div class="col-md-4 col-sm-4 mb-xs-30">
                                <div class="image-part">
                                    <img alt="" src="images/coupon.jpg" /> <br /><br />
                                    <h2 class="heading" style="font-size: 14px"> Enter The Relevant Coupon Code On The Checkout Section Of Your Cart. </h2>
                                </div>
                            </div>
                            <div class="col-md-8 col-sm-8 mb-xs-30">
                            <div class="col-md-5 col-sm-5">
                                <h2 class="heading">Coupon Codes</h2>
                                <asp:Repeater ID="rpCouponList" runat="server">
                                    <ItemTemplate>
                                        <p>Coupon Code: <b><%#Eval("couponcode") %> </b></p>
                                        <p>Discount: <b><%#Eval("discount") %>%</b></p>
                                        <p>Min Purchase: <b><%#Eval("price") %> Rs</b></p>
                                        <hr />
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                            <div class="col-md-3 col-sm-3">
                                <h2 class="heading"> Save Up !</h2>
                                <div id="coupon"><span class="count">20 </span> <span style="color:#fff">% Discount</span></div>
                                <hr />
                               
                                <div id="coupon"><span class="count">15 </span> <span style="color:#fff">% Discount</span></div>
                                <hr />

                                <div id="coupon"><span class="count">10 </span> <span style="color:#fff">% Discount</span></div>
                                <hr />

                                <div id="coupon"><span class="count">5 </span> <span style="color:#fff">% Discount</span></div>
                                <hr />
                           
                            </div>
                            </div>
                        </div>
                    </div>
                </div>
            </section>

            <!-- Brand logo block Start  -->
            <section class="ptb-95">
                <div class="container">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="brand">
                                <div id="brand-logo" class="owl-carousel align_center">
                                    <asp:Repeater ID="rpBrand" runat="server">
                                        <ItemTemplate>
                                            <div class="item"><a href="product.aspx?category=<%# Eval("Categoryid") %>">
                                                <img src='<%# Eval("BrandImage","Upload/brand/{0}") %>' alt="#"></a></div>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </section>
            <!-- Brand logo block End  -->


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
    

    $('.count').each(function () {
    $(this).prop('Counter',0).animate({
        Counter: $(this).text()
    }, {
        duration: 4000,
        easing: 'swing',
        step: function (now) {
            $(this).text(Math.ceil(now));
        }
    });
    });
        </script>
</body>
</html>
