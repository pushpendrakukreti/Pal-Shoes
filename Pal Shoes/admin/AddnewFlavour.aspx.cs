using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SqlAccess;
using System.Drawing;

public partial class admin_AddnewFlavour : System.Web.UI.Page
{
    Products local_product = new Products();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Convert.ToString(Session["userid"])) && Convert.ToString(Session["logintype"]) == "Admin")
        {

        }
        else
        {
            Response.Redirect("Login.aspx");
        }
    }
    protected void btnAddFlavour_Click(object sender, EventArgs e)
    {
        try
        {
            string flavour = txtFlavour.Text;
            int result = 0;
            if (local_product.IsFlavourExists(flavour, "N", "") == "F")
            {
                result = local_product.AddNewFlavour(flavour);
                if (result != 0)
                {
                    lblMessage.ForeColor = Color.Green;
                    lblMessage.Text = "New Flavour Name Added!";
                    txtFlavour.Text = "";
                }
            }
            else
            {
                lblMessage.ForeColor = Color.Red;
                lblMessage.Text = "Flavour Name Already Exists?";
            }
        }
        catch
        { }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("AddProduct.aspx");
    }
}