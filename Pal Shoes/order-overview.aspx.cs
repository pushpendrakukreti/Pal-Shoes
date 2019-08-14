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

public partial class order_overview : System.Web.UI.Page
{
    Products local_product = new Products();
    ShoppingCart local_cart = new ShoppingCart();
    Catergoy local_category = new Catergoy();
    WalletCls local_wallet = new WalletCls();
    SubCategory local_subcategory = new SubCategory();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Session["username"] as string))
        {
            if (!string.IsNullOrEmpty(Session["Transid"] as string))
            {
                if (Session["checkout"] != null)
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

                        string transactId = Convert.ToString(Session["Transid"]);
                        if (Session["Transid"] != null)
                        {
                            BindOrderOverview(transactId);
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
                        BindWalletAmount();
                        BindWalletAmountOrdOvw();
                        string userID;
                        if (!string.IsNullOrEmpty(Session["userid"] as string))
                        {
                            userID = Convert.ToString(Session["userid"]);
                            BindShippingAdress(userID);
                            BindBillingAdress(userID);
                            //lblpayWalletAmt.Font.Bold = true;
                            //lblpayWalletAmt.Text = "Wallet Amount : "+ Session["walletamount"].ToString();
                        }
                        else
                        {
                            string transid = Session["Transid"].ToString();
                            BindShippingAdress2(transid);
                            BindBillingAdress2(transid);
                        }
                    }
                    //ddCurrency.Enabled = false;
                    BindLoginLogout();
                    BindCategoryMenu();
                    BindAccessories();

                    if (Session["username"].ToString() == "Guest")
                    {
                        //radWallet.Checked = false;
                        //radWallet.Enabled = false;
                        radCOD.Checked = true;
                    }
                }
                else
                {
                    Response.Redirect("~/checkout.aspx");
                }
            }
            else
            {
                Response.Redirect("~/Default.aspx");
            }
        }
        else
        {
            Response.Redirect("~/login.aspx");
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

    private void BindShippingAdress(string userid)
    {
        DataTable dt = new DataTable();
        try
        {
            dt = local_cart.getUserShippingAddress(userid);
            if (dt.Rows.Count > 0)
            {
                rpShippingAddress.DataSource = dt;
                rpShippingAddress.DataBind();
            }
        }
        catch
        { }
    }
    private void BindBillingAdress(string userid)
    {
        DataTable dt = new DataTable();
        try
        {
            dt = local_cart.getUserBillingAddress(userid);
            if (dt.Rows.Count > 0)
            {
                rpBillingAddress.DataSource = dt;
                rpBillingAddress.DataBind();
            }
        }
        catch
        { }
    }

    private void BindShippingAdress2(string transid)
    {
        DataTable dt = new DataTable();
        try
        {
            dt = local_cart.getUserShippingAddress2(transid);
            if (dt.Rows.Count > 0)
            {
                rpShippingAddress.DataSource = dt;
                rpShippingAddress.DataBind();
            }
        }
        catch
        { }
    }
    private void BindBillingAdress2(string transid)
    {
        DataTable dt = new DataTable();
        try
        {
            dt = local_cart.getUserBillingAddress2(transid);
            if (dt.Rows.Count > 0)
            {
                rpBillingAddress.DataSource = dt;
                rpBillingAddress.DataBind();
            }
        }
        catch
        { }
    }

    private void BindOrderOverview(string transactionid)
    {
        DataTable dt = new DataTable();
        dt = local_cart.getOrderOverviewItems(transactionid);
        if (dt.Rows.Count > 0)
        {
            rpOrderOverview.DataSource = dt;
            rpOrderOverview.DataBind();

            DataTable dt2 = new DataTable();
            //dt2 = local_cart.getTotalPriceShoppingCart(transactionid);
            dt2 = local_cart.getTotalPriceAfterCouponShoppingCart(transactionid);
            if (Convert.ToString(Session["currency"]) == "INR")
            {
                Session["total"] = dt2.Rows[0]["totalprice2"].ToString();
                lblSubtotal.Text = "INR " + Session["total"].ToString();
                Bindshippingcharges();
            }
            else if (Convert.ToString(Session["currency"]) == "USD")
            {
                Session["total"] = dt2.Rows[0]["totalprice2"].ToString();
                decimal tot = Convert.ToDecimal(Session["total"]);
                decimal charge = Convert.ToDecimal(65);
                decimal dollar = tot / charge;
                dollar = Math.Round(dollar, 2);
                lblSubtotal.Text = "USD " + dollar;
                Bindshippingcharges();
            }
        }
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
                string total1 = lblSubtotal.Text;
                string[] temp = total1.Split(' ');
                string currency = temp[0];
                decimal total = Convert.ToDecimal(temp[1]);
                //decimal total = Convert.ToDecimal(total1);
                decimal amount = total + shippingcharges;
                Session["TotalAmount"] = amount;
                shippingcharges = Math.Round(shippingcharges, 2);
                Session["ShippingCharges"] = shippingcharges;
                if (shippingcharges != 0)
                {
                    lblShippingcharge.Text = currency + " " + Convert.ToString(shippingcharges);
                }
                else
                {
                    lblShippingcharge.Text = "Free";
                }

                lblTotalamount.Text = currency + " " + Convert.ToString(amount);
            }
        }
        catch (Exception ex)
        { throw new Exception(ex.Message); }
    }

    protected void rpOrderOverview_ItemCommand(object source, RepeaterCommandEventArgs e)
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
                    BindOrderOverview(trasid);//rebind
                    Response.Redirect("~/order-overview.aspx", false);
                    BintCartItemsPrice();
                }
            }
        }
        catch
        { }
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

    protected void rpOrderOverview_ItemDataBound(object sender, RepeaterItemEventArgs e)
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

    protected void lnkSubmitOrder_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["ok"] == null)
            {
                string Orderno = System.DateTime.Now.ToString("yyyyMMdd");
                long orderid = ShoppingCart.OrderDtID("OrderId", "t_OrderDetail");
                string OrderdtlId = Orderno + orderid;
                Session["OrderId"] = OrderdtlId;

                DataTable dt = new DataTable();
                if (Session["username"].ToString() == "Guest")
                {
                    dt = local_cart.getShoppingCartItemsGuestOrderOvr(Session["TransId"].ToString());
                }
                else
                {
                    dt = local_cart.getShoppingCartItemsOrderOvr(Session["TransId"].ToString());

                }
                if (dt.Rows.Count > 0)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append("Your Order # : " + OrderdtlId.ToString() + " has been successfully placed." +"<br /><br />The order currently is under process and will dispatch soon. Order shipment and tracking number will be updated shortly post shipment.<br /><br />We Thank You, For your Patronage Towards Pal Shoes Shop.<br /><br /><big>Your order details are :</big><br/><br/><br/>");

                    sb.Append("<table style='width:750px; text-align:left;' cellpadding='0' cellspacing='0'><tr><td style='font-weight:normal;text-decoration:none;color:#FFFFFF;text-align:left;padding:10px 15px 10px 15px;border-top:solid 1px #D5D5D5;border-bottom:solid 1px #D5D5D5;border-left:solid 1px #D5D5D5;vertical-align:top; background:#00AFEF;' colspan='6'><center>ORDER DETAILS</center></td></tr><tr><td style='width:100px; font-weight:normal;text-decoration:none;color:#676767;text-align:left;padding:10px 15px 10px 15px;border-top:solid 1px #D5D5D5;border-bottom:solid 1px #D5D5D5;border-left:solid 1px #D5D5D5;vertical-align:top;'>Product</td><td style='width:190px; font-weight:normal;text-decoration:none;color:#676767;text-align:left;padding:10px 15px 10px 15px;border-top:solid 1px #D5D5D5;border-bottom:solid 1px #D5D5D5;border-left:solid 1px #D5D5D5;vertical-align:top;'>Product Name</td><td style='width:230px; font-weight:normal;text-decoration:none;color:#676767;text-align:left;padding:10px 15px 10px 15px;border-top:solid 1px #D5D5D5;border-bottom:solid 1px #D5D5D5;border-left:solid 1px #D5D5D5;vertical-align:top;'>Variant Name</td><td style='width:100px;font-weight:normal;text-decoration:none;color:#676767;text-align:left;padding:10px 15px 10px 15px;border-top:solid 1px #D5D5D5;border-bottom:solid 1px #D5D5D5;border-left:solid 1px #D5D5D5;vertical-align:top;'>Qty</td><td style='width:110px; font-weight:normal;text-decoration:none;color:#676767;text-align:left;padding:10px 15px 10px 15px;border-top:solid 1px #D5D5D5;border-bottom:solid 1px #D5D5D5;border-left:solid 1px #D5D5D5;border-right:solid 1px #D5D5D5;vertical-align:top;'>Amount</td></tr>");

                    Session["finalshippingcharges"] = Convert.ToString(dt.Rows[0]["FinalShippingCharges"]);

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string price = "";
                        if (Session["currency"].ToString() == "INR")
                        {
                            price = "INR " + dt.Rows[i]["inprice"].ToString();
                        }
                        else if (Session["currency"].ToString() == "USD")
                        {
                            price = "USD " + dt.Rows[i]["usdprice"].ToString();
                        }
                        string weight = dt.Rows[i]["Capacity"].ToString();
                        string flavour = dt.Rows[i]["pflavour"].ToString();

                        Session["Customername"] = Convert.ToString(dt.Rows[i]["BillFname"]) + " " + Convert.ToString(dt.Rows[i]["BillLname"]); ;
                        Session["CustEmailid"] = Convert.ToString(dt.Rows[i]["BillEmailid"]);
                        Session["BillingMobile"] = Convert.ToString(dt.Rows[i]["BillContact"]);
                        Session["BillingAddress"] = Convert.ToString(dt.Rows[i]["BillAddress"]);
                        if (Session["username"].ToString() == "Guest")
                        {
                            Session["CustIdentifyno"] = Convert.ToString(dt.Rows[i]["BillContact"]);
                        }
                        else
                        {
                            Session["CustIdentifyno"] = Convert.ToString(dt.Rows[i]["UserIdentifyNo"]);
                        }

                        Session["shipfullname"] = Convert.ToString(dt.Rows[i]["ShipFname"]) + " " + Convert.ToString(dt.Rows[i]["ShipLname"]);
                        Session["shipcontact"] = Convert.ToString(dt.Rows[i]["ShipMobile"]);
                        Session["shipcompany"] = Convert.ToString(dt.Rows[i]["ShipCompany"]);
                        Session["shipaddress"] = Convert.ToString(dt.Rows[i]["ShipAddress"]);
                        Session["shipnearby"] = Convert.ToString(dt.Rows[i]["ShipNearby"]);
                        Session["shipstate"] = Convert.ToString(dt.Rows[i]["State"]);
                        Session["shipcity"] = Convert.ToString(dt.Rows[i]["ShipCity"]);
                        Session["shippincode"] = Convert.ToString(dt.Rows[i]["ShipZip"]);
                        Session["shipcountry"] = Convert.ToString(dt.Rows[i]["Country"]);
                       
                        sb.Append("<tr><td style='font-weight:normal;text-decoration:none;color:#676767;text-align:left;padding:10px 15px 10px 15px;border-top:solid 1px #D5D5D5;border-bottom:solid 1px #D5D5D5;border-left:solid 1px #D5D5D5;vertical-align:top;'><img src='http://Palshoes.in/Upload/thumbnails/" + dt.Rows[i]["thumbnail"].ToString() + "' height='70px' /></td><td width='400px' style='font-weight:normal;text-decoration:none;color:#676767;text-align:left;text-align:left;padding:10px 15px 10px 15px;border-top:solid 1px #D5D5D5;border-bottom:solid 1px #D5D5D5;border-left:solid 1px #D5D5D5;border-right:solid 1px #D5D5D5;vertical-align:top;'>" + dt.Rows[i]["Name"].ToString() + "</td>");

                        sb.Append("<td style='font-weight:normal;text-decoration:none;color:#676767;text-align:left;padding:10px 15px 10px 15px;border-top:solid 1px #D5D5D5;border-bottom:solid 1px #D5D5D5;border-right:solid 1px #D5D5D5;vertical-align:top;'>" + dt.Rows[i]["VariantName"].ToString() + "</td>");

                        //sb.Append("<td style='font-weight:normal;text-decoration:none;color:#676767;text-align:left;padding:10px 15px 10px 15px;border-top:solid 1px #D5D5D5;border-bottom:solid 1px #D5D5D5;border-right:solid 1px #D5D5D5;vertical-align:top;'>" + dt.Rows[i]["ShippingCharges"].ToString() + " Rs</td>");

                        sb.Append("<td style='font-weight:normal;text-decoration:none;color:#676767;text-align:left;padding:10px 15px 10px 15px;border-top:solid 1px #D5D5D5;border-bottom:solid 1px #D5D5D5;border-right:solid 1px #D5D5D5;vertical-align:top;'>Quantity:" + dt.Rows[i]["Quantity"].ToString() + "</td>");
                        
                        //sb.Append("<td style='font-weight:normal;text-decoration:none;color:#676767;text-align:left;padding:10px 15px 10px 15px;border-top:solid 1px #D5D5D5;border-bottom:solid 1px #D5D5D5;border-right:solid 1px #D5D5D5;vertical-align:top;'>" + weight + "</td>");
                    
                        //sb.Append("<td style='font-weight:normal;text-decoration:none;color:#676767;text-align:left;padding:10px 15px 10px 15px;border-top:solid 1px #D5D5D5;border-bottom:solid 1px #D5D5D5;border-right:solid 1px #D5D5D5;vertical-align:top;'>" + flavour + "</td>");

                        sb.Append("<td style='font-weight:normal;text-decoration:none;color:#676767;text-align:left;padding:10px 15px 10px 15px;border-top:solid 1px #D5D5D5;border-bottom:solid 1px #D5D5D5;border-right:solid 1px #D5D5D5;vertical-align:top;'>" + price + "</td></tr>");
                    }

                    sb.Append("<tr><td colspan='3' align='right' style='font-weight:normal;text-decoration:none;color:#676767;padding:10px 70px 10px 15px;border:solid 1px #D5D5D5;vertical-align:top;'>Shipping Charges:" + Session["finalshippingcharges"] + " Rs</td>  <td colspan='2' align='right' style='font-weight:normal;text-decoration:none;color:#676767;padding:10px 60px 10px 15px;border:solid 1px #D5D5D5;vertical-align:top;'>SubTotal : " + Session["currency"].ToString() + " " + Session["TotalAmount"].ToString() + "</td></tr>");

                    //sb.Append("<tr><td colspan='2' align='right' style='font-weight:normal;text-decoration:none;color:#676767;padding:10px 70px 10px 15px;border:solid 1px #D5D5D5;vertical-align:top;'>SubTotal : " + Session["currency"].ToString() + " " + Session["TotalAmount"].ToString() + "</td></tr>");

                    sb.Append("</table><br/><br/>");
                    sb.Append("Shipping Details : <br/><br/>");
                    sb.Append("<table style='width:700px;text-align:left;' cellpadding='0' cellspacing='0' >");
                    sb.Append("<tr><td width='300px' style='font-weight:normal;text-decoration:none;color:#676767;text-align:left;text-align:left;padding:10px 15px 10px 15px;border-top:solid 1px #D5D5D5;border-bottom:solid 1px #D5D5D5;border-left:solid 1px #D5D5D5;border-right:solid 1px #D5D5D5;vertical-align:top;'>Full Name :</td>");
                    sb.Append("<td width='400px' style='font-weight:normal;text-decoration:none;color:#676767;text-align:left;padding:10px 15px 10px 15px;border-top:solid 1px #D5D5D5;border-bottom:solid 1px #D5D5D5;border-right:solid 1px #D5D5D5;vertical-align:top;'>" + Session["shipfullname"].ToString() + "</td></tr>");
                    sb.Append("<tr><td width='300px' style='font-weight:normal;text-decoration:none;color:#676767;text-align:left;text-align:left;padding:10px 15px 10px 15px;border-top:solid 1px #D5D5D5;border-bottom:solid 1px #D5D5D5;border-left:solid 1px #D5D5D5;border-right:solid 1px #D5D5D5;vertical-align:top;'>Mobile Number :</td>");
                    sb.Append("<td width='400px' style='font-weight:normal;text-decoration:none;color:#676767;text-align:left;padding:10px 15px 10px 15px;border-top:solid 1px #D5D5D5;border-bottom:solid 1px #D5D5D5;border-right:solid 1px #D5D5D5;vertical-align:top;'>" + Session["shipcontact"].ToString() + "</td></tr>");
                    sb.Append("<tr><td width='300px' style='font-weight:normal;text-decoration:none;color:#676767;text-align:left;text-align:left;padding:10px 15px 10px 15px;border-top:solid 1px #D5D5D5;border-bottom:solid 1px #D5D5D5;border-left:solid 1px #D5D5D5;border-right:solid 1px #D5D5D5;vertical-align:top;'>Addres :</td>");
                    sb.Append("<td width='400px' style='font-weight:normal;text-decoration:none;color:#676767;text-align:left;padding:10px 15px 10px 15px;border-top:solid 1px #D5D5D5;border-bottom:solid 1px #D5D5D5;border-right:solid 1px #D5D5D5;vertical-align:top;'>" + Session["shipaddress"].ToString() + "</td></tr>");
                    sb.Append("<tr><td width='300px' style='font-weight:normal;text-decoration:none;color:#676767;text-align:left;text-align:left;padding:10px 15px 10px 15px;border-top:solid 1px #D5D5D5;border-bottom:solid 1px #D5D5D5;border-left:solid 1px #D5D5D5;border-right:solid 1px #D5D5D5;vertical-align:top;'>Near by :</td>");
                    sb.Append("<td width='400px' style='font-weight:normal;text-decoration:none;color:#676767;text-align:left;padding:10px 15px 10px 15px;border-top:solid 1px #D5D5D5;border-bottom:solid 1px #D5D5D5;border-right:solid 1px #D5D5D5;vertical-align:top;'>" + Session["shipnearby"].ToString() + "</td></tr>");
                    sb.Append("<tr><td width='300px' style='font-weight:normal;text-decoration:none;color:#676767;text-align:left;text-align:left;padding:10px 15px 10px 15px;border-top:solid 1px #D5D5D5;border-bottom:solid 1px #D5D5D5;border-left:solid 1px #D5D5D5;border-right:solid 1px #D5D5D5;vertical-align:top;'>Company :</td>");
                    sb.Append("<td width='400px' style='font-weight:normal;text-decoration:none;color:#676767;text-align:left;padding:10px 15px 10px 15px;border-top:solid 1px #D5D5D5;border-bottom:solid 1px #D5D5D5;border-right:solid 1px #D5D5D5;vertical-align:top;'>" + Session["shipcompany"].ToString() + "</td></tr>");
                    sb.Append("<tr><td width='300px' style='font-weight:normal;text-decoration:none;color:#676767;text-align:left;text-align:left;padding:10px 15px 10px 15px;border-top:solid 1px #D5D5D5;border-bottom:solid 1px #D5D5D5;border-left:solid 1px #D5D5D5;border-right:solid 1px #D5D5D5;vertical-align:top;'>City :</td>");
                    sb.Append("<td width='400px' style='font-weight:normal;text-decoration:none;color:#676767;text-align:left;padding:10px 15px 10px 15px;border-top:solid 1px #D5D5D5;border-bottom:solid 1px #D5D5D5;border-right:solid 1px #D5D5D5;vertical-align:top;'>" + Session["shipcity"].ToString() + "</td></tr>");
                    sb.Append("<tr><td width='300px' style='font-weight:normal;text-decoration:none;color:#676767;text-align:left;text-align:left;padding:10px 15px 10px 15px;border-top:solid 1px #D5D5D5;border-bottom:solid 1px #D5D5D5;border-left:solid 1px #D5D5D5;border-right:solid 1px #D5D5D5;vertical-align:top;'>State :</td>");
                    sb.Append("<td width='400px' style='font-weight:normal;text-decoration:none;color:#676767;text-align:left;padding:10px 15px 10px 15px;border-top:solid 1px #D5D5D5;border-bottom:solid 1px #D5D5D5;border-right:solid 1px #D5D5D5;vertical-align:top;'>" + Session["shipstate"].ToString() + "</td></tr>");
                    sb.Append("<tr><td width='300px' style='font-weight:normal;text-decoration:none;color:#676767;text-align:left;text-align:left;padding:10px 15px 10px 15px;border-top:solid 1px #D5D5D5;border-bottom:solid 1px #D5D5D5;border-left:solid 1px #D5D5D5;border-right:solid 1px #D5D5D5;vertical-align:top;'>Country:</td>");
                    sb.Append("<td width='400px' style='font-weight:normal;text-decoration:none;color:#676767;text-align:left;padding:10px 15px 10px 15px;border-top:solid 1px #D5D5D5;border-bottom:solid 1px #D5D5D5;border-right:solid 1px #D5D5D5;vertical-align:top;'>" + "INDIA" + "</td></tr>");
                    //sb.Append("<td width='400px' style='font-weight:normal;text-decoration:none;color:#676767;text-align:left;padding:10px 15px 10px 15px;border-top:solid 1px #D5D5D5;border-bottom:solid 1px #D5D5D5;border-right:solid 1px #D5D5D5;vertical-align:top;'>" + Session["shipcountry"].ToString() + "</td></tr>");
                    sb.Append("<tr><td width='300px' style='font-weight:normal;text-decoration:none;color:#676767;text-align:left;text-align:left;padding:10px 15px 10px 15px;border-top:solid 1px #D5D5D5;border-bottom:solid 1px #D5D5D5;border-left:solid 1px #D5D5D5;border-right:solid 1px #D5D5D5;vertical-align:top;'>Zip Code :</td>");
                    sb.Append("<td width='400px' style='font-weight:normal;text-decoration:none;color:#676767;text-align:left;padding:10px 15px 10px 15px;border-top:solid 1px #D5D5D5;border-bottom:solid 1px #D5D5D5;border-right:solid 1px #D5D5D5;vertical-align:top;'>" + Session["shippincode"].ToString() + "</td></tr>");

                    sb.Append("<img src='http://www.Palshoes-tabletennis.com/images/Palshoes-logo.png' alt='complogo'/>");

                    //sb.Append("<img src='images/Palshoes-logo.png' alt='complogo'/><br/>");

                    sb.Append("</table><br/><br/>Thanks & Regards <br/>Palshoes-Tabletennis.com<br/>(+91) 8287045220");
                    Session["body"] = sb.ToString();
                    Session["ok"] = "Order ok";
                }
            }


            string PaymentType = "";
            if (Session["username"].ToString() != "Guest")
            {
                //if (radWallet.Checked == true)
                //{
                //    decimal walletamt = Convert.ToDecimal(Session["walletamount"]);
                //    decimal totalpayamt = Convert.ToDecimal(Session["TotalAmount"]);
                //    Int32 UserId = Convert.ToInt32(Session["userid"]);
                //    int result = 0;

                //    int walletid = Convert.ToInt32(Session["walletid"]);
                //    string transaction = Session["TransId"].ToString();
                //    string trastype = "Purchage Product";
                //    string custname = Session["Customername"].ToString();
                //    string custemailid = Session["CustEmailid"].ToString();
                //    string useridentifyno = Session["CustIdentifyno"].ToString();
                //    if (totalpayamt <= walletamt)
                //    {
                //        PaymentType = "Wallet";
                //        result = local_wallet.UpdateCustomerWalletFundtransfer(totalpayamt, UserId);
                //        local_wallet.DeductCustomerWalletAmt(walletid, UserId, walletamt, totalpayamt, transaction, trastype, PaymentType, custname, custemailid, UserId, useridentifyno);
                //        if (result != 0)
                //        {
                //            Session["paymentmode"] = PaymentType;
                //            Response.Redirect("~/order-complete.aspx", false);
                //        }
                //    }
                //    else
                //    {
                //        decimal extraamt = totalpayamt - walletamt;
                //        PaymentType = "Wallet and Card";
                //        Session["TotalAmount"] = extraamt;
                //        result = local_wallet.UpdateCustomerWalletFundtransfer(walletamt, UserId);
                //        local_wallet.DeductCustomerWalletAmt(walletid, UserId, walletamt, walletamt, transaction, trastype, PaymentType, custname, custemailid, UserId, useridentifyno);
                //        if (result != 0)
                //        {
                //            Session["paymentmode"] = PaymentType;
                //            Response.Redirect("~/pay/Default.aspx", false);
                //        }
                //    }
                //}
                if (radCard.Checked == true)
                {
                    PaymentType = "Card";
                    Session["paymentmode"] = PaymentType;
                    Response.Redirect("~/payment/Default.aspx", false);
                }
                else if (radCOD.Checked == true)
                {
                    PaymentType = "Cash On Delivery";
                    Session["paymentmode"] = PaymentType;
                    Response.Redirect("~/order-complete.aspx", false);
                }
            }
            else
            {
                if (radCard.Checked == true)
                {
                    PaymentType = "Card";
                    Session["paymentmode"] = PaymentType;
                    Response.Redirect("~/pay/Default.aspx", false);
                }
                else if (radCOD.Checked == true)
                {
                    PaymentType = "Cash On Delivery";
                    Session["paymentmode"] = PaymentType;
                    Response.Redirect("~/order-complete.aspx", false);
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }


    private void BindWalletAmountOrdOvw()
    {
        try
        {
            DataTable dt = new DataTable();
            string user = Session["userid"].ToString();
            dt = local_wallet.getWalletOrderOvwAmount(user);
            if (dt.Rows.Count > 0)
            {
                Session["walletamount"] = dt.Rows[0]["Amount"].ToString();
                Session["walletid"] = dt.Rows[0]["WalletId"].ToString();
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
                    empResult.Add(dt.Rows[i]["name"].ToString());
                }
            }
        }
        catch
        { }
        return empResult;
    }
}