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
using System.Drawing;

public partial class admin_login : System.Web.UI.Page
{
    chkLogin local_service = new chkLogin();
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    
    protected void btbLogin_Click(object sender, EventArgs e)
    {
        try
        {
            string Result = "";
            Result = local_service.getuserInfo(Convert.ToString(txtUserName.Text), Convert.ToString(txtPassword.Text));
            if (Result.IndexOf("~") >= 0)
            {
                Session["LoginID"] = Convert.ToString(Result.Split('~')[0]);
                Session["username"] = Convert.ToString(Result.Split('~')[1]);
                Session["logintype"] = Convert.ToString(Result.Split('~')[2]);
                Session["UserId"] = Convert.ToString(Result.Split('~')[3]);
                Response.Redirect("~/admin/Default.aspx");
            }
            else
            {
                lblMessage.ForeColor = Color.Red;
                lblMessage.Text = Result;
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
    }
    
}