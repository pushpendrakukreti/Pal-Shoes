<%@ Page Title="" Language="C#" MasterPageFile="~/admin/AdminPage.master" AutoEventWireup="true" CodeFile="FundTransfer2.aspx.cs" Inherits="admin_FundTransfer2" %>
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
        .Acdtails{ width:100%; margin:0; padding:0;}

  #ContentPlaceHolder1_DataList1{ width:100%; }
  table.beneAC{ width:100%; margin:0 auto 0 auto; padding:0; border:solid 1px #00B0F0; border-collapse:collapse;}
  .beneAC td{ padding:10px; vertical-align:top; }
  .bor1{ border-left:solid 1px #00B0F0;}
  .bor3{border-bottom:solid 1px #00B0F0;}
  .bor2{ border-bottom:solid 1px #DEDEDE;}
  .red{ color:Red; font-size:12px;}
  
  
  .modalBackground
    {
        background-color: Black;
        filter: alpha(opacity=40);
        opacity: 0.4;
    }
    .modalPopup
    {
        background-color: #FFFFFF;
        width: 300px;
        border: 3px solid #0DA9D0;
    }
    .modalPopup .header
    {
        background-color: #2FBDF1;
        height: 30px;
        color: White;
        line-height: 30px;
        text-align: center;
        font-weight: bold;
        padding:2px;
    }
    .modalPopup .body
    {
        min-height: 50px;
        line-height: 30px;
        text-align: center;
        font-weight: bold;
        padding:20px 0 10px 0;
    }
    .modalPopup .footer
    {
        padding: 1px;
    }
    .modalPopup .yes, .modalPopup .no
    {
        height: 23px;
        color: White;
        line-height: 23px;
        text-align: center;
        font-weight: bold;
        cursor: pointer;
    }
    .modalPopup .yes
    {
        background-color: #2FBDF1;
        border: 1px solid #0DA9D0;
    }
    .modalPopup .no
    {
        background-color: #9F9F9F;
        border: 1px solid #5C5C5C;
    }
    .footer1 { margin:0; padding:5px; text-align:center; color:Black; background-color:#FFFFFF;}
    #txtCode{ width:150px; height:20px;}
    #btnVerify{width:100px;}
    #btnNo{width:70px;}

  .minheight{ min-height:440px;}
  </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="minheight">
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <Triggers>
    <asp:PostBackTrigger ControlID="btnSubmit" />
    </Triggers>
    <ContentTemplate>
 <div class="row">
    <div class="col-lg-6 col-md-8 col-sm-8 col-lg-offset-3 col-sm-offset-2">
        <div class="main-form full">
        <div class="row">
            <div class="col-xs-12 mb-20">
            <div class="heading-part heading-bg">
                <h2 class="heading">Fund Transfer</h2>
            </div>
                <h3 class="text-center"><asp:Label ID="lblAPIbalance" runat="server" Text=""></asp:Label></h3>
                <asp:Label ID="lblMessage" CssClass="message2" runat="server" Text=""></asp:Label>
            </div>
            <div class="col-xs-12">
            <div class="Acdtails">
                <asp:DataList ID="DataList1" runat="server">
                <ItemTemplate>
                <table class="beneAC">
                <tr>
                <td class="bor2">Beneficiary Name</td><td class="bor2">: <%# Eval("BeneficiaryName")%></td>
                <td class="bor1 bor2">User Identify No</td><td class="bor2">: <%# Eval("CustIdentifyno")%></td>
                </tr>
                <tr>
                <td class="bor2">Account Number</td><td class="bor2">: <%# Eval("BankAccNo")%></td>
                <td class="bor1 bor2">Mobile Number</td><td class="bor2">: <%# Eval("SenderMobile")%></td>
                </tr>
                <tr>
                <td class="bor3">IFSC Code</td><td class="bor3">: <%# Eval("IfscCode")%></td>
                <td class="bor1 bor3">Beneficiay Type</td><td class="bor3">: <%# Eval("BeneficiaryType")%></td>
                </tr>
                </table>
                </ItemTemplate>
                </asp:DataList>
            </div>
            </div>
            <div class="col-sm-6">
            <br />
            <div class="input-box">
                <label for="f-name">Transfer Amount</label>
                <asp:TextBox ID="txtAmount" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                 ErrorMessage="* Amount" ControlToValidate="txtAmount" Display="Dynamic" 
                    ValidationGroup="g1" ForeColor="Red"></asp:RequiredFieldValidator>
                <cc1:FilteredTextBoxExtender runat="server" ID="FilteredTextBoxExtender2" TargetControlID="txtAmount"
                     FilterType="Custom" Enabled="true" FilterMode="ValidChars" ValidChars="1234567890.">
                </cc1:FilteredTextBoxExtender>
            </div>
            </div>
            <div class="col-sm-6">
            <br />
            <div class="input-box">
                <%--<label for="f-name">Transfer OT&nbsp;P</label>
                <asp:TextBox ID="txtOtp" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                 ErrorMessage="* Enter your transfer OTP" ControlToValidate="txtOtp" Display="Dynamic" 
                    ValidationGroup="g1" ForeColor="Red"></asp:RequiredFieldValidator>--%>
            </div>
            </div>
            <div class="col-xs-12">
            <br />
            <p class="red">By clicking on the "Confirm & Transfer" it have will tranfered amount to customer account that has completed the registration process. 
            We urge You to take carefully now so that You are informed that is added beneficiary account details are successfully verify by Api.</p>
            </div>
            <div class="col-xs-12">
            <br />
            <asp:Button ID="btnSubmit" runat="server" Text="Confirm & Transfer" ValidationGroup="g1" 
                    class="btn-black right-side" onclick="btnSubmit_Click"></asp:Button>
            </div>
        </div>
        </div>
    </div>
    </div>
 </ContentTemplate>
 </asp:UpdatePanel>


  <asp:HiddenField ID="HiddenField1" runat="server"></asp:HiddenField>
        <cc1:ModalPopupExtender ID="mpe" runat="server" PopupControlID="pnlPopup" TargetControlID="HiddenField1" 
            CancelControlID="btnNo" BackgroundCssClass="modalBackground">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="pnlPopup" runat="server" CssClass="modalPopup" Style="display: none">
            <div class="body">
                <asp:Label ID="lblError" runat="server" Text=""></asp:Label><br />
                <label runat="server" id="code">Enter OTP:</label> <asp:TextBox ID="txtCode" runat="server"></asp:TextBox>
            </div>
            <div class="footer1" align="right">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnVerify" runat="server" OnClick="btnVerify_Click" Text="Verify OTP" />
                &nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnNo" runat="server" Text="Close" />
            </div>
        </asp:Panel>
 </div>
</asp:Content>
