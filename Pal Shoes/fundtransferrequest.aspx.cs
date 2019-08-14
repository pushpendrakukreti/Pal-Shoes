using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using SqlAccess;
using System.IO;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;
using System.Collections.Specialized;
using System.Web.UI.HtmlControls;
using System.Drawing;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Web.Services;
using System.Collections;

public partial class fundtransferrequest : System.Web.UI.Page
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

        if (!string.IsNullOrEmpty(Convert.ToString(Session["userid"])) && Session["logintype"].ToString() != "Admin")
        {
            UserId = Session["userid"].ToString();
            if (!IsPostBack)
            {
                //BindPaymentType();
                BindRequstFundTransfer();
                //BindRequstPayAmount();
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
    

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("account.aspx");
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string fullname = txtFullName.Text;
            string mobileno = txtContactNumber.Text;
            decimal amount = Convert.ToDecimal(txtAmount.Text);
            string requestdate = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string requesttype = "Fund Transfer";
            string requStatus = "Waiting";
            bool request = false;
            Int32 userid = Convert.ToInt32(Session["userid"]);
            long useridentifyno = GetUserIdentifyNo(UserId);
            string systemip = GetIPAddress();
            int result = 0;

            result = local_wallet.AddTransferRequest(userid, amount, requestdate, requesttype, requStatus, request, useridentifyno, systemip);
            if (result != 0)
            {
                
                BindRequstFundTransfer();
                txtFullName.Text = "";
                txtAmount.Text = "";
                txtContactNumber.Text = "";
                mpe.Show();
                lblpopup.Text = "Request send successfully,<br/> it will be take 24 houres.";
                //lblMessage.ForeColor = Color.Green;
                //lblMessage.Text = "Request send successfully, it will be take 24 houres.";
            }
        }
        catch (Exception ex)
        {
            lblMessage.ForeColor = Color.Red;
            lblMessage.Text = ex.Message;
        }
    }


    private long GetUserIdentifyNo(string userid)
    {
        DataTable dt = new DataTable();
        long useridetity = -1;
        try
        {
            dt = local_wallet.getUserIdentifyno(userid);
            if (dt.Rows.Count > 0)
            {
                useridetity = Convert.ToInt64(dt.Rows[0]["UserIdentifyNo"]);
            }
        }
        catch
        { }
        return useridetity;
    }

    private void BindRequstFundTransfer()
    {
        try
        {
            DataTable dt = new DataTable();
            string user = Session["userid"].ToString();
            dt = local_wallet.getRequestFundTransfer(user);
            if (dt.Rows.Count > 0)
            {
                gvFundTransfer.DataSource = dt;
                gvFundTransfer.DataBind();
            }
        }
        catch
        { }
    }


    public string GetIPAddress()
    {
        string IPAddress = "";
        IPHostEntry Host = default(IPHostEntry);
        string Hostname = null;
        Hostname = System.Environment.MachineName;
        Host = Dns.GetHostEntry(Hostname);
        foreach (IPAddress IP in Host.AddressList)
        {
            if (IP.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
            {
                IPAddress = Convert.ToString(IP);
            }
        }
        return IPAddress;
    }

    protected void gvFundTransfer_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblstatus = (Label)e.Row.FindControl("lblStatus");
                if (lblstatus.Text == "Waiting")
                {
                    lblstatus.ForeColor = Color.Green;
                }
                else if (lblstatus.Text == "Request Cancel")
                {
                    lblstatus.ForeColor = Color.Red;
                }
                else if (lblstatus.Text == "Amount Transfered")
                {
                    lblstatus.ForeColor = Color.Gray;
                }
                else
                {
                    lblstatus.ForeColor = Color.Orange;
                }
            }
        }
        catch
        {
        }
    }



    //Pay Amount
    protected void btnCancel2_Click(object sender, EventArgs e)
    {
        Response.Redirect("account.aspx");
    }

    protected void btnPaySubmit_Click(object sender, EventArgs e)
    {
        //try
        //{
        //    string fullname = txtName.Text;
        //    string mobileno = txtContactno.Text;
        //    decimal amount = Convert.ToDecimal(txtPayamount.Text);
        //    string requestdate = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        //    string requesttype = "Pay Amount";
        //    string requStatus = "Waiting";
        //    bool request = false;
        //    Int32 userid = Convert.ToInt32(Session["userid"]);
        //    long useridentifyno = GetUserIdentifyNo(UserId);
        //    string systemip = GetIPAddress();
        //    string paymenttype = ddPaytype.SelectedItem.ToString();
        //    long transactonno = Convert.ToInt64(txtTransaction.Text);
        //    int result = 0;

        //    result = local_wallet.AddPayAmountRequest(userid, amount, requestdate, requesttype, requStatus, request, useridentifyno, systemip, paymenttype, transactonno);
        //    if (result != 0)
        //    {
        //        lblMessage.ForeColor = Color.Green;
        //        lblMessage.Text = "Request send successfully, it will be take 24 houres.";
        //        BindRequstPayAmount();
        //        clearform2();
        //    }
        //}
        //catch (Exception ex)
        //{
        //    lblMessage.ForeColor = Color.Red;
        //    lblMessage.Text = ex.Message;
        //}
    }

    private void BindRequstPayAmount()
    {
        //try
        //{
        //    DataTable dt = new DataTable();
        //    string user = Session["userid"].ToString();
        //    dt = local_wallet.getRequestPayWalletAmount(user);
        //    if (dt.Rows.Count > 0)
        //    {
        //        gvPayamout.DataSource = dt;
        //        gvPayamout.DataBind();
        //    }
        //}
        //catch
        //{ }
    }

    protected void gvPayamout_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblstatus = (Label)e.Row.FindControl("lblStatus");
                if (lblstatus.Text == "Waiting")
                {
                    lblstatus.ForeColor = Color.Green;
                }
                else if (lblstatus.Text == "Request Cancel")
                {
                    lblstatus.ForeColor = Color.Red;
                }
                else if (lblstatus.Text == "Amount Added")
                {
                    lblstatus.ForeColor = Color.Gray;
                }
                else
                {
                    lblstatus.ForeColor = Color.Orange;
                }
            }
        }
        catch
        {
        }
    }

    //void clearform2()
    //{
    //    txtName.Text = "";
    //    txtContactno.Text = "";
    //    txtPayamount.Text = "";
    //    txtTransaction.Text = "";
    //    ddPaytype.SelectedIndex = 0;
    //}

    //private void BindPaymentType()
    //{
    //    ddPaytype.Items.Add("Chaque");
    //    ddPaytype.Items.Add("NEFT/IMPS");
    //    ddPaytype.Items.Add("RTGS");
    //    ddPaytype.DataBind();
    //    ddPaytype.Items.Insert(0, "Select PaymentType");
    //}


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