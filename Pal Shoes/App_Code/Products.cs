using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using SqlAccess;
using System.Data.SqlClient;
using System.Collections;

/// <summary>
/// Summary description for Products
/// </summary>
public class Products
{
    //home page
    public DataTable getNewProdtucts()
    {
        try
        {
            string query = @"Select top 4 t_Product.ProductId, Name, [Description], Price, convert(decimal(18,2), Price - (Price * Discount)/100)as discoutprice, [Image], Thumbnail, SKUCode, Discount FROM t_Product INNER JOIN t_ProductCategory ON t_Product.ProductId=t_ProductCategory.ProductId INNER JOIN t_Category ON t_ProductCategory.CategoryId= t_Category.CategoryId where t_Category.CategoryName !='ACCESSORIES' AND IsActive=1 AND Ptype=2 order by ProductId desc";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public DataTable getBestSellersProducts()
    {
        try
        {
            string query = @"Select top 4 t_Product.ProductId, Name, [Description], Price, convert(decimal(18,2), Price - (Price * Discount)/100)as discoutprice, [Image], Thumbnail, SKUCode, Discount FROM t_Product INNER JOIN t_ProductCategory ON t_Product.ProductId=t_ProductCategory.ProductId INNER JOIN t_Category ON t_ProductCategory.CategoryId= t_Category.CategoryId where t_Category.CategoryName !='ACCESSORIES' AND IsActive=1 AND Ptype=3 order by ProductId desc";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public DataTable getNewProducts()
    {
        try
        {
            string query = @"select top 4 t_Product.ProductId, Name, [Description], Price, convert(decimal(18,2), Price - (Price * Discount)/100)as discoutprice, [Image], Thumbnail, SKUCode, Discount FROM t_Product INNER JOIN t_ProductCategory ON t_Product.ProductId=t_ProductCategory.ProductId INNER JOIN t_Category ON t_ProductCategory.CategoryId= t_Category.CategoryId where t_Category.CategoryName !='ACCESSORIES' AND IsActive=1 AND Ptype=4 order by ProductId desc";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable getFeaturedProdtucts()
    {
        try
        {
            string query = @"select top 4 t_Product.ProductId, Name, [Description], Price, convert(decimal(18,2), Price - (Price * Discount)/100)as discoutprice, [Image], Thumbnail, SKUCode, Discount FROM t_Product INNER JOIN t_ProductCategory ON t_Product.ProductId=t_ProductCategory.ProductId INNER JOIN t_Category ON t_ProductCategory.CategoryId= t_Category.CategoryId where t_Category.CategoryName !='ACCESSORIES' AND IsActive=1 AND Ptype=4 order by ProductId desc";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    //Related Products
    public DataTable getRelatedProdtuctInPD(string productid)
    {
        try
        {
            string query = @"select top 12 p.ProductId, p.Name, p.[Description], p.Price, convert(decimal(18,2), p.Price - (p.Price * p.Discount)/100)as discoutprice, p.[Image], p.Thumbnail, p.SKUCode, p.Discount FROM t_Product p inner join t_ProductCategory pc on p.ProductId=pc.ProductId where p.IsActive=1 and pc.CategoryId in (select CategoryId from t_ProductCategory where ProductId='" + productid + "') and pc.SubCategoryId in(select SubCategoryId from t_ProductCategory where ProductId='" + productid + "')";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public DataTable getHomeProdtucts()
    {
        try
        {
            string query = "select top 9 ProductId, Name, [Description], Price, convert(decimal(18,2), Price - (Price * Discount)/100)as discoutprice, [Image], Thumbnail, SKUCode, Discount FROM t_Product order by ProductId desc";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable getProdtuctsBySubCategory(string subcategory)
    {
        try
        {
            string query = @"select t_Product.ProductId, Name, [Description], Price, [Image], Thumbnail, SKUCode, Discount 
            FROM t_Product INNER JOIN t_ProductCategory ON t_Product.ProductId=t_ProductCategory.ProductId 
            WHERE t_ProductCategory.SubCategoryId='" + subcategory + "' and t_Product.IsActive=1 order by t_Product.ProductId desc";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable getProdtuctsByCategory(string category)
    {
        try
        {
            string query = @"select p.ProductId, Name, [Description], Price, p.[Image], p.Thumbnail, SKUCode, Discount FROM t_Product p INNER JOIN t_ProductCategory pc ON p.ProductId=pc.ProductId where pc.CategoryId='" + category + "' and p.IsActive=1 order by p.ProductId desc";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }


    public DataTable getPageProdtuctByCategory(string category)
    {
        try
        {
            string query = @"select top 4 p.ProductId, Name, [Description], Price, p.[Image], p.Thumbnail, SKUCode, Discount FROM t_Product p INNER JOIN t_ProductCategory pc ON p.ProductId=pc.ProductId where pc.CategoryId='" + category + "' order by p.ProductId desc";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    //admin
    public DataTable getProdtucts()
    {
        try
        {
            string query = "select ProductId, Name, [Description], Price, [Image], Thumbnail, (case when IsActive='1' then 'Active' when IsActive='0' then 'Not Active' end)as Active, SKUCode, Discount, DateAdded, Brand, ShippingCharges, Capacity, Producttype FROM t_Product order by ProductId desc";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public DataTable getProdtuctsByCateogyides(string categoryid)
    {
        try
        {
            string query = @"select p.ProductId, Name, [Description], Price, [Image], Thumbnail, (case when IsActive='1' then 'Active' when IsActive='0' then 'Not Active' end)as Active, SKUCode, Discount, DateAdded, Brand, Modelno, Capacity, Producttype FROM t_Product p inner join t_ProductCategory pc on p.ProductId= pc.ProductId where pc.CategoryId='" + categoryid + "' order by ProductId desc";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable getSearchProductBySubCatgAndCategory(string category, string subcategory)
    {
        ArrayList Arr_fields_name = new ArrayList();
        ArrayList Arr_fields_value = new ArrayList();
        Arr_fields_name.Add("CategoryId");
        Arr_fields_name.Add("SubCategoryId");
        Arr_fields_value.Add(category);
        Arr_fields_value.Add(subcategory);
        
        string flag = "f";
        try
        {
            string search = @"select p.ProductId, Name, [Description], Price, [Image], Thumbnail, (case when IsActive='1' then 'Active' when IsActive='0' then 'Not Active' end)as Active, SKUCode, Discount, DateAdded, Brand, Modelno, Capacity, Producttype FROM t_Product p inner join t_ProductCategory pc on p.ProductId= pc.ProductId";

            if (Arr_fields_value.Count > 0)
            {
                for (int k = 0; k < Arr_fields_value.Count; k++)
                {
                    if (Convert.ToString(Arr_fields_value[k]) != "0" && Convert.ToString(Arr_fields_value[k]) != "")
                    {
                        if (flag == "f")
                        {
                            search += " where pc." + Arr_fields_name[k] + "='" + Arr_fields_value[k] + "'";
                            flag = "t";
                        }
                        else
                        {
                            search += " and pc." + Arr_fields_name[k] + "='" + Arr_fields_value[k] + "'";
                        }
                    }
                }
                search += " order by ProductId desc";
            }
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, search);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable getProdtuctsByProductId(string productid)
    {
        try
        {
            string query = @"Select Top 1  p.ProductId, Name, [Description], Price,convert(decimal(18,2), Price - (Price * Discount)/100)as discoutprice, [Image], Thumbnail, (case when IsActive=1 then 'Active' when IsActive=0 then 'Not Active' end)as Active, 
SKUCode, Discount, DateAdded, Brand, ShippingCharges, Capacity, Producttype, q.Quantity FROM t_Product p inner join t_Quantity q on p.ProductId= q.ProductId where p.ProductId='" + productid + "'";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    
    public int AddNewProduct(string producturl, string producttitle, string productkeywords, string productshortDes, string name, string variantnames, string variantquantity, decimal price, string image, string thumbnail, bool active, string skucode, Int32 discount, string date, Int32 subcategoryid, Int32 quantity, string brand, string shippingcharges, string capacity, string producttype, string categoryid, int types, string description)
    {
        try
        {
            int success = 0;
            success = MySqlDataAccess.ExecuteNonQuery(MySqlDataAccess.ConnectionString, CommandType.StoredProcedure, "ProAddProduct", new SqlParameter("@Producturl", producturl), new SqlParameter("@Producttitle", producttitle), new SqlParameter("@Productkeywords", productkeywords), new SqlParameter("@ProductshortDes", productshortDes), new SqlParameter("@Name", name), new SqlParameter("@Variantnames", variantnames) ,  new SqlParameter("@Variantquantity", variantquantity), new SqlParameter("@Price", price), new SqlParameter("@Image", image), new SqlParameter("@Thumbnail", thumbnail), new SqlParameter("@IsActive", active), new SqlParameter("@SKUCode", skucode), new SqlParameter("@Discount", discount), new SqlParameter("@DateAdded", date), new SqlParameter("@SubCategoryId", subcategoryid), new SqlParameter("@Quantity", quantity), new SqlParameter("@Brand", brand), new SqlParameter("@Shippingcharges", shippingcharges), new SqlParameter("@Capacity", capacity), new SqlParameter("@Producttype", producttype), new SqlParameter("@CategoryId", categoryid), new SqlParameter("@ptype", types), new SqlParameter("@Description", description));
            return success;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable getProductByProductId(string Productid)
    {
        try
        {
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.StoredProcedure, "ProGetProductById", new SqlParameter("@ProductId", Productid));
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    
    public DataTable getProductDescriptionById(string Productid)
    {
        try
        {
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.StoredProcedure, "ProGetProductDescriptionById", new SqlParameter("@ProductId", Productid));
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable getProductDescriptionImg(string productid)
    {
        try
        {
            string query = @"Select ProductId, Thumbnail, Image, BackImage, LeftImage, RightImage FROM t_Product where ProductId='" + productid + "'";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public DataTable getVariantforDropd(string productid)
    {
        try
        {
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, "select * from t_Quantity where ProductId='"+ productid + "'");
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
  
    public DataTable getVquantityByVnamee(string productid, string variantname)
    {
        try
        {
            string query = @"Select QuantityId, Quantity, ProductId, Variantname, VariantQuantity FROM t_Quantity where ProductId='" + productid + "' And Variantname='"+ variantname + "'";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    
    public int UpdateNewProduct(string productid, string producturl, string producttitle, string productkeywords, string productshortDes, string name, string description, decimal price, string variantnames, string variantquantity, string image, string thumbnail, bool active, string skucode, Int32 discount, string date, Int32 quantity, string brand, string shippingcharges, string capacity, string producttype, Int32 subcategoryid, string categoryid, int types)
    {
        try
        {
            int success = 0;
            success = MySqlDataAccess.ExecuteNonQuery(MySqlDataAccess.ConnectionString, CommandType.StoredProcedure, "ProUpdateProduct", new SqlParameter("@ProductId", productid), new SqlParameter("@Producturl", producturl), new SqlParameter("@Producttitle", producttitle), new SqlParameter("@Productkeywords", productkeywords), new SqlParameter("@ProductshortDes", productshortDes), new SqlParameter("@Name", name), new SqlParameter("@Description", description), new SqlParameter("@Price", price), new SqlParameter("@Variantnames", variantnames), new SqlParameter("@Variantquantity", variantquantity), new SqlParameter("@Image", image), new SqlParameter("@Thumbnail", thumbnail), new SqlParameter("@IsActive", active), new SqlParameter("@SKUCode", skucode), new SqlParameter("@Discount", discount), new SqlParameter("@DateAdded", date), new SqlParameter("@Quantity", quantity), new SqlParameter("@Brand", brand), new SqlParameter("@Shippingcharges", shippingcharges), new SqlParameter("@Capacity", capacity), new SqlParameter("@Producttype", producttype), new SqlParameter("@Subcategoryid", subcategoryid), new SqlParameter("@CategoryId", categoryid), new SqlParameter("@ptype", types));
            return success;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }


    public int DeleteProduct(int Productid)
    {
        try
        {
            int result;
            result = MySqlDataAccess.ExecuteNonQuery(MySqlDataAccess.ConnectionString, CommandType.StoredProcedure, "ProDeleteProduct", new SqlParameter("@ProductId", Productid));
            return result;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

   
//    public DataTable getProductDescriptionImg11(string productid)
//    {
//        try
//        {
//            string query = @"select top 6 p.ProductId, p.Thumbnail FROM t_Product p inner join t_ProductCategory pc
//on p.ProductId=pc.ProductId where pc.CategoryId in(select CategoryId from t_ProductCategory where ProductId='"+ productid+"') and pc.SubCategoryId in(select SubCategoryId from t_ProductCategory where ProductId='"+ productid +"')";
//            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
//        }
//        catch (Exception ex)
//        {
//            throw new Exception(ex.Message);
//        }
//    }

    public DataTable getDefaultProducts()
    {
        try
        {
            string query = @"SELECT ProductId, Name, [Description], Price, [Image], Thumbnail, SKUCode, Discount, Capacity FROM t_Product order by ProductId DESC";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable getDefaultProductsOffer(string pageNumber, out int howManyPages)
    {
        try
        {
            string conn = MySqlDataAccess.ConnectionString;
            SqlConnection con = new SqlConnection(conn);
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "GetProuctsOnDefaultOffer";
            cmd.Connection = con;
            SqlParameter param = new SqlParameter();

            param = cmd.CreateParameter();
            param.ParameterName = "@PageNumber";
            param.Value = pageNumber;
            param.DbType = DbType.Int32;
            cmd.Parameters.Add(param);

            param = cmd.CreateParameter();
            param.ParameterName = "@ProductsPerPage";
            param.Value = Configuration.ProductsPerPage;
            param.DbType = DbType.Int32;
            cmd.Parameters.Add(param);

            param = cmd.CreateParameter();
            param.ParameterName = "@HowManyProducts";
            param.Direction = ParameterDirection.Output;
            param.DbType = DbType.Int32;
            cmd.Parameters.Add(param);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable table = new DataTable();
            da.Fill(table);

            int howManyProducts = Int32.Parse(cmd.Parameters["@HowManyProducts"].Value.ToString());
            howManyPages = (int)Math.Ceiling((double)howManyProducts / (double)Configuration.ProductsPerPage);

            return table;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public DataTable getProductsOnCategory7(string CategoryId, string pageNumber, out int howManyPages)
    {
        try
        {
            string conn = MySqlDataAccess.ConnectionString;
            SqlConnection con = new SqlConnection(conn);
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "GetProuctsOnCategory";
            cmd.Connection = con;
            SqlParameter param = new SqlParameter();

            param = cmd.CreateParameter();
            param.ParameterName = "@CategoryId";
            param.Value = CategoryId;
            param.DbType = DbType.Int32;
            cmd.Parameters.Add(param);

            param = cmd.CreateParameter();
            param.ParameterName = "@PageNumber";
            param.Value = pageNumber;
            param.DbType = DbType.Int32;
            cmd.Parameters.Add(param);

            param = cmd.CreateParameter();
            param.ParameterName = "@ProductsPerPage";
            param.Value = Configuration.ProductsPerPage;
            param.DbType = DbType.Int32;
            cmd.Parameters.Add(param);

            param = cmd.CreateParameter();
            param.ParameterName = "@HowManyProducts";
            param.Direction = ParameterDirection.Output;
            param.DbType = DbType.Int32;
            cmd.Parameters.Add(param);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable table = new DataTable();
            da.Fill(table);

            int howManyProducts = Int32.Parse(cmd.Parameters["@HowManyProducts"].Value.ToString());
            howManyPages = (int)Math.Ceiling((double)howManyProducts / (double)Configuration.ProductsPerPage);

            return table;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public DataTable getProductsOnSubCategory7(string SubcategoryId, string pageNumber, out int howManyPages)
    {
        try
        {
            string conn = MySqlDataAccess.ConnectionString;
            SqlConnection con = new SqlConnection(conn);
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "GetProuctsOnSubCategory";
            cmd.Connection = con;
            SqlParameter param = new SqlParameter();

            param = cmd.CreateParameter();
            param.ParameterName = "@SubCategoryId";
            param.Value = SubcategoryId;
            param.DbType = DbType.Int32;
            cmd.Parameters.Add(param);

            param = cmd.CreateParameter();
            param.ParameterName = "@PageNumber";
            param.Value = pageNumber;
            param.DbType = DbType.Int32;
            cmd.Parameters.Add(param);

            param = cmd.CreateParameter();
            param.ParameterName = "@ProductsPerPage";
            param.Value = Configuration.ProductsPerPage;
            param.DbType = DbType.Int32;
            cmd.Parameters.Add(param);

            param = cmd.CreateParameter();
            param.ParameterName = "@HowManyProducts";
            param.Direction = ParameterDirection.Output;
            param.DbType = DbType.Int32;
            cmd.Parameters.Add(param);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable table = new DataTable();
            da.Fill(table);

            int howManyProducts = Int32.Parse(cmd.Parameters["@HowManyProducts"].Value.ToString());
            howManyPages = (int)Math.Ceiling((double)howManyProducts / (double)Configuration.ProductsPerPage);

            return table;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable getAtoZProducts5(string pageNumber, out int howManyPages)
    {
        try
        {
            string conn = MySqlDataAccess.ConnectionString;
            SqlConnection con = new SqlConnection(conn);
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "GetProuctsOnAtoZ";
            cmd.Connection = con;
            SqlParameter param = new SqlParameter();

            param = cmd.CreateParameter();
            param.ParameterName = "@PageNumber";
            param.Value = pageNumber;
            param.DbType = DbType.Int32;
            cmd.Parameters.Add(param);

            param = cmd.CreateParameter();
            param.ParameterName = "@ProductsPerPage";
            param.Value = Configuration.ProductsPerPage;
            param.DbType = DbType.Int32;
            cmd.Parameters.Add(param);

            param = cmd.CreateParameter();
            param.ParameterName = "@HowManyProducts";
            param.Direction = ParameterDirection.Output;
            param.DbType = DbType.Int32;
            cmd.Parameters.Add(param);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable table = new DataTable();
            da.Fill(table);

            int howManyProducts = Int32.Parse(cmd.Parameters["@HowManyProducts"].Value.ToString());
            howManyPages = (int)Math.Ceiling((double)howManyProducts / (double)Configuration.ProductsPerPage);

            return table;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public DataTable getZtoAProducts5(string pageNumber, out int howManyPages)
    {
        try
        {
            string conn = MySqlDataAccess.ConnectionString;
            SqlConnection con = new SqlConnection(conn);
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "GetProuctsOnZtoA";
            cmd.Connection = con;
            SqlParameter param = new SqlParameter();

            param = cmd.CreateParameter();
            param.ParameterName = "@PageNumber";
            param.Value = pageNumber;
            param.DbType = DbType.Int32;
            cmd.Parameters.Add(param);

            param = cmd.CreateParameter();
            param.ParameterName = "@ProductsPerPage";
            param.Value = Configuration.ProductsPerPage;
            param.DbType = DbType.Int32;
            cmd.Parameters.Add(param);

            param = cmd.CreateParameter();
            param.ParameterName = "@HowManyProducts";
            param.Direction = ParameterDirection.Output;
            param.DbType = DbType.Int32;
            cmd.Parameters.Add(param);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable table = new DataTable();
            da.Fill(table);

            int howManyProducts = Int32.Parse(cmd.Parameters["@HowManyProducts"].Value.ToString());
            howManyPages = (int)Math.Ceiling((double)howManyProducts / (double)Configuration.ProductsPerPage);

            return table;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public DataTable getLowToHighProducts5(string pageNumber, out int howManyPages)
    {
        try
        {
            string conn = MySqlDataAccess.ConnectionString;
            SqlConnection con = new SqlConnection(conn);
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "GetProuctsOnLowtoHigh";
            cmd.Connection = con;
            SqlParameter param = new SqlParameter();

            param = cmd.CreateParameter();
            param.ParameterName = "@PageNumber";
            param.Value = pageNumber;
            param.DbType = DbType.Int32;
            cmd.Parameters.Add(param);

            param = cmd.CreateParameter();
            param.ParameterName = "@ProductsPerPage";
            param.Value = Configuration.ProductsPerPage;
            param.DbType = DbType.Int32;
            cmd.Parameters.Add(param);

            param = cmd.CreateParameter();
            param.ParameterName = "@HowManyProducts";
            param.Direction = ParameterDirection.Output;
            param.DbType = DbType.Int32;
            cmd.Parameters.Add(param);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable table = new DataTable();
            da.Fill(table);

            int howManyProducts = Int32.Parse(cmd.Parameters["@HowManyProducts"].Value.ToString());
            howManyPages = (int)Math.Ceiling((double)howManyProducts / (double)Configuration.ProductsPerPage);

            return table;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public DataTable getHighToLowProducts5(string pageNumber, out int howManyPages)
    {
        try
        {
            string conn = MySqlDataAccess.ConnectionString;
            SqlConnection con = new SqlConnection(conn);
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "GetProuctsOnHighToLow";
            cmd.Connection = con;
            SqlParameter param = new SqlParameter();

            param = cmd.CreateParameter();
            param.ParameterName = "@PageNumber";
            param.Value = pageNumber;
            param.DbType = DbType.Int32;
            cmd.Parameters.Add(param);

            param = cmd.CreateParameter();
            param.ParameterName = "@ProductsPerPage";
            param.Value = Configuration.ProductsPerPage;
            param.DbType = DbType.Int32;
            cmd.Parameters.Add(param);

            param = cmd.CreateParameter();
            param.ParameterName = "@HowManyProducts";
            param.Direction = ParameterDirection.Output;
            param.DbType = DbType.Int32;
            cmd.Parameters.Add(param);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable table = new DataTable();
            da.Fill(table);

            int howManyProducts = Int32.Parse(cmd.Parameters["@HowManyProducts"].Value.ToString());
            howManyPages = (int)Math.Ceiling((double)howManyProducts / (double)Configuration.ProductsPerPage);

            return table;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable getHighToLowByCategoryId(string categoryid, string pageNumber, out int howManyPages)
    {
        try
        {
            string conn = MySqlDataAccess.ConnectionString;
            SqlConnection con = new SqlConnection(conn);
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "GetProuctsOnHighToLowByCategory";
            cmd.Connection = con;
            SqlParameter param = new SqlParameter();

            param = cmd.CreateParameter();
            param.ParameterName = "@PageNumber";
            param.Value = pageNumber;
            param.DbType = DbType.Int32;
            cmd.Parameters.Add(param);

            param = cmd.CreateParameter();
            param.ParameterName = "@CategoryId";
            param.Value = categoryid;
            param.DbType = DbType.Int32;
            cmd.Parameters.Add(param);

            param = cmd.CreateParameter();
            param.ParameterName = "@ProductsPerPage";
            param.Value = Configuration.ProductsPerPage;
            param.DbType = DbType.Int32;
            cmd.Parameters.Add(param);

            param = cmd.CreateParameter();
            param.ParameterName = "@HowManyProducts";
            param.Direction = ParameterDirection.Output;
            param.DbType = DbType.Int32;
            cmd.Parameters.Add(param);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable table = new DataTable();
            da.Fill(table);

            int howManyProducts = Int32.Parse(cmd.Parameters["@HowManyProducts"].Value.ToString());
            howManyPages = (int)Math.Ceiling((double)howManyProducts / (double)Configuration.ProductsPerPage);

            return table;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable getLowToHighByCategoryId(string categoryid, string pageNumber, out int howManyPages)
    {
        try
        {
            string conn = MySqlDataAccess.ConnectionString;
            SqlConnection con = new SqlConnection(conn);
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "GetProuctsOnLowtoHighByCategory";
            cmd.Connection = con;
            SqlParameter param = new SqlParameter();

            param = cmd.CreateParameter();
            param.ParameterName = "@PageNumber";
            param.Value = pageNumber;
            param.DbType = DbType.Int32;
            cmd.Parameters.Add(param);

            param = cmd.CreateParameter();
            param.ParameterName = "@CategoryId";
            param.Value = categoryid;
            param.DbType = DbType.Int32;
            cmd.Parameters.Add(param);

            param = cmd.CreateParameter();
            param.ParameterName = "@ProductsPerPage";
            param.Value = Configuration.ProductsPerPage;
            param.DbType = DbType.Int32;
            cmd.Parameters.Add(param);

            param = cmd.CreateParameter();
            param.ParameterName = "@HowManyProducts";
            param.Direction = ParameterDirection.Output;
            param.DbType = DbType.Int32;
            cmd.Parameters.Add(param);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable table = new DataTable();
            da.Fill(table);

            int howManyProducts = Int32.Parse(cmd.Parameters["@HowManyProducts"].Value.ToString());
            howManyPages = (int)Math.Ceiling((double)howManyProducts / (double)Configuration.ProductsPerPage);

            return table;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }


    public DataTable getViewAllProducts(string productalltype, string pageNumber, out int howManyPages)
    {
        try
        {
            string conn = MySqlDataAccess.ConnectionString;
            SqlConnection con = new SqlConnection(conn);
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "GetProuctsAllNewProducts";
            cmd.Connection = con;
            SqlParameter param = new SqlParameter();

            param = cmd.CreateParameter();
            param.ParameterName = "@Ptype";
            param.Value = productalltype;
            param.DbType = DbType.Int32;
            cmd.Parameters.Add(param);

            param = cmd.CreateParameter();
            param.ParameterName = "@PageNumber";
            param.Value = pageNumber;
            param.DbType = DbType.Int32;
            cmd.Parameters.Add(param);

            param = cmd.CreateParameter();
            param.ParameterName = "@ProductsPerPage";
            param.Value = Configuration.ProductsPerPage;
            param.DbType = DbType.Int32;
            cmd.Parameters.Add(param);

            param = cmd.CreateParameter();
            param.ParameterName = "@HowManyProducts";
            param.Direction = ParameterDirection.Output;
            param.DbType = DbType.Int32;
            cmd.Parameters.Add(param);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable table = new DataTable();
            da.Fill(table);

            int howManyProducts = Int32.Parse(cmd.Parameters["@HowManyProducts"].Value.ToString());
            howManyPages = (int)Math.Ceiling((double)howManyProducts / (double)Configuration.ProductsPerPage);

            return table;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    

    public DataTable getProductsLowToHigh(string compareid)
    {
        try
        {
            string query = @"select p.ProductId, Name, [Description], Price, convert(decimal(18,2), Price - (Price * Discount)/100)as discoutprice, [Image], Thumbnail, SKUCode, Discount FROM t_Product p inner join CompareProduct cp on p.ProductId= cp.ProductId where cp.ComSession='" + compareid + "' order by discoutprice ASC";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public DataTable getProductsHighToLow(string compareid)
    {
        try
        {
            string query = @"select p.ProductId, Name, [Description], Price, convert(decimal(18,2), Price - (Price * Discount)/100)as discoutprice, [Image], Thumbnail, SKUCode, Discount FROM t_Product p inner join CompareProduct cp on p.ProductId= cp.ProductId where cp.ComSession='" + compareid + "' order by discoutprice DESC";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    //Product Category
    public DataTable getProductCategory()
    {
        try
        {
            string query = @"SELECT c.CategoryID, c.CategoryName, count(p.ProductId) as product FROM t_Category c inner JOIN t_ProductCategory p ON c.CategoryId=p.CategoryId GROUP BY c.CategoryID, c.CategoryName";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    //Flavour
    public DataTable getProductFlavour()
    {
        try
        {
            string query = "SELECT Producttype, (select COUNT(ProductId)) as product from t_Product group by Producttype";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public DataTable getProductByFlavour(string flavour, string pageNumber, out int howManyPages)
    {
        try
        {
            string conn = MySqlDataAccess.ConnectionString;
            SqlConnection con = new SqlConnection(conn);
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "GetProuctsOnFlavour";
            cmd.Connection = con;
            SqlParameter param = new SqlParameter();

            param = cmd.CreateParameter();
            param.ParameterName = "@Producttype";
            param.Value = flavour;
            param.DbType = DbType.String;
            cmd.Parameters.Add(param);

            param = cmd.CreateParameter();
            param.ParameterName = "@PageNumber";
            param.Value = pageNumber;
            param.DbType = DbType.Int32;
            cmd.Parameters.Add(param);

            param = cmd.CreateParameter();
            param.ParameterName = "@ProductsPerPage";
            param.Value = Configuration.ProductsPerPage;
            param.DbType = DbType.Int32;
            cmd.Parameters.Add(param);

            param = cmd.CreateParameter();
            param.ParameterName = "@HowManyProducts";
            param.Direction = ParameterDirection.Output;
            param.DbType = DbType.Int32;
            cmd.Parameters.Add(param);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable table = new DataTable();
            da.Fill(table);

            int howManyProducts = Int32.Parse(cmd.Parameters["@HowManyProducts"].Value.ToString());
            howManyPages = (int)Math.Ceiling((double)howManyProducts / (double)Configuration.ProductsPerPage);

            return table;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    //Capacity
    public DataTable getProductToBindCapacity()
    {
        try
        {
            string query = "SELECT Capacity, (select COUNT(ProductId)) as product from t_Product group by Capacity";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public DataTable getProductByCapacity(string capacity)
    {
        try
        {
            string query = "select ProductId, Name, [Description], Price, [Image], Thumbnail, SKUCode, Discount, Capacity FROM t_Product WHERE Capacity='" + capacity + "'";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }


    //price base
    public DataTable getProductOnPriceBasis(string price1, string price2)
    {
        try
        {
            string query = @"select top 9 ProductId, Name, [Description], Price, [Image], Thumbnail, SKUCode, Discount FROM t_Product WHERE Price BETWEEN '" + price1 + "' AND '" + price2 + "' order by ProductId desc";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable getProductByPrice(string price)
    {
        try
        {
            string query = @"select ProductId, Name, [Description], Price, [Image], Thumbnail, SKUCode, Discount, Capacity FROM t_Product WHERE Price between '" + price + "' and ('" + price + "'+ 1000)";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    
    //Product Brand
    public DataTable getBrandProducts()
    {
        try
        {
            string query = "select top 100 Brand, COUNT(productid)as product from t_Product group by Brand";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public DataTable getProductByBrand(string brand, string pageNumber, out int howManyPages)
    {
        try
        {
            string conn = MySqlDataAccess.ConnectionString;
            SqlConnection con = new SqlConnection(conn);
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "GetProuctsOnBrand";
            cmd.Connection = con;
            SqlParameter param = new SqlParameter();

            param = cmd.CreateParameter();
            param.ParameterName = "@Brand";
            param.Value = brand;
            param.DbType = DbType.String;
            cmd.Parameters.Add(param);

            param = cmd.CreateParameter();
            param.ParameterName = "@PageNumber";
            param.Value = pageNumber;
            param.DbType = DbType.Int32;
            cmd.Parameters.Add(param);

            param = cmd.CreateParameter();
            param.ParameterName = "@ProductsPerPage";
            param.Value = Configuration.ProductsPerPage;
            param.DbType = DbType.Int32;
            cmd.Parameters.Add(param);

            param = cmd.CreateParameter();
            param.ParameterName = "@HowManyProducts";
            param.Direction = ParameterDirection.Output;
            param.DbType = DbType.Int32;
            cmd.Parameters.Add(param);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable table = new DataTable();
            da.Fill(table);

            int howManyProducts = Int32.Parse(cmd.Parameters["@HowManyProducts"].Value.ToString());
            howManyPages = (int)Math.Ceiling((double)howManyProducts / (double)Configuration.ProductsPerPage);

            return table;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public string IsExistsSKUCode(string skucode, string Update, string productid)
    {
        string FLAG = "F";
        DataTable dt = new DataTable();
        if (Update == "N")
            dt = MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, "select ProductId, SKUCode from t_Product where SKUCode='" + skucode + "'");
        else
            dt = MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, "select ProductId, SKUCode from t_Product where SKUCode='" + skucode + "' AND ProductId !='" + productid + "'");

        if (dt.Rows.Count > 0)
        {
            FLAG = "T";
        }
        return FLAG;
    }


    public DataTable searchProducts(string productname)
    {
        try
        {
            string query = @"select ProductId, Name, [Description], Price, [Image], Thumbnail, IsActive, SKUCode, Discount, DateAdded, Brand, Modelno, Capacity, Producttype, Warranty FROM t_Product where Name like '" + productname + "%' OR Name ='" + productname + "' OR Name like '%" + productname + "%'";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    
    //Recommended Product
    public DataTable getRecommendedProduct1()
    {
        try
        {
            string query = @"select top 3 ProductId, Name, [Description], Price, [Image], Thumbnail, SKUCode, 
Discount, Modelno, Capacity FROM t_Product";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public DataTable getRecommendedProduct2()
    {
        try
        {
            string query = @"select top 3 ProductId, Name, [Description], Price, [Image], Thumbnail, SKUCode, 
Discount, Modelno, Capacity FROM t_Product order by ProductId desc";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable getCategorySubCategoryOnProduct(string productid)
    {
        try
        {
            string query = "select CategoryId, SubcategoryId from t_ProductCategory where ProductId='"+productid+"'";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch(Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable getRelatedImages(string categoryid, string subcategoryid)
    {
        try
        {
            string query = @"select top 3 p.ProductId, p.Name, p.[Image], p.Thumbnail FROM t_Product p inner join t_ProductCategory pc
on p.ProductId=pc.ProductId where pc.CategoryId='" + categoryid + "' and pc.SubCategoryId='" + subcategoryid + "' order by ProductId desc";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    

    //Tag Product
    public DataTable getTagProducts()
    {
        try
        {
            string query = @"select top 4 ProductId, Name, Price, [Image], Thumbnail, SKUCode, Discount, Modelno, Capacity FROM t_Product";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    
    public int AddUserRating(int rating)
    {
        try
        {
            string query = "insert into t_Rating (Rate) values('" + rating + "')";
            int intvalue = MySqlDataAccess.ExecuteNonQuery(MySqlDataAccess.ConnectionString, CommandType.Text, query);
            return intvalue;
        }
        catch(Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public DataTable getRating()
    {
        try
        {
            string query = "select Id, Rate from t_Rating";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
            
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public int InsertReview(Int32 productid, int rating, string name, string email, string review)
    {
        try
        {
            string query = "insert into t_RatingDetail (ProductId,Rating,Name,Emailid,Review) values('" + productid + "','" + rating + "','" + name + "','" + email + "','" + review + "')";
            int intvalue = MySqlDataAccess.ExecuteNonQuery(MySqlDataAccess.ConnectionString, CommandType.Text, query);
            return intvalue;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }


    //Compare Product
    public int InsertCompareProduct(Int32 productid, string comsession)
    {
        try
        {
            string query = "insert into CompareProduct (ProductId, ComSession) values('" + productid + "','" + comsession + "')";
            int intvalue = MySqlDataAccess.ExecuteNonQuery(MySqlDataAccess.ConnectionString, CommandType.Text, query);
            return intvalue;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public string IfCompareProductExists(Int32 productid, string comsession)
    {
        string FLAG = "F";
        DataTable dt = new DataTable();
        string query = "select ProductId, ComSession from CompareProduct where ProductId='" + productid + "' AND ComSession='" + comsession + "'";
            dt = MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        if (dt.Rows.Count > 0)
        {
            FLAG = "T";
        }
        return FLAG;
    }

    public DataTable getCompareProducts(string comsession)
    {
        try
        {
            string query = @"select top 4 p.ProductId, Name, CASE WHEN LEN(Description) <= 500 THEN Description ELSE SUBSTRING(Description, 1, 500) + '...' END AS Description, Price, convert(decimal(18,2), Price - (Price * Discount)/100)as discoutprice, p.[Image], p.Thumbnail, SKUCode, Discount, Brand, Modelno, Capacity, Producttype FROM t_Product p INNER JOIN CompareProduct cp ON p.ProductId=cp.ProductId where cp.ComSession='" + comsession + "'";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch(Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public static string DeleteRecord(string tableName, string columnName, string identityNumber)
    {
        string result = "";
        try
        {
            MySqlDataAccess.ExecuteNonQuery(MySqlDataAccess.ConnectionString, CommandType.Text, "delete from " + tableName + " where " + columnName + "='" + identityNumber + "'");
            result = "Success";
        }
        catch
        {
            result = "Fail";
        }
        return result;
    }

    public DataTable getSlideProduct(string productid)
    {
        try
        {
            string query = @"select top 12 p.ProductId, p.Name, p.[Description], p.Price, convert(decimal(18,2), p.Price - (p.Price * p.Discount)/100)as discoutprice, p.[Image], p.Thumbnail, p.SKUCode, p.Discount FROM t_Product p inner join t_ProductCategory pc
on p.ProductId=pc.ProductId where p.IsActive=1 and pc.CategoryId in (select CategoryId from t_ProductCategory where ProductId='" + productid + "') and pc.SubCategoryId in(select SubCategoryId from t_ProductCategory where ProductId='" + productid + "')";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    
    public int AddOtherProductImage(string productid, string bakimage, string leftimage, string rightimage)
    {
        try
        {
            string query = "update t_Product set BackImage='" + bakimage + "', LeftImage='" + leftimage + "', RightImage='" + rightimage + "' where ProductId='" + productid + "'";
            int intvalue = MySqlDataAccess.ExecuteNonQuery(MySqlDataAccess.ConnectionString, CommandType.Text, query);
            return intvalue;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    

    //Flavour
    public int AddNewFlavour(string flavourname)
    {
        try
        {
            return MySqlDataAccess.ExecuteNonQuery(MySqlDataAccess.ConnectionString, CommandType.Text, "INSERT INTO Flavour (FlavourName) VALUES ('" + flavourname + "')");
            
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public string IsFlavourExists(string flavour, string Update, string id)
    {
        try
        {
            string FLAG = "F";
            DataTable dt = new DataTable();
            if (Update == "N")
                dt = MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, "select FlavourName from Flavour where FlavourName='" + flavour + "'");
            else
                dt = MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, "select FlavourName from Flavour where FlavourName='" + flavour + "' AND FlavourID !='" + id + "'");

            if (dt.Rows.Count > 0)
            {
                FLAG = "T";
            }
            return FLAG;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public DataTable getFlavourname()
    {
        try
        {
            string query = @"select FlavourID, FlavourName from Flavour";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable getToCheckProductInStock(string productid, string flavourid)
    {
        try  
        {
            string query = @"select Quantity from t_Quantity q inner join t_Product p on q.ProductId=p.ProductId where p.ProductId='" + productid + "' and q.FlavourID='" + flavourid + "'";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable GetTotalVquantityByVname(Int32 productid)
    {
        try
        {
            string query = @"Select Sum(Quantity)as proquantity from t_Quantity where ProductId='" + productid + "'";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }


    //Search record
    public DataTable getSearchRecord(string name, string brand, string price, string modelno, string capacity, string producttype)
    {
        ArrayList Arr_fields_name = new ArrayList();
        ArrayList Arr_fields_value = new ArrayList();
        Arr_fields_name.Add("Name");
        Arr_fields_name.Add("Price");
        Arr_fields_name.Add("Brand");
        Arr_fields_name.Add("Modelno");
        Arr_fields_name.Add("Capacity");
        Arr_fields_name.Add("Producttype");
        Arr_fields_value.Add(name);
        Arr_fields_value.Add(price);
        Arr_fields_value.Add(brand);
        Arr_fields_value.Add(modelno);
        Arr_fields_value.Add(capacity);
        Arr_fields_value.Add(producttype);

        string flag = "f";
        try
        {
            string search = @"select top 20 ProductId, Name, [Description], Price, convert(decimal(18,2), Price - (Price * Discount)/100)as discoutprice, [Image], Thumbnail, SKUCode, Discount, Brand, Modelno, Capacity, Producttype FROM t_Product";

            if (Arr_fields_value.Count > 0)
            {
                for (int k = 0; k < Arr_fields_value.Count; k++)
                {
                    if (Convert.ToString(Arr_fields_value[k]) != "0" && Convert.ToString(Arr_fields_value[k]) != "")
                    {
                        if (flag == "f")
                        {
                            if (k == 0 || k == 1 || k == 2 || k == 4 || k == 5)
                            {
                                search += " where " + Arr_fields_name[k] + " like '" + Arr_fields_value[k] + "%'";
                                flag = "t";
                            }
                            else
                            {
                                search += "  where " + Arr_fields_name[k] + "='" + Arr_fields_value[k] + "'";
                                flag = "t";
                            }
                        }
                        else
                        {
                            if (k == 0 || k == 1 || k == 2 || k == 4 || k == 5)
                            {
                                search += "  or " + Arr_fields_name[k] + " like '" + Arr_fields_value[k] + "%'";
                            }
                            else
                            {
                                search += "  or " + Arr_fields_name[k] + " ='" + Arr_fields_value[k] + "'";
                            }
                        }
                    }
                }
            }
            //search += " order by ProductId desc";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, search);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }


    public DataTable getDataToMail(string productid)
    {
        try
        {
            string query = @"SELECT ProductId, Name, Brand, Capacity, (SELECT FlavourName FROM Flavour WHERE FlavourID=t_Product.Producttype)as flavour FROM t_Product WHERE ProductId='" + productid + "'";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public int AddProductReview(string productid, int rating, string name, string emailid, string review, bool active)
    {
        try
        {
            string query = "INSERT INTO t_RatingDetail(ProductId,Rating,Name,Emailid,Review, active, ReviewDate) VALUES ('" + productid + "','" + rating + "','" + name + "','" + emailid + "','" + review + "', '" + active + "', GETDATE())";
            return MySqlDataAccess.ExecuteNonQuery(MySqlDataAccess.ConnectionString, CommandType.Text, query);

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable getProductReviewDisplay(string productid)
    {
        try
        {
            string query = @"SELECT ProductId,Rating,Name,Emailid,Review, active, ReviewDate FROM t_RatingDetail WHERE ProductId='" + productid + "'";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public DataTable getProductReviewCount(string productid)
    {
        try
        {
            string query = @"SELECT count(RatingId)as comment FROM t_RatingDetail WHERE ProductId='" + productid + "'";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }


    public int AddPostcodeDelivery(string country, string state, string postcode)
    {
        try
        {
            string query = "INSERT INTO CheckDelivery(Countryid,Stateid,Postcode) VALUES ('" + country + "','" + state + "','" + postcode + "')";
            return MySqlDataAccess.ExecuteNonQuery(MySqlDataAccess.ConnectionString, CommandType.Text, query);

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public string IsExistsPostCode(string potalcode, string Update, string id)
    {
        string FLAG = "F";
        DataTable dt = new DataTable();
        if (Update == "N")
            dt = MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, "select Postcode from CheckDelivery where Postcode='" + potalcode + "'");
        else
            dt = MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, "select Postcode from CheckDelivery where Postcode='" + potalcode + "' AND DeliveryId !='" + id + "'");

        if (dt.Rows.Count > 0)
        {
            FLAG = "T";
        }
        return FLAG;
    }

    public DataTable getDeliveryPostcode()
    {
        try
        {
            string query = @"SELECT DeliveryId, (select ContryName from Country where CountryCode= CheckDelivery.Countryid) 
as Country, Countryid, (select StateName from State where StateCode= CheckDelivery.Stateid) as State, Stateid, Postcode FROM CheckDelivery order by Countryid DESC";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public string IfDeliveryExists(string potalcode, string state, string country)
    {
        string FLAG = "F";
        DataTable dt = new DataTable();
        dt = MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, "select Postcode from CheckDelivery where Postcode='" + potalcode + "' and Stateid='" + state + "' and Countryid='" + country + "'");
        if (dt.Rows.Count > 0)
        {
            FLAG = "T";
        }
        return FLAG;
    }

    public DataTable getBackImages(string productid)
    {
        try
        {
            string query = @"SELECT ProductId, BackImage, LeftImage, RightImage from t_Product where ProductId='" + productid + "'";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable geCompareID(string comsessionid, string productid)
    {
        try
        {
            string query = @"SELECT CompareId from CompareProduct where ComSession='"+comsessionid+"' and ProductId='" + productid + "'";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public int RemoveCompare(Int32 compareid)
    {
        try
        {
            string query = "Delete from CompareProduct where CompareId='" + compareid + "'";
            return MySqlDataAccess.ExecuteNonQuery(MySqlDataAccess.ConnectionString, CommandType.Text, query);

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }


    public int AddContactSendMail(string contactname, string contactnum, string emailid, string message)
    {
        try
        {
            string query = "insert into Contact(Contactname,Contactnumber,emailid,messages) values('" + contactname + "','" + contactnum + "','" + emailid + "','" + message + "')";
            return MySqlDataAccess.ExecuteNonQuery(MySqlDataAccess.ConnectionString, CommandType.Text, query);

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }


    public DataTable getProductByProductType(string producttype)
    {
        try
        {
            string query = @"SELECT * from t_Product where Ptype='" + producttype + "'";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }



    public DataTable getProductNavName(string categoryid)
    {
        try
        {
            string query = @"select CategoryName from t_Category where CategoryId='" + categoryid + "'";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public DataTable getProductNavName2(string subcategoryid)
    {
        try
        {
            string query = @"select SubName,  (select CategoryName from t_Category where CategoryId=t_SubCategory.CategoryId)as category from t_SubCategory where SubCategoryId='" + subcategoryid + "'";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    //Coupon Add

    public int AddNewCoupon(string couponStatus, string couponcode, int discountcopon, decimal price, bool active)
    {
        try
        {
            string query = @"Insert into Coupon(couponstatus, couponcode, discount, price, couponactive, CouponDate) values('"+ couponStatus + "','" + couponcode + "','" + discountcopon + "','" + price + "','" + active + "',GETDATE())";
            int intvalue = MySqlDataAccess.ExecuteNonQuery(MySqlDataAccess.ConnectionString, CommandType.Text, query);
            return intvalue;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public string IfCouponExists(string couponcode)
    {
        string FLAG = "F";
        DataTable dt = new DataTable();
        string query = "Select couponcode from Coupon where couponcode='" + couponcode + "'";
        dt = MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        if (dt.Rows.Count > 0)
        {
            FLAG = "T";
        }
        return FLAG;
    }


    public DataTable getCouponDetails(string couponcode)
    {
        try
        {
            string query = @"select * from Coupon where couponcode='" + couponcode + "' and couponactive=1";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable getCouponAllDetails()
    {
        try
        {
            string query = @"Select * from Coupon where couponstatus!=1 order by cid DESC";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public int UpdateCouponStatus(Int32 couponid, bool active)
    {
        try
        {
            string query = "Update Coupon set couponactive='" + active + "' where cid='" + couponid + "'";
            int intvalue = MySqlDataAccess.ExecuteNonQuery(MySqlDataAccess.ConnectionString, CommandType.Text, query);
            return intvalue;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public int ProductCoupon(string cid)
    {
        try
        {
            string query = @"Delete From Coupon where cid='" + cid + "'";
            return MySqlDataAccess.ExecuteNonQuery(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    //For Special Coupons

    public DataTable getSpecialCouponAllDetails()
    {
        try
        {
            string query = @"Select * from Coupon where couponstatus=1 order by cid DESC";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    //Admin Subscription List

    public int AddNewSubscribe(string emailid, string date)
    {
        try
        {
            string query = @"Insert into Subscribe(Emailid, Subdate) values('" + emailid + "', '" + date + "')";
            return MySqlDataAccess.ExecuteNonQuery(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public DataTable getAllSubscriber()
    {
        try
        {
            string query = @"Select * from Subscribe order by Id ASC";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public int DeleteSubscription(Int32 id)
    {
        try
        {
            string query = @"Delete From Subscribe where Id='" + id + "'";
            return MySqlDataAccess.ExecuteNonQuery(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable getSubscriptionList(int pageIndex, int pageSize, out int recordCount)
    {
        try
        {
            string conn = MySqlDataAccess.ConnectionString;
            SqlConnection con = new SqlConnection(conn);
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "GetMemberAmountRequest";
            cmd.Connection = con;
            SqlParameter param = new SqlParameter();

            param = cmd.CreateParameter();
            param.ParameterName = "@PageIndex";
            param.Value = pageIndex;
            param.DbType = DbType.Int32;
            cmd.Parameters.Add(param);

            param = cmd.CreateParameter();
            param.ParameterName = "@PageSize";
            param.Value = pageSize;
            param.DbType = DbType.Int32;
            cmd.Parameters.Add(param);

            param = cmd.CreateParameter();
            param.ParameterName = "@RecordCount";
            param.Direction = ParameterDirection.Output;
            param.DbType = DbType.Int32;
            cmd.Parameters.Add(param);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable table = new DataTable();
            da.Fill(table);

            recordCount = Convert.ToInt32(cmd.Parameters["@RecordCount"].Value);

            return table;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    //Admin Add Blog

    public int AddNewBlog(string title, string description, string date, string imagefile)
    {
        try
        {
            string query = @"Insert into Blog(Blogtitle, Blogbody, BlogDate, imagefile) values('" + title + "','" + description + "', '"+ date + "', '" + imagefile + "')";
            return MySqlDataAccess.ExecuteNonQuery(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public int UpdateBlog(string title, string description, string blogid, string imagefile)
    {
        try
        {
            string query = @"update Blog set Blogtitle='" + title + "', Blogbody='" + description + "', imagefile='" + imagefile + "' where Blogid='" + blogid + "'";
            return MySqlDataAccess.ExecuteNonQuery(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable getAllBlogs()
    {
        try
        {
            string query = @"Select Blogid, Blogtitle, substring(Blogbody, 1, 200) as [Blogbody], BlogDate, imagefile from Blog order by Blogid DESC";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable getBlogByid(string blogid)
    {
        try
        {
            string query = @"select * from Blog where Blogid='" + blogid + "'";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public int DeleteBlog(Int32 blogid)
    {
        try
        {
            string query = @"Delete from Blog where Blogid='" + blogid + "'";
            return MySqlDataAccess.ExecuteNonQuery(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }


    //Admin Add Event

    public int AddNewEvent(string title, string description, string date, string imagefile)
    {
        try
        {
            string query = @"Insert into t_Events(Eventtitle, Eventbody, EventDate, imagefile) values('" + title + "','" + description + "', '" + date + "', '" + imagefile + "')";
            return MySqlDataAccess.ExecuteNonQuery(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public int UpdateEvent(string title, string description, string eventid, string imagefile)
    {
        try
        {
            string query = @"Update t_Events set Eventtitle='" + title + "', Eventbody='" + description + "', imagefile='" + imagefile + "' where Eventid='" + eventid + "'";
            return MySqlDataAccess.ExecuteNonQuery(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable getAllEvents()
    {
        try
        {
            string query = @"Select Eventid, Eventtitle, substring(Eventbody, 1, 200) as [Eventbody], EventDate, imagefile from t_Events order by Eventid DESC";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable getEventByid(string eventid)
    {
        try
        {
            string query = @"Select * from t_Events where Eventid='" + eventid + "'";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public int DeleteEvent(Int32 eventid)
    {
        try
        {
            string query = @"Delete from t_Events where Eventid='" + eventid + "'";
            return MySqlDataAccess.ExecuteNonQuery(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }




}