using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using SqlAccess;
using System.Web.Services;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections;
using System.Text;
using System.Drawing;

public partial class cart : System.Web.UI.Page
{
    Catergoy local_category = new Catergoy();
    SubCategory local_subcategory = new SubCategory();
    Products local_product = new Products();
    ShoppingCart local_cart = new ShoppingCart();
    OrderDetails local_orderlist = new OrderDetails();
    WalletCls local_wallet = new WalletCls();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["cart"] = "Shoppingcart";

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

            string transactId = Convert.ToString(Session["Transid"]);
            if (Session["Transid"] != null)
            {
                BindShoppingCart(transactId);
            }
            else
            {
                mpe.Show();
                lblpopup.Text = "Your shopping cart is empty!";
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
            BintCartItems();
            //BindContry();
            //BindState();
        }
        if (Session["userid"] != null)
        {
            pnlwishlist.Visible = true;
        }
        BindLoginLogout();
        BindCategoryMenu();
        BindWalletAmount();
        BindAccessories();
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
   
    
    //Bind Cart
    //private void BindShoppingCart(string transactid)
    //{
    //    try
    //    {
    //        DataTable dt = new DataTable();
    //        dt = local_cart.getDataInShoppingCart(transactid);
    //        if (dt.Rows.Count > 0)
    //        {
    //            ddShoppingCart.DataSource = dt;
    //            ddShoppingCart.DataBind();

    //            DataTable dt2 = new DataTable();
    //            dt2 = local_cart.getTotalPriceShoppingCart(transactid);
    //            if (Convert.ToString(Session["currency"]) == "INR")
    //            {
    //                Session["total"] = dt2.Rows[0]["totalprice2"].ToString();
    //                lblTotal.Text = "INR " + Session["total"].ToString();
    //                lblTotal1.Text = "INR " + Session["total"].ToString();
    //                Bindshippingcharges();
    //            }
    //            else if (Convert.ToString(Session["currency"]) == "USD")
    //            {
    //                Session["total"] = dt2.Rows[0]["totalprice2"].ToString();
    //                decimal tot = Convert.ToDecimal(Session["total"]);
    //                decimal charge = Convert.ToDecimal(65);
    //                decimal dollar = tot / charge;
    //                dollar = Math.Round(dollar, 2);
    //                lblTotal.Text = "USD " + dollar;
    //                lblTotal1.Text = "USD " + dollar;
    //                Bindshippingcharges();
    //            }
    //        }
    //    }
    //    catch
    //    { }
    //}

    private void BindShoppingCart(string transactid)
    {
        try
        {
            DataTable dt = new DataTable();
            dt = local_cart.getDataInShoppingCart(transactid);
            if (dt.Rows.Count > 0)
            {
                ddShoppingCart.DataSource = dt;
                ddShoppingCart.DataBind();

                string carid = Convert.ToString(dt.Rows[0]["CartId"]);

                Session["productid"] = Convert.ToString(dt.Rows[0]["ProductId"]);

                DataTable dt2 = new DataTable();
                dt2 = local_cart.getTotalPriceShoppingCart(transactid);

                if (dt2.Rows.Count > 0)
                {
                    if (Session["couponcode"] == null)
                    {
                        Session["total"] = dt2.Rows[0]["totalprice2"].ToString();
                        lblTotal1.Text = Session["total"].ToString();
                        lblTotal.Text = Session["total"].ToString();

                        //local_cart.UpdateCartItemByCoupon(carid, Convert.ToDecimal(Session["total"]));
                    }
                    else
                    {
                        if (Session["coupon"] != null)
                        {
                            decimal totalcoup = Convert.ToDecimal(Session["coupon"].ToString());
                            decimal subtamount = Convert.ToDecimal(Session["total"].ToString());
                            decimal totalprice_session = Convert.ToDecimal(Session["total"]);
                            decimal couponprice = Convert.ToDecimal(Session["couponprice"]);
                            if (couponprice != 0 && couponprice <= totalprice_session)
                            {
                                decimal finaldiscount = subtamount * totalcoup / 100;
                                decimal finaltotal = subtamount - finaldiscount;
                                Session["total"] = finaltotal;
                                lblTotal1.Text = Session["total"].ToString();
                                lblTotal.Text = Session["total"].ToString();

                                Session["coupon"] = null;
                                Session["couponprice"] = null;
                                Session["couponcode"] = null;

                                //local_cart.UpdateCartItemByCoupon(carid, finaltotal);

                                lblApplyCoupon.Text = "Coupon Apply Successfully & Discount Price is " + (finaldiscount) + "Rs";
                            }
                        }
                        else
                        {
                            lblTotal1.Text = Session["total"].ToString();
                            lblTotal.Text = Session["total"].ToString();

                            lblApplyCoupon.Text = "";
                            
                        }
                    }
                    Bindshippingcharges();
                }
            }
        }
        catch
        { }
    }

