<%@ Page Title="" Language="C#" MasterPageFile="~/admin/AdminPage.master" AutoEventWireup="true" CodeFile="UpdateFixedAmount.aspx.cs" Inherits="admin_UpdateFixedAmount" %>
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
    <asp:PostBackTrigger ControlID="ImageButton1" />
    </Triggers>
    <ContentTemplate>
 <div class="row">
    <div class="col-lg-6 col-md-8 col-sm-8 col-lg-offset-3 col-sm-offset-2">
        <div class="main-form full">
        <div class="row">
            <div class="col-xs-12 mb-20">
            <div class="heading-part heading-bg">
                <h2 class="heading">Update Fixed Amount</h2>
            </div>
            <asp:Label ID="lblMessage" CssClass="message2" runat="server" Text=""></asp:Label>
            </div>
            <div class="col-sm-6">
            <div class="input-box">
                <label for="login-pass">Amount In (%)</label>
                <asp:TextBox ID="txtamount" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="g1" 
                    ControlToValidate="txtamount" ErrorMessage="* Percetage" ForeColor="Red" Display="Dynamic">
                </asp:RequiredFieldValidator>
                <cc1:FilteredTextBoxExtender runat="server" ID="FilteredTextBoxExtender1" TargetControlID="txtamount"
                     FilterType="Custom" Enabled="true" FilterMode="ValidChars" ValidChars="1234567890.">
                </cc1:FilteredTextBoxExtender>
            </div>
            </div>
            <div class="col-sm-6">
                <label>Updated Percentage</label>
                <asp:Label ID="lblPercentage" runat="server" Text=""></asp:Label>
            </div>
            <div class="col-xs-12">
            <br />
            <div class="left-side">
             <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="g1" 
                    class="btn-black" onclick="btnSubmit_Click"></asp:Button>
            </div>
            <%--<asp:Button ID="btnCancel" runat="server" Text="Cancel" 
                    class="btn-black right-side"></asp:Button>--%>
            
            </div>
        </div>
        </div>
    </div>
    </div>

    <br />
    <center><asp:ImageButton ID="ImageButton1" ImageUrl="~/img/excel.png" 
            runat="server" onclick="ImageButton1_Click" Width="29px" Height="29px" /></center>
    <br />
       <center> <asp:Label ID="Label1" runat="server" Text=""></asp:Label></center>
    <asp:GridView ID="gvWalletDetails" runat="server" GridLines="None" 
            CssClass="mGrid" PagerStyle-CssClass="pgr" AllowPaging="false" 
            AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False" 
            onrowcommand="gvWalletDetails_RowCommand">
        <Columns>
            <asp:TemplateField HeaderText="S.No.">
                <ItemTemplate>
                    <%# Container.DataItemIndex + 1 %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="UserIdentify" HeaderText="User ID" />
            <asp:BoundField DataField="Percentage" HeaderText="Percentage (%)" />
            <asp:BoundField DataField="FixAmount" HeaderText="Fixed Amount" />
            <asp:BoundField DataField="addAmount" HeaderText="Added Amount" />
            <asp:BoundField DataField="deductAmount" HeaderText="Detach Amount" />
            <asp:BoundField DataField="AddDate" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Date" />
            <asp:BoundField DataField="TotalAmount" HeaderText="Total" />
            <asp:TemplateField HeaderText="Details">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkFixeDetail" CommandName="Details" CommandArgument='<%# Eval("UserID") %>' runat="server">Fix Detail</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
        <HeaderStyle Height="32px" />
        <PagerStyle CssClass="pgr"></PagerStyle>
        </asp:GridView>
 </ContentTemplate>
 </asp:UpdatePanel>
</div>
</asp:Content>
