using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
public partial class admin_EditBlog : System.Web.UI.Page
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
                //if (Session["Category"] != null)
                //{
                //    string category = Session["Category"].ToString();
                //    string subcategory = "";
                //    if (ddSubCategory.SelectedIndex > 0)
                //    {
                //        subcategory = Convert.ToString(ddCategory.SelectedValue);
                //    }
                //    BindgvProductByCategory(category, subcategory);
                //}
                //BindCategories();
                //BindProductypes();
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
            dt = local_product.getAllBlogs();
            if (dt.Rows.Count > 0)
            {
                gvBlog.DataSource = dt;
                gvBlog.DataBind();
            }
        }
        catch
        { }
    }
    //private void BindgvProductByCategory(string categoryid, string subcategory)
    //{
    //    try
    //    {
    //        DataTable dt = new DataTable();
    //        dt = local_product.getSearchProductBySubCatgAndCategory(categoryid, subcategory);
    //        if (dt.Rows.Count > 0)
    //        {
    //            gvProduct.DataSource = dt;
    //            gvProduct.DataBind();
    //        }
    //        else
    //        {
    //            gvProduct.DataSource = null;
    //            gvProduct.DataBind();
    //        }
    //    }
    //    catch
    //    { }
    //}
    //private void BindCategories()
    //{
    //    DataTable dt = new DataTable();
    //    try
    //    {
    //        dt = local_category.getAllCategory();
    //        if (dt.Rows.Count > 0)
    //        {
    //            ddCategory.DataSource = dt;
    //            ddCategory.DataTextField = "CategoryName";
    //            ddCategory.DataValueField = "CategoryId";
    //            ddCategory.DataBind();
    //            ddCategory.Items.Insert(0, "Select Category");
    //        }
    //    }
    //    catch
    //    { }
    //}

    //private void BindProductypes()
    //{
    //    ddProducttype.Items.Add("Offer Product");
    //    ddProducttype.Items.Add("New Product");
    //    ddProducttype.Items.Add("Treding Product");
    //    ddProducttype.Items.Add("Feature Product");
    //    ddProducttype.DataBind();
    //    ddProducttype.Items.Insert(0, "Select Product Type");
    //}

    //private void BindSubCategory(string category)
    //{
    //    try
    //    {
    //        DataTable dt = new DataTable();
    //        dt = local_subcategory.getSubCategoryInCategoery(category);
    //        if (dt.Rows.Count > 0)
    //        {
    //            ddSubCategory.DataSource = dt;
    //            ddSubCategory.DataTextField = "SubName";
    //            ddSubCategory.DataValueField = "SubCategoryId";
    //            ddSubCategory.DataBind();
    //            ddSubCategory.Items.Insert(0, "--Select Sub-Category--");
    //        }
    //        else
    //        {
    //            ddSubCategory.Items.Clear();
    //        }
    //    }
    //    catch
    //    { }
    //}

    protected void gvBlog_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            Response.Redirect("~/admin/Addnewblog.aspx?blogid=" + e.CommandArgument);
        }
    }

    protected void DeleteRecord(object sender, EventArgs e)
    {
        try
        {
            int id = int.Parse((sender as Button).CommandArgument);
            int success = 0;
            success = local_product.DeleteBlog(id);
            if (success != 0)
            {
                BindgvProduct();
                lblmessage.ForeColor = Color.Green;
                lblmessage.Text = "Blog Deleted Successfully!";
            }
            else
            {
                lblmessage.ForeColor = Color.Red;
                lblmessage.Text = "Blog Delete Failed?";
            }
        }
        catch (Exception ex)
        {
            lblmessage.ForeColor = Color.Red;
            lblmessage.Text = ex.Message;
        }
    }
    protected void gvBlog_RowDeleting1(object sender, GridViewDeleteEventArgs e)
    {
    }
    //protected void gvProduct_PageIndexChanging(object sender, GridViewPageEventArgs e)
    //{
    //    gvProduct.PageIndex = e.NewPageIndex;
    //    if (ddCategory.SelectedIndex > 0)
    //    {
    //        string categoryid = Convert.ToString(ddCategory.SelectedValue);
    //        string subcategory = "";
    //        if (ddSubCategory.SelectedIndex > 0)
    //        {
    //            subcategory = Convert.ToString(ddCategory.SelectedValue);
    //        }
    //        BindgvProductByCategory(categoryid, subcategory);
    //    }
    //    else
    //    {
    //        BindgvProduct();
    //    }
    //}
    //protected void ddCategory_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddCategory.SelectedIndex > 0)
    //    {
    //        ddProducttype.Enabled = false;

    //        string categoryid = Convert.ToString(ddCategory.SelectedValue);
    //        BindSubCategory(categoryid);
    //        string subcategory = "";
    //        if (ddSubCategory.SelectedIndex > 0)
    //        {
    //            subcategory = Convert.ToString(ddCategory.SelectedValue);
    //        }
    //        BindgvProductByCategory(categoryid, subcategory);
    //        Session["Category"] = categoryid;
    //    }
    //    else
    //    {
    //        BindgvProduct();
    //        ddSubCategory.Items.Clear();
    //        ddProducttype.Enabled = true;
    //    }
    //}

    protected void gvProduct_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvBlog.PageIndex = e.NewPageIndex;
        //if (ddCategory.SelectedIndex > 0)
        //{
        //    string categoryid = Convert.ToString(ddCategory.SelectedValue);
        //    string subcategory = "";
        //    if (ddSubCategory.SelectedIndex > 0)
        //    {
        //        subcategory = Convert.ToString(ddCategory.SelectedValue);
        //    }
        //    BindgvProductByCategory(categoryid, subcategory);
        //}
        //else
        //{
        //    BindgvProduct();
        //}
    }

}