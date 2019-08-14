using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;

namespace payu_bolt
{
    public partial class Response : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Write("<h2>BOLT Payment Response</h2>");
            Response.Write("Key: "+Request.Form["key"]+"<br />");
            Response.Write("Salt: " + Request.Form["salt"] + "<br />");
            Response.Write("Txnid: " + Request.Form["txnid"] + "<br />");
            Response.Write("Amount: " + Request.Form["amount"] + "<br />");
            Response.Write("Product Info: " + Request.Form["productinfo"] + "<br />");
            Response.Write("First Name: " + Request.Form["firstname"] + "<br />");
            Response.Write("Email: " + Request.Form["email"] + "<br />");
            Response.Write("Myhpayid: " + Request.Form["mihpayid"] + "<br />");
            Response.Write("Status: " + Request.Form["status"] + "<br />");
            Response.Write("UDF5: " + Request.Form["udf5"] + "<br />");
            Response.Write("Hash: " + Request.Form["hash"] + "<br />");

        }
    }
}