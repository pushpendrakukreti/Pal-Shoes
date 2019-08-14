<%@ Page Language="C#" AutoEventWireup="true" CodeFile="wishlist.aspx.cs" Inherits="wishlist" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Pal Shoes</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="keywords" content="" />
    <meta name="distribution" content="global" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1" />

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

        .modalBackground {
            background-color: Black;
            filter: alpha(opacity=40);
            opacity: 0.4;
        }

        .modalPopup {
            background-color: #FFFFFF;
            width: 300px;
            border: 3px solid #0DA9D0;
        }

            .modalPopup .header {
                background-color: #2FBDF1;
                height: 30px;
                color: White;
                line-height: 30px;
                text-align: center;
                font-weight: bold;
                padding: 2px;
            }

            .modalPopup .body {
                min-height: 50px;
                line-height: 30px;
                text-align: center;
                font-weight: bold;
                padding: 20px 0 10px 0;
            }

            .modalPopup .footer {
                padding: 1px;
            }

            .modalPopup .yes, .modalPopup .no {
                height: 23px;
                color: White;
                line-height: 23px;
                text-align: center;
                font-weight: bold;
                cursor: pointer;
            }

            .modalPopup .yes {
                background-color: #2FBDF1;
                border: 1px solid #0DA9D0;
            }

            .modalPopup .no {
                background-color: #9F9F9F;
                border: 1px solid #5C5C5C;
            }

        .footer1 {
            margin: 0;
            padding: 5px;
            text-align: center;
            color: Black;
            background-color: #FFFFFF;
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

            <!-- Bread Crumb START -->
            <div class="container">
                <div class="bread-crumb mtb-30 center-xs">
                    <div class="page-title">My Wishlist</div>
                    <div class="bread-crumb-inner right-side float-none-xs">
                        <ul>
                            <li><a href="default.aspx">Home</a><i class="fa fa-angle-right"></i></li>
                            <li><span>Wishlist</span></li>
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
                <div class="header">
                </div>
                <div class="body">
                    <p class="text-center">
                        <asp:Label ID="lblpopup" runat="server" Text=""></asp:Label></p>
                </div>
                <div class="footer1" align="right">
                    <asp:Button ID="btnNo" runat="server" Text="OK" />
                </div>
            </asp:Panel>

            <!-- CONTAIN START -->
            <section class="pb-95">
    <div class="container">
      <div class="row">
        <div class="col-xs-12 mb-xs-30">
        <center>
        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
        <label>Recomended Product Name : </label>
        <asp:TextBox ID="txtRecomended" runat="server"></asp:TextBox>
        <asp:Button ID="btnRecomended" runat="server" Text="Send Request" 
                onclick="btnRecomended_Click"></asp:Button>
        </center>
        <br />
          <div class="cart-item-table commun-table">
            <div class="table-responsive">
              <table class="table">
                <thead>
                  <tr>
                    <th>Product</th>
                    <th>Product Name</th>
                    <th>Price</th>
                    <th>Sub Total</th>
                    <th>Action</th>
                  </tr>
                </thead>
                <tbody>
                <asp:Repeater ID="ddShoppingCart" runat="server" 
                        onitemcommand="ddShoppingCart_ItemCommand" 
                        onitemdatabound="ddShoppingCart_ItemDataBound">
                <ItemTemplate>
                  <tr>
                    <td>
                      <a href="">
                        <div class="product-image"><img src='<%# Eval("Thumbnail","Upload/thumbnails/{0}") %>' alt=""/></div>
                      </a>
                    </td>
                    <td>
                      <div class="product-title">
                        <a href=""><%# Eval("Name")%></a>
                      </div>
                    </td>
                    <td>
                      <div class="input-box">
                        <select data-id="100" class="quantity_cart" name="quantity_cart">
                          <option selected="" value="1">1</option>
                        </select>
                      </div>
                    </td>
                    <td>
                      <ul>
                        <li>
                          <div class="base-price price-box">
                            <span class="price">
                            <asp:Literal ID="lblprice" runat="server" Text='<%# Bind("iprice") %>'></asp:Literal>
                            <asp:Label ID="lblprice1" runat="server" Text='<%# Bind("iprice") %>' style="display:none"></asp:Label>
                            <asp:Literal ID="lblusdprice" runat="server" Text='<%# Bind("usdprice") %>'></asp:Literal>
                            </span>
                          </div>
                        </li>
                      </ul>
                    </td>
                    <td>
                      	<asp:LinkButton ID="LinkButton1" runat="server" 
                            CommandArgument='<%#DataBinder.Eval(Container.DataItem, "WishId")%>' 
                            CommandName="Delete">
                            <i title="Remove Item From Cart" data-id="100" class="fa fa-trash cart-remove-item"></i>
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
                                Do you want delete this Item?
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
        </div>
      </div>
      <div class="mb-30">
        <div class="row">
          <div class="col-sm-6">
            <div class="mt-30">
              <a href="product.aspx" class="btn btn-black"><span><i class="fa fa-angle-left"></i></span>Continue Shopping</a>
            </div>
          </div>
          <div class="col-sm-6">
            <div class="mt-30 right-side float-none-xs">
              <%--<a class="btn btn-black">Update Cart</a>--%>
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
