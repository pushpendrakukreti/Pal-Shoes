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

public partial class admin_Addnewevent : System.Web.UI.Page
{
    Products local_product = new Products();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Convert.ToString(Session["userid"])) && Convert.ToString(Session["logintype"]) == "Admin")
        {
            if (!IsPostBack)
            {
                string eventid = Request.QueryString["eventid"];
                if (eventid != null)
                {
                    BindEventByeventid(eventid);
                }

                if (ViewState["ID"] != null)
                {
                    btnAddEvent.Text = "Update";
                    // txtBlogtitle.Enabled = true;
                }
            }
        }
        else
        {
            Response.Redirect("Login.aspx");
        }
    }

    protected void btnAddEvent_Click(object sender, EventArgs e)
    {
        string title = txtEventtitle.Text;
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
                    //string imgPath = "~/Upload/images/";
                    //string thumbPath = "~/Upload/thumbnails/";

                    string imgPath = "~/Upload/event/";
                    
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


                result = local_product.AddNewEvent(title, description, date, blogimage);

                if (result != 0)
                {
                    lblMessage.Text = "New Event Added Successfully";
                    lblMessage.ForeColor = Color.Green;
                    BindBlog();
                    txtEventtitle.Text = "";
                    CKEditor1.Text = "";
                }
                else
                {
                    lblMessage.Text = "Event Not Added?";
                    lblMessage.ForeColor = Color.Red;
                }
            }
            else //edit
            {
                string blogimage = "";
                if (UploadImage.HasFile)
                {
                    string imgPath = "~/Upload/event/";
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

                string eventid = Request.QueryString["Eventid"];
                result = local_product.UpdateEvent(title, description, eventid, blogimage);

                if (result != 0)
                {
                    lblMessage.Text = "Event Updated Successfully";
                    lblMessage.ForeColor = Color.Green;
                    BindBlog();
                    txtEventtitle.Text = "";
                    CKEditor1.Text = "";
                    Session["Eventid"] = null;
                    Session["ID"] = null;
                    btnAddEvent.Text = "Add Event";
                    lblImage.Text = "";
                }
                else
                {
                    lblMessage.Text = "Event Not Updated?";
                    lblMessage.ForeColor = Color.Red;
                }
            }
        }
        catch (Exception ex)
        { throw new Exception(ex.Message); }
    }

    private void BindBlog()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = local_product.getAllEvents();
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
            string eventid = Convert.ToString(e.CommandArgument);
            Session["ID"] = eventid;
            Response.Redirect("Addnewevent.aspx");
        }
        if (e.CommandName == "Delete")
        {
            string blogid = Convert.ToString(e.CommandArgument);
            int result = 0;
            result = local_product.DeleteEvent(Convert.ToInt32(blogid));
            if (result != 0)
            {
                lblMessage.Text = "Event Deleted Successfully";
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

    private void BindEventByeventid(string eventid)
    {
        try
        {
            DataTable dt = new DataTable();
            dt = local_product.getEventByid(eventid);
            if (dt.Rows.Count > 0)
            {
                txtEventtitle.Text = Convert.ToString(dt.Rows[0]["Eventtitle"]);
                CKEditor1.Text = Convert.ToString(dt.Rows[0]["Eventbody"]);
                lblImage.Text = Convert.ToString(dt.Rows[0]["imagefile"]);

                ViewState["ID"] = Convert.ToString(dt.Rows[0]["Eventid"]);
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
        Response.Redirect("~/admin/Addnewevent.aspx");
    }
}