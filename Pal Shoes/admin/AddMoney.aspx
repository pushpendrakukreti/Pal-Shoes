<%@ Page Title="" Language="C#" MasterPageFile="~/admin/AdminPage.master" AutoEventWireup="true" CodeFile="AddMoney.aspx.cs" Inherits="admin_AddAmount" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<style type="text/css">
.lblUserID{ text-align:center; margin:0 auto 0 auto;  font-size:medium; font-weight:bold;}
</style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <Triggers>
    <asp:PostBackTrigger ControlID="btnSubmit" />
    <asp:AsyncPostBackTrigger ControlID="ddPaymenttype" />
    </Triggers>
    <ContentTemplate>
 <div class="row">
    <div class="col-lg-6 col-md-8 col-sm-8 col-lg-offset-3 col-sm-offset-2">
        <div class="main-form full">
        <div class="row">
            <div class="col-xs-12 mb-20">
            <div class="heading-part heading-bg">
                <h2 class="heading">Add Money To Wallet</h2>
            </div>
            <asp:Label ID="lblMessage" CssClass="message2" runat="server" Text=""></asp:Label>
            </div>
            <div class="col-xs-12">
            <center><label>User ID :</label> <asp:Label ID="lblUserID" CssClass="lblUserID" runat="server" Text=""></asp:Label></center>
            <div class="input-box">
                <label for="f-name">Cutomer Full Name</label>
                <asp:TextBox ID="txtFulltname" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                ErrorMessage="Full Name" ControlToValidate="txtFulltname" Display="Dynamic" 
                ValidationGroup="g1" ForeColor="Red"></asp:RequiredFieldValidator>
            </div>
            </div>
            <div class="col-xs-12">
            <br />
            <div class="input-box">
                <label for="login-email">Email Address</label>
                <asp:TextBox ID="txtEmailid" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                ErrorMessage="Email Id" ControlToValidate="txtEmailid" Display="Dynamic" 
                ValidationGroup="g1" ForeColor="Red"></asp:RequiredFieldValidator>
            </div>
            </div>
            <div class="col-xs-12">
            <br />
            <div class="input-box">
                <label for="login-pass">Amount In Rupees</label>
                <asp:TextBox ID="txtamount" runat="server" placeholder="0" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="g1" 
                    ControlToValidate="txtamount" ErrorMessage="* Wallet Amount" ForeColor="Red" Display="Dynamic">
                </asp:RequiredFieldValidator>
                <asp:FilteredTextBoxExtender runat="server" ID="FilteredTextBoxExtender1" TargetControlID="txtamount"
                     FilterType="Custom" Enabled="true" FilterMode="ValidChars" ValidChars="1234567890.">
                </asp:FilteredTextBoxExtender>
            </div>
            </div>
            <div class="col-xs-12">
            <br />
            <div class="input-box">
                <label for="Payment">Payment Type</label>
                <asp:DropDownList ID="ddPaymenttype" runat="server" AutoPostBack="true" 
                    onselectedindexchanged="ddPaymenttype_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ValidationGroup="g1" 
                ControlToValidate="ddPaymenttype" ErrorMessage="* Payment Type" ForeColor="Red" Display="Dynamic"
                 InitialValue="Select Payment Type">
                </asp:RequiredFieldValidator>
            </div>
            </div>
            <div class="col-xs-12" id="trans" runat="server">
            <br />
            <div class="input-box">
                <label for="transactionno">Cheque/Transaction No.</label>
                <asp:TextBox ID="txtTransactionNo" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            </div>
            <div class="col-xs-12">
            <br />
            <div class="input-box">
                <label for="remarks">Remarks</label>
                <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            </div>
            <div class="col-xs-12">
            <br />
            <div class="left-side">
             <asp:Button ID="btnCancel" runat="server" Text="Cancel" 
                    class="btn-black right-side" onclick="btnCancel_Click"></asp:Button>
            </div>
            <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="g1" 
                    class="btn-black right-side" onclick="btnSubmit_Click"></asp:Button>
            </div>
        </div>
        </div>
    </div>
    </div>
 </ContentTemplate>
 </asp:UpdatePanel>
</asp:Content>

