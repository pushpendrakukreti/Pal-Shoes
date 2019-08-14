using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using SqlAccess;
using System.Drawing;
using System.Net.Mail;
using System.Net;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Web.Services;
using System.Collections;
using System.Text;

public partial class addsuccess : System.Web.UI.Page
{
    Catergoy local_category = new Catergoy();
    SubCategory local_subcategory = new SubCategory();
    Products local_product = new Products();
    ShoppingCart local_cart = new ShoppingCart();
    OrderDetails local_orderlist = new OrderDetails();

    WalletCls ob_wallet = new WalletCls();
    BEL ob_bel = new BEL();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Convert.ToString(Session["userid"])) && Session["logintype"].ToString() != "Admin")
        {
            if (!IsPostBack)
            {
                Session["currency"] = "INR";
                if (!string.IsNullOrEmpty(Session["Transid"] as string))
                {
                    BintCartItemsPrice();
                }
                BintCartItems();
                BindWalletAmount();

                AddAmountToWallet();//add to wallet
            }
            BindCategoryMenu();
            BindLoginLogout();
            BindAccessories();

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
    private void AddAmountToWallet()
    {
        if (Session["walletamount"] != null)
        {
            ob_bel.WalletId = Convert.ToInt32(Session["walletid"]);
            ob_bel.UserId = Convert.ToInt32(Session["userid"]);
            ob_bel.Amount = Convert.ToDecimal(Session["walletamount"]);
            ob_bel.AddedAmount = Convert.ToDecimal(Session["TotalAmount"]);
            ob_bel.Customername = Session["Customername"].ToString();
            ob_bel.EmailId = Session["CustEmailid"].ToString();
            ob_bel.UserIdetifyNo = Session["CustIdentifyno"].ToString();
            ob_bel.PaymentType = Session["transactiontype"].ToString();
            ob_bel.TransactionNo = 0;
            ob_bel.WalletStatus = "";

            int user = Convert.ToInt32(Session["userid"]);
            ob_bel.ActionBy = user;

            ShoppingCart obj_bll = new ShoppingCart();
            int result = 0;
            try
            {
                result = obj_bll.AddMoney(ob_bel);

                if (result != 0)
                {
                    string useridentifyno = Session["CustIdentifyno"].ToString();
                    if (useridentifyno != null)
                    {
                        SendMsg(useridentifyno);
                    }
                    
                    lblmessage.ForeColor = Color.Green;
                    lblmessage.Text = "Amount added successfully!<br/><br/>" + Session["TotalAmount"].ToString() + " rupees added into your wallet.";

                    Session["Customername"] = null;
                    Session["CustEmailid"] = null;
                    Session["CustIdentifyno"] = null;
                    Session["walletid"] = null;
                    Session["walletamount"] = null;
                    Session["transactiontype"] = null;
                    Session["TotalAmount"] = null;
                }
            }
            catch
            { }
        }
        else
        {
            Response.Redirect("account.aspx");
        }
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
            dt = ob_wallet.getWalletIconAmount(user);
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


    public void SendMsg(string mobileno)
    {
        try
        {
            string key = null;
            string Sender_id = null;
            string contactnumber = mobileno;
            string Message = null;
            key = "45A27FD357A379";
            Sender_id = "NZFUND";
            Message = "Dear Palshoes User " + Session["Customername"].ToString() + ", " + Session["TotalAmount"].ToString() + " rupees added into your wallet.";
            SendSMS(key, contactnumber, Sender_id, Message);
        }
        catch
        { }
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