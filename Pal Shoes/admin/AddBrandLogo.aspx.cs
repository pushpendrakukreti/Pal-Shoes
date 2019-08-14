using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Data;
using SqlAccess;
using System.IO;
using System.Text;
using System.Drawing.Imaging;

public partial class admin_AddBrandLogo : System.Web.UI.Page
{
    Catergoy local_category = new Catergoy();
    SubCategory local_subcategory = new SubCategory();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Convert.ToString(Session["userid"])) && Convert.ToString(Session["logintype"]) == "Admin")
        {
            if (!IsPostBack)
            {
                BindBrand();
                BindBrands();
            }
        }
        else
        {
            Response.Redirect("Login.aspx");
        }
    }
    private void BindBrands()
    {
        DataTable dt = new DataTable();
        try
        {
            dt = local_category.getBrands();
            if (dt.Rows.Count > 0)
            {
                gvBrands.DataSource = dt;
                gvBrands.DataBind();
            }
        }
        catch
        { }
    }
    private void BindBrand()
    {
        DataTable dt = new DataTable();
        try
        {
            dt = local_category.getProductBrand();
            if (dt.Rows.Count > 0)
            {
                ddBrand.DataSource = dt;
                ddBrand.DataTextField = "Brand";
                ddBrand.DataValueField = "Brand";
                ddBrand.DataBind();
                ddBrand.Items.Insert(0, "--Select Brand--");
            }
        }
        catch
        { }
    }

    private void BindCategory(string brandname)
    {
        try
        {
            DataTable dt = new DataTable();
            dt = local_category.getCategoryOnBrand(brandname);
            if (dt.Rows.Count > 0)
            {
                ddCategory.DataSource = dt;
                ddCategory.DataTextField = "CategoryName";
                ddCategory.DataValueField = "CategoryId";
                ddCategory.DataBind();
                ddCategory.Items.Insert(0, "--Select Category--");
            }
        }
        catch
        { }
    }
    

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ViewState["ID"] = null;
        Response.Redirect("Default.aspx");
    }

    protected void btnAddBrand_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(Convert.ToString(ViewState["ID"])))
        {
            try
            {
                string ImageFile = "";
                if (UploadImage.HasFile)
                {
                    string thumbPath = "~/Upload/brand/";

                    ImageFile = Path.GetFileName(Server.MapPath(UploadImage.PostedFile.FileName));
                    ImageFile = System.DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + ImageFile;
                    thumbPath = thumbPath + ImageFile;
                    System.Drawing.Bitmap bmpUploadedImage = new System.Drawing.Bitmap(UploadImage.PostedFile.InputStream);
                    System.Drawing.Image objImage = ScaleImage(bmpUploadedImage, 120);
                    objImage.Save(Server.MapPath(thumbPath), ImageFormat.Png);

                    string brandname = Convert.ToString(ddBrand.SelectedValue);
                    string categoryid = "";
                    if (ddCategory.SelectedIndex > 0)
                    {
                        categoryid = Convert.ToString(ddCategory.SelectedValue);
                    }

                    int success = 0;
                    if (local_category.IsBrandExists(brandname, categoryid, "N", "") == "F")
                    {
                        success = local_category.AddBrand(brandname, categoryid, ImageFile);
                        if (success != 0)
                        {
                            lblMessage.ForeColor = Color.Green;
                            lblMessage.Text = "Brand Image Added";
                            BindBrands();
                        }
                    }
                    else
                    {
                        lblMessage.ForeColor = Color.Red;
                        lblMessage.Text = "Brand Image Already Exists";
                    }
                }
                else
                {
                    lblMessage.ForeColor = Color.Red;
                    lblMessage.Text = "Please Select Brand Image";
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }
        else
        { 
        }
    }

    protected void ddBrand_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddBrand.SelectedIndex > 0)
        {
            string brandname = Convert.ToString(ddBrand.SelectedValue);
            BindCategory(brandname);
        }
        else
        {
            ddCategory.Items.Clear();
        }
    }
    
    protected void gvBrands_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            string brandid = Convert.ToString(e.CommandArgument);
            Products.DeleteRecord("t_Brand", "BrandId", brandid);
            BindBrands();
        }
    }
    protected void gvBrands_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvBrands.PageIndex = e.NewPageIndex;
        BindBrands();
    }
    protected void gvBrands_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
    }


    public static System.Drawing.Image ScaleImage(System.Drawing.Image image, int maxImageHeight)
    {
        var ratio = (double)maxImageHeight / image.Height;
        var newWidth = (int)(image.Width * ratio);
        var newHeight = (int)(image.Height * ratio);
        var newImage = new Bitmap(newWidth, newHeight);
        using (var g = Graphics.FromImage(newImage))
        {
            g.DrawImage(image, 0, 0, newWidth, newHeight);
        }
        return newImage;
    }
}