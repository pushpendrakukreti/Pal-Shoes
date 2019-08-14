using System;
using System.Collections.Generic;
using System.Linq;
using SqlAccess;
using System.Configuration;
using System.Collections;

/// <summary>
/// Summary description for Configuration
/// </summary>
public class Configuration
{
    private readonly static int productsPerPage;
    private readonly static string siteName;

    public static int ProductsPerPage
    {
        get { return productsPerPage; }
    }

    public static string SiteName
    {
        get { return siteName; }
    }

    static Configuration()
    {
        productsPerPage = System.Int32.Parse(ConfigurationManager.AppSettings["ProductsPerPage"]);
        siteName = ConfigurationManager.AppSettings["SiteName"];
    }

    //API
    public static string SuvidhaUser
    {
        get
        {
            return ConfigurationManager.AppSettings["USERID"];
        }
    }
    public static string SuvidhaPin
    {
        get
        {
            return ConfigurationManager.AppSettings["PIN"];
        }
    }
}