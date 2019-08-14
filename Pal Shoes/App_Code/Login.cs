using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using SqlAccess;
using System.Data.SqlClient;

/// <summary>
/// Summary description for Login
/// </summary>
public class chkLogin
{
    public string getuserInfo(string Userid, string password)
    {
        DataTable dt_login = new DataTable();
        string result = "";
        dt_login = MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, "SELECT loginid, username, loginattempt, password, logintype, UserId, Mobileno FROM t_login where username='" + Userid + "' OR Mobileno='" + Userid + "' and active='A'");
        if (dt_login.Rows.Count > 0 && password == Convert.ToString(dt_login.Rows[0]["password"]))
        {
            if (Convert.ToInt64(dt_login.Rows[0]["loginattempt"]) >= 10)
            {
                result = "Your Account is Disabled, please contact to administrator";
            }
            else
            {
                result = Convert.ToString(dt_login.Rows[0]["loginid"]) + "~" + Convert.ToString(dt_login.Rows[0]["username"]) + "~" + Convert.ToString(dt_login.Rows[0]["logintype"]) + "~" + Convert.ToString(dt_login.Rows[0]["UserId"]);
                string query = @"update t_login set loginattempt = 0 where username='" + Userid + "'";
                object intReturnValue = MySqlDataAccess.ExecuteScalar(MySqlDataAccess.ConnectionString, CommandType.Text, query);
            }
        }
        else
        {
            try
            {
                if (dt_login.Rows.Count > 0)
                {
                    if (Convert.ToInt64(dt_login.Rows[0]["loginattempt"]) >= 3)
                    {
                        result = "Your Account is Disabled, please contact to administrator";
                    }
                    else
                    {
                        string query = @"update t_login set loginattempt =(loginattempt)+1 where username='" + Userid + "'";
                        object intReturnValue = MySqlDataAccess.ExecuteScalar(MySqlDataAccess.ConnectionString, CommandType.Text, query);
                        result = "Invalid username or Password, please try again.";
                    }
                }
                else
                {
                    result = "Invalid username or Password, please try again.";
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        return result;
    }



    public string getUserLogin(string Userid, string password)
    {
        DataTable dt_login = new DataTable();
        string result = "";
        dt_login = MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, "SELECT UserId, Name, Emailid, Password FROM t_User where Emailid='" + Userid + "'");
        if (dt_login.Rows.Count > 0 && password == Convert.ToString(dt_login.Rows[0]["Password"]))
        {
            result = Convert.ToString(dt_login.Rows[0]["UserId"]) + "~" + Convert.ToString(dt_login.Rows[0]["Name"]) + "~" + Convert.ToString(dt_login.Rows[0]["Emailid"]);
        }
        else
        {
            result = "Invalid username or Password, please try again.";
        }
        return result;
    }

    public int CreateNewUser(string fname, string lname, string emailid, string password, string active, int role, string logintype, string useridno)
    {
        try
        {
            int inval = MySqlDataAccess.ExecuteNonQuery(MySqlDataAccess.ConnectionString, CommandType.StoredProcedure, "ProNewCreateUser", new SqlParameter("@Firstname", fname), new SqlParameter("@Lastname", lname), new SqlParameter("@Emailid", emailid), new SqlParameter("@password", password), new SqlParameter("@active", active), new SqlParameter("@role", role), new SqlParameter("@logintype", logintype), new SqlParameter("@UserIdenttno", useridno));
            return Convert.ToInt32(inval);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public string IfUserExists(string usermobile, string emailid, string Update, string loginid)
    {
        string FLAG = "F";
        DataTable dt = new DataTable();
        if (Update == "N")
            dt = MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, "select LoginID, Mobileno, UserName from t_login where Mobileno='" + usermobile + "' OR UserName='" + emailid + "'");
        else
            dt = MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, "select LoginID, Mobileno, UserName from t_login where Mobileno='" + usermobile + "' OR UserName='" + emailid + "' AND UserId!='" + loginid + "'");

        if (dt.Rows.Count > 0)
        {
            FLAG = "T";
        }
        return FLAG;
    }

    public int UpdateUserInShoppingCart(string loginid, string cartid)
    {
        try
        {
            string query = "update ShopingCart set UserId='" + loginid + "' where CartId='" + cartid + "'";
            object intvalue = MySqlDataAccess.ExecuteScalar(MySqlDataAccess.ConnectionString, CommandType.Text, query);
            return Convert.ToInt32(intvalue);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public int UpdateUserInWishlist(string loginid, string wishid)
    {
        try
        {
            string query = "update t_Wishlist set UserId='" + loginid + "' where WishId='" + wishid + "'";
            object intvalue = MySqlDataAccess.ExecuteScalar(MySqlDataAccess.ConnectionString, CommandType.Text, query);
            return Convert.ToInt32(intvalue);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable getCartIdOnTransactionId(string transid)
    {
        try
        {
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, "select CartId, UserId from ShopingCart where TransId='" + transid + "'");
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable getWishIdOnTransId(string transid)
    {
        try
        {
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, "select WishId, UserId from t_Wishlist where TransId='" + transid + "'");
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public static long GenNumber(string col, string table)
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
                intReturnValue = Convert.ToInt32(intReturnValue) + 1;
            }
            return Convert.ToInt64(intReturnValue);
        }
    }



    //IfUserPassExists
    public string IfUserPassExists(string username, string password, string Update, string loginid)
    {
        string FLAG = "F";
        DataTable dt = new DataTable();
        if (Update == "N")
            dt = MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, "select LoginID, UserName, Password from t_login where UserName='" + username + "' OR Mobileno='" + username + "' AND Password='" + password + "'");
        else
            dt = MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, "select LoginID,UserName, Password from t_login where UserName='" + username + "' OR Mobileno='" + username + "' AND Password='" + password + "' AND LoginID!='" + loginid + "'");

        if (dt.Rows.Count > 0)
        {
            FLAG = "T";
        }
        return FLAG;
    }

    
    public int UpdatePassword(string newpassword, string loginid)
    {
        try
        {
            string query = "update t_login set Password='" + newpassword + "' where LoginID='" + loginid + "' AND LoginID !=1";
            return MySqlDataAccess.ExecuteNonQuery(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public int UpdatePasswordAdmin(string newpassword, string loginid)
    {
        try
        {
            string query = "update t_login set Password='" + newpassword + "' where LoginID='" + loginid + "'";
            return MySqlDataAccess.ExecuteNonQuery(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable getdataLogin(string username)
    {
        try
        {
            string query = "select LoginID, UserName, Password from t_login where UserName='" + username + "' OR Mobileno='" + username + "'";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }


    public DataTable getLoginActiveData()
    {
        try
        {
            string query = "select LoginID, UserName, Password, Active from t_login order by LoginID desc";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public int ActiveWallet(string active, string loginid, int walletpin)
    {
        try
        {
            string query = "update Wallet set wallStatus='" + active + "', WalletPin='"+walletpin+"' where WalletId=(select WalletId from Wallet where UserId='" + loginid + "')";
            object intvalue = MySqlDataAccess.ExecuteScalar(MySqlDataAccess.ConnectionString, CommandType.Text, query);
            return Convert.ToInt32(intvalue);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public int DeletUser(string userid)
    {
        try
        {
            object intvalue = MySqlDataAccess.ExecuteScalar(MySqlDataAccess.ConnectionString, CommandType.StoredProcedure, "ProDeleteUser", new SqlParameter("@userid", userid));
            return Convert.ToInt32(intvalue);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }


    public DataTable getUserDataforgotpassword(string mobileno)
    {
        try
        {
            string query = "Select LoginID, Password, UserName, UserId, Mobileno from t_login where Mobileno='" + mobileno + "'";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }


    public DataTable getAdminMobileNo()
    {
        try
        {
            string query = "select Mobileno from t_login where UserId='1'";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}