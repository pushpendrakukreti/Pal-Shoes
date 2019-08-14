using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SqlAccess;
using System.Drawing;
using System.Text;
using System.Net;
using System.Net.Mail;
using System.IO;
using System.Web.Services;
using System.Text.RegularExpressions;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections;

public partial class order_complete : System.Web.UI.Page
{
    Products local_product = new Products();
    ShoppingCart local_cart = new ShoppingCart();
    Catergoy local_category = new Catergoy();
    OrderDetails obj_order = new OrderDetails();
    SubCategory local_subcategory = new SubCategory();
    WalletCls local_wallet = new WalletCls();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Session["username"] as string))
        {
            if (!string.IsNullOrEmpty(Session["Transid"] as string))
            {
                if (Session["paymentmode"] != null)
                {
                    if (!IsPostBack)
                    {
                        if (!string.IsNullOrEmpty(Session["currency"] as string))
                        {
                            if (Session["currency"].ToString() == "INR")
                            {
                                //ddCurrency.SelectedValue = "1";
                            }
                            else
                            {
                                //ddCurrency.SelectedValue = "2";
                            }
                        }
                        else
                        {
                            Session["currency"] = "INR";
                        }

                        //
                        UpdateOrderDetails();
                        if (Session["ok"] != null)
                        {
                            SendOrderMessage();
                            SendToClientMail();
                        }
                        //

                        if (!string.IsNullOrEmpty(Session["Tid"] as string))
                        {
                            BindWishlistNo();
                        }
                        if (!string.IsNullOrEmpty(Session["compare"] as string))
                        {
                            BindCompareItemsNo();
                        }
                        if (!string.IsNullOrEmpty(Session["Transid"] as string))
                        {
                            BintCartItemsPrice();
                        }
                        BintCartItems();
                        BindWalletAmount();
                        string userID;
                        if (!string.IsNullOrEmpty(Session["userid"] as string))
                        {
                            userID = Convert.ToString(Session["userid"]);
                            BindShippingAdress(userID);
                            BindBillingAdress(userID);
                        }
                        else
                        {
                            string transid = Session["Transid"].ToString();
                            BindShippingAdress2(transid);
                            BindBillingAdress2(transid);
                        }

                        //ddCurrency.Enabled = false;
                        BindLoginLogout();
                        BindCategoryMenu();
                        BindAccessories();
                        if (Session["userid"] != null)
                        {
                            pnlwishlist.Visible = true;
                        }

                        string transactId = Convert.ToString(Session["Transid"]);
                        if (Session["Transid"] != null)
                        {
                            UpdateOrderDetails(transactId);
                            BindOrderOverview(transactId);
                            BindOrderComplete(transactId);
                        }

                        Session["checkout"] = null;
                        Session["ok"] = null;
                        Session["paymentmode"] = null;
                        Session["currency"] = null;
                        Session["Transid"] = null;
                        Session["total"] = null;
                        Session["ShippingCharges"] = null;
                        Session["TotalAmount"] = null;
                        Session["OrderId"] = null;
                        Session["CustEmailid"] = null;
                        Session["BillingMobile"] = null;
                        Session["guestcheckout"] = null;
                        Session["VariantName"] = null;

                    }
                }
                else
                {
                    Response.Redirect("~/payment.aspx");
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
    private void UpdateOrderDetails()
    {
        try
        {
            DataTable dt = new DataTable();
            if (Session["username"].ToString() == "Guest")
            {
                dt = local_cart.getShoppingCartItemsGuest(Session["TransId"].ToString());
            }
            else
            {
                dt = local_cart.getShoppingCartItems(Session["TransId"].ToString());
            }
            if (dt.Rows.Count > 0)
            {
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    BEL obj_d = new BEL();
                    OrderDetails obj_order = new OrderDetails();

                    Int32 productId = Convert.ToInt32(dt.Rows[j]["ProductId"].ToString());
                    string thumbnailimg = dt.Rows[j]["Thumbnail"].ToString();
                    int Quantity = Convert.ToInt32(dt.Rows[j]["Quantity"].ToString());

                    if (Session["currency"].ToString() == "INR")
                    {
                        obj_d.ItemPrice = Convert.ToDecimal(dt.Rows[j]["inprice"]);
                    }
                    else if (Session["currency"].ToString() == "USD")
                    {
                        obj_d.ItemPrice = Convert.ToDecimal(dt.Rows[j]["usdprice"]);
                    }

                    obj_d.Productname = Convert.ToString(dt.Rows[j]["Name"]);

                    string variantname = Convert.ToString(dt.Rows[j]["VariantName"]);
                    obj_d.Variantname = Convert.ToString(dt.Rows[j]["VariantName"]);
                    obj_d.Variantquantity = Convert.ToInt32(dt.Rows[j]["VariantQuantity"]);
                    
                    obj_d.Quantity = Quantity;

                    obj_d.Thumbnail = thumbnailimg;
                    obj_d.Orderdetailid = Session["OrderId"].ToString();
                    obj_d.Currency = Convert.ToString(Session["currency"]);
                    decimal itemsubtotal = 0;
                    if (Session["total"] != null)
                    {
                        if (Session["currency"].ToString() == "INR")
                        {
                            itemsubtotal = Convert.ToDecimal(dt.Rows[j]["totalinprice"].ToString());
                        }
                        else if (Session["currency"].ToString() == "USD")
                        {
                            itemsubtotal = Convert.ToDecimal(dt.Rows[j]["totalusdprice"].ToString());
                        }
                    }
                    obj_d.ItemSubtotal = itemsubtotal;
                    decimal shippingCharges = 0;
                    if (Session["ShippingCharges"] != null)
                    {
                        if (Session["currency"].ToString() == "INR")
                        {
                            shippingCharges = Convert.ToDecimal(dt.Rows[j]["ShippingCharges"].ToString());
                        }
                        else if (Session["currency"].ToString() == "USD")
                        {
                            shippingCharges = Convert.ToDecimal(dt.Rows[j]["usdshippingCharges"].ToString());
                        }
                    }
                    obj_d.ShippinCharge = shippingCharges;

                    string CouponPrice = Convert.ToString(dt.Rows[j]["CouponPrice"]);

                    string finalshippingcharges = Convert.ToString(dt.Rows[j]["FinalShippingCharges"]);

                    if (finalshippingcharges != "")
                    {
                        obj_d.FinalShippingCharges = Convert.ToInt32(finalshippingcharges);
                    }
                    else { obj_d.FinalShippingCharges = 0; }

                    if (CouponPrice != "")
                    {
                        obj_d.finalCouponPrice = CouponPrice;
                    }
                    else
                    {
                        obj_d.finalCouponPrice = "0";
                    }

                    //obj_d.finalCouponPrice = Convert.ToString(dt.Rows[j]["CouponPrice"]);

                    obj_d.Paymentmode = Session["paymentmode"].ToString();
                    obj_d.Orderstatus = "Order";
                    bool ordCancel = false;
                    obj_d.OrderCancel = ordCancel;
                    obj_d.Capacity = Convert.ToString(dt.Rows[j]["Capacity"]);
                    obj_d.Flavour = Convert.ToString(dt.Rows[j]["Flavour"]);

                    obj_d.ProductId = productId;
                    obj_d.TransId = Convert.ToString(Session["TransId"]);
                    obj_d.Userid = Convert.ToInt32(Session["userid"]);
                    //
                    obj_d.ShipFname = Convert.ToString(dt.Rows[j]["ShipFname"]);
                    obj_d.ShipLname = Convert.ToString(dt.Rows[j]["ShipLname"]);
                    obj_d.ShipEmail = Convert.ToString(dt.Rows[j]["ShipEmail"]);
                    string shippcompay = Convert.ToString(dt.Rows[j]["ShipCompany"]);
                    if (shippcompay != "")
                    {
                        obj_d.ShipCompany = shippcompay;
                    }
                    else
                    {
                        obj_d.ShipCompany = "";
                    }
                    obj_d.ShipMobile = Convert.ToString(dt.Rows[j]["ShipMobile"]);
                    obj_d.ShipAddress = Convert.ToString(dt.Rows[j]["ShipAddress"]);
                    obj_d.ShipNearby = Convert.ToString(dt.Rows[j]["ShipNearby"]);
                    obj_d.ShipCountry = Convert.ToString(dt.Rows[j]["ShipCountry"]);
                    string shipState = Convert.ToString(dt.Rows[j]["ShipState"]);
                    if (shipState != "")
                    {
                        obj_d.ShipState = shipState;
                    }
                    else
                    {
                        obj_d.ShipState = "";
                    }
                    obj_d.ShipCity = Convert.ToString(dt.Rows[j]["ShipCity"]);
                    obj_d.ShipZip = Convert.ToString(dt.Rows[j]["ShipZip"]);

                    obj_d.BillFname = Convert.ToString(dt.Rows[j]["BillFname"]);
                    obj_d.BillLname = Convert.ToString(dt.Rows[j]["BillLname"]);
                    obj_d.BillEmailid = Convert.ToString(dt.Rows[j]["BillEmailid"]);
                    string billcompay = Convert.ToString(dt.Rows[j]["BillCompay"]);
                    if (billcompay != "")
                    {
                        obj_d.BillCompay = billcompay;
                    }
                    else
                    {
                        obj_d.BillCompay = "";
                    }
                    obj_d.BillContact = Convert.ToString(dt.Rows[j]["BillContact"]);
                    obj_d.BillAddress = Convert.ToString(dt.Rows[j]["BillAddress"]);
                    string nearbybilladdress = Convert.ToString(dt.Rows[j]["BillNearby"]);
                    if (nearbybilladdress != "")
                    {
                        obj_d.BillNearby = nearbybilladdress;
                    }
                    else
                    {
                        obj_d.BillNearby = "";
                    }
                    obj_d.BillCountry = Convert.ToString(dt.Rows[j]["BillCountry"]);
                    string billstate = Convert.ToString(dt.Rows[j]["BillState"]);
                    if (billstate != "")
                    {
                        obj_d.BillState = billstate;
                    }
                    else
                    {
                        obj_d.BillState = "";
                    }
                    obj_d.BillCity = Convert.ToString(dt.Rows[j]["BillCity"]);
                    obj_d.BillZip = Convert.ToString(dt.Rows[j]["BillZip"]);
                    obj_d.UserIdetifyNo = "";
                    if (Session["username"].ToString() == "Guest")
                    {
                        obj_d.UserIdetifyNo = Convert.ToString(dt.Rows[j]["BillContact"]);
                    }
                    else
                    {
                        obj_d.UserIdetifyNo = Convert.ToString(dt.Rows[j]["UserIdentifyNo"]);
                    }
                    try
                    {
                        obj_order.AddOrderDetails(obj_d);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                    finally
                    {
                        obj_order = null;
                    }

                     //Update Product Quantity after the order placed. 
                    DataTable dt2 = local_cart.getQuantityfromTable(productId, variantname);
                    string oldquantitysize = dt2.Rows[0]["Quantity"].ToString();
                    string variantname1 = dt2.Rows[0]["Variantname"].ToString();

                    string userselectedvariant = dt.Rows[j]["VariantName"].ToString();
                    Int32 userquantityselected = Convert.ToInt32(dt.Rows[j]["Quantity"]);
                    Int32 quantitysize = Convert.ToInt32(dt.Rows[j]["VariantQuantity"]);

                    Int32 updatequantitysize = quantitysize - userquantityselected;

                    //Declare a arraylist for getting comma separated string
                    ArrayList arrlist = new ArrayList();
                    //check wether the re is comma in the end of the string
                    if (variantname1.Trim().EndsWith(","))
                    {
                        variantname1 = variantname1.Substring(0, variantname1.Length - 1);
                    }
                    //split the comma separated string into arraylist
                    arrlist.AddRange(variantname1.Split(','));

                    Int32 find = 0;
                    for (int i = 0; i < arrlist.Count; i++)
                    {
                      if (arrlist[i].ToString() == userselectedvariant)
                        {
                           find = i;
                        }
                    }

                //Declare a arraylist for getting comma separated string
                ArrayList arrlist1 = new ArrayList();
                //check wether the re is comma in the end of the string
                if (oldquantitysize.Trim().EndsWith(","))
                {
                    oldquantitysize = oldquantitysize.Substring(0, oldquantitysize.Length - 1);
                }
                //split the comma separated string into arraylist
                arrlist1.AddRange(oldquantitysize.Split(','));

                    //arrlist1.IndexOf(find, updatequantitysize);
                    arrlist1[find] = updatequantitysize;

                    string actualquantity = "";
                    for (int k = 0; k < arrlist1.Count; k++)
                    {
                        actualquantity = String.Join(",", arrlist1.ToArray());
                    }

                    //Int32 actualquantity = quantitysize - Quantity;
                    local_cart.UpdateQuantity(productId, actualquantity);
                }
            }
        }
        catch(Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    private void SendToClientMail()
    {
        try
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("noreply@Palshoes-tabletennis.com", "Pal Shoes-Shop");
            mail.To.Add(Session["CustEmailid"].ToString());
            mail.IsBodyHtml = true;
            mail.Subject = "Order Details.. ";
            mail.Body = Session["body"].ToString();
            SmtpClient smtp = new SmtpClient("Palshoes-tabletennis.com", 25);
            smtp.Credentials = new System.Net.NetworkCredential("noreply@Palshoes-tabletennis.com", "Mgfy57&8");
            smtp.EnableSsl = false;
            smtp.Send(mail);

            lblMessage.ForeColor = Color.Green;
            lblMessage.Text = "Your Product Order Number is : " + Session["OrderId"].ToString() + "<br/>The order has been placed and will be delivered very soon. <br />Thanks You.";
        }
        catch
        { }
    }
    public void SendOrderMessage()
    {
        //try
        //{
        //    string key = null;
        //    string Sender_id = null;
        //    string contactnumber = null;
        //    string Message = null;
        //    key = "45A27FD357A379";
        //    Sender_id = "NZFUND";

        //    Message = "Your Product Order Number is " + Session["OrderId"] + ", The order has been placed and will be delivered very soon. To more detail check your mail.";
        //    contactnumber = Session["BillingMobile"].ToString();
        //    SendSMS(key, contactnumber, Sender_id, Message);
        //}
        //catch
        //{ }
    }
    public string SendSMS(string key, string contactno, string Sender_id, string Message)
    {
        WebClient client = new WebClient();
        string baseurl = "http://textsms.co.in/app/smsapi/index.php?key=" + key + "&routeid=6&type=text&contacts=" + contactno + "&senderid=" + Sender_id + "&msg=" + Message + "";

        Stream data = client.OpenRead(baseurl);
        StreamReader reader = new StreamReader(data);
        string s = reader.ReadToEnd();
        data.Close();
        reader.Close();
        return s;
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

    //Order Complete
    private void BindOrderOverview(string transactionid)
    {
        DataTable dt = new DataTable();
        try
        {
            dt = local_cart.getOrderComplete(transactionid);
            if (dt.Rows.Count > 0)
            {
                rpOrderOverview.DataSource = dt;
                rpOrderOverview.DataBind();

                DataTable dt2 = new DataTable();
                //dt2 = obj_order.getTotalPriceOrderComplete(transactionid);
                dt2 = local_cart.getTotalPriceAfterCouponShoppingCart(transactionid);
                if (Convert.ToString(Session["currency"]) == "INR")
                {
                    Session["total"] = dt2.Rows[0]["totalprice2"].ToString();
                    //lblSubtotal.Text = "INR " + Session["total"].ToString();
                    Bindshippingcharges();
                }
                else if (Convert.ToString(Session["currency"]) == "USD")
                {
                    Session["total"] = dt2.Rows[0]["totalprice2"].ToString();
                    //lblSubtotal.Text = "USD " + dollar;
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
            dt = obj_order.getShippingChargeOrderComplete(trasid);
            if (dt.Rows.Count > 0)
            {
                decimal shippingcharges = 0;
                for (int p = 0; p < dt.Rows.Count; p++)
                {
                    if (Session["currency"].ToString() == "INR")
                    {
                        decimal shipping = Convert.ToDecimal(dt.Rows[p]["ShippingCharge"]);
                        decimal quantity = Convert.ToDecimal(dt.Rows[p]["Quantity"]);
                        shippingcharges += shipping * quantity;
                    }
                    else
                    {
                        decimal shipping = Convert.ToDecimal(dt.Rows[p]["ShippingCharge"]);
                        decimal quantity = Convert.ToDecimal(dt.Rows[p]["Quantity"]);
                        shippingcharges += shipping * quantity;
                    }
                }
                decimal total = Convert.ToDecimal(Session["total"]);
                decimal amount = total + shippingcharges;
                Session["TotalAmount"] = amount;
                string currency = "INR";
                //if (ddCurrency.SelectedIndex == 0)
                //{
                //    currency = "INR";
                //}
                //else
                //{ currency = "USD"; }

                lblTotalamount.Text = currency + " " + Convert.ToString(amount);
            }
        }
        catch
        { }
    }

    protected void rpOrderOverview_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (Session["currency"].ToString() == "INR")
        {
            Literal lblprice = (Literal)e.Item.FindControl("lblprice");
            Literal lblusdprice = (Literal)e.Item.FindControl("lblusdprice");
            lblprice.Visible = true;
            lblusdprice.Visible = false;

            lblprice.Text = "INR " + lblprice.Text;
        }
        else
        {
            Literal lblprice = (Literal)e.Item.FindControl("lblprice");
            Literal lblusdprice = (Literal)e.Item.FindControl("lblusdprice");
            lblprice.Visible = false;
            lblusdprice.Visible = true;

            lblusdprice.Text = "USD " + lblusdprice.Text;
        }
    }

    private void BindOrderComplete(string transactionid)
    {
        DataTable dt = new DataTable();
        try
        {
            dt = obj_order.getOrderCompleted(transactionid);
            if (dt.Rows.Count > 0)
            {
                lblOrderdate.Text = Convert.ToString(dt.Rows[0]["DD MON YYYY"]);
                lblOrderNo.Text = "#" + Convert.ToString(dt.Rows[0]["Orderdetailid"]);
                lblPaymentmode.Text = Convert.ToString(dt.Rows[0]["Paymentmode"]);
            }
        }
        catch
        { }
    }
    private void UpdateOrderDetails(string transactionId)
    {
        try
        {
            string paymentmode = Session["paymentmode"].ToString();
            string orderstatus = "Order";

            obj_order.UpdateOrderDetail(transactionId, paymentmode, orderstatus);
        }
        catch
        {
        }
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