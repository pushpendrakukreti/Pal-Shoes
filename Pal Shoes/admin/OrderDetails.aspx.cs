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

public partial class admin_OrderDetails : System.Web.UI.Page
{
    OrderDetails local_order = new OrderDetails();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Convert.ToString(Session["userid"])) && Convert.ToString(Session["logintype"]) == "Admin")
        {
            if (!IsPostBack)
            {
                BindOrderDetails();

                string date1 = System.DateTime.Now.ToString("dd/MM/yyyy");
                txtDate1.Text = date1;
            }
        }
        else
        {
            Response.Redirect("Login.aspx");
        }
    }


    private void BindOrderDetails()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = local_order.getOrderDetails();
            if (dt.Rows.Count > 0)
            {
                gvOrderDetails.DataSource = dt;
                gvOrderDetails.DataBind();
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

    protected void gvOrderDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvOrderDetails.PageIndex = e.NewPageIndex;
        BindOrderDetails();
        lblMessage.Text = "";
    }
    protected void gvOrderDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Details")
        {
            string orderid = Convert.ToString(e.CommandArgument);
            Response.Redirect("Order.aspx?transaction=" + orderid, false);
        }
        if (e.CommandName == "UpdateOrder")
        {
            string orderid = Convert.ToString(e.CommandArgument);
            GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
            Label status = row.FindControl("lblOrderStatus") as Label;
            if (status.Text == "Order" || status.Text == "Product in Shipping")
            {
                Session["OrderID"] = orderid;
                Response.Redirect("UpdateOrderStatus.aspx", false);
            }
            else
            {
                lblMessage.ForeColor = Color.Red;
                lblMessage.Text = "It can't be updated!";
            }
        }
    }
    protected void gvOrderDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            Int32 orderid = Convert.ToInt32(gvOrderDetails.DataKeys[e.RowIndex].Value);
            int success = 0;
            //success = local_order.DeleteOrderDetails(orderid);
            if (success != 0)
            {
                BindOrderDetails();
                lblMessage.ForeColor = Color.Green;
                lblMessage.Text = "Product Order Deleted!";
            }
            else
            {
                lblMessage.ForeColor = Color.Red;
                lblMessage.Text = "Order Delete Failed!";
            }
        }
        catch (Exception ex)
        {
            lblMessage.ForeColor = Color.Red;
            lblMessage.Text = ex.Message;
        }
    }

    protected void gvOrderDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label status = (Label)e.Row.FindControl("lblOrderStatus");

            if (status.Text == "Order")
            {
                status.ForeColor = Color.Green;
            }
            else if (status.Text == "Order Cancel")
            {
                status.ForeColor = Color.Red;
            }
            else if (status.Text == "Order Completed")
            {
                status.ForeColor = Color.DimGray;
            }
            else if (status.Text == "Product in Shipping")
            {
                status.ForeColor = Color.Teal;
            }
            else
            {
                status.ForeColor = Color.OrangeRed;
                status.Text = "Incomplete Order";
            }
        }
    }

    protected void btnGetDetails_Click(object sender, EventArgs e)
    {
        lblMessage.Text = "";
        try
        {
            DataTable dt = new DataTable();

            if (txtOrderId.Text != "")
            {
                string orderdetID = txtOrderId.Text;
                dt = local_order.getOrderDetailsByOrderId(orderdetID);
                if (dt.Rows.Count > 0)
                {
                    gvOrderDetails.DataSource = dt;
                    gvOrderDetails.DataBind();
                }
                else
                {
                    gvOrderDetails.DataSource = null;
                    gvOrderDetails.DataBind();
                }
            }
            else if (txtDate1.Text != "")
            {
                string date1 = txtDate1.Text;
                date1 = Convert.ToDateTime(date1).ToString("yyyy-MM-dd");
                string date2 = "";
                if (txtDate2.Text != "")
                {
                    date2 = txtDate2.Text;
                    date2 = Convert.ToDateTime(date2).ToString("yyyy-MM-dd");
                }
                dt = local_order.getOrderDetailsByDate(date1, date2);
                if (dt.Rows.Count > 0)
                {
                    gvOrderDetails.DataSource = dt;
                    gvOrderDetails.DataBind();
                }
                else
                {
                    gvOrderDetails.DataSource = null;
                    gvOrderDetails.DataBind();
                }
            }
            else
            {
                BindOrderDetails();
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
    }


    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        DataTable dt = new DataTable("Order");
        dt.Columns.Add("S.N.", typeof(Int32));
        dt.Columns.Add("User ID", typeof(string));
        dt.Columns.Add("Order-NO", typeof(string));
        dt.Columns.Add("Customer Name", typeof(string));
        dt.Columns.Add("Contact No", typeof(string));
        dt.Columns.Add("Order Date", typeof(string));
        dt.Columns.Add("Payment", typeof(string));
        dt.Columns.Add("Status", typeof(string));

        foreach (GridViewRow row in gvOrderDetails.Rows)
        {
            Int32 sn = row.DataItemIndex + 1;
            string useridetify = row.Cells[1].Text;
            string orderno = row.Cells[2].Text;
            string customer = row.Cells[3].Text;
            string contact = row.Cells[4].Text;
            string date = row.Cells[5].Text;
            string payment = row.Cells[6].Text;
            Label status1 = (Label)row.FindControl("lblOrderStatus");
            string status = status1.Text;

            dt.Rows.Add(sn, useridetify, orderno, customer, contact, date, payment, status);
        }

        if (dt.Rows.Count > 0)
        {
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, "Order");

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                string FileName = "Order_detail_" + DateTime.Now.ToShortDateString() + ".xlsx";
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
    
}