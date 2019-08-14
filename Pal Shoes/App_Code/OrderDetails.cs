using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using SqlAccess;
using System.Drawing;

/// <summary>
/// Summary description for OrderDetails
/// </summary>
public class OrderDetails
{
    ShoppingCart obj_cart = new ShoppingCart();

    public DataTable getOrderDetails()
    {
        try
        {
            string query = @"SELECT OrderId, ProductId, TransId, Userid, ShipFname, ShipLname, ShipEmail, ShipCompany, ShipMobile, ShipAddress, ShipNearby, ShipCountry, ShipState, ShipCity, ShipZip, (BillFname+' '+BillLname)as Name, BillEmailid, BillCompay, 
BillContact, BillAddress, BillNearby, BillCountry, BillState, BillCity, BillZip, Orderdate, Productname, Quantity, ItemPrice, Thumbnail, Orderdetailid, Currency, Paymentmode, ItemSubTotal, ShippingCharge, Capacity,(select FlavourName from [Flavour] where FlavourID=t_OrderDetail.Flavour)as Flavours, Orderstatus, OrderCancel,UseridentifyNo FROM t_OrderDetail order by OrderId DESC";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public DataTable getSalesDetails()
    {
        try
        {
            string query = @"SELECT OrderId, ProductId, TransId, Userid, ShipFname, ShipLname, ShipEmail, ShipCompany, ShipMobile, ShipAddress, ShipNearby, ShipCountry, ShipState, ShipCity, ShipZip, (BillFname+' '+BillLname)as Name, BillEmailid, BillCompay, BillContact, BillAddress, BillNearby, BillCountry, BillState, BillCity, BillZip, Orderdate, Productname, Quantity, ItemPrice, Thumbnail, Orderdetailid, Currency, Paymentmode, ItemSubTotal, ShippingCharge, ISNULL(Orderstatus, 0), OrderCancel, UseridentifyNo FROM t_OrderDetail where Orderstatus='Order Completed' and OrderCancel=0 order by OrderId DESC";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable getOrderDetailsByOrderId(string orderid)
    {
        try
        {
            string query = @"SELECT OrderId, ProductId, TransId, Userid, ShipFname, ShipLname, ShipEmail, ShipCompany, ShipMobile, ShipAddress, ShipNearby, ShipCountry, ShipState, ShipCity, ShipZip, (BillFname+' '+BillLname)as Name, BillEmailid, BillCompay, BillContact, BillAddress, BillNearby, BillCountry, BillState, BillCity, BillZip, Orderdate, Productname, Quantity, ItemPrice, Thumbnail, Orderdetailid, Currency, Paymentmode, ItemSubTotal, ShippingCharge, Orderstatus, OrderCancel, UseridentifyNo FROM t_OrderDetail WHERE Orderdetailid='" + orderid + "' OR BillContact='" + orderid + "'";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public DataTable getSalesDetailsByOrderId(string orderid)
    {
        try
        {
            string query = @"SELECT OrderId, ProductId, TransId, Userid, ShipFname, ShipLname, ShipEmail, ShipCompany, ShipMobile, ShipAddress, ShipNearby, ShipCountry, ShipState, ShipCity, ShipZip, (BillFname+' '+BillLname)as Name, BillEmailid, BillCompay, BillContact, BillAddress, BillNearby, BillCountry, BillState, BillCity, BillZip, Orderdate, Productname, Quantity, ItemPrice, Thumbnail, Orderdetailid, Currency, Paymentmode, ItemSubTotal, ShippingCharge, Orderstatus, OrderCancel, UseridentifyNo FROM t_OrderDetail WHERE Orderdetailid='" + orderid + "' OR UseridentifyNo='" + orderid + "' and Orderstatus='Order Completed' and OrderCancel=0";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable getOrderDetailsByDate(string date1, string date2)
    {
        try
        {
            string query = "";
            if (date1 != "" && date2 != "")
            {
                query = @"SELECT OrderId, ProductId, TransId, Userid, ShipFname, ShipLname, ShipEmail, ShipCompany, 
ShipMobile, ShipAddress, ShipNearby, ShipCountry, ShipState, ShipCity, ShipZip, 
(BillFname+' '+BillLname)as Name, BillEmailid, BillCompay, BillContact, BillAddress, 
BillNearby, BillCountry, BillState, BillCity, BillZip, Orderdate, Productname, Quantity,
 ItemPrice, Thumbnail, Orderdetailid, Currency, Paymentmode, ItemSubTotal, ShippingCharge, 
 Orderstatus, OrderCancel, UseridentifyNo FROM t_OrderDetail 
 where Cast(Orderdate AS DATE) >='" + date1 + "' ANd Cast(Orderdate AS DATE)<= '" + date2 + "' order by Orderdate";
            }
            else
            {
                query = @"SELECT OrderId, ProductId, TransId, Userid, ShipFname, ShipLname, ShipEmail, ShipCompany, 
ShipMobile, ShipAddress, ShipNearby, ShipCountry, ShipState, ShipCity, ShipZip, 
(BillFname+' '+BillLname)as Name, BillEmailid, BillCompay, BillContact, BillAddress, 
BillNearby, BillCountry, BillState, BillCity, BillZip, Orderdate, Productname, Quantity,
 ItemPrice, Thumbnail, Orderdetailid, Currency, Paymentmode, ItemSubTotal, ShippingCharge, 
 Orderstatus, OrderCancel, UseridentifyNo FROM t_OrderDetail 
 where Cast(Orderdate AS DATE)='" + date1 + "' order by Orderdate";
            }
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }


    public DataTable getSalesDetailsByDate(string date1, string date2)
    {
        try
        {
            string query = "";
            if (date1 != "" && date2 != "")
            {
                query = @"SELECT OrderId, ProductId, TransId, Userid, ShipFname, ShipLname, ShipEmail, ShipCompany, 
ShipMobile, ShipAddress, ShipNearby, ShipCountry, ShipState, ShipCity, ShipZip, 
(BillFname+' '+BillLname)as Name, BillEmailid, BillCompay, BillContact, BillAddress, 
BillNearby, BillCountry, BillState, BillCity, BillZip, Orderdate, Productname, Quantity,
 ItemPrice, Thumbnail, Orderdetailid, Currency, Paymentmode, ItemSubTotal, ShippingCharge, 
 Orderstatus, OrderCancel FROM t_OrderDetail 
 where Orderstatus='Order Completed' AND OrderCancel=0 AND Cast(Orderdate AS DATE) >='" + date1 + "' AND Cast(Orderdate AS DATE)<= '" + date2 + "' order by Orderdate";
            }
            else
            {
                query = @"SELECT OrderId, ProductId, TransId, Userid, ShipFname, ShipLname, ShipEmail, ShipCompany, 
ShipMobile, ShipAddress, ShipNearby, ShipCountry, ShipState, ShipCity, ShipZip, 
(BillFname+' '+BillLname)as Name, BillEmailid, BillCompay, BillContact, BillAddress, 
BillNearby, BillCountry, BillState, BillCity, BillZip, Orderdate, Productname, Quantity,
 ItemPrice, Thumbnail, Orderdetailid, Currency, Paymentmode, ItemSubTotal, ShippingCharge, 
 Orderstatus, OrderCancel FROM t_OrderDetail 
 where Orderstatus='Order Completed' AND OrderCancel=0 AND Cast(Orderdate AS DATE)='" + date1 + "' order by Orderdate";
            }
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public int DeleteOrderDetails(Int32 orderid)
    {
        try
        {
            string query = "delete from t_OrderDetail where OrderId='" + orderid + "'";
            int intvalue;
            intvalue = MySqlDataAccess.ExecuteNonQuery(MySqlDataAccess.ConnectionString, CommandType.Text, query);
            return intvalue;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }


    public DataTable getOrderDetailsByTransaction(string transid)
    {
        try
        {
            string query = "Select OrderId, ProductId, TransId, Userid, ShipFname, ShipLname, ShipEmail, ShipCompany, ShipMobile, ShipAddress, ShipNearby, ShipCountry, ShipState, (select ContryName from Country where CountryCode= t_OrderDetail.ShipCountry) as sCountry, (select StateName from State where StateCode= t_OrderDetail.ShipState)as sState, ShipCity, ShipZip, BillFname, BillLname, BillEmailid, BillCompay, BillContact, BillAddress, BillNearby, BillCountry, BillState,(select ContryName from Country where CountryCode= t_OrderDetail.BillCountry) as bCountry, (select StateName from State where StateCode= t_OrderDetail.BillState)as bState, BillCity, BillZip, Orderdate, Productname, Quantity, ItemPrice, Thumbnail, Orderdetailid, Currency, Paymentmode, ItemSubTotal, ShippingCharge, Orderstatus, OrderCancel FROM t_OrderDetail WHERE TransId='" + transid + "'";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable getOrdersDetailsByTransaction(string transid)
    {
        try
        {
            //string query = "Select OrderId, ProductId, TransId, Userid, ShipFname, ShipLname, ShipEmail, ShipCompany, ShipMobile, ShipAddress, ShipNearby, ShipCountry, ShipState, (select ContryName from Country where CountryCode= t_OrderDetail.ShipCountry) as sCountry, (select StateName from State where StateCode= t_OrderDetail.ShipState)as sState, ShipCity, ShipZip, BillFname, BillLname, BillEmailid, BillCompay, BillContact, BillAddress, BillNearby, BillCountry, BillState,(select ContryName from Country where CountryCode= t_OrderDetail.BillCountry) as bCountry, (select StateName from State where StateCode= t_OrderDetail.BillState)as bState, BillCity, BillZip, Orderdate, Productname, Quantity, ItemPrice, Thumbnail, Orderdetailid, Currency, Paymentmode, ItemSubTotal, ShippingCharge, FinalShippingCharges, Orderstatus, OrderCancel, UseridentifyNo, VariantName FROM t_OrderDetail WHERE Orderstatus='Order' OR Orderstatus='Order Completed' AND OrderCancel=0 AND TransId='" + transid + "'";

            string query = "Select OrderId, ProductId, TransId, Userid, ShipFname, ShipLname, ShipEmail, ShipCompany, ShipMobile, ShipAddress, ShipNearby, ShipCountry, ShipState, (select ContryName from Country where CountryCode= t_OrderDetail.ShipCountry) as sCountry, (select StateName from State where StateCode= t_OrderDetail.ShipState)as sState, ShipCity, ShipZip, BillFname, BillLname, BillEmailid, BillCompay, BillContact, BillAddress, BillNearby, BillCountry, BillState,(select ContryName from Country where CountryCode= t_OrderDetail.BillCountry) as bCountry, (select StateName from State where StateCode= t_OrderDetail.BillState)as bState, BillCity, BillZip, Orderdate, Productname, Quantity, ItemPrice, Thumbnail, Orderdetailid, Currency, Paymentmode, ItemSubTotal, ShippingCharge, FinalShippingCharges, Orderstatus, OrderCancel, UseridentifyNo, VariantName FROM t_OrderDetail WHERE OrderCancel=0 AND TransId='" + transid + "'";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable getSalesDetailsByTransaction(string transid)
    {
        try
        {
            string query = "Select OrderId, ProductId, TransId, Userid, ShipFname, ShipLname, ShipEmail, ShipCompany, ShipMobile, ShipAddress, ShipNearby, ShipCountry, ShipState, (select ContryName from Country where CountryCode= t_OrderDetail.ShipCountry) as sCountry, (select StateName from State where StateCode= t_OrderDetail.ShipState)as sState, ShipCity, ShipZip, BillFname, BillLname, BillEmailid, BillCompay, BillContact, BillAddress, BillNearby, BillCountry, BillState,(select ContryName from Country where CountryCode= t_OrderDetail.BillCountry) as bCountry, (select StateName from State where StateCode= t_OrderDetail.BillState)as bState, BillCity, BillZip, Orderdate, Productname, Quantity, ItemPrice, Thumbnail, Orderdetailid, Currency, Paymentmode, ItemSubTotal, ShippingCharge, FinalShippingCharges, Orderstatus, OrderCancel, UseridentifyNo, VariantName FROM t_OrderDetail WHERE Orderstatus='Order Completed' AND OrderCancel=0 AND TransId='" + transid + "'";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    //Wishlist
    public int AddNewWishlist(Int32 productid, string transid, decimal price, Int32 dicount, string name, string thumbnail, int userid, string usermobile)
    {
        try
        {
            string query = "insert into t_Wishlist (ProductId, TransId, Price, Discount, Name, Thumbnail, UserId, UserIdentifyNo) values('" + productid + "','" + transid + "','" + price + "','" + dicount + "','" + name + "','" + thumbnail + "','" + userid + "','" + usermobile + "')";
            return MySqlDataAccess.ExecuteNonQuery(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    //Get Wishlist
    public DataTable getWishlist(string userid)
    {
        try
        {
            string query = @"select top 20 WishId, ProductId, TransId, Price, Discount, Name, Thumbnail, UserId, convert(decimal(10,2), Price - discount)as iprice, convert(decimal(10,2), (Price - discount) / 64.35) as usdprice FROM t_Wishlist WHERE UserId='" + userid + "' AND ProductId is not NULL order by WishId DESC";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable getWishlistAdmin()
    {
        try
        {
            string query = @"select WishId, ProductId, TransId, Price, Discount, Name, Thumbnail, UserId, (select (Firstname+' '+Lastname) from t_User where UserId=t_Wishlist.UserId)as fullname, convert(decimal(10,2), Price - discount)as iprice, convert(decimal(10,2), (Price - discount) / 64.35) as usdprice, UserIdentifyNo FROM t_Wishlist where ProductId is not Null order by WishId DESC";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public DataTable getRecomendedProduct()
    {
        try
        {
            string query = @"select WishId, Name, UserId, (select (Firstname+' '+Lastname) from t_User where UserId=t_Wishlist.UserId)as fullname, (select Emailid from t_User where UserId=t_Wishlist.UserId)as Email, UserIdentifyNo FROM t_Wishlist where ProductId is Null order by WishId DESC";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public int DeletWishlist(int wislistid)
    {
        try
        {
            string query = "delete from t_Wishlist where WishId='" + wislistid + "'";
            return MySqlDataAccess.ExecuteNonQuery(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }


    public int AddOrderDetails(BEL obj_d)
    {
        try
        {
            return obj_cart.InsertProductDetails(obj_d);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            obj_cart = null;
        }
    }

    public int UpdateUserDetails(BEL obj_d)
    {
        try
        {
            return obj_cart.UpdateUsersDetails(obj_d);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            obj_cart = null;
        }
    }

    public int UpdateUserDetailsGuest(BEL obj_d)
    {
        try
        {
            return obj_cart.UpdateUsersDetailsGuest(obj_d);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            obj_cart = null;
        }
    }

    public int AddUpdateUserDetails(BEL obj_d)
    {
        try
        {
            return obj_cart.addUpdateUsersDetails(obj_d);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            obj_cart = null;
        }
    }

    public int UpdateOrderDetail(string transactionID, string paymentmode, string orderstatus)
    {
        try
        {
            string query = "update t_OrderDetail set Paymentmode='" + paymentmode + "', Orderstatus='" + orderstatus + "' where TransId='" + transactionID + "'";
            return MySqlDataAccess.ExecuteNonQuery(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }


    public DataTable getOrderCompleted(string transid)
    {
        try
        {
            string query = "SELECT OrderId, ProductId, TransId, Userid, convert(varchar(11), Orderdate, 106) AS [DD MON YYYY], Orderdetailid, Paymentmode, Orderstatus FROM t_OrderDetail WHERE TransId='" + transid + "' AND OrderCancel=0";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable getTotalPriceOrderComplete(string TransId)
    {
        try
        {
            string query = @"select sum(cast(ItemSubTotal as decimal(18,2)))as totalprice2 FROM t_OrderDetail  WHERE Orderstatus='Order' AND TransId='" + TransId + "'";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable getShippingChargeOrderComplete(string transid)
    {
        try
        {
            string query = @"select ShippingCharge, Quantity From t_OrderDetail where TransId='" + transid + "'";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable getShippingChargeOrderlist(string transid)
    {
        try
        {
            string query = @"select ShippingCharge, Quantity From t_OrderDetail where Orderstatus='Order' AND TransId='" + transid + "'";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable getTransationNoByUserID(string userid)
    {
        try
        {
            string query = @"select top 1 TransId from t_OrderDetail where Userid='" + userid + "' and Orderstatus='Order' AND OrderCancel=0 order by OrderId DESC";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public int CancelOrder(string transid, string orderid, bool cancel)
    {
        try
        {
            string query = @"update t_OrderDetail Set Orderstatus='Order Cancel', OrderCancel='" + cancel + "' where Orderstatus='Order' and TransId='" + transid + "' and OrderId='" + orderid + "'";
            return MySqlDataAccess.ExecuteNonQuery(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }


    public int AddRecomendedProduct(string productname, string userid)
    {
        try
        {
            string query = "insert into t_Wishlist(Name, UserId) values('" + productname + "', '" + userid + "')";
            return MySqlDataAccess.ExecuteNonQuery(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public string IsWishlistExist(Int32 productid, string transactionid, string Update, string Id)
    {
        string FLAG = "F";
        DataTable dt = new DataTable();
        if (Update == "N")
            dt = MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, "select ProductId, TransId from t_Wishlist where ProductId='" + productid + "' AND TransId= '" + transactionid + "'");
        else
            dt = MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, "select ProductId, TransId from t_Wishlist where ProductId='" + productid + "' AND TransId= '" + transactionid + "' AND WishId !='" + Id + "'");

        if (dt.Rows.Count > 0)
        {
            FLAG = "T";
        }
        return FLAG;
    }

    public string IsRecomendedExist(string productname, string userid, string Update, string Id)
    {
        string FLAG = "F";
        DataTable dt = new DataTable();
        if (Update == "N")
            dt = MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, "select ProductId, TransId from t_Wishlist where Name='" + productname + "' AND UserId= '" + userid + "'");
        else
            dt = MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, "select ProductId, TransId from t_Wishlist where Name='" + productname + "' AND UserId= '" + userid + "' AND WishId !='" + Id + "'");

        if (dt.Rows.Count > 0)
        {
            FLAG = "T";
        }
        return FLAG;
    }

    
    public int UpdateOrderStatus(string orderstatus, string deliverydate, string orderno, string orderid)
    {
        try
        {
            string query = "update t_OrderDetail set Orderstatus='" + orderstatus + "', DeliveryDate='" + deliverydate + "' where Orderdetailid='" + orderno + "' and OrderId='" + orderid + "'";
            return MySqlDataAccess.ExecuteNonQuery(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable getOrderStatusInfo(string orderid)
    {
        try
        {
            string query = "SELECT OrderId, Orderdetailid FROM t_OrderDetail WHERE OrderId='" + orderid + "' AND OrderCancel=0";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }


    public DataTable getSalesOrderChart()
    {
        try
        {
            string query = @"SELECT YEAR(Orderdate) [Year], MONTH(Orderdate) as [Month],
 DATENAME(MONTH, Orderdate) [MonthName], SUM(ItemSubTotal) AS TotalSales
FROM t_OrderDetail
GROUP BY YEAR(Orderdate), MONTH(Orderdate),
 DATENAME(MONTH, Orderdate)";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}