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
using System.Text;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections;

public partial class login : System.Web.UI.Page
{
    chkLogin local_login = new chkLogin();
    ShoppingCart local_cart = new ShoppingCart();
    Products local_product = new Products();
    Catergoy local_category = new Catergoy();
    SubCategory local_subcategory = new SubCategory();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Session["Transid"] as string))
            {
                BintCartItemsPrice();
                guest1.Visible = true;
                btnGuestLogin.Visible = true;
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

            if (Request.Cookies["user"] != null)
            {
                txtUsername.Text = Request.Cookies["user"].Value;
            }
            if (Request.Cookies["pwd"] != null)
            {
                txtPassword.Attributes.Add("value", Request.Cookies["pwd"].Value);
            }
            if (Request.Cookies["user"] != null && Request.Cookies["pwd"] != null)
            {
                CheckBox1.Checked = true;
            }
        }
        if (Session["userid"] != null)
        {
            pnlwishlist.Visible = true;
            Panel1.Visible = false;
            pnlMyaccount.Visible = true;
        }
        BindCategoryMenu();
        BindLoginLogout();
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
            Response.Redirect("~/Default.aspx", false);
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

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        try
        {
            string Result = "";
            Result = local_login.getuserInfo(Convert.ToString(txtUsername.Text), Convert.ToString(txtPassword.Text));
            if (Result.IndexOf("~") >= 0)
            {
                Session["loginid"] = Convert.ToString(Result.Split('~')[0]);
                Session["username"] = Convert.ToString(Result.Split('~')[1]);
                Session["logintype"] = Convert.ToString(Result.Split('~')[2]);
                Session["userid"] = Convert.ToString(Result.Split('~')[3]);

                if (CheckBox1.Checked == true)
                {
                    Response.Cookies["user"].Value = txtUsername.Text;
                    Response.Cookies["pwd"].Value = txtPassword.Text;
                    Response.Cookies["user"].Expires = DateTime.Now.AddDays(30);
                    Response.Cookies["pwd"].Expires = DateTime.Now.AddDays(30);
                }
                else
                {
                    Response.Cookies["user"].Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies["pwd"].Expires = DateTime.Now.AddDays(-1);
                }


                if (Session["logintype"].ToString() != "Admin")
                {
                    if (!string.IsNullOrEmpty(Session["Transid"] as string))
                    {
                        string transid = Convert.ToString(Session["Transid"]);
                        DataTable dt2 = new DataTable();
                        dt2 = local_login.getCartIdOnTransactionId(transid);
                        if (dt2.Rows.Count > 0)
                        {
                            for (int l = 0; l < dt2.Rows.Count; l++)
                            {
                                string cartid = Convert.ToString(dt2.Rows[l]["CartId"]);
                                string userid = Convert.ToString(Session["userid"]);
                                local_login.UpdateUserInShoppingCart(userid, cartid);
                            }
                        }
                    }
                

                    if (Session["checkout"] != null)
                    {
                        Response.Redirect("~/checkout.aspx", false);
                    }
                    else if (Session["cart"] != null)
                    {
                        Response.Redirect("~/cart.aspx", false);
                    }
                    else if (Session["account"] != null)
                    {
                        Response.Redirect("~/account.aspx", false);
                    }
                    else
                    {
                        Response.Redirect("~/Default.aspx", false);
                    }
                }
                else
                {
                    Session.Abandon();
                    Session.Clear();
                    lblMessage1.ForeColor = Color.Red;
                    lblMessage1.Text = "Invalid User/Password";
                }
            }
            else
            {
                lblMessage1.ForeColor = Color.Red;
                lblMessage1.Text = Result;
            }
        }
        catch (Exception ex)
        {
            lblMessage1.Text = ex.Message;
        }
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

    protected void btnGuestLogin_Click(object sender, EventArgs e)
    {
        Session["username"] = "Guest";
        Session["logintype"] = "Guest";
        
        Response.Redirect("~/checkout.aspx");
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