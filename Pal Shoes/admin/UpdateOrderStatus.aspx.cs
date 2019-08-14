using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SqlAccess;
using System.Drawing;

public partial class admin_UpdateOrderStatus : System.Web.UI.Page
{
    OrderDetails obj_order = new OrderDetails();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Convert.ToString(Session["userid"])) && Convert.ToString(Session["logintype"]) == "Admin")
        {
            if (!IsPostBack)
            {
                BindDropOrderStatus();

                if (Session["OrderID"] != null)
                {
                    BindOrderStatusInfo(Session["OrderID"].ToString());
                }
            }
        }
        else
        {
            Response.Redirect("Login.aspx");
        }
    }

    private void BindDropOrderStatus()
    {
        ddOrderStatus.Items.Add("Product in Shipping");
        ddOrderStatus.Items.Add("Order Completed");
        ddOrderStatus.DataBind();
        ddOrderStatus.Items.Insert(0, "Select Order Status");
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string orderstatus = "";
        if (ddOrderStatus.SelectedIndex > 0)
        {
            orderstatus = ddOrderStatus.SelectedValue;
        }
        string deliverydate = txtDate1.Text;
        deliverydate = Convert.ToDateTime(deliverydate).ToString("yyyy-MM-dd HH:mm:ss");
        string orderdtlno = txtOrderNo.Text;
        string orderid = Session["OrderID"].ToString();
        int success = 0;
        try
        {
            success = obj_order.UpdateOrderStatus(orderstatus, deliverydate, orderdtlno, orderid);
            if (success != 0)
            {
                lblMessage.ForeColor = Color.Green;
                lblMessage.Text = "Order Status Updated!";
                Response.Redirect("OrderDetails.aspx", false);
            }
        }
        catch(Exception ex)
        {
            lblMessage.ForeColor = Color.Red;
            lblMessage.Text = ex.Message;
        }
    }

    private void BindOrderStatusInfo(string orderid)
    {
        DataTable dt=new DataTable();
        dt = obj_order.getOrderStatusInfo(orderid);
        if (dt.Rows.Count > 0)
        {
            txtOrderNo.Text = dt.Rows[0]["Orderdetailid"].ToString();
            txtOrderNo.Enabled = false;
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("OrderDetails.aspx");
    }
}