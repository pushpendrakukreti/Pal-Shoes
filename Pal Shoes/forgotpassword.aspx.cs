using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SqlAccess;
using System.Drawing;
using System.Text;
using System.Net;
using System.Net.Mail;
using System.IO;
using System.Web.Services;
using System.Text.RegularExpressions;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections;


public partial class forgetpassword : System.Web.UI.Page
{
    chkLogin local_login = new chkLogin();
    ShoppingCart local_cart = new ShoppingCart();
    Products local_product = new Products();
    Catergoy local_category = new Catergoy();
    SubCategory local_subcategory = new SubCategory();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Session["Transid"] as string))
            {
                BintCartItemsPrice();
            }
            if (!string.IsNullOrEmpty(Session["Tid"] as string))
            {
                BindWishlistNo();
            }
            if (!string.IsNullOrEmpty(Session["compare"] as string))
            {
                BindCompareItemsNo();
            }
            BintCartItems();
        }
        if (Session["userid"] != null)
        {
            pnlwishlist.Visible = true;
            pnlMyaccount.Visible = true;
        }
        BindCategoryMenu();
        BindLoginLogout();
        BindAccessories();
    }

    protected void btnSubscribe_Click(object sender, EventArgs e)
    {
        string emailid = txtemail.Text;
        string date = DateTime.Now.ToString("dd/MM/yyyy");
        if (emailid != "")
        {
            int success = 0;
            success = local_product.AddNewSubscribe(emailid, date);
            if (success != 0)
            {
                txtemail.Text = "";
                lblMessageSubscribe.ForeColor = Color.White;
                lblMessageSubscribe.Text = "Message Send Successfully";
                ViewState["ID"] = null;
            }
            else
            {
                lblMessageSubscribe.ForeColor = Color.White;
                lblMessageSubscribe.Text = "Message Send Failed";
            }
        }
    }
    private void BindLoginLogout()
    {
        if (!string.IsNullOrEmpty(Session["userid"] as string))
        {
            lnkLogin.Text = "<i class='fa fa-sign-out'></i> Logout";
        }
        else
        {
            lnkLogin.Text = "<i class='fa fa-lock'></i> Sign Up";
        }
    }
    protected void lnkLogin_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Session["userid"] as string))
        {
            lnkLogin.Text = "<i class='fa fa-sign-out'></i> Logout";
            Session.Abandon();
            Session.Clear();
            Response.Redirect("~/default.aspx", false);
        }
        else
        {
            lnkLogin.Text = "<i class='fa fa-lock'></i> Sign Up";
            Response.Redirect("~/register.aspx", false);
        }
    }

    //Nav Menu
    private void BindCategoryMenu()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = local_category.getMenuCategory();
            if (dt.Rows.Count > 0)
            {
                rpMenuCategory.DataSource = dt;
                rpMenuCategory.DataBind();
            }
        }
        catch
        { }
    }
    protected void Menu_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            Repeater rplist = (Repeater)e.Item.FindControl("rpSubCategory");
            DataRowView drow = e.Item.DataItem as DataRowView;
            string categoryid = drow.Row.ItemArray[0].ToString();
            DataTable dt = new DataTable();
            dt = local_category.getMenuSubCategory(categoryid);
            if (dt.Rows.Count > 0)
            {
                rplist.DataSource = dt;
                rplist.DataBind();
            }
        }
        catch
        { }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        try
        {
            string mobileno = txtMobileno.Text.Trim();
            dt = local_login.getUserDataforgotpassword(mobileno);
            if (dt.Rows.Count > 0)
            {
                string loginid = dt.Rows[0]["LoginID"].ToString();
                string userid = dt.Rows[0]["UserId"].ToString();
                string username = dt.Rows[0]["UserName"].ToString();
                Session["usermailid"] = username;
                string newpassword = GenRandomCode(7);
                Session["newpassword"] = newpassword;
                int result = 0;

                result = local_login.UpdatePassword(newpassword, loginid);
                if (result != 0)
                {
                    SendPassword();
                    mpe.Show();
                    lblError.ForeColor = Color.Green;
                    lblError.Text = "Your password has been changed<br/>and send on your mail id.";
                    clear();
                }
            }
            else
            {
                lblMessage2.ForeColor = Color.Red;
                lblMessage2.Text = "Invalid Mobile Number!";
            }
        }
        catch (Exception ex)
        {
            lblMessage2.ForeColor = Color.Red;
            lblMessage2.Text = ex.Message;
        }
        }

    protected void btnVerify_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/login.aspx");
    }

    private void clear()
    {
        txtName.Text = "";
        txtMobileno.Text = "";
    }

    private void BintCartItems()
    {
        try
        {
            DataTable dt = new DataTable();
            if (Session["Transid"] != null)
            {
                string transctionid = Session["Transid"].ToString();
                dt = local_cart.getCartItemNum(transctionid);
                if (dt.Rows.Count > 0)
                {
                    rpnotification.DataSource = dt;
                    rpnotification.DataBind();
                }
            }
            else
            {
                dt.Columns.Add("Items", typeof(string));
                DataRow row = dt.NewRow();
                row["Items"] = "0";
                dt.Rows.Add(row);
                rpnotification.DataSource = dt;
                rpnotification.DataBind();
            }
        }
        catch
        { }
    }
    private void BintCartItemsPrice()
    {
        try
        {
            DataTable dt = new DataTable();
            string transctionid = Session["Transid"].ToString();
            dt = local_cart.getCartItemtotPrice(transctionid);
            if (dt.Rows.Count > 0)
            {
                rpItempPrice.DataSource = dt;
                rpItempPrice.DataBind();
            }
        }
        catch
        { }
    }

    private void BindWishlistNo()
    {
        try
        {
            DataTable dt = new DataTable();
            string transid = Session["Tid"].ToString();
            dt = local_cart.getWishListCurrentNo(transid);
            if (dt.Rows.Count > 0)
            {
                Rpwishlist.DataSource = dt;
                Rpwishlist.DataBind();
            }
        }
        catch
        { }
    }
    private void BindCompareItemsNo()
    {
        try
        {
            DataTable dt = new DataTable();
            string transid = Session["compare"].ToString();
            dt = local_cart.getCompareItemsNo(transid);
            if (dt.Rows.Count > 0)
            {
                rpCompare.DataSource = dt;
                rpCompare.DataBind();
            }
        }
        catch
        { }
    }

    private void BindAccessories()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = local_subcategory.getAccessoriesSubCategory();
            if (dt.Rows.Count > 0)
            {
                //rbAccessories.DataSource = dt;
                //rbAccessories.DataBind();
            }
        }
        catch
        { }
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

    public void SendPassword()
    {
        try
        {
            string emailid = Session["usermailid"].ToString();
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("noreply@Palshoes-tabletennis.com", "Pal Shoes-Shop");
            mail.To.Add(emailid);
            mail.IsBodyHtml = true;
            mail.Subject = "New Password Generated.. " + txtName.Text;
            mail.Body = "Name :" + " " + txtName.Text + " <br /><br />" + 
                        "New Password :" + " " + Session["newpassword"].ToString() + " <br /><br />"   +  " ";
            SmtpClient smtp = new SmtpClient("Palshoes-tabletennis.com", 25);
            smtp.Credentials = new System.Net.NetworkCredential("noreply@Palshoes-tabletennis.com", "Mgfy57&8");
            smtp.EnableSsl = false;
            smtp.Send(mail);

            //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('Your password has been changed , And send on your mail id. ')", true);
            clear();
        }
       
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

        //try
        //{
        //    string key = null;
        //    string Sender_id = null;
        //    string contactnumber = null;
        //    string Message = null;
        //    key = "45A27FD357A379";
        //    Sender_id = "NZFUND";

        //    Message = "Your Palshoes new password is : " + Session["newpassword"].ToString();
        //    contactnumber = txtMobileno.Text;
        //    SendSMS(key, contactnumber, Sender_id, Message);
        //}
        //catch
        //{ }
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


    protected void Searchbtn_Click(object sender, EventArgs e)
    {
        if (txtSearch.Text != "")
        {
            string search = txtSearch.Text.Trim();
            Response.Redirect("search.aspx?search=" + search);
        }
        else
        {

        }
    }


    [WebMethod]
    public static List<string> GetSerchResult(string searchdata)
    {
        List<string> empResult = new List<string>();
        string name = searchdata;
        string brand = searchdata;
        string price = searchdata;
        string modelno = searchdata;
        string capacity = searchdata;
        string producttype = searchdata;
        Products ob_p = new Products();
        try
        {
            DataTable dt = new DataTable();
            dt = ob_p.getSearchRecord(name, brand, price, modelno, capacity, producttype);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    empResult.Add(dt.Rows[i]["name"].ToString());
                }
            }
        }
        catch
        { }
        return empResult;
    }
}