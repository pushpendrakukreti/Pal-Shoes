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

public partial class admin_DeductMoney : System.Web.UI.Page
{
    WalletCls ob_wallet = new WalletCls();
    BEL ob_bel = new BEL();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Convert.ToString(Session["userid"])) && Convert.ToString(Session["logintype"]) == "Admin")
        {
            if (!IsPostBack)
            {
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
            }
        }
        catch
        { }
    }

    

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Session["NewUserID"] as string))
        {
            ob_bel.WalletId = Convert.ToInt32(Session["walletid"]);
            ob_bel.UserId = Convert.ToInt32(Session["NewUserID"]);
            ob_bel.Amount = Convert.ToDecimal(Session["walletamount"]);
            ob_bel.DeductedAmount = Convert.ToDecimal(txtamount.Text);
            ob_bel.Customername = txtFulltname.Text;
            ob_bel.EmailId = txtEmailid.Text;
            ob_bel.UserIdetifyNo = lblUserID.Text;

            ob_bel.WalletStatus = "";
            if (txtRemarks.Text != "")
            {
                ob_bel.WalletStatus = txtRemarks.Text;
            }
            int user = Convert.ToInt32(Session["userid"]);
            ob_bel.ActionBy = user;
            decimal UserWalletAmount = 0;
            if(Session["walletamount"] !=null)
            {
                UserWalletAmount = Convert.ToDecimal(Session["walletamount"]);
            }

            ShoppingCart obj_bll = new ShoppingCart();
            int result = 0;
            try
            {
                if (UserWalletAmount >= Convert.ToDecimal(txtamount.Text))
                {
                    result = obj_bll.DeductMoney(ob_bel);

                    if (result != 0)
                    {
                        lblMessage.ForeColor = Color.Green;
                        lblMessage.Text = txtamount.Text + " rupees deducted from " + txtFulltname.Text + " wallet.";

                        string useridentifyno = lblUserID.Text;
                        if (useridentifyno != null)
                        {
                            SendMsg(useridentifyno);
                        }
                        txtamount.Text = "";
                        txtRemarks.Text = "";
                        Session["NewUserID"] = null;
                        Session["walletid"] = null;
                        Session["walletamount"] = null;
                        Response.Redirect("AddtoWallet.aspx", false);
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
    }


    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("AddtoWallet.aspx");
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
            Message = "Dear paulshoes User " + txtFulltname.Text + ", " + txtamount.Text + " rupees deducted from your wallet.";
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
}