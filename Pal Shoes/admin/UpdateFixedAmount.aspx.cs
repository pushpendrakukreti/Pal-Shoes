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

public partial class admin_UpdateFixedAmount : System.Web.UI.Page
{
    WalletCls ob_wallet = new WalletCls();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Convert.ToString(Session["userid"])) && Convert.ToString(Session["logintype"]) == "Admin")
        {
            if (!IsPostBack)
            {
                BindPercentage();
                BindFixPercentage();
            }
            
        }
        else
        {
            Response.Redirect("Login.aspx");
        }
    }

    private void BindPercentage()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = ob_wallet.getPercentagelbl();
            if (dt.Rows.Count > 0)
            {
                lblPercentage.Text = dt.Rows[0]["Percentage"].ToString() + "%";
            }
        }
        catch
        { }
    }

    private void BindFixPercentage()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = ob_wallet.getfixPercentage();
            if (dt.Rows.Count > 0)
            {
                gvWalletDetails.DataSource = dt;
                gvWalletDetails.DataBind();
            }
            else
            {
                Label1.Text = "<br/><br/><br/>There is no any records.";
                Label1.Font.Size = 20;
            }
        }
        catch
        { }
    }
    //

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            decimal percentage = Convert.ToDecimal(txtamount.Text);
            int result = 0;

            result = ob_wallet.UpdatePercentage(percentage);
            if (result != 0)
            {
                BindFixPercentage();
                txtamount.Text = "";
                lblMessage.ForeColor = Color.Green;
                lblMessage.Text = "Fixed amount percentage updated";
                BindPercentage();
            }
        }
        catch
        { }
    }
    protected void gvWalletDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Details")
        {
            string userid=Convert.ToString(e.CommandArgument);
            Response.Redirect("FixedAmountDetails.aspx?fix=" + userid);
        }
    }



    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            DataTable dt = new DataTable("FixedAmount");
            dt.Columns.Add("S.N.", typeof(Int32));
            dt.Columns.Add("User ID", typeof(string));
            dt.Columns.Add("Percentage", typeof(string));
            dt.Columns.Add("Fixed Amount", typeof(string));
            dt.Columns.Add("Add Amount", typeof(string));
            dt.Columns.Add("Detach Amount", typeof(string));
            dt.Columns.Add("Date", typeof(string));
            dt.Columns.Add("Total", typeof(string));

            foreach (GridViewRow row in gvWalletDetails.Rows)
            {
                Int32 sn = row.DataItemIndex + 1;
                string useridetify = row.Cells[1].Text;
                string percentage = row.Cells[2].Text;
                string fixamount = row.Cells[3].Text;
                string addamount = row.Cells[4].Text;
                string dtachamount = row.Cells[5].Text;
                string date1 = row.Cells[6].Text;
                string total = row.Cells[7].Text;

                dt.Rows.Add(sn, useridetify, percentage, fixamount, addamount, dtachamount, date1, total);
            }

            if (dt.Rows.Count > 0)
            {
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt, "FixedAmount");

                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    string FileName = "FixedAmount_" + DateTime.Now.ToShortDateString() + ".xlsx";
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
        catch
        { }
    }
}