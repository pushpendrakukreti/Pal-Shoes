using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using SqlAccess;

public partial class admin_CouponDetails : System.Web.UI.Page
{
    Catergoy local_category = new Catergoy();
    SubCategory local_sucategory = new SubCategory();
    Products local_product = new Products();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Convert.ToString(Session["username"])) && Convert.ToString(Session["logintype"]) == "Admin")
        {
            if (!IsPostBack)
            {
                BindCouponDetail();
                BindSpecialCouponDetail();
            }
        }
        else
        {
            Response.Redirect("Login.aspx");
        }
    }
    

    private void BindCouponDetail()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = local_product.getCouponAllDetails();
            if (dt.Rows.Count > 0)
            {
                gvCoupon.DataSource = dt;
                gvCoupon.DataBind();
            }
        }
        catch
        { }
    }

    protected void gvCoupon_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            string cid = Convert.ToString(e.CommandArgument);
            local_product.ProductCoupon(cid);
            lblMessage.Text = "Data Deleted Successfully";
            BindCouponDetail();
        }
    }

    protected void gvCoupon_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }


    //Code For Special Coupons

    private void BindSpecialCouponDetail()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = local_product.getSpecialCouponAllDetails();
            if (dt.Rows.Count > 0)
            {
                gvSpecialCoupon.DataSource = dt;
                gvSpecialCoupon.DataBind();
            }
        }
        catch
        { }
    }

    protected void gvSpecialCoupon_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            string cid = Convert.ToString(e.CommandArgument);
            local_product.ProductCoupon(cid);
            lblMessage.Text = "Data Deleted Successfully";
            BindSpecialCouponDetail();
        }
    }

    protected void gvSpecialCoupon_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
}