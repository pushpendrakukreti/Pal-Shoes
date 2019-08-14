using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using SqlAccess;
using System.Drawing;

public partial class admin_ProductReview : System.Web.UI.Page
{
    Products local_product = new Products();
    WalletCls local_wallet = new WalletCls();
    chkLogin local_login = new chkLogin();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Convert.ToString(Session["userid"])) && Convert.ToString(Session["logintype"]) == "Admin")
        {
            if (!IsPostBack)
            {
                BindRegistrationDetails();
                txtDate1.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            }
        }
        else
        {
            Response.Redirect("Login.aspx");
        }
    }

    private void BindRegistrationDetails()
    {
        DataTable dt = new DataTable();
        try
        {
            dt = local_wallet.getProductReviewActive();
            if (dt.Rows.Count > 0)
            {
                gvRegistration.DataSource = dt;
                gvRegistration.DataBind();
            }
            else
            {
                lblMessage.Text = "<br/><br/><br/>There is no any records.";
                lblMessage.Font.Size = 20;
            }
        }
        catch
        { }
    }

    protected void gvRegistration_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ImageButton img = (ImageButton)e.Row.FindControl("BtnActive");
            Label active = (Label)e.Row.FindControl("lblActive");

            if (active.Text == "Active")
            {
                img.ImageUrl = "~/img/ActiveU.png";
                active.ForeColor = Color.Green;
            }
            else
            {
                img.ImageUrl = "~/img/InactiveU.png";
                active.ForeColor = Color.Red;
            }
        }
    }
    protected void gvRegistration_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //if (e.CommandName == "Delete")
        //{
        //    string userid = Convert.ToString(e.CommandArgument);
        //    local_login.DeletUser(userid);
        //    BindRegistrationDetails();
        //}
    }

    protected void BtnActive_Click(object sender, EventArgs e)
    {
        try
        {
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            Label id = (Label)gvRow.FindControl("lblId");
            Label active = (Label)gvRow.FindControl("lblActive");
            
            bool act= true;
            if (active.Text == "Active")
            {
                //status = "Inactive";
                act=false;
            }
            else
            {
                //status = "Active";
                act=true;
            }
            local_wallet.UpdateProductReview(act, id.Text);
            BindRegistrationDetails();
        }
        catch
        { }
    }

    protected void gvRegistration_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvRegistration.PageIndex = e.NewPageIndex;
        BindRegistrationDetails();
    }

    protected void btnGetDetails_Click(object sender, EventArgs e)
    {
        lblMessage.Text = "";
        try
        {
            DataTable dt = new DataTable();

            if (txtDate1.Text != "")
            {
                string date1 = txtDate1.Text;
                date1 = Convert.ToDateTime(date1).ToString("yyyy-MM-dd");
                string date2 = "";
                if (txtDate2.Text != "")
                {
                    date2 = txtDate2.Text;
                    date2 = Convert.ToDateTime(date2).ToString("yyyy-MM-dd");
                }
                dt = local_wallet.getRegistrationUserDetailByDate(date1, date2);
                if (dt.Rows.Count > 0)
                {
                    gvRegistration.DataSource = dt;
                    gvRegistration.DataBind();
                }
                else
                {
                    gvRegistration.DataSource = null;
                    gvRegistration.DataBind();
                }
            }
            else
            {
                BindRegistrationDetails();
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
    }

}