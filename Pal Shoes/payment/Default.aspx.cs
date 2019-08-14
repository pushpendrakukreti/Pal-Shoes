using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SqlAccess;
using System.Configuration;
using System.Net;

public partial class ccAvenue_Default : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindProcessData();
        }
    }

    private void BindProcessData()
    {
        try
        {
            merchant_id.Text = ConfigurationManager.AppSettings["MerchantId"];
            order_id.Text = Convert.ToString(Session["OrderId"]);
            amount.Text = Convert.ToString(Session["TotalAmount"]);
            currency.Text = Convert.ToString(Session["currency"]);
            redirect_url.Text = "http://palshoes.com/order-complete.aspx";
            cancel_url.Text = "http://palshoes.com/Default.aspx";
            billing_name.Text = Convert.ToString(Session["shipfullname"]);
            billing_address.Text = Convert.ToString(Session["shipaddress"]);
            billing_city.Text = Convert.ToString(Session["shipcity"]);
            billing_state.Text = Convert.ToString(Session["shipstate"]);
            billing_zip.Text = Convert.ToString(Session["shippincode"]);
            billing_country.Text = "INDIA";
            billing_tel.Text = Convert.ToString(Session["shipcontact"]);
            billing_email.Text = Convert.ToString(Session["CustEmailid"]);
        }
        catch
        { }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            Session["Tid"] = tid.Value;
            Session["Merchantid"] = merchant_id.Text;
            Session["redirecturl"] = redirect_url.Text;
            Session["cancelurl"] = cancel_url.Text;
            //Session["cutomername"] = Session["fname"] + " " + Session["lname"];
            Session["cutomername"] = Session["shipfullname"];

            Response.Redirect("ccavRequestHandler.aspx");
        }
        catch(Exception ex)
        { throw new Exception(ex.Message); }
    }
}