using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SqlAccess;
using System.Drawing;

public partial class admin_SaleItem : System.Web.UI.Page
{
    ShoppingCart local_cart = new ShoppingCart();
    OrderDetails obj_order = new OrderDetails();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Convert.ToString(Session["userid"])) && Convert.ToString(Session["logintype"]) == "Admin")
        {
            if (!IsPostBack)
            {
                string transid = Request.QueryString["transaction"];
                if (transid != null)
                {
                    BindOrderDetailsByTrasaction(transid);
                    BindCartItems(transid);
                }
            }
        }
        else
        {
            Response.Redirect("Login.aspx");
        }
    }

    //private void BindCartItems(string Transid)
    //{
    //    try
    //    {
    //        DataTable dt = new DataTable();
    //        dt = local_cart.getSalesComplete(Transid);
    //        if (dt.Rows.Count > 0)
    //        {
    //            DataList1.DataSource = dt;
    //            DataList1.DataBind();

    //            decimal total = 0;
    //            decimal shipping = 0;

    //            lblShipping.Text = "INR " + Convert.ToDecimal(dt.Rows[0]["FinalShippingCharges"]);

    //            for (int i = 0; i < dt.Rows.Count; i++)
    //            {
    //                decimal pdtotal;
    //                decimal shipcharge;
    //                decimal quantity;
    //                string currency = dt.Rows[i]["Currency"].ToString();

    //                if (currency == "INR")
    //                {
    //                    //pdtotal = Convert.ToDecimal(dt.Rows[i]["ItemSubTotal"]);
    //                    pdtotal = Convert.ToDecimal(dt.Rows[i]["CouponPrice"]);
    //                    total = total + pdtotal;
    //                    shipcharge = Convert.ToDecimal(dt.Rows[i]["ShippingCharge"]);
    //                    quantity = Convert.ToDecimal(dt.Rows[i]["Quantity"]);
    //                    shipping = quantity * shipcharge;
    //                    total = total + shipping;
    //                    //lblShipping.Text = "INR " + Convert.ToString(shipping);
    //                    lblTotal.Text = "INR " + total;

    //                }
    //                else if (currency == "USD")
    //                {
    //                    pdtotal = Convert.ToDecimal(dt.Rows[i]["ItemSubTotal"]);
    //                    decimal usd = Convert.ToDecimal(65);
    //                    total = total + pdtotal;
    //                    shipcharge = Convert.ToDecimal(dt.Rows[i]["ShippingCharge"]);
    //                    quantity = Convert.ToDecimal(dt.Rows[i]["Quantity"]);
    //                    shipping = quantity * shipcharge;
    //                    total = total + shipping;
    //                    lblShipping.Text = "USD " + Convert.ToString(shipping);
    //                    lblTotal.Text = "USD " + total;
    //                }
    //            }

    //            Int32 finalprice = Convert.ToInt32(total);
    //            //lblGSTPrice.Text = Convert.ToString(finalprice * 12 / 100);
    //            lblGSTPrice.Text = Convert.ToString(Convert.ToInt32(finalprice * .107));
    //        }
    //    }
    //    catch(Exception ex)
    //    { throw new Exception(ex.Message); }
    //}

    private void BindCartItems(string Transid)
    {
        try
        {
            DataTable dt = new DataTable();
            dt = local_cart.getSalesComplete(Transid);
            if (dt.Rows.Count > 0)
            {
                DataList1.DataSource = dt;
                DataList1.DataBind();

                decimal total = 0;
                decimal subtotal = 0;
                decimal shipping = 0;

                //No need extra column for shipping charges, automaticaaly done
                //lblShipping.Text = "INR " + Convert.ToDecimal(dt.Rows[0]["FinalShippingCharges"]);


                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    decimal pdtotal;
                    decimal pdsubtotal;
                    decimal shipcharges;
                    decimal quantity;
                    string currency = dt.Rows[i]["Currency"].ToString();

                    if (currency == "INR")
                    {
                        pdsubtotal = Convert.ToDecimal(dt.Rows[i]["ItemSubTotal"]);
                        pdtotal = Convert.ToDecimal(dt.Rows[0]["CouponPrice"]);
                        total = total + pdtotal;

                        subtotal = subtotal + pdsubtotal;

                        shipcharges = Convert.ToDecimal(dt.Rows[i]["ShippingCharge"]);
                        quantity = Convert.ToDecimal(dt.Rows[i]["Quantity"]);
                        shipcharges = quantity * shipcharges;
                        total = total + shipping;
                        shipping = shipping + shipcharges;
                        lblShipping.Text = "INR " + Convert.ToString(shipping);

                        Session["Shippingcharges"] = shipping;
                        //lblTotal.Text = "INR " + total;
                        lbltotalproductprice.Text = "INR " + subtotal;

                        lblTotal.Text = "INR " + (Convert.ToInt32(dt.Rows[0]["CouponPrice"]) + shipping);

                        if (subtotal > pdtotal)
                        {
                            lblPromotionDiscount.Text = "|| Promotion Applied = INR " + Convert.ToString(Convert.ToInt32(subtotal - pdtotal));
                            lblPromotionDiscount.Visible = true;
                        }

                    }
                    else if (currency == "USD")
                    {
                        pdtotal = Convert.ToDecimal(dt.Rows[i]["ItemSubTotal"]);
                        decimal usd = Convert.ToDecimal(65);
                        total = total + pdtotal;
                        shipcharges = Convert.ToDecimal(dt.Rows[i]["ShippingCharge"]);
                        quantity = Convert.ToDecimal(dt.Rows[i]["Quantity"]);
                        shipping = quantity * shipcharges;
                        total = total + shipping;
                        lblShipping.Text = "USD " + Convert.ToString(shipping);
                        lblTotal.Text = "USD " + total;
                    }
                }

                Int32 finalprice = Convert.ToInt32(Convert.ToDecimal(dt.Rows[0]["CouponPrice"]) + Convert.ToInt32(Session["Shippingcharges"]));
                //lblGSTPrice.Text = Convert.ToString(finalprice * 12 / 100);
                lblGSTPrice.Text = Convert.ToString(Convert.ToInt32(finalprice * .107));
            }
        }
        catch (Exception ex)
        { throw new Exception(ex.Message); }
    }

    string orderid;
    private void BindOrderDetailsByTrasaction(string transid)
    {
        try
        {
            DataTable dt = new DataTable();
            dt = obj_order.getSalesDetailsByTransaction(transid);
            if (dt.Rows.Count > 0)
            {
                orderid = dt.Rows[0]["OrderId"].ToString();

                txtSFName.Text = dt.Rows[0]["ShipFname"].ToString();
                txtSLName.Text = dt.Rows[0]["ShipLname"].ToString();
                txtSPhoneno.Text = dt.Rows[0]["ShipMobile"].ToString();
                txtSEmailid.Text = dt.Rows[0]["ShipEmail"].ToString();
                txtShipAddress.Text = dt.Rows[0]["ShipAddress"].ToString();
                txtSNearBy.Text = dt.Rows[0]["ShipNearby"].ToString();
                txtSCity.Text = dt.Rows[0]["ShipCity"].ToString();
                txtSState.Text = dt.Rows[0]["sState"].ToString();
                txtSPincode.Text = dt.Rows[0]["ShipZip"].ToString();
                //txtSCountry.Text = dt.Rows[0]["sCountry"].ToString();
                txtSCountry.Text = "India";

                lblBFname.Text = dt.Rows[0]["BillFname"].ToString();
                lblBLname.Text = dt.Rows[0]["BillLname"].ToString();
                lblBContact.Text = dt.Rows[0]["BillContact"].ToString();
                lblBEmailid.Text = dt.Rows[0]["BillEmailid"].ToString();
                lblBillingAddress.Text = dt.Rows[0]["BillAddress"].ToString();
                lblBNearby.Text = dt.Rows[0]["BillNearby"].ToString();
                lblBCity.Text = dt.Rows[0]["BillCity"].ToString();
                lblBState.Text = dt.Rows[0]["bState"].ToString();
                lblBPincode.Text = dt.Rows[0]["BillZip"].ToString();
                //lblBCountry.Text = dt.Rows[0]["bCountry"].ToString();
                lblBCountry.Text = "India";

                Session["CustId"] = dt.Rows[0]["Orderdetailid"].ToString();
                Session["ProductId"] = dt.Rows[0]["ProductId"].ToString();
                Session["Currencies"] = dt.Rows[0]["Currency"].ToString();

                lblPaymentMode.Text = dt.Rows[0]["Paymentmode"].ToString();
                lblCurrency.Text = dt.Rows[0]["Currency"].ToString();
                lblOrderno.Text = dt.Rows[0]["Orderdetailid"].ToString();
                lblCustomerId.Text = dt.Rows[0]["UseridentifyNo"].ToString();
                lblOrderTime.Text = Convert.ToDateTime(dt.Rows[0]["Orderdate"]).ToString("HH:mm:ss tt");
                lblSaleDate.Text = Convert.ToDateTime(dt.Rows[0]["Orderdate"]).ToString("dd/MM/yyyy");
            }
        }
        catch
        { }
    }

    protected void lnkprint_Click(object sender, EventArgs e)
    {
        string printorder = Request.QueryString["transaction"];
        if (printorder != null)
        {
            Response.Redirect("saleprint.aspx?sales=" + printorder);
        }
    }


    private string GetTransactionNoToBindOrder(string userid)
    {
        DataTable dt = new DataTable();
        string transctionNo = "";
        try
        {
            dt = obj_order.getTransationNoByUserID(userid);
            if (dt.Rows.Count > 0)
            {
                transctionNo = Convert.ToString(dt.Rows[0]["TransId"]);
            }
        }
        catch { }
        return transctionNo;
    }

    protected void DataList1_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        string transid = Request.QueryString["transaction"];
        Session["currency"] = BindCurrency(transid);
        if (Session["currency"].ToString() == "INR")
        {
            Literal lbltotal = (Literal)e.Item.FindControl("lbltotal");
            Literal lblusdtotal = (Literal)e.Item.FindControl("lblusdtotal");
            Literal lblprice = (Literal)e.Item.FindControl("lblprice");
            Literal lblusdprice = (Literal)e.Item.FindControl("lblusdprice");

            Literal lblvariant = (Literal)e.Item.FindControl("lblvariant");

            lbltotal.Visible = true;
            lblprice.Visible = true;

            lblusdprice.Visible = false;
            lblusdtotal.Visible = false;

            lbltotal.Text = "INR " + lbltotal.Text;
            lblprice.Text = "INR " + lblprice.Text;
        }
        else
        {
            Literal lbltotal = (Literal)e.Item.FindControl("lbltotal");
            Literal lblusdtotal = (Literal)e.Item.FindControl("lblusdtotal");
            Literal lblprice = (Literal)e.Item.FindControl("lblprice");
            Literal lblusdprice = (Literal)e.Item.FindControl("lblusdprice");
            lbltotal.Visible = false;
            lblprice.Visible = false;

            lblusdprice.Visible = true;
            lblusdtotal.Visible = true;

            lblusdprice.Text = "USD " + lblusdprice.Text;
            lblusdtotal.Text = "USD " + lblusdtotal.Text;
        }
    }

    private string BindCurrency(string transactionid)
    {
        DataTable dt = new DataTable();
        string currencey = "";
        try
        {
            dt = local_cart.getCurrencyBySession(transactionid);
            if (dt.Rows.Count > 0)
            {
                currencey = dt.Rows[0]["Currency"].ToString();
            }
        }
        catch
        { }
        return currencey;
    }
}