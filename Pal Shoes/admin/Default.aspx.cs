using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SqlAccess;
using System.Text;
using System.Drawing;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using InfoSoftGlobal;
using System.Globalization;
using System.Threading;


public partial class admin_AdminPanel : System.Web.UI.Page
{
    Products ob_product = new Products();
    WalletCls ob_wallet = new WalletCls();
    OrderDetails ob_order = new OrderDetails();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Convert.ToString(Session["userid"])) && Convert.ToString(Session["logintype"]) == "Admin")
        {
            if(!IsPostBack)
            {
                BindGraphData();
            }
        }
        else
        {
            Response.Redirect("Login.aspx");
        }
    }


    private void BindGraphData()
    {
        //DataTable dt = new DataTable();
        //dt = ob_order.getSalesOrderChart();
        //if (dt.Rows.Count > 0)
        //{
        //    string[] XPointMember = new string[dt.Rows.Count];
        //    int[] YPointMember = new int[dt.Rows.Count];

        //    for (int count = 0; count < dt.Rows.Count; count++)
        //    {
        //        XPointMember[count] = dt.Rows[count]["MonthName"].ToString();
        //        YPointMember[count] = Convert.ToInt32(dt.Rows[count]["TotalSales"]);
        //    }
        //    Chart1.Series[0].Points.DataBindXY(XPointMember, YPointMember);
        //    Chart1.ChartAreas[0].AxisY.LabelStyle.Format = "{0:0}";
        //}
    }


    //protected override void InitializeCulture()
    //{
    //    CultureInfo ci = new CultureInfo("en-IN");
    //    ci.NumberFormat.CurrencySymbol = "₹";
    //    Thread.CurrentThread.CurrentCulture = ci;

    //    base.InitializeCulture();
    //}   

}