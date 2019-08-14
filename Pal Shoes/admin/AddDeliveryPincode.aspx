<%@ Page Title="" Language="C#" MasterPageFile="~/admin/AdminPage.master" AutoEventWireup="true" CodeFile="AddDeliveryPincode.aspx.cs" Inherits="admin_AddDeliveryPincode" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<style type="text/css">
table.category{ margin:0px 0px 0px 0px; padding:0; border:none; width:100%;}
.category td
{
    padding:7px; vertical-align:top; margin:0;
}

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

</style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <Triggers>
    <asp:PostBackTrigger ControlID="btnCheckDelivery" />
    </Triggers>
    <ContentTemplate>
 <div class="row">
    <div class="col-lg-6 col-md-8 col-sm-8 col-lg-offset-3 col-sm-offset-2">
        <div class="main-form full">
        <div class="row">
            <div class="col-xs-12 mb-20">
            <div class="heading-part heading-bg">
                <h2 class="heading">Add Delivery Postcode</h2>
            </div>
            <asp:Label ID="lblMessage" CssClass="message2" runat="server" Text=""></asp:Label>
            </div>
            <div class="col-xs-12">
            <div class="input-box mb-20">
                <asp:DropDownList ID="ddCountry" runat="server">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" 
                ErrorMessage="Country" ControlToValidate="ddCountry" Display="Dynamic" 
                ValidationGroup="d1" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
            </div>
            </div>
            <div class="col-xs-12">
            <div class="input-box mb-20">
                <asp:DropDownList ID="ddState" runat="server">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" 
                    ErrorMessage="State" ControlToValidate="ddState" Display="Dynamic" 
                    ValidationGroup="d1" ForeColor="Red" InitialValue=""></asp:RequiredFieldValidator>
            </div>
            </div>
            <div class="col-xs-12">
            <div class="input-box mb-20">
                <asp:TextBox ID="txtPostcodezip" Height="35px" placeholder="Postcode/zip" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" 
                    ErrorMessage="* Postcode/zip" ControlToValidate="txtPostcodezip" Display="Dynamic" 
                    ValidationGroup="d1" ForeColor="Red"></asp:RequiredFieldValidator>
               </div>
            </div>
            <div class="col-xs-12">
            <div class="left-side">
             <asp:Button ID="btnCancel" runat="server" Text="Cancel" 
                    class="btn-black right-side"></asp:Button>
            </div>
            <div class="input-box mb-20">
            <asp:Button ID="btnCheckDelivery" class="btn-black right-side" runat="server" ValidationGroup="d1" 
                            Text="Add Postalcode" onclick="btnCheckDelivery_Click"></asp:Button>
            </div>
            </div>
        </div>
        </div>
        <br />
        <p class="text-center">
        <asp:Label ID="Label1" runat="server" Text=""></asp:Label></p>
    <asp:GridView ID="gvRegistration" runat="server" GridLines="None" 
        CssClass="mGrid" PagerStyle-CssClass="pgr" AllowPaging="true"
            AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False" 
         PageSize="40" onpageindexchanging="gvRegistration_PageIndexChanging">
        <Columns>
            <asp:TemplateField HeaderText="S.No.">
                <ItemTemplate>
                    <%# Container.DataItemIndex + 1 %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Country" HeaderText="Country" />
            <asp:BoundField DataField="State" HeaderText="State" />
            <asp:BoundField DataField="Postcode" HeaderText="Postcode" />
        </Columns>
        <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
        <HeaderStyle Height="32px" />
        <PagerStyle CssClass="pgr"></PagerStyle>
    </asp:GridView>
    <br />
    </div>
    </div>
   
 </ContentTemplate>
 </asp:UpdatePanel>
</asp:Content>

