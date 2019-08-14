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

public partial class account : System.Web.UI.Page
{
    Catergoy local_category = new Catergoy();
    SubCategory local_subcategory = new SubCategory();
    Products local_product = new Products();
    ShoppingCart local_cart = new ShoppingCart();
    WalletCls local_wallet = new WalletCls();
    chkLogin local_login = new chkLogin();
    OrderDetails obj_order = new OrderDetails();
    string UserId;
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["account"] = "User Account";

        if (!string.IsNullOrEmpty(Convert.ToString(Session["userid"])) && Session["logintype"].ToString() !="Admin")
        {
            UserId = Session["userid"].ToString();
            if (!IsPostBack)
            {
                BindContry();
                BindState();

                //BindACDetails(UserId);

                //txtDate1.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                BindUseNameAndID(UserId);

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
                if (!string.IsNullOrEmpty(Session["userid"] as string))
                {
                    string userID = Convert.ToString(Session["userid"]);
                    BindOrderOverview(userID);
                    BindOrderComplete(userID);

                    BindShippingAdress(userID);
                    BindBillingAdress(userID);
                    BindUserWalletDetails(userID);

                    BindOldOrderOverview(userID);//old

                    BindUserInformation(UserId);
                }
                BintCartItems();

                if (!string.IsNullOrEmpty(Session["Transid"] as string))
                {
                    BintCartItemsPrice();
                }
            }
            BindCategoryMenu();
            BindLoginLogout();
            BindWalletAmount();
            BindAccessories();

            if (!string.IsNullOrEmpty(Session["Tid"] as string))
            {
                BindWishlistNo();
            }
            if (!string.IsNullOrEmpty(Session["compare"] as string))
            {
                BindCompareItemsNo();
            }
            Panel1.Visible = false;
            if (Session["userid"] != null)
            {
                pnlwishlist.Visible = true;
                //Panel2.Visible = false;
            }
        }
        else
        {
            Response.Redirect("Login.aspx");
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
    
    private void BindUserInformation(string userid)
    {
        try
        {
            DataTable dt = new DataTable();
            dt = local_cart.getUserInformation(userid);
            if (dt.Rows.Count > 0)
            {
                txtFullName.Text = Convert.ToString(dt.Rows[0]["Shippingname"]);
                txtEmailAddress.Text = Convert.ToString(dt.Rows[0]["ShipEmail"]);
                txtCompany.Text = Convert.ToString(dt.Rows[0]["ShipCompany"]);
                txtContactNumber.Text = Convert.ToString(dt.Rows[0]["ShipMobile"]);
                txtShippingAddress.Text = Convert.ToString(dt.Rows[0]["ShipAddress"]);
                txtShippingLandmark.Text = Convert.ToString(dt.Rows[0]["ShipNearby"]);
                ddCountry.SelectedValue = Convert.ToString(dt.Rows[0]["ShipCountry"]);
                ddState.SelectedValue = Convert.ToString(dt.Rows[0]["ShipState"]);
                txtSelectCity.Text = Convert.ToString(dt.Rows[0]["ShipCity"]);
                txtPostcodezip.Text = Convert.ToString(dt.Rows[0]["ShipZip"]);

                txtFullNameBill.Text = Convert.ToString(dt.Rows[0]["Billingname"]);
                txtEmailAddressBill.Text = Convert.ToString(dt.Rows[0]["BillEmailid"]);
                txtCompanyBill.Text = Convert.ToString(dt.Rows[0]["BillCompay"]);
                txtContactNumberBill.Text = Convert.ToString(dt.Rows[0]["BillContact"]);
                txtBillingAddressBill.Text = Convert.ToString(dt.Rows[0]["BillAddress"]);
                txtBillingLandmarkBill.Text = Convert.ToString(dt.Rows[0]["BillNearby"]);
                ddCountryBill.SelectedValue = Convert.ToString(dt.Rows[0]["BillCountry"]);
                ddStateBill.SelectedValue = Convert.ToString(dt.Rows[0]["BillState"]);
                txtSelectCityBill.Text = Convert.ToString(dt.Rows[0]["BillCity"]);
                txtPostcodezipBill.Text = Convert.ToString(dt.Rows[0]["BillZip"]);
            }
        }
        catch
        { }
    }
    private void BindUseNameAndID(string userid)
    {
        DataTable dt = new DataTable();
        try
        {
            dt = local_cart.getUserAccountNameAndID(userid);
            if (dt.Rows.Count > 0)
            {
                lblUserName.Text = dt.Rows[0]["Firstname"].ToString() + " (" + dt.Rows[0]["UserIdentifyNo"].ToString() + ")";
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

                lblUserName.Text = dt.Rows[0]["Fullname"].ToString();
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


    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox1.Checked == true)
        {
            txtFullName.Text = txtFullNameBill.Text;
            txtEmailAddress.Text = txtEmailAddressBill.Text;
            txtShippingAddress.Text = txtBillingAddressBill.Text;
            txtShippingLandmark.Text = txtBillingLandmarkBill.Text;
            txtCompany.Text = txtCompanyBill.Text;
            txtContactNumber.Text = txtContactNumberBill.Text;
            txtPostcodezip.Text = txtPostcodezipBill.Text;
            txtSelectCity.Text = txtSelectCityBill.Text;
            ddCountry.SelectedValue = ddCountryBill.SelectedValue;
            ddState.SelectedValue = ddStateBill.SelectedValue;
            Session["checked"] = "Same as shipping";
        }
        else if (CheckBox1.Checked == false)
        {
            txtFullName.Text = "";
            txtEmailAddress.Text = "";
            txtShippingAddress.Text = "";
            txtShippingLandmark.Text = "";
            txtCompany.Text = "";
            txtContactNumber.Text = "";
            txtPostcodezip.Text = "";
            txtSelectCity.Text = "";
        }
    }

    private void BindContry()
    {
        DataTable dt = new DataTable();
        try
        {
            dt = local_cart.getCountry();
            if (dt.Rows.Count > 0)
            {
                ddCountry.DataSource = dt;
                ddCountry.DataTextField = "ContryName";
                ddCountry.DataValueField = "CountryCode";
                ddCountry.DataBind();
                ddCountry.Items.Insert(0, "Select Country");
                ddCountry.SelectedIndex = 103;

                ddCountryBill.DataSource = dt;
                ddCountryBill.DataTextField = "ContryName";
                ddCountryBill.DataValueField = "CountryCode";
                ddCountryBill.DataBind();
                ddCountryBill.Items.Insert(0, "Select Country");
                ddCountryBill.SelectedIndex = 103;
            }
        }
        catch
        { }
    }
    private void BindState()
    {
        DataTable dt = new DataTable();
        try
        {
            dt = local_cart.getState();
            if (dt.Rows.Count > 0)
            {
                ddState.DataSource = dt;
                ddState.DataTextField = "StateName";
                ddState.DataValueField = "StateCode";
                ddState.DataBind();
                ddState.Items.Insert(0, "Select State");
                ddState.SelectedIndex = 9;

                ddStateBill.DataSource = dt;
                ddStateBill.DataTextField = "StateName";
                ddStateBill.DataValueField = "StateCode";
                ddStateBill.DataBind();
                ddStateBill.Items.Insert(0, "Select State");
                ddStateBill.SelectedIndex = 9;
            }
        }
        catch
        { }
    }

    protected void lnkSubmit_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Convert.ToString(Session["userid"])))
        {
            BEL obj_d = new BEL();
            OrderDetails obj_order = new OrderDetails();
            if (CheckBox1.Checked == true)
            {
                txtFullName.Text = txtFullNameBill.Text;
                txtEmailAddress.Text = txtEmailAddressBill.Text;
                txtShippingAddress.Text = txtBillingAddressBill.Text;
                txtShippingLandmark.Text = txtBillingLandmarkBill.Text;
                txtCompany.Text = txtCompanyBill.Text;
                txtContactNumber.Text = txtContactNumberBill.Text;
                txtPostcodezip.Text = txtPostcodezipBill.Text;
                txtSelectCity.Text = txtSelectCityBill.Text;
                ddCountry.SelectedValue = ddCountryBill.SelectedValue;
                ddState.SelectedValue = ddStateBill.SelectedValue;
            }

            try
            {
                string billfullname = txtFullNameBill.Text;
                string billfirstname;
                string billlastname;
                string[] billnames = billfullname.ToString().Trim().Split(new char[] { ' ' }, 3);
                if (billnames.Length == 1)
                {
                    billfirstname = billnames[0];
                    billlastname = "";
                }
                else if (billnames.Length == 3)
                {
                    billfirstname = billnames[0];
                    billlastname = billnames[1] + " " + billnames[2];
                }
                else
                {
                    billfirstname = billnames[0];
                    billlastname = billnames[1];
                }

                obj_d.BillFname = billfirstname;
                obj_d.BillLname = billlastname;
                obj_d.BillEmailid = txtEmailAddressBill.Text;
                obj_d.BillCompay = "";
                if (txtCompanyBill.Text != "")
                {
                    obj_d.BillCompay = txtCompanyBill.Text;
                }
                obj_d.BillContact = txtContactNumberBill.Text;
                obj_d.BillAddress = txtBillingAddressBill.Text;
                obj_d.BillNearby = "";
                if (txtBillingLandmarkBill.Text != "")
                {
                    obj_d.BillNearby = txtBillingLandmarkBill.Text;
                }
                obj_d.BillCountry = ddCountryBill.SelectedValue;
                obj_d.BillState = ddStateBill.SelectedValue;
                obj_d.BillCity = "";
                if (txtSelectCityBill.Text != "")
                {
                    obj_d.BillCity = txtSelectCityBill.Text;
                }
                obj_d.BillZip = "";
                if (txtPostcodezipBill.Text != "")
                {
                    obj_d.BillZip = txtPostcodezipBill.Text;
                }


                string fullname = txtFullName.Text;
                string fname;
                string lname;
                string[] names = fullname.ToString().Trim().Split(new char[] { ' ' }, 3);
                if (names.Length == 1)
                {
                    fname = names[0];
                    lname = "";
                }
                else if (names.Length == 3)
                {
                    fname = names[0];
                    lname = names[1] + " " + names[2];
                }
                else
                {
                    fname = names[0];
                    lname = names[1];
                }

                obj_d.ShipFname = fname;
                obj_d.ShipLname = lname;
                obj_d.ShipEmail = "";
                if (txtEmailAddress.Text != "")
                {
                    obj_d.ShipEmail = txtEmailAddress.Text;
                }
                obj_d.ShipCompany = "";
                if (txtCompany.Text != "")
                {
                    obj_d.ShipCompany = txtCompany.Text;
                }
                obj_d.ShipMobile = "";
                if (txtContactNumber.Text != "")
                {
                    obj_d.ShipMobile = txtContactNumber.Text;
                }
                obj_d.ShipAddress = "";
                if (txtShippingAddress.Text != "")
                {
                    obj_d.ShipAddress = txtShippingAddress.Text;
                }
                obj_d.ShipNearby = "";
                if (txtShippingLandmark.Text != "")
                {
                    obj_d.ShipNearby = txtShippingLandmark.Text;
                }
                obj_d.ShipCountry = ddCountry.SelectedValue;

                obj_d.ShipState = ddState.SelectedValue;
                obj_d.ShipCity = "";
                if (txtSelectCity.Text != "")
                {
                    obj_d.ShipCity = txtSelectCity.Text;
                }
                obj_d.ShipZip = "";
                if (txtPostcodezip.Text != "")
                {
                    obj_d.ShipZip = txtPostcodezip.Text;
                }

                int result = 0;
                obj_d.UserId = Convert.ToInt32(Session["userid"]);

                result = obj_order.UpdateUserDetails(obj_d);
                if (result != 0)
                {
                    BindUserInformation(UserId);
                    mpe.Show();
                    lblpopup.Text = "User Account Details Updated!";
                    lbla.Visible = false;
                    txtwalletamount.Visible = false;
                    Addwallet.Visible = false;
                    //lblMessage.ForeColor=Color.Green;
                    //lblMessage.Text = "User Account Details Updated!";
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        else
        {
            Response.Redirect("Login.aspx", false);
        }
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (txtUsername.Text != "" && txtOldPassword.Text != "" && txtNewPassword.Text != "" && txtConfirmpass.Text != "")
        {
            DataTable dt = new DataTable();
            string username = txtUsername.Text.Trim();
            string oldpassword = txtOldPassword.Text.Trim();
            string newpassword = txtNewPassword.Text.Trim();
            string confirmpassword = txtConfirmpass.Text.Trim();

            if (local_login.IfUserPassExists(username, oldpassword, "N", "") == "T")
            {
                dt = local_login.getdataLogin(username);
                string loginid = Convert.ToString(dt.Rows[0]["LoginID"]);

                local_login.UpdatePassword(newpassword, loginid);
                clear();
                mpe.Show();
                lblpopup.Text = "Your Password Changed";
                lbla.Visible = false;
                txtwalletamount.Visible = false;
                Addwallet.Visible = false;
                //lblMessage.ForeColor = Color.Green;
                //lblMessage.Text = "Your Password Changed";
            }
            else
            {
                mpe.Show();
                lblpopup.Text = "Invalid Username/Password";
                lbla.Visible = false;
                txtwalletamount.Visible = false;
                Addwallet.Visible = false;
                //lblMessage.ForeColor = Color.Red;
                //lblMessage.Text = "Username/Password Invalid";
            }
        }
        else
        {
            mpe.Show();
            lblpopup.Text = "Please fill the user mobile nubmer <br/>or email-id and password";
            lbla.Visible = false;
            txtwalletamount.Visible = false;
            Addwallet.Visible = false;
            //Response.Write("<script> alert('Fill User Email-ID and Password'); </script>");
        }
    }

    private void clear()
    {
        txtUsername.Text = "";
        txtOldPassword.Text = "";
        txtNewPassword.Text = "";
        txtConfirmpass.Text = "";
    }


    //Order List
    private void BindOrderOverview(string userid)
    {
        DataTable dt = new DataTable();
        try
        {
            string transctionID = GetTransactionNoToBindOrder(userid);
            dt = local_cart.getOrderCompletetoUser(transctionID);
            if (dt.Rows.Count > 0)
            {
                rpOrderlist.DataSource = dt;
                rpOrderlist.DataBind();

                DataTable dt2 = new DataTable();
                dt2 = obj_order.getTotalPriceOrderComplete(transctionID);
                if (Convert.ToString(Session["currency"]) == "INR")
                {
                    Session["total"] = dt2.Rows[0]["totalprice2"].ToString();
                    //lblSubtotal.Text = "INR " + Session["total"].ToString();
                    Bindshippingcharges();
                }
                else if (Convert.ToString(Session["currency"]) == "USD")
                {
                    Session["total"] = dt2.Rows[0]["totalprice2"].ToString();
                    //decimal dollars = Session["total"];
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
            string transid = GetTransactionNoToBindOrder(UserId);
            dt = obj_order.getShippingChargeOrderlist(transid);
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

    
    protected void rpOrderlist_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Delete")
            {
                string id = e.CommandArgument.ToString();
                string transid = GetTransactionNoToBindOrder(UserId);
                bool cancel = true;
                int result = 0;
                result = obj_order.CancelOrder(transid, id, cancel);

                if (result > 0)
                {
                    BindOrderOverview(transid);

                    mpe.Show();
                    lblpopup.Text = "Product Order has been canceled!!";
                    lbla.Visible = false;
                    txtwalletamount.Visible = false;
                    Addwallet.Visible = false;
                    //lblMessage.ForeColor = Color.Green;
                    //lblMessage.Text = "Product Order has been canceled!";
                    if (!string.IsNullOrEmpty(Session["userid"] as string))
                    {
                        string userID = Convert.ToString(Session["userid"]);
                        BindOrderOverview(userID);
                        BindOrderComplete(userID);
                    }
                }
            }
        }
        catch
        { }
    }
    
    protected void rpOrderlist_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (Session["currency"].ToString() == "INR")
        {
            Literal lbltotal = (Literal)e.Item.FindControl("lbltotal");
            Literal lblusdtotal = (Literal)e.Item.FindControl("lblusdtotal");
            lbltotal.Visible = true;
            lblusdtotal.Visible = false;

            lbltotal.Text = "INR " + lbltotal.Text;
        }
        else
        {
            Literal lbltotal = (Literal)e.Item.FindControl("lbltotal");
            Literal lblusdtotal = (Literal)e.Item.FindControl("lblusdtotal");
            lbltotal.Visible = false;
            lblusdtotal.Visible = true;

            lblusdtotal.Text = "USD " + lblusdtotal.Text;
        }
    }

    private void BindOrderComplete(string userid)
    {
        DataTable dt = new DataTable();
        try
        {
            string transactionid = GetTransactionNoToBindOrder(userid);
            dt = obj_order.getOrderCompleted(transactionid);
            if (dt.Rows.Count > 0)
            {
                lblOrderdate.Text = Convert.ToString(dt.Rows[0]["DD MON YYYY"]);
                lblOrderNo.Text = "#"+ Convert.ToString(dt.Rows[0]["Orderdetailid"]);
            }
        }
        catch
        { }
    }


    private void BindOldOrderOverview(string userid)
    {
        DataTable dt = new DataTable();
        try
        {
            dt = local_cart.getOldOrderCompletetoUser(userid);
            if (dt.Rows.Count > 0)
            {
                rpOldOrderlist.DataSource = dt;
                rpOldOrderlist.DataBind();
            }
        }
        catch
        { }
    }
    protected void rpOldOrderlist_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (Session["currency"].ToString() == "INR")
        {
            Literal lbltotal = (Literal)e.Item.FindControl("lbltotal");
            Literal lblusdtotal = (Literal)e.Item.FindControl("lblusdtotal");
            lbltotal.Visible = true;
            lblusdtotal.Visible = false;

            lbltotal.Text = "INR " + lbltotal.Text;
        }
        else
        {
            Literal lbltotal = (Literal)e.Item.FindControl("lbltotal");
            Literal lblusdtotal = (Literal)e.Item.FindControl("lblusdtotal");
            lbltotal.Visible = false;
            lblusdtotal.Visible = true;

            lblusdtotal.Text = "USD " + lblusdtotal.Text;
        }
    }


    private string GetTransactionNoToBindOrder(string userid)
    {
        DataTable dt = new DataTable();
        string transctionNo = "";
        try
        {
            dt = obj_order.getTransationNoByUserID(userid);
            if (dt.Rows.Count > 0)
            {
                transctionNo = Convert.ToString(dt.Rows[0]["TransId"]);
            }
        }
        catch { }
        return transctionNo;
    }


    private void BindUserWalletDetails(string userid)
    {
        DataTable dt = new DataTable();
        try
        {
            dt = local_wallet.getUserWalletDetails(userid);
            //if (dt.Rows.Count > 0)
            //{
            //    gvWalletDetails.DataSource = dt;
            //    gvWalletDetails.DataBind();
            //}
            //else
            //{
            //    gvWalletDetails.DataSource = null;
            //    gvWalletDetails.DataBind();
            //}
        }
        catch
        { }
    }
    //protected void gvWalletDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    //{
    //    gvWalletDetails.PageIndex = e.NewPageIndex;
    //    try
    //    {
    //        if (txtDate1.Text != "")
    //        {
    //            DataTable dt = new DataTable();
    //            string date1 = txtDate1.Text;
    //            date1 = Convert.ToDateTime(date1).ToString("yyyy-MM-dd");
    //            string date2 = "";
    //            if (txtDate2.Text != "")
    //            {
    //                date2 = txtDate2.Text;
    //                date2 = Convert.ToDateTime(date2).ToString("yyyy-MM-dd");
    //            }
    //            dt = local_wallet.getUserWalletDetailsByDate(date1, date2, UserId);
    //            if (dt.Rows.Count > 0)
    //            {
    //                gvWalletDetails.DataSource = dt;
    //                gvWalletDetails.DataBind();
    //            }
    //            else
    //            {
    //                gvWalletDetails.DataSource = null;
    //                gvWalletDetails.DataBind();
    //            }
    //        }
    //    }
    //    catch
    //    { }
    //}


    //protected void btnGetDetails_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        DataTable dt = new DataTable();
    //        if (txtDate1.Text != "")
    //        {
    //            string date1 = txtDate1.Text;
    //            date1 = Convert.ToDateTime(date1).ToString("yyyy-MM-dd");
    //            string date2 = "";
    //            if (txtDate2.Text != "")
    //            {
    //                date2 = txtDate2.Text;
    //                date2 = Convert.ToDateTime(date2).ToString("yyyy-MM-dd");
    //            }
    //            dt = local_wallet.getUserWalletDetailsByDate(date1, date2, UserId);
    //            if (dt.Rows.Count > 0)
    //            {
    //                gvWalletDetails.DataSource = dt;
    //                gvWalletDetails.DataBind();
    //            }
    //            else
    //            {
    //                gvWalletDetails.DataSource = null;
    //                gvWalletDetails.DataBind();
    //            }
    //        }
    //        else
    //        {
    //            BindUserWalletDetails(UserId);
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMessage.ForeColor = Color.Red;
    //        lblMessage.Text = ex.Message;
    //    }
    //}

    //protected void btnAddAccount_Click(object sender, EventArgs e)
    //{
    //    string customername = txtAcFullname.Text;
    //    long mobileno = Convert.ToInt64(txtMobileNo.Text);
    //    string bankname = txtBankName.Text;
    //    long accontno = Convert.ToInt64(txtAccountNumber.Text);
    //    string ifsccode = txtIFSCCode.Text;
    //    int userid = Convert.ToInt32(Session["userid"]);
    //    int result = 0;

    //    try
    //    {
    //        if (local_wallet.IFAccountExists(userid, "N", "") == "F")
    //        {
    //            result = local_wallet.AddCustomerAccountDetails(mobileno, customername, accontno, ifsccode, bankname, userid);
    //            if (result != 0)
    //            {
    //                clearform();
    //                BindACDetails(UserId);
    //                mpe.Show();
    //                lblpopup.Text = "Bank Account Details Added Successfully!";
    //                lbla.Visible = false;
    //                txtwalletamount.Visible = false;
    //                Addwallet.Visible = false;
    //                //lblMessage.ForeColor = Color.Green;
    //                //lblMessage.Text = "Bank Account Details Added Successfully!";
    //            }
    //        }
    //        else
    //        {
    //            Int32 AcID = Convert.ToInt32(ViewState["ACID"]);
    //            result = local_wallet.UpdateCustomerAccountDetails(mobileno, customername, accontno, ifsccode, bankname, AcID);
    //            if (result != 0)
    //            {
    //                clearform();
    //                BindACDetails(UserId);
    //                mpe.Show();
    //                lblpopup.Text = "Bank Account Details Update Successfully!";
    //                lbla.Visible = false;
    //                txtwalletamount.Visible = false;
    //                Addwallet.Visible = false;
    //                //lblMessage.ForeColor = Color.Green;
    //                //lblMessage.Text = "Bank Account Details Update Successfully!";
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMessage.ForeColor = Color.Red;
    //        lblMessage.Text = ex.Message;
    //    }
    //}

    //private void BindACDetails(string userid)
    //{
    //    try
    //    {
    //        DataTable dt = new DataTable();
    //        dt = local_wallet.getUserBankAcDetails(userid);
    //        if (dt.Rows.Count > 0)
    //        {
    //            txtAcFullname.Text = dt.Rows[0]["BeneName"].ToString();
    //            txtMobileNo.Text = dt.Rows[0]["Mobileno"].ToString();
    //            txtBankName.Text = dt.Rows[0]["BankName"].ToString();
    //            txtAccountNumber.Text = dt.Rows[0]["AccountNo"].ToString();
    //            txtIFSCCode.Text = dt.Rows[0]["IFSC"].ToString();
    //            ViewState["ACID"] = dt.Rows[0]["CAcID"].ToString();
    //        }
    //    }
    //    catch
    //    { }
    //}

    //void clearform()
    //{
    //    txtAcFullname.Text = "";
    //    txtMobileNo.Text = "";
    //    txtBankName.Text = "";
    //    txtAccountNumber.Text = "";
    //    txtIFSCCode.Text = "";
    //}


    protected void lnkOldOrderList_Click(object sender, EventArgs e)
    {
        Panel1.Visible = true;
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

    protected void imgAddtowallet_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            BindAddtoWalletDetails(UserId);
            mpe.Show();
            lblpopup.Text = "Please enter your amount";
            txtwalletamount.Visible = true;
            Addwallet.Visible = true;
        }
        catch
        { }
    }

    protected void Addwallet_Click(object sender, EventArgs e)
    {
        try
        {
            Session["TotalAmount"] = txtwalletamount.Text;
            Response.Redirect("~/pay/Default.aspx", false);
            //Response.Redirect("~/addsuccess.aspx", false);
        }
        catch
        { }
    }

    private void BindAddtoWalletDetails(string userid)
    {
        try
        {
            DataTable dt = new DataTable();
            dt = local_wallet.getUserDetails(userid);
            if (dt.Rows.Count > 0)
            {
                Session["Customername"] = dt.Rows[0]["fullname"].ToString();
                Session["CustEmailid"] = dt.Rows[0]["Emailid"].ToString();
                Session["CustIdentifyno"] = dt.Rows[0]["UserIdentifyNo"].ToString();
                Session["walletid"] = dt.Rows[0]["WalletId"].ToString();
                Session["walletamount"] = dt.Rows[0]["Amount"].ToString();
                Session["transactiontype"] = "Add to wallet";
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
}