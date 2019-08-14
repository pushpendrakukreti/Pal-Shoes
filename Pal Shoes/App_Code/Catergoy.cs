using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using SqlAccess;
using System.Data.SqlClient;

/// <summary>
/// Summary description for Catergoy
/// </summary>
public class Catergoy
{
    public int CreateNewCategory(string categoryname)
    {
        try
        {
            string query = "insert into t_Category (CategoryName) values('" + categoryname + "')";
            object intVal = MySqlDataAccess.ExecuteScalar(MySqlDataAccess.ConnectionString, CommandType.Text, query);
            return Convert.ToInt32(intVal);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public int UpdateCategory(Int32 categoryid, string categoryname)
    {
        try
        {
            string query = "update t_Category set CategoryName='" + categoryname + "' where CategoryId='" + categoryid + "'";
            object intVal = MySqlDataAccess.ExecuteScalar(MySqlDataAccess.ConnectionString, CommandType.Text, query);
            return Convert.ToInt32(intVal);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable getAllCategory()
    {
        try
        {
            string query = "select CategoryId, CategoryName from t_Category";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public int DeleteCategory(Int32 cagegoryid)
    {
        try
        {
            int success = 0;
            string query = "delete from t_Category where CategoryId='" + cagegoryid + "'";
            return success = MySqlDataAccess.ExecuteNonQuery(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public string IsCategoryExists(string categoryname, string Update, string categoryid)
    {
        string FLAG = "F";
        DataTable dt = new DataTable();
        if (Update == "N")
            dt = MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, "select CategoryName from t_Category where CategoryName='" + categoryname + "'");
        else
            dt = MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, "select CategoryName from t_Category where CategoryName='" + categoryname + "' AND CategoryId!='"+categoryid+"'");

        if (dt.Rows.Count > 0)
        {
            FLAG = "T";
        }
        return FLAG;
    }


    //Home Side Menu
    public DataTable getMenuCategory()
    {
        try
        {
            //string query = "Select CategoryId, CategoryName FROM t_Category where CategoryName !='ACCESSORIES'";
            string query = "Select CategoryId, CategoryName FROM t_Category";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public DataTable getMenuCategoryTop5()
    {
        try
        {
            string query = "select top 5 CategoryId, CategoryName FROM t_Category";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable getMenuSubCategory(string categoryid)
    {
        try
        {
            string query = "SELECT SubCategoryId, SubName FROM t_SubCategory WHERE CategoryId='" + categoryid + "'";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable getProductBrand()
    {
        try
        {
            string query = "select Distinct Brand from t_Product Order by Brand ASC";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public DataTable getCategoryOnBrand(string brandname)
    {
        try
        {
            string query = @"select c.CategoryId,c.CategoryName from t_Category c inner join t_ProductCategory pc on c.CategoryId = pc.CategoryId inner join t_Product p on pc.ProductId= p.ProductId where p.Brand='" + brandname + "'";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }


    public int AddBrand(string brandname, string categoryid, string brandimage)
    {
        try
        {
            string query = "insert into t_Brand(Brandname, Categoryid, BrandImage) values('" + brandname + "','" + categoryid + "','" + brandimage + "')";
            object intVal = MySqlDataAccess.ExecuteNonQuery(MySqlDataAccess.ConnectionString, CommandType.Text, query);
            return Convert.ToInt32(intVal);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public string IsBrandExists(string brandname, string categoryid, string Update, string id)
    {
        string FLAG = "F";
        DataTable dt = new DataTable();
        if (Update == "N")
            dt = MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, "select Brandname, Categoryid from t_Brand where Brandname='" + brandname + "' AND Categoryid='" + categoryid + "'");
        else
            dt = MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, "select Brandname, Categoryid from t_Brand where Brandname='" + brandname + "' AND Categoryid='" + categoryid + "' AND BrandId!='" + id + "'");

        if (dt.Rows.Count > 0)
        {
            FLAG = "T";
        }
        return FLAG;
    }

    public DataTable getBrands()
    {
        try
        {
            string query = "select BrandId, Brandname,(select CategoryName from t_Category where CategoryId= t_Brand.Categoryid)as Category, Categoryid, BrandImage from t_Brand Order by BrandId";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}