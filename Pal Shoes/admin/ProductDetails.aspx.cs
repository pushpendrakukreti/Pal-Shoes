using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SqlAccess;
using System.Drawing;

public partial class admin_ProductDetails : System.Web.UI.Page
{
    Products local_product = new Products();
    Catergoy local_category = new Catergoy();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Convert.ToString(Session["userid"])) && Convert.ToString(Session["logintype"]) == "Admin")
        {
            if (!IsPostBack)
            {
                string productid = Request.QueryString["product"];
                if (productid != null)
                {
                    BindProductDetails(productid);
                }
            }
        }
        else
        {
            Response.Redirect("Login.aspx");
        }
    }

    private void BindProductDetails(string productid)
    {
        try
        {
            DataTable dt = new DataTable();
            dt = local_product.getProdtuctsByProductId(productid);
            if (dt.Rows.Count > 0)
            {
                dtlProductDetails.DataSource = dt;
                dtlProductDetails.DataBind();
            }
        }
        catch
        { }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect("EditProduct.aspx");
    }
}