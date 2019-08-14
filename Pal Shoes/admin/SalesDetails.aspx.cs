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

public partial class admin_SalesDetails : System.Web.UI.Page
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
            dt = local_order.getSalesDetails();
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
            Response.Redirect("~/admin/SaleItem.aspx?transaction=" + orderid, false);
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
                dt = local_order.getSalesDetailsByOrderId(orderdetID);
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
                dt = local_order.getSalesDetailsByDate(date1, date2);
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
        DataTable dt = new DataTable("Sales");
        dt.Columns.Add("S.N.", typeof(Int32));
        dt.Columns.Add("User ID", typeof(string));
        dt.Columns.Add("Order-NO", typeof(string));
        dt.Columns.Add("Customer", typeof(string));
        dt.Columns.Add("Date", typeof(string));
        dt.Columns.Add("Place", typeof(string));
        dt.Columns.Add("Iteme Name", typeof(string));
        dt.Columns.Add("Qty", typeof(int));
        dt.Columns.Add("Total Price", typeof(decimal));

        foreach (GridViewRow row in gvOrderDetails.Rows)
        {
            Int32 sn = row.DataItemIndex + 1;
            string useridetify = row.Cells[1].Text;
            string orderno = row.Cells[2].Text;
            string custome = row.Cells[3].Text;
            string date = row.Cells[4].Text;
            string place = row.Cells[5].Text;
            string itemename = row.Cells[7].Text;
            int quantity = Convert.ToInt32(row.Cells[8].Text);
            decimal totalprice = Convert.ToDecimal(row.Cells[9].Text);
            dt.Rows.Add(sn, useridetify, orderno, custome, date, place, itemename, quantity, totalprice);
        }

        if (dt.Rows.Count > 0)
        {
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, "Sales");

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                string FileName = "Sales_detail_" + DateTime.Now.ToShortDateString() + ".xlsx";
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