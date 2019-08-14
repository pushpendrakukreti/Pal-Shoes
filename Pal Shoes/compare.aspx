<%@ Page Language="C#" AutoEventWireup="true" CodeFile="compare.aspx.cs" Inherits="compare" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/Pager.ascx" TagName="Pager" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Pal Shoes</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="description" content="" />
    <meta name="keywords" content="" />
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
    <script type="text/javascript">
        function radioMe(e) {
            if (!e) e = window.event;
            var sender = e.target || e.srcElement;

            if (sender.nodeName != 'INPUT') return;
            var checker = sender;
            var chkBox = document.getElementById('<%= CheckBoxList1.ClientID %>');
            var chks = chkBox.getElementsByTagName('INPUT');
            for (i = 0; i < chks.length; i++) {
                if (chks[i] != checker)
                    chks[i].checked = false;
            }
        }
    </script>
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

    <style type="text/css">
        .chk {
            margin: 0;
            padding: 0;
            width: auto;
            border: none;
        }

            .chk td {
                margin: 0;
                padding: 0;
                vertical-align: middle;
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
            min-height: 452px;
        }

            .style_prevu_kit:hover {
                background-color: #FFF;
                -webkit-box-shadow: 0px 3px 20px 3px rgba(0,0,0,0.75);
                -moz-box-shadow: 0px 3px 20px 3px rgba(0,0,0,0.75);
                box-shadow: 0px 3px 20px 3px rgba(0,0,0,0.75);
                z-index: 555555;
                -webkit-transition: all 400ms ease-in;
                -webkit-transform: scale(1.1);
                -ms-transition: all 400ms ease-in;
                -ms-transform: scale(1.1);
                -moz-transition: all 400ms ease-in;
                -moz-transform: scale(1.1);
                transition: all 400ms ease-in;
                transform: scale(1.1);
                border: 2px solid #00AFEF;
            }

        .vtextright {
            float: right;
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

        table.compproduct {
            margin: 0 auto 10px auto;
            background: #FFFFFF;
            padding: 0;
            text-align: center;
            width: 95%;
        }

        .compproduct td {
            vertical-align: top;
            padding: 6px;
            font-size: 12px;
            border: 1px solid#D5D5D5;
            height: auto;
        }

            .compproduct td img {
                padding: 0px;
                border: 1px solid#D5D5D5;
                width: 200px;
            }

            .compproduct td .remove {
                height: 16px;
                width: 50px;
                border: 0;
                padding: 0px;
                margin: 0;
                background: none;
                line-height: 2px;
                font-size: 11px;
                font-weight: 400;
            }

        .sale-label a {
            color: #FFFFFF;
        }
    </style>

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
                <div class="bread-crumb mtb-30">
                    <%--<table class="compproduct" cellpadding="6" cellspacing="6">
            <tr>
            <td><div class='product-item'><div class='product-image'>
            <a href='product-detail.aspx?product='>
            <img src='Upload/thumbnails/20171208195655-ultimate_nutrition_prostar_100_whey_protein_5.28_lb_chocolate_creme.jpg' alt='' /></a></div>
            <div class='product-item-details'><div class='product-item-name'>
            <a href='product-detail.aspx?product='>msdf ldsgjsd sdgsdgdsg</a></div>
            <div class='price-box'>Price: <i class='fa fa-inr' aria-hidden='true'></i> 
            <span class='price'>345.00</span></div></div></div></td>
            <td><img src="img/No-Product.png" alt="" /></td>
            <td><img src="img/No-Product.png" alt="" /></td>
            <td><img src="img/No-Product.png" alt="" /></td>
            </tr>
            </table>--%>
                    <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
                    <div class="text-center">
                        <asp:LinkButton ID="linkToCompare" runat="server" class="btn btn-black"
                            OnClick="linkToCompare_Click">Compare Products Now</asp:LinkButton>
                        &nbsp;&nbsp;
            <b><big>
                <asp:Label ID="lblCompareItem" runat="server" Text=""></asp:Label></big></b>
                    </div>
                </div>
            </div>
            <!-- Bread Crumb END -->


            <!-- CONTAIN START -->
            <section class="pb-95">
    <div class="container">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
      <div class="row">
        <div class="col-md-3 col-sm-4 mb-xs-30">
          <div class="sidebar-block">
            <div class="sidebar-box listing-box mb-40">
              <span class="opener plus"></span>
              <div class="main_title sidebar-title">
                <h3><span>Categories</span></h3>
              </div>
              <div class="sidebar-contant">
                <ul>
                 <asp:Repeater ID="rpCategory" runat="server">
                 <ItemTemplate>
                    <li><a href='product.aspx?category=<%# Eval("CategoryId") %>'>
                    <%# Eval("CategoryName")%> <span>(<%# Eval("product")%>)</span></a></li>
                 </ItemTemplate>
                 </asp:Repeater>
                </ul>
              </div>
            </div>
            <div class="sidebar-box filter-sidebar mb-40">
              <span class="opener plus"></span>
              <div class="main_title sidebar-title">
                <h3><span>Brand</span></h3>
              </div>
              <div class="sidebar-contant">
                <%--<div class="price-range mb-30">
                  <div class="inner-title">Price range</div>
                  <input class="price-txt" type="text" id="amount"/>
                  <div id="slider-range"></div>
                </div>--%>
                <div class="filter-inner-box mb-20">
                  <%--<div class="inner-title">Brand</div>--%>
                 <asp:CheckBoxList CssClass="chk" ID="CheckBoxList1" runat="server" AutoPostBack="True" 
                          onselectedindexchanged="CheckBoxList1_SelectedIndexChanged1"></asp:CheckBoxList>
                  <%--<ul id="brd">
                  <asp:Repeater ID="rpBrand" runat="server" onitemdatabound="rpBrand_ItemDataBound">
                  <ItemTemplate>
                    <li>
                      <span><asp:CheckBox ID="CheckBox1" runat="server"></asp:CheckBox></span>
                      <label for="apple-brand"><a href='product.aspx?Brand=<%# Eval("Brand") %>'>
                      <%# Eval("Brand")%></a> <span>(<%# Eval("product")%>)</span></label>
                    </li>
                  </ItemTemplate>
                  </asp:Repeater>
                  </ul>--%>
                </div>
                <%--<div class="filter-inner-box mb-20">
                  <div class="inner-title">Flavour</div>
                  <ul>
                  <asp:Repeater ID="rpFlavour" runat="server">
                  <ItemTemplate>
                    <li>
                      <span><input type="checkbox" class="checkbox" id="Checkbox1" /></span>
                      <label for="apple-brand"><a href='product.aspx?Flavour=<%# Eval("Producttype") %>'><%# Eval("Producttype")%></a> <span>(<%# Eval("product")%>)</span></label>
                    </li>
                  </ItemTemplate>
                  </asp:Repeater>
                  </ul>
                </div>--%>
                <%--<a href="#" class="btn btn-black">Refine</a>--%>
              </div>
            </div>
          </div>
        </div>
        <div class="col-md-9 col-sm-8">
          <div class="shorting mb-30">
            <div class="row">
              <div class="col-md-6">
                <div class="view">
                  <div class="list-types grid active ">
                    <a href="product.aspx">
                      <div class="grid-icon list-types-icon"></div>
                    </a> 
                  </div>
                  <div class="list-types list">
                    <%--<a href="#">
                      <div class="list-icon list-types-icon"></div>
                    </a> --%>
                  </div>
                </div>
                <div class="short-by float-right-sm">
                  <span>Sort By</span>
                  <div class="select-item">
                    <asp:DropDownList ID="ddSortBy" runat="server" AutoPostBack="True" 
                          onselectedindexchanged="ddSortBy_SelectedIndexChanged">
                        <asp:ListItem Text="Products Price" Value="0"></asp:ListItem>
                        <asp:ListItem Text="price (Low to High)" Value="1"></asp:ListItem>
                        <asp:ListItem Text="price (High to Low)" Value="2"></asp:ListItem>
                    </asp:DropDownList>
                  </div>
                </div>
              </div>
              <div class="col-md-6">
                <div class="show-item right-side float-left-sm">
                  <span>Show</span>
                  <div class="select-item">
                    <select>
                      <%--<option value="" selected="selected">9</option>--%>
                      <option value="">12</option>
                    </select>
                  </div>
                  <span>Per Page</span>&nbsp;&nbsp;&nbsp;
                  <div class="compare float-right-sm">
                    &nbsp;&nbsp;
                  </div>
                </div>
              </div>
            </div>
          </div>

          <div class="product-listing">
            <div class="row mlr_-20">

          <asp:Repeater ID="rpProduct" runat="server">
          <ItemTemplate>
           <div class="col-md-4 col-xs-6 plr-20">
              <div class="style_prevu_kit product-item">
                <div class="product-image">
                <asp:LinkButton ID="btnAddToCompare" CommandArgument='<%# Eval("ProductId") %>'
                  OnClick="btnAddToCompare_Click" runat="server">
                    <div class="sale-label">
                    Add to compare
                    </div>
                    <img src='Upload/thumbnails/<%# Eval("Thumbnail") %>' alt="" />
                  </asp:LinkButton>
                </div>
                <div class="product-item-details">
                  <div class="rating-summary-block">
                      <div title="53%" class="rating-result">
                        <span style="width:53%"></span>
                      </div>
                    </div>
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
                        <li class="pro-wishlist-icon" title="Wishlist">
                        <asp:LinkButton ID="lnkWishlist" runat="server"
                          CommandArgument='<%# Eval("ProductId") %>' OnClick="lnkWishlist_Click"></asp:LinkButton>
                        </li>
                        <li class="pro-compare-icon" title="Compare">
                        <asp:LinkButton ID="lnkCompare" runat="server"
                          CommandArgument='<%# Eval("ProductId") %>' OnClick="lnkCompare_Click"></asp:LinkButton>
                        </li>
                      </ul>
                    </div>
                </div>
              </div>
              </div>
            </ItemTemplate>
         </asp:Repeater>

            <div class="row">
              <div class="col-xs-12">
                <div class="pagination-bar">
                <uc1:Pager ID="bottomPager" runat="server" Visible="false" />
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
