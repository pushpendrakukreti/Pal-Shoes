using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SqlAccess;
using System.Drawing;

public partial class admin_AddnewCoupon : System.Web.UI.Page
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
                //BindUserEmailid();
            }
        }
        else
        {
            Response.Redirect("Login.aspx");
        }
    }

    //private void BindUserEmailid()
    //{
    //    try
    //    {
    //        DataTable dt = new DataTable();
    //        dt = local_category.getAllUserEmailid();
    //        if (dt.Rows.Count > 0)
    //        {
    //            ddEmailID.DataSource = dt;
    //            ddEmailID.DataTextField = "Emailid";
    //            ddEmailID.DataValueField = "UserId";
    //            ddEmailID.DataBind();
    //            ddEmailID.Items.Insert(0, "Select User Emailid");
    //        }
    //    }
    //    catch
    //    { }
    //}

    protected void btnCreateSubCategory_Click(object sender, EventArgs e)
    {
        string couponStatus = Convert.ToString(ddSpecialCoupon.SelectedValue);
        string couponcode = txtCouponCode.Text.Trim();
        //string useremail = ddEmailID.SelectedItem.ToString();
        //Int32 userid = Convert.ToInt32(ddEmailID.SelectedValue);
        decimal priceamount = Convert.ToDecimal(txtPrice.Text.Trim());
        int discount = Convert.ToInt32(txtDiscountpercent.Text);
        bool active=true;
        int result = 0;
        try
        {
            if (local_product.IfCouponExists(couponcode) == "F")
            {
                //result= local_product.AddNewCoupon(useremail, userid, couponcode, discount, priceamount, active);
                result = local_product.AddNewCoupon(couponStatus, couponcode, discount, priceamount, active);

                if (result != 0)
                {
                    lblMessage.ForeColor = Color.Green;
                    lblMessage.Text = "New Coupon Created & Coupon-Code : "+txtCouponCode.Text;

                    txtCouponCode.Text = "";
                    txtDiscountpercent.Text = "";
                    txtPrice.Text = "";
                    ddSpecialCoupon.SelectedIndex = 0;
                }
            }
            else
            {
                lblMessage.ForeColor = Color.Red;
                lblMessage.Text = "Coupon-code already exists!";
            }
        }
        catch (Exception ex)
        {
            lblMessage.ForeColor = Color.Red;
            lblMessage.Text = ex.Message;
        }
    }
}