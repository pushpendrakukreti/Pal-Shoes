using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Drawing.Imaging;
using System.Data;
using SqlAccess;
using System.IO;
using System.Drawing.Drawing2D;

public partial class admin_BackImage : System.Web.UI.Page
{
    Products obj_product = new Products();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Convert.ToString(Session["userid"])) && Convert.ToString(Session["logintype"]) == "Admin")
        {
            if (!IsPostBack)
            {
                string productid= Request.QueryString["product"];
                if (productid != null)
                {
                    BindBackImg(productid);
                }
            }
        }
        else
        {
            Response.Redirect("Login.aspx");
        }
    }

    private void BindBackImg(string productid)
    {
        try
        {
            DataTable dt = new DataTable();
            dt = obj_product.getBackImages(productid);
            if (dt.Rows.Count > 0)
            {
                gvBackImage.DataSource = dt;
                gvBackImage.DataBind();

                lblImage.Text = dt.Rows[0]["BackImage"].ToString();
                lblImage2.Text = dt.Rows[0]["LeftImage"].ToString();
                lblImage3.Text = dt.Rows[0]["RightImage"].ToString();
            }
        }
        catch
        { }
    }

    protected void btnBackImage_Click(object sender, EventArgs e)
    {
        try
        {
            string productid = Request.QueryString["product"];
            if (productid != null)
            {
                string ImageFile = "";
                if (UploadImage.HasFile)
                {
                    string imgPath = "~/Upload/images/";
                    string thumbPath = "~/Upload/thumbnails/";
                    string smallPath = "~/Upload/small/";

                    ImageFile = Path.GetFileName(Server.MapPath(UploadImage.PostedFile.FileName));
                    ImageFile = System.DateTime.Now.ToString("yyyyMMddHHmmss") + "-back-" + ImageFile;
                    thumbPath = thumbPath + ImageFile;
                    System.Drawing.Bitmap bmpUploadedImage = new System.Drawing.Bitmap(UploadImage.PostedFile.InputStream);
                    System.Drawing.Image objImage = ScaleImage(bmpUploadedImage);
                    objImage.Save(Server.MapPath(thumbPath), ImageFormat.Png);

                    smallPath = smallPath + ImageFile;
                    System.Drawing.Image objImage2 = ScaleImage2(bmpUploadedImage);
                    objImage2.Save(Server.MapPath(smallPath), ImageFormat.Png);

                    imgPath = imgPath + ImageFile;
                    UploadImage.SaveAs(MapPath(imgPath));
                }
                else
                {
                    ImageFile = lblImage.Text;
                }

                string ImageFile2 = "";
                if (UploadImage2.HasFile)
                {
                    string imgPath = "~/Upload/images/";
                    string thumbPath = "~/Upload/thumbnails/";
                    string smallPath = "~/Upload/small/";

                    ImageFile2 = Path.GetFileName(Server.MapPath(UploadImage2.PostedFile.FileName));
                    ImageFile2 = System.DateTime.Now.ToString("yyyyMMddHHmmss") + "-back-" + ImageFile2;
                    thumbPath = thumbPath + ImageFile2;
                    System.Drawing.Bitmap bmpUploadedImage = new System.Drawing.Bitmap(UploadImage2.PostedFile.InputStream);
                    System.Drawing.Image objImage = ScaleImage(bmpUploadedImage);
                    objImage.Save(Server.MapPath(thumbPath), ImageFormat.Png);

                    smallPath = smallPath + ImageFile2;
                    System.Drawing.Image objImage2 = ScaleImage2(bmpUploadedImage);
                    objImage2.Save(Server.MapPath(smallPath), ImageFormat.Png);

                    imgPath = imgPath + ImageFile2;
                    UploadImage2.SaveAs(MapPath(imgPath));
                }
                else
                {
                    ImageFile2 = lblImage2.Text;
                }

                string ImageFile3 = "";
                if (UploadImage3.HasFile)
                {
                    string imgPath = "~/Upload/images/";
                    string thumbPath = "~/Upload/thumbnails/";
                    string smallPath = "~/Upload/small/";

                    ImageFile3 = Path.GetFileName(Server.MapPath(UploadImage3.PostedFile.FileName));
                    ImageFile3 = System.DateTime.Now.ToString("yyyyMMddHHmmss") + "-back-" + ImageFile3;
                    thumbPath = thumbPath + ImageFile3;
                    System.Drawing.Bitmap bmpUploadedImage = new System.Drawing.Bitmap(UploadImage3.PostedFile.InputStream);
                    System.Drawing.Image objImage = ScaleImage(bmpUploadedImage);
                    objImage.Save(Server.MapPath(thumbPath), ImageFormat.Png);

                    smallPath = smallPath + ImageFile3;
                    System.Drawing.Image objImage2 = ScaleImage2(bmpUploadedImage);
                    objImage2.Save(Server.MapPath(smallPath), ImageFormat.Png);

                    imgPath = imgPath + ImageFile3;
                    UploadImage3.SaveAs(MapPath(imgPath));
                }
                else
                {
                    ImageFile3 = lblImage3.Text;
                }

                int result = 0;

                result = obj_product.AddOtherProductImage(productid, ImageFile, ImageFile2, ImageFile3);
                if (result != 0)
                {
                    lblMessage.ForeColor = Color.Green;
                    lblMessage.Text = "Product Other Image Added!";
                    BindBackImg(productid);
                }
            }
            else
            {
                lblMessage.ForeColor = Color.Red;
                lblMessage.Text = "Go View Product & select Product!";
            }
        }
        catch (Exception ex)
        {
            lblMessage.ForeColor = Color.Red;
            lblMessage.Text = ex.Message;
        }
    }




    //public static System.Drawing.Image ScaleImage(System.Drawing.Image image, int maxImageHeight)
    //{
    //    var ratio = (double)maxImageHeight / image.Height;
    //    var newWidth = (int)(image.Width * ratio);
    //    var newHeight = (int)(image.Height * ratio);
    //    var newImage = new Bitmap(newWidth, newHeight);
    //    using (var g = Graphics.FromImage(newImage))
    //    {
    //        g.DrawImage(image, 0, 0, newWidth, newHeight);
    //    }
    //    return newImage;
    //}

    public static System.Drawing.Image ScaleImage(System.Drawing.Image image)
    {
        //var ratio = (double)maxImageHeight / image.Height;
        var newWidth = (int)(400);
        var newHeight = (int)(400);
        var newImage = new Bitmap(newWidth, newHeight);
        using (var g = Graphics.FromImage(newImage))
        {
            g.DrawImage(image, 0, 0, newWidth, newHeight);
        }
        return newImage;
    }
    public static System.Drawing.Image ScaleImage2(System.Drawing.Image image)
    {
        //var ratio = (double)maxImageHeight / image.Height;
        var newWidth = (int)(100);
        var newHeight = (int)(100);
        var newImage = new Bitmap(newWidth, newHeight);
        using (var g = Graphics.FromImage(newImage))
        {
            g.DrawImage(image, 0, 0, newWidth, newHeight);
        }
        return newImage;
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/admin/EditProduct.aspx");
    }

   
    protected void gvBackImage_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvBackImage.EditIndex = -1;
        string productid = Request.QueryString["product"];
        if (productid != null)
        {
            BindBackImg(productid);
        }
    }
   
}