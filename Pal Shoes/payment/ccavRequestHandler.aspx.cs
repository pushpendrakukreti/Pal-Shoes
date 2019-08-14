using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CCA.Util;
using System.Configuration;

public partial class SubmitData : System.Web.UI.Page
{
    CCACrypto ccaCrypto = new CCACrypto();
    string MerchantId = ConfigurationManager.AppSettings["MerchantId"];
    string workingKey = ConfigurationManager.AppSettings["WorkingKey"];//put in the 32bit alpha numeric key in the quotes provided here.
    
    //string ccaRequest = "";
    public string strEncRequest = "";
    public string strAccessCode = ConfigurationManager.AppSettings["AccessCode"];// put the access key in the quotes provided here.

    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!IsPostBack)
        //{
        //    BindData();

        //    foreach (string name in Request.Form)
        //    {
        //        if (name != null)
        //        {
        //            if (!name.StartsWith("_"))
        //            {
        //                ccaRequest = ccaRequest + name + "=" + Request.Form[name] + "&";
        //                /* Response.Write(name + "=" + Request.Form[name]);
        //                  Response.Write("</br>");*/
        //            }
        //        }
        //    }
        //    strEncRequest = ccaCrypto.Encrypt(ccaRequest, workingKey);

        //    lb_Message.Text = "Wait... Redirecting to Payment Gateway.";
        //}
        //else
        //{
        //    lb_Message.Text = "Error on Payment Gateway. Kindly try again.";
        //}

        if (!IsPostBack)
        {
            string ResponseUrl = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) +
                       HttpContext.Current.Request.ApplicationPath + "payment/ccavResponseHandler.aspx";

            lbltid.Text = Session["Tid"].ToString();
            lblMerchantId.Text = Convert.ToString(Session["MerchantId"]);
            lblOrderId.Text = Convert.ToString(Session["OrderId"]);
            lblAmount.Text = Convert.ToString(Session["TotalAmount"]);
            lblcurrency.Text = Session["currency"].ToString();
            lblRedirectUrl.Text = Convert.ToString(Session["redirecturl"]);
            lblCancelUrl.Text = Convert.ToString(Session["cancelurl"]);
            lblCustomerName.Text = Convert.ToString(Session["cutomername"]);
            lblCustAddr.Text = Convert.ToString(Session["BillingAddress"]);
            lblCustCity.Text = Convert.ToString(Session["shipcity"]);
            lblCustState.Text = Convert.ToString(Session["shipstate"]);
            lblZipCode.Text = Convert.ToString(Session["shippincode"]);
            lblCustCountry.Text = "INDIA";
            lblCustPhone.Text = Convert.ToString(Session["BillingMobile"]);
            lblCustEmail.Text = Convert.ToString(Session["CustEmailid"]);

            string Res = ccaCrypto.getchecksum(lblMerchantId.Text, lblOrderId.Text, lblAmount.Text, ResponseUrl, workingKey);
            string ToEncrypt = "order_id=" + lblOrderId.Text + "&currency=" + lblcurrency.Text + "&amount=" + lblAmount.Text + "&merchant_id=" + lblMerchantId.Text + "&redirect_url=" + ResponseUrl + "&cancel_url=" + lblCancelUrl.Text + "&language=en" + "&checksum=" + Res + "&billing_name=" + lblCustomerName.Text + "&billing_address=" + lblCustAddr.Text + "&billing_city=" + lblCustCity.Text + "&billing_state=" + lblCustState.Text + "&billing_zip=" + lblZipCode.Text + "&billing_country=" + lblCustCountry.Text + "&billing_tel=" + lblCustPhone.Text + "&billing_email=" + lblCustEmail.Text;

            strEncRequest = ccaCrypto.Encrypt(ToEncrypt, workingKey);

            encRequest.Value = strEncRequest;
            access_code.Value = strAccessCode;

            lb_Message.Text = "Wait... Redirecting to Payment Gateway.";
        }
        else
        {
            lb_Message.Text = "Error on Payment Gateway. Kindly try again.";
        }
    }
}