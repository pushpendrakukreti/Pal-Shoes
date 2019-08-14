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
using System.Text;
using System.Web.Script.Serialization;
using System.Collections.Specialized;
using System.Web.UI.HtmlControls;
using System.Drawing;
using System.Xml;
using System.Configuration;

public partial class pay_PaymentResponse : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                NameValueCollection nvc = Request.Form;

                if (Request.Params["mmp_txn"] != null)
                {
                    string postingmmp_txn = Request.Params["mmp_txn"].ToString();
                    int postingmer_txn = Convert.ToInt32(Request.Params["mer_txn"]);
                    string postinamount = Request.Params["amt"].ToString();
                    string postingprod = Request.Params["prod"].ToString();
                    string postingdate = Request.Params["date"].ToString();
                    string postingbank_txn = Request.Params["bank_txn"].ToString();
                    string postingf_code = Request.Params["f_code"].ToString();
                    string postingbank_name = Request.Params["bank_name"].ToString();
                    string signature = Request.Params["signature"].ToString();
                    string postingdiscriminator = Request.Params["discriminator"].ToString();

                    string respHashKey = "KEYRESP123657234";
                    string ressignature = "";
                    string strsignature = postingmmp_txn + postingmer_txn + postingf_code + postingprod + postingdiscriminator + postinamount + postingbank_txn;
                    //string strsignature = postingmmp_txn + postingmer_txn1 + postingf_code + postingprod + discriminator + postinamount + postingbank_txn;
                    byte[] bytes = Encoding.UTF8.GetBytes(respHashKey);
                    byte[] b = new System.Security.Cryptography.HMACSHA512(bytes).ComputeHash(Encoding.UTF8.GetBytes(strsignature));
                    ressignature = byteToHexString(b).ToLower();

                    if (signature == ressignature)
                    {
                        lblStatus.Text = "Signature matched...";

                    }
                    else
                    {
                        lblStatus.Text = "Signature Mismatched...";
                    }
                }
            }
        }
        catch (Exception ex)
        {
            lblStatus.Text = ex.Message;
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