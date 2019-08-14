using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SqlAccess;
using System.Drawing;

public partial class admin_WishList : System.Web.UI.Page
{
    OrderDetails local_order = new OrderDetails();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Convert.ToString(Session["userid"])) && Convert.ToString(Session["logintype"]) == "Admin")
        {
            if (!IsPostBack)
            {
                BindWishlist();
            }
        }
        else
        {
            Response.Redirect("Login.aspx");
        }
    }

    private void BindWishlist()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = local_order.getWishlistAdmin();
            if (dt.Rows.Count > 0)
            {
                gvWishlist.DataSource = dt;
                gvWishlist.DataBind();
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
    protected void gvWishlist_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        
    }

    protected void DeleteRecord(object sender, EventArgs e)
    {
        try
        {
            int id = int.Parse((sender as LinkButton).CommandArgument);
            int success = 0;
            success = local_order.DeletWishlist(id);
            if (success != 0)
            {
                BindWishlist();
                lblMessage.ForeColor = Color.Green;
                lblMessage.Text = "Wishlist Deleted Successfully";
            }
            else
            {
                lblMessage.ForeColor = Color.Red;
                lblMessage.Text = "Category Delete Failed";
            }
        }
        catch (Exception ex)
        {
            lblMessage.ForeColor = Color.Red;
            lblMessage.Text = ex.Message;
        }
    }

    protected void gvWishlist_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvWishlist.PageIndex = e.NewPageIndex;
        BindWishlist();
    }
}