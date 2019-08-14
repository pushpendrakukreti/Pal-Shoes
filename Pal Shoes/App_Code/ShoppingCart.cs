using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using SqlAccess;
using System.Data.SqlClient;

/// <summary>
/// Summary description for ShoppingCart
/// </summary>

public class ShoppingCart
{
    //Modelno change to Shipping charges

    WalletCls ob_wal = new WalletCls();

    public DataTable getDataFromProdtuct(string productid)
    {
        try
        {
            //string query = @"Select ProductId, Name, [Description], convert(int, Price)as price, convert(int, Price - (Price * Discount)/100)as totalprice, convert(int, (Price * Discount)/100)as discountprice, [Image], Thumbnail, SKUCode, Discount, Modelno, Capacity, Producttype FROM t_Product WHERE ProductId='" + productid + "'";

            string query = @"Select top 1 t_Product.ProductId, Name, [Description], convert(int, Price)as price, convert(int, Price - (Price * Discount)/100)as totalprice, convert(int, (Price * Discount)/100)as discountprice, [Image], Thumbnail, SKUCode, Discount, ShippingCharges, Capacity, Producttype, Quantity, Variantname FROM t_Product INNER JOIN t_Quantity ON t_Product.ProductId=t_Quantity.ProductId WHERE t_Product.ProductId='" + productid + "'";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable getDataFromProdtuctByVariant(string productid, string variantname)
    {
        try
        {
            //string query = @"Select top 1 t_Product.ProductId, Name, [Description], convert(int, Price)as price, convert(int, Price - (Price * Discount)/100)as totalprice, convert(int, (Price * Discount)/100)as discountprice, [Image], Thumbnail, SKUCode, Discount, ShippingCharges, Capacity, Producttype, Quantity, Variantname FROM t_Product INNER JOIN t_Quantity ON t_Product.ProductId=t_Quantity.ProductId WHERE t_Product.ProductId='" + productid + "' And Variantname='" + variantname + "'";
            string query = @"Select top 1 t_Product.ProductId, Name, [Description], convert(int, Price)as price, convert(int, Price - (Price * Discount)/100)as totalprice, convert(int, (Price * Discount)/100)as discountprice, [Image], Thumbnail, SKUCode, Discount, ShippingCharges, Capacity, Producttype, Quantity, Variantname FROM t_Product INNER JOIN t_Quantity ON t_Product.ProductId=t_Quantity.ProductId WHERE t_Product.ProductId='" + productid + "' ";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public DataTable getproductQuantityById(Int32 productId)
    {
        try
        {
            string query = @"Select Quantity, ProductId, FlavourID FROM t_Quantity WHERE ProductId='" + productId + "'";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    
    public int InsertIntoShoppingCart(string transid, Int32 productid, decimal price, string variantname, Int32 variantquantity, Int32 quantity, Int32 discount, decimal totalprice, Int32 shippingcharge, string productname, string thumbnail, string Capacity, Int32 userid, int flavour)
    {
        try
        {
            string query = "Insert into ShopingCart (TransId, ProductId, Price, VariantName, VariantQuantity, Quantity, Discount, TotalPrice, ShippingCharges, Name, Thumbnail, Capacity, UserId, Flavour) values('" + transid + "','" + productid + "','" + price + "','"+ variantname + "', '"+ variantquantity + "','" + quantity + "','" + discount + "','" + totalprice + "','" + shippingcharge + "','" + productname + "','" + thumbnail + "','" + Capacity + "','" + userid + "','" + flavour + "')";
            int intVal;
            intVal = MySqlDataAccess.ExecuteNonQuery(MySqlDataAccess.ConnectionString, CommandType.Text, query);
            return intVal;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }


    //Shopping Cart
    public DataTable getDataInShoppingCart(string TransId)
    {
        try
        {
            string query = @"Select *, (ShippingCharges)as sprice, convert(decimal(10,2), Price - discount)as iprice, convert(decimal(10,2), (Price - discount) / 65) as usdprice, convert(decimal(10,2), TotalPrice) as totalprice1, convert(decimal(10,2), TotalPrice/65)as totalusdprice FROM ShopingCart WHERE TransId='" + TransId + "'";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public DataTable getTotalPriceShoppingCart(string TransId)
    {
        try
        {
            string query = @"Select sum(cast(TotalPrice as decimal(18,2)))as totalprice2 FROM ShopingCart WHERE TransId='" + TransId + "'";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable getTotalPriceAfterCouponShoppingCart(string TransId)
    {
        try
        {
            string query = @"Select (CouponPrice)as totalprice2  FROM ShopingCart WHERE TransId='" + TransId + "'";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }


    public DataTable getSubTotalShoppingCart(string TransId, string Id)
    {
        try
        {
            string query = @"select CartId, TransId, Price, Quantity, TotalPrice, ShippingCharges FROM ShopingCart WHERE TransId='" + TransId + "' AND CartId='" + Id + "'";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public int DeleteCartItem(string transid, string cartid)
    {
        try
        {
            string query = @"Delete from ShopingCart WHERE TransId='" + transid + "' AND CartId='" + cartid + "'";
            int value = 0;
            value = MySqlDataAccess.ExecuteNonQuery(MySqlDataAccess.ConnectionString, CommandType.Text, query);
            return value;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }


    public int UpdateCartItem(Int32 quantity, decimal totalprice, string cartid)
    {
        try
        {
            string query = @"Update ShopingCart set Quantity = '" + quantity + "', TotalPrice = '" + totalprice + "' where CartId = '" + cartid + "'";
            object intvalue = MySqlDataAccess.ExecuteScalar(MySqlDataAccess.ConnectionString, CommandType.Text, query);
            return Convert.ToInt32(intvalue);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public int UpdateCartItemByCoupon(string carid, decimal totalpriceaftercoupon, Int32 finalshippingcharges)
    {
        try
        {
            string query = @"Update ShopingCart set CouponPrice = '" + totalpriceaftercoupon + "', FinalShippingCharges='"+ finalshippingcharges + "' where CartId = '" + carid + "'";
            object intvalue = MySqlDataAccess.ExecuteScalar(MySqlDataAccess.ConnectionString, CommandType.Text, query);
            return Convert.ToInt32(intvalue);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }



    //Insert Order Details
    public int InsertProductDetails(BEL obj_d)
    {
        try
        {
            SqlParameter[] p = new SqlParameter[43];
            p[0] = new SqlParameter("@ProductId", obj_d.ProductId);
            p[1] = new SqlParameter("@TransId", obj_d.TransId);
            p[2] = new SqlParameter("@Userid", obj_d.Userid);
            p[3] = new SqlParameter("@ShipFname", obj_d.ShipFname);
            p[4] = new SqlParameter("@ShipLname", obj_d.ShipLname);
            p[5] = new SqlParameter("@ShipEmail", obj_d.ShipEmail);
            p[6] = new SqlParameter("@ShipCompany", obj_d.ShipCompany);
            p[7] = new SqlParameter("@ShipMobile", obj_d.ShipMobile);
            p[8] = new SqlParameter("@ShipAddress", obj_d.ShipAddress);
            p[9] = new SqlParameter("@ShipNearby", obj_d.ShipNearby);
            p[10] = new SqlParameter("@ShipCountry", obj_d.ShipCountry);
            p[11] = new SqlParameter("@ShipState", obj_d.ShipState);
            p[12] = new SqlParameter("@ShipCity", obj_d.ShipCity);
            p[13] = new SqlParameter("@ShipZip", obj_d.ShipZip);
            p[14] = new SqlParameter("@BillFname", obj_d.BillFname);
            p[15] = new SqlParameter("@BillLname", obj_d.BillLname);
            p[16] = new SqlParameter("@BillEmailid", obj_d.BillEmailid);
            p[17] = new SqlParameter("@BillCompay", obj_d.BillCompay);
            p[18] = new SqlParameter("@BillContact", obj_d.BillContact);
            p[19] = new SqlParameter("@BillAddress", obj_d.BillAddress);
            p[20] = new SqlParameter("@BillNearby", obj_d.BillNearby);
            p[21] = new SqlParameter("@BillCountry", obj_d.BillCountry);
            p[22] = new SqlParameter("@BillState", obj_d.BillState);
            p[23] = new SqlParameter("@BillCity", obj_d.BillCity);
            p[24] = new SqlParameter("@BillZip", obj_d.BillZip);
            p[25] = new SqlParameter("@Productname", obj_d.Productname);
            p[26] = new SqlParameter("@Quantity", obj_d.Quantity);
            p[27] = new SqlParameter("@Thumbnail", obj_d.Thumbnail);
            p[28] = new SqlParameter("@Orderdetailid", obj_d.Orderdetailid);
            p[29] = new SqlParameter("@Currency", obj_d.Currency);
            p[30] = new SqlParameter("@Paymentmode", obj_d.Paymentmode);
            p[31] = new SqlParameter("@ItemSubTotal", obj_d.ItemSubtotal);
            p[32] = new SqlParameter("@ShippingCharge", obj_d.ShippinCharge);
            p[33] = new SqlParameter("@ItemPrice", obj_d.ItemPrice);
            p[34] = new SqlParameter("@Orderstatus", obj_d.Orderstatus);
            p[35] = new SqlParameter("@OrderCancel", obj_d.OrderCancel);
            p[36] = new SqlParameter("@Useridentifyno", obj_d.UserIdetifyNo);
            p[37] = new SqlParameter("@Capacity", obj_d.Capacity);
            p[38] = new SqlParameter("@Flavour", obj_d.Flavour);

            p[39] = new SqlParameter("@VariantName", obj_d.Variantname);
            p[40] = new SqlParameter("@VariantQuantity", obj_d.Variantquantity);
            p[41] = new SqlParameter("@CouponPrice", obj_d.finalCouponPrice);
            p[42] = new SqlParameter("@FinalShippingCharges", obj_d.FinalShippingCharges);
            
            return MySqlDataAccess.ExecuteNonQuery(MySqlDataAccess.ConnectionString, CommandType.StoredProcedure, "ProAddOrderDetails", p);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public static long OrderDtID(string col, string table)
    {
        int count = 0;
        while (true)
        {
            count = count + 1;
            string query = "select isnull(max(" + col + "),0) from " + table + "";
            object intReturnValue = MySqlDataAccess.ExecuteScalar(MySqlDataAccess.ConnectionString, CommandType.Text, query);
            if (Convert.ToInt64(intReturnValue) == 0)
            {
                intReturnValue = 1;
            }
            else
            {
                intReturnValue = Convert.ToInt64(intReturnValue) + 1;
            }
            return Convert.ToInt64(intReturnValue);
        }
    }


    public int UpdateUsersDetails(BEL obj_d)
    {
        try
        {
            SqlParameter[] p = new SqlParameter[23];
            p[0] = new SqlParameter("@ShipFname", obj_d.ShipFname);
            p[1] = new SqlParameter("@ShipLname", obj_d.ShipLname);
            p[2] = new SqlParameter("@ShipEmail", obj_d.ShipEmail);
            p[3] = new SqlParameter("@ShipCompany", obj_d.ShipCompany);
            p[4] = new SqlParameter("@ShipMobile", obj_d.ShipMobile);
            p[5] = new SqlParameter("@ShipAddress", obj_d.ShipAddress);
            p[6] = new SqlParameter("@ShipNearby", obj_d.ShipNearby);
            p[7] = new SqlParameter("@ShipCountry", obj_d.ShipCountry);
            p[8] = new SqlParameter("@ShipState", obj_d.ShipState);
            p[9] = new SqlParameter("@ShipCity", obj_d.ShipCity);
            p[10] = new SqlParameter("@ShipZip", obj_d.ShipZip);
            p[11] = new SqlParameter("@BillFname", obj_d.BillFname);
            p[12] = new SqlParameter("@BillLname", obj_d.BillLname);
            p[13] = new SqlParameter("@BillEmailid", obj_d.BillEmailid);
            p[14] = new SqlParameter("@BillCompay", obj_d.BillCompay);
            p[15] = new SqlParameter("@BillContact", obj_d.BillContact);
            p[16] = new SqlParameter("@BillAddress", obj_d.BillAddress);
            p[17] = new SqlParameter("@BillNearby", obj_d.BillNearby);
            p[18] = new SqlParameter("@BillCountry", obj_d.BillCountry);
            p[19] = new SqlParameter("@BillState", obj_d.BillState);
            p[20] = new SqlParameter("@BillCity", obj_d.BillCity);
            p[21] = new SqlParameter("@BillZip", obj_d.BillZip);
            p[22] = new SqlParameter("@Userid", obj_d.UserId);

            return MySqlDataAccess.ExecuteNonQuery(MySqlDataAccess.ConnectionString, CommandType.StoredProcedure, "ProUpdateUserDetails", p);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public int UpdateUsersDetailsGuest(BEL obj_d)
    {
        try
        {
            SqlParameter[] p = new SqlParameter[23];
            p[0] = new SqlParameter("@ShipFname", obj_d.ShipFname);
            p[1] = new SqlParameter("@ShipLname", obj_d.ShipLname);
            p[2] = new SqlParameter("@ShipEmail", obj_d.ShipEmail);
            p[3] = new SqlParameter("@ShipCompany", obj_d.ShipCompany);
            p[4] = new SqlParameter("@ShipMobile", obj_d.ShipMobile);
            p[5] = new SqlParameter("@ShipAddress", obj_d.ShipAddress);
            p[6] = new SqlParameter("@ShipNearby", obj_d.ShipNearby);
            p[7] = new SqlParameter("@ShipCountry", obj_d.ShipCountry);
            p[8] = new SqlParameter("@ShipState", obj_d.ShipState);
            p[9] = new SqlParameter("@ShipCity", obj_d.ShipCity);
            p[10] = new SqlParameter("@ShipZip", obj_d.ShipZip);
            p[11] = new SqlParameter("@BillFname", obj_d.BillFname);
            p[12] = new SqlParameter("@BillLname", obj_d.BillLname);
            p[13] = new SqlParameter("@BillEmailid", obj_d.BillEmailid);
            p[14] = new SqlParameter("@BillCompay", obj_d.BillCompay);
            p[15] = new SqlParameter("@BillContact", obj_d.BillContact);
            p[16] = new SqlParameter("@BillAddress", obj_d.BillAddress);
            p[17] = new SqlParameter("@BillNearby", obj_d.BillNearby);
            p[18] = new SqlParameter("@BillCountry", obj_d.BillCountry);
            p[19] = new SqlParameter("@BillState", obj_d.BillState);
            p[20] = new SqlParameter("@BillCity", obj_d.BillCity);
            p[21] = new SqlParameter("@BillZip", obj_d.BillZip);
            p[22] = new SqlParameter("@TransactionId", obj_d.TransId);

            return MySqlDataAccess.ExecuteNonQuery(MySqlDataAccess.ConnectionString, CommandType.StoredProcedure, "ProUpdateUserDetailsGuest", p);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }


    public int addUpdateUsersDetails(BEL obj_d)
    {
        try
        {
            SqlParameter[] p = new SqlParameter[23];
            p[0] = new SqlParameter("@ShipFname", obj_d.ShipFname);
            p[1] = new SqlParameter("@ShipLname", obj_d.ShipLname);
            p[2] = new SqlParameter("@ShipEmail", obj_d.ShipEmail);
            p[3] = new SqlParameter("@ShipCompany", obj_d.ShipCompany);
            p[4] = new SqlParameter("@ShipMobile", obj_d.ShipMobile);
            p[5] = new SqlParameter("@ShipAddress", obj_d.ShipAddress);
            p[6] = new SqlParameter("@ShipNearby", obj_d.ShipNearby);
            p[7] = new SqlParameter("@ShipCountry", obj_d.ShipCountry);
            p[8] = new SqlParameter("@ShipState", obj_d.ShipState);
            p[9] = new SqlParameter("@ShipCity", obj_d.ShipCity);
            p[10] = new SqlParameter("@ShipZip", obj_d.ShipZip);
            p[11] = new SqlParameter("@BillFname", obj_d.BillFname);
            p[12] = new SqlParameter("@BillLname", obj_d.BillLname);
            p[13] = new SqlParameter("@BillEmailid", obj_d.BillEmailid);
            p[14] = new SqlParameter("@BillCompay", obj_d.BillCompay);
            p[15] = new SqlParameter("@BillContact", obj_d.BillContact);
            p[16] = new SqlParameter("@BillAddress", obj_d.BillAddress);
            p[17] = new SqlParameter("@BillNearby", obj_d.BillNearby);
            p[18] = new SqlParameter("@BillCountry", obj_d.BillCountry);
            p[19] = new SqlParameter("@BillState", obj_d.BillState);
            p[20] = new SqlParameter("@BillCity", obj_d.BillCity);
            p[21] = new SqlParameter("@BillZip", obj_d.BillZip);
            p[22] = new SqlParameter("@TransactionId", obj_d.TransId);

            return MySqlDataAccess.ExecuteNonQuery(MySqlDataAccess.ConnectionString, CommandType.StoredProcedure, "ProaddUpdateUserDetails", p);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }


    public DataTable getShoppingCartItems(string TransId)
    {
        try
        {
            string query = @"Select s.CartId, s.TransId, s.ProductId, s.Price, s.VariantName, s.VariantQuantity, s.Quantity, s.Discount, s.TotalPrice, s.ShippingCharges, s.FinalShippingCharges, convert(decimal(10,2), ShippingCharges *10 / 65) as usdshippingCharges, s.Name, s.Thumbnail, s.Capacity, s.UserId,  convert(decimal(10,2), Price - discount)as inprice, convert(decimal(10,2), (Price - discount) / 65) as usdprice, convert(decimal(10,2), TotalPrice) as totalinprice, convert(decimal(10,2), TotalPrice/65)as totalusdprice, s.Capacity, s.Flavour, u.ShipFname, u.ShipLname, u.ShipEmail, u.ShipCompany, u.ShipMobile, u.ShipAddress, u.ShipNearby, u.ShipCountry, u.ShipState, u.ShipCity, u.ShipZip, u.BillFname, u.BillLname, u.BillEmailid, u.BillCompay, u.BillContact, u.BillAddress, u.BillNearby, u.BillCountry, u.BillState, u.BillCity, u.BillZip, u.UserIdentifyNo, CouponPrice FROM ShopingCart s inner join t_User u on s.UserId=u.UserId WHERE s.TransId='" + TransId + "'";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public DataTable getShoppingCartItemsGuest(string TransId)
    {
        try
        {
            string query = @"Select s.CartId, s.TransId, s.ProductId, s.Price, s.VariantName, s.VariantQuantity, s.Quantity, s.Quantity, s.Discount, s.TotalPrice, s.ShippingCharges, s.FinalShippingCharges, convert(decimal(10,2), ShippingCharges *10 / 65) as usdshippingCharges, s.Name, s.Thumbnail, s.Capacity, s.UserId, convert(decimal(10,2), Price - discount)as inprice, convert(decimal(10,2), (Price - discount) / 65) as usdprice, convert(decimal(10,2), TotalPrice) as totalinprice, convert(decimal(10,2), TotalPrice/65)as totalusdprice, s.Capacity, s.Flavour, u.ShipFname, u.ShipLname, u.ShipEmail, u.ShipCompany, u.ShipMobile, u.ShipAddress, u.ShipNearby, u.ShipCountry, u.ShipState, u.ShipCity, u.ShipZip, u.BillFname, u.BillLname, u.BillEmailid, u.BillCompay, u.BillContact, u.BillAddress, u.BillNearby, u.BillCountry, u.BillState, u.BillCity, u.BillZip, u.UserIdentifyNo VariantName, ShippingCharges, CouponPrice, FinalShippingCharges FROM ShopingCart s inner join t_User u on s.TransId=u.TransId WHERE s.TransId='" + TransId + "'";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    //Order Overview
    public DataTable getShoppingCartItemsOrderOvr(string TransId)
    {
        try
        {
            string query = @"Select s.CartId, s.TransId, s.ProductId, s.Price, s.Quantity, s.Discount, s.TotalPrice, s.ShippingCharges, convert(decimal(10,2), ShippingCharges *10 / 65) as usdshippingCharges, s.Name, s.Thumbnail, s.Capacity, s.UserId, convert(decimal(10,2), Price - discount)as inprice, convert(decimal(10,2), (Price - discount) / 65) as usdprice, convert(decimal(10,2), TotalPrice) as totalinprice, convert(decimal(10,2), TotalPrice/65)as totalusdprice, s.Capacity, (select FlavourName from Flavour where FlavourID= s.Flavour)as pflavour, u.ShipFname, u.ShipLname, u.ShipEmail, u.ShipCompany, u.ShipMobile, u.ShipAddress, u.ShipNearby, (select ContryName from Country where CountryCode= u.ShipCountry) as Country, (select StateName from State where StateCode= u.ShipState) as State, u.ShipCity, u.ShipZip, u.BillFname, u.BillLname, u.BillEmailid, u.BillCompay, u.BillContact, u.BillAddress, u.BillNearby, (select ContryName from Country where CountryCode= u.BillCountry) as BCountry, (select StateName from State where StateCode= u.BillState) as BState, u.BillCity, u.BillZip, u.UserIdentifyNo, VariantName, ShippingCharges, CouponPrice, FinalShippingCharges FROM ShopingCart s inner join t_User u on s.UserId=u.UserId WHERE s.TransId='" + TransId + "'";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public DataTable getShoppingCartItemsGuestOrderOvr(string TransId)
    {
        try
        {
            string query = @"select s.CartId, s.TransId, s.ProductId, s.Price, s.Quantity, s.Discount, s.TotalPrice, s.ShippingCharges, convert(decimal(10,2), ShippingCharges *10 / 65) as usdshippingCharges, s.Name, s.Thumbnail, s.Capacity, s.UserId, convert(decimal(10,2), Price - discount)as inprice, convert(decimal(10,2), (Price - discount) / 65) as usdprice, convert(decimal(10,2), TotalPrice) as totalinprice, convert(decimal(10,2), TotalPrice/65)as totalusdprice, s.Capacity, (select FlavourName from Flavour where FlavourID= s.Flavour)as pflavour, u.ShipFname, u.ShipLname, u.ShipEmail, u.ShipCompany, u.ShipMobile, u.ShipAddress, u.ShipNearby, (select ContryName from Country where CountryCode= u.ShipCountry) as Country, (select StateName from State where StateCode= u.ShipState) as State, u.ShipCity, u.ShipZip, u.BillFname, u.BillLname, u.BillEmailid, u.BillCompay, u.BillContact, u.BillAddress, u.BillNearby, (select ContryName from Country where CountryCode= u.BillCountry) as BCountry, (select StateName from State 
where StateCode= u.BillState) as BState, u.BillCity, u.BillZip, u.UserIdentifyNo, VariantName, ShippingCharges, FinalShippingCharges FROM ShopingCart s inner join t_User u on s.TransId=u.TransId WHERE s.TransId='" + TransId + "'";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable getOrderOverviewItems(string TransId)
    {
        try
        {
            string query = @"Select *, convert(decimal(10,2), Price - discount)as inrprice, convert(decimal(10,2), (Price - discount) / 65) as usdprice, convert(decimal(10,2), TotalPrice) as intotalprice, convert(decimal(10,2), TotalPrice/65)as usdtotalprice FROM ShopingCart WHERE TransId='" + TransId + "'";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable getOrderComplete(string TransId)
    {
        try
        {
            string query = @"Select OrderId, ProductId, (select Name from t_Product where ProductId= t_OrderDetail.ProductId)as Name, TransId, Userid, Orderdate, Productname, VariantName, VariantQuantity, Quantity, ItemPrice, Thumbnail, Orderdetailid, Currency, Paymentmode, ItemSubTotal, ShippingCharge, FinalShippingCharges, Orderstatus, OrderCancel, CouponPrice FROM t_OrderDetail WHERE TransId='" + TransId + "'";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public DataTable getSalesComplete(string TransId)
    {
        try
        {
            string query = @"Select OrderId, ProductId, (select Name from t_Product where ProductId= t_OrderDetail.ProductId)as Name, TransId, Userid, Orderdate, Productname, Quantity, ItemPrice, Thumbnail, Orderdetailid, Currency, Paymentmode, ItemSubTotal, ShippingCharge, Orderstatus, OrderCancel, VariantName, CouponPrice, FinalShippingCharges  FROM t_OrderDetail WHERE Orderstatus='Order Completed' AND OrderCancel=0 AND TransId='" + TransId + "'";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable getCurrencyBySession(string TransId)
    {
        try
        {
            string query = @"select Currency FROM t_OrderDetail WHERE TransId='" + TransId + "'";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable getOrderCompletetoUser(string TransId)
    {
        try
        {
            string query = @"Select OrderId, ProductId,(select Name from t_Product where ProductId= t_OrderDetail.ProductId)as Name, TransId, Userid, Orderdate, Productname, Quantity, ItemPrice, Thumbnail, Orderdetailid, Currency, Paymentmode, ItemSubTotal, ShippingCharge, Orderstatus, OrderCancel, VariantName FROM t_OrderDetail WHERE TransId='" + TransId + "' AND Orderstatus='Order'";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable getOldOrderCompletetoUser(string userid)
    {
        try
        {
            string query = @"Select top 20 OrderId, ProductId,(select Name from t_Product where ProductId= t_OrderDetail.ProductId)as Name, TransId, Userid, Orderdate, Productname, Quantity, ItemPrice, Thumbnail, Orderdetailid, Currency, Paymentmode, ItemSubTotal, ShippingCharge, Orderstatus, OrderCancel, Orderdate, convert(varchar(11), Orderdate, 106) AS [Order Placed], VariantName FROM t_OrderDetail WHERE Userid='" + userid + "' order by OrderId DESC";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable getQuantityfromTable(Int32 productid, string variantname)
    {
        //string query = "Select Quantity from t_Quantity where ProductId='" + productid + "' AND Variantname='"+ variantname + "'";
        string query = "Select Quantity, Variantname from t_Quantity where ProductId='" + productid + "'";
        return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
    }

    public int UpdateQuantity(Int32 productid, string quantity)
    {
        try
        {
            string query = "Update t_Quantity set Quantity='" + quantity + "' where ProductId='" + productid + "'";
            int result = MySqlDataAccess.ExecuteNonQuery(MySqlDataAccess.ConnectionString, CommandType.Text, query);
            return result;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable getTotalQuantityByvname(string productid)
    {
       string query = "Select Sum(Quantity) as itmequantity from t_Quantity where ProductId='" + productid + "'";
        return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
    }

    public DataTable getShippingCharges(string transid)
    {
        try
        {
            string query = "Select ShippingCharges, convert(decimal(10,2), ShippingCharges *10 /65) as usdshipping, Quantity From ShopingCart where TransId='" + transid + "'";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }


    public DataTable getCartItemNum(string transid)
    {
        try
        {
            string query = "select COUNT(TransId) as Items from ShopingCart where TransId='" + transid + "'";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public DataTable getCartItemtotPrice(string transid)
    {
        try
        {
            //string query = @"Select SUM(convert(decimal(18,2), Quantity * Price - Discount))as itmeprice from ShopingCart where TransId='" + transid + "'";
            string query = @"Select Sum(TotalPrice)as itmeprice from ShopingCart where TransId='" + transid + "'";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public DataTable getWishListCurrentNo(string transid)
    {
        try
        {
            string query = "select COUNT(WishId) as wishNum from t_Wishlist where TransId='" + transid + "'";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public DataTable getCompareItemsNo(string transid)
    {
        try
        {
            string query = "select COUNT(CompareId) as compare from CompareProduct where ComSession='" + transid + "'";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable getUserAccountNameAndID(string userid)
    {
        try
        {
            string query = "select Firstname, UserIdentifyNo from t_User where UserId='" + userid + "'";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable getUserInformation(string userid)
    {
        try
        {
            string query = "Select UserId, (ShipFname+' '+ShipLname)as Shippingname, ShipEmail, ShipCompany, ShipMobile, ShipAddress, ShipNearby, ShipCountry, (select ContryName from Country where CountryCode= t_User.ShipCountry) as Country, ShipState, (select StateName from State where StateCode= t_User.ShipState)as State, ShipCity, ShipZip, (BillFname+' '+BillLname)as Billingname, BillEmailid, BillCompay, BillContact, BillAddress, BillNearby, BillCountry, (select ContryName from Country where CountryCode= t_User.BillCountry)as BCountry, BillState, (select StateName from State where StateCode= t_User.BillState)as BState, BillCity, BillZip from t_User where UserId='" + userid + "'";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }


    public int AddMoney(BEL obj_data)
    {
        try
        {
            return ob_wal.AddWalletMoney(obj_data);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            ob_wal = null;
        }
    }

    public int DeductMoney(BEL obj_data)
    {
        try
        {
            return ob_wal.DeductWalletMoney(obj_data);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            ob_wal = null;
        }
    }

    //User Account
    public DataTable getUserShippingAddress(string userid)
    {
        try
        {
            string query = @"select UserId, (ShipFname+' '+ShipLname)as Shippingname, ShipAddress, ShipNearby, (select ContryName from Country where CountryCode= t_User.ShipCountry) as Country, (select StateName from State where StateCode= t_User.ShipState)as State, ShipCity, ShipZip from t_User where UserId='" + userid + "'";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public DataTable getUserBillingAddress(string userid)
    {
        try
        {
            string query = @"select UserId, (BillFname+' '+BillLname)as Billingname, BillAddress, BillNearby, (select ContryName from Country where CountryCode= t_User.BillCountry) as Country, (select StateName from State where StateCode= t_User.BillState)as State, BillCity, BillZip from t_User where UserId='" + userid + "'";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable getUserShippingAddress2(string transid)
    {
        try
        {
            string query = @"select UserId, (ShipFname+' '+ShipLname)as Shippingname, ShipAddress, ShipNearby, (select ContryName from Country where CountryCode= t_User.ShipCountry) as Country, (select StateName from State where StateCode= t_User.ShipState)as State, ShipCity, ShipZip from t_User where Transid='" + transid + "'";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public DataTable getUserBillingAddress2(string transid)
    {
        try
        {
            string query = @"select UserId, (BillFname+' '+BillLname)as Billingname, BillAddress, BillNearby, (select ContryName from Country where CountryCode= t_User.BillCountry) as Country, (select StateName from State where StateCode= t_User.BillState)as State, BillCity, BillZip from t_User where Transid='" + transid + "'";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable getCountry()
    {
        try
        {
            string query = @"Select CountryID, CountryCode, ContryName from Country";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public DataTable getState()
    {
        try
        {
            string query = @"Select StateID, StateCode, StateName, CountryCode from State";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }


    public DataTable getUserIdetifyNumber(string userid)
    {
        try
        {
            string query = @"select UserIdentifyNo from t_User where UserId='" + userid + "'";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable getProductId(string transactionid)
    {
        try
        {
            string query = @"select ProductId from ShopingCart where TransId='" + transactionid + "'";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}