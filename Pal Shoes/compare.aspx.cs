﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using SqlAccess;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Web.Services;
using System.Collections;
using System.Text;
using System.Drawing;

public partial class compare : System.Web.UI.Page
{
    Catergoy local_category = new Catergoy();
    SubCategory local_subcategory = new SubCategory();
    Products local_product = new Products();
    ShoppingCart local_cart = new ShoppingCart();
    OrderDetails local_orderlist = new OrderDetails();
    WalletCls local_wallet = new WalletCls();

    protected void Page_Load(object sender, EventArgs e)
    {
        CheckBoxList1.Attributes.Add("onclick", "radioMe(event);");

        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Session["currency"] as string))
            {
                if (Session["currency"].ToString() == "USD")
                {
                    //ddCurrency.SelectedValue = "2";
                }
                else
                {
                    //ddCurrency.SelectedValue = "1";
                }
            }
            else
            {
                Session["currency"] = "INR";
            }

            BindProductCategory();
            BindFlavour();
            BindProductBrand();

            string subcategoryid = Request.QueryString["subcategory"];
            string categoryid = Request.QueryString["category"];
            string flavour = Request.QueryString["Flavour"];
            string brand = Request.QueryString["Brand"];
            if (subcategoryid != null)
            {
                BindProductBySubcategory(subcategoryid);
            }
            if (categoryid != null)
            {
                BindProductByCategory(categoryid);
            }
            if (categoryid == null && subcategoryid == null && flavour == null && brand == null)
            {
                if (ddSortBy.SelectedIndex > 0)
                {
                    if (ddSortBy.SelectedValue == "1")
                    {
                        BindProductsLowToHigh();
                    }
                    else if (ddSortBy.SelectedValue == "2")
                    {
                        BindProductsHighToLow();
                    }
                }
                else
                {
                    BindDefaultProducts();
                }
            }
            if (flavour != null)
            {
                BindProductByFlavour(flavour);
            }
            if (brand != null)
            {
                BindProductByBrand(brand);
            }

            if (!string.IsNullOrEmpty(Session["Transid"] as string))
            {
                BintCartItemsPrice();
            }
            BintCartItems();
            
        }
        if (Session["userid"] != null)
        {
            pnlwishlist.Visible = true;
        }
        BindCategoryMenu();
        BindLoginLogout();
        BindWalletAmount();
        BindAccessories();
        BindProductToCompare();// Compare

        if (!string.IsNullOrEmpty(Session["Tid"] as string))
        {
            BindWishlistNo();
        }
        if (!string.IsNullOrEmpty(Session["compare"] as string))
        {
            BindCompareItemsNo();
        }
    }

    protected void btnSubscribe_Click(object sender, EventArgs e)
    {
        string emailid = txtemail.Text;
        string date = DateTime.Now.ToString("dd/MM/yyyy");
        if (emailid != "")
        {
            int success = 0;
            success = local_product.AddNewSubscribe(emailid, date);
            if (success != 0)
            {
                txtemail.Text = "";
                lblMessageSubscribe.ForeColor = Color.White;
                lblMessageSubscribe.Text = "Message Send Successfully";
                ViewState["ID"] = null;
            }
            else
            {
                lblMessageSubscribe.ForeColor = Color.White;
                lblMessageSubscribe.Text = "Message Send Failed";
            }
        }
    }
    private void BindDefaultProducts()
    {
        string page = Request.QueryString["Page"];
        if (page == null)
            page = "1";

        int howManyPages = 1;
        string firstPageUrl = "";
        string pagerFormat = "";

        try
        {
            DataTable dt = new DataTable();
            dt = local_product.getDefaultProductsOffer(page, out howManyPages);
            if (dt.Rows.Count > 0)
            {
                rpProduct.DataSource = dt;
                rpProduct.DataBind();

                firstPageUrl = Link.ToProductOnDefault("1");
                pagerFormat = Link.ToProductOnDefault("{0}");
            }
            bottomPager.Show(int.Parse(page), howManyPages, firstPageUrl, pagerFormat, true);
        }
        catch
        { }
    }


    //rpProduct
    private void BindProductBySubcategory(string subcategoryid)
    {
        string page = Request.QueryString["Page"];
        if (page == null)
            page = "1";

        int howManyPages = 1;
        string firstPageUrl = "";
        string pagerFormat = "";

        try
        {
            DataTable dt = new DataTable();
            dt = local_product.getProductsOnSubCategory7(subcategoryid, page, out howManyPages);
            if (dt.Rows.Count > 0)
            {
                rpProduct.DataSource = dt;
                rpProduct.DataBind();

                firstPageUrl = Link.ToProductSubCategory(subcategoryid, "1");
                pagerFormat = Link.ToProductSubCategory(subcategoryid, "{0}");
            }
            bottomPager.Show(int.Parse(page), howManyPages, firstPageUrl, pagerFormat, true);
        }
        catch
        { }
    }
    private void BindProductByCategory(string categoryid)
    {
        string page = Request.QueryString["Page"];
        if (page == null)
            page = "1";

        int howManyPages = 1;
        string firstPageUrl = "";
        string pagerFormat = "";

        try
        {
            DataTable dt = new DataTable();
            dt = local_product.getProductsOnCategory7(categoryid, page, out howManyPages);
            if (dt.Rows.Count > 0)
            {
                rpProduct.DataSource = dt;
                rpProduct.DataBind();

                firstPageUrl = Link.ToProductOnCategory(categoryid, "1");
                pagerFormat = Link.ToProductOnCategory(categoryid, "{0}");
            }
            bottomPager.Show(int.Parse(page), howManyPages, firstPageUrl, pagerFormat, true);
        }
        catch
        { }
    }

    private void BindProductByFlavour(string flavour)
    {
        string page = Request.QueryString["Page"];
        if (page == null)
            page = "1";

        int howManyPages = 1;
        string firstPageUrl = "";
        string pagerFormat = "";

        try
        {
            DataTable dt = new DataTable();
            dt = local_product.getProductByFlavour(flavour, page, out howManyPages);
            if (dt.Rows.Count > 0)
            {
                rpProduct.DataSource = dt;
                rpProduct.DataBind();

                firstPageUrl = Link.ToProductOnFlavour(flavour, "1");
                pagerFormat = Link.ToProductOnFlavour(flavour, "{0}");
            }
            bottomPager.Show(int.Parse(page), howManyPages, firstPageUrl, pagerFormat, true);
        }
        catch
        { }
    }

    private void BindProductByBrand(string brand)
    {
        string page = Request.QueryString["Page"];
        if (page == null)
            page = "1";

        int howManyPages = 1;
        string firstPageUrl = "";
        string pagerFormat = "";

        try
        {
            DataTable dt = new DataTable();
            dt = local_product.getProductByBrand(brand, page, out howManyPages);
            if (dt.Rows.Count > 0)
            {
                rpProduct.DataSource = dt;
                rpProduct.DataBind();

                firstPageUrl = Link.ToProductOnBrand(brand, "1");
                pagerFormat = Link.ToProductOnBrand(brand, "{0}");
            }
            bottomPager.Show(int.Parse(page), howManyPages, firstPageUrl, pagerFormat, true);
        }
        catch
        { }
    }

    protected void ddCurrency_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (ddCurrency.SelectedIndex == 0)
        //{
        //    Session["currency"] = "INR";
        //}
        //else if (ddCurrency.SelectedIndex == 1)
        //{
        //    Session["currency"] = "USD";
        //}
    }

    private void BindLoginLogout()
    {
        if (!string.IsNullOrEmpty(Session["userid"] as string))
        {
            lnkLogin.Text = "<i class='fa fa-sign-out'></i> Logout";
        }
        else
        {
            lnkLogin.Text = "<i class='fa fa-lock'></i> Sign Up";
        }
    }
    protected void lnkLogin_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Session["userid"] as string))
        {
            lnkLogin.Text = "<i class='fa fa-sign-out'></i> Logout";
            Session.Abandon();
            Session.Clear();
            Response.Redirect("~/default.aspx", false);
        }
        else
        {
            lnkLogin.Text = "<i class='fa fa-lock'></i> Sign Up";
            Response.Redirect("~/register.aspx", false);
        }
    }


    private void BindProductCategory()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = local_product.getProductCategory();
            if (dt.Rows.Count > 0)
            {
                rpCategory.DataSource = dt;
                rpCategory.DataBind();
            }
        }
        catch
        { }
    }
    private void BindProductBrand()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = local_product.getBrandProducts();
            if (dt.Rows.Count > 0)
            {
                //rpBrand.DataSource = dt;
                //rpBrand.DataBind();
                CheckBoxList1.DataSource = dt;
                CheckBoxList1.DataTextField = "Brand";
                CheckBoxList1.DataValueField = "Brand";
                CheckBoxList1.DataBind();
            }
        }
        catch
        { }
    }

    //Nav Menu
    private void BindCategoryMenu()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = local_category.getMenuCategory();
            if (dt.Rows.Count > 0)
            {
                rpMenuCategory.DataSource = dt;
                rpMenuCategory.DataBind();
            }
        }
        catch
        { }
    }
    protected void Menu_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            Repeater rplist = (Repeater)e.Item.FindControl("rpSubCategory");
            DataRowView drow = e.Item.DataItem as DataRowView;
            string categoryid = drow.Row.ItemArray[0].ToString();
            DataTable dt = new DataTable();
            dt = local_category.getMenuSubCategory(categoryid);
            if (dt.Rows.Count > 0)
            {
                rplist.DataSource = dt;
                rplist.DataBind();
            }
        }
        catch
        { }
    }

    private void BindFlavour()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = local_product.getProductFlavour();
            if (dt.Rows.Count > 0)
            {
                //rpFlavour.DataSource = dt;
                //rpFlavour.DataBind();
            }
        }
        catch
        { }
    }

    //Cart Items
    private void BintCartItems()
    {
        try
        {
            DataTable dt = new DataTable();
            if (Session["Transid"] != null)
            {
                string transctionid = Session["Transid"].ToString();
                dt = local_cart.getCartItemNum(transctionid);
                if (dt.Rows.Count > 0)
                {
                    rpnotification.DataSource = dt;
                    rpnotification.DataBind();
                }
            }
            else
            {
                dt.Columns.Add("Items", typeof(string));
                DataRow row = dt.NewRow();
                row["Items"] = "0";
                dt.Rows.Add(row);
                rpnotification.DataSource = dt;
                rpnotification.DataBind();
            }
        }
        catch
        { }
    }
    private void BintCartItemsPrice()
    {
        try
        {
            DataTable dt = new DataTable();
            string transctionid = Session["Transid"].ToString();
            dt = local_cart.getCartItemtotPrice(transctionid);
            if (dt.Rows.Count > 0)
            {
                rpItempPrice.DataSource = dt;
                rpItempPrice.DataBind();
            }
        }
        catch
        { }
    }


    //Add to Cart
    protected void linkAddtoCart_Click(object sender, EventArgs e)
    {
        if (Session["Transid"] == null)
        {
            Random rd = new Random();
            string Tid = rd.Next().ToString();
            Session["Transid"] = Tid;
        }
        if (Session["total"] == null)
        {
            Session["total"] = "0";
        }

        try
        {
            DataTable dt = new DataTable();
            LinkButton btn = (LinkButton)(sender);
            string product = btn.CommandArgument;
            dt = local_cart.getDataFromProdtuct(product);
            if (dt.Rows.Count > 0)
            {
                string transid = Session["Transid"].ToString();
                Int32 productid = Convert.ToInt32(dt.Rows[0]["ProductId"]);
                string name = Convert.ToString(dt.Rows[0]["Name"]);
                decimal price = Convert.ToDecimal(dt.Rows[0]["Price"]);
                string capacity = Convert.ToString(dt.Rows[0]["Capacity"]);
                Int32 discount = Convert.ToInt32(dt.Rows[0]["discountprice"]);
                decimal totalprice = Convert.ToDecimal(dt.Rows[0]["totalprice"]);
                string thumbnail = Convert.ToString(dt.Rows[0]["Thumbnail"]);

                Int32 quantity = 1;
                string variantname = Convert.ToString(dt.Rows[0]["Variantname"]);
                Int32 variantquantity = Convert.ToInt32(dt.Rows[0]["Quantity"]);

                Int32 shippingchare = Convert.ToInt32(dt.Rows[0]["Modelno"].ToString());
                int flavour1 = Convert.ToInt32(dt.Rows[0]["Producttype"].ToString());

                Int32 userid = Convert.ToInt32(Session["userid"]);
                int result = 0;
                result = local_cart.InsertIntoShoppingCart(transid, productid, price, variantname, variantquantity, quantity, discount, totalprice, shippingchare, name, thumbnail, capacity, userid, flavour1);
                if (result != 0)
                {
                    Response.Redirect("~/cart.aspx", false);
                }
            }
        }
        catch
        { }
    }


    protected void lnkCompare_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btn = (LinkButton)(sender);
            string productid = btn.CommandArgument;
            Int32 ProductId = Convert.ToInt32(productid);
            if (Session["compare"] == null)
            {
                Random rd = new Random();
                string csid = rd.Next().ToString();
                Session["compare"] = csid;
            }

            string comsession = Session["compare"].ToString();
            int count = 1;
            if (Session["count"] == null)
            {
                Session["count"] = count;
            }
            int result;
            int c = Convert.ToInt32(Session["count"]);
            if (c <= 4)
            {
                if (local_product.IfCompareProductExists(ProductId, comsession) == "F")
                {
                    result = local_product.InsertCompareProduct(ProductId, comsession);

                    count = Convert.ToInt32(Session["count"]);
                    count = count + 1;
                    Session["count"] = count;
                    if (result != 0)
                    {
                        BindProductToCompare();

                        string subcategoryid = Request.QueryString["subcategory"];
                        string categoryid = Request.QueryString["category"];
                        string flavour = Request.QueryString["Flavour"];
                        string brand = Request.QueryString["Brand"];
                        if (subcategoryid != null)
                        {
                            Response.Redirect("~/compare.aspx?subcategory=" + subcategoryid + "", false);
                        }
                        else if (categoryid != null)
                        {
                            Response.Redirect("~/compare.aspx?category=" + categoryid + "", false);
                        }
                        else if (flavour != null)
                        {
                            Response.Redirect("~/compare.aspx?Flavour=" + flavour + "", false);
                        }
                        else if (brand != null)
                        {
                            Response.Redirect("~/compare.aspx?Brand=" + brand + "", false);
                        }
                        else
                        {
                            Response.Redirect("~/compare.aspx", false);
                        }
                    }
                }
                else
                {
                    mpe.Show();
                    lblpopup.Text = "You are already added this product!";
                    //Response.Write("<script>alert('You are already added this product!')</script>");
                }
            }
            else
            {
                mpe.Show();
                lblpopup.Text = "You can compare maximum 4 products!";
                //Response.Write("<script>alert('You can compare maximum 4 products!')</script>");
            }
        }
        catch
        { }
    }


    protected void lnkWishlist_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["userid"] != null)
            {
                if (Session["Tid"] == null)
                {
                    Random rd = new Random();
                    string Tid = rd.Next().ToString();
                    Session["Tid"] = Tid;
                }
                DataTable dt = new DataTable();
                LinkButton btn = (LinkButton)(sender);
                string product = btn.CommandArgument;
                dt = local_cart.getDataFromProdtuct(product);
                if (dt.Rows.Count > 0)
                {
                    string transid = Session["Tid"].ToString();
                    Int32 productid = Convert.ToInt32(dt.Rows[0]["ProductId"]);
                    decimal price = Convert.ToDecimal(dt.Rows[0]["Price"]);
                    Int32 discount = Convert.ToInt32(dt.Rows[0]["discountprice"]);
                    string name = Convert.ToString(dt.Rows[0]["Name"]);
                    string thumbnail = Convert.ToString(dt.Rows[0]["Thumbnail"]);
                    Int32 userid = Convert.ToInt32(Session["userid"]);
                    string usermobile = BindUserIdentify(Session["userid"].ToString());

                    int result = 0;
                    if (local_orderlist.IsWishlistExist(productid, transid, "N", "") == "F")
                    {
                        result = local_orderlist.AddNewWishlist(productid, transid, price, discount, name, thumbnail, userid, usermobile);
                        if (result != 0)
                        {
                            string subcategoryid = Request.QueryString["subcategory"];
                            string categoryid = Request.QueryString["category"];
                            string flavour = Request.QueryString["Flavour"];
                            string brand = Request.QueryString["Brand"];
                            if (subcategoryid != null)
                            {
                                Response.Redirect("~/compare.aspx?subcategory=" + subcategoryid + "", false);
                            }
                            else if (categoryid != null)
                            {
                                Response.Redirect("~/compare.aspx?category=" + categoryid + "", false);
                            }
                            else if (flavour != null)
                            {
                                Response.Redirect("~/compare.aspx?Flavour=" + flavour + "", false);
                            }
                            else if (brand != null)
                            {
                                Response.Redirect("~/compare.aspx?Brand=" + brand + "", false);
                            }
                            else
                            {
                                Response.Redirect("~/compare.aspx", false);
                            }
                        }
                        else
                        {
                            mpe.Show();
                            lblpopup.Text = "You are already added to wishlist!";
                            //Response.Write("<script>alert('You are already added to wishlist!')</script>");
                        }
                    }
                }
            }
            else
            {
                mpe.Show();
                lblpopup.Text = "Please login and then add to wishlist.";
                //Response.Redirect("~/login.aspx", false);
            }
        }
        catch
        { }
    }

    private void BindWishlistNo()
    {
        try
        {
            DataTable dt = new DataTable();
            string transid = Session["Tid"].ToString();
            dt = local_cart.getWishListCurrentNo(transid);
            if (dt.Rows.Count > 0)
            {
                Rpwishlist.DataSource = dt;
                Rpwishlist.DataBind();
            }
        }
        catch
        { }
    }
    private void BindCompareItemsNo()
    {
        try
        {
            DataTable dt = new DataTable();
            string transid = Session["compare"].ToString();
            dt = local_cart.getCompareItemsNo(transid);
            if (dt.Rows.Count > 0)
            {
                rpCompare.DataSource = dt;
                rpCompare.DataBind();
                lblCompareItem.Text = "(" + Convert.ToString(dt.Rows[0]["compare"]) + ")";
            }
        }
        catch
        { }
    }


    protected void ddSortBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddSortBy.SelectedIndex > 0)
        {
            if (ddSortBy.SelectedValue == "1")
            {
                BindProductsLowToHigh();
            }
            else if (ddSortBy.SelectedValue == "2")
            {
                BindProductsHighToLow();
            }
        }
    }

    private void BindProductsAtoZ()
    {
        string page = Request.QueryString["Page"];
        if (page == null)
            page = "1";

        int howManyPages = 1;
        string firstPageUrl = "";
        string pagerFormat = "";

        try
        {
            DataTable dt = new DataTable();
            dt = local_product.getAtoZProducts5(page, out howManyPages);
            if (dt.Rows.Count > 0)
            {
                rpProduct.DataSource = dt;
                rpProduct.DataBind();

                firstPageUrl = Link.ToProductOnDefault("1");
                pagerFormat = Link.ToProductOnDefault("{0}");
            }
            bottomPager.Show(int.Parse(page), howManyPages, firstPageUrl, pagerFormat, true);
        }
        catch
        { }
    }

    private void BindProductsZtoA()
    {
        string page = Request.QueryString["Page"];
        if (page == null)
            page = "1";

        int howManyPages = 1;
        string firstPageUrl = "";
        string pagerFormat = "";

        try
        {
            DataTable dt = new DataTable();
            dt = local_product.getZtoAProducts5(page, out howManyPages);
            if (dt.Rows.Count > 0)
            {
                rpProduct.DataSource = dt;
                rpProduct.DataBind();

                firstPageUrl = Link.ToProductOnDefault("1");
                pagerFormat = Link.ToProductOnDefault("{0}");
            }
            bottomPager.Show(int.Parse(page), howManyPages, firstPageUrl, pagerFormat, true);
        }
        catch
        { }
    }

    private void BindProductsLowToHigh()
    {
        string page = Request.QueryString["Page"];
        if (page == null)
            page = "1";

        int howManyPages = 1;
        string firstPageUrl = "";
        string pagerFormat = "";

        try
        {
            DataTable dt = new DataTable();
            dt = local_product.getLowToHighProducts5(page, out howManyPages);
            if (dt.Rows.Count > 0)
            {
                rpProduct.DataSource = dt;
                rpProduct.DataBind();

                firstPageUrl = Link.ToProductOnDefault("1");
                pagerFormat = Link.ToProductOnDefault("{0}");
            }
            bottomPager.Show(int.Parse(page), howManyPages, firstPageUrl, pagerFormat, true);
        }
        catch
        { }
    }

    private void BindProductsHighToLow()
    {
        string page = Request.QueryString["Page"];
        if (page == null)
            page = "1";

        int howManyPages = 1;
        string firstPageUrl = "";
        string pagerFormat = "";

        try
        {
            DataTable dt = new DataTable();
            dt = local_product.getHighToLowProducts5(page, out howManyPages);
            if (dt.Rows.Count > 0)
            {
                rpProduct.DataSource = dt;
                rpProduct.DataBind();

                firstPageUrl = Link.ToProductOnDefault("1");
                pagerFormat = Link.ToProductOnDefault("{0}");
            }
            bottomPager.Show(int.Parse(page), howManyPages, firstPageUrl, pagerFormat, true);
        }
        catch
        { }
    }

    private void BindWalletAmount()
    {
        try
        {
            DataTable dt = new DataTable();
            string user = Session["userid"].ToString();
            dt = local_wallet.getWalletIconAmount(user);
            if (dt.Rows.Count > 0)
            {
                //rpWallet.DataSource = dt;
                //rpWallet.DataBind();
            }
        }
        catch
        { }
    }


    private void BindAccessories()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = local_subcategory.getAccessoriesSubCategory();
            if (dt.Rows.Count > 0)
            {
                //rbAccessories.DataSource = dt;
                //rbAccessories.DataBind();
            }
        }
        catch
        { }
    }


    private string BindUserIdentify(string userid)
    {
        DataTable dt = new DataTable();
        string usermobile = "";
        try
        {
            dt = local_cart.getUserIdetifyNumber(userid);
            if (dt.Rows.Count > 0)
            {
                usermobile = dt.Rows[0]["UserIdentifyNo"].ToString();
            }
        }
        catch
        { }
        return usermobile;
    }

    protected void rpBrand_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            if ((e.Item.ItemType == ListItemType.Item) || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lblstatus = (Label)e.Item.FindControl("lblstatus");
                CheckBox chekbox = (CheckBox)e.Item.FindControl("CheckBox1");
                string brandname = Request.QueryString["Brand"];

            }
        }
        catch
        { }
    }


    protected void CheckBoxList1_SelectedIndexChanged1(object sender, EventArgs e)
    {
        foreach (ListItem item in CheckBoxList1.Items)
        {
            if (item.Selected)
            {
                string selected = item.Value;
                if (selected != null)
                {
                    BindProductByBrand(selected);
                }
            }
        }
    }


    protected void Searchbtn_Click(object sender, EventArgs e)
    {
        if (txtSearch.Text != "")
        {
            string search = txtSearch.Text.Trim();
            Response.Redirect("search.aspx?search=" + search);
        }
        else
        {
            
        }
    }


    [WebMethod]
    public static List<string> GetSerchResult(string searchdata)
    {
        List<string> empResult = new List<string>();
        string name = searchdata;
        string brand = searchdata;
        string price = searchdata;
        string modelno = searchdata;
        string capacity = searchdata;
        string producttype = searchdata;
        Products ob_p = new Products();
        try
        {
            DataTable dt = new DataTable();
            dt = ob_p.getSearchRecord(name, brand, price, modelno, capacity, producttype);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    empResult.Add(dt.Rows[i]["Name"].ToString());
                }
            }
        }
        catch
        { }
        return empResult;
    }


    protected void btnAddToCompare_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btn = (LinkButton)(sender);
            string productid = btn.CommandArgument;
            Int32 ProductId = Convert.ToInt32(productid);
            if (Session["compare"] == null)
            {
                Random rd = new Random();
                string csid = rd.Next().ToString();
                Session["compare"] = csid;
            }

            string comsession = Session["compare"].ToString();
            int count = 1;
            if (Session["count"] == null)
            {
                Session["count"] = count;
            }
            int result;
            int c = Convert.ToInt32(Session["count"]);
            if (c <= 4)
            {
                if (local_product.IfCompareProductExists(ProductId, comsession) == "F")
                {
                    result = local_product.InsertCompareProduct(ProductId, comsession);

                    count = Convert.ToInt32(Session["count"]);
                    count = count + 1;
                    Session["count"] = count;
                    if (result != 0)
                    {

                        BindProductToCompare();

                        string subcategoryid = Request.QueryString["subcategory"];
                        string categoryid = Request.QueryString["category"];
                        string flavour = Request.QueryString["Flavour"];
                        string brand = Request.QueryString["Brand"];
                        if (subcategoryid != null)
                        {
                            Response.Redirect("~/compare.aspx?subcategory=" + subcategoryid + "", false);
                        }
                        else if (categoryid != null)
                        {
                            Response.Redirect("~/compare.aspx?category=" + categoryid + "", false);
                        }
                        else if (flavour != null)
                        {
                            Response.Redirect("~/compare.aspx?Flavour=" + flavour + "", false);
                        }
                        else if (brand != null)
                        {
                            Response.Redirect("~/compare.aspx?Brand=" + brand + "", false);
                        }
                        else
                        {
                            Response.Redirect("~/compare.aspx", false);
                        }
                    }
                }
                else
                {
                    mpe.Show();
                    lblpopup.Text = "You are already added this product!";
                    //Response.Write("<script>alert('You are already added this product!')</script>");
                }
            }
            else
            {
                mpe.Show();
                lblpopup.Text = "You can compare maximum 4 products!";
                //Response.Write("<script>alert('You can compare maximum 4 products!')</script>");
            }
        }
        catch
        { }
    }

    private void BindProductToCompare()
    {
        StringBuilder sb = new StringBuilder();
        
        try
        {
            sb.Append("<table class='compproduct' cellpadding='6' cellspacing='6'><tr>");
            DataTable dt = new DataTable();
            string compareSession = "";
            if (!string.IsNullOrEmpty(Session["compare"] as string))
            {
                compareSession = Session["compare"].ToString();
            }
            dt = local_product.getCompareProducts(compareSession);
            if (dt.Rows.Count > 0)
            {
                int rowcount = 0;
                foreach (DataRow drow in dt.Rows)
                {
                    sb.Append("<td><div class='product-item'><div class='product-image'><a href='product-detail.aspx?product=" + drow["ProductID"] + "'><img src='Upload/thumbnails/" + drow["Thumbnail"] + "' alt='' /></a></div><div class='product-item-details'><div class='product-item-name'><a href='product-detail.aspx?product=" + drow["ProductID"] + "'>" + drow["Name"] + "</a></div><div class='price-box'>Price: <i class='fa fa-inr' aria-hidden='true'></i> <span class='price'>" + drow["discoutprice"] + "</span></div></div></div></td>");
                    rowcount = rowcount + 1;
                    Session["rowcount"] = rowcount;
                }

                if (Convert.ToInt32(Session["rowcount"]) == 1)
                {
                    sb.Append("<td><img src='img/No-Product.png' alt='' /></td><td><img src='img/No-Product.png' alt='' /></td><td><img src='img/No-Product.png' alt='' /></td>");
                }
                else if (Convert.ToInt32(Session["rowcount"]) == 2)
                {
                    sb.Append("<td><img src='img/No-Product.png' alt='' /></td><td><img src='img/No-Product.png' alt='' /></td>");
                }
                else if (Convert.ToInt32(Session["rowcount"]) == 3)
                {
                    sb.Append("<td><img src='img/No-Product.png' alt='' /></td>");
                }
            }
            else
            {
                sb.Append("<td><img src='img/No-Product.png' alt='' /></td><td><img src='img/No-Product.png' alt='' /></td><td><img src='img/No-Product.png' alt='' /></td><td><img src='img/No-Product.png' alt='' /></td>");
            }
            sb.Append("</tr></table>");
            PlaceHolder1.Controls.Add(new Literal { Text = sb.ToString() });
        }
        catch
        { }
    }

    protected void linkToCompare_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt32(Session["count"]) > 2)
        {
            Response.Redirect("compare-product.aspx");
        }
        else
        {
            mpe.Show();
            lblpopup.Text = "Minimum 2 products required to compare!";
            //Response.Write("<script>alert('Minimum 2 products required to compare!')</script>");
        }
    }

    
    protected void removeCompare_Click(object sender, EventArgs e)
    {
        try
        {
            int resut = 0;
            LinkButton btn = (LinkButton)(sender);
            string productid = btn.CommandArgument;
            string comparesession = Session["compare"].ToString();
            Int32 compareid = GetCompareID(comparesession, productid);
            resut = local_product.RemoveCompare(compareid);
            if (resut != 0)
            {
                if (!string.IsNullOrEmpty(Session["compare"] as string))
                {
                    string compareSession = Session["compare"].ToString();
                }
                int compareitem = Convert.ToInt32(Session["count"]);
                compareitem = compareitem - 1;
                Session["count"] = compareitem;
            }
        }
        catch
        { }
    }


    private Int32 GetCompareID(string comsession, string productid)
    {
        DataTable dt = new DataTable();
        Int32 compareid = 0;
        try
        {
            dt = local_product.geCompareID(comsession, productid);
            if (dt.Rows.Count > 0)
            {
                compareid = Convert.ToInt32(dt.Rows[0]["CompareId"]);
            }
        }
        catch
        { }
        return compareid;
    }

}