    private void Bindshippingcharges()
    {
        try
        {
            DataTable dt = new DataTable();
            string trasid = Session["Transid"].ToString();
            dt = local_cart.getShippingCharges(trasid);
            if (dt.Rows.Count > 0)
            {
                decimal shippingcharges = 0;
                for (int p = 0; p < dt.Rows.Count; p++)
                {
                    if (Session["currency"].ToString() == "INR")
                    {
                        decimal shipping = Convert.ToDecimal(dt.Rows[p]["ShippingCharges"]);
                        decimal quantity = Convert.ToDecimal(dt.Rows[p]["Quantity"]);
                        shippingcharges += shipping * quantity;
                    }
                    else
                    {
                        decimal shipping = Convert.ToDecimal(dt.Rows[p]["usdshipping"]);
                        decimal quantity = Convert.ToDecimal(dt.Rows[p]["Quantity"]);
                        shippingcharges += shipping * quantity;
                    }
                }
                string total1 = lblTotal.Text;
                string[] temp = total1.Split(' ');
                string currency = temp[0];
                // decimal total = Convert.ToDecimal(temp[1]);
                decimal total = Convert.ToDecimal(total1);
                decimal amount = total + shippingcharges;
                if (shippingcharges >= 0)
                {
                    //lblShippingCost.Text = currency + " " + Convert.ToString(shippingcharges);
                    lblShippingCost.Text = Convert.ToString(shippingcharges);

                }
                else
                {
                    lblShippingCost.Text = "Free";
                }

                //lblTotal1.Text = currency + " " + Convert.ToString(amount);
                lblTotal1.Text = Convert.ToString(amount);

                //Update final Coupon Price 
                string transactid = Session["Transid"].ToString();
                DataTable dt1 = new DataTable();
                dt1 = local_cart.getDataInShoppingCart(transactid);
                string carid = Convert.ToString(dt1.Rows[0]["CartId"]);
                
                local_cart.UpdateCartItemByCoupon(carid, total, Convert.ToInt32(lblShippingCost.Text));
            }
        }
        catch(Exception ex)
        { throw new Exception(ex.Message); }
    }

    private void BindSubTotal(string Id)
    {
        try
        {
            DataTable dt = new DataTable();
            string transid = Session["Transid"].ToString();
            dt = local_cart.getSubTotalShoppingCart(transid, Id);
            if (dt.Rows.Count > 0)
            {
                int totalprice = Convert.ToInt32(dt.Rows[0]["TotalPrice"].ToString());
                int total = Convert.ToInt32(Session["total"].ToString());
                total = total - totalprice;
                Session["total"] = total;
            }
        }
        catch
        { }
    }

    protected void dlqty_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DropDownList ddlist = (DropDownList)(sender);
            RepeaterItem Item = (RepeaterItem)ddlist.NamingContainer;
            Label price = (Label)Item.FindControl("lblprice1");
            //Label sprice = (Label)Item.FindControl("lblsprice");

