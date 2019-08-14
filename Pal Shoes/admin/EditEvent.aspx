<%@ Page Title="" Language="C#" MasterPageFile="~/admin/AdminPage.master" AutoEventWireup="true" CodeFile="EditEvent.aspx.cs" Inherits="admin_EditEvent" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <style type="text/css">
table.product{ margin:0px 0 10px 0; padding:0; border:none; width:90%;}
.product td
{
    padding:5px; vertical-align:top; text-align:center;
}
.admindecs
{
	width:99%; margin:0px; padding:0px;
	height:120px;
	overflow:hidden;
}

table.gvproduct{width:100%; margin:0; padding:0; border-collapse:collapse; font-size:13px; border:1px solid #818181;}
.gvproduct th{ padding:5px; vertical-align:middle; }
.gvproduct td{padding:3px; vertical-align:middle;}

.gvproduct td:nth-child(1){width:5%;}
.gvproduct td:nth-child(2){width:16%;}
.gvproduct td:nth-child(3){width:10%;}
.gvproduct td:nth-child(4){width:30%;}
.gvproduct td:nth-child(5){width:8%;}
.gvproduct td:nth-child(6){width:6%;}
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
<center><asp:Label ID="lblmessage" runat="server" Text=""></asp:Label></center>
<asp:GridView ID="gvEvent" runat="server" AutoGenerateColumns="False" 
        CssClass="gvproduct" CellPadding="4" ForeColor="#333333" PageSize="25" 
        GridLines="None" AllowPaging="true" onrowcommand="gvEvent_RowCommand" 
        onrowdeleting="gvEvent_RowDeleting1">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:TemplateField HeaderText="S.N.">
                <ItemTemplate>
                <%# Container.DataItemIndex + 1 %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Event Photo">
                <ItemTemplate>
                    <asp:Image ID="Image1" ImageUrl='<%# Eval("imagefile","../Upload/event/{0}") %>' Height="100px" Width="300px" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Eventtitle" HeaderText="Event Title" />
            <asp:BoundField DataField="Eventbody" HeaderText="Event Description" />
            <asp:BoundField DataField="EventDate" HeaderText="Event Date" />
            <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:Button ID="btnEdit" runat="server" Text="Edit" CommandName="Edit" CommandArgument='<%#Eval("Eventid") %>'></asp:Button>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                    <asp:Button ID="btnDelete" runat="server" Text="Delete" CommandArgument='<%#Eval("Eventid") %>' 
                        OnClick="DeleteRecord"></asp:Button>
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
                            <asp:Button ID="btnYes" runat="server" Text="Yes" />
                            <asp:Button ID="btnNo" runat="server" Text="No" />
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
    <br /><br />
</asp:Content>

