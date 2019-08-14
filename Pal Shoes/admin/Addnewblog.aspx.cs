using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing.Imaging;
using System.Drawing;
using System.Data;
using System.IO;
public partial class admin_Addnewblog : System.Web.UI.Page
{
    Products local_product = new Products();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Convert.ToString(Session["userid"])) && Convert.ToString(Session["logintype"]) == "Admin")
        {
            if (!IsPostBack)
            {
                string blogid = Request.QueryString["blogid"];
                if (blogid != null)
                {
                    BindBlogByblogid(blogid);
                }

                if (ViewState["ID"] != null)
                {
                    btnAddBlog.Text = "Update";
                   // txtBlogtitle.Enabled = true;
                }
            }
        }
        else
        {
            Response.Redirect("Login.aspx");
        }
    }

    protected void btnAddBlog_Click(object sender, EventArgs e)
    {
        string title = txtBlogtitle.Text;
        string description = CKEditor1.Text.Trim();
        //int categoryid = Convert.ToInt32(ddCategory.SelectedValue);
        string date = DateTime.Now.ToString("dd/MM/yyyy");
        int result = 0;
        try
        {
            //if (string.IsNullOrEmpty(Convert.ToString(Session["blogid"])))
            if (string.IsNullOrEmpty(Convert.ToString(ViewState["ID"])))
            {
                string blogimage = "";
                if (UploadImage.HasFile)
                {
                    string imgPath = "~/Upload/blog/";
                    //string thumbPath = "~/Upload/thumbnails/";

                    string ImageFile = Path.GetFileName(Server.MapPath(UploadImage.PostedFile.FileName));
                    blogimage = System.DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + ImageFile;
                    //thumbPath = thumbPath + blogimage;
                    System.Drawing.Bitmap bmpUploadedImage = new System.Drawing.Bitmap(UploadImage.PostedFile.InputStream);
                    System.Drawing.Image objImage = ScaleImage(bmpUploadedImage, 300);
                    //System.Drawing.Image objImage = ScaleImage(bmpUploadedImage);
                    //objImage.Save(Server.MapPath(thumbPath), ImageFormat.Png);

                    imgPath = imgPath + blogimage;
                    UploadImage.SaveAs(MapPath(imgPath));
                }


                result = local_product.AddNewBlog(title, description, date, blogimage);

                if (result != 0)
                {
                    lblMessage.Text = "New blog added";
                    lblMessage.ForeColor = Color.Green;
                    BindBlog();
                    txtBlogtitle.Text = "";
                    CKEditor1.Text = "";
                }
                else
                {
                    lblMessage.Text = "Blog not added?";
                    lblMessage.ForeColor = Color.Red;
                }
            }
            else //edit
            {
                string blogimage = "";
                if (UploadImage.HasFile)
                {
                    string imgPath = "~/Upload/blog/";
                    //string thumbPath = "~/Upload/thumbnails/";

                    string ImageFile = Path.GetFileName(Server.MapPath(UploadImage.PostedFile.FileName));
                    blogimage = System.DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + ImageFile;
                    //thumbPath = thumbPath + blogimage;
                    System.Drawing.Bitmap bmpUploadedImage = new System.Drawing.Bitmap(UploadImage.PostedFile.InputStream);
                    System.Drawing.Image objImage = ScaleImage(bmpUploadedImage, 300);
                    //System.Drawing.Image objImage = ScaleImage(bmpUploadedImage);
                    //objImage.Save(Server.MapPath(thumbPath), ImageFormat.Png);

                    imgPath = imgPath + blogimage;
                    UploadImage.SaveAs(MapPath(imgPath));
                }
                else
                {
                    blogimage = lblImage.Text;
                }

                string blogid = Request.QueryString["blogid"];
                result = local_product.UpdateBlog(title, description, blogid, blogimage);

                if (result != 0)
                {
                    lblMessage.Text = "Blog updated successfully";
                    lblMessage.ForeColor = Color.Green;
                    BindBlog();
                    txtBlogtitle.Text = "";
                    CKEditor1.Text = "";
                    Session["blogid"] = null;
                    Session["ID"] = null;
                    btnAddBlog.Text = "Add Blog";
                    lblImage.Text = "";
                }
                else
                {
                    lblMessage.Text = "Blog not updated?";
                    lblMessage.ForeColor = Color.Red;
                }
            }
        }
        catch(Exception ex)
        { throw new Exception(ex.Message); }
      }

    private void BindBlog()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = local_product.getAllBlogs();
            if (dt.Rows.Count > 0)
            {
                //gvBlog.DataSource = dt;
                //gvBlog.DataBind();
            }
        }
        catch
        { }
    }

    protected void gvBlog_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            string blogid = Convert.ToString(e.CommandArgument);
            Session["ID"] = blogid;
            Response.Redirect("Addnewblog.aspx");
        }
        if (e.CommandName == "Delete")
        {
            string blogid = Convert.ToString(e.CommandArgument);
            int result = 0;
            result = local_product.DeleteBlog(Convert.ToInt32(blogid));
            if (result != 0)
            {
                lblMessage.Text = "Blog Deleted";
                lblMessage.ForeColor = Color.Green;
                BindBlog();
            }
        }
    }

    protected void gvBlog_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //gvBlog.PageIndex = e.NewPageIndex;
        BindBlog();
    }
    protected void gvBlog_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    private void BindBlogByblogid(string blogid)
    {
        try
        {
            DataTable dt = new DataTable();
            dt = local_product.getBlogByid(blogid);
            if (dt.Rows.Count > 0)
            {
                txtBlogtitle.Text = Convert.ToString(dt.Rows[0]["Blogtitle"]);
                CKEditor1.Text = Convert.ToString(dt.Rows[0]["Blogbody"]);
                lblImage.Text = Convert.ToString(dt.Rows[0]["imagefile"]);
                
                ViewState["ID"] = Convert.ToString(dt.Rows[0]["Blogid"]);
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
    //public static System.Drawing.Image ScaleImage(System.Drawing.Image image)
    //{
    //    //var ratio = (double)maxImageHeight / image.Height;
    //    var newWidth = (int)(270);
    //    var newHeight = (int)(160);
    //    var newImage = new Bitmap(newWidth, newHeight);
    //    using (var g = Graphics.FromImage(newImage))
    //    {
    //        g.DrawImage(image, 0, 0, newWidth, newHeight);
    //    }
    //    return newImage;
    //}


    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/admin/Addnewblog.aspx");
    }
}