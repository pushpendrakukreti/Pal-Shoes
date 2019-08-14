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
using System.IO;
using System.Xml;
using ClosedXML.Excel;

public partial class admin_FundTransferReport : System.Web.UI.Page
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
                BindFundTransferReportDefault();
                txtDate1.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            }
        }
        else
        {
            Response.Redirect("Login.aspx");
        }
    }

    private void BindFundTransferReportDefault()
    {
        DataTable dt = new DataTable();
        try
        {
            dt = local_wallet.getFundTransferReport();
            if (dt.Rows.Count > 0)
            {
                gvFundTransferReport.DataSource = dt;
                gvFundTransferReport.DataBind();
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


    protected void gvFundTransferReport_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvFundTransferReport.PageIndex = e.NewPageIndex;
        BindFundTransferReportDefault();
    }

    protected void btnGetDetails_Click(object sender, EventArgs e)
    {
        lblMessage.Text = "";
        try
        {
            DataTable dt = new DataTable();

            if (txtSearch.Text != "")
            {
                dt = local_wallet.getFundTransferReportValues(txtSearch.Text);
                if (dt.Rows.Count > 0)
                {
                    gvFundTransferReport.DataSource = dt;
                    gvFundTransferReport.DataBind();
                }
                else
                {
                    gvFundTransferReport.DataSource = null;
                    gvFundTransferReport.DataBind();
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
                dt = local_wallet.getFundTransferReportDate(date1, date2);
                if (dt.Rows.Count > 0)
                {
                    gvFundTransferReport.DataSource = dt;
                    gvFundTransferReport.DataBind();
                }
                else
                {
                    gvFundTransferReport.DataSource = null;
                    gvFundTransferReport.DataBind();
                }
            }
            else
            {
                BindFundTransferReportDefault();
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
    }


    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        DataTable dt = new DataTable("FundTransfer");
        dt.Columns.Add("S.N.", typeof(Int32));
        dt.Columns.Add("User ID", typeof(string));
        dt.Columns.Add("Beneficiary Name", typeof(string));
        dt.Columns.Add("Account No", typeof(string));
        dt.Columns.Add("Bene Type", typeof(string));
        dt.Columns.Add("Mobile No", typeof(string));
        dt.Columns.Add("Transaction No", typeof(string));
        dt.Columns.Add("Amount", typeof(decimal));
        dt.Columns.Add("Date", typeof(string));

        foreach (GridViewRow row in gvFundTransferReport.Rows)
        {
            Int32 sn = row.DataItemIndex + 1;
            string useridetify = row.Cells[1].Text;
            string customer = row.Cells[2].Text;
            string accountno = row.Cells[3].Text;
            string benetype = row.Cells[4].Text;
            string contact = row.Cells[5].Text;
            string transaction = row.Cells[6].Text;
            decimal amount = Convert.ToDecimal(row.Cells[7].Text);
            string date = row.Cells[8].Text;

            dt.Rows.Add(sn, useridetify, customer, accountno, benetype, contact, transaction, amount, date);
        }

        if (dt.Rows.Count > 0)
        {
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, "FundTransfer");

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                string FileName = "FundTransfer_detail_" + DateTime.Now.ToShortDateString() + ".xlsx";
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