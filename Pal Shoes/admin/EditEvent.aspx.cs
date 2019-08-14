using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;

public partial class admin_EditEvent : System.Web.UI.Page
{
    Products local_product = new Products();
    Catergoy local_category = new Catergoy();
    SubCategory local_subcategory = new SubCategory();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Convert.ToString(Session["userid"])) && Convert.ToString(Session["logintype"]) == "Admin")
        {
            if (!IsPostBack)
            {
                BindgvProduct();
            }
        }
        else
        {
            Response.Redirect("Login.aspx");
        }

    }

    private void BindgvProduct()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = local_product.getAllEvents();
            if (dt.Rows.Count > 0)
            {
                gvEvent.DataSource = dt;
                gvEvent.DataBind();
            }
        }
        catch
        { }
    }
    protected void gvEvent_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            Response.Redirect("~/admin/Addnewevent.aspx?eventid=" + e.CommandArgument);
        }
    }

    protected void DeleteRecord(object sender, EventArgs e)
    {
        try
        {
            int id = int.Parse((sender as Button).CommandArgument);
            int success = 0;
            success = local_product.DeleteEvent(id);
            if (success != 0)
            {
                BindgvProduct();
                lblmessage.ForeColor = Color.Green;
                lblmessage.Text = "Event Deleted Successfully!";
            }
            else
            {
                lblmessage.ForeColor = Color.Red;
                lblmessage.Text = "Event Delete Failed?";
            }
        }
        catch (Exception ex)
        {
            lblmessage.ForeColor = Color.Red;
            lblmessage.Text = ex.Message;
        }
    }
    protected void gvEvent_RowDeleting1(object sender, GridViewDeleteEventArgs e)
    {
    }
 
    protected void gvProduct_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvEvent.PageIndex = e.NewPageIndex;
    }
}