using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Collections.Specialized;
using CCA.Util;
using System.Data;
using System.Configuration;

public partial class ResponseHandler : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //string workingKey = "";//put in the 32bit alpha numeric key in the quotes provided here
        //CCACrypto ccaCrypto = new CCACrypto();
        //string encResponse = ccaCrypto.Decrypt(Request.Form["encResp"], workingKey);
        //NameValueCollection Params = new NameValueCollection();
        //string[] segments = encResponse.Split('&');
        //foreach (string seg in segments)
        //{
        //    string[] parts = seg.Split('=');
        //    if (parts.Length > 0)
        //    {
        //        string Key = parts[0].Trim();
        //        string Value = parts[1].Trim();
        //        Params.Add(Key, Value);
        //    }
        //}

        //for (int i = 0; i < Params.Count; i++)
        //{
        //    Response.Write(Params.Keys[i] + " = " + Params[i] + "<br>");
        //}

        if (!IsPostBack)
        {
            string WorkingKey = ConfigurationManager.AppSettings["WorkingKey"]; // PROVIDED BY ccAvenue
            CCACrypto func = new CCACrypto();
            string encResponse = Request.Form.ToString();
            string[] segments = encResponse.Split('&');
            List<string> Response_values = new List<string>((func.Decrypt(segments[0].Split('=')[1], WorkingKey).Split('&')).ToList());
            Response_values.Add(segments[1]);
            NameValueCollection Params = new NameValueCollection();

            foreach (string seg in Response_values)
            {
                string[] parts = seg.Split('=');
                if (parts.Length > 0)
                {
                    string Key = parts[0].Trim();
                    string Value = parts[1].Trim();

                    Params.Add(Key, Value);
                }
            }
            //for (int i = 0; i < Params.Count; i++)
            //{
            //    Response.Write(Params.Keys[i] + " = " + Params[i] + "<br>");
            //}

            try
            {
                //Verify with ccAvenue


                //string strVerify = func.verifychecksum(Params["order_id"].ToString(), Params["tracking_id"].ToString(), Params["amount"].ToString(), Params["order_status"].ToString(),Params["payment_mode"].ToString(), Params["vault"].ToString());

                //if (strVerify.ToUpper() == "TRUE")
                // {
                if (Session["TotalAmount"].ToString() != null)
                {
                    // UPDATE THE PAYMENT STATUS IN DB

                    //Update the Payment Status in DB
                    if (Params["order_status"].ToString() == "Success")

                        Response.Redirect("http://palshoes.com/order-complete.aspx", false);
                    else
                        Response.Redirect("http://palshoes.com/404.html", false);

                    // REDIRECT TO SUCCESS PAGE OR SHOW THE PAYMENT RESPONSE ON SAME PAGE

                }
                else
                {
                    Response.Redirect("http://palshoes.com/404.html", false);
                    // PAYMENT IS FAIL
                    // REDIRECT TO ERROR PAGE
                }

                //}
                //else
                //{
                //  Response.Redirect("http://palshoes.com/404.html", false);
                // PAYMENT VERIFICATION IS FAILED
                // REDIRECT TO ERROR PAGE
                // }
            }
            catch (Exception ex)
            {
                // ERROR IN PROCESSING
                // REDIRECT TO ERROR PAGE
                Response.Write("<script>alert('" + ex.Message + "')</script>");
            }
        }
    }
}