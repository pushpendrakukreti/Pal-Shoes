<%@ Page Title="" Language="C#" MasterPageFile="~/admin/AdminPage.master" Culture="en-GB" AutoEventWireup="true" CodeFile="AdminWallet.aspx.cs" Inherits="admin_AdminWallet" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<style type="text/css">
.lblUserID{ text-align:center; margin:0 auto 0 auto;  font-size:medium; font-weight:bold;}

.mGrid { width: 100%; background-color: #fff; margin:0px 1px 2px 1px; border: solid 1px #525252; border-collapse:collapse; }
.mGrid td { padding: 7px; border: solid 1px #c1c1c1; color: #717171;font-size: 14px;}
.mGrid th { padding: 7px; color: #fff; background: #424242 url(/images/grd_head.png) repeat-x top; border-left: solid 1px #525252; font-size:14px; }
.mGrid .alt { }
.mGrid .pgr {background: #424242 url(/images/grd_pgr.png) repeat-x top; height:32px;}
.mGrid .pgr table { margin: 5px 0; }
.mGrid .pgr td { border-width: 0; padding: 0 6px; border-left: solid 1px #666;  font-weight: bold; color: #fff; line-height: 12px; font-size:1.1em;}

.mGrid td a{ text-decoration:none; color:#0000FF;}
table.ord{ width:auto; margin:0 auto 0 auto;}
.ord td{ }
.minheight{ min-height:440px;}
</style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="minheight">
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <Triggers>
    <asp:PostBackTrigger ControlID="btnSubmit" />
    <asp:AsyncPostBackTrigger ControlID="btnGetDetails" />
    </Triggers>
    <ContentTemplate>
 <div class="row">
    <div class="col-lg-6 col-md-8 col-sm-8 col-lg-offset-3 col-sm-offset-2">
        <div class="main-form full">
        <div class="row">
            <div class="col-xs-12 mb-20">
            <div class="heading-part heading-bg">
                <h2 class="heading">Add Balance To Admin Wallet</h2>
            </div>
            <asp:Label ID="lblMessage" CssClass="message2" runat="server" Text=""></asp:Label>
            </div>
            <div class="col-sm-6">
            <div class="input-box">
                <label for="login-pass">Amount In Rupees</label>
                <asp:TextBox ID="txtamount" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="g1" 
                    ControlToValidate="txtamount" ErrorMessage="* Wallet Amount" ForeColor="Red" Display="Dynamic">
                </asp:RequiredFieldValidator>
                <cc1:FilteredTextBoxExtender runat="server" ID="FilteredTextBoxExtender1" TargetControlID="txtamount"
                     FilterType="Custom" Enabled="true" FilterMode="ValidChars" ValidChars="1234567890.">
                </cc1:FilteredTextBoxExtender>
            </div>
            </div>
            <div class="col-xs-12">
            <br />
            <div class="left-side">
             <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="g1" 
                    class="btn-black" onclick="btnSubmit_Click"></asp:Button>
            </div>
            <asp:Button ID="btnCancel" runat="server" Text="Cancel" 
                    class="btn-black right-side" onclick="btnCancel_Click"></asp:Button>
            
            </div>
        </div>
        </div>
    </div>
    </div>

    <br />
    <table class="ord">
    <tr>
    <td>
        Date: <asp:TextBox ID="txtDate1" runat="server" Width="170px"></asp:TextBox>
        <img src="~/img/Calendar.gif" id="img_date1" runat="server" alt="" />
        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="img_date1"
            TargetControlID="txtDate1" Format="dd/MM/yyyy">
        </cc1:CalendarExtender>
        <asp:CustomValidator ID="CustomValidator1" runat="server" ClientValidationFunction="ValidateDate" 
        ControlToValidate="txtDate1" ErrorMessage="dd/mm/yyyy" ValidationGroup="g2" ForeColor="Red" />
    </td>
    <td>
        To Date: <asp:TextBox ID="txtDate2" runat="server" Width="170px"></asp:TextBox>
        <img src="~/img/Calendar.gif" id="img_date2" runat="server" alt="" />
        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" PopupButtonID="img_date2"
            TargetControlID="txtDate2" Format="dd/MM/yyyy">
        </cc1:CalendarExtender>
        <asp:CustomValidator ID="CustomValidator2" runat="server" ClientValidationFunction="ValidateDate" 
        ControlToValidate="txtDate2" ErrorMessage="dd/mm/yyyy" ValidationGroup="g2" ForeColor="Red" />
    </td>
    <td>
        <asp:Button ID="btnGetDetails" CssClass="getbutton" runat="server" ValidationGroup="g2" 
            Text="Get Report" onclick="btnGetDetails_Click" />
    </td>
    </tr>
    </table>
    <br />
    <asp:GridView ID="gvWalletDetails" runat="server" GridLines="None" 
        CssClass="mGrid" PagerStyle-CssClass="pgr" AllowPaging="true" 
            AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False" 
         PageSize="20" onpageindexchanging="gvWalletDetails_PageIndexChanging">
        <Columns>
            <asp:TemplateField HeaderText="S.No.">
                <ItemTemplate>
                    <%# Container.DataItemIndex + 1 %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="totamount" HeaderText="Wallet Ballance" />
            <asp:BoundField DataField="AddedAmount" HeaderText="Added Amount" />
            <asp:BoundField DataField="AddedDate" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Add Date" />
            <asp:BoundField DataField="Actionby" HeaderText="Action By" />
        </Columns>
        <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
        <HeaderStyle Height="32px" />
        <PagerStyle CssClass="pgr"></PagerStyle>
    </asp:GridView>
    <br />
 </ContentTemplate>
 </asp:UpdatePanel>
 </div>
</asp:Content>
