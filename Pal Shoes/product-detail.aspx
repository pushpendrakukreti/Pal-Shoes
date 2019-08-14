<%@ Page Language="C#" AutoEventWireup="true" CodeFile="product-detail.aspx.cs" Inherits="product_detail" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <%--<title>Pal Shoes</title>--%>
    <%--<meta id="url" runat="server" />
<meta id="key" runat="server" />
<meta id="des" runat="server" />--%>

    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1" />
    <meta name="keywords" content="" />
    <meta name="description" content="" />

    <link href="https://fonts.googleapis.com/css?family=Open+Sans:400,400i,600,700,800" rel="stylesheet" />
    <link rel="stylesheet" type="text/css" href="css/font-awesome.min.css" />
    <link rel="stylesheet" type="text/css" href="css/bootstrap.css" />
    <link rel="stylesheet" type="text/css" href="css/jquery-ui.css" />
    <link rel="stylesheet" type="text/css" href="css/owl.carousel.css" />
    <link rel="stylesheet" type="text/css" href="css/fotorama.css" />
    <link rel="stylesheet" type="text/css" href="css/custom.css" />
    <link rel="stylesheet" type="text/css" href="css/responsive.css" />
    <script type='text/javascript' src='//platform-api.sharethis.com/js/sharethis.js#property=5a30b7e3972cda0013356f17&product=sop' async='async'></script>
    <link href="autocomplete/jquery-ui.css" rel="stylesheet" type="text/css" />
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

    <link rel="shortcut icon" href="images/favicon.png" />
    <script src='jquery-1.8.3.min.js'></script>
    <script src='jquery.elevatezoom.js'></script>
    <style type="text/css">
        /*set a border on the images to prevent shifting*/
        #gallery_01 img {
            border: 2px solid #cecdcd;
            margin-top: 5px;
        }
        /*Change the colour*/
        .active img {
            border: 2px solid #333 !important;
        }

        .style_prevu_kit {
            background-color: #FFF;
            display: inline-block;
            width: 100%;
            height: auto;
            position: relative;
            -webkit-transition: all 400ms ease-in;
            -webkit-transform: scale(0.95);
            -ms-transition: all 400ms ease-in;
            -ms-transform: scale(0.95);
            -moz-transition: all 400ms ease-in;
            -moz-transform: scale(0.95);
            transition: all 400ms ease-in;
            transform: scale(0.95);
            border: 2px solid #00AFEF;
            min-height: 400px;
        }

            .style_prevu_kit:hover {
                background-color: #FFF;
                -webkit-box-shadow: 0px 3px 20px 3px rgba(0,0,0,0.75);
                -moz-box-shadow: 0px 3px 20px 3px rgba(0,0,0,0.75);
                box-shadow: 0px 3px 20px 3px rgba(0,0,0,0.75);
                z-index: 555555;
                -webkit-transition: all 400ms ease-in;
                -webkit-transform: scale(1);
                -ms-transition: all 400ms ease-in;
                -ms-transform: scale(1);
                -moz-transition: all 400ms ease-in;
                -moz-transform: scale(1);
                transition: all 400ms ease-in;
                transform: scale(1);
                border: 2px solid #00AFEF;
            }

        .vtextright {
            float: right;
        }

        .product-image a img {
            width: 100%;
            height: auto;
        }

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

    <style type="text/css">
        @media only screen and (min-width: 0px) and (max-device-width: 520px) {
            .product-detail-main {
                padding-top: 86px;
            }

            .table1 {
                overflow-y: scroll;
                width: 100%
            }
        }
    </style>

    <script type="text/javascript">
        if (screen.width <= 699) {
        } else {


            $(function () {
                var mobile;
                if (screen.width <= 699) {

                    //initiate the plugin and pass the id of the div containing gallery images
                    $("#zoom_03").elevateZoom({ gallery: 'gallery_01', cursor: 'pointer', galleryActiveClass: 'active', imageCrossfade: true });

                    //pass the images to Fancybox
                    $("#zoom_03").bind("click", function (e) {
                        var ez = $('#zoom_03').data('elevateZoom');
                        $.fancybox(ez.getGalleryList());
                        return false;
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
                  <li style="padding-top: 40px;">
                      <%--<span id="blinker"><a href="coupon-list.aspx" style="text-align: left;font-size: 28px;font-weight: 600;padding-right: 100px;border-right: none"> SPECIAL DISCOUNT! </a></span>--%>
                  </li>
                  <li class="account-icon"><a href="account.aspx" title="My Account"><span></span> My Account</a></li>
                  <li><a href="register.aspx">
                  <asp:LinkButton ID="lnkLogin" runat="server" onclick="lnkLogin_Click"></asp:LinkButton></a>
                  </li>
                </ul>
               </div>
			  

			   <div class="header-right-part right-side float-none-xs">
                <ul>
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
                    <div class="page-title">Product Detail</div>
                    <div class="bread-crumb-inner right-side float-none-xs">
                        <ul>
                            <li><a href="Default.aspx">Home</a><i class="fa fa-angle-right"></i></li>
                            <li><span>Product Detail</span></li>
                        </ul>
                    </div>
                </div>
            </div>
            <!-- Bread Crumb END -->

            <!-- CONTAIN START -->
            <section>
    <div class="container">
      <div class="row">
        <div class="col-md-5 col-sm-5 mb-xs-30">
            <div class="table1">
        <div style="height:400px; width:400px;" class="zoomWrapper">
          <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
            </div>
        <script type="text/javascript">
            if (screen.width <= 699) { } else {
                //initiate the plugin and pass the id of the div containing gallery images
                $("#zoom_03").elevateZoom({ gallery: 'gallery_01', cursor: 'pointer', galleryActiveClass: 'active', imageCrossfade: true });

                //pass the images to Fancybox
                $("#zoom_03").bind("click", function (e) {
                    var ez = $('#zoom_03').data('elevateZoom');
                    $.fancybox(ez.getGalleryList());
                    return false;
                });
            }
        </script>
        </div>

        </div>
        <div class="col-md-7 col-sm-7">
          <div class="row">
          <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
          <Triggers>
          <asp:PostBackTrigger ControlID="btnAddToCart" />
          <asp:AsyncPostBackTrigger ControlID="ddFlavour" />
          <asp:AsyncPostBackTrigger ControlID="lnkWishlist" />
          </Triggers>
          <ContentTemplate>--%>
            <div class="col-xs-12">
              <div class="product-detail-main">
                <div class="product-item-details">
                  <h1 class="product-item-name">Product Name: <asp:Label ID="lblProductName" runat="server" Text=""></asp:Label></h1>
                  <%--<div class="rating-summary-block">
                    <div title="53%" class="rating-result">
                      <span style="width:53%"></span>
                    </div>
                  </div>--%>
                  <div class="product-info-stock-sku">
                   <big>Brand : <asp:Label ID="lblBrand" runat="server" Text=""></asp:Label></big>
                  </div>
                  <div class="price-box">
                    Special Price : <i class="fa fa-inr" aria-hidden="true"></i> <span class="price"><asp:Label ID="lblSpecialPrice" runat="server" Text=""></asp:Label></span><br />
                    <asp:Panel ID="panelprice" runat="server">
                    Regular Price : <i class="fa fa-inr" aria-hidden="true"></i>  <del class="price old-price"><asp:Label ID="lblRegularPrice" runat="server" Text=""></asp:Label></del>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<label>Discount : </label>
                      <span class="info-deta"><asp:Label ID="lblDiscount" runat="server" Text=""></asp:Label></span>
                    </asp:Panel>
                  </div>
                  <div class="product-info-stock-sku">
                    <div>
                      <label>SKU : </label>
                      <span class="info-deta"><asp:Label ID="lblSKUCode" runat="server" Text=""></asp:Label></span>
                    </div>
                  </div>
                  <div class="product-info-stock-sku">
                    <div>
                      <label>Product Size : &nbsp;&nbsp;&nbsp;</label>
                      <span class="info-deta">
                          <asp:DropDownList ID="ddVariantNames" runat="server" AutoPostBack="true" 
            onselectedindexchanged="ddVariantNames_SelectedIndexChanged" Width="210px">
                         </asp:DropDownList>
                          
                      </span>
                    </div>
                 </div>
                  <div class="product-info-stock-sku">
                    <div>
                      <label>Available Quantity : </label>
                      <span class="info-deta">
                          <asp:Label ID="lblVariantQuantity" runat="server" Text=""></asp:Label>
                      </span>
                    </div>
                 </div>
                  <%--<div class="product-info-stock-sku">
                    <div>
                      <label>Available Quantity : </label>
                      <span class="info-deta"><asp:Label ID="lblQuantity" runat="server" Text=""></asp:Label></span>
                    </div>
                  </div>--%>
                  
                  <div class="product-info-stock-sku">
                  <div>
                    <label>Availability : </label>
                    <span class="info-deta"><asp:Label ID="lblInStock" runat="server" Text=""></asp:Label></span>
                  </div>
                  </div>
                  <div class="mb-40">
                    <div class="product-qty">
                      <label for="qty">Qty :</label>
                      <div class="custom-qty">
                        <button onclick="var result = document.getElementById('qty'); var qty = result.value; if( !isNaN( qty ) &amp;&amp; qty &gt; 1 ) result.value--;return false;" class="reduced items" type="button"> <i class="fa fa-minus"></i> </button>
                        <input type="text" runat="server" class="input-text qty" title="Qty" value="1" maxlength="8" id="qty" name="qty" />
                        <button onclick="var result = document.getElementById('qty'); var qty = result.value; if( !isNaN( qty )) result.value++;return false;" class="increase items" type="button"> <i class="fa fa-plus"></i> </button>
                      </div>
                    </div>
                    <div class="bottom-detail cart-button">
                      <ul>
                        <li class="pro-cart-icon">
                          <div>
                            <%--<button title="Add to Cart" class="btn-black"><span></span>Add to Cart</button>--%>
                            <big><big><big><big><i class="fa fa-cart-plus" aria-hidden="true"></i></big></big></big></big>
                            <asp:Button ID="btnAddToCart" runat="server" Text="Add to Cart" class="btn-black" onclick="btnAddToCart_Click"></asp:Button>
                          </div>
                        </li>
                      </ul>
                    </div>
                  </div>
                  <div class="bottom-detail">
                    <ul>
                      <li class="pro-wishlist-icon">
                      <asp:LinkButton ID="lnkWishlist" OnClick="lnkWishlist_Click" runat="server"><span></span>Wishlist</asp:LinkButton>
                      </li>
                      <li class="pro-compare-icon">
                      <asp:LinkButton ID="lnkCompare" OnClick="lnkCompare_Click" runat="server"><span></span>Compare</asp:LinkButton>
                      </li>
                      <li class="pro-email-icon">
                       <a href="" id="mailto" runat="server"><span></span>Email to Friends</a>
                      </li>
                    </ul>
                  </div>
                  <div class="share-link">
                    <div class="social-link">
                      <div class="sharethis-inline-share-buttons"></div>
                    </div>
                  </div>
                </div>

                <asp:HiddenField ID="HiddenField1" runat="server"></asp:HiddenField>
                <cc1:ModalPopupExtender ID="mpe" runat="server" PopupControlID="pnlPopup" TargetControlID="HiddenField1" 
                    CancelControlID="btnNo" BackgroundCssClass="modalBackground">
                </cc1:ModalPopupExtender>
                <asp:Panel ID="pnlPopup" runat="server" CssClass="modalPopup" Style="display: none">
                    <div class="header">
                        Product
                    </div>
                    <div class="body">
                       <p class="text-center"><asp:Label ID="lblpopup" runat="server" Text=""></asp:Label></p>
                    </div>
                    <div class="footer1" align="right">
                        <asp:Button ID="btnNo" runat="server" Text="OK" />
                    </div>
                </asp:Panel>

              </div>
            </div>
           <%--</ContentTemplate>
          </asp:UpdatePanel>--%>
          </div>
        </div>
      </div>
     </div>
  </section>

            <section class="mtb-60">
    <div class="container">
      <div class="product-detail-tab flipInX">
        <div class="row">
          <div class="col-md-12">
            <div id="tabs">
              <ul class="nav nav-tabs">
                <li><a class="tab-Description selected" title="Description">Description</a></li>
                <li><a class="tab-Reviews" title="Reviews">Reviews</a></li>
              </ul>
            </div>
            <div id="items">
              <div class="tab_content">
                <ul>
                  <li>
                    <div class="items-Description selected">
                      <div class="Description"> <strong><asp:Label ID="lblDescriptionHead" runat="server" Text=""></asp:Label></strong><br />
                        <p><asp:Label ID="lblDescriptionDetail" runat="server" Text=""></asp:Label></p>
                      </div>
                    </div>
                  </li>
                  <li>
                    <div class="items-Reviews">
                      <div class="comments-area">
                        <h4>Comments<span>(<asp:Label ID="lblcomments" runat="server" Text="0"></asp:Label>)</span></h4>
                      </div>
                      <ul class="comment-list mt-30">
                      <asp:PlaceHolder ID="PlaceHolder2" runat="server"></asp:PlaceHolder>
                      </ul>
                      <div class="main-form mt-30">
                         <h4>Leave a comments</h4>
                        <div class="row mt-30">
                         <div >
                            <div class="col-sm-4 mb-20">
                              <asp:TextBox ID="txtName" placeholder="Name" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-sm-4 mb-20">
                              <asp:TextBox ID="txtEmail" placeholder="Email" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-xs-12 col-sm-8 mb-20">
                              <asp:TextBox ID="txtMessage" placeholder="Review/Comments" Rows="3" cols="30" runat="server" TextMode="MultiLine"></asp:TextBox>
                            </div>
                            <div class="col-xs-12">
                              <asp:Button ID="btnReview" runat="server" OnClick="btnReview_Click" class="btn-black" Text="Submit"></asp:Button>
                            </div>
                          </div>
                        </div>
                      </div>
                    </div>
                  </li>
                </ul>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </section>
            <section class="pb-95">
    <div class="container">
      <div class="product-slider owl-slider">
        <div class="row">
          <div class="col-xs-12">
            <div class="heading-part align-center mb-40">
              <h2 class="main_title">Related Products</h2>
            </div>
          </div>
        </div>
        <div class="row">
          <div class="product-slider-main position-r">
            <div class="owl-carousel pro_cat_slider">

            <asp:Repeater ID="rpRelatedproducts" runat="server">
            <ItemTemplate>
              <div class="item">
                <div class="style_prevu_kit product-item">
                  <div class="product-image">
                    <a href='product-detail.aspx?product=<%# Eval("ProductId") %>'>
                      <img src='Upload/thumbnails/<%# Eval("Thumbnail") %>' alt="" />
                    </a>
                  </div>
                  <div class="product-item-details">
                    <!--<div class="rating-summary-block">
                      <div title="53%" class="rating-result">
                        <span style="width:53%"></span>
                      </div>
                    </div>-->
                    <div class="product-item-name">
                      <a href='product-detail.aspx?product=<%# Eval("ProductId") %>'><%# Eval("Name")%></a>
                    </div>
                    <div class="price-box">
                    Special Price: <i class="fa fa-inr" aria-hidden="true"></i> <span class="price"><%# Eval("discoutprice")%></span><br />
                    Regular Price: <i class="fa fa-inr" aria-hidden="true"></i> <del class="price old-price"><%# Eval("Price")%></del>
                   </div>
                  </div>
                  <div class="product-detail-inner">
                    <div class="detail-inner-left left-side">
                      <ul>
                        <li class="pro-cart-icon">
                          <div>
                          <button title="Add to Cart"><span></span></button>
                          <asp:LinkButton ID="linkAddtoCart" runat="server" Text="Add to Cart" CssClass="addcart"
                          CommandArgument='<%# Eval("ProductId") %>' OnClick="linkAddtoCart_Click" ToolTip="Add to Cart"></asp:LinkButton>
                        </div>
                        </li>
                      </ul>
                    </div>
                    <div class="detail-inner-left right-side">
                      <ul>
                        <li class="pro-wishlist-icon">
                        <asp:LinkButton ID="lnkWishlist2" runat="server"
                          CommandArgument='<%# Eval("ProductId") %>' OnClick="lnkWishlist2_Click"></asp:LinkButton>
                        </li>
                        <li class="pro-compare-icon">
                        <asp:LinkButton ID="lnkCompare2" runat="server"
                          CommandArgument='<%# Eval("ProductId") %>' OnClick="lnkCompare2_Click"></asp:LinkButton>
                        </li>
                      </ul>
                    </div>
                  </div>
                </div>
              </div>
             </ItemTemplate>
            </asp:Repeater>

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
                                                <asp:TextBox ID="txtemailSubs" runat="server" Text="" placeholder="Email Here..."></asp:TextBox>
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

    <script type="text/javascript" src="autocomplete/jquery-ui.min.js"></script>


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
