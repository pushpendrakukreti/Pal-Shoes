using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using SqlAccess;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;


public partial class admin_AddVideo : System.Web.UI.Page
{
    Videos local_video = new Videos();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Convert.ToString(Session["userid"])) && Convert.ToString(Session["logintype"]) == "Admin")
        {
            if (!IsPostBack)
            {
                BindgvVideos();
            }
        }
        else
        {
            Response.Redirect("Login.aspx");
        }

    }

    protected void btnAddurl_Click(object sender, EventArgs e)
    {
        try
        {
            string url = txtvideoUrl.Text;
            if (txtvideoUrl.Text.Length > 0 && url.Contains("youtube.com"))
            {
                string videotitle = txtVideotitle.Text.Trim();
                string ytFormattedUrl = GetYouTubeID(url);
                string shorturlid = GetShortID(ytFormattedUrl);

                string videodate = System.DateTime.Now.ToString();
                videodate = Convert.ToDateTime(videodate).ToString("yyyy-MM-dd HH:mm:ss");

                if (!CheckDuplicate(ytFormattedUrl))
                {
                    local_video.AddVideoUrl(videotitle, ytFormattedUrl, shorturlid, videodate);

                    lblMessage.ForeColor = Color.Green;
                    lblMessage.Text = "Video Url Added Successfully!";
                    txtVideotitle.Text = "";
                    txtvideoUrl.Text = "";

                    BindgvVideos();
                }
                else
                {
                    lblMessage.ForeColor = Color.Red;
                    lblMessage.Text = "This video already exist!";
                }
            }
            else
            {
                lblMessage.ForeColor = Color.Red;
                lblMessage.Text = "URL is not a valid YOUTUBE video link?";
            }
        }
        catch(Exception ex)
        {
            lblMessage.ForeColor = Color.Red;
            lblMessage.Text = ex.Message;
        }
    }


    private string GetYouTubeID(string youTubeUrl)
    {
        Match regexMatch = Regex.Match(youTubeUrl, "^[^v]+v=(.{11}).*", RegexOptions.IgnoreCase);
        if (regexMatch.Success)
        {
            return "http://www.youtube.com/v/" + regexMatch.Groups[1].Value + "&hl=en&fs=1";
        }
        return youTubeUrl;
    }

    private string GetShortID(string youTUrl)
    {
        return youTUrl.Replace("http://www.youtube.com/v/", "").Replace("&hl=en&fs=1", "");
    }

    public bool CheckDuplicate(string youTubeUrl1)
    {
        bool exists = false;

        DataTable dt = new DataTable();
        dt = local_video.checkDuplicate(youTubeUrl1);

        exists = dt.Rows.Count > 0 ? true : false;

        return exists;
    }

    private void BindgvVideos()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = local_video.getVideosUrl2();
            if (dt.Rows.Count > 0)
            {
                gvHomeVideo.DataSource = dt;
                gvHomeVideo.DataBind();
            }
            else
            {
                gvHomeVideo.DataSource = null;
                gvHomeVideo.DataBind();
            }
        }
        catch
        { }
    }

    protected void DeleteRecord(object sender, EventArgs e)
    {
        try
        {
            int id = int.Parse((sender as Button).CommandArgument);
            int success = 0;
            success = local_video.DeleteVideo(id);
            if (success != 0)
            {
                BindgvVideos();
                lblmessage1.ForeColor = Color.Green;
                lblmessage1.Text = "Video Deleted Successfully!";
            }
            else
            {
                lblmessage1.ForeColor = Color.Red;
                lblmessage1.Text = "Video Delete Failed?";
            }
        }
        catch (Exception ex)
        {
            lblmessage1.ForeColor = Color.Red;
            lblmessage1.Text = ex.Message;
        }
    }
    protected void gvHomeVideo_RowDeleting1(object sender, GridViewDeleteEventArgs e)
    {
    }

    protected void gvHomeVideo_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
       
    }
}