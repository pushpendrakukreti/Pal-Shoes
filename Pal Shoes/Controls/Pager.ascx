   <%@ Control Language="C#" AutoEventWireup="true" CodeFile="Pager.ascx.cs" Inherits="Controls_Pager" %>
        <ul>
            <li><asp:HyperLink ID="prevLink" Runat="server"><<</asp:HyperLink></li>
        <asp:Repeater ID="pagesRepeater" runat="server">
            <ItemTemplate>
            <li><asp:HyperLink ID="hyperlink" runat="server" Text='<%# Eval("Page") %>' NavigateUrl='<%# Eval("Url") %>' /></li>
            </ItemTemplate>
        </asp:Repeater>
            <li><asp:HyperLink ID="nextLink" Runat="server">>></asp:HyperLink></li>
		</ul>