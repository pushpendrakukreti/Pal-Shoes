using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using System.Text;
using System.Security.Cryptography;

namespace payu_bolt
{
    public partial class Hash : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            byte[] hash;
            string postData = new System.IO.StreamReader(Request.InputStream).ReadToEnd();
            dynamic data = JsonConvert.DeserializeObject(postData);
            string d = data.key+"|"+ data.txnid+"|"+ data.amount + "|" + data.pinfo + "|" + data.fname + "|" + data.email+"|||||"+ data.udf5+"||||||"+ data.salt;
            var datab = Encoding.UTF8.GetBytes(d);
            using (SHA512 shaM = new SHA512Managed())
            {
                hash = shaM.ComputeHash(datab);
            }
            
            
            string json = "{\"success\":\"" +GetStringFromHash(hash) + "\"}";
            Response.Clear();
            Response.ContentType = "application/json; charset=utf-8";
            Response.Write(json);
            Response.End();
        }

        private static string GetStringFromHash(byte[] hash)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                result.Append(hash[i].ToString("X2").ToLower());
            }
            return result.ToString();
        }
    }
}