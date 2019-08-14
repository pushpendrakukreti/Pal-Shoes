<%@ Page Title="" Language="C#" MasterPageFile="~/admin/AdminPage.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="admin_login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
table.login
{
    margin:20px auto 0 auto; padding:0px; width:60%; min-width:420px;
    border:1px solid #fc9e2c;
    border-radius:5px;-moz-border-radius:5px;-webkit-border-radius:5px;-o-border-radius:5px;
}
.login td
{
    padding:10px; vertical-align:top; text-align:left;
}
.login td:nth-child(1){ width:50%; text-align:center;}
.login td:nth-child(2){ width:50%;}

.minheight{ min-height:440px;}
</style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="minheight">
	<div class="col-sm-12">
        <table class="login">
        <tr>
        <td colspan="2">
        <h2 class="text-center">Admin Login</h2>
            <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
        </td>
        </tr>
		<tr>
			<td>Username</td>
			<td><asp:TextBox ID="txtUserName" runat="server" CssClass="login-inp"></asp:TextBox></td>
		</tr>
		<tr>
			<td>Password</td>
			<td><asp:TextBox ID="txtPassword" TextMode="Password" runat="server" CssClass="login-inp"></asp:TextBox>
			</td>
		</tr>
		<tr>
			<td>
                <input type="checkbox" class="checkbox-size" id="login-check" />
                <label for="login-check">Remember me</label>
            </td>
			<td><asp:Button ID="btbLogin" runat="server" Text="Login" CssClass="submit-login" 
                    onclick="btbLogin_Click" /></td>
		</tr>
        <tr>
        <td>&nbsp;</td><td></td>
        </tr>
	</table>
</div>
</div>
</asp:Content>

