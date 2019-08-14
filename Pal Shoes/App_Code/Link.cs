using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Link
/// </summary>
public class Link
{
	public Link()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    private static string BuildAbsolute(string relativeUri)
    {
        // get current uri
        Uri uri = HttpContext.Current.Request.Url;
        // build absolute path
        string app = HttpContext.Current.Request.ApplicationPath;
        if (!app.EndsWith("/")) app += "/";
        relativeUri = relativeUri.TrimStart('/');
        // return the absolute path
        return HttpUtility.UrlPathEncode(String.Format("http://{0}:{1}{2}{3}", uri.Host, uri.Port, app, relativeUri));
    }

    public static string ToProductOnDefault(string page)
    {
        if (page == "1")
            return BuildAbsolute(String.Format("product.aspx"));
        else
            return BuildAbsolute(String.Format("product.aspx?Page={0}", page));
    }
    public static string ToProductOnDefault()
    {
        return ToProductOnDefault("1");
    }
    public static string ToProductOnDefault2(string page)
    {
        if (page == "1")
            return BuildAbsolute(String.Format("product-list.aspx"));
        else
            return BuildAbsolute(String.Format("product-list.aspx?Page={0}", page));
    }
    public static string ToProductOnDefault2()
    {
        return ToProductOnDefault2("1");
    }

    public static string ToAllProductOnDefault(string page)
    {
        if (page == "1")
            return BuildAbsolute(String.Format("viewallproducts.aspx"));
        else
            return BuildAbsolute(String.Format("viewallproducts.aspx?Page={0}", page));
    }
    public static string ToAllProductOnDefault()
    {
        return ToAllProductOnDefault("1");
    }

    public static string ToProductOnCategory(string categoryid, string page)
    {
        if (page == "1")
            return BuildAbsolute(String.Format("product.aspx?category={0}", categoryid));
        else
            return BuildAbsolute(String.Format("product.aspx?category={0}&Page={1}", categoryid, page));
    }
    public static string ToProductOnCategory(string categoryId)
    {
        return ToProductOnCategory(categoryId, "1");
    }

    public static string ToProductOnFlavour(string flavour, string page)
    {
        if (page == "1")
            return BuildAbsolute(String.Format("product.aspx?Flavour={0}", flavour));
        else
            return BuildAbsolute(String.Format("product.aspx?Flavour={0}&Page={1}", flavour, page));
    }
    public static string ToProductOnFlavour(string flavour)
    {
        return ToProductOnFlavour(flavour, "1");
    }

    public static string ToProductOnBrand(string flavour, string page)
    {
        if (page == "1")
            return BuildAbsolute(String.Format("product.aspx?Brand={0}", flavour));
        else
            return BuildAbsolute(String.Format("product.aspx?Brand={0}&Page={1}", flavour, page));
    }
    public static string ToProductOnBrand(string flavour)
    {
        return ToProductOnBrand(flavour, "1");
    }

    public static string ToProductSubCategory(string subcategoryid, string page)
    {
        if (page == "1")
            return BuildAbsolute(String.Format("product.aspx?subcategory={0}", subcategoryid));
        else
            return BuildAbsolute(String.Format("product.aspx?subcategory={0}&Page={1}", subcategoryid, page));
    }
    public static string ToProductSubCategory(string subcategoryid)
    {
        return ToProductSubCategory(subcategoryid, "1");
    }



    //list
    public static string ToProductOnCategory2(string categoryid, string page)
    {
        if (page == "1")
            return BuildAbsolute(String.Format("product-list.aspx?category={0}", categoryid));
        else
            return BuildAbsolute(String.Format("product-list.aspx?category={0}&Page={1}", categoryid, page));
    }
    public static string ToProductOnCategory2(string categoryId)
    {
        return ToProductOnCategory2(categoryId, "1");
    }

    public static string ToProductOnFlavour2(string flavour, string page)
    {
        if (page == "1")
            return BuildAbsolute(String.Format("product-list.aspx?Flavour={0}", flavour));
        else
            return BuildAbsolute(String.Format("product-list.aspx?Flavour={0}&Page={1}", flavour, page));
    }
    public static string ToProductOnFlavour2(string flavour)
    {
        return ToProductOnFlavour2(flavour, "1");
    }

    public static string ToProductOnBrand2(string flavour, string page)
    {
        if (page == "1")
            return BuildAbsolute(String.Format("product-list.aspx?Brand={0}", flavour));
        else
            return BuildAbsolute(String.Format("product-list.aspx?Brand={0}&Page={1}", flavour, page));
    }
    public static string ToProductOnBrand2(string flavour)
    {
        return ToProductOnBrand2(flavour, "1");
    }

    public static string ToProductSubCategory2(string subcategoryid, string page)
    {
        if (page == "1")
            return BuildAbsolute(String.Format("product-list.aspx?subcategory={0}", subcategoryid));
        else
            return BuildAbsolute(String.Format("product-list.aspx?subcategory={0}&Page={1}", subcategoryid, page));
    }
    public static string ToProductSubCategory2(string subcategoryid)
    {
        return ToProductSubCategory2(subcategoryid, "1");
    }
}