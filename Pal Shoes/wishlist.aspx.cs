﻿using System;
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

public partial class wishlist : System.Web.UI.Page
{
    Catergoy local_category = new Catergoy();
    SubCategory local_subcategory = new SubCategory();
    Products local_product = new Products();
    ShoppingCart local_cart = new ShoppingCart();
    OrderDetails local_orderlist = new OrderDetails();
    WalletCls local_wallet = new WalletCls();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Session["userid"] as string))
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
                string userid = Session["userid"].ToString();
                BindWishlist(userid);
                BindWalletAmount();
            }
            if (Session["userid"] != null)
            {
                pnlwishlist.Visible = true;
            }
            BindLoginLogout();
            BindCategoryMenu();
            BindAccessories();
        }
        else
        {
            Response.Redirect("~/login.aspx");
        }
    }

    private void BindWishlist(string userid)
    {
        try
        {
            DataTable dt = new DataTable();
            dt = local_orderlist.getWishlist(userid);
            if (dt.Rows.Count > 0)
            {
                ddShoppingCart.DataSource = dt;
                ddShoppingCart.DataBind();
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

    
    protected void ddShoppingCart_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Delete")
            {
                int id = Convert.ToInt32(e.CommandArgument.ToString());
                int result = 0;
                result = local_orderlist.DeletWishlist(id);

                if (result > 0)
                {
                    string userid = Session["userid"].ToString();
                    BindWishlist(userid);
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

    protected void btnRecomended_Click(object sender, EventArgs e)
    {
        if (txtRecomended.Text != "")
        {
            string productname = txtRecomended.Text;
            string userid = Session["userid"].ToString();
            int result = 0;

            if (local_orderlist.IsRecomendedExist(productname, userid, "N", "") == "F")
            {
                result = local_orderlist.AddRecomendedProduct(productname, userid);

                if (result != 0)
                {
                    mpe.Show();
                    lblpopup.Text = "Recomended Product Request Send Successfully!";
                    //lblMsg.ForeColor = Color.Green;
                    //lblMsg.Text = "Recomended Product Request Send Successfully!";
                    txtRecomended.Text = "";
                }
            }
            else
            {
                mpe.Show();
                lblpopup.Text = "Recomended Product Already Request Submited?";
                //lblMsg.ForeColor = Color.Red;
                //lblMsg.Text = "Recomended Product Already Request Submited?";
            }
        }
        else
        {
            lblMsg.ForeColor = Color.Red;
            lblMsg.Text = "Enter Recomended Product Name<br />";
        }
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

