using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using SqlAccess;
using System.Drawing;
using System.Net.Mail;
using System.Net;

public partial class admin_AddAmount : System.Web.UI.Page
{
    WalletCls ob_wallet = new WalletCls();
    BEL ob_bel = new BEL();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Convert.ToString(Session["userid"])) && Convert.ToString(Session["logintype"]) == "Admin")
        {
            if (!IsPostBack)
            {
                BindPaymenttype();
                BindPaytype();

                if (Session["RequestUserid"] != null)
                {
                    string userId = Session["RequestUserid"].ToString();
                    BindUserWalletDetails(userId);
                }
                if (Session["NewUserID"] != null)
                {
                    string userId = Session["NewUserID"].ToString();
                    BindUserWalletDetails(userId);
                }
            }
        }
        else
        {
            Response.Redirect("Login.aspx");
        }
    }

    private void BindUserWalletDetails(string userID)
    {
        try
        {
            DataTable dt = new DataTable();
            dt = ob_wallet.getUserDetails(userID);
            if (dt.Rows.Count > 0)
            {
                txtFulltname.Text = dt.Rows[0]["fullname"].ToString();
                txtEmailid.Text = dt.Rows[0]["Emailid"].ToString();
                lblUserID.Text = dt.Rows[0]["UserIdentifyNo"].ToString();
                txtFulltname.Enabled = false;
                txtEmailid.Enabled = false;
                Session["walletid"] = dt.Rows[0]["WalletId"].ToString();
                Session["walletamount"] = dt.Rows[0]["Amount"].ToString();

                if (Session["RequestUserid"] != null)
                {
                    txtamount.Text = Session["RequestAmount"].ToString();
                    txtTransactionNo.Text = Session["TransactionNo"].ToString();
                    string paymenttype = Session["PaymentType"].ToString();
                    ddPaymenttype.Text = paymenttype;
                }
            }
        }
        catch
        { }
    }

    private void BindPaymenttype()
    {
        ddPaymenttype.Items.Add("Cash");
        ddPaymenttype.Items.Add("Chaque");
        ddPaymenttype.Items.Add("NEFT/IMPS");
        ddPaymenttype.Items.Add("RTGS");
        ddPaymenttype.DataBind();
        ddPaymenttype.Items.Insert(0, "Select Payment Type");
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Session["RequestUserid"] as string))
        {
            ob_bel.WalletId = Convert.ToInt32(Session["walletid"]);
            ob_bel.UserId = Convert.ToInt32(Session["RequestUserid"]);
            ob_bel.Amount = Convert.ToDecimal(Session["walletamount"]);
            ob_bel.AddedAmount = Convert.ToDecimal(txtamount.Text);
            ob_bel.Customername = txtFulltname.Text;
            ob_bel.EmailId = txtEmailid.Text;
            ob_bel.UserIdetifyNo = lblUserID.Text;
            ob_bel.PaymentType = "";
            if (ddPaymenttype.SelectedIndex > 0)
            {
                ob_bel.PaymentType = Convert.ToString(ddPaymenttype.SelectedValue);
            }
            ob_bel.TransactionNo = 0;
            
            if (txtTransactionNo.Text != "")
            {
                ob_bel.TransactionNo = Convert.ToInt64(txtTransactionNo.Text);
            }
            ob_bel.WalletStatus = "";
            if (txtRemarks.Text != "")
            {
                ob_bel.WalletStatus = txtRemarks.Text;
            }
            int user = Convert.ToInt32(Session["userid"]);
            ob_bel.ActionBy = user;
            decimal WalletAmtFromAdd = 0;
            string loginid = Convert.ToString(Session["loginid"]);
            WalletAmtFromAdd = GetMainBalance(loginid);

            ShoppingCart obj_bll = new ShoppingCart();
            int result = 0;
            try
            {
                if (WalletAmtFromAdd >= Convert.ToDecimal(txtamount.Text))
                {
                    result = obj_bll.AddMoney(ob_bel);

                    if (result != 0)
                    {
                        bool status = true;
                        string requeststatus = "Amount Added";
                        long transaccont = Convert.ToInt64(txtTransactionNo.Text);
                        decimal tranferamount = Convert.ToDecimal(Session["RequestAmount"]);
                        string requestid = Convert.ToString(Session["RequestID"]);
                        if (Session["RequestID"] != null)
                        {
                            ob_wallet.UpdateCustomerPayWalletStatus(status, tranferamount, transaccont, requeststatus, requestid);
                        }

                        lblMessage.ForeColor = Color.Green;
                        lblMessage.Text = txtamount.Text + " rupees added into " + txtFulltname.Text + " wallet.";

                        string useridentifyno = lblUserID.Text;
                        if (useridentifyno != null)
                        {
                            SendMsg(useridentifyno);
                        }
                        txtamount.Text = "";
                        txtRemarks.Text = "";
                        txtTransactionNo.Text = "";
                        ddPaymenttype.SelectedIndex = 0;
                        Session["RequestUserid"] = null;
                        Session["RequestAmount"] = null;
                        Session["TransactionNo"] = null;
                        Session["PaymentType"] = null;
                        Session["walletid"] = null;
                        Session["walletamount"] = null;
                        Response.Redirect("MemberDepostRequest.aspx", false);
                    }
                }
                else
                {
                    lblMessage.ForeColor = Color.Red;
                    lblMessage.Text = txtamount.Text + " rupees amount should be less than wallet amount.";
                }
            }
            catch (Exception ex)
            {
                lblMessage.ForeColor = Color.Red;
                lblMessage.Text = ex.Message;
            }
        }

        if (!string.IsNullOrEmpty(Session["NewUserID"] as string))
        {
            ob_bel.WalletId = Convert.ToInt32(Session["walletid"]);
            ob_bel.UserId = Convert.ToInt32(Session["NewUserID"]);
            ob_bel.Amount = Convert.ToDecimal(Session["walletamount"]);
            ob_bel.AddedAmount = Convert.ToDecimal(txtamount.Text);
            ob_bel.Customername = txtFulltname.Text;
            ob_bel.EmailId = txtEmailid.Text;
            ob_bel.UserIdetifyNo = lblUserID.Text;

            ob_bel.PaymentType = "";
            if (ddPaymenttype.SelectedIndex > 0)
            {
                ob_bel.PaymentType = Convert.ToString(ddPaymenttype.SelectedValue);
            }
            ob_bel.TransactionNo = 0;
            if (txtTransactionNo.Text != "")
            {
                ob_bel.TransactionNo = Convert.ToInt64(txtTransactionNo.Text);
            }
            ob_bel.WalletStatus = "";
            if (txtRemarks.Text != "")
            {
                ob_bel.WalletStatus = txtRemarks.Text;
            }
            int user = Convert.ToInt32(Session["userid"]);
            ob_bel.ActionBy = user;
            //decimal WalletAmtFromAdd = 0;
            //string loginid = Convert.ToString(Session["loginid"]);
            //WalletAmtFromAdd = GetMainBalance(loginid);

            ShoppingCart obj_bll = new ShoppingCart();
            int result = 0;
            try
            {
                result = obj_bll.AddMoney(ob_bel);

                if (result != 0)
                {
                    lblMessage.ForeColor = Color.Green;
                    lblMessage.Text = txtamount.Text + " rupees added into " + txtFulltname.Text + " wallet.";

                    string useridentifyno = lblUserID.Text;
                    if (useridentifyno != null)
                    {
                        SendMsg(useridentifyno);
                    }
                    txtamount.Text = "";
                    txtRemarks.Text = "";
                    txtTransactionNo.Text = "";
                    ddPaymenttype.SelectedIndex = 0;
                    Session["NewUserID"] = null;
                    Session["walletid"] = null;
                    Session["walletamount"] = null;
                    Response.Redirect("AddtoWallet.aspx", false);
                }
            }
            catch (Exception ex)
            {
                lblMessage.ForeColor = Color.Red;
                lblMessage.Text = ex.Message;
            }
        }
    }

    

    private decimal GetMainBalance(string loginid)
    {
        decimal balance = 0;
        DataTable dt = new DataTable();
        try
        {
            dt = ob_wallet.getMainBalance(loginid);
            if (dt.Rows.Count > 0)
            {
                balance = Convert.ToDecimal(dt.Rows[0]["Amount"]);
            }
        }
        catch
        { }
        return balance;
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
            Message = "Dear paulshoes User " + txtFulltname.Text + ", "  + txtamount.Text + " rupees added into your wallet.";
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

    protected void ddPaymenttype_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindPaytype();
    }
    private void BindPaytype()
    {
        if (ddPaymenttype.SelectedIndex == 1)
        {
            trans.Visible = false;
        }
        else
        {
            trans.Visible = true;
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("AddtoWallet.aspx");
    }
}