using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using SqlAccess;
using System.Drawing;
using System.Text;
using System.Net;
using System.Net.Mail;
using System.IO;
using System.Text;
using System.Web.Services;
using System.Text.RegularExpressions;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using ClosedXML.Excel;
using System.Collections;

public partial class admin_Registration : System.Web.UI.Page
{
    Products local_product = new Products();
    WalletCls local_wallet = new WalletCls();
    chkLogin local_login = new chkLogin();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Convert.ToString(Session["userid"])) && Convert.ToString(Session["logintype"]) == "Admin")
        {
            if (!IsPostBack)
            {
                BindRegistrationDetails();
                txtDate1.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            }
        }
        else
        {
            Response.Redirect("Login.aspx");
        }
    }

    private void BindRegistrationDetails()
    {
        DataTable dt = new DataTable();
        try
        {
            dt = local_wallet.getRegistraionDetail();
            if (dt.Rows.Count > 0)
            {
                gvRegistration.DataSource = dt;
                gvRegistration.DataBind();
            }
            else
            {
                lblMessage.Text = "<br/><br/><br/>There is no any records.";
                lblMessage.Font.Size = 20;
            }
        }
        catch
        { }
    }

    protected void gvRegistration_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ImageButton img = (ImageButton)e.Row.FindControl("BtnActive");
            Label active = (Label)e.Row.FindControl("lblActive");

            if (active.Text == "Active")
            {
                img.ImageUrl = "~/img/ActiveU.png";
                active.ForeColor = Color.Green;
            }
            else
            {
                img.ImageUrl = "~/img/InactiveU.png";
                active.ForeColor = Color.Red;
            }
        }
    }
    protected void gvRegistration_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            string userid = Convert.ToString(e.CommandArgument);
            string deletecode = GenRandomCode(5);
            Session["VerificationCode"] = deletecode;
            Session["deleteid"] = userid;
            SendMessage();
            mpe.Show();
            lblError.Text = "User delete code send on admin mobile.";
        }
    }

    protected void btnVerify_Click(object sender, EventArgs e)
    {
        if (txtCode.Text != "")
        {
            if (txtCode.Text == Session["VerificationCode"].ToString())
            {
                try
                {
                    local_login.DeletUser(Session["deleteid"].ToString());
                    BindRegistrationDetails();

                    mpe.Show();
                    lblError.ForeColor = Color.Green;
                    lblError.Text = "Selected user deleted.";
                    code.Visible = false;
                    txtCode.Visible = false;
                    btnVerify.Visible = false;

                    Session["VerificationCode"] = null;
                    Session["deleteid"] = null;
                }
                catch
                { }
            }
            else
            {
                mpe.Show();
                lblError.ForeColor = Color.Red;
                lblError.Text = "Invalid user delete code.";
            }
        }
    }

    protected void BtnActive_Click(object sender, EventArgs e)
    {
        try
        {
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            Label id = (Label)gvRow.FindControl("lblId");
            Label active = (Label)gvRow.FindControl("lblActive");
            string status = "";
            int walletpin = 0;
            if (active.Text == "Active")
            {
                status = "Inactive";
            }
            else
            {
                status = "Active";
                walletpin = GenerateWalletPin();
            }
            local_login.ActiveWallet(status, Convert.ToString(id.Text), walletpin);
            BindRegistrationDetails();
        }
        catch
        { }
    }
    
    protected void gvRegistration_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvRegistration.PageIndex = e.NewPageIndex;
        BindRegistrationDetails();
    }

    public int GenerateWalletPin()
    {
        int _min = 1000;
        int _max = 9999;
        Random _rdm = new Random();
        return _rdm.Next(_min, _max);
    }


    protected void btnGetDetails_Click(object sender, EventArgs e)
    {
        lblMessage.Text = "";
        try
        {
            DataTable dt = new DataTable();

            if (txtSearch.Text != "")
            {
                dt = local_wallet.getRegistrationUserDetailByUserID(txtSearch.Text);
                if (dt.Rows.Count > 0)
                {
                    gvRegistration.DataSource = dt;
                    gvRegistration.DataBind();
                }
                else
                {
                    gvRegistration.DataSource = null;
                    gvRegistration.DataBind();
                }
            }
            else if (txtDate1.Text != "")
            {
                string date1 = txtDate1.Text;
                date1 = Convert.ToDateTime(date1).ToString("yyyy-MM-dd");
                string date2 = "";
                if (txtDate2.Text != "")
                {
                    date2 = txtDate2.Text;
                    date2 = Convert.ToDateTime(date2).ToString("yyyy-MM-dd");
                }
                dt = local_wallet.getRegistrationUserDetailByDate(date1, date2);
                if (dt.Rows.Count > 0)
                {
                    gvRegistration.DataSource = dt;
                    gvRegistration.DataBind();
                }
                else
                {
                    gvRegistration.DataSource = null;
                    gvRegistration.DataBind();
                }
            }
            else
            {
                BindRegistrationDetails();
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
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
            Message = "paulshoes register user delete code is : " + Session["VerificationCode"].ToString();
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


    protected void gvRegistration_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

	protected void ImageButton1_Click1(object sender, ImageClickEventArgs e)
    {
        DataTable dt = new DataTable("Register");
        dt.Columns.Add("S.N.", typeof(Int32));
        dt.Columns.Add("User ID", typeof(string));
        dt.Columns.Add("First Name", typeof(string));
        dt.Columns.Add("Last Name", typeof(string));
        dt.Columns.Add("Email ID", typeof(string));
        dt.Columns.Add("Create Date", typeof(string));
        dt.Columns.Add("Status", typeof(string));

        foreach (GridViewRow row in gvRegistration.Rows)
        {
            Int32 sn = row.DataItemIndex + 1;
            string useridetify = row.Cells[1].Text;
            string fname = row.Cells[2].Text;
            string lname = row.Cells[3].Text;
            string email = row.Cells[4].Text;
            string createdate = row.Cells[5].Text;
            Label status1 = (Label)row.FindControl("lblRegistrationStatus");
            string status = status1.Text;
            dt.Rows.Add(sn, useridetify, fname, lname, email, createdate, status);
        }

        if (dt.Rows.Count > 0)
        {
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, "Register");

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                string FileName = "Registration_detail_" + DateTime.Now.ToShortDateString() + ".xlsx";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=" + FileName);
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
        }
    }
}