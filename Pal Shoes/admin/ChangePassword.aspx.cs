using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using SqlAccess;
using System.Drawing;

public partial class admin_ChangePassword : System.Web.UI.Page
{
    chkLogin local_login = new chkLogin();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Convert.ToString(Session["userid"])) && Convert.ToString(Session["logintype"]) == "Admin")
        {
            //
        }
        else
        {
            Response.Redirect("Login.aspx");
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (txtUsername.Text != "" && txtOldPassword.Text != "" && txtNewPassword.Text != "" && txtConfirmpass.Text != "")
        {
            DataTable dt = new DataTable();
            string username = txtUsername.Text.Trim();
            string oldpassword = txtOldPassword.Text.Trim();
            string newpassword = txtNewPassword.Text.Trim();
            string confirmpassword = txtConfirmpass.Text.Trim();

            if (local_login.IfUserPassExists(username, oldpassword, "N", "") == "T")
            {
                dt = local_login.getdataLogin(username);
                string loginid = Convert.ToString(dt.Rows[0]["LoginID"]);

                local_login.UpdatePasswordAdmin(newpassword, loginid);
                clear();
                mpe.Show();
                lblpopup.Text = "Your Password Changed Successfully";
            }
            else
            {
                lblmainMsg.ForeColor = Color.Red;
                lblmainMsg.Text = "Username/Password Invalid";
            }
        }
        else
        {
            mpe.Show();
            lblpopup.Text = "Please fill the reqired details!";
        }
    }

    private void clear()
    {
        txtUsername.Text = "";
        txtOldPassword.Text = "";
        txtNewPassword.Text = "";
        txtConfirmpass.Text = "";
    }
}