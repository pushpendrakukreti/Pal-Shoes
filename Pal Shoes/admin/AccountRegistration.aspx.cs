using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using SqlAccess;
using System.IO;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;
using System.Collections.Specialized;
using System.Web.UI.HtmlControls;
using System.Drawing;

public partial class AccountRegistration : System.Web.UI.Page
{
    WalletCls obj_wallet = new WalletCls();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Convert.ToString(Session["userid"])) && Convert.ToString(Session["logintype"]) == "Admin")
        {
            if (!IsPostBack)
            {
                BindUserIdentifyno();
            }
        }
        else
        {
            Response.Redirect("Login.aspx");
        }
    }
    public class CustRegistraion
    {
        public int UserId { get; set; }
        public string BeneficiaryType { get; set; }
        public string BankAccNo { get; set; }
        public string ConfirmBankAccNo { get; set; }
        public string BeneficiaryName { get; set; }
        public string SenderMobile { get; set; }
        public string IfscCode { get; set; }
        public string otc { get; set; }
        public string requestNo { get; set; }
        public string otcRef { get; set; }
        public string benfCode { get; set; }
        public string Type { get; set; }
        public string ErrorMsg { get; set; }
        public string Message { get; set; }
        public string Request { get; set; }
        public string Response { get; set; }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string uid = Configuration.SuvidhaUser;
            string pin = Configuration.SuvidhaPin;
            string fname = txtFirstname.Text;
            string lname = txtLastname.Text;
            string mobileno = txtMobileno.Text;
            string URL = "http://recharge.suvidhaarecharge.com/api/MoneyTransfer/CustomerRegistration?Username=" + uid + "&Password=" + pin + "&Mobile=" + mobileno + "&FName=" + fname + "&LName=" + lname + "";

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(URL);

            string postData = "http://recharge.suvidhaarecharge.com/api/MoneyTransfer/CustomerRegistration?Username=" + uid + "&Password=" + pin + "&Mobile=" + mobileno + "&FName=" + fname + "&LName=" + lname + "";
            httpWebRequest.Method = WebRequestMethods.Http.Post;
            httpWebRequest.ContentType = "application/x-www-form-urlencoded";
            httpWebRequest.ContentLength = postData.Length;

            using (StreamWriter writer = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                writer.Write(postData);
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                JavaScriptSerializer js = new JavaScriptSerializer();
                CustRegistraion cr = js.Deserialize<CustRegistraion>(result);
                int user_id = cr.UserId;
                string requestno = cr.requestNo;
                string errormsg = cr.ErrorMsg;
                Session["OtcRef"] = requestno;
                Session["Mobileno"] = mobileno;

                if (errormsg != null)
                {
                    lblMessage.ForeColor = Color.Red;
                    lblMessage.Text = errormsg;
                }
                else
                {
                    Int32 custid = Convert.ToInt32(ddUserIDno.SelectedValue);
                    string userIdentyno = ddUserIDno.SelectedItem.ToString();

                    obj_wallet.AddAPIcustomerRegistration(user_id, requestno, custid, userIdentyno);
                    lblMessage.ForeColor = Color.Green;
                    lblMessage.Text = "OTP send on registered mobile";
                }
                txtFirstname.Text = "";
                txtLastname.Text = "";
                txtMobileno.Text = "";
            }
        }
        catch
        { }
    }


    protected void btnOTP_Click(object sender, EventArgs e)
    {
        try
        {
            string uid = Configuration.SuvidhaUser;
            string pin = Configuration.SuvidhaPin;
            string otcref = "";
            if (Session["OtcRef"] != null)
            {
                otcref = Session["OtcRef"].ToString();
            }
            string otc = txtOtp.Text;
            string mobileno = "";
            if (Session["Mobileno"] != null)
            {
                mobileno = Session["Mobileno"].ToString();
            }
            string URL = "http://recharge.suvidhaarecharge.com/api/MoneyTransfer/VerifyCustomerRegOtc?Username=" + uid + "&Password=" + pin + "&Mobile=" + mobileno + "&OtcRef=" + otcref + "&Otc=" + otc + "";

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(URL);

            string postData = "http://recharge.suvidhaarecharge.com/api/MoneyTransfer/VerifyCustomerRegOtc?Username=" + uid + "&Password=" + pin + "&Mobile=" + mobileno + "&OtcRef=" + otcref + "&Otc=" + otc + "";
            httpWebRequest.Method = WebRequestMethods.Http.Post;
            httpWebRequest.ContentType = "application/x-www-form-urlencoded";
            httpWebRequest.ContentLength = postData.Length;

            using (StreamWriter writer = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                writer.Write(postData);
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                JavaScriptSerializer js = new JavaScriptSerializer();
                CustRegistraion cr = js.Deserialize<CustRegistraion>(result);
                int user_id = cr.UserId;
                string sedermobile = cr.SenderMobile;
                string Otp = cr.otc;
                string requestno = cr.requestNo;
                string Otpref=cr.otcRef;
                string errormsg = cr.ErrorMsg;
                string type=cr.Type;
                
                string rtId = GetApiTID(Otpref);

                if (requestno == "Success")
                {
                    obj_wallet.UpdateAPIcustomerRegistOtc(user_id, sedermobile, Otp, requestno, Otpref, type, rtId);

                    lblMessage.ForeColor = Color.Green;
                    lblMessage.Text = "Registration OTP verify successfully";
                }
                else
                {
                    lblMessage.ForeColor = Color.Red;
                    lblMessage.Text = errormsg;
                }
            }
        }
        catch
        { }
    }
    private string GetApiTID(string refrenceno)
    {
        DataTable dt = new DataTable();
        string custID = "";
        try
        {
            dt = obj_wallet.getApitbleID(refrenceno);
            if (dt.Rows.Count > 0)
            {
                custID = dt.Rows[0]["RTID"].ToString();
            }
        }
        catch
        { }
        return custID;
    }

    
    
    protected void linkVerify_Click(object sender, EventArgs e)
    {
        ModalPopupExtender1.Show();
    }
    protected void linkBeneficiary_Click(object sender, EventArgs e)
    {
        Response.Redirect("AddBeneficiary.aspx");
    }



    protected void lnkResedOtp_Click(object sender, EventArgs e)
    {
        try
        {
            string userid = ddUserIDno.SelectedValue;
            string mobilenum = txtMobileno.Text;
            BindOtcrefMobile(userid, mobilenum);

            string uid = Configuration.SuvidhaUser;
            string pin = Configuration.SuvidhaPin;
            string otcref = "";
            if (Session["OtcRef"] != null)
            {
                otcref = Session["OtcRef"].ToString();
            }
            string mobileno = "";
            if (Session["Mobileno"] != null)
            {
                mobileno = Session["Mobileno"].ToString();
            }
            string URL = "http://recharge.suvidhaarecharge.com/api/MoneyTransfer/ResendOtc?Username=" + uid + "&Password=" + pin + "&Mobile=" + mobileno + "&OTCREF=" + otcref + "";

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(URL);

            string postData = "http://recharge.suvidhaarecharge.com/api/MoneyTransfer/ResendOtc?Username=" + uid + "&Password=" + pin + "&Mobile=" + mobileno + "&OTCREF=" + otcref + "";
            httpWebRequest.Method = WebRequestMethods.Http.Post;
            httpWebRequest.ContentType = "application/x-www-form-urlencoded";
            httpWebRequest.ContentLength = postData.Length;

            using (StreamWriter writer = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                writer.Write(postData);
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                JavaScriptSerializer js = new JavaScriptSerializer();
                CustRegistraion cr = js.Deserialize<CustRegistraion>(result);
                int user_id = cr.UserId;
                string sedermobile = cr.SenderMobile;
                string Otp = cr.otc;
                string requestno = cr.requestNo;
                string Otpref = cr.otcRef;
                string errormsg = cr.ErrorMsg;

                if (errormsg !=null)
                {
                    lblMessage.ForeColor = Color.Red;
                    lblMessage.Text = errormsg;
                }
                else
                {
                    lblMessage.ForeColor = Color.Green;
                    lblMessage.Text = "Resend OTP on registered mobile";
                }
            }
        }
        catch
        { }
    }
    private void BindOtcrefMobile(string userid, string mobileno)
    {
        DataTable dt = new DataTable();
        try
        {
            dt = obj_wallet.getResendOtcRequestMobile(mobileno, userid);
            if (dt.Rows.Count > 0)
            {
                Session["OtcRef"] = dt.Rows[0]["requestNo"].ToString();
                Session["Mobileno"] = dt.Rows[0]["SenderMobile"].ToString();
            }
        }
        catch
        { }
    }


    protected void ddUserIDno_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddUserIDno.SelectedIndex > 0)
        {
            BindUserDetails(ddUserIDno.SelectedValue);
        }
    }
    private void BindUserIdentifyno()
    {
        DataTable dt = new DataTable();
        try
        {
            dt = obj_wallet.getUserIdentifyNo();
            if (dt.Rows.Count > 0)
            {
                ddUserIDno.DataSource = dt;
                ddUserIDno.DataTextField = "UserIdentifyNo";
                ddUserIDno.DataValueField = "UserId";
                ddUserIDno.DataBind();
                ddUserIDno.Items.Insert(0, "Select User ID");
            }
        }
        catch
        { }
    }
    private void BindUserDetails(string user)
    {
        DataTable dt = new DataTable();
        try
        {
            dt = obj_wallet.getUserDetailBene(user);
            if (dt.Rows.Count > 0)
            {
                string fullname = dt.Rows[0]["BeneName"].ToString();
                string fname;
                string lname;
                string[] names = fullname.ToString().Trim().Split(new char[] { ' ' }, 3);
                if (names.Length == 1)
                {
                    fname = names[0];
                    lname = "";
                }
                else if (names.Length == 3)
                {
                    fname = names[0];
                    lname = names[1] + " " + names[2];
                }
                else
                {
                    fname = names[0];
                    lname = names[1];
                }
                txtFirstname.Text = fname;
                txtLastname.Text = lname;
                txtMobileno.Text = dt.Rows[0]["Mobileno"].ToString();
            }
            else
            {
                txtFirstname.Text = "";
                txtLastname.Text = "";
                txtMobileno.Text = "";
            }
        }
        catch
        { }
    }
}