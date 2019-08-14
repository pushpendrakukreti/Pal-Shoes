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

public partial class admin_FundTransfer : System.Web.UI.Page
{
    WalletCls obj_wallet = new WalletCls();
    chkLogin local_login = new chkLogin();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Convert.ToString(Session["userid"])) && Convert.ToString(Session["logintype"]) == "Admin")
        {
            if (!IsPostBack)
            {
                BindBenetype();
                BindDDAccountNubmer();
                getAPIwalletBalance();

                if (Session["RequestID"] != null)
                {
                    BindFundTransferAcDetail();
                }
            }
        }
        else
        {
            Response.Redirect("Login.aspx");
        }
    }
    public class Beneficiary
    {
        public string BeneficiaryCode { get; set; }
        public string BeneficiaryName { get; set; }
        public string BeneficiaryType { get; set; }
        public string AccountNumber { get; set; }
        public string AccountType { get; set; }
        public string IFSC { get; set; }
        public string Active { get; set; }
    }
    public class CustRegistraion
    {
        public string Mobileno { get; set; }
        public string Balance { get; set; }
        public string RequestNo { get; set; }
        public string CardExists { get; set; }
        public string CardDetails { get; set; }

        public string Response { get; set; }
        public string Message { get; set; }
        public string Code { get; set; }
        public string ErrorMsg { get; set; }
        public List<Beneficiary> beneficiary{ get; set; }
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
        if (Session["APIbalance"] != null)
        {
            decimal adminbalance = Convert.ToDecimal(Session["APIbalance"]);
            decimal transferamount = Convert.ToDecimal(txtAmount.Text);
            decimal walletamount = 0;
            if (adminbalance >= transferamount)
            {
                if (Session["WalletAmount"] != null)
                {
                    walletamount = Convert.ToDecimal(Session["WalletAmount"]);
                }
                if (walletamount >= transferamount)
                {
                    string verification_code = GenRandomCode(5);
                    Session["TransferAmount"] = transferamount;
                    Session["TranferOTP"] = verification_code;
                    SendMessage();
                    Response.Redirect("FundTransfer2.aspx");
                }
                else
                {
                    lblMessage.ForeColor = Color.Red;
                    lblMessage.Text = "Transfer amount should be less than wallet amount.";
                }
            }
            else
            {
                lblMessage.ForeColor = Color.Red;
                lblMessage.Text = "You haven't enough balace in API to transfer.";
            }
        }
    }

    private void BindDDAccountNubmer()
    {
        DataTable dt = new DataTable();
        try
        {
            dt = obj_wallet.getUserAccountNubmer();
            if (dt.Rows.Count > 0)
            {
                ddAcountno.DataSource = dt;
                ddAcountno.DataTextField = "Accountno";
                ddAcountno.DataValueField = "CustomerId";
                ddAcountno.DataBind();
                ddAcountno.Items.Insert(0, "Select Account No");
            }
        }
        catch
        { }
    }
    private void BindUserAccountDtail(string userid)
    {
        DataTable dt=new DataTable();
        try
        {
            dt = obj_wallet.getBeneAccountDetail(userid);
            if(dt.Rows.Count >0)
            {
                DataList1.DataSource = dt;
                DataList1.DataBind();

                Session["BeneName"] = dt.Rows[0]["BeneficiaryName"].ToString();
                Session["BeneAccount"] = dt.Rows[0]["BankAccNo"].ToString();
                Session["MobileNo"] = dt.Rows[0]["SenderMobile"].ToString();
                Session["WalletAmount"] = dt.Rows[0]["Amount"].ToString();
                Session["UserIdenfifyNo"] = dt.Rows[0]["CustIdentifyno"].ToString();
                Session["IfscCode"] = dt.Rows[0]["IfscCode"].ToString();
                Session["BeneficiaryType"] = dt.Rows[0]["BeneficiaryType"].ToString();
                Session["Customerid"] = dt.Rows[0]["CustomerId"].ToString();
                BindGetLogin(Session["MobileNo"].ToString());
            }
        }
        catch
        {}
    }

    protected void ddAcountno_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddAcountno.SelectedIndex > 0)
        {
            BindUserAccountDtail(ddAcountno.SelectedValue);
        }
    }


    private void BindGetLogin(string mobileno)
    {
        try
        {
            string uid = Configuration.SuvidhaUser;
            string pin = Configuration.SuvidhaPin;
            string URL = "http://recharge.suvidhaarecharge.com/api/MoneyTransfer/GetLogin?Username=" + uid + "&Password=" + pin + "&Mobile=" + mobileno + "";
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(URL);

            httpWebRequest.Method = WebRequestMethods.Http.Get;
            httpWebRequest.ContentType = "application/x-www-form-urlencoded";
            

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                JavaScriptSerializer js = new JavaScriptSerializer();
                CustRegistraion cr = js.Deserialize<CustRegistraion>(result);

                string mobile = cr.Mobileno;
                string balance = cr.Balance;
                string requestno = cr.RequestNo;
                string card = cr.CardExists;
                string errormsg = cr.ErrorMsg;
                string ressponse = cr.Response;
                string message = cr.Message;
                string code = cr.Code;
                Int32 ac_beneficiary = cr.beneficiary.Count;
                string benefcode = cr.beneficiary[0].BeneficiaryCode;
                //string benefname = cr.beneficiary[1].BeneficiaryName;
                //string beneftype = cr.beneficiary[2].BeneficiaryType;
                //string accountno = cr.beneficiary[3].AccountNumber;
                //string accounttype = cr.beneficiary[4].AccountType;
                //string beneifsc = cr.beneficiary[5].IFSC;
                //string active = cr.beneficiary[6].Active;
                Session["BenfCode"] = benefcode;

                string accountno = ddAcountno.SelectedItem.ToString();
                string rtID = GetApiEditID(accountno, mobileno);

                if (ressponse == "SUCCESS")
                {
                    obj_wallet.UpdateAPIBeneLogDetail(requestno, benefcode, message, rtID);
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

    private string GetApiEditID(string accountno, string mobileno)
    {
        DataTable dt = new DataTable();
        string custID = "";
        try
        {
            dt = obj_wallet.getApiTabelIDbyAC(accountno, mobileno);
            if (dt.Rows.Count > 0)
            {
                custID = dt.Rows[0]["RTID"].ToString();
            }
        }
        catch
        { }
        return custID;
    }


    private void getAPIwalletBalance()
    {
        try
        {
            string suvuid = Configuration.SuvidhaUser;
            string suvpin = Configuration.SuvidhaPin;
            if (Session["logintype"].ToString() == "Admin")
            {
                var request = (HttpWebRequest)WebRequest.Create("http://recharge.suvidhaarecharge.com/api/Recharge/GetBalance?Username=" + suvuid + "&Password=" + suvpin + "");

                var response = (HttpWebResponse)request.GetResponse();
                using (var streamReader = new StreamReader(response.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    Decimal amounts = Convert.ToDecimal(result);
                    Session["APIbalance"] = amounts;
                    lblAPIBalance.ForeColor = Color.Green;
                    lblAPIBalance.Text = "API BALANCE : Rs. " + Convert.ToString(amounts);
                }
            }
        }
        catch
        { }
    }

    private void BindFundTransferAcDetail()
    {
        try
        {
            string userid = Session["RequestUserid"].ToString();
            ddAcountno.SelectedValue = userid;
            txtAmount.Text = Session["RequestAmount"].ToString();
            BindUserAccountDtail(userid);
        }
        catch
        { }
    }




    public void SendMessage()
    {
        try
        {
            string key = null;
            string Sender_id = null;
            string contactnumber = null;
            string Message = null;
            key = "45A27FD357A379";
            Sender_id = "NZFUND";
            Message = "paulshoes withdrawal request fund transfer OTP : " + Session["TranferOTP"].ToString();
            contactnumber = mobilenumber();
            SendSMS(key, contactnumber, Sender_id, Message);
        }
        catch
        { }
    }
    public string SendSMS(string key, string contactno, string Sender_id, string Message)
    {
        WebClient client = new WebClient();
        string baseurl = "http://textsms.co.in/app/smsapi/index.php?key=" + key + "&routeid=6&type=text&contacts=" + contactno + "&senderid=" + Sender_id + "&msg=" + Message + "";

        Stream data = client.OpenRead(baseurl);
        StreamReader reader = new StreamReader(data);
        string s = reader.ReadToEnd();
        data.Close();
        reader.Close();
        return s;
    }

    public static string GenRandomCode(int PasswordLength)
    {
        string _allowedChars = "0123456789abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ";
        Random randNum = new Random();
        char[] chars = new char[PasswordLength];
        int allowedCharCount = _allowedChars.Length;
        for (int i = 0; i < PasswordLength; i++)
        {
            chars[i] = _allowedChars[(int)((_allowedChars.Length) * randNum.NextDouble())];
        }
        return new string(chars);
    }

    private string mobilenumber()
    {
        DataTable dt = new DataTable();
        string mobileno = "";
        try
        {
            dt = local_login.getAdminMobileNo();
            if (dt.Rows.Count > 0)
            {
                mobileno = dt.Rows[0]["Mobileno"].ToString();
            }
        }
        catch
        { }
        return mobileno;
    }
}