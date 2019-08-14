using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SqlAccess;
using System.Drawing;

public partial class admin_AdminWallet : System.Web.UI.Page
{
    WalletCls ob_wallet = new WalletCls();
    BEL ob_bel = new BEL();
    Products local_product = new Products();
    chkLogin local_login = new chkLogin();
    string UserId;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Convert.ToString(Session["userid"])) && Convert.ToString(Session["logintype"]) == "Admin")
        {
            UserId = Session["userid"].ToString();
            if (!IsPostBack)
            {
                BindUserWalletDetails(UserId);
                txtDate1.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            }
        }
        else
        {
            Response.Redirect("Login.aspx");
        }
    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        ob_bel.WalletId = 1;
        ob_bel.UserId = Convert.ToInt32(Session["userid"]);
        ob_bel.AddedAmount = Convert.ToDecimal(txtamount.Text);
        int user = Convert.ToInt32(Session["userid"]);
        ob_bel.ActionBy = user;
        decimal WalletAmtFromAdd = 0;
        string loginid = Convert.ToString(Session["loginid"]);
        WalletAmtFromAdd = GetMainBalance(loginid);
        ob_bel.Amount = WalletAmtFromAdd;

        int result = 0;
        try
        {
            result = ob_wallet.AddAdminBallance(ob_bel);

            if (result != 0)
            {
                ob_wallet.UpdateAdminBallance(ob_bel.AddedAmount, user);
                lblMessage.ForeColor = Color.Green;
                lblMessage.Text = txtamount.Text + " rupees amount added into admin wallet.";
                BindUserWalletDetails(UserId);
                txtamount.Text = "";
            }
        }
        catch (Exception ex)
        {
            lblMessage.ForeColor = Color.Red;
            lblMessage.Text = ex.Message;
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

    
    protected void btnGetDetails_Click(object sender, EventArgs e)
    {
        lblMessage.Text = "";
        try
        {
            DataTable dt = new DataTable();
            if (txtDate1.Text != "")
            {
                string date1 = txtDate1.Text;
                date1 = Convert.ToDateTime(date1).ToString("yyyy-MM-dd");
                string date2 = "";
                if (txtDate2.Text != "")
                {
                    date2 = txtDate2.Text;
                    date2 = Convert.ToDateTime(date2).ToString("yyyy-MM-dd");
                }
                dt = ob_wallet.getAdminWalletDetailsByDate(date1, date2, UserId);
                if (dt.Rows.Count > 0)
                {
                    gvWalletDetails.DataSource = dt;
                    gvWalletDetails.DataBind();
                }
                else
                {
                    gvWalletDetails.DataSource = null;
                    gvWalletDetails.DataBind();
                }
            }
            else
            {
                BindUserWalletDetails(UserId);
            }
        }
        catch (Exception ex)
        {
            lblMessage.ForeColor = Color.Red;
            lblMessage.Text = ex.Message;
        }
    }

    private void BindUserWalletDetails(string userid)
    {
        DataTable dt = new DataTable();
        try
        {
            dt = ob_wallet.getAdminWalletDetails(userid);
            if (dt.Rows.Count > 0)
            {
                gvWalletDetails.DataSource = dt;
                gvWalletDetails.DataBind();
            }
            else
            {
                gvWalletDetails.DataSource = null;
                gvWalletDetails.DataBind();
            }
        }
        catch
        { }
    }

    protected void gvWalletDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvWalletDetails.PageIndex = e.NewPageIndex;
        BindUserWalletDetails(UserId);
    }
    

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx");
    }
}