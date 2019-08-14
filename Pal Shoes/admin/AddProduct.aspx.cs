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

public partial class admin_AddProduct : System.Web.UI.Page
{
    Catergoy local_category = new Catergoy();
    SubCategory local_sucategory = new SubCategory();
    Products local_product = new Products();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Convert.ToString(Session["userid"])) && Convert.ToString(Session["logintype"]) =="Admin")
        {
            if (!IsPostBack)
            {
                BindCategory();
                string productid = Request.QueryString["id"];
                if (productid != null)
                {
                    BindProductByProductid(productid);
                }
              //  BindFlavour();

                if (ViewState["ID"] != null)
                {
                    btnAddProduct.Text = "Update";
                    txtProductCode.Enabled = true;
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
    private void BindSubCategory(string category)
    {
        try
        {
            DataTable dt = new DataTable();
            dt = local_sucategory.getSubCategoryInCategoery(category);
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

    //private void BindFlavour()
    //{
    //    try
    //    {
    //        DataTable dt = new DataTable();
    //        dt = local_product.getFlavourname();
    //        if (dt.Rows.Count > 0)
    //        {
    //            ddFlavour.DataSource = dt;
    //            ddFlavour.DataTextField = "FlavourName";
    //            ddFlavour.DataValueField = "FlavourID";
    //            ddFlavour.DataBind();
    //            ddFlavour.Items.Insert(0, "Select Flavour");
    //        }
    //    }
    //    catch
    //    { }
    //}


    protected void btnAddProduct_Click(object sender, EventArgs e)
    {
        if (txtProductName.Text.Length > 0)
        {
            if (string.IsNullOrEmpty(Convert.ToString(ViewState["ID"])))
            {
                try
                {
                    if (UploadImage.HasFile)
                    {
                        string imgPath = "~/Upload/images/";
                        string thumbPath = "~/Upload/thumbnails/";
                        string smallPath = "~/Upload/small/";

                        string ImageFile = Path.GetFileName(Server.MapPath(UploadImage.PostedFile.FileName));
                        ImageFile = System.DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + ImageFile;
                        thumbPath = thumbPath + ImageFile;
                        System.Drawing.Bitmap bmpUploadedImage = new System.Drawing.Bitmap(UploadImage.PostedFile.InputStream);
                        //System.Drawing.Image objImage = ScaleImage(bmpUploadedImage, 170);
                        System.Drawing.Image objImage = ScaleImage(bmpUploadedImage);
                        objImage.Save(Server.MapPath(thumbPath), ImageFormat.Png);

                        smallPath = smallPath + ImageFile;
                        System.Drawing.Image objImage2 = ScaleImage2(bmpUploadedImage);
                        objImage2.Save(Server.MapPath(smallPath), ImageFormat.Png);

                        imgPath = imgPath + ImageFile;
                        UploadImage.SaveAs(MapPath(imgPath));

                        string categoryid = Convert.ToString(ddCategory.SelectedValue);
                        Int32 subcategoryid = 0;
                        if (ddSubCategory.SelectedIndex > 0)
                        {
                            subcategoryid = Convert.ToInt32(ddSubCategory.SelectedValue);
                        }

                        string producturl = txtProducturl.Text;
                        string producttitle = txtProductTitle.Text;
                        string productkeywords = txtProductKeywords.Text;
                        string productshortDes = txtProductShortDes.Text;

                        string name = txtProductName.Text.Trim();

                        string variantnames = txtVariant.Text;
                        string variantquantity = txtVariantquantity.Text;

                        decimal price = Convert.ToDecimal(txtPrice.Text.Trim());

                        bool active;
                        if (CheckBox1.Checked == true)
                        { active = true; }
                        else
                        { active = false; }
                        txtProductCode.Text = "";
                        if (txtProductName.Text != "")//SKU code
                        {
                            txtProductCode.Text = txtProductName.Text + " " + txtPrice.Text;
                        }
                        Int32 discount = 0;
                        if (txtDiscount.Text.Length > 0)
                        {
                            discount = Convert.ToInt32(txtDiscount.Text.Trim());
                        }
                        // Int32 quantity = Convert.ToInt32(txtQuantity.Text.Trim());
                        Int32 quantity=0;
                        string date = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        string brand = txtBrand.Text.Trim();
                        string shippingcharges = "0"; //Shipping Charges
                        if (txtshippingcharges.Text.Length > 0)
                        {
                            shippingcharges = txtshippingcharges.Text.Trim();
                        }
                        string weightlbl = "0";  //Weight
                        if (txtProductCapacity.Text != "")
                        { weightlbl = txtProductCapacity.Text.Trim(); }

                        string flavour = "";  //Flavour
                        int selectproduct = 1;

                        if (rdOfferProduct.Checked == true)
                        {
                            selectproduct = 1;
                        }
                        else if (rdNewproduct.Checked == true)
                        {
                            selectproduct = 2;
                        }
                        else if (rdBestSeller.Checked == true)
                        {
                            selectproduct = 3;
                        }
                        else if (rdFeature.Checked == true)
                        {
                            selectproduct = 4;
                        }

                        string description = CKEditor1.Text;

                        int success = 0;
                        success = local_product.AddNewProduct(producturl, producttitle, productkeywords, productshortDes, name, variantnames, variantquantity, price, ImageFile, ImageFile, active, txtProductCode.Text, discount, date, subcategoryid, quantity, brand, shippingcharges, weightlbl, flavour, categoryid, selectproduct, description);
                        if (success != 0)
                        {
                            clear();
                            lblMessage.ForeColor = Color.Green;
                            lblMessage.Text = "Product Added Successfully";
                            ViewState["ID"] = null;
                        }
                        else
                        {
                            lblMessage.ForeColor = Color.Red;
                            lblMessage.Text = "Product Addition Failed";
                        }

                    }
                    else
                    {
                        lblMessage.ForeColor = Color.Red;
                        lblMessage.Text = "Select Product Photo";
                    }
                }
                catch (Exception ex)
                {
                    lblMessage.Text = ex.Message;
                }
            }
            else //Update Product
            {
                string ImageFile;
                if (UploadImage.HasFile)
                {
                    string imgPath = "~/Upload/images/";
                    string thumbPath = "~/Upload/thumbnails/";
                    string smsllPath = "~/Upload/small/";

                    ImageFile = Path.GetFileName(Server.MapPath(UploadImage.PostedFile.FileName));
                    ImageFile = System.DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + ImageFile;
                    thumbPath = thumbPath + ImageFile;
                    System.Drawing.Bitmap bmpUploadedImage = new System.Drawing.Bitmap(UploadImage.PostedFile.InputStream);
                    //System.Drawing.Image objImage = ScaleImage(bmpUploadedImage, 170);
                    System.Drawing.Image objImage = ScaleImage(bmpUploadedImage);
                    objImage.Save(Server.MapPath(thumbPath), ImageFormat.Png);

                    smsllPath = smsllPath + ImageFile;
                    System.Drawing.Image objImage2 = ScaleImage2(bmpUploadedImage);
                    objImage2.Save(Server.MapPath(smsllPath), ImageFormat.Png);

                    imgPath = imgPath + ImageFile;
                    UploadImage.SaveAs(MapPath(imgPath));
                }
                else
                {
                    ImageFile = lblImage.Text;
                }

                try
                {
                    string productid = Request.QueryString["id"];
                    
                    string producturl = txtProducturl.Text.Trim();
                    string producttitle = txtProductTitle.Text.Trim();
                    string productkeywords = txtProductKeywords.Text.Trim();
                    string productshortDes = txtProductShortDes.Text.Trim();

                    string name = txtProductName.Text.Trim();
                    string description = CKEditor1.Text;
                    decimal price = Convert.ToDecimal(txtPrice.Text.Trim());

                    string variantnames = txtVariant.Text.Trim();
                    string variantquantity = txtVariantquantity.Text.Trim();

                    bool active;
                    if (CheckBox1.Checked == true)
                    { active = true; }
                    else
                    { active = false; }
                    string productcode = "";
                    if (txtProductCode.Text != "")//SKU code
                    {
                        productcode = txtProductCode.Text.Trim();
                    }
                    Int32 discount = 0;
                    if (txtDiscount.Text.Length > 0)
                    {
                        discount = Convert.ToInt32(txtDiscount.Text.Trim());
                    }
                    //Int32 quantity = Convert.ToInt32(txtQuantity.Text.Trim());
                    Int32 quantity = 0;
                    string date = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    string categoryid = Convert.ToString(ddCategory.SelectedValue);
                    Int32 subcategoryid = 0;
                    if (ddSubCategory.SelectedIndex > 0)
                    {
                        subcategoryid = Convert.ToInt32(ddSubCategory.SelectedValue);
                    }
                    string brand = txtBrand.Text.Trim();
                    string shippingcharges = txtshippingcharges.Text.Trim();
                    string weightlbl = txtProductCapacity.Text.Trim();
                    //string flavour = ddFlavour.SelectedValue;
                    string flavour = "";
                    int selectproduct = 1;

                    if (rdOfferProduct.Checked == true)
                    {
                        selectproduct = 1;
                    }
                    else if (rdNewproduct.Checked == true)
                    {
                        selectproduct = 2;
                    }
                    else if (rdBestSeller.Checked == true)
                    {
                        selectproduct = 3;
                    }
                    else if (rdFeature.Checked == true)
                    {
                        selectproduct = 4;
                    }

                    int success = 0;
                    success = local_product.UpdateNewProduct(productid, producturl, producttitle, productkeywords, productshortDes, name, description, price, variantnames, variantquantity, ImageFile, ImageFile, active, productcode, discount, date, quantity, brand, shippingcharges, weightlbl, flavour, subcategoryid, categoryid, selectproduct);
                    if (success != 0)
                    {
                        clear();
                        ViewState["ID"] = null;
                        btnAddProduct.Text = "Add Product";
                        lblMessage.ForeColor = Color.Green;
                        lblMessage.Text = "Product Updated Successfully";
                        Response.Redirect("~/admin/EditProduct.aspx", false);
                    }
                    else
                    {
                        lblMessage.ForeColor = Color.Red;
                        lblMessage.Text = "Product Update Failed";
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
            lblMessage.Text = "Enter Product Name";
        }
    }

    private void BindProductByProductid(string productid)
    {
        try
        {
            DataTable dt = new DataTable();
            dt = local_product.getProductByProductId(productid);
            if (dt.Rows.Count > 0)
            {
                ddCategory.SelectedValue = Convert.ToString(dt.Rows[0]["Category"]);
                ddSubCategory.SelectedValue = Convert.ToString(dt.Rows[0]["SubCategory"]);
                if (ddCategory.SelectedIndex > 0)
                {
                    string category = Convert.ToString(ddCategory.SelectedValue);
                    BindSubCategory(category);
                }
                txtProducturl.Text = Convert.ToString(dt.Rows[0]["Producturl"]);
                txtProductTitle.Text = Convert.ToString(dt.Rows[0]["Producttitle"]);
                txtProductKeywords.Text = Convert.ToString(dt.Rows[0]["Productkeywords"]);
                txtProductShortDes.Text = Convert.ToString(dt.Rows[0]["ProductshortDes"]);

                txtProductName.Text = Convert.ToString(dt.Rows[0]["Name"]);
                CKEditor1.Text = Convert.ToString(dt.Rows[0]["Description"]);
                txtPrice.Text = Convert.ToString(dt.Rows[0]["Price"]);
                txtProductCapacity.Text = Convert.ToString(dt.Rows[0]["Capacity"]);
                
                txtVariant.Text = Convert.ToString(dt.Rows[0]["Variantname"]);
                txtVariantquantity.Text = Convert.ToString(dt.Rows[0]["Quantity"]);

                lblImage.Text = Convert.ToString(dt.Rows[0]["Image"]);
                bool active = Convert.ToBoolean(dt.Rows[0]["IsActive"]);
                if (active == true)
                {
                    CheckBox1.Checked = true;
                }
                else
                { CheckBox1.Checked = false; }
                txtProductCode.Text = Convert.ToString(dt.Rows[0]["SKUCode"]);
                txtDiscount.Text = Convert.ToString(dt.Rows[0]["Discount"]);
                txtBrand.Text = Convert.ToString(dt.Rows[0]["Brand"]);
                txtshippingcharges.Text = Convert.ToString(dt.Rows[0]["ShippingCharges"]);
                txtProductCapacity.Text = Convert.ToString(dt.Rows[0]["Capacity"]);
                ViewState["ID"] = Convert.ToString(dt.Rows[0]["ProductId"]);

                int selectproduct = Convert.ToInt32(dt.Rows[0]["Ptype"]);
                rdNewproduct.Checked = false;
                
                if (selectproduct == 1)
                {
                    rdOfferProduct.Checked = true;
                }
                else if (selectproduct == 2)
                {
                    rdNewproduct.Checked = true;
                }
                else if (selectproduct == 3)
                {
                    rdBestSeller.Checked = true;
                }
                else if (selectproduct == 4)
                {
                    rdFeature.Checked = true;
                }

                //  txtQuantity.Text = Convert.ToString(dt.Rows[0]["Quantity"]);
                //  ddFlavour.SelectedValue = Convert.ToString(dt.Rows[0]["Producttype"]);

            }
        }
        catch(Exception ex)
        { throw new Exception(ex.Message); }
    }

    protected void ddCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddCategory.SelectedIndex > 0)
        {
            string category = Convert.ToString(ddCategory.SelectedValue);
            BindSubCategory(category);
        }
        else
        {
            ddSubCategory.Items.Clear();
        }
    }

    public static System.Drawing.Image ScaleImage1(System.Drawing.Image image, int maxImageHeight)
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

    private void clear()
    {
        txtProducturl.Text = "";
        txtProductTitle.Text = "";
        txtProductKeywords.Text = "";
        txtProductShortDes.Text = "";
        txtProductName.Text = "";
        txtBrand.Text = "";
        txtPrice.Text = "";
        txtDiscount.Text = "";
        txtVariant.Text = "";
        txtVariantquantity.Text = "";
        txtProductCapacity.Text = "";
        txtshippingcharges.Text = "";
        txtProductCode.Text = "";
        CKEditor1.Text = "";
       
        //if (ddSubCategory.SelectedIndex != -1)
        //{
        //    ddSubCategory.SelectedIndex = 0;
        //}
      //  txtQuantity.Text = "1";
        //txtModelNo.Text = "";
      //  ddFlavour.SelectedIndex = 0;
        
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/admin/EditProduct.aspx");
    }
    protected void btnNewFlavour_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/admin/AddnewFlavour.aspx");
    }
}