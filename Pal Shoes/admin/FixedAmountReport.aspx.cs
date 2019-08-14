using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using SqlAccess;
using System.IO;
using System.Xml;
using ClosedXML.Excel;

public partial class admin_FixedAmountReport : System.Web.UI.Page
{
    WalletCls ob_wallet = new WalletCls();
    Products local_product = new Products();
    chkLogin local_login = new chkLogin();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Convert.ToString(Session["userid"])) && Convert.ToString(Session["logintype"]) == "Admin")
        {
            if (!IsPostBack)
            {
                txtDate1.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                BindFixPercentage();
            }
        }
        else
        {
            Response.Redirect("Login.aspx");
        }
    }

    private void BindFixPercentage()
    {
        DataTable dt = new DataTable();
        dt = ob_wallet.getfixAmountReports();
        if (dt.Rows.Count > 0)
        {
            gvWalletDetails.DataSource = dt;
            gvWalletDetails.DataBind();
        }
        else
        {
            lblMessage.Text = "<br/><br/><br/>There is no any records.";
            lblMessage.Font.Size = 20;
        }
    }
    //


    protected void btnGetDetails_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = new DataTable();
            if (txtSearch.Text != "")
            {
                dt = ob_wallet.getfixAmountReportsearch(txtSearch.Text.Trim());
                if (dt.Rows.Count > 0)
                {
                    gvWalletDetails.DataSource = dt;
                    gvWalletDetails.DataBind();
                }
                else
                {
                    gvWalletDetails.DataSource = null;
                    gvWalletDetails.DataBind();
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
                dt = ob_wallet.getfixAmountReportseacheByDate(date1, date2);
                if (dt.Rows.Count > 0)
                {
                    gvWalletDetails.DataSource = dt;
                    gvWalletDetails.DataBind();
                }
                else
                {
                    gvWalletDetails.DataSource = null;
                    gvWalletDetails.DataBind();
                }
            }
            else
            {
                BindFixPercentage();
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
    }

    protected void gvWalletDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvWalletDetails.PageIndex = e.NewPageIndex;
        BindFixPercentage();
    }


    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            DataTable dt = new DataTable("FixedAmount");
            dt.Clear();
            dt.Columns.Add("S.N.", typeof(Int32));
            dt.Columns.Add("User ID", typeof(string));
            dt.Columns.Add("User Name", typeof(string));
            dt.Columns.Add("Fixed Amount", typeof(string));
            dt.Columns.Add("No Of Days", typeof(string));
            dt.Columns.Add("Calculate Amount", typeof(string));
            dt.Columns.Add("Date", typeof(string));
            dt.Columns.Add("Total Balance", typeof(decimal));

            foreach (GridViewRow row in gvWalletDetails.Rows)
            {
                Int32 sn = row.DataItemIndex + 1;
                string useridetify = row.Cells[1].Text;
                string username = row.Cells[2].Text;
                string fixamount = row.Cells[3].Text;
                string nofday = row.Cells[4].Text;
                string calcamount = row.Cells[5].Text;
                string date = row.Cells[6].Text;
                decimal totalbalance = Convert.ToDecimal(row.Cells[7].Text);

                dt.Rows.Add(sn, useridetify, username, fixamount, nofday, calcamount, date, totalbalance);
            }

            if (dt.Rows.Count > 0)
            {
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt, "FixedAmount");

                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    string FileName = "FixedAmount_Report_" + DateTime.Now.ToShortDateString() + ".xlsx";
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
        catch (Exception ex)
        {
            throw new Exception(ex.ToString());
        }
        finally
        { }
    }
}