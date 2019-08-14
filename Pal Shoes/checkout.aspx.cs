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
using System.Drawing;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections;
using System.Text;


public partial class checkout : System.Web.UI.Page
{
    Catergoy local_category = new Catergoy();
    SubCategory local_subcategory = new SubCategory();
    Products local_product = new Products();
    ShoppingCart local_cart = new ShoppingCart();
    OrderDetails local_orderlist = new OrderDetails();
    WalletCls local_wallet = new WalletCls();
    chkLogin local_login = new chkLogin();

    protected void Page_Load(object sender, EventArgs e)
    {
        Session["checkout"] = "Checkout";

        if (!string.IsNullOrEmpty(Session["username"] as string))
        {
            if (!string.IsNullOrEmpty(Session["Transid"] as string))
            {
                if (!IsPostBack)
                {
                    BindContry();
                    BindState();

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
                    string userid = Convert.ToString(Session["userid"]);
                    if (Session["logintype"].ToString() != "Guest")
                    {
                        BindUserInformation(userid);
                    }
                }
                
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
                    pnlwishlist.Visible = true;
                }
                //ddCurrency.Enabled = false;
                BindLoginLogout();
                BindCategoryMenu();
                BindWalletAmount();
                BindAccessories();
                BindSlideProduct();

                if (rdshipping.Checked == true)
                {
                    pnlShipping.Visible = true;
                    pnlInfomation.Visible = false;
                }
                else if(rdbilling.Checked==true)
                {
                    pnlShipping.Visible = false;
                    pnlInfomation.Visible = true;
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
    private void BindUserInformation(string userid)
    {
        try
        {
            DataTable dt = new DataTable();
            dt = local_cart.getUserInformation(userid);
            if (dt.Rows.Count > 0)
            {
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

    private void BindWalletAmount()
    {
        try
        {
            DataTable dt = new DataTable();
            string user = "";
            if (Session["userid"].ToString()!="Guest")
            {
                user = Session["userid"].ToString();
            }
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

    protected void lnkNext_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Convert.ToString(Session["username"])))
        {
            BEL obj_d = new BEL();
            OrderDetails obj_order = new OrderDetails();
            if (rdbilling.Checked == true)
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

                if (Session["username"].ToString() == "Guest")
                {
                    obj_d.TransId = Session["Transid"].ToString();

                    if (Session["guestcheckout"] == null)
                    {
                        result = obj_order.AddUpdateUserDetails(obj_d);

                        if (result != 0)
                        {
                            Response.Redirect("order-overview.aspx", false);
                            Session["guestcheckout"] = "guest checkout";
                        }
                    }
                    else
                    {
                        obj_order.UpdateUserDetailsGuest(obj_d);//by transid update
                        Response.Redirect("order-overview.aspx", false);
                    }
                }
                else
                {
                    obj_d.UserId = Convert.ToInt32(Session["userid"]);

                    result = obj_order.UpdateUserDetails(obj_d);
                    if (result != 0)
                    {
                        Response.Redirect("order-overview.aspx", false);
                    }
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


    private void BindSlideProduct()
    {
        string productid = GetProductID();
        try
        {
            DataTable dt = new DataTable();
            dt = local_product.getSlideProduct(productid);
            if (dt.Rows.Count > 0)
            {
                rptImages.DataSource = dt;
                rptImages.DataBind();
            }
        }
        catch
        { }
    }

    private string GetProductID()
    {
        string transid = Session["Transid"].ToString();
        string productid = "";
        try
        {
            DataTable dt = new DataTable();
            dt = local_cart.getProductId(transid);
            if (dt.Rows.Count > 0)
            {
                productid = dt.Rows[0]["ProductId"].ToString();
            }
        }
        catch
        { }
        return productid;
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

    protected void rdbilling_CheckedChanged(object sender, EventArgs e)
    {
        if (rdbilling.Checked == true)
        {
            BindSlideProduct();
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
    }

    protected void rdshipping_CheckedChanged(object sender, EventArgs e)
    {
        if (rdshipping.Checked == true)
        {
            txtFullName.Text = "";
            txtEmailAddress.Text = "";
            txtShippingAddress.Text = "";
            txtShippingLandmark.Text = "";
            txtCompany.Text = "";
            txtContactNumber.Text = "";
            txtPostcodezip.Text = "";
            txtSelectCity.Text = "";
            BindSlideProduct();
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