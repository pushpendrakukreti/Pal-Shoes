<%@ Page Title="" Language="C#" MasterPageFile="~/admin/AdminPage.master" AutoEventWireup="true" CodeFile="AddnewFlavour.aspx.cs" Inherits="admin_AddnewFlavour" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<style>
.minheight{ min-height:440px;}
</style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="minheight">
<div class="row">
    <div class="col-lg-6 col-md-8 col-sm-8 col-lg-offset-3 col-sm-offset-2">
        <div class="main-form full">
        <div class="row">
            <div class="col-xs-12 mb-20">
            <div class="heading-part heading-bg">
                <h2 class="heading">Add New Flavour</h2>
            </div>
            <asp:Label ID="lblMessage" CssClass="message2" runat="server" Text=""></asp:Label>
            </div>
            <div class="col-xs-12">
            <div class="input-box">
                <label for="f-name">Flavour Name</label>
                <asp:TextBox ID="txtFlavour" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="g1" 
                    ControlToValidate="txtFlavour" ErrorMessage="* Flavour Name" ForeColor="Red" Display="Dynamic">
                </asp:RequiredFieldValidator>
            </div>
            </div>
            <div class="col-xs-12">
            <br />
            <asp:Button ID="btnAddFlavour" runat="server" Text="Add Flavour" class="btn-black" 
                    ValidationGroup="g1" onclick="btnAddFlavour_Click" />
            <asp:Button ID="btnCancel" runat="server" Text="Back to Product" 
                    class="btn-black right-side" onclick="btnCancel_Click"></asp:Button>
            </div>
        </div>
    </div>
    </div>
    </div>
    </div>
</asp:Content>