            decimal price2 = Convert.ToDecimal(price.Text);
            //Int32 sprice2 = Convert.ToInt32(sprice.Text);
            string cartid = ddlist.DataValueField.ToString();

            Int32 qty = Convert.ToInt32(ddlist.Text);
            decimal totalprice = price2 * qty;
            //int totalsprice = sprice2 * 2;

            //local_cart.UpdateCartItem(qty, totalprice, totalsprice, cartid);
            local_cart.UpdateCartItem(qty, totalprice, cartid);
            string transactId = Convert.ToString(Session["Transid"]);
            BindShoppingCart(transactId);
            BintCartItemsPrice();
        }
        catch(Exception ex)
        { throw new Exception(ex.Message); }
    }


    //protected void dlqty_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        DropDownList ddlist = (DropDownList)(sender);
    //        RepeaterItem Item = (RepeaterItem)ddlist.NamingContainer;
    //        Label price = (Label)Item.FindControl("lblprice1");
    //        decimal price2 = Convert.ToDecimal(price.Text);
    //        string cartid = ddlist.DataValueField.ToString();
    //        Int32 qty = Convert.ToInt32(ddlist.Text);

    //        string productId = (Session["productid"].ToString());

    //        DataTable dt = new DataTable();
    //        dt = local_cart.getTotalQuantityByvname(productId);
    //        Int32 totalqty = Convert.ToInt32(dt.Rows[0]["itmequantity"]);


    //        if (totalqty >= qty)
    //        {   decimal totalprice = price2 * qty;
    //            local_cart.UpdateCartItem(qty, totalprice, cartid);
    //            string transactId = Convert.ToString(Session["Transid"]);
    //            BindShoppingCart(transactId);
    //            BintCartItemsPrice();
    //        }

    //        else
    //        {
    //            mpe.Show();
    //            lblpopup.Text = "Please check product availability then add.";
    //        }
    //    }
    //    catch(Exception ex)
    //    { throw new Exception(ex.Message); }
    //}

    protected void ddCurrency_SelectedIndexChanged(object sender, EventArgs e)
    {
        //string transactId = Convert.ToString(Session["Transid"]);
        //if (ddCurrency.SelectedIndex == 0)
        //{
        //    Session["currency"] = "INR";
        //    if (Session["Transid"] != null)
        //    {
        //        BindShoppingCart(transactId);
        //    }
        //}
        //else if (ddCurrency.SelectedIndex == 1)
        //{
        //    Session["currency"] = "USD";
        //    if (Session["Transid"] != null)
        //    {
        //        BindShoppingCart(transactId);
        //    }
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

    
    protected void ddShoppingCart_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Delete")
            {
                string id = e.CommandArgument.ToString();
                string trasid = Session["Transid"].ToString();
                int result = 0;
                result = local_cart.DeleteCartItem(trasid, id);

                if (result > 0)
                {
                    BindSubTotal(id);
                    Response.Redirect("~/cart.aspx", false);
                }
            }
        }
        catch
        { }
    }

    protected void ddShoppingCart_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (Session["currency"].ToString() == "INR")
        {
            Literal lbltotal = (Literal)e.Item.FindControl("lbltotal");
            Literal lblusdtotal = (Literal)e.Item.FindControl("lblusdtotal");
            Literal lblprice = (Literal)e.Item.FindControl("lblprice");
            Literal lblusdprice = (Literal)e.Item.FindControl("lblusdprice");
            lbltotal.Visible = true;
            lblprice.Visible = true;

            lblusdprice.Visible = false;
            lblusdtotal.Visible = false;

            lbltotal.Text = "INR " + lbltotal.Text;
            lblprice.Text = "INR " + lblprice.Text;
        }
        else
        {
            Literal lbltotal = (Literal)e.Item.FindControl("lbltotal");
            Literal lblusdtotal = (Literal)e.Item.FindControl("lblusdtotal");
            Literal lblprice = (Literal)e.Item.FindControl("lblprice");
            Literal lblusdprice = (Literal)e.Item.FindControl("lblusdprice");
            lbltotal.Visible = false;
            lblprice.Visible = false;

            lblusdprice.Visible = true;
            lblusdtotal.Visible = true;

            lblusdprice.Text = "USD " + lblusdprice.Text;
            lblusdtotal.Text = "USD " + lblusdtotal.Text;
        }
    }

    //
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
            }
        }
        catch
        { }
    }

    protected void lnkCheckout_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/checkout.aspx", false);
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


    //private void BindContry()
    //{
    //    DataTable dt = new DataTable();
    //    try
    //    {
    //        dt = local_cart.getCountry();
    //        if (dt.Rows.Count > 0)
    //        {
    //            ddCountry.DataSource = dt;
    //            ddCountry.DataTextField = "ContryName";
    //            ddCountry.DataValueField = "CountryCode";
    //            ddCountry.DataBind();
    //            ddCountry.Items.Insert(0, "Select Country");
    //            ddCountry.SelectedIndex = 103;

    //        }
    //    }
    //    catch
    //    { }
    //}
    //private void BindState()
    //{
    //    DataTable dt = new DataTable();
    //    try
    //    {
    //        dt = local_cart.getState();
    //        if (dt.Rows.Count > 0)
    //        {
    //            ddState.DataSource = dt;
    //            ddState.DataTextField = "StateName";
    //            ddState.DataValueField = "StateCode";
    //            ddState.DataBind();
    //            ddState.Items.Insert(0, "Select State");
    //            ddState.SelectedIndex = 9;
    //        }
    //    }
    //    catch
    //    { }
    //}

    //protected void btnCheckDelivery_Click(object sender, EventArgs e)
    //{
    //    DataTable dt = new DataTable();
    //    try
    //    {
    //        string country = ddCountry.SelectedValue;
    //        string state = ddState.SelectedValue;
    //        string postcode = txtPostcodezip.Text;

    //        if (local_product.IfDeliveryExists(postcode, state, country) == "T")
    //        {
    //            mpe.Show();
    //            lblpopup.Text = "Product Delivery Available!";
    //        }
    //        else
    //        {
    //            mpe.Show();
    //            lblpopup.Text = "Product Delivery Not Available!";
    //        }
    //    }
    //    catch
    //    { }
    //}

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

    protected void btnCoupon_Click(object sender, EventArgs e)
    {
        if (txtcoupon.Text != "")
        {
            if (local_product.IfCouponExists(txtcoupon.Text.Trim()) == "T")
            {
                DataTable dt = new DataTable();
                try
                {
                    dt = local_product.getCouponDetails(txtcoupon.Text.Trim());
                    if (dt.Rows.Count > 0)
                    {
                        if (Convert.ToDecimal(Session["total"]) == Convert.ToDecimal(dt.Rows[0]["price"]) || Convert.ToDecimal(dt.Rows[0]["price"]) < Convert.ToDecimal(Session["total"].ToString()))
                        {
                            Session["coupon"] = dt.Rows[0]["discount"];
                            Session["couponcode"] = txtcoupon.Text;
                            Session["couponprice"] = Convert.ToDecimal(dt.Rows[0]["price"]);
                            Int32 couponid = Convert.ToInt32(dt.Rows[0]["cid"]);

                            string transactId = Convert.ToString(Session["Transid"]);
                            if (Session["Transid"] != null)
                            {
                                BindShoppingCart(transactId);
                            }

                            bool active = false;
                            //local_product.UpdateCouponStatus(couponid, active);
                        }
                        else
                        {
                            lblCoupon.Text = "Coupan is not applicable this amount";
                        }
                    }
                    else
                    {
                        lblCoupon.Text = "Coupan code is not valid!";
                    }
                }
                catch
                { }
            }
        }
        else
        {
            lblCoupon.ForeColor = Color.Red;
            lblCoupon.Text = "Please enter coupon-code?";
        }
    }
}