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

public partial class admin_ApiUserdetail : System.Web.UI.Page
{
    WalletCls obj_wallet = new WalletCls();
    chkLogin obj_login = new chkLogin();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Convert.ToString(Session["userid"])) && Convert.ToString(Session["logintype"]) == "Admin")
        {
            if (!IsPostBack)
            {
                BindBeneficiaryDetails();
                txtDate1.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            }
        }
        else
        {
            Response.Redirect("Login.aspx");
        }
    }

    private void BindBeneficiaryDetails()
    {
        DataTable dt = new DataTable();
        try
        {
            dt = obj_wallet.getBeneficiaryDetail();
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

    protected void gvRegistration_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvRegistration.PageIndex = e.NewPageIndex;
        BindBeneficiaryDetails();
        lblMessage.Text = "";
    }
    

    protected void btnGetDetails_Click(object sender, EventArgs e)
    {
        lblMessage.Text = "";
        try
        {
            DataTable dt = new DataTable();
            if (txtSearch.Text != "")
            {
                dt = obj_wallet.getBeneficiaryDetailByID(txtSearch.Text);
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
                dt = obj_wallet.getBeneficiaryDetailByDate(date1, date2);
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
                BindBeneficiaryDetails();
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
    }


    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        DataTable dt = new DataTable("Benificiary");
        dt.Columns.Add("S.N.", typeof(Int32));
        dt.Columns.Add("User ID", typeof(string));
        dt.Columns.Add("Customer Name", typeof(string));
        dt.Columns.Add("Account Number", typeof(string));
        dt.Columns.Add("Mobile Number", typeof(string));
        dt.Columns.Add("Bene Type", typeof(string));
        dt.Columns.Add("IFSC", typeof(string));
        dt.Columns.Add("Status", typeof(string));

        foreach (GridViewRow row in gvRegistration.Rows)
        {
            Int32 sn = row.DataItemIndex + 1;
            string useridetify = row.Cells[1].Text;
            string customer = row.Cells[2].Text;
            string accountno = row.Cells[3].Text;
            string contact = row.Cells[4].Text;
            string benetype = row.Cells[5].Text;
            string ifsc = row.Cells[6].Text;
            string status = row.Cells[7].Text;

            dt.Rows.Add(sn, useridetify, customer, accountno, contact, benetype, ifsc, status);
        }

        if (dt.Rows.Count > 0)
        {
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, "Benificiary");

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                string FileName = "Benificiary_detail_" + DateTime.Now.ToShortDateString() + ".xlsx";
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