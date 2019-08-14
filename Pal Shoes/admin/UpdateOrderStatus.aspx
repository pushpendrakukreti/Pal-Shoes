<%@ Page Title="" Language="C#" MasterPageFile="~/admin/AdminPage.master" Culture="en-GB" AutoEventWireup="true" CodeFile="UpdateOrderStatus.aspx.cs" Inherits="admin_UpdateOrderStatus" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<style type="text/css">
.lblUserID{ text-align:center; margin:0 auto 0 auto;  font-size:medium; font-weight:bold;}
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
                <h2 class="heading">Update Order Status</h2>
            </div>
            <asp:Label ID="lblMessage" CssClass="message2" runat="server" Text=""></asp:Label>
            </div>
            <div class="col-xs-12">
            <div class="input-box">
                <label for="f-name">Enter Order No.</label>
                <asp:TextBox ID="txtOrderNo" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                ErrorMessage="* Order No" ControlToValidate="txtOrderNo" Display="Dynamic" 
                ValidationGroup="g1" ForeColor="Red"></asp:RequiredFieldValidator>
            </div>
            </div>
            <div class="col-xs-12">
            <br />
            <div class="input-box">
                <label for="f-name">Dispatched Date</label>
                <asp:TextBox ID="txtDate1" runat="server"></asp:TextBox>
                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="txtDate1"
                    TargetControlID="txtDate1" Format="dd/MM/yyyy">
                </cc1:CalendarExtender>
                <asp:CustomValidator ID="CustomValidator1" runat="server" ClientValidationFunction="ValidateDate" 
                 ControlToValidate="txtDate1" ErrorMessage="dd/mm/yyyy" ValidationGroup="g1" ForeColor="Red" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                    ErrorMessage="* Dispatched Date" ControlToValidate="txtDate1" Display="Dynamic" 
                    ValidationGroup="g1" ForeColor="Red"></asp:RequiredFieldValidator>
            </div>
            </div>
            <div class="col-xs-12">
            <div class="input-box">
                <label for="login-pass">Order Status</label>
                <asp:DropDownList ID="ddOrderStatus" runat="server">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="g1" 
                    ControlToValidate="ddOrderStatus" InitialValue="Select Order Status" ErrorMessage="* Select Order Status" ForeColor="Red" Display="Dynamic">
                </asp:RequiredFieldValidator>
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
 </div>
</asp:Content>

