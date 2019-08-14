using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SqlAccess;
using System.Drawing;

public partial class admin_CreateCategory : System.Web.UI.Page
{
    Catergoy local_category = new Catergoy();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Convert.ToString(Session["userid"])) && Convert.ToString(Session["logintype"]) == "Admin")
        {
            if (!IsPostBack)
            {
                BindgvCategory();
            }
        }
        else
        {
            Response.Redirect("Login.aspx");
        }
    }

    private void BindgvCategory()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = local_category.getAllCategory();
            if (dt.Rows.Count > 0)
            {
                gvcat.DataSource = dt;
                gvcat.DataBind();
            }
        }
        catch
        { }
    }

    protected void gvcat_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvcat.EditIndex = -1;
        BindgvCategory();
    }
    protected void gvcat_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvcat.EditIndex = e.NewEditIndex;
        BindgvCategory();
    }
    
    protected void DeleteRecord(object sender, EventArgs e)
    {
        try
        {
            int id = int.Parse((sender as LinkButton).CommandArgument);
            int success = 0;
            success = local_category.DeleteCategory(id);
            if (success != 0)
            {
                BindgvCategory();
                lblmainMsg.ForeColor = Color.Green;
                lblmainMsg.Text = "Category Deleted";
            }
            else
            {
                lblmainMsg.ForeColor = Color.Red;
                lblmainMsg.Text = "Category Delete Failed";
            }
        }
        catch (Exception ex)
        {
            lblmainMsg.ForeColor = Color.Red;
            lblmainMsg.Text = ex.Message;
        }
    }

    protected void gvcat_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
    }
    protected void gvcat_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        Int32 id = Convert.ToInt32(gvcat.DataKeys[e.RowIndex].Value);
        TextBox category = (TextBox)gvcat.Rows[e.RowIndex].FindControl("gvtxtcat");
        try
        {
            local_category.UpdateCategory(id, category.Text);
            gvcat.EditIndex = -1;
            BindgvCategory();
            lblmainMsg.ForeColor = Color.Green;
            lblmainMsg.Text = "Category Updated";
        }
        catch (Exception ex)
        {
            lblmainMsg.Text = ex.Message;
        }
  }
    

    protected void btnCreateCategory_Click(object sender, EventArgs e)
    {
        if (txtcategory.Text.Length > 0)
        {
            try
            {
                if (local_category.IsCategoryExists(txtcategory.Text.Trim(), "N", "") == "F")
                {
                    local_category.CreateNewCategory(txtcategory.Text.Trim());
                    txtcategory.Text = "";
                    lblmainMsg.ForeColor = Color.Green;
                    lblmainMsg.Text = "Category Created";
                    BindgvCategory();
                }
                else
                {
                    lblmainMsg.ForeColor = Color.Red;
                    lblmainMsg.Text = "Category Already Exsits";
                }
            }
            catch (Exception)
            {
                lblmainMsg.ForeColor = Color.Red;
                lblmainMsg.Text = "Category Creation Failed";
            }
        }
        else
        {
            lblmainMsg.ForeColor = Color.Red;
            lblmainMsg.Text = "Enter Category Name";
        }
    }
}