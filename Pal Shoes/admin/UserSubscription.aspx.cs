using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SqlAccess;
using System.Drawing;
using System.IO;
using System.Xml;
using ClosedXML.Excel;

public partial class admin_UserSubscription : System.Web.UI.Page
{
    Products local_product = new Products();
    private int PageSize = 50;
    protected void Page_Load(object sender, EventArgs e)
    {
        BindSubscriptionList();
    }

    private void BindSubscriptionList()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = local_product.getAllSubscriber();
            if (dt.Rows.Count > 0)
            {
                gvSubscriber.DataSource = dt;
                gvSubscriber.DataBind();
            }

            else
            {
                gvSubscriber.DataSource = null;
                gvSubscriber.DataBind();
            }
        }
        catch
        { }
    }

    protected void DeleteRecord(object sender, EventArgs e)
    {
        try
        {
            int id = int.Parse((sender as Button).CommandArgument);
            int success = 0;
            success = local_product.DeleteSubscription(id);
            if (success != 0)
            {
                BindSubscriptionList();
                lblmessage.ForeColor = Color.Green;
                lblmessage.Text = "Subscription Deleted Successfully!";
            }
            else
            {
                lblmessage.ForeColor = Color.Red;
                lblmessage.Text = "Subscription Delete Failed?";
            }
        }
        catch (Exception ex)
        {
            lblmessage.ForeColor = Color.Red;
            lblmessage.Text = ex.Message;
        }
    }

    protected void gvSubscriber_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            Response.Redirect("~/admin/AddProduct.aspx?id=" + e.CommandArgument);
        }
    }

    protected void gvSubscriber_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvSubscriber.PageIndex = e.NewPageIndex;
        //if (ddCategory.SelectedIndex > 0)
        //{
        //    string categoryid = Convert.ToString(ddCategory.SelectedValue);
        //    string subcategory = "";
        //    if (ddSubCategory.SelectedIndex > 0)
        //    {
        //        subcategory = Convert.ToString(ddCategory.SelectedValue);
        //    }
        //    BindgvProductByCategory(categoryid, subcategory);
        //}
        //else
        //{
        //    BindgvProduct();
        //}
    }
    protected void gvSubscriber_RowDeleting1(object sender, GridViewDeleteEventArgs e)
    {
    }

    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            DataTable dt = new DataTable("Subscriber");
            dt = local_product.getAllSubscriber();
            if (dt.Rows.Count > 0)
            {
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt, "Subscriber");

                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    string FileName = "Subscriber_detail_" + DateTime.Now.ToShortDateString() + ".xlsx";
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename=" + FileName);
                    using (MemoryStream MyMemoryStream = new MemoryStream())
                    {
                        wb.SaveAs(MyMemoryStream);
                        MyMemoryStream.WriteTo(Response.OutputStream);
                        Response.Flush();
                        Response.End();
                    }
                }
            }
        }
        catch (Exception)
        { }
    }

}