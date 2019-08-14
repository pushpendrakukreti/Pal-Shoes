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
using System.Configuration;

public partial class product_detail : System.Web.UI.Page
{
    Catergoy local_category = new Catergoy();
    SubCategory local_subcategory = new SubCategory();
    Products local_product = new Products();
    ShoppingCart local_cart = new ShoppingCart();
    OrderDetails local_orderlist = new OrderDetails();
    WalletCls local_wallet = new WalletCls();

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
           
            if (!string.IsNullOrEmpty(Session["Tid"] as string))
            {
                BindWishlistNo();
            }
            if (!string.IsNullOrEmpty(Session["compare"] as string))
            {
                BindCompareItemsNo();
            }
            string productid = Request.QueryString["product"];
            if (productid != null)
            {
                BindProductDetails(productid);
                
                BindTrendingProducts(productid);
                BindReviews(productid);

                BindVariantNames(productid);
            }
            //lblInStock.ForeColor = Color.OrangeRed;
            //lblInStock.Text = "Check Product Availability By Varient";
            //BindFlavour();
            BintCartItems();
			
			string page = Request.QueryString["product"];
            DataTable dtMeta = this.GetData(page);

            //Add Page Title
            this.Page.Title = dtMeta.Rows[0]["Producttitle"].ToString();

            //Add Keywords Meta Tag
            //HtmlMeta keywords = new HtmlMeta();
            //keywords.HttpEquiv = "Productkeywords";
            //keywords.Name = "Productkeywords";
            //keywords.Content = dtMeta.Rows[0]["Productkeywords"].ToString();
            //this.Page.Header.Controls.Add(keywords);

            //Add Description Meta Tag
            //HtmlMeta description = new HtmlMeta();
            //description.HttpEquiv = "ProductshortDes";
            //description.Name = "ProductshortDes";
            //description.Content = dtMeta.Rows[0]["ProductshortDes"].ToString();
            //this.Page.Header.Controls.Add(description);

            //******************************************************

            //Show data by id
            //url.Attributes["name"] = "url";
            //url.Attributes["content"] = dtMeta.Rows[0]["Producturl"].ToString();

            //key.Attributes["name"] = "keywords";
            //key.Attributes["content"] = dtMeta.Rows[0]["Productkeywords"].ToString();

            //des.Attributes["name"] = "description";
            //des.Attributes["content"] = dtMeta.Rows[0]["ProductshortDes"].ToString();

            //page.MetaKeywords = dtMeta.Rows[0]["Productkeywords"].ToString();

            //******************************************************

