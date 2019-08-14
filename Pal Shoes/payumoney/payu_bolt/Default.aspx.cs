using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace payu_bolt
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string surl = ((HttpContext.Current.Request.ServerVariables["HTTPS"] != "" && HttpContext.Current.Request.ServerVariables["HTTP_HOST"] != "off") || HttpContext.Current.Request.ServerVariables["SERVER_PORT"] == "443") ? "https://" : "http://";
	        surl += HttpContext.Current.Request.ServerVariables["HTTP_HOST"] + HttpContext.Current.Request.ServerVariables["REQUEST_URI"] + "/Response.aspx";
            Session.Add("surl",surl);

            Random r = new Random();
            string txnid = "Txn" + r.Next(100, 9999);
            Session.Add("txnid", txnid);

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
                redirect_url.Text = "http://paulshoes.in/order-complete.aspx";
                cancel_url.Text = "http://paulshoes.in/Default.aspx";
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
            catch (Exception ex)
            { throw new Exception(ex.Message); }
        }

    }


}
