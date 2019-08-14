<%@ Page Title="" Language="C#" MasterPageFile="~/admin/AdminPage.master" AutoEventWireup="true" CodeFile="MemberAmountRequest.aspx.cs" Inherits="admin_MemberAmountRequest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<style type="text/css">
table.Request{ margin:0 auto 0 auto; width:100%; background-color:#FFFFFF; padding:0; border:solid 2px #999; border-collapse: collapse;}
.Request td{ padding:10px; margin:0; vertical-align:middle; border: solid 1px #D7D7D7;}
.Request th{ padding:10px; margin:0; vertical-align:middle; border: solid 1px #999;}

.page_enabled, .page_disabled
{
    display: inline-block;
    height: 20px;
    min-width: 20px;
    line-height: 20px;
    text-align: center;
    text-decoration: none;
    border: 1px solid #ccc;
}
.page_enabled
{
    background-color: #eee;
    color: #000;
}
.page_disabled
{
    background-color: #6C6C6C;
    color: #fff !important;
}
.pagerp{ margin:10px auto 0 auto; padding:0; text-align:center; vertical-align:top; width:98%;}
.minheight{ min-height:440px;}
</style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="minheight">
<h2 class="text-center">Member Amount Request</h2>
<p class="text-center">
<asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>&nbsp;&nbsp;&nbsp;
<asp:ImageButton ID="ImageButton1" ImageUrl="~/img/excel.png" 
            runat="server" onclick="ImageButton1_Click" Width="29px" Height="29px" />
</p>

<div class="table-responsive">
    
    <table class="Request">
    <tr>
    <th>S.No.</th><th>User ID</th><th>Member Name</th><th>Wallet</th><th>Request Date</th><th>Request Amount</th><th>Transfer Status</th>
    </tr>
    <asp:Repeater ID="rpAmountRequests" runat="server" 
            onitemcommand="rpAmountRequests_ItemCommand" 
            onitemdatabound="rpAmountRequests_ItemDataBound">
    <ItemTemplate>
    <tr>
    <td>
        <%# Container.ItemIndex +1 %>
        <asp:Label ID="lblUserid" runat="server" Text='<%# Eval("UserID")%>' Visible="false"></asp:Label>
    </td>
    <td><%# Eval("UserIdetifyno")%></td>
    <td><%# Eval("name")%></td><td><%# Eval("Amount")%></td>
    <td><%# Eval("RequestDate", "{0:dd/MM/yyyy}")%></td>
    <td>
        <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("reqamount")%>'></asp:Label></td>
    <td>
        <asp:Label ID="lblstatus" runat="server" Text='<%# Bind("Request") %>' Visible="False"></asp:Label>
        <asp:LinkButton ID="lnkFundTransfer" runat="server" CommandArgument='<%# Eval("RequstID") %>' CommandName="FundTransfer" 
         Text="Fund Transfer" CssClass="btn2 btn-success btn-sm" Font-Bold="true"></asp:LinkButton>
        <asp:LinkButton ID="lnkTransferDone" runat="server" Text="Transfer Done" CssClass="btn2 btn-warning btn-sm" 
         Font-Bold="true"></asp:LinkButton>
    </td>
    </tr>
    </ItemTemplate>
    </asp:Repeater>
    </table>
    <div class="pagerp">
    <asp:Repeater ID="rptPager" runat="server">
    <ItemTemplate>
        <asp:LinkButton ID="lnkPage" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>'
            CssClass='<%# Convert.ToBoolean(Eval("Enabled")) ? "page_enabled" : "page_disabled" %>'
            OnClick="Page_Changed" OnClientClick='<%# !Convert.ToBoolean(Eval("Enabled")) ? "return false;" : "" %>'></asp:LinkButton>
    </ItemTemplate>
    </asp:Repeater>
    </div>
</div>
</div>
</asp:Content>
