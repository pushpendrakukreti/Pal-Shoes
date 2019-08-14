using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public struct PageUrl
{
    private string page;
    private string url;
    public string Page
    {
        get { return page; }
    }
    public string Url
    {
        get { return url; }
    }
    public PageUrl(string page, string url)
    {
        this.page = page;
        this.url = url;
    }
}

public partial class Controls_Pager : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void Show(int currentPage, int howManyPages, string firstPageUrl, string pageUrlFormat, bool showPages)
    {
        if (howManyPages > 1)
        {
            this.Visible = true;

            //currentPageLabel.Text = currentPage.ToString();
            //howManyPagesLabel.Text = howManyPages.ToString();

            if (currentPage == 1)
            {
                prevLink.Enabled = false;
            }
            else
            {
                prevLink.NavigateUrl = (currentPage == 2) ? firstPageUrl : String.Format(pageUrlFormat, currentPage - 1);
            }
            if (currentPage == howManyPages)
            {
                nextLink.Enabled = false;
            }
            else
            {
                nextLink.NavigateUrl = String.Format(pageUrlFormat, currentPage + 1);
            }

            if (showPages)
            {
                PageUrl[] pages = new PageUrl[howManyPages];
                pages[0] = new PageUrl("1", firstPageUrl);

                for (int i = 2; i <= howManyPages; i++)
                {
                    pages[i - 1] = new PageUrl(i.ToString(), String.Format(pageUrlFormat, i));
                }

                pages[currentPage - 1] = new PageUrl((currentPage).ToString(), "");

                pagesRepeater.DataSource = pages;
                pagesRepeater.DataBind();
            }
        }
    }
}