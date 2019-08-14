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

public partial class admin_AddBeneficiary : System.Web.UI.Page
{
    WalletCls obj_wallet = new WalletCls();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Convert.ToString(Session["userid"])) && Convert.ToString(Session["logintype"]) == "Admin")
        {
            if (!IsPostBack)
            {
                BindBenetype();
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
        public Int32 UserId { get; set; }
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
    private void BindBenetype()
    {
        ddBeneType.Items.Add("IMPS");
        ddBeneType.Items.Add("NEFT");
        ddBeneType.DataBind();
        ddBeneType.Items.Insert(0, "Beneficiary Type");
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string uid = Configuration.SuvidhaUser;
            string pin = Configuration.SuvidhaPin;
            string mobileno = txtMobileno.Text;
            string beneName = txtNameBene.Text;
            string accountno = txtAccountno.Text;
            string benetype = ddBeneType.SelectedItem.ToString();
            string ifsc = txtIfsccode.Text;

            string URL = "http://recharge.suvidhaarecharge.com/api/MoneyTransfer/AddNewBeneficiary?Username=" + uid + "&Password=" + pin + "&Mobile=" + mobileno + "&Name=" + beneName + "&AccNo=" + accountno + "&BeneType=" + benetype + "&ifsc=" + ifsc + "";

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(URL);

            string postData = "http://recharge.suvidhaarecharge.com/api/MoneyTransfer/AddNewBeneficiary?Username=" + uid + "&Password=" + pin + "&Mobile=" + mobileno + "&Name=" + beneName + "&AccNo=" + accountno + "&BeneType=" + benetype + "&ifsc=" + ifsc + "";
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
                string benefictype = cr.BeneficiaryType;
                string bankAcno = cr.BankAccNo;
                string benename = cr.BeneficiaryName;
                string sendermobile = cr.SenderMobile;
                string ifsccode = cr.IfscCode;
                string Requestno = cr.requestNo;
                string type = cr.Type;
                string errormsg = cr.ErrorMsg;
                Session["OtcRef1"] = Requestno;
                Session["Mobileno"] = sendermobile;

                string rtId = GetApiTIDByMobile(sendermobile);

                if (errormsg != null)
                {
                    lblMessage.ForeColor = Color.Red;
                    lblMessage.Text = errormsg;
                }
                else
                {
                    lblMessage.ForeColor = Color.Green;
                    lblMessage.Text = "OTP send on mobile";

                    obj_wallet.UpdateAPIaddnewBeneficiary(user_id, benefictype, accountno, benename, sendermobile, ifsccode, Requestno, type, rtId);
                }
                txtMobileno.Text = "";
                txtAccountno.Text = "";
                txtIfsccode.Text = "";
                txtNameBene.Text = "";
                ddBeneType.SelectedIndex = 0;
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
            if (Session["OtcRef1"] != null)
            {
                otcref = Session["OtcRef1"].ToString();
            }
            string otc = txtOTP.Text;
            string mobileno = "";
            if (Session["Mobileno"] != null)
            {
                mobileno = Session["Mobileno"].ToString();
            }
            string URL = "http://recharge.suvidhaarecharge.com/api/MoneyTransfer/VerifyBeneRegOtc?Username=" + uid + "&Password=" + pin + "&Mobile=" + mobileno + "&OtcRef=" + otcref + "&Otc=" + otc + "";

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(URL);

            string postData = "http://recharge.suvidhaarecharge.com/api/MoneyTransfer/VerifyBeneRegOtc?Username=" + uid + "&Password=" + pin + "&Mobile=" + mobileno + "&OtcRef=" + otcref + "&Otc=" + otc + "";
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

                string rtId = GetApiTID(Otpref);
                
                if (requestno == "Success")
                {
                    obj_wallet.UpdateAPIBeneficiaryOtc(user_id, otc, requestno, otcref, rtId);

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

    private string GetApiTIDByMobile(string MobileNo)
    {
        DataTable dt = new DataTable();
        string custID = "";
        try
        {
            dt = obj_wallet.getApitbleIDbyMobile(MobileNo);
            if (dt.Rows.Count > 0)
            {
                custID = dt.Rows[0]["RTID"].ToString();
            }
        }
        catch
        { }
        return custID;
    }
    private string GetApiTID(string Otpref)
    {
        DataTable dt = new DataTable();
        string custID = "";
        try
        {
            dt = obj_wallet.getApitbleID(Otpref);
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
        Response.Redirect("FundTransfer1.aspx");
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
            if (Session["OtcRef1"] != null)
            {
                otcref = Convert.ToString(Session["OtcRef1"]);
            }
            string mobileno = "";
            if (Session["Mobileno"] != null)
            {
                mobileno = Convert.ToString(Session["Mobileno"]);
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

                if (errormsg != null)
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
                Session["Mobileno"] = dt.Rows[0]["SenderMobile"].ToString();
                Session["OtcRef1"] = dt.Rows[0]["requestNo"].ToString();
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
                txtNameBene.Text = dt.Rows[0]["BeneName"].ToString();
                txtMobileno.Text = dt.Rows[0]["Mobileno"].ToString();
            }
            else
            {
                txtNameBene.Text = "";
                txtMobileno.Text = "";
            }
        }
        catch
        { }
    }
}