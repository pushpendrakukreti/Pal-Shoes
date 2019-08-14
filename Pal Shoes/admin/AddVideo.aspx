<%@ Page Title="" Language="C#" MasterPageFile="~/admin/AdminPage.master" AutoEventWireup="true" CodeFile="AddVideo.aspx.cs" Inherits="admin_AddVideo" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
   
<style type="text/css">
    table.gvproduct{width:100%; margin:0; padding:0; border-collapse:collapse; font-size:13px; border:1px solid #818181;}
.gvproduct th{ padding:5px; vertical-align:middle; }
.gvproduct td{padding:3px; vertical-align:middle;}

.gvproduct td:nth-child(1){width:5%;}
.gvproduct td:nth-child(2){width:40%;}
.gvproduct td:nth-child(3){width:15%;}
.gvproduct td:nth-child(4){width:10%;}
.gvproduct td:nth-child(5){width:8%;}
.gvproduct td:nth-child(6){width:12%;}
.gvproduct td:nth-child(7){width:9%;}
.gvproduct td:nth-child(8){width:6%;}
.gvproduct td:nth-child(9){width:6%;}
.gvproduct td:nth-child(10){width:9%;}
.gvproduct td:nth-child(11){width:6%;}
.gvproduct td:nth-child(12){width:6%;}


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
    }
    .modalPopup .body
    {
        min-height: 50px;
        line-height: 30px;
        text-align: center;
        font-weight: bold;
    }
    .modalPopup .footer
    {
        padding: 3px;
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
    .footer1 { margin:0; padding:5px; text-align:center; color:Black; background-color:#FFFF80;}
    
</style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <Triggers>
    <asp:PostBackTrigger ControlID="btnAddurl" />
    </Triggers>
    <ContentTemplate>
 <div class="row">
    <div class="col-lg-6 col-md-8 col-sm-8 col-lg-offset-3 col-sm-offset-2">
        <div class="main-form full">
        <div class="row">
            <div class="col-xs-12 mb-20">
            <div class="heading-part heading-bg">
                <h2 class="heading">Add Home Video</h2>
            </div>
            <asp:Label ID="lblMessage" CssClass="message2" runat="server" Text=""></asp:Label>
            </div>
            <div class="col-xs-12">
            <div class="input-box">
                <label for="f-name">Video URL</label>
                <asp:TextBox ID="txtvideoUrl" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ErrorMessage="* Video URL" ControlToValidate="txtvideoUrl" Display="Dynamic" 
                    ValidationGroup="g1" ForeColor="Red"></asp:RequiredFieldValidator>
            </div>
            </div>
            <div class="col-xs-12">
            <br />
            <div class="input-box">
                <label for="login-email">Video Title</label>
                <asp:TextBox ID="txtVideotitle" runat="server"></asp:TextBox>
            </div>
            </div>
            <div class="col-xs-12">
            <br />
            <div class="left-side">
             <asp:Button ID="btnCancel" runat="server" Text="Cancel" 
                    class="btn-black right-side"></asp:Button>
            </div>
            <asp:Button ID="btnAddurl" runat="server" Text="Add Video Url" class="btn-black right-side" 
                onclick="btnAddurl_Click" ValidationGroup="g1" />
            </div>
        </div>
            <br /><br />

<div class="row">
             <center><asp:Label ID="lblmessage1" runat="server" Text=""></asp:Label></center>
<asp:GridView ID="gvHomeVideo" runat="server" AutoGenerateColumns="False" 
        CssClass="gvproduct" CellPadding="4" ForeColor="#333333" PageSize="25" 
        GridLines="None" AllowPaging="true" onrowdeleting="gvHomeVideo_RowDeleting1" 
        onpageindexchanging="gvHomeVideo_PageIndexChanging">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:TemplateField HeaderText="S.N.">
                <ItemTemplate>
                <%# Container.DataItemIndex + 1 %>
                </ItemTemplate>
            </asp:TemplateField>
           
            <asp:BoundField DataField="videotitle" HeaderText="Video Name" />
            
                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                    <asp:Button ID="btnDelete" runat="server" Text="Delete" CommandArgument='<%#Eval("videoid") %>' OnClick="DeleteRecord" class="btn-black right-side"></asp:Button>
                    <cc1:ConfirmButtonExtender ID="cbe" runat="server" DisplayModalPopupID="mpe" TargetControlID="btnDelete">
                    </cc1:ConfirmButtonExtender>
                    <cc1:ModalPopupExtender ID="mpe" runat="server" PopupControlID="pnlPopup" TargetControlID="btnDelete" OkControlID = "btnYes"
                        CancelControlID="btnNo" BackgroundCssClass="modalBackground">
                    </cc1:ModalPopupExtender>
                    <asp:Panel ID="pnlPopup" runat="server" CssClass="modalPopup" Style="display: none">
                        <div class="header">
                            Confirmation
                        </div>
                        <div class="body">
                            Do you want to delete this record?
                        </div>
                        <div class="footer1" align="right">
                            <asp:Button ID="btnYes" runat="server" Text="Yes"  style="background-color: #2fbdf1"/>
                            <hr />
                            <asp:Button ID="btnNo" runat="server" Text="No" style="background-color: #2fbdf1"/>
                        </div>
                    </asp:Panel>
                    </ItemTemplate>
                </asp:TemplateField>
        </Columns>
        <EditRowStyle BackColor="#F0F0F0" />
        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#23786d" Font-Bold="True" HorizontalAlign="Left" ForeColor="White" />
        <PagerStyle BackColor="#23786d" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#E3EAEB" />
        <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
        <SortedAscendingCellStyle BackColor="#F8FAFA" />
        <SortedAscendingHeaderStyle BackColor="#246B61" />
        <SortedDescendingCellStyle BackColor="#D4DFE1" />
        <SortedDescendingHeaderStyle BackColor="#15524A" />
    </asp:GridView>
         </div>
        </div>
        <br />
        <br />
        <br />
        <br />
    </div>
    </div>
 </ContentTemplate>
 </asp:UpdatePanel>
</asp:Content>

