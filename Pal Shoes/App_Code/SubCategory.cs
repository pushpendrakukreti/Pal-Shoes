using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using SqlAccess;
using System.Data.SqlClient;

/// <summary>
/// Summary description for SubCategory
/// </summary>
public class SubCategory
{
    public int AddSubCategory(string categoryid, string subname, string subdescription, string images, string thumbnail)
    {
        try
        {
            int success = 0;
            success = MySqlDataAccess.ExecuteNonQuery(MySqlDataAccess.ConnectionString, CommandType.StoredProcedure, "ProAddSubCategory", new SqlParameter("@CategoryId", categoryid), new SqlParameter("@SubName", subname), new SqlParameter("@SubDescription", subdescription), new SqlParameter("@Image", images), new SqlParameter("@Thumbnail", thumbnail));
            return success;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public int UpdateSubCategory(Int32 subcategoryid, string subname, string subdescription, Int32 categoryid)
    {
        try
        {
            int success = 0;
            success = MySqlDataAccess.ExecuteNonQuery(MySqlDataAccess.ConnectionString, CommandType.StoredProcedure, "ProUpdateSubCategory", new SqlParameter("@SubCategoryId", subcategoryid), new SqlParameter("@SubName", subname), new SqlParameter("@SubDescription", subdescription), new SqlParameter("@Categoryid", categoryid));
            return success;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable getAllSubCategory()
    {
        try
        {
            string query = "select SubCategoryId, (select CategoryName from t_Category where CategoryId=t_SubCategory.CategoryId)as Category, SubName, SubDescription, [Image], Thumbnail from t_SubCategory Order by SubCategoryId DESC";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable GetSubCategoryById(string subcategoryid)
    {
        try
        {
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.StoredProcedure, "ProGetSubCategoryById", new SqlParameter("@SubCategoryId", subcategoryid));
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public int DeleteSubCategory(int subcategoryid)
    {
        try
        {
            string query = "delete from t_SubCategory where SubCategoryId='" + subcategoryid + "'";
            return MySqlDataAccess.ExecuteNonQuery(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable getSubCategoryInCategoery(string categoryid)
    {
        try
        {
            string query = "select SubCategoryId, CategoryId, SubName, SubDescription, [Image], Thumbnail from t_SubCategory where CategoryId='" + categoryid + "'";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }



    public DataTable getAccessoriesSubCategory()
    {
        try
        {
            string query = @"select SubCategoryId, sb.CategoryId, SubName from t_SubCategory sb
  inner join t_Category c on sb.CategoryId= c.CategoryId where c.CategoryName ='ACCESSORIES'";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}