<%@ Page Language="C#" AutoEventWireup="true" Culture="en-GB" CodeFile="fundtransferrequest.aspx.cs" Inherits="fundtransferrequest" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="keywords" content="" />
    <meta name="distribution" content="global" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1" />
    <title>Pal Shoes</title>
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
        .mGrid {
            width: 100%;
            background-color: #fff;
            margin: 0px 1px 2px 1px;
            border: solid 1px #525252;
            border-collapse: collapse;
        }

            .mGrid td {
                padding: 7px;
                border: solid 1px #c1c1c1;
                color: #717171;
                font-size: 14px;
            }

            .mGrid th {
                padding: 7px;
                color: #fff;
                background: #424242 url(/images/grd_head.png) repeat-x top;
                border-left: solid 1px #525252;
                font-size: 14px;
            }

            .mGrid .alt {
            }

            .mGrid .pgr {
                background: #424242 url(/images/grd_pgr.png) repeat-x top;
                height: 32px;
            }

                .mGrid .pgr table {
                    margin: 5px 0;
                }

                .mGrid .pgr td {
                    border-width: 0;
                    padding: 0 6px;
                    border-left: solid 1px #666;
                    font-weight: bold;
                    color: #fff;
                    line-height: 12px;
                    font-size: 1.1em;
                }

            .mGrid td a {
                text-decoration: none;
                color: #0000FF;
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
                    <div class="page-title"><a href="account.aspx">Account</a></div>
                    <div class="bread-crumb-inner right-side float-none-xs">
                        <ul>
                            <li><a href="Default.aspx">Home</a><i class="fa fa-angle-right"></i></li>
                            <li><a href="account.aspx"><span>Account Settings</span></a></li>
                        </ul>
                    </div>
                </div>
            </div>
            <!-- Bread Crumb END -->

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
                  <a href="javascript:void(0)">Fund Transfer Request<i class="fa fa-angle-right"></i>
                  </a>
                </li>
                <%--<li id="step2">
                  <a href="javascript:void(0)">Wallet Payment Request<i class="fa fa-angle-right"></i>
                  </a>
                </li>--%>
              </ul>
            </div>
          </div>
        </div>

        <asp:HiddenField ID="HiddenField1" runat="server"></asp:HiddenField>
                <cc1:ModalPopupExtender ID="mpe" runat="server" PopupControlID="pnlPopup" TargetControlID="HiddenField1" 
                    CancelControlID="btnNo" BackgroundCssClass="modalBackground">
                </cc1:ModalPopupExtender>
                <asp:Panel ID="pnlPopup" runat="server" CssClass="modalPopup" Style="display: none">
                    <div class="header">
                    </div>
                    <div class="body">
                       <p class="text-center"><asp:Label ID="lblpopup" runat="server" Text=""></asp:Label></p>
                    </div>
                    <div class="footer1" align="right">
                        <asp:Button ID="btnNo" runat="server" Text="OK" />
                    </div>
                </asp:Panel>


        <div class="col-md-9 col-sm-8">

        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
          <Triggers>
          <asp:PostBackTrigger ControlID="btnSubmit" />
          <asp:AsyncPostBackTrigger ControlID="btnCancel" />
          </Triggers>
          <ContentTemplate>
          <div id="data-step1" class="account-content" data-temp="tabdata">
            <div class="row">
              <div class="col-xs-12">
                <div class="heading-part heading-bg mb-30">
                  <h2 class="heading m-0">Fund Transfer Request</h2>
                </div>
                <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
              </div>
            </div>
            <div class="m-0">
              <div class="main-form full">
                <div class="mb-20">
                  <div class="row">
                    <div class="col-sm-12">
                      <label>Beneficiary Name</label>
                      <div class="input-box">
                        <asp:TextBox ID="txtFullName" runat="server"></asp:TextBox>
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" 
                            ErrorMessage="* Full Name" ControlToValidate="txtFullName" Display="Dynamic" 
                            ValidationGroup="g1" ForeColor="Red"></asp:RequiredFieldValidator>
                      </div>
                    </div>
                    <div class="col-sm-12">
                      <label>Mobile Number</label>
                      <div class="input-box">
                        <asp:TextBox ID="txtContactNumber" runat="server"></asp:TextBox>
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" 
                            ErrorMessage="* Mobile Number" ControlToValidate="txtContactNumber" Display="Dynamic" 
                            ValidationGroup="g1" ForeColor="Red"></asp:RequiredFieldValidator>
                      </div>
                    </div>
                    <div class="col-sm-12">
                       <label>Transfer Amount</label>
                      <div class="input-box">
                        <asp:TextBox ID="txtAmount" runat="server"></asp:TextBox>
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" 
                            ErrorMessage="* Transfer Amount" ControlToValidate="txtAmount" Display="Dynamic" 
                            ValidationGroup="g1" ForeColor="Red"></asp:RequiredFieldValidator>
                        <cc1:FilteredTextBoxExtender runat="server" ID="FilteredTextBoxExtender9" TargetControlID="txtAmount"
                            FilterType="Custom" Enabled="true" FilterMode="ValidChars" ValidChars="1234567890."></cc1:FilteredTextBoxExtender>
                      </div>
                    </div>
                    <div class="col-sm-12">
                    <div class="left-side">
                     <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="g1" 
                            class="btn btn-black right-side" onclick="btnSubmit_Click"></asp:Button>
                    </div>
                      
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" 
                            class="btn-black right-side" onclick="btnCancel_Click"></asp:Button>
                    </div>
                  </div>
                </div>
              </div>
            </div>
           

         <asp:GridView ID="gvFundTransfer" runat="server" GridLines="None" 
            CssClass="mGrid" PagerStyle-CssClass="pgr" 
            AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False" 
                  onrowdatabound="gvFundTransfer_RowDataBound">
            <Columns>
            <asp:TemplateField HeaderText="S.N.">
                <ItemTemplate>
                    <%# Container.DataItemIndex + 1 %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="UserIdetifyno" HeaderText="Mobile No" />
            <asp:BoundField DataField="Amount" HeaderText="Amount" />
            <asp:BoundField DataField="RequestDate" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Requet Date" />
            <asp:BoundField DataField="AccountNo" HeaderText="Account No" />
            <asp:BoundField DataField="TransferDate" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Transfer Date" />
             <asp:TemplateField HeaderText="Status">
                 <ItemTemplate>
                     <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("RequestStatus") %>'></asp:Label>
                 </ItemTemplate>
             </asp:TemplateField>
        </Columns>
        <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
        <HeaderStyle Height="32px" />
        <PagerStyle CssClass="pgr"></PagerStyle>
        </asp:GridView>
       </div>
          
          <%--<div id="data-step2" class="account-content" data-temp="tabdata" style="display:none">
             <div class="row">
              <div class="col-xs-12">
                <div class="heading-part heading-bg mb-30">
                  <h2 class="heading m-0">Wallet Payment Request</h2>
                </div>
              </div>
            </div>
            <div class="m-0">
              <div class="main-form full">
                <div class="mb-20">
                  <div class="row">
                    <div class="col-sm-6">
                      <label>Your Name</label>
                      <div class="input-box">
                        <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                            ErrorMessage="* Youor Name" ControlToValidate="txtName" Display="Dynamic" 
                            ValidationGroup="g2" ForeColor="Red"></asp:RequiredFieldValidator>
                      </div>
                    </div>
                    <div class="col-sm-6">
                      <label>Mobile Number</label>
                      <div class="input-box">
                        <asp:TextBox ID="txtContactno" runat="server"></asp:TextBox>
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                            ErrorMessage="* Contact Number" ControlToValidate="txtContactno" Display="Dynamic" 
                            ValidationGroup="g2" ForeColor="Red"></asp:RequiredFieldValidator>
                      </div>
                    </div>
                    <div class="col-sm-6">
                       <label>Pay Amount</label>
                      <div class="input-box">
                        <asp:TextBox ID="txtPayamount" runat="server"></asp:TextBox>
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                            ErrorMessage="* Pay Amount" ControlToValidate="txtPayamount" Display="Dynamic" 
                            ValidationGroup="g2" ForeColor="Red"></asp:RequiredFieldValidator>
                        <cc1:FilteredTextBoxExtender runat="server" ID="FilteredTextBoxExtender1" TargetControlID="txtAmount"
                            FilterType="Custom" Enabled="true" FilterMode="ValidChars" ValidChars="1234567890."></cc1:FilteredTextBoxExtender>
                      </div>
                    </div>

                    <div class="col-sm-6">
                       <label>Payment Type</label>
                      <div class="input-box">
                        <asp:DropDownList ID="ddPaytype" runat="server"></asp:DropDownList>
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                            ErrorMessage="* Pay Type" ControlToValidate="ddPaytype" Display="Dynamic" 
                            ValidationGroup="g2" ForeColor="Red" InitialValue="Select PaymentType"></asp:RequiredFieldValidator>
                      </div>
                    </div>
                    <div class="col-sm-6">
                       <label>Transaction/Chaque No</label>
                      <div class="input-box">
                        <asp:TextBox ID="txtTransaction" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                            ErrorMessage="* Transaction No" ControlToValidate="txtTransaction" Display="Dynamic" 
                            ValidationGroup="g2" ForeColor="Red"></asp:RequiredFieldValidator>
                        <cc1:FilteredTextBoxExtender runat="server" ID="FilteredTextBoxExtender3" TargetControlID="txtTransaction"
                            FilterType="Custom" Enabled="true" FilterMode="ValidChars" ValidChars="1234567890"></cc1:FilteredTextBoxExtender>
                      </div>
                    </div>
                    <div class="col-sm-12">
                    <div class="left-side">
                     <asp:Button ID="btnPaySubmit" runat="server" Text="Submit" ValidationGroup="g2" 
                            class="btn btn-black right-side" onclick="btnPaySubmit_Click"></asp:Button>
                    </div>
                    <asp:Button ID="btnCancel2" runat="server" Text="Cancel" 
                            class="btn-black right-side" onclick="btnCancel2_Click"></asp:Button>
                      
                    </div>
                  </div>
                </div>
              </div>
            </div>

         <asp:GridView ID="gvPayamout" runat="server" GridLines="None" 
            CssClass="mGrid" PagerStyle-CssClass="pgr" 
            AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False" 
                  onrowdatabound="gvPayamout_RowDataBound">
          <Columns>
            <asp:TemplateField HeaderText="S.N.">
                <ItemTemplate>
                    <%# Container.DataItemIndex + 1 %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="UserIdetifyno" HeaderText="Mobile No" />
            <asp:BoundField DataField="Amount" HeaderText="Amount" />
            <asp:BoundField DataField="RequestDate" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Requet Date" />
            <asp:BoundField DataField="AccountNo" HeaderText="Transaction No" />
            <asp:BoundField DataField="TransferDate" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Pay Date" />
             <asp:TemplateField HeaderText="Status">
                 <ItemTemplate>
                     <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("RequestStatus") %>'></asp:Label>
                 </ItemTemplate>
             </asp:TemplateField>
        </Columns>
        <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
        <HeaderStyle Height="32px" />
        <PagerStyle CssClass="pgr"></PagerStyle>
        </asp:GridView>
        </div>--%>
        </ContentTemplate>
       </asp:UpdatePanel>

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
