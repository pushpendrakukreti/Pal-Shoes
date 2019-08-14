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

public partial class admin_FundTransfer2 : System.Web.UI.Page
{
    WalletCls obj_wallet = new WalletCls();
    static Random random = new Random();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Convert.ToString(Session["userid"])) && Convert.ToString(Session["logintype"]) == "Admin")
        {
            if (!IsPostBack)
            {
                getAPIwalletBalance();
                if (Session["TranferOTP"] != null)
                {
                    BindUserAccountDtail();
                    mpe.Show();
                    lblError.Text = "Fund Transfer OTP send on admin mobile.";
                }
                else
                {
                    mpe.Show();
                }
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

                    lblAPIbalance.ForeColor = Color.Green;
                    lblAPIbalance.Text = "API BALANCE : Rs. " + Convert.ToString(amounts);
                }
            }
        }
        catch
        { }
    }


    protected void btnVerify_Click(object sender, EventArgs e)
    {
        if (txtCode.Text == Session["TranferOTP"].ToString())
        {
            Session["txtCodeTranferOTP"] = txtCode.Text.Trim();
        }
        else
        {
            mpe.Show();
            lblError.Text = "Invalid Transfer OTP";
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["TranferOTP"].ToString() == Session["txtCodeTranferOTP"].ToString())
            {
                string uid = Configuration.SuvidhaUser;
                string pin = Configuration.SuvidhaPin;
                string mobileno = Session["MobileNo"].ToString();
                string benecode = Session["BenfCode"].ToString();
                string ifsccode = Session["IfscCode"].ToString();
                string amount = Session["TransferAmount"].ToString();
                string paytype = Session["BeneficiaryType"].ToString();
                string accountno = Session["BeneAccount"].ToString();
                string beneName = Session["BeneName"].ToString();
                string transactionId = TransactionID();

                string URL = "http://recharge.suvidhaarecharge.com/api/MoneyTransfer/MoneyTransfer?Username=" + uid + "&Password=" + pin + "&Mobile=" + mobileno + "&BeneCode=" + benecode + "&IFSC=" + ifsccode + "&Amount=" + amount + "&PayType=" + paytype + "&AccountNumber=" + accountno + "&Name=" + beneName + "&YourTransactionID=" + transactionId + "";

                var httpWebRequest = (HttpWebRequest)WebRequest.Create(URL);

                string postData = "http://recharge.suvidhaarecharge.com/api/MoneyTransfer/MoneyTransfer?Username=" + uid + "&Password=" + pin + "&Mobile=" + mobileno + "&BeneCode=" + benecode + "&IFSC=" + ifsccode + "&Amount=" + amount + "&PayType=" + paytype + "&AccountNumber=" + accountno + "&Name=" + benecode + "&YourTransactionID=" + transactionId + "";
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
                    Int32 user_id = cr.UserId;
                    string bankAcno = cr.BankAccNo;
                    string benename = cr.BeneficiaryName;
                    string sendermobile = cr.SenderMobile;
                    string requestno = cr.requestNo;
                    string errormsg = cr.ErrorMsg;
                    string message = cr.Message;

                    if (message == "SUCCESS")
                    {
                        string trasactionno = TransactionID();
                        string sysIPaddress = GetIPAddress();
                        Int32 userID = Convert.ToInt32(Session["Customerid"]);

                        obj_wallet.UpdateCustomerWalletFundtransfer(Convert.ToDecimal(txtAmount.Text), userID);

                        obj_wallet.AddFundTransferDetail(trasactionno, userID, Session["UserIdenfifyNo"].ToString(), Session["BeneName"].ToString(), Session["BeneAccount"].ToString(), Session["BeneficiaryType"].ToString(), Session["IfscCode"].ToString(), Session["MobileNo"].ToString(), Convert.ToDecimal(Session["TransferAmount"]), sysIPaddress);

                        string mobile = Session["MobileNo"].ToString();
                        SendMsg(mobile);
                        string requeststatus = "Amount Transfered";

                        BindUserWalletDetails(Convert.ToString(userID));
                        Session["transactiontype"] = "Withdrawal Amount";
                        int logid = Convert.ToInt32(Session["userid"]);
                        obj_wallet.DeductCustomerWalletAmt(Convert.ToInt32(Session["walletid"]), userID, Convert.ToDecimal(Session["walletamount"]), Convert.ToDecimal(Session["TransferAmount"]), trasactionno, Session["transactiontype"].ToString(), requeststatus, Session["Customername"].ToString(), Session["CustEmailid"].ToString(), logid, Session["UserIdenfifyNo"].ToString());
                        obj_wallet.UpdateCustomerWalletFundtransfer(Convert.ToDecimal(Session["TransferAmount"]), logid);

                        bool status = true;
                        long transaccont = Convert.ToInt64(Session["BeneAccount"]);
                        decimal tranferamount = Convert.ToDecimal(Session["TransferAmount"]);
                        string requestid = Convert.ToString(Session["RequestID"]);
                        if (Session["RequestID"] != null)
                        {
                            obj_wallet.UpdateCustomerFundTransferStatus(status, tranferamount, transaccont, requeststatus, requestid);
                        }

                        lblMessage.ForeColor = Color.Green;
                        lblMessage.Text = "Amount Transfer Successfully!";
                        Session["MobileNo"] = null;
                        Session["BeneAccount"] = null;
                        Session["BeneName"] = null;
                        Session["IfscCode"] = null;
                        Session["BeneficiaryType"] = null;
                        Session["TransferAmount"] = null;
                        Session["Customerid"] = null;
                        Session["TranferOTP"] = null;
                        txtAmount.Text = "";
                        Session["RequestUserid"] = null;
                        Session["RequestID"] = null;
                        Session["RequestAmount"] = null;

                        Session["TranferOTP"] = null;
                        Session["txtCodeTranferOTP"] = null;
                    }
                    if (errormsg != null)
                    {
                        lblMessage.ForeColor = Color.Red;
                        lblMessage.Text = errormsg;
                    }
                }
            }
            else
            {
                lblMessage.ForeColor = Color.Red;
                lblMessage.Text = "OTP entered is incorrect";

            }
        }
        catch
        { }
    }


   

    private void BindUserAccountDtail()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("BeneficiaryName", typeof(string));
        dt.Columns.Add("BankAccNo", typeof(string));
        dt.Columns.Add("SenderMobile", typeof(string));
        dt.Columns.Add("CustIdentifyno", typeof(string));
        dt.Columns.Add("IfscCode", typeof(string));
        dt.Columns.Add("BeneficiaryType", typeof(string));
        dt.Columns.Add("Amount", typeof(decimal));
        dt.Columns.Add("CustomerID", typeof(Int32));

        DataRow drow = dt.NewRow();
        drow["BeneficiaryName"] = Session["BeneName"].ToString();
        drow["BankAccNo"] = Session["BeneAccount"].ToString();
        drow["SenderMobile"] = Session["MobileNo"].ToString();
        drow["CustIdentifyno"] = Session["UserIdenfifyNo"].ToString();
        drow["IfscCode"] = Session["IfscCode"].ToString();
        drow["BeneficiaryType"] = Session["BeneficiaryType"].ToString();
        drow["Amount"] = Session["TransferAmount"];
        drow["CustomerID"] = Session["Customerid"];
        dt.Rows.Add(drow);

        try
        {
            if (dt.Rows.Count > 0)
            {
                DataList1.DataSource = dt;
                DataList1.DataBind();

                txtAmount.Text = dt.Rows[0]["Amount"].ToString();
                txtAmount.Font.Bold = true;
                txtAmount.Enabled = false;
            }
        }
        catch
        { }
    }

    private string TransactionID()
    {
        string transNo = "";
    
        for (Int32 i = 0; i < 2; i++)
        {
            transNo = (Convert.ToString(random.Next(000000001, 900000000)));
        }
        return transNo;
    }


    public string GetIPAddress()
    {
        string IPAddress = "";
        IPHostEntry Host = default(IPHostEntry);
        string Hostname = null;
        Hostname = System.Environment.MachineName;
        Host = Dns.GetHostEntry(Hostname);
        foreach (IPAddress IP in Host.AddressList)
        {
            if (IP.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
            {
                IPAddress = Convert.ToString(IP);
            }
        }
        return IPAddress;
    }


    private void BindUserWalletDetails(string userid)
    {
        try
        {
            DataTable dt = new DataTable();
            dt = obj_wallet.getUserDetails(userid);
            if (dt.Rows.Count > 0)
            {
                Session["Customername"] = dt.Rows[0]["fullname"].ToString();
                Session["CustEmailid"] = dt.Rows[0]["Emailid"].ToString();
                Session["CustIdentifyno"] = dt.Rows[0]["UserIdentifyNo"].ToString();
                Session["walletid"] = dt.Rows[0]["WalletId"].ToString();
                Session["walletamount"] = dt.Rows[0]["Amount"].ToString();
                Session["transactiontype"] = "Add to wallet";
            }
        }
        catch
        { }
    }


    public void SendMsg(string mobileno)
    {
        try
        {
            string key = null;
            string Sender_id = null;
            string contactnumber = mobileno;
            string Message = null;
            key = "45A27FD357A379";
            Sender_id = "NZFUND";
            Message = "Dear paulshoes User, " + Session["TransferAmount"].ToString() + " rupees added into your wallet.";
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
}