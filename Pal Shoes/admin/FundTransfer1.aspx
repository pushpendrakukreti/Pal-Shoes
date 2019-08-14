<%@ Page Title="" Language="C#" MasterPageFile="~/admin/AdminPage.master" AutoEventWireup="true" CodeFile="FundTransfer1.aspx.cs" Inherits="admin_FundTransfer" %>
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
  table.beneAC{ width:100%; margin:20px auto 0 auto; background-color:White; padding:0; border:solid 1px #00B0F0; border-collapse:collapse;}
  .beneAC td{ padding:10px; vertical-align:top; }
  .bor1{ border-left:solid 1px #00B0F0;}
  .bor3{border-bottom:solid 1px #00B0F0;}
  .bor2{ border-bottom:solid 1px #DEDEDE;}
  
  .minheight{ min-height:440px;}
  </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="minheight">
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <Triggers>
    <asp:PostBackTrigger ControlID="btnSubmit" />
    <asp:AsyncPostBackTrigger ControlID="ddAcountno" />
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
                <h3 class="text-center"><asp:Label ID="lblAPIBalance" runat="server" Text=""></asp:Label></h3>
                <asp:Label ID="lblMessage" CssClass="message2" runat="server" Text=""></asp:Label>
                </div>
            <div class="col-sm-6">
            
            <div class="input-box">
                <label for="login-pass">Bene Account Number</label>
                <asp:DropDownList ID="ddAcountno" runat="server" AutoPostBack="True" 
                    onselectedindexchanged="ddAcountno_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="g1" 
                    ControlToValidate="ddAcountno" ErrorMessage="* Select Account No" ForeColor="Red" Display="Dynamic">
                </asp:RequiredFieldValidator>
            </div>
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
                <td class="bor2">IFSC Code</td><td class="bor2">: <%# Eval("IfscCode")%></td>
                <td class="bor1 bor2">Wallet Amount</td><td class="bor2">: <%# Eval("Amount")%></td>
                </tr>
                <tr>
                <td class="bor3">Beneficiay Type</td><td class="bor3">: <%# Eval("BeneficiaryType")%></td>
                <td class="bor1 bor3"></td><td class="bor3">: </td>
                </tr>
                </table>
                </ItemTemplate>
                </asp:DataList>
            </div>
            </div>
            
            <div class="col-sm-6">
            <br />
            <div class="input-box">
                <label for="f-name">Amount</label>
                <asp:TextBox ID="txtAmount" placeholder="0" runat="server"></asp:TextBox>
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
                <label for="f-name">Beneficiary Type</label>
                <asp:DropDownList ID="ddBeneType" runat="server">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                ErrorMessage="* Beneficiary Type" ControlToValidate="ddBeneType" Display="Dynamic" 
                ValidationGroup="g1" ForeColor="Red" InitialValue="Beneficiary Type"></asp:RequiredFieldValidator>
            </div>
            </div>
            <div class="col-xs-12">
            <br />
            <asp:Button ID="btnSubmit" runat="server" Text="Next ->" ValidationGroup="g1" 
                    class="btn-black right-side" onclick="btnSubmit_Click"></asp:Button>
            </div>

        </div>
        </div>
    </div>
    </div>
 </ContentTemplate>
 </asp:UpdatePanel>
 </div>
</asp:Content>
