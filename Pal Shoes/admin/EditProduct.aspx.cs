using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SqlAccess;
using System.Drawing;

public partial class admin_EditProduct : System.Web.UI.Page
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
                if (Session["Category"] != null)
                {
                    string category= Session["Category"].ToString();
                    string subcategory = "";
                    if (ddSubCategory.SelectedIndex > 0)
                    {
                        subcategory = Convert.ToString(ddCategory.SelectedValue);
                    }
                    BindgvProductByCategory(category, subcategory);
                }
                BindCategories();
                BindProductypes();
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
            dt = local_product.getProdtucts();
            if (dt.Rows.Count > 0)
            {
                gvProduct.DataSource = dt;
                gvProduct.DataBind();
            }
            else
            {
                gvProduct.DataSource = null;
                gvProduct.DataBind();
            }
        }
        catch
        { }
    }
    private void BindgvProductByCategory(string categoryid, string subcategory)
    {
        try
        {
            DataTable dt = new DataTable();
            dt = local_product.getSearchProductBySubCatgAndCategory(categoryid, subcategory);
            if (dt.Rows.Count > 0)
            {
                gvProduct.DataSource = dt;
                gvProduct.DataBind();
            }
            else
            {
                gvProduct.DataSource = null;
                gvProduct.DataBind();
            }
        }
        catch
        { }
    }
    private void BindCategories()
    {
        DataTable dt = new DataTable();
        try
        {
            dt = local_category.getAllCategory();
            if (dt.Rows.Count > 0)
            {
                ddCategory.DataSource = dt;
                ddCategory.DataTextField = "CategoryName";
                ddCategory.DataValueField = "CategoryId";
                ddCategory.DataBind();
                ddCategory.Items.Insert(0, "Select Category");
            }
        }
        catch
        { }
    }

    private void BindProductypes()
    {
        ddProducttype.Items.Add("Offer Product");
        ddProducttype.Items.Add("New Product");
        ddProducttype.Items.Add("Treding Product");
        ddProducttype.Items.Add("Feature Product");
        ddProducttype.DataBind();
        ddProducttype.Items.Insert(0, "Select Product Type");
    }

    private void BindSubCategory(string category)
    {
        try
        {
            DataTable dt = new DataTable();
            dt = local_subcategory.getSubCategoryInCategoery(category);
            if (dt.Rows.Count > 0)
            {
                ddSubCategory.DataSource = dt;
                ddSubCategory.DataTextField = "SubName";
                ddSubCategory.DataValueField = "SubCategoryId";
                ddSubCategory.DataBind();
                ddSubCategory.Items.Insert(0, "--Select Sub-Category--");
            }
            else
            {
                ddSubCategory.Items.Clear();
            }
        }
        catch
        { }
    }

    protected void gvProduct_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            Response.Redirect("~/admin/AddProduct.aspx?id=" + e.CommandArgument);
        }
    }

    protected void DeleteRecord(object sender, EventArgs e)
    {
        try
        {
            int id = int.Parse((sender as Button).CommandArgument);
            int success = 0;
            success = local_product.DeleteProduct(id);
            if (success != 0)
            {
                BindgvProduct();
                lblmessage.ForeColor = Color.Green;
                lblmessage.Text = "Product Deleted Successfully!";
            }
            else
            {
                lblmessage.ForeColor = Color.Red;
                lblmessage.Text = "Product Delete Failed?";
            }
        }
        catch (Exception ex)
        {
            lblmessage.ForeColor = Color.Red;
            lblmessage.Text = ex.Message;
        }
    }
    protected void gvProduct_RowDeleting1(object sender, GridViewDeleteEventArgs e)
    {
    }
    protected void gvProduct_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvProduct.PageIndex = e.NewPageIndex;
        if (ddCategory.SelectedIndex > 0)
        {
            string categoryid = Convert.ToString(ddCategory.SelectedValue);
            string subcategory = "";
            if (ddSubCategory.SelectedIndex > 0)
            {
                subcategory = Convert.ToString(ddCategory.SelectedValue);
            }
            BindgvProductByCategory(categoryid, subcategory);
        }
        else
        {
            BindgvProduct();
        }
    }
    protected void ddCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddCategory.SelectedIndex > 0)
        {
            ddProducttype.Enabled = false;

            string categoryid = Convert.ToString(ddCategory.SelectedValue);
            BindSubCategory(categoryid);
            string subcategory ="";
            if (ddSubCategory.SelectedIndex > 0)
            {
                subcategory = Convert.ToString(ddCategory.SelectedValue);
            }
            BindgvProductByCategory(categoryid, subcategory);
            Session["Category"] = categoryid;
        }
        else
        {
            BindgvProduct();
            ddSubCategory.Items.Clear();
            ddProducttype.Enabled = true;
        }
    }

    protected void ddSubCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddCategory.SelectedIndex > 0)
        {
            ddProducttype.Enabled = false;
            string categoryid = Convert.ToString(ddCategory.SelectedValue);
            string subcategory = "";
            if (ddSubCategory.SelectedIndex > 0)
            {
                subcategory = Convert.ToString(ddSubCategory.SelectedValue);
            }
            BindgvProductByCategory(categoryid, subcategory);
            Session["Category"] = categoryid;
        }
        else
        {
            BindgvProduct();
            ddSubCategory.Items.Clear();
        }
    }

    protected void ddProducttype_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        if (ddProducttype.SelectedIndex > 0)
        {
            ddCategory.Enabled = false;
            ddSubCategory.Enabled = false;
            string ptype = ddProducttype.SelectedValue;
            string types = "";
            if (ptype == "Offer Product")
            { types = "1"; }
            else if (ptype == "New Product")
            { types = "2"; }
            else if (ptype == "Treding Product")
            { types = "3"; }
            else if (ptype == "Feature Product")
            { types = "4"; }

            try
            {
                dt = local_product.getProductByProductType(types);
                if (dt.Rows.Count > 0)
                {
                    gvProduct.DataSource = dt;
                    gvProduct.DataBind();
                }
            }
            catch
            { }
        }
        else
        {
            ddCategory.Enabled = true;
            ddSubCategory.Enabled = true;
            BindgvProduct();
        }
    }
}