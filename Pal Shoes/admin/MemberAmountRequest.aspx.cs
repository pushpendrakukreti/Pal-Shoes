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

public partial class admin_MemberAmountRequest : System.Web.UI.Page
{
    WalletCls ob_wallet = new WalletCls();
    private int PageSize = 50;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Convert.ToString(Session["userid"])) && Convert.ToString(Session["logintype"]) == "Admin")
        {
            if (!IsPostBack)
            {
                this.BindMembersDetails(1);
            }
        }
        else
        {
            Response.Redirect("Login.aspx");
        }
    }

    private void BindMembersDetails(int pageIndex)
    {
        DataTable dt = new DataTable();
        try
        {
            int pageOutput = 1;
            dt = ob_wallet.getMemberAmountRequest1(pageIndex, PageSize, out pageOutput);
            if (dt.Rows.Count > 0)
            {
                rpAmountRequests.DataSource = dt;
                rpAmountRequests.DataBind();

                this.PopulatePager(pageOutput, pageIndex);
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

    protected void rpAmountRequests_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "FundTransfer")
        {
            Label userid = (Label)e.Item.FindControl("lblUserid");
            Label amount = (Label)e.Item.FindControl("lblAmount");
            Session["RequestUserid"] = userid.Text;
            Session["RequestAmount"] = amount.Text;
            Session["RequestID"] = e.CommandArgument;
            Response.Redirect("FundTransfer1.aspx", false);
        }
    }
    protected void rpAmountRequests_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            if ((e.Item.ItemType == ListItemType.Item) || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lblstatus = (Label)e.Item.FindControl("lblstatus");
                LinkButton lnktransfer = (LinkButton)e.Item.FindControl("lnkFundTransfer");
                LinkButton lnktransferdone = (LinkButton)e.Item.FindControl("lnkTransferDone");
                if (lblstatus.Text == "False")
                {
                    lnktransfer.Visible = true;
                    lnktransferdone.Visible = false;
                }
                else
                {
                    lnktransfer.Visible = false;
                    lnktransferdone.Visible = true;
                }
            }
        }
        catch
        {
        }
    }


    protected void Page_Changed(object sender, EventArgs e)
    {
        int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
        this.BindMembersDetails(pageIndex);
    }

    private void PopulatePager(int recordCount, int currentPage)
    {
        double dblPageCount = (double)((decimal)recordCount / Convert.ToDecimal(PageSize));
        int pageCount = (int)Math.Ceiling(dblPageCount);
        List<ListItem> pages = new List<ListItem>();
        if (pageCount > 0)
        {
            for (int i = 1; i <= pageCount; i++)
            {
                pages.Add(new ListItem(i.ToString(), i.ToString(), i != currentPage));
            }
        }
        rptPager.DataSource = pages;
        rptPager.DataBind();
    }


    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        DataTable dt = new DataTable("Withdrawal");
        int pageOutput = 1;
        dt = ob_wallet.getMemberAmountRequest1(1, PageSize, out pageOutput);

        if (dt.Rows.Count > 0)
        {
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, "Withdrawal");

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                string FileName = "Withdrawal_Request_detail_" + DateTime.Now.ToShortDateString() + ".xlsx";
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