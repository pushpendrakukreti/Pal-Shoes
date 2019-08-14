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

public partial class fixamount : System.Web.UI.Page
{
    Catergoy local_category = new Catergoy();
    SubCategory local_subcategory = new SubCategory();
    Products local_product = new Products();
    ShoppingCart local_cart = new ShoppingCart();
    OrderDetails local_orderlist = new OrderDetails();
    WalletCls local_wallet = new WalletCls();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Session["userid"] as string) && Convert.ToString(Session["logintype"])=="User")
        {
            string UserID=Session["userid"].ToString();
            if (!IsPostBack)
            {
                BindToFixAmount(UserID);
                BindFixAmountDetail(UserID);

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
                BindWalletAmount();
            }
            BindLoginLogout();
            BindCategoryMenu();
            BindAccessories();
            if (Session["userid"] != null)
            {
                pnlwishlist.Visible = true;
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

    

    //Fix
    private void BindToFixAmount(string userid)
    {
        DataTable dt = new DataTable();
        try
        {
            dt = local_wallet.getToFixWalletAmount(userid);
            if (dt.Rows.Count > 0)
            {
                rpFixamount.DataSource = dt;
                rpFixamount.DataBind();
            }
        }
        catch
        { }
    }
    protected void rpFixamount_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "FixAmount")
        {
            if ((e.Item.ItemType == ListItemType.Item) || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lblfixamount = (Label)e.Item.FindControl("lblFixamount");
                TextBox txtamount = (TextBox)e.Item.FindControl("txtAmounts");
                if (txtamount.Text == "")
                {
                    lblMessage.ForeColor = Color.Red;
                    lblMessage.Text = "Enter the new value of add amout.";
                    return;
                }
                Label lblwalletamount = (Label)e.Item.FindControl("lblWalletamt");
                decimal walletblce = 0;
                if (lblwalletamount.Text != "0")
                {
                    walletblce = Convert.ToDecimal(lblwalletamount.Text);
                }
                decimal fixamount = 0;
                if (lblfixamount.Text != "")
                {
                    fixamount = Convert.ToDecimal(lblfixamount.Text);
                }
                decimal addedamount = Convert.ToDecimal(txtamount.Text);
                decimal fixamounts = fixamount + addedamount;
                string useridetifyno = GetUserIdentifyNo(Convert.ToString(Session["userid"]));
                Int32 userid = Convert.ToInt32(Session["userid"]);
                decimal percent = getPercetage();
                int result = 0;

                if (walletblce != 0)
                {
                    try
                    {
                        if (local_wallet.IsFixExists(userid) == "T")
                        {
                            result = local_wallet.AddNewFixAmount(fixamounts, addedamount, useridetifyno, userid, percent);
                            local_wallet.UpdateCustomerWalletFundtransfer(addedamount, userid);
                            if (result != 0)
                            {
                                string user = Convert.ToString(Session["userid"]);
                                BindFixAmountDetail(user);
                                BindToFixAmount(user);
                                mpe.Show();
                                lblpopup.Text = "Your amount added to fixed amount.";
                                //lblMessage.ForeColor = Color.Green;
                                //lblMessage.Text = "Your amount added to fixed amount.";
                            }
                        }
                        else
                        {
                            string monthcount = "Month Begin";
                            result = local_wallet.AddNewFixAmountFirst(fixamounts, addedamount, useridetifyno, monthcount, userid, percent);
                            local_wallet.UpdateCustomerWalletFundtransfer(addedamount, userid);
                            if (result != 0)
                            {
                                string user = Convert.ToString(Session["userid"]);
                                BindFixAmountDetail(user);
                                BindToFixAmount(user);
                                mpe.Show();
                                lblpopup.Text = "Your amount added to fixed amount.";
                                //lblMessage.ForeColor = Color.Green;
                                //lblMessage.Text = "Your amount added to fixed amount.";
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        lblMessage.ForeColor = Color.Red;
                        lblMessage.Text = ex.Message;
                    }
                }
                else
                {
                    mpe.Show();
                    lblpopup.Text = "You haven't enough wallet balance to fix amount.";
                    //lblMessage.ForeColor = Color.Red;
                    //lblMessage.Text = "You haven't enough wallet balance to fix amount.";
                }
            }
        }

        if (e.CommandName == "DetachAmount")
        {
            if ((e.Item.ItemType == ListItemType.Item) || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lblfixamount = (Label)e.Item.FindControl("lblFixamount");
                TextBox txtamount = (TextBox)e.Item.FindControl("txtAmounts");
                Label lblwalletamount = (Label)e.Item.FindControl("lblWalletamt");
                if (txtamount.Text == "")
                {
                    lblMessage.ForeColor = Color.Red;
                    lblMessage.Text = "Enter the new value of deduct amount.";
                    return;
                }
                decimal walletblce = 0;
                if (lblwalletamount.Text != "0.00")
                {
                    walletblce = Convert.ToDecimal(lblwalletamount.Text);
                }
                decimal fixamount = 0;
                if (lblfixamount.Text != "")
                {
                    fixamount = Convert.ToDecimal(lblfixamount.Text);
                }
                decimal deductamount = Convert.ToDecimal(txtamount.Text);
                
                string useridetifyno = GetUserIdentifyNo(Convert.ToString(Session["userid"]));
                Int32 userid = Convert.ToInt32(Session["userid"]);
                decimal percent = getPercetage();

                int result = 0;
                try
                {
                    if (local_wallet.IsFixExists(userid) == "T")
                    {
                        if (fixamount >= deductamount)
                        {
                            fixamount = fixamount - deductamount;

                            result = local_wallet.DeductNewFixAmount(fixamount, deductamount, useridetifyno, userid, percent);
                            local_wallet.UpdateCustomerWalletDeductAdd(deductamount, userid);
                            if (result != 0)
                            {
                                string user = Convert.ToString(Session["userid"]);
                                BindFixAmountDetail(user);
                                BindToFixAmount(user);
                                mpe.Show();
                                lblpopup.Text = "Amount detached from fixed amount.";
                                //lblMessage.ForeColor = Color.Green;
                                //lblMessage.Text = "Amount detached from fixed amount.";
                            }
                        }
                        else
                        {
                            mpe.Show();
                            lblpopup.Text = "You have not enough fixed amount to deduct.";
                            //lblMessage.ForeColor = Color.Red;
                            //lblMessage.Text = "You have not enough fixed amount to deduct.";
                        }
                    }
                    else
                    {
                        mpe.Show();
                        lblpopup.Text = "You haven't fixed amount to detach.";
                        //lblMessage.ForeColor = Color.Red;
                        //lblMessage.Text = "You haven't fixed amount to detach.";
                    }
                }
                catch (Exception ex)
                {
                    lblMessage.ForeColor = Color.Red;
                    lblMessage.Text = ex.Message;
                }
            }
        }
    }


    private string GetUserIdentifyNo(string userid)
    {
        DataTable dt = new DataTable();
        string useridetity = "";
        try
        {
            dt = local_wallet.getUserIdentifyno(userid);
            if (dt.Rows.Count > 0)
            {
                useridetity = Convert.ToString(dt.Rows[0]["UserIdentifyNo"]);
            }
        }
        catch
        { }
        return useridetity;
    }



    private void BindFixAmountDetail(string userid)
    {
        DataTable dt = new DataTable();
        try
        {
            dt = local_wallet.getFixAmountDetail(userid);
            if (dt.Rows.Count > 0)
            {
                gvFiexedAmount.DataSource = dt;
                gvFiexedAmount.DataBind();
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

    private decimal getPercetage()
    {
        decimal percet=0;
        try
        {
            DataTable dt = new DataTable();
            dt = local_wallet.getPercentage1();
            if (dt.Rows.Count > 0)
            {
               percet=Convert.ToDecimal(dt.Rows[0]["Percentage"]);
            }
        }
        catch
        { }
        return percet;
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
