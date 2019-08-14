<%@ Page Title="" Language="C#" MasterPageFile="~/admin/AdminPage.master" AutoEventWireup="true" CodeFile="AccountRegistration.aspx.cs" Inherits="AccountRegistration" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<style type="text/css">
        body
        {
            font-family: Arial;
            font-size: 10pt;
        }
        .modalBackground
        {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }
        .modalPopup
        {
            background-color: #fff;
            border: 3px solid #ccc;
            padding: 10px;
            width: 300px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <Triggers>
    <asp:PostBackTrigger ControlID="btnSubmit" />
    <asp:AsyncPostBackTrigger ControlID="btnOTP" />
    </Triggers>
    <ContentTemplate>
 <div class="row">
    <div class="col-lg-6 col-md-8 col-sm-8 col-lg-offset-3 col-sm-offset-2">
        <div class="main-form full">
        <div class="row">
            <div class="col-xs-12 mb-20">
            <div class="heading-part heading-bg">
                <h2 class="heading">Customer Account Registration</h2>
            </div>
            <asp:Label ID="lblMessage" CssClass="message2" runat="server" Text=""></asp:Label>
            </div>
            <div class="col-xs-12">
            <div class="input-box">
                <label for="f-name">User ID (Identify Number)</label>
                <asp:DropDownList ID="ddUserIDno" runat="server" AutoPostBack="true" 
                    onselectedindexchanged="ddUserIDno_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                ErrorMessage="* User Mobile No" ControlToValidate="ddUserIDno" Display="Dynamic" 
                ValidationGroup="g1" ForeColor="Red" InitialValue="Select User ID"></asp:RequiredFieldValidator>
            </div>
            </div>
            <div class="col-xs-12">
            <br />
            <div class="input-box">
                <label for="f-name">First Name</label>
                <asp:TextBox ID="txtFirstname" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                ErrorMessage="* First Name" ControlToValidate="txtFirstname" Display="Dynamic" 
                ValidationGroup="g1" ForeColor="Red"></asp:RequiredFieldValidator>
            </div>
            </div>
            <div class="col-xs-12">
            <br />
            <div class="input-box">
                <label for="f-name">Last Name</label>
                <asp:TextBox ID="txtLastname" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                ErrorMessage="* Last Name" ControlToValidate="txtLastname" Display="Dynamic" 
                ValidationGroup="g1" ForeColor="Red"></asp:RequiredFieldValidator>
            </div>
            </div>
            <div class="col-xs-12">
            <br />
            <div class="input-box">
                <label for="login-pass">Mobile Number</label>
                <asp:TextBox ID="txtMobileno" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="g1" 
                    ControlToValidate="txtMobileno" ErrorMessage="* Mobile Number" ForeColor="Red" Display="Dynamic">
                </asp:RequiredFieldValidator>
                <cc1:FilteredTextBoxExtender runat="server" ID="FilteredTextBoxExtender1" TargetControlID="txtMobileno"
                     FilterType="Custom" Enabled="true" FilterMode="ValidChars" ValidChars="1234567890">
                </cc1:FilteredTextBoxExtender>
            </div>
            </div>
            <div class="col-xs-12">
            <br />
            <asp:LinkButton ID="linkVerify" onclick="linkVerify_Click" runat="server"><b>Verify OTP</b></asp:LinkButton>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:LinkButton ID="lnkResedOtp" onclick="lnkResedOtp_Click" runat="server"><b>Resend OTP</b></asp:LinkButton>
            <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="g1" 
                    class="btn-black right-side" onclick="btnSubmit_Click"></asp:Button>
            </div>
            <div class="col-xs-12">
            <br />
            <p class="text-center"><asp:LinkButton ID="linkBeneficiary" onclick="linkBeneficiary_Click" runat="server"><big><u><b>Add New Beneficiary Ac</b></u></big></asp:LinkButton></p>
            </div>

        <cc1:ModalPopupExtender ID="ModalPopupExtender1" BehaviorID="mpe" runat="server"
            PopupControlID="pnlPopup" TargetControlID="linkVerify" BackgroundCssClass="modalBackground" CancelControlID ="btnHide">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="pnlPopup" runat="server" CssClass="modalPopup" Style="display: none">
            <div class="header">
                <b>Customer Registration</b>
            </div>
            <div class="body">
                Enter Registration OTP :
                <asp:TextBox ID="txtOtp" runat="server"></asp:TextBox>
                <br />
                <asp:Button ID="btnOTP" runat="server" CssClass="btn-default" onclick="btnOTP_Click" Text="VERIFY OTP" /> 
                <asp:Button ID="btnHide" runat="server" Text="CLOSE" CssClass="btn-default" />
            </div>
        </asp:Panel>
        </div>
        </div>
    </div>
    </div>
 </ContentTemplate>
 </asp:UpdatePanel>
</asp:Content>
