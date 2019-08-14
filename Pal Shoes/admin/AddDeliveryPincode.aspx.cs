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

public partial class admin_AddDeliveryPincode : System.Web.UI.Page
{
    Products local_product = new Products();
    ShoppingCart local_cart = new ShoppingCart();
    OrderDetails local_orderlist = new OrderDetails();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Convert.ToString(Session["userid"])) && Convert.ToString(Session["logintype"]) == "Admin")
        {
            if (!IsPostBack)
            {
                BindContry();
                BindState();
                BindDeliveryPostcode();
            }
        }
        else
        {
            Response.Redirect("Login.aspx");
        }
    }

    private void BindDeliveryPostcode()
    {
        DataTable dt = new DataTable();
        try
        {
            dt = local_product.getDeliveryPostcode();
            if (dt.Rows.Count > 0)
            {
                gvRegistration.DataSource = dt;
                gvRegistration.DataBind();
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
            }
        }
        catch
        { }
    }

    protected void btnCheckDelivery_Click(object sender, EventArgs e)
    {
        try
        {
            string country = ddCountry.SelectedValue;
            string state = ddState.SelectedValue;
            string postcode = txtPostcodezip.Text;
            int result = 0;

            if (local_product.IsExistsPostCode(postcode, "N", "") == "F")
            {
                result = local_product.AddPostcodeDelivery(country, state, postcode);
                if (result != 0)
                {
                    lblMessage.ForeColor = Color.Green;
                    lblMessage.Text = "Delivery postcode added successfully!";
                    txtPostcodezip.Text = "";
                    BindDeliveryPostcode();
                }
            }
            else
            {
                lblMessage.ForeColor = Color.Red;
                lblMessage.Text = "Postcode already exists!";
            }
        }
        catch (Exception ex)
        {
            lblMessage.ForeColor = Color.Red;
            lblMessage.Text = ex.Message;
        }
    }
    protected void gvRegistration_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvRegistration.PageIndex = e.NewPageIndex;
        BindDeliveryPostcode();
    }
}