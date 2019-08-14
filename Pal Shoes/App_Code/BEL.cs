using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using SqlAccess;

/// <summary>
/// Summary description for BEL
/// </summary>
public class BEL
{
    public Int32 ProductId { get; set; }
    public string TransId { get; set; }
    public Int32 Userid { get; set; }
    public string ShipFname { get; set; }
    public string ShipLname { get; set; }
    public string ShipEmail { get; set; }
    public string ShipCompany { get; set; }
    public string ShipMobile { get; set; }
    public string ShipAddress { get; set; }
    public string ShipNearby { get; set; }
    public string ShipCountry { get; set; }
    public string ShipState { get; set; }
    public string ShipCity { get; set; }
    public string ShipZip { get; set; }
    public string BillFname { get; set; }
    public string BillLname { get; set; }
    public string BillEmailid { get; set; }
    public string BillCompay { get; set; }
    public string BillContact { get; set; }
    public string BillAddress { get; set; }
    public string BillNearby { get; set; }
    public string BillCountry { get; set; }
    public string BillState { get; set; }
    public string BillCity { get; set; }
    public string BillZip { get; set; }
    public string Productname { get; set; }
    public Int32 Quantity { get; set; }
    public decimal ItemPrice { get; set; }
    public string Thumbnail { get; set; }
    public string Orderdetailid { get; set; }
    public string Currency { get; set; }
    public string Paymentmode { get; set; }
    public decimal Totalamount { get; set; }
    public decimal ItemSubtotal { get; set; }
    public decimal ShippinCharge { get; set; }
    public string Orderstatus { get; set; }
    public bool OrderCancel { get; set; }

    public int WalletId { get; set; }
    public int UserId { get; set; }
    public decimal Amount { get; set; }
    public decimal AddedAmount { get; set; }
    public decimal DeductedAmount { get; set; }
    public long TransactionNo { get; set; }
    public string PaymentType { get; set; }
    public string WalletStatus { get; set; }
    public int ActionBy { get; set; }
    public string Customername { get; set; }
    public string EmailId { get; set; }
    public decimal BounceMoney { get; set; }
    public string UserIdetifyNo { get; set; }
    public string Capacity { get; set; }
    public string Flavour { get; set; }

    public string finalCouponPrice { get; set; }
    public string Variantname { get; set; }
    public Int32 Variantquantity { get; set; }

    public Int32 FinalShippingCharges { get; set; }
}