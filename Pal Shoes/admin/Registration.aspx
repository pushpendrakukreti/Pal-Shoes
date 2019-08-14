<%@ Page Title="" Language="C#" MasterPageFile="~/admin/AdminPage.master" Culture="en-GB" AutoEventWireup="true" CodeFile="Registration.aspx.cs" Inherits="admin_Registration" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

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
.minheight{ min-height:440px;}

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
</style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="minheight">
    <h3 class="text-center">Registered User</h3>
<table class="ord">
    <tr>
    <td>
        Date: <asp:TextBox ID="txtDate1" runat="server" Width="150px"></asp:TextBox>
        <img src="~/img/Calendar.gif" id="img_date1" runat="server" alt="" />
        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="img_date1"
            TargetControlID="txtDate1" Format="dd/MM/yyyy">
        </cc1:CalendarExtender>
        <asp:CustomValidator ID="CustomValidator1" runat="server" ClientValidationFunction="ValidateDate" 
        ControlToValidate="txtDate1" ErrorMessage="dd/mm/yyyy" ValidationGroup="g1" ForeColor="Red" />
    </td>
    <td>
        To Date: <asp:TextBox ID="txtDate2" runat="server" Width="150px"></asp:TextBox>
        <img src="~/img/Calendar.gif" id="img_date2" runat="server" alt="" />
        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" PopupButtonID="img_date2"
            TargetControlID="txtDate2" Format="dd/MM/yyyy">
        </cc1:CalendarExtender>
        <asp:CustomValidator ID="CustomValidator2" runat="server" ClientValidationFunction="ValidateDate" 
        ControlToValidate="txtDate2" ErrorMessage="dd/mm/yyyy" ValidationGroup="g1" ForeColor="Red" />
    </td>
    <td>
        User ID: <asp:TextBox ID="txtSearch" runat="server" Width="150px"></asp:TextBox>
    </td>
    <td>
        <asp:Button ID="btnGetDetails" CssClass="getbutton" runat="server" ValidationGroup="g1" 
            Text="Get Detail" onclick="btnGetDetails_Click" />
    </td>
		<td>
        &nbsp;&nbsp;&nbsp;<asp:ImageButton ID="ImageButton1" ImageUrl="~/img/excel.png" 
            runat="server" Width="29px" Height="29px" OnClick="ImageButton1_Click1" /></td>
    </tr>
    </table>
    <p class="text-center">
        <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label></p>
    <asp:GridView ID="gvRegistration" runat="server" GridLines="None" 
        CssClass="mGrid" PagerStyle-CssClass="pgr" 
            AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False" 
        onrowcommand="gvRegistration_RowCommand" PageSize="25" 
        onrowdatabound="gvRegistration_RowDataBound" 
        onpageindexchanging="gvRegistration_PageIndexChanging" 
        onrowdeleting="gvRegistration_RowDeleting">
        <Columns>
            <asp:TemplateField HeaderText="S.No.">
                <ItemTemplate>
                    <%# Container.DataItemIndex + 1 %>
                    <asp:Label ID="lblId" Visible="false" runat="server" Text='<%# Eval("UserId") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="UserIdentifyNo" HeaderText="User ID" />
            <asp:BoundField DataField="Firstname" HeaderText="First Name" />
            <asp:BoundField DataField="Lastname" HeaderText="Last Name" />
            <asp:BoundField DataField="Emailid" HeaderText="Email ID" />
            <asp:BoundField DataField="walletDate" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Create Date" />
            <asp:TemplateField HeaderText="Status">
                <ItemTemplate>
                    <asp:Label ID="lblActive" runat="server" Text='<%# Bind("wallStatus") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:ImageButton ID="BtnActive" ImageUrl='<%# Eval("wallStatus") %>' OnClick="BtnActive_Click" 
                    CommandArgument='<%# Eval("UserId") %>' Width="21px" Height="21px" runat="server"></asp:ImageButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:ImageButton ID="BtnDelete" CommandArgument='<%# Eval("UserId") %>' ImageUrl="~/img/Trash_d.png"
                     CommandName="Delete" ToolTip="Delete" Width="25px" Height="25px" runat="server"></asp:ImageButton>
                </ItemTemplate>
            </asp:TemplateField>
				<asp:TemplateField HeaderText="Status">
                <ItemTemplate>
                    <asp:Label ID="lblRegistrationStatus" runat="server" Text='<%# Bind("wallStatus") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
        <HeaderStyle Height="32px" />
        <PagerStyle CssClass="pgr"></PagerStyle>
    </asp:GridView>

        <asp:HiddenField ID="HiddenField1" runat="server"></asp:HiddenField>
        <cc1:ModalPopupExtender ID="mpe" runat="server" PopupControlID="pnlPopup" TargetControlID="HiddenField1" 
            CancelControlID="btnNo" BackgroundCssClass="modalBackground">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="pnlPopup" runat="server" CssClass="modalPopup" Style="display: none">
            <div class="header">
            </div>
            <div class="body">
                <asp:Label ID="lblError" runat="server" Text=""></asp:Label><br />
                <label runat="server" id="code">Enter Code:</label> <asp:TextBox ID="txtCode" runat="server"></asp:TextBox>
            </div>
            <div class="footer1" align="right">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnVerify" runat="server" OnClick="btnVerify_Click" Text="Delete" />
                &nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnNo" runat="server" Text="Close" />
            </div>
        </asp:Panel>
</div>
</asp:Content>

