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

public partial class admin_WalletDetails : System.Web.UI.Page
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
                if (!string.IsNullOrEmpty(Session["WalletUser"] as string))
                {
                    string userID = Session["WalletUser"].ToString();
                    if (userID != null)
                    {
                        BindUserWalletDetails(userID);
                        txtDate1.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                    }
                }
            }
        }
        else
        {
            Response.Redirect("Login.aspx");
        }
    }

    private void BindUserWalletDetails(string userid)
    {
        DataTable dt = new DataTable();
        try
        {
            dt = local_wallet.getUserWalletDetails(userid);
            if (dt.Rows.Count > 0)
            {
                gvWalletDetails.DataSource = dt;
                gvWalletDetails.DataBind();
            }
            else
            {
                gvWalletDetails.DataSource = null;
                gvWalletDetails.DataBind();

                lblMessage.Text = "<br/><br/><br/>There is no any records.";
                lblMessage.Font.Size = 20;
            }
        }
        catch
        { }
    }

    protected void gvWalletDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvWalletDetails.PageIndex = e.NewPageIndex;
        string userID = Session["WalletUser"].ToString();
        if (userID != null)
        {
            BindUserWalletDetails(userID);
        }
    }


    protected void btnGetDetails_Click(object sender, EventArgs e)
    {
        lblMessage.Text = "";
        try
        {
            DataTable dt = new DataTable();

            if (txtSearch.Text != "")
            {
                dt = local_wallet.getUserWalletDetailsSearch(txtSearch.Text);
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
                dt = local_wallet.getUserWalletDetailsByDate(date1, date2, Session["WalletUser"].ToString());
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
                string userID = Session["WalletUser"].ToString();
                if (userID != null)
                {
                    BindUserWalletDetails(userID);
                }
            }
        }
        catch (Exception ex)
        {
            lblMessage.ForeColor = Color.Red;
            lblMessage.Text = ex.Message;
        }
    }

    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        DataTable dt = new DataTable("Wallet");
        dt.Columns.Add("S.N.", typeof(Int32));
        dt.Columns.Add("User ID", typeof(string));
        dt.Columns.Add("Customer Name", typeof(string));
        dt.Columns.Add("Balance", typeof(decimal));
        dt.Columns.Add("Add Amount", typeof(decimal));
        dt.Columns.Add("Deduct Amount", typeof(decimal));
        dt.Columns.Add("Date", typeof(string));
        dt.Columns.Add("Payment", typeof(string));
        dt.Columns.Add("Transaction No", typeof(string));
        dt.Columns.Add("Remarks", typeof(string));

        foreach (GridViewRow row in gvWalletDetails.Rows)
        {
            Int32 sn = row.DataItemIndex + 1;
            string useridetify = row.Cells[1].Text;
            string customer = row.Cells[2].Text;
            string balance = row.Cells[3].Text;
            string addamt = row.Cells[4].Text;
            addamt = addamt.Replace("&nbsp;", "0");
            string deduct = row.Cells[5].Text;
            deduct = deduct.Replace("&nbsp;", "0");
            string date = row.Cells[6].Text;
            string payment = row.Cells[7].Text;
            payment = payment.Replace("&nbsp;", "NA");
            string transaction = row.Cells[8].Text;
            transaction = transaction.Replace("&nbsp;", "NA");
            string status = row.Cells[9].Text;
            status = status.Replace("&nbsp;", "NA");

            dt.Rows.Add(sn, useridetify, customer, balance, addamt, deduct, date, payment, transaction, status);
        }

        if (dt.Rows.Count > 0)
        {
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, "Wallet");

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                string FileName = "Wallet_detail_" + DateTime.Now.ToShortDateString() + ".xlsx";
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