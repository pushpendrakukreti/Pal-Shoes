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

public partial class admin_CreateSubcategory : System.Web.UI.Page
{
    Catergoy local_category = new Catergoy();
    SubCategory local_subcategory = new SubCategory();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Convert.ToString(Session["userid"])) && Convert.ToString(Session["logintype"]) == "Admin")
        {
            if (!IsPostBack)
            {
                BindCategory();
                BindSubCategory();
                string subcategory = Request.QueryString["id"];
                BindSubCategoryById(subcategory);

                if (ViewState["ID"] != null)
                {
                    btnCreateSubCategory.Text = "Update";
                }
            }
        }
        else
        {
            Response.Redirect("Login.aspx");
        }
    }

    private void BindCategory()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = local_category.getAllCategory();
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

    protected void btnCreateSubCategory_Click(object sender, EventArgs e)
    {
        if (ddCategory.SelectedIndex > 0)
        {
            if (txtSubCategory.Text.Length > 0)
            {
                if (string.IsNullOrEmpty(Convert.ToString(ViewState["ID"])))
                {
                    try
                    {
                        string ImageFile = "";
                        if (UploadImage.HasFile)
                        {
                            string imgPath = "~/Upload/images/";
                            string thumbPath = "~/Upload/thumbnails/";

                            ImageFile = Path.GetFileName(Server.MapPath(UploadImage.PostedFile.FileName));
                            ImageFile = System.DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + ImageFile;
                            thumbPath = thumbPath + ImageFile;
                            System.Drawing.Bitmap bmpUploadedImage = new System.Drawing.Bitmap(UploadImage.PostedFile.InputStream);
                            System.Drawing.Image objImage = ScaleImage(bmpUploadedImage, 250);
                            objImage.Save(Server.MapPath(thumbPath), ImageFormat.Png);

                            imgPath = imgPath + ImageFile;
                            UploadImage.SaveAs(MapPath(imgPath));
                        }

                        string categoryid = Convert.ToString(ddCategory.SelectedValue);
                        int success = 0;
                        success = local_subcategory.AddSubCategory(categoryid, txtSubCategory.Text, txtDescription.Text, ImageFile, ImageFile);
                        if (success != 0)
                        {
                            clear();
                            lblMessage.ForeColor = Color.Green;
                            lblMessage.Text = "Sub Category Created Successfully";
                            BindSubCategory();
                        }
                        else
                        {
                            lblMessage.ForeColor = Color.Red;
                            lblMessage.Text = "Sub Category Creation Failed";
                        }
                    }
                    catch (Exception ex)
                    {
                        lblMessage.Text = ex.Message;
                    }
                }
                else //Update subcategory
                {
                    //string ImageFile;
                    //if (UploadImage.HasFile)
                    //{
                    //    string imgPath = "~/Upload/images/";
                    //    string thumbPath = "~/Upload/thumbnails/";

                    //    ImageFile = Path.GetFileName(Server.MapPath(UploadImage.PostedFile.FileName));
                    //    ImageFile = System.DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + ImageFile;
                    //    thumbPath = thumbPath + ImageFile;
                    //    System.Drawing.Bitmap bmpUploadedImage = new System.Drawing.Bitmap(UploadImage.PostedFile.InputStream);
                    //    System.Drawing.Image objImage = ScaleImage(bmpUploadedImage, 250);
                    //    objImage.Save(Server.MapPath(thumbPath), ImageFormat.Png);

                    //    imgPath = imgPath + ImageFile;
                    //    UploadImage.SaveAs(MapPath(imgPath));
                    //}
                    //else
                    //{
                    //    ImageFile = lblImage.Text;
                    //}

                    try
                    {
                        Int32 categoryid = Convert.ToInt32(ddCategory.SelectedValue);
                        Int32 subcategoryid = Convert.ToInt32(ViewState["ID"]);
                        int success = 0;
                        success = local_subcategory.UpdateSubCategory(subcategoryid, txtSubCategory.Text, txtDescription.Text, categoryid);
                        if (success != 0)
                        {
                            clear();
                            lblMessage.ForeColor = Color.Green;
                            lblMessage.Text = "Sub Category Updated Successfully";
                            BindSubCategory();
                            ViewState["ID"] = null;
                            lblImage.Text = "";
                            btnCreateSubCategory.Text = "Create Sub Category";
                        }
                        else
                        {
                            lblMessage.ForeColor = Color.Red;
                            lblMessage.Text = "Sub Category Update Failed";
                        }
                    }
                    catch (Exception ex)
                    {
                        lblMessage.Text = ex.Message;
                    }
                }
            }
            else
            {
                lblMessage.ForeColor = Color.Red;
                lblMessage.Text = "Enter Sub Category Name";
            }
        }
        else
        {
            lblMessage.ForeColor = Color.Red;
            lblMessage.Text = "Select Category Name";
        }
    }

    private void BindSubCategory()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = local_subcategory.getAllSubCategory();
            if (dt.Rows.Count > 0)
            {
                gvSubCategory.DataSource = dt;
                gvSubCategory.DataBind();
            }
        }
        catch
        { }
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

    private void clear()
    {
        txtSubCategory.Text = "";
        txtDescription.Text = "";
    }

    private void BindSubCategoryById(string subcategoryid)
    {
        try
        {
            DataTable dt = new DataTable();
            dt = local_subcategory.GetSubCategoryById(subcategoryid);
            if (dt.Rows.Count > 0)
            {
                txtSubCategory.Text = Convert.ToString(dt.Rows[0]["SubName"]);
                ddCategory.SelectedValue = Convert.ToString(dt.Rows[0]["CategoryId"]);
                txtDescription.Text = Convert.ToString(dt.Rows[0]["SubDescription"]);
                lblImage.Text = Convert.ToString(dt.Rows[0]["Image"]);
                ViewState["ID"] = Convert.ToString(dt.Rows[0]["SubCategoryId"]);
            }
        }
        catch
        { }
    }

    protected void gvSubCategory_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            Response.Redirect("~/admin/CreateSubcategory.aspx?id=" + e.CommandArgument);
        }
    }
    protected void gvSubCategory_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ViewState["ID"] = null;
        Response.Redirect("CreateSubcategory.aspx");
    }

    protected void DeleteRecord(object sender, EventArgs e)
    {
        try
        {
            int id = int.Parse((sender as Button).CommandArgument);
            int success = 0;
            success = local_subcategory.DeleteSubCategory(id);
            if (success != 0)
            {
                lblMessage.ForeColor = Color.Green;
                lblMessage.Text = "Sub Category Deleted";
                BindSubCategory();
            }
            else
            {
                lblMessage.ForeColor = Color.Red;
                lblMessage.Text = "Sub Category Deltete Failed";
            }
        }
        catch (Exception ex)
        {
            lblMessage.ForeColor = Color.Red;
            lblMessage.Text = ex.Message;
        }
    }
}