            //Show data without id
            Page.MetaKeywords = dtMeta.Rows[0]["Productkeywords"].ToString();
            Page.MetaDescription = dtMeta.Rows[0]["ProductshortDes"].ToString();
        }
        string product = Request.QueryString["product"];
        BindDetailsImage(product);
        BindCategoryMenu();
        BindLoginLogout();
        BindWalletAmount();
        BindAccessories();
        BinMailtoF(product);

        if (Session["userid"] != null)
        {
            //pnlwishlist.Visible = true;
        }
    }

    private DataTable GetData(string page)
    {
        string query = "Select Producturl, Producttitle, Productkeywords, ProductshortDes from t_Product WHERE ProductId = LOWER(@Page)";
        string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand(query))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Page", page);
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    return dt;
                }
            }
        }
    }

	
    private void BindProductDetails(string productid)
    {
        try
        {
            DataTable dt = new DataTable();
            dt = local_product.getProductDescriptionById(productid);
            if (dt.Rows.Count > 0)
            {
                ViewState["productID"] = Convert.ToString(dt.Rows[0]["ProductId"]);
                lblProductName.Text = Convert.ToString(dt.Rows[0]["Name"]);
                //lblProductName.Text = Convert.ToString(dt.Rows[0]["Name"]) + " " + Convert.ToString(dt.Rows[0]["Capacity"]) + " " + Convert.ToString(dt.Rows[0]["flavour"]);
               
                lblRegularPrice.Text = Convert.ToString(dt.Rows[0]["price"]);
                lblSpecialPrice.Text = Convert.ToString(dt.Rows[0]["itemprice"]);
                lblDiscount.ForeColor = System.Drawing.Color.Red;
                lblDiscount.Text = Convert.ToString(dt.Rows[0]["Discount"])+"%";
                int discounts=Convert.ToInt32(dt.Rows[0]["Discount"]);
                if (discounts == 0)
                { panelprice.Visible = false; }

               // lblQuantity.Text = Convert.ToString(dt.Rows[0]["Quantity"]);
                lblSKUCode.Text = Convert.ToString(dt.Rows[0]["SKUCode"]);
                lblBrand.Text = Convert.ToString(dt.Rows[0]["Brand"]);

                lblDescriptionHead.Text = Convert.ToString(dt.Rows[0]["Name"]);
                lblDescriptionDetail.Text = Convert.ToString(dt.Rows[0]["Description"]);

                Session["VariantnameQuantity"] = Convert.ToString(dt.Rows[0]["Quantity"]);
                string variantname = Convert.ToString(dt.Rows[0]["Variantname"]);

                //Declare a string arraylist and join the string with variantname data coming from database
                string[] allStr = new String[] { "--Select Product Size -- ", variantname };
                string str = String.Join(", ", allStr);

                //Declare a arraylist for getting comma separated string
                ArrayList arr = new ArrayList();
                //check wether the re is comma in the end of the string
                if (str.Trim().EndsWith(","))
                {
                    str = str.Substring(0, str.Length - 1);
                }
                //split the comma separated string into arraylist
                arr.AddRange(str.Split(','));

                //loop through the arraylist items & add the item to Dropdownlist
                for (int i = 0; i < arr.Count; i++)
                {
                    ddVariantNames.Items.Insert(i, new ListItem(arr[i].ToString(), (i + 1).ToString()));

                }
            }
        }
        catch(Exception ex)
        { throw new Exception(ex.Message); }
    }

    private string GetVquantityByVname(string variantname)
    {
        DataTable dt = new DataTable();
        string vquantity = "";
        try
        {
            string productid = Request.QueryString["product"];
            dt = local_product.getVquantityByVnamee(productid, variantname); ;
            if (dt.Rows.Count > 0)
            {
                vquantity = Convert.ToString(dt.Rows[0]["Quantity"]);
            }
        }

        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return vquantity;
    }

    protected void ddVariantNames_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddVariantNames.SelectedIndex > 0)
        {
            string variantname = Convert.ToString(ddVariantNames.SelectedItem);
            Int32 variantindex = Convert.ToInt32(ddVariantNames.SelectedIndex);

            //We add a string on 0 index now the selected index should be decrement by 1 Declare a arraylist for getting comma separated string
            Int32 incrementvariant = variantindex - 1;

            string str = Session["VariantnameQuantity"].ToString();
            //Declare a arraylist for getting comma separated string
            ArrayList arr = new ArrayList();
            //check wether the re is comma in the end of the string
            if (str.Trim().EndsWith(","))
            {
                str = str.Substring(0, str.Length - 1);
            }
            //split the comma separated string into arraylist
            arr.AddRange(str.Split(','));

            lblVariantQuantity.Text = arr[incrementvariant].ToString();

            //lblVariantQuantity.Text = GetVquantityByVname(variantname);
            Session["VariantName"] = variantname;
            // Session["VariantQuantity"] = lblVariantQuantity.Text;
            lblInStock.Text = "In Stock";
            lblInStock.ForeColor = System.Drawing.Color.Green;
        }
        else
        {
            lblInStock.ForeColor = System.Drawing.Color.Red;
            lblInStock.Text = "Not In Stock";
            ddVariantNames.Items.Clear();
        }
    }

    private void BindVariantNames(string productid)
    {
        //try
        //{
        //    DataTable dt = new DataTable();
        //    dt = local_product.getVariantforDropd(productid);
        //    if (dt.Rows.Count > 0)
        //    {
        //        ddVariantNames.DataSource = dt;
        //        ddVariantNames.DataTextField = "Variantname";
        //        ddVariantNames.DataValueField = "QuantityId";
        //        ddVariantNames.DataBind();
               
        //        ddVariantNames.Items.Insert(0, "--Select Variant Name--");
        //    }
        //}
        //catch
        //{ }
    }
 
    private void BindDetailsImage(string productid)
    {
        try
        {
            DataTable dt = new DataTable();
            dt = local_product.getProductDescriptionImg(productid);
            if (dt.Rows.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("<img style='border: 1px solid rgb(232, 232, 230); position: absolute; width:400px; height:400px;' id='zoom_03' src='Upload/thumbnails/" + dt.Rows[0]["Thumbnail"].ToString() + "' data-zoom-image='Upload/images/" + dt.Rows[0]["Image"].ToString() + "'>");

                sb.Append("<div id='gallery_01'>");

                sb.Append("<a href='#' class='elevatezoom-gallery active' data-update='' data-image='Upload/thumbnails/" + dt.Rows[0]["Thumbnail"].ToString() + "' data-zoom-image='Upload/images/" + dt.Rows[0]["Image"].ToString() + "'>");
                sb.Append("<img src='Upload/small/" + dt.Rows[0]["Thumbnail"].ToString() + "' width='95' alt=''></a>");

                sb.Append("<a href='#' class='elevatezoom-gallery' data-image='Upload/thumbnails/" + dt.Rows[0]["BackImage"].ToString() + "' data-zoom-image='Upload/images/" + dt.Rows[0]["BackImage"].ToString() + "'>");
                sb.Append("<img src='Upload/small/" + dt.Rows[0]["BackImage"].ToString() + "' width='95' alt=''></a>");

                sb.Append("<a href='#' class='elevatezoom-gallery' data-image='Upload/thumbnails/" + dt.Rows[0]["LeftImage"].ToString() + "' data-zoom-image='Upload/images/" + dt.Rows[0]["LeftImage"].ToString() + "'>");
                sb.Append("<img src='Upload/small/" + dt.Rows[0]["LeftImage"].ToString() + "' width='95' alt=''></a>");

                sb.Append("<a href='#' class='elevatezoom-gallery' data-image='Upload/thumbnails/" + dt.Rows[0]["RightImage"].ToString() + "' data-zoom-image='Upload/images/" + dt.Rows[0]["RightImage"].ToString() + "'>");
                sb.Append("<img src='Upload/small/" + dt.Rows[0]["RightImage"].ToString() + "' width='95' alt=''></a>");

                sb.Append("</div>");
                PlaceHolder1.Controls.Add(new Literal { Text = sb.ToString() });

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

    private void BindTrendingProducts(string productid)
    {
        try
        {
            DataTable dt = new DataTable();
            dt = local_product.getRelatedProdtuctInPD(productid);
            if (dt.Rows.Count > 0)
            {
                rpRelatedproducts.DataSource = dt;
                rpRelatedproducts.DataBind();
            }
        }
        catch
        { }
    }


    //private Int32 GetLastProductQuantityById(string productId)
    //{
    //    DataTable dt = new DataTable();
    //    Int32 quantity = 0;
    //    try
    //    {
    //        dt = local_cart.getproductQuantityById(Convert.ToInt32(productId));
    //        if (dt.Rows.Count > 0)
    //        {
    //            quantity = Convert.ToInt32(dt.Rows[0]["Quantity"]);
    //        }
    //    }

    //    catch (Exception ex)
    //    {
    //        throw new Exception(ex.Message);
    //    }
    //    return quantity;
    //}


    //Add to Cart
    protected void btnAddToCart_Click(object sender, EventArgs e)
    {

        Int32 quantity = 1;
        if (qty.Value != null)
        {
            quantity = Convert.ToInt32(qty.Value);
        }

        if (lblInStock.Text == "In Stock")
        {
            //Int32 oldquantity = Convert.ToInt32(GetVquantityByVname(Session["VariantName"].ToString()));
            Int32 oldquantity = Convert.ToInt32(lblVariantQuantity.Text);

        if (quantity <= oldquantity)
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
                    string product = ViewState["productID"].ToString();

                    string variantname = Session["VariantName"].ToString();

                    //string variantname = Convert.ToString(dt.Rows[0]["Variantname"]);

                    dt = local_cart.getDataFromProdtuctByVariant(product, variantname);
                    if (dt.Rows.Count > 0)
                    {
                        string transid = Session["Transid"].ToString();
                        Int32 productid = Convert.ToInt32(dt.Rows[0]["ProductId"]);
                        string name = Convert.ToString(dt.Rows[0]["Name"]);
                        decimal price = Convert.ToDecimal(dt.Rows[0]["Price"]);
                        string capacity = Convert.ToString(dt.Rows[0]["Capacity"]);
                        Int32 discount = Convert.ToInt32(dt.Rows[0]["discountprice"]);
                        string thumbnail = Convert.ToString(dt.Rows[0]["Thumbnail"]);

                        decimal totalprice = Convert.ToDecimal(dt.Rows[0]["totalprice"]);
                        totalprice = totalprice * quantity;
                        Int32 shippingcharge = Convert.ToInt32(dt.Rows[0]["ShippingCharges"].ToString());
                        //int flavour = Convert.ToInt32(dt.Rows[0]["Producttype"].ToString());
                        int flavour = 0;
                        Int32 userid = Convert.ToInt32(Session["userid"]);

                        //string variantname = Convert.ToString(dt.Rows[0]["Variantname"]);
                        //Int32 variantquantity = Convert.ToInt32(dt.Rows[0]["Quantity"]);
                        Int32 variantquantity = oldquantity;

                        int result = 0;
                        result = local_cart.InsertIntoShoppingCart(transid, productid, price, variantname, variantquantity, quantity, discount, totalprice, shippingcharge, name, thumbnail, capacity, userid, flavour);
                        if (result != 0)
                        {
                            ViewState["productID"] = null;
                            Response.Redirect("~/cart.aspx", false);
                        }
                    }
                }
                catch (Exception ex)
                { throw new Exception(ex.Message); }
            }

            else
            {
                mpe.Show();
                lblpopup.Text = "May Be Out Of Stock.<br />Please check product availability then add.";
                
            }

        }

        else
        {
            lblInStock.ForeColor = Color.Red;
            lblInStock.Text = "Please Select product variant?";
        }

    }

    //Products add without quantity check 
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

                Int32 shippingcharges = Convert.ToInt32(dt.Rows[0]["Modelno"].ToString());
                int flavour = Convert.ToInt32(dt.Rows[0]["Producttype"].ToString());

                Int32 userid = Convert.ToInt32(Session["userid"]);
                int result = 0;
                result = local_cart.InsertIntoShoppingCart(transid, productid, price, variantname, variantquantity, quantity, discount, totalprice, shippingcharges, name, thumbnail, capacity, userid, flavour);
                if (result != 0)
                {
                    Response.Redirect("~/cart.aspx", false);
                }
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
                string product = ViewState["productID"].ToString();
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
            string product = ViewState["productID"].ToString();
            Int32 ProductId = Convert.ToInt32(product);
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
                        Response.Redirect("~/product-detail.aspx?product=" + product, false);
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

    //protected void ddFlavour_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    DataTable dt = new DataTable();
    //    try
    //    {
    //        if (ddFlavour.SelectedIndex > 0)
    //        {
    //            string productid = Request.QueryString["product"];
    //            string flavour = ddFlavour.SelectedValue;
              //  dt = local_product.getToCheckProductInStock(productid, flavour);
    //            if (dt.Rows.Count > 0)
    //            {
    //                lblInStock.Text = "In Stock";
    //                lblInStock.ForeColor = System.Drawing.Color.Green;
    //            }
    //            else
    //            {
    //                lblInStock.ForeColor = System.Drawing.Color.Red;
    //                lblInStock.Text = "Not In Stock";
    //            }
    //        }
    //        else
    //        {
    //            lblInStock.ForeColor = System.Drawing.Color.Red;
    //            lblInStock.Text = "Not In Stock";
    //        }
    //    }
    //    catch
    //    { }
    //}


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

    private void BinMailtoF(string productid)
    {
        DataTable dt = new DataTable();
        try
        {
            dt = local_product.getDataToMail(productid);
            if (dt.Rows.Count > 0)
            {
                string mailfreind = Convert.ToString(dt.Rows[0]["Name"]) + " " + Convert.ToString(dt.Rows[0]["Capacity"]) + " " + Convert.ToString(dt.Rows[0]["flavour"]);
                mailto.HRef = "mailto:?subject=" + mailfreind + "";
            }
        }
        catch
        { }
    }

    protected void btnReview_Click(object sender, EventArgs e)
    {
        try
        {
            string productid=Request.QueryString["product"];
            string name = txtName.Text;
            string email = txtEmail.Text;
            string review = txtMessage.Text;
            int rating = 3;
            bool active = false;
            int result = 0;
            result = local_product.AddProductReview(productid, rating, name, email, review, active);
            if (result != 0)
            {
                mpe.Show();
                lblpopup.Text = "Product Review submited,<br/> after admin examine it will be show.";
                txtName.Text = "";
                txtEmail.Text = "";
                txtMessage.Text = "";
            }
        }
        catch
        { }
    }

    private void BindReviews(string productid)
    {
        try
        {
            StringBuilder sb = new StringBuilder();
            DataTable dt = new DataTable();
            dt = local_product.getProductReviewDisplay(productid);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sb.Append("<li><div class='comment-user'> <img src='images/comment-user.jpg' alt=''/> </div><div class='comment-detail'>");
                    sb.Append("<div class='user-name'>" + dt.Rows[i]["Name"].ToString() + "</div><div class='post-info'>");
                    sb.Append("<ul><li>" + dt.Rows[i]["ReviewDate"].ToString() + "</li></ul></div>");
                    sb.Append("<p>" + dt.Rows[i]["Review"].ToString() + "</p></div></li>");
                }
                PlaceHolder2.Controls.Add(new Literal { Text = sb.ToString() });
            }
            DataTable dt1 = new DataTable();
            dt1 = local_product.getProductReviewCount(productid);
            if (dt1.Rows.Count > 0)
            {
                lblcomments.Text = dt1.Rows[0]["comment"].ToString();
            }
        }
        catch
        { }
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
        string emailid = txtemailSubs.Text;
        string date = DateTime.Now.ToString("dd/MM/yyyy");
        if (emailid != "")
        {
            int success = 0;
            success = local_product.AddNewSubscribe(emailid, date);
            if (success != 0)
            {
                txtemailSubs.Text = "";
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