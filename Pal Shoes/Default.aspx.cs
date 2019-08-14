using System;
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
public partial class _Default : System.Web.UI.Page
{
    Catergoy local_category = new Catergoy();
    SubCategory local_subcategory = new SubCategory();
    Products local_product = new Products();
    ShoppingCart local_cart = new ShoppingCart();
    OrderDetails local_orderlist = new OrderDetails();
    WalletCls local_wallet = new WalletCls();
    Videos local_video = new Videos();

    protected void Page_Load(object sender, EventArgs e)
    {
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
            if (!string.IsNullOrEmpty(Session["Transid"] as string))
            {
                BintCartItemsPrice();
            }
            BintCartItems();
            BindNewProducts();
            BindBestSellersProducts();
            BindTrendingproducts();
            //BindFeaturedProducts();
            BindBrands();
            BindAccessories();
            bindVideoUrl();
        }
        BindCategoryMenu();
        BindLoginLogout();
        // BindWalletAmount();

        if (!string.IsNullOrEmpty(Session["Tid"] as string))
        {
            BindWishlistNo();
        }
        if (!string.IsNullOrEmpty(Session["compare"] as string))
        {
            BindCompareItemsNo();
        }
        if (Session["userid"] != null)
        {
           // pnlwishlist.Visible = true;
        }
        Session["Newproduct"] = null;
    }

    private void BindNewProducts()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = local_product.getNewProdtucts();
            if (dt.Rows.Count > 0)
            {
                rpNewproducts.DataSource = dt;
                rpNewproducts.DataBind();
            }
        }
        catch
        { }
    }
    private void BindBestSellersProducts()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = local_product.getBestSellersProducts();
            if (dt.Rows.Count > 0)
            {
                rpBestSellersProducts.DataSource = dt;
                rpBestSellersProducts.DataBind();
            }
        }
        catch
        { }
    }
    private void BindTrendingproducts()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = local_product.getNewProducts();
            if (dt.Rows.Count > 0)
            {
                rpTrendingproducts.DataSource = dt;
                rpTrendingproducts.DataBind();
            }
        }
        catch
        { }
    }
    private void BindFeaturedProducts()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = local_product.getFeaturedProdtucts();
            if (dt.Rows.Count > 0)
            {
                //rpFeaturedproducts.DataSource = dt;
                //rpFeaturedproducts.DataBind();
            }
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
                Int32 discount = Convert.ToInt32(dt.Rows[0]["discountprice"]);
                string capacity = Convert.ToString(dt.Rows[0]["Capacity"]);
                decimal totalprice = Convert.ToDecimal(dt.Rows[0]["totalprice"]);
                string thumbnail = Convert.ToString(dt.Rows[0]["Thumbnail"]);

                Int32 quantity = 1;
                string variantname = Convert.ToString(dt.Rows[0]["Variantname"]);
                Int32 variantquantity = Convert.ToInt32(dt.Rows[0]["Quantity"]);

                Int32 shippingcharges = Convert.ToInt32(dt.Rows[0]["ShippingCharges"].ToString());
                //int flavour = Convert.ToInt32(dt.Rows[0]["Producttype"].ToString());
                int flavour = 0;

                Int32 userid = Convert.ToInt32(Session["userid"]);
                int result = 0;
                result = local_cart.InsertIntoShoppingCart(transid, productid, price, variantname, variantquantity, quantity, discount, totalprice, shippingcharges, name, thumbnail, capacity, userid, flavour);
                if (result != 0)
                {
                    Response.Redirect("~/default.aspx", false);
                }
            }
        }
        catch
        { }
    }


    protected void linkAddtoCart2_Click(object sender, EventArgs e)
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
                Int32 discount = Convert.ToInt32(dt.Rows[0]["discountprice"]);
                string capacity = Convert.ToString(dt.Rows[0]["Capacity"]);
                decimal totalprice = Convert.ToDecimal(dt.Rows[0]["totalprice"]);
                string thumbnail = Convert.ToString(dt.Rows[0]["Thumbnail"]);

                Int32 quantity = 1;
                string variantname = Convert.ToString(dt.Rows[0]["Variantname"]);
                Int32 variantquantity = Convert.ToInt32(dt.Rows[0]["Quantity"]);

                Int32 shippingcharges = Convert.ToInt32(dt.Rows[0]["ShippingCharges"].ToString());

                //int flavour = Convert.ToInt32(dt.Rows[0]["Producttype"].ToString());
                int flavour = 0;
                Int32 userid = Convert.ToInt32(Session["userid"]);
                int result = 0;
                result = local_cart.InsertIntoShoppingCart(transid, productid, price, variantname, variantquantity, quantity, discount, totalprice, shippingcharges, name, thumbnail, capacity, userid, flavour);
                if (result != 0)
                {
                    Response.Redirect("~/default.aspx", false);
                }
            }
        }
        catch
        { }
    }


    protected void linkAddtoCart3_Click(object sender, EventArgs e)
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
                Int32 discount = Convert.ToInt32(dt.Rows[0]["discountprice"]);
                string capacity = Convert.ToString(dt.Rows[0]["Capacity"]);
                decimal totalprice = Convert.ToDecimal(dt.Rows[0]["totalprice"]);
                string thumbnail = Convert.ToString(dt.Rows[0]["Thumbnail"]);

                Int32 quantity = 1;
                string variantname = Convert.ToString(dt.Rows[0]["Variantname"]);
                Int32 variantquantity = Convert.ToInt32(dt.Rows[0]["Quantity"]);

                Int32 shippingcharges = Convert.ToInt32(dt.Rows[0]["ShippingCharges"].ToString());

                //int flavour = Convert.ToInt32(dt.Rows[0]["Producttype"].ToString());
                int flavour = 0;

                Int32 userid = Convert.ToInt32(Session["userid"]);
                int result = 0;
                result = local_cart.InsertIntoShoppingCart(transid, productid, price, variantname, variantquantity, quantity, discount, totalprice, shippingcharges, name, thumbnail, capacity, userid, flavour);
                if (result != 0)
                {
                    Response.Redirect("~/default.aspx", false);
                }
            }
        }
        catch
        { }
    }

    protected void linkAddtoCart4_Click(object sender, EventArgs e)
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
                Int32 discount = Convert.ToInt32(dt.Rows[0]["discountprice"]);
                string capacity = Convert.ToString(dt.Rows[0]["Capacity"]);
                decimal totalprice = Convert.ToDecimal(dt.Rows[0]["totalprice"]);
                string thumbnail = Convert.ToString(dt.Rows[0]["Thumbnail"]);

                Int32 quantity = 1;
                string variantname = Convert.ToString(dt.Rows[0]["Variantname"]);
                Int32 variantquantity = Convert.ToInt32(dt.Rows[0]["Quantity"]);

                Int32 shippingcharges = Convert.ToInt32(dt.Rows[0]["ShippingCharges"].ToString());

                //int flavour = Convert.ToInt32(dt.Rows[0]["Producttype"].ToString());
                int flavour = 0;

                Int32 userid = Convert.ToInt32(Session["userid"]);
                int result = 0;
                result = local_cart.InsertIntoShoppingCart(transid, productid, price, variantname, variantquantity, quantity, discount, totalprice, shippingcharges, name, thumbnail, capacity, userid, flavour);
                if (result != 0)
                {
                    Response.Redirect("~/default.aspx", false);
                }
            }
        }
        catch
        { }
    }


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
        catch(Exception ex)
        { throw new Exception(ex.Message); }
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
                //Rpwishlist.DataSource = dt;
                //Rpwishlist.DataBind();
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
                //rpCompare.DataSource = dt;
                //rpCompare.DataBind();
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
                            Response.Redirect("~/default.aspx", false);
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
    protected void lnkWishlist2_Click(object sender, EventArgs e)
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
                            Response.Redirect("~/default.aspx", false);
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
    protected void lnkWishlist3_Click(object sender, EventArgs e)
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
                            Response.Redirect("~/default.aspx", false);
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
    protected void lnkWishlist4_Click(object sender, EventArgs e)
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
                            Response.Redirect("~/default.aspx", false);
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
                        Response.Redirect("~/default.aspx", false);
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

    protected void lnkCompare2_Click(object sender, EventArgs e)
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
                        Response.Redirect("~/default.aspx", false);
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

    protected void lnkCompare3_Click(object sender, EventArgs e)
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
                        Response.Redirect("~/default.aspx", false);
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

    protected void lnkCompare4_Click(object sender, EventArgs e)
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
                        Response.Redirect("~/default.aspx", false);
                    }
                }
                else
                {
                    mpe.Show();
                    lblpopup.Text = "You are already added this product!";
                    //mpe.Show();
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



    //private void BindWalletAmount()
    //{
    //    try
    //    {
    //        DataTable dt = new DataTable();
    //        string user = Session["userid"].ToString();
    //        dt = local_wallet.getWalletIconAmount(user);
    //        if (dt.Rows.Count > 0)
    //        {
    //            rpWallet.DataSource = dt;
    //            rpWallet.DataBind();
    //        }
    //    }
    //    catch
    //    { }
    //}

    private void BindBrands()
    {
        DataTable dt = new DataTable();
        try
        {
            dt = local_category.getBrands();
            if (dt.Rows.Count > 0)
            {
                //rpBrand.DataSource = dt;
                //rpBrand.DataBind();
            }
        }
        catch
        { }
    }


    private void bindVideoUrl()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = local_video.getVideosUrl();
            if (dt.Rows.Count > 0)
            {
                rtPlayVideos.DataSource = dt;
                rtPlayVideos.DataBind();
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


    protected void Searchbtn_Click(object sender, EventArgs e)
    {
        //if (txtSearch.Text != "")
        //{
        //    string search = txtSearch.Text.Trim();
        //    Response.Redirect("search.aspx?search=" + search);
        //}
        //else
        //{

        //}
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
                    empResult.Add(dt.Rows[i]["name"].ToString());
                }
            }
        }
        catch
        { }
        return empResult;
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
}