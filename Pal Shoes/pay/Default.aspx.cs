using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SqlAccess;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Text;
using System.Web.Script.Serialization;
using System.Collections.Specialized;
using System.Web.UI.HtmlControls;
using System.Drawing;
using System.Xml;
using System.Configuration;

public partial class pay_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string MerchantLogin = "197";
        string MerchantPass = "Test@123";
        string TransactionType = "NBFundtransfer";
        string ProductID = "NSE";
        string TransactionID = Convert.ToString(Session["Transid"]);
        string TransactionAmount = Convert.ToString(Session["TotalAmount"]);
        string TransactionCurrency = Convert.ToString(Session["currency"]);
        string ClientCodes = "007";
        string CustomerAccountNo = Convert.ToString(Session["OrderId"]);
        string TransactionServiceCharge = "0";
        string TransactionDateTime = System.DateTime.Now.ToString();

        TransferFund(MerchantLogin, MerchantPass, ProductID, ClientCodes, CustomerAccountNo, TransactionType, TransactionAmount, TransactionCurrency, TransactionServiceCharge, TransactionID, TransactionDateTime);

    }

    public static void TransferFund(string MerchantLogin, string MerchantPass, string ProductID, string ClientCode, string CustomerAccountNo, string TransactionType, string TransactionAmount, string TransactionCurrency, string TransactionServiceCharge, string TransactionID, string TransactionDateTime)
    {

        string strURL, strClientCode, strClientCodeEncoded;
        byte[] b;
        string strResponse = "";
        string ru = "http://paulshoes.com/order-complete.aspx";
        //string ru = "http://paulshoes.com/login.aspx";

        try
        {
            b = Encoding.UTF8.GetBytes(ClientCode);
            strClientCode = Convert.ToBase64String(b);
            strClientCodeEncoded = HttpUtility.UrlEncode(strClientCode);
            strURL = "" + ConfigurationManager.AppSettings["TransferURL"].ToString();///
            strURL = strURL.Replace("[MerchantLogin]", MerchantLogin + "&");
            strURL = strURL.Replace("[MerchantPass]", MerchantPass + "&");
            strURL = strURL.Replace("[TransactionType]", TransactionType + "&");
            strURL = strURL.Replace("[ProductID]", ProductID + "&");
            strURL = strURL.Replace("[TransactionAmount]", TransactionAmount + "&");
            strURL = strURL.Replace("[TransactionCurrency]", TransactionCurrency + "&");
            strURL = strURL.Replace("[TransactionServiceCharge]", TransactionServiceCharge + "&");
            strURL = strURL.Replace("[ClientCode]", strClientCodeEncoded + "&");
            strURL = strURL.Replace("[TransactionID]", TransactionID + "&");
            strURL = strURL.Replace("[TransactionDateTime]", TransactionDateTime + "&");
            strURL = strURL.Replace("[CustomerAccountNo]", CustomerAccountNo + "&");
            //strURL = strURL.Replace("[MerchantDiscretionaryData]", MerchantDiscretionaryData + "&");
            //strURL = strURL.Replace("[BankID]", BankID + "&");
            strURL = strURL.Replace("[ru]", ru + "&");// Remove on Production

            //  string reqHashKey = requestkey;
            string reqHashKey = "KEY123657234";
            string signature = "";
            string strsignature = MerchantLogin + MerchantPass + TransactionType + ProductID + TransactionID + TransactionAmount + TransactionCurrency;
            byte[] bytes = Encoding.UTF8.GetBytes(reqHashKey);
            byte[] bt = new System.Security.Cryptography.HMACSHA512(bytes).ComputeHash(Encoding.UTF8.GetBytes(strsignature));
            // byte[] b = new HMACSHA512(bytes).ComputeHash(Encoding.UTF8.GetBytes(prodid));
            signature = byteToHexString(bt).ToLower();
            //ExceptionLogger.LogExceptionDetails(null, "[Log]" + signature);
            strURL = strURL.Replace("[signature]", signature);

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls; //| SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12; // comparable to modern browsers

            HttpContext.Current.Response.Redirect(strURL, false);

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

    }

    public static string byteToHexString(byte[] byData)
    {
        StringBuilder sb = new StringBuilder((byData.Length * 2));
        for (int i = 0; (i < byData.Length); i++)
        {
            int v = (byData[i] & 255);
            if ((v < 16))
            {
                sb.Append('0');
            }

            sb.Append(v.ToString("X"));

        }

        return sb.ToString();
    }
}
    
