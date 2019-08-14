using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using SqlAccess;

/// <summary>
/// Summary description for Wallet
/// </summary>
public class WalletCls
{
    //Bind Wallet Icon
    public DataTable getWalletIconAmount(string userid)
    {
        try
        {
            string query = "select Amount from Wallet where wallStatus='Active' AND UserId='" + userid + "'";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public DataTable getWalletOrderOvwAmount(string userid)
    {
        try
        {
            string query = "select WalletId, Amount from Wallet where wallStatus='Active' AND UserId='" + userid + "'";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public DataTable getAdminWalletAmount(string userid)
    {
        try
        {
            string query = "select Amount from Wallet where wallStatus='Active' AND UserId='" + userid + "'";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable getRegistraionDetail()
    {
        try
        {
            string query = "select u.UserId, u.Firstname, u.Lastname, u.Emailid, w.wallStatus, w.walletDate, u.UserIdentifyNo from t_User u inner join Wallet w on u.UserId= w.UserId where u.UserId !=1 Order by u.UserId DESC";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public DataTable getMemberDetails()
    {
        try
        {
            string query = @"select u.UserId, (u.Firstname+' '+u.Lastname) as name, u.Emailid, u.UserIdentifyNo, w.Amount, w.WalletPin, w.wallStatus, w.walletDate from t_User u inner join Wallet w on u.UserId= w.UserId where u.UserId!=1 Order by u.UserId DESC";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable getUserWalletDetails(string userid)
    {
        try
        {
            string query = @"select WdID, WalletId, UserId,(select (Firstname+' '+Lastname) name from t_User where UserId= WalletDetails.UserId)as Customername, Amount, (Amount + ISNULL(AddedAmount,0) - ISNULL(DeductedAmount, 0))as totamount, AddedAmount, DeductedAmount, AddedDate, TransactionNo, Transactiontype, WalletStatus, Customername, Emailid, ActionBy, UserIdentifyno from WalletDetails WHERE UserId='" + userid + "' order by WdID DESC";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public DataTable getUserWalletDetailsSearch(string searchby)
    {
        try
        {
            string query = @"select WdID, WalletId, UserId, (select (Firstname+' '+Lastname) name from t_User where UserId= WalletDetails.UserId)as Customername, Amount, (Amount + ISNULL(AddedAmount, 0) - ISNULL(DeductedAmount, 0))as totamount, AddedAmount, DeductedAmount, AddedDate, TransactionNo, Transactiontype, WalletStatus, Customername, Emailid, ActionBy, UserIdentifyno from WalletDetails WHERE UserIdentifyno='" + searchby + "' OR TransactionNo='" + searchby + "' order by WdID DESC";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable getAdminWalletDetails(string userid)
    {
        try
        {
            string query = @"select WdID, WalletId, UserId, Amount, (Amount + ISNULL(AddedAmount,0) - ISNULL(DeductedAmount, 0))as totamount, AddedAmount, AddedDate, WalletStatus, Actionby from WalletDetails WHERE UserId='" + userid + "' AND WalletId=1 order by WdID DESC";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }


    public DataTable getMainBalance(string loginid)
    {
        try
        {
            string query = @"select Amount from Wallet w inner join t_login l on w.UserId= l.UserId where l.LoginID= '" + loginid + "'";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable getUserToAddWalletAmount()
    {
        try
        {
            string query = @"select u.UserId, (u.Firstname+' '+ u.Lastname) as name, u.Emailid, u.UserIdentifyNo, w.wallStatus, 
w.walletDate, w.Amount from t_User u inner join Wallet w on u.UserId= w.UserId where w.UserId !=1 Order by u.UserId DESC";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable getUserAddWalletAmountByUserId(string useridenty)
    {
        try
        {
            string query = @"select u.UserId, (u.Firstname+' '+ u.Lastname) as name, u.Emailid, u.UserIdentifyNo, w.wallStatus, 
w.walletDate, w.Amount from t_User u inner join Wallet w on u.UserId= w.UserId where UserIdentifyNo='"+useridenty+"' Order by u.UserId DESC";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    //Customer Details
    public DataTable getUserDetails(string userid)
    {
        try
        {
            string query = @"select u.UserId, (Firstname+' '+Lastname)as fullname, u.Emailid, u.UserIdentifyNo, w.wallStatus, 
w.Amount, w.WalletId from t_User u inner join Wallet w on u.UserId= w.UserId where u.UserId='" + userid + "'";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }


    public int AddWalletMoney(BEL obj_d)
    {
        try
        {
            SqlParameter[] p = new SqlParameter[11];
            p[0] = new SqlParameter("@WalletId", obj_d.WalletId);
            p[1] = new SqlParameter("@UserId", obj_d.UserId);
            p[2] = new SqlParameter("@Amount", obj_d.Amount);
            p[3] = new SqlParameter("@AddedAmount", obj_d.AddedAmount);
            p[4] = new SqlParameter("@TransactionNo", obj_d.TransactionNo);
            p[5] = new SqlParameter("@Transactiontype", obj_d.PaymentType);
            p[6] = new SqlParameter("@WalletStatus", obj_d.WalletStatus);
            p[7] = new SqlParameter("@Customername", obj_d.Customername);
            p[8] = new SqlParameter("@Emailid", obj_d.EmailId);
            p[9] = new SqlParameter("@Actionby", obj_d.ActionBy);
            p[10] = new SqlParameter("@UserIdentifyno", obj_d.UserIdetifyNo);

            return MySqlDataAccess.ExecuteNonQuery(MySqlDataAccess.ConnectionString, CommandType.StoredProcedure, "ProAddMoneyToWallet", p);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public int DeductWalletMoney(BEL obj_d)
    {
        try
        {
            SqlParameter[] p = new SqlParameter[9];
            p[0] = new SqlParameter("@WalletId", obj_d.WalletId);
            p[1] = new SqlParameter("@UserId", obj_d.UserId);
            p[2] = new SqlParameter("@Amount", obj_d.Amount);
            p[3] = new SqlParameter("@DeductedAmount", obj_d.DeductedAmount);
            p[4] = new SqlParameter("@WalletStatus", obj_d.WalletStatus);
            p[5] = new SqlParameter("@Customername", obj_d.Customername);
            p[6] = new SqlParameter("@Emailid", obj_d.EmailId);
            p[7] = new SqlParameter("@Actionby", obj_d.ActionBy);
            p[8] = new SqlParameter("@UserIdentifyno", obj_d.UserIdetifyNo);

            return MySqlDataAccess.ExecuteNonQuery(MySqlDataAccess.ConnectionString, CommandType.StoredProcedure, "ProDeductMoneyFromWallet", p);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }


    public int AddCustomerAccountDetails(long mobileno, string customername, long accountno, string ifsccode, string bankname, int userid)
    {
        try
        {
            string query = "insert into CustomerAccount(Mobileno, BeneName, AccountNo, IFSC, BankName, UserId) VALUES('" + mobileno + "','" + customername + "','" + accountno + "','" + ifsccode + "','" + bankname + "','" + userid + "')";
            return MySqlDataAccess.ExecuteNonQuery(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public int UpdateCustomerAccountDetails(long mobileno, string customername, long accountno, string ifsccode, string bankname, Int32 acid)
    {
        try
        {
            string query = "update CustomerAccount set Mobileno='" + mobileno + "', BeneName='" + customername + "', AccountNo='" + accountno + "', IFSC='" + ifsccode + "', BankName='" + bankname + "' where CAcID='" + acid + "'";
            return MySqlDataAccess.ExecuteNonQuery(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public string IFAccountExists(int userid, string Update, string Id)
    {
        string FLAG = "F";
        DataTable dt = new DataTable();
        if (Update == "N")
            dt = MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, "select AccountNo, UserId from CustomerAccount where UserId='" + userid + "'");
        else
            dt = MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, "select AccountNo, UserId from CustomerAccount where UserId='" + userid + "' AND CAcID !='" + Id + "'");

        if (dt.Rows.Count > 0)
        {
            FLAG = "T";
        }
        return FLAG;
    }
    public DataTable getUserBankAcDetails(string userid)
    {
        try
        {
            string query = @"select CAcID, Mobileno, BeneName, AccountNo, IFSC, BankName, UserId from CustomerAccount where UserId='" + userid + "'";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable getUserIdentifyNo()
    {
        try
        {
            string query = @"select u.UserId, u.UserIdentifyNo from t_User u inner join t_login l on u.UserId= l.UserId where l.LoginType='User'";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable getUserDetailBene(string userid)
    {
        try
        {
            string query = @"select CAcID, Mobileno, BeneName, AccountNo, IFSC, BankName, UserId from CustomerAccount where UserId='" + userid + "'";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    
    public DataTable getUserAccountNubmer()
    {
        try
        {
            string query = @"select ISNULL(BankAccNo, 0)as Accountno, CustomerId from APITransaction where BankAccNo!=0";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable getBeneAccountDetail(string userid)
    {
        try
        {
            string query = @"select RTID, a.UserId, BeneficiaryType, BankAccNo, ConfirmBankAccNo, BeneficiaryName, 
SenderMobile, IfscCode, requestNo, otcRef, otc, benfCode, Type, CustomerId, CustIdentifyno, Tstatus, w.Amount from APITransaction a inner join Wallet w on a.CustomerId= w.UserId where a.CustomerId='" + userid + "'";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }


    //API transaction
    public int AddAPIcustomerRegistration(int Userid, string requestno, Int32 custid, string useridetifyno)
    {
        try
        {
            string query = "insert into APITransaction(UserId, requestNo, CustomerId, CustIdentifyno) values('" + Userid + "','" + requestno + "','" + custid + "','" + useridetifyno + "')";
            return MySqlDataAccess.ExecuteNonQuery(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public int UpdateAPIcustomerRegistOtc(int Userid, string smobileno, string otc, string requestno, string otcref, string type, string rtID)
    {
        try
        {
            string query = "update APITransaction SET UserId='" + Userid + "', SenderMobile='" + smobileno + "', otc='" + otc + "', requestNo='" + requestno + "', otcRef='" + otcref + "', Type='" + type + "' where RTID='" + rtID + "'";
            return MySqlDataAccess.ExecuteNonQuery(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public int UpdateAPIaddnewBeneficiary(int Userid, string benetype, string accountno, string benefname, string mobileno, string ifsccode, string requestno, string type, string rtID)
    {
        try
        {
            string query = "update APITransaction SET UserId='" + Userid + "', BeneficiaryType='" + benetype + "', BankAccNo='" + accountno + "', BeneficiaryName='" + benefname + "', SenderMobile='" + mobileno + "', IfscCode='" + ifsccode + "', requestNo='" + requestno + "', Type='" + type + "' where RTID='" + rtID + "'";
            return MySqlDataAccess.ExecuteNonQuery(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public int UpdateAPIBeneficiaryOtc(int Userid, string otc, string requestno, string otcref, string rtID)
    {
        try
        {
            string query = "update APITransaction SET UserId='" + Userid + "', otc='" + otc + "', requestNo='" + requestno + "', otcRef='" + otcref + "' where RTID='" + rtID + "'";
            return MySqlDataAccess.ExecuteNonQuery(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public int UpdateAPIBeneLogDetail(string requestno, string benefcode, string message, string rtID)
    {
        try
        {
            string query = "update APITransaction SET requestNo='" + requestno + "', benfCode='" + benefcode + "', Tstatus='" + message + "', BeneDate=Getdate() where RTID='" + rtID + "'";
            return MySqlDataAccess.ExecuteNonQuery(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable getApitbleID(string refrenceno)
    {
        try
        {
            string query = @"select MAX(RTID)as RTID from APITransaction where requestNo='" + refrenceno + "'";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public DataTable getApitbleIDbyMobile(string mobileno)
    {
        try
        {
            string query = @"select MAX(RTID)as RTID from APITransaction where SenderMobile='" + mobileno + "'";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable getApiTabelIDbyAC(string accountno, string mobileno)
    {
        try
        {
            string query = @"select RTID from APITransaction where BankAccNo='" + accountno + "' and SenderMobile='" + mobileno + "'";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable getResendOtcRefMobile(string mobileno, string userid)
    {
        try
        {
            string query = @"select otcRef, SenderMobile from APITransaction where CustomerId='" + userid + "' and SenderMobile='" + mobileno + "'";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public DataTable getResendOtcRequestMobile(string mobileno, string userid)
    {
        try
        {
            string query = @"select requestNo, SenderMobile from APITransaction where CustomerId='" + userid + "' and SenderMobile='" + mobileno + "'";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }


    public int AddFundTransferDetail(string transferno, Int32 userid, string useridefity, string benename, string accountno, string benetype, string ifsccode, string mobileno, decimal amount, string ipaddress)
    {
        try
        {
            string query = "insert into FundTransfer(TransactionNo, UserId, UserIdentifyno, BeneName, Accountno, BeneType, IFSCcode, Mobileno, Amount, Trasferdate, SystemIP) values('" + transferno + "','" + userid + "','" + useridefity + "','" + benename + "','" + accountno + "','" + benetype + "','" + ifsccode + "','" + mobileno + "','" + amount + "', getdate(),'" + ipaddress + "')";
            return MySqlDataAccess.ExecuteNonQuery(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }


    public int UpdateCustomerWalletFundtransfer(decimal amount, Int32 userid)
    {
        try
        {
            string query = "update Wallet set Amount=(Amount- " + amount + ") where wallStatus='Active' and UserId='" + userid + "'";
            return MySqlDataAccess.ExecuteNonQuery(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public int UpdateCustomerWalletDeductAdd(decimal amount, Int32 userid)
    {
        try
        {
            string query = "update Wallet set Amount=(Amount+ " + amount + ") where wallStatus='Active' and UserId='" + userid + "'";
            return MySqlDataAccess.ExecuteNonQuery(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public int DeductCustomerWalletAmt(int walletid, Int32 userid, decimal wamount, decimal deductamount, string transctionid, string trastype, string paytype, string custname, string emailid, int actionby, string identifyno)
    {
        try
        {
            string query = "insert into WalletDetails (WalletId,UserId,Amount, DeductedAmount,AddedDate, TransactionNo, Transactiontype, WalletStatus, Customername, Emailid, ActionBy, UserIdentifyno) values('" + walletid + "','" + userid + "','" + wamount + "','" + deductamount + "', GETDATE(), '" + transctionid + "','" + trastype + "','" + paytype + "','" + custname + "','" + emailid + "','" + actionby + "','" + identifyno + "')";
            return MySqlDataAccess.ExecuteNonQuery(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    
    public DataTable getFundTransferReport()
    {
        try
        {
            string query = @"select ID, TransactionNo, UserId, UserIdentifyno, BeneName, Accountno, BeneType, IFSCcode, Mobileno, Amount, Trasferdate, SystemIP from FundTransfer order by ID desc";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public DataTable getFundTransferReportValues(string searchby)
    {
        try
        {
            string query = @"select ID, TransactionNo, UserId, UserIdentifyno, BeneName, Accountno, 
BeneType, IFSCcode, Mobileno, Amount, Trasferdate, SystemIP from FundTransfer where TransactionNo='" + searchby + "' OR UserIdentifyno='" + searchby + "' OR Accountno='" + searchby + "' OR Mobileno='" + searchby + "' order by ID desc";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public DataTable getFundTransferReportDate(string date1, string date2)
    {
        try
        {
            string query = "";
            if (date1 != "" && date2 != "")
            {
                query = @"select ID, TransactionNo, UserId, UserIdentifyno, BeneName, Accountno, BeneType, IFSCcode, Mobileno, Amount, Trasferdate, SystemIP from FundTransfer where Cast(Trasferdate AS DATE) >='" + date1 + "' AND Cast(Trasferdate AS DATE)<= '" + date2 + "' order by Trasferdate";
            }
            else
            {
                query = @"select ID, TransactionNo, UserId, UserIdentifyno, BeneName, Accountno, BeneType, IFSCcode, Mobileno, Amount, Trasferdate, SystemIP from FundTransfer where Cast(Trasferdate AS DATE) ='" + date1 + "' order by Trasferdate";
            }
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }


    public DataTable getUserAddWalletAmountByDate(string date1, string date2)
    {
        try
        {
            string query = "";
            if (date1 != "" && date2 != "")
            {
                query = @"select u.UserId, (u.Firstname+' '+ u.Lastname) as name, u.Emailid, u.UserIdentifyNo, w.wallStatus, 
w.walletDate, w.Amount from t_User u inner join Wallet w on u.UserId= w.UserId where Cast(w.walletDate AS DATE) >='" + date1 + "' AND Cast(w.walletDate AS DATE)<= '" + date2 + "' order by w.walletDate";
            }
            else
            {
                query = @"select u.UserId, (u.Firstname+' '+ u.Lastname) as name, u.Emailid, u.UserIdentifyNo, w.wallStatus, 
w.walletDate, w.Amount from t_User u inner join Wallet w on u.UserId= w.UserId where Cast(w.walletDate AS DATE) ='" + date1 + "' order by w.walletDate";
            }
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }


    public DataTable getRegistrationUserDetailByUserID(string useridenty)
    {
        try
        {
            string query = @"select u.UserId, u.Firstname, u.Lastname, u.Emailid, w.wallStatus, w.walletDate, u.UserIdentifyNo from t_User u inner join Wallet w on u.UserId= w.UserId where u.UserIdentifyNo='" + useridenty + "' Order by u.UserId DESC";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public DataTable getRegistrationUserDetailByDate(string date1, string date2)
    {
        try
        {
            string query = "";
            if (date1 != "" && date2 != "")
            {
                query = @"select u.UserId, u.Firstname, u.Lastname, u.Emailid, w.wallStatus, w.walletDate, u.UserIdentifyNo from t_User u inner join Wallet w on u.UserId= w.UserId where Cast(w.walletDate AS DATE) >='" + date1 + "' AND Cast(w.walletDate AS DATE)<= '" + date2 + "' order by w.walletDate";
            }
            else
            {
                query = @"select u.UserId, u.Firstname, u.Lastname, u.Emailid, w.wallStatus, w.walletDate, u.UserIdentifyNo from t_User u inner join Wallet w on u.UserId= w.UserId where Cast(w.walletDate AS DATE) ='" + date1 + "' order by w.walletDate";
            }
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }


    public DataTable BindMembersDetailsByUserID(string useridenty)
    {
        try
        {
            string query = @"select u.UserId, (u.Firstname+' '+u.Lastname) as name, u.Emailid, u.UserIdentifyNo, w.Amount, w.WalletPin, w.wallStatus, w.walletDate from t_User u inner join Wallet w on u.UserId= w.UserId where u.UserIdentifyNo='" + useridenty + "' Order by u.UserId DESC";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public DataTable BindMembersDetailsByDate(string date1, string date2)
    {
        try
        {
            string query = "";
            if (date1 != "" && date2 != "")
            {
                query = @"select u.UserId, (u.Firstname+' '+u.Lastname) as name, u.Emailid, u.UserIdentifyNo, w.Amount, w.WalletPin, w.wallStatus, w.walletDate from t_User u inner join Wallet w on u.UserId= w.UserId where Cast(w.walletDate AS DATE) >='" + date1 + "' AND Cast(w.walletDate AS DATE)<= '" + date2 + "' order by w.walletDate";
            }
            else
            {
                query = @"select u.UserId, (u.Firstname+' '+u.Lastname) as name, u.Emailid, u.UserIdentifyNo, w.Amount, w.WalletPin, w.wallStatus, w.walletDate from t_User u inner join Wallet w on u.UserId= w.UserId where Cast(w.walletDate AS DATE) ='" + date1 + "' order by w.walletDate";
            }
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }


    public DataTable getUserWalletDetailsByDate(string date1, string date2, string userid)
    {
        try
        {
            string query = "";
            if (date1 != "" && date2 != "")
            {
                query = @"select WdID, WalletId, UserId, (select (Firstname+' '+Lastname) name from t_User where UserId= WalletDetails.UserId)as Customername, Amount, (Amount + ISNULL(AddedAmount,0) - ISNULL(DeductedAmount, 0))as totamount, AddedAmount, DeductedAmount, AddedDate, TransactionNo, Transactiontype, WalletStatus, Customername, Emailid, ActionBy, UserIdentifyno from WalletDetails WHERE Cast(AddedDate AS DATE) >='" + date1 + "' AND Cast(AddedDate AS DATE)<= '" + date2 + "' AND UserId='" + userid + "' order by AddedDate";
            }
            else
            {
                query = @"select WdID, WalletId, UserId, (select (Firstname+' '+Lastname) name from t_User where UserId= WalletDetails.UserId)as Customername, Amount, (Amount + ISNULL(AddedAmount,0) - ISNULL(DeductedAmount, 0))as totamount, AddedAmount, DeductedAmount, AddedDate, TransactionNo, Transactiontype, WalletStatus, Customername, Emailid, ActionBy, UserIdentifyno from WalletDetails WHERE Cast(AddedDate AS DATE) ='" + date1 + "' AND UserId='" + userid + "' order by AddedDate";
            }
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }


    public int AddTransferRequest(int userid, decimal amount, string requestdate, string requesttype, string requeststatus, bool request, long useridentifyno, string systemip)
    {
        try
        {
            string query = "INSERT INTO AmountRequest (UserID, Amount, RequestDate, RequetType, RequestStatus, Request, UserIdetifyno, SystemIP) VALUES('" + userid + "','" + amount + "','" + requestdate + "','" + requesttype + "','" + requeststatus + "','" + request + "','" + useridentifyno + "','" + systemip + "')";
            return MySqlDataAccess.ExecuteNonQuery(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public int AddPayAmountRequest(int userid, decimal amount, string requestdate, string requesttype, string requeststatus, bool request, long useridentifyno, string systemip, string paymenttype, long transactionno)
    {
        try
        {
            string query = "INSERT INTO AmountRequest (UserID, Amount, RequestDate, RequetType, RequestStatus, Request, UserIdetifyno, SystemIP, PaymentType, AccountNo) VALUES('" + userid + "','" + amount + "','" + requestdate + "','" + requesttype + "','" + requeststatus + "','" + request + "','" + useridentifyno + "','" + systemip + "','" + paymenttype + "','" + transactionno + "')";
            return MySqlDataAccess.ExecuteNonQuery(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable getUserIdentifyno(string userid)
    {
        try
        {
            string query = @"select UserIdentifyNo from t_User where UserID='" + userid + "'";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable getRequestFundTransfer(string userid)
    {
        try
        {
            string query = @"select UserID, Amount, RequestDate, RequetType, RequestStatus, Request, UserIdetifyno, TransferDate,  SystemIP, AccountNo from AmountRequest WHERE UserID='" + userid + "' AND RequetType='Fund Transfer' order by RequstID DESC";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public DataTable getRequestPayWalletAmount(string userid)
    {
        try
        {
            string query = @"select UserID, Amount, RequestDate, RequetType, RequestStatus, Request, UserIdetifyno, TransferDate,  SystemIP, AccountNo from AmountRequest WHERE UserID='" + userid + "' AND RequetType='Pay Amount' order by RequstID DESC";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }


    public DataTable getNoOfRequestFundTransfer()
    {
        try
        {
            string query = @"select COUNT(RequstID)as norequest from AmountRequest where Request='False' AND RequetType='Fund Transfer'";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }


    public DataTable getMemberAmountRequest()
    {
        try
        {
            string query = @"select r.RequstID, r.UserID, r.RequestDate, (r.Amount)as reqamount, r.Request,
r.RequestStatus, r.UserIdetifyno,
(u.Firstname+' '+u.Lastname)as name, w.Amount from AmountRequest r inner join t_User u on r.UserID= u.UserId
inner join Wallet w on u.UserId =w.UserId where RequetType='Fund Transfer' order by r.RequstID DESC";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public DataTable getMemberAmountRequest1(int pageIndex, int pageSize, out int recordCount)
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

    public DataTable getMemberPayAmountRequest()
    {
        try
        {
            string query = @"select r.RequstID, r.UserID, r.RequestDate, (r.Amount)as reqamount, r.Request,
r.RequestStatus, r.UserIdetifyno, r.AccountNo, r.PaymentType,
(u.Firstname+' '+u.Lastname)as name, w.Amount from AmountRequest r inner join t_User u on r.UserID= u.UserId
inner join Wallet w on u.UserId= w.UserId where RequetType='Pay Amount' order by r.RequstID DESC";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public DataTable getMemberPayAmountRequest1(int pageIndex, int pageSize, out int recordCount)
    {
        try
        {
            string conn = MySqlDataAccess.ConnectionString;
            SqlConnection con = new SqlConnection(conn);
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "GetPayAmountRequest";
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


    public int UpdateCustomerFundTransferStatus(bool status, decimal transferamount, long accountno, string requeststatus, string requestid)
    {
        try
        {
            string query = "update AmountRequest set Request='" + status + "', TransferAmount='" + transferamount + "', AccountNo='" + accountno + "', TransferDate=GetDate(), RequestStatus='" + requeststatus + "' where RequstID='" + requestid + "'";
            return MySqlDataAccess.ExecuteNonQuery(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public int UpdateCustomerPayWalletStatus(bool status, decimal transferamount, long transactionno, string requeststatus, string requestid)
    {
        try
        {
            string query = "update AmountRequest set Request='" + status + "', TransferAmount='" + transferamount + "', AccountNo='" + transactionno + "', TransferDate=GetDate(), RequestStatus='" + requeststatus + "' where RequstID='" + requestid + "'";
            return MySqlDataAccess.ExecuteNonQuery(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }


    public DataTable getToFixWalletAmount(string userid)
    {
        try
        {
            string query = @"select top 1 w.WalletId, w.Amount, FixId, FixAmount, AddDate, f.UserID, f.Percentage 
from Wallet w left join FixedAmount f on w.UserId = f.UserID
where w.UserId='" + userid + "' order by f.FixId desc";

            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }


    public int AddNewFixAmount(decimal fixamount, decimal addamount, string useridentify, Int32 userid, decimal percentage)
    {
        try
        {
            string query = "insert into FixedAmount(FixAmount, AddDate, addAmount, UserIdentify, UserID, Percentage) values('" + fixamount + "', GetDate(), '" + addamount + "', '" + useridentify + "', '" + userid + "', '" + percentage + "')";
            return MySqlDataAccess.ExecuteNonQuery(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public int AddNewFixAmountFirst(decimal fixamount, decimal addamount, string useridentify, string monthcount, Int32 userid, decimal percentage)
    {
        try
        {
            string query = "insert into FixedAmount(FixAmount, AddDate, addAmount, UserIdentify, MonthBegin, UserID, Percentage) values('" + fixamount + "', GetDate(), '" + addamount + "', '" + useridentify + "', '" + monthcount + "', '" + userid + "', '" + percentage + "')";
            return MySqlDataAccess.ExecuteNonQuery(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public int DeductNewFixAmount(decimal fixamount, decimal deductamount, string useridentify, Int32 userid, decimal percentage)
    {
        try
        {
            string query = "insert into FixedAmount(FixAmount, AddDate, deductAmount, UserIdentify, UserID, Percentage) values('" + fixamount + "', GetDate(), '" + deductamount + "', '" + useridentify + "', '" + userid + "', '" + percentage + "')";
            return MySqlDataAccess.ExecuteNonQuery(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }


    public string IsFixExists(int userid)
    {
        string FLAG = "F";
        DataTable dt = new DataTable();
        dt = MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, "select UserID, FixAmount from FixedAmount where UserId='" + userid + "'");

        if (dt.Rows.Count > 0)
        {
            FLAG = "T";
        }
        return FLAG;
    }

    public DataTable getFixAmountDetail(string userid)
    {
        try
        {
            string query = @"select FixId, FixAmount, AddDate, addAmount, deductAmount, UserIdentify, UserID from FixedAmount where UserId='" + userid + "' order by FixId DESC";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    

    public int AddAdminBallance(BEL obj_d)
    {
        try
        {
            string query = "insert into WalletDetails(WalletId,UserId, Amount,AddedAmount, AddedDate,ActionBy) values('" + obj_d.WalletId + "','" + obj_d.UserId + "','" + obj_d.Amount + "','" + obj_d.AddedAmount + "',GETDATE(),'" + obj_d.ActionBy + "')";
            return MySqlDataAccess.ExecuteNonQuery(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public int UpdateAdminBallance(decimal amount, int user)
    {
        try
        {
            string query = "update Wallet set Amount= (Amount+" + amount + ") where WalletId=1 AND UserId=" + user + " AND wallStatus='Active'";
            return MySqlDataAccess.ExecuteNonQuery(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable getAdminWalletDetailsByDate(string date1, string date2, string userid)
    {
        try
        {
            string query = "";
            if (date1 != "" && date2 != "")
            {
                query = @"select WdID, WalletId, UserId, Amount, (Amount + ISNULL(AddedAmount,0) - ISNULL(DeductedAmount, 0))as totamount, AddedAmount, AddedDate, WalletStatus, ActionBy from WalletDetails WHERE Cast(AddedDate AS DATE) >='" + date1 + "' AND Cast(AddedDate AS DATE)<= '" + date2 + "' AND UserId='" + userid + "' AND WalletId=1 order by AddedDate";
            }
            else
            {
                query = @"select WdID, WalletId, UserId, Amount, (Amount + ISNULL(AddedAmount,0) - ISNULL(DeductedAmount, 0))as totamount, AddedAmount, AddedDate, WalletStatus, Actionby from WalletDetails WHERE Cast(AddedDate AS DATE) ='" + date1 + "' AND UserId='" + userid + "' AND WalletId=1 order by AddedDate";
            }
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }


    public DataTable getBeneficiaryDetail()
    {
        try
        {
            string query = @"select BeneficiaryType, BankAccNo, ConfirmBankAccNo, BeneficiaryName, SenderMobile, IfscCode, CustomerId, CustIdentifyno, Tstatus, BeneDate from APITransaction order by RTID DESC";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public DataTable getBeneficiaryDetailByID(string useridenty)
    {
        try
        {
            string query = @"select RTID, BeneficiaryType, BankAccNo, ConfirmBankAccNo, BeneficiaryName, SenderMobile, IfscCode, CustomerId, CustIdentifyno, Tstatus,BeneDate from APITransaction where CustIdentifyno='" + useridenty + "' order by RTID DESC";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public DataTable getBeneficiaryDetailByDate(string date1, string date2)
    {
        try
        {
            string query = "";
            if (date1 != "" && date2 != "")
            {
                query = @"select BeneficiaryType, BankAccNo, ConfirmBankAccNo, BeneficiaryName, SenderMobile, IfscCode, CustomerId, CustIdentifyno, Tstatus, BeneDate from APITransaction where Cast(BeneDate AS DATE) >='" + date1 + "' AND Cast(BeneDate AS DATE)<= '" + date2 + "' order by BeneDate";
            }
            else
            {
                query = @"select BeneficiaryType, BankAccNo, ConfirmBankAccNo, BeneficiaryName, SenderMobile, IfscCode, CustomerId, CustIdentifyno, Tstatus, BeneDate from APITransaction where Cast(BeneDate AS DATE) >='" + date1 + "' order by BeneDate";
            }
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }


    public DataTable getProductReviewActive()
    {
        try
        {
            string query = "select RatingId,ProductId,Rating,Name,Emailid,Review,ReviewDate,(case when active=0 then 'Inactive' when active=1 then 'Active' end)as active from t_RatingDetail order by RatingId DESC";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public DataTable UpdateProductReview(bool active, string rid)
    {
        try
        {
            string query = "update t_RatingDetail set Active='" + active + "' where RatingId='" + rid + "'";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public int UpdatePercentage(decimal percentage)
    {
        try
        {
            string query = "update [Percent] set Percentage='" + percentage + "' where pid >=1";
            return MySqlDataAccess.ExecuteNonQuery(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable getfixPercentage()
    {
        try
        {
            string query = @"select FixId, f.UserID, FixAmount, addAmount, AddDate, Percentage, deductAmount, 
UserIdentify, convert(decimal(18, 3), FixAmount +(FixAmount * Percentage/100))as TotalAmount from FixedAmount f 
inner join t_User u on f.UserID = u.UserId where f.FixId in (select MAX(FixId) 
from FixedAmount where UserID=f.UserId)";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public DataTable getPercentagelbl()
    {
        try
        {
            string query = @"select * from [Percent]";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable getfixeAmountReport(string customerid)
    {
        try
        {
            string query = "select * from FixedAmount where UserID='" + customerid + "' order by FixId DESC";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable getfixAmountReports()
    {
        try
        {
            string query = @"select CalcID, FixAmount, CalcAmount, NoOfday, date, TotalBalance, UserIdentifyNo, (Firstname+' '+Lastname)as name from FixedAmountReport f inner join t_User u on f.UserId = u.UserId";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public DataTable getfixAmountReportsearch(string fixesearch)
    {
        try
        {
            string query = @"select CalcID, FixAmount, CalcAmount, NoOfday, date, TotalBalance, UserIdentifyNo, (Firstname+' '+Lastname)as name from FixedAmountReport f inner join t_User u on f.UserId = u.UserId where UserIdentifyNo='" + fixesearch + "'";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public DataTable getfixAmountReportseacheByDate(string date1, string date2)
    {
        try
        {
            string query = "";
            if (date1 != "" && date2 != "")
            {
                query = @"select CalcID, FixAmount, CalcAmount, NoOfday, date, TotalBalance, UserIdentifyNo, (Firstname+' '+Lastname)as name from FixedAmountReport f inner join t_User u on f.UserId = u.UserId where Cast(date AS DATE) >='" + date1 + "' AND Cast(date AS DATE)<= '" + date2 + "' order by date";
            }
            else
            {
                query = @"select CalcID, FixAmount, CalcAmount, NoOfday, date, TotalBalance, UserIdentifyNo, (Firstname+' '+Lastname)as name from FixedAmountReport f inner join t_User u on f.UserId = u.UserId where Cast(date AS DATE) ='" + date1 + "'";
            }
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }



    public DataTable getfixeAmountReportSerch(string useridenty, string custmerid)
    {
        try
        {
            string query = @"select * from FixedAmount where UserIdentify='" + useridenty + "' and UserID='"+custmerid+"' order by FixId DESC";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable getfixeAmountReportSerchByDate(string date1, string date2, string cutomerid)
    {
        try
        {
            string query = "";
            if (date1 != "" && date2 != "")
            {
                query = @"select * from FixedAmount where Cast(AddDate AS DATE) >='" + date1 + "' AND Cast(AddDate AS DATE)<= '" + date2 + "' and UserID='" + cutomerid + "' order by FixId DESC";
            }
            else
            {
                query = @"select * from FixedAmount where Cast(AddDate AS DATE) ='" + date1 + "' and UserID='" + cutomerid + "'";
            }
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }


    public DataTable getPercentage1()
    {
        try
        {
            string query = "select * from [Percent]";
            return MySqlDataAccess.ExecuteDataTable(MySqlDataAccess.ConnectionString, CommandType.Text, query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}