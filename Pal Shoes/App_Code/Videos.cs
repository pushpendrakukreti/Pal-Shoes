using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using SqlAccess;

/// <summary>
/// Summary description for Videos
/// </summary>
public class Videos
{
    public DataTable checkDuplicate(string videoUrl)
    {
        try
        {
            string query = String.Format("select * from t_video where videourl='{0}'", videoUrl);
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public int AddVideoUrl(string videotitle, string videourl, string videourlid, string date)
    {
        try
        {
            string query = "Insert into t_video(videotitle,videourl,videourlid,video_date) values('" + videotitle + "','" + videourl + "','" + videourlid + "', '" + date + "')";
            object intvalue = MySqlDataAccess.ExecuteScalar(MySqlDataAccess.ConnectionString, CommandType.Text, query);
            return Convert.ToInt32(intvalue);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable getVideosUrl()
    {
        try
        {
            string query = "Select top 10 videoid, videotitle, videourl, videourlid, video_date from t_video order by videoid desc";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch(Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable getVideosUrl2()
    {
        try
        {
            string query = "Select videoid, videotitle, videourl, videourlid, video_date from t_video order by videoid desc";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public int DeleteVideo(Int32 videoid)
    {
        try
        {
            string query = "Delete from t_video where videoid='" + videoid + "'";
            return MySqlDataAccess.ExecuteNonQuery(MySqlDataAccess.ConnectionString, CommandType.Text, query);

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}