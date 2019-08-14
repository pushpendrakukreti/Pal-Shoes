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


public partial class admin_AdminPage : System.Web.UI.MasterPage
{
    WalletCls local_wallet = new WalletCls();

    protected void Page_Load(object sender, EventArgs e)
    {
        BindLoginLogout();
        if (!string.IsNullOrEmpty(Session["userid"] as string))
        {
            string UserId = Session["userid"].ToString();
            getAPIwalletBalance();
            BindWalletAmount(UserId);
            BindNoOfAmountRequest();
            pnlLogin.Visible = false;
        }
    }

    private void BindWalletAmount(string userid)
    {
        try
        {
            DataTable dt = new DataTable();
            dt = local_wallet.getAdminWalletAmount(userid);
            if (dt.Rows.Count > 0)
            {
                //rpWallet.DataSource = dt;
                //rpWallet.DataBind();
            }
        }
        catch
        { }
    }
    private void BindLoginLogout()
    {
        if (!string.IsNullOrEmpty(Session["UserId"] as string))
        {
            lnkLogin.Text = "<i class='fa fa-sign-out'></i> Logout";
        }
        else
        {
            lnkLogin.Text = "<i class='fa fa-lock'></i> Login";
        }
    }
    protected void lnkLogin_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Session["UserId"] as string))
        {
            lnkLogin.Text = "<i class='fa fa-sign-out'></i> Logout";
            Session.Abandon();
            Session.Clear();
            Response.Redirect("../default.aspx", false);
        }
        else
        {
            lnkLogin.Text = "<i class='fa fa-lock'></i> Login";
            Response.Redirect("~/admin/login.aspx", false);
        }
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
                    Decimal amounts=Convert.ToDecimal(result);

                    //API_balance.Visible = true;
                    //lblAPIBalance.Text = "<i class='fa fa-inr' aria-hidden='true'></i> " + Convert.ToString(amounts);
                }
            }
        }
        catch
        { }
    }


    private void BindNoOfAmountRequest()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = local_wallet.getNoOfRequestFundTransfer();
            if (dt.Rows.Count > 0)
            {
                //rpRequest.DataSource = dt;
                //rpRequest.DataBind();
            }
            else
            {
                dt.Columns.Add("norequest", typeof(string));
                DataRow row = dt.NewRow();
                row["norequest"] = "0";
                dt.Rows.Add(row);
                //rpRequest.DataSource = dt;
                //rpRequest.DataBind();
            }
        }
        catch
        { }
    }
}
