<%@ Page Title="" Language="C#" Culture="en-IN" MasterPageFile="~/admin/AdminPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="admin_AdminPanel" %>
<%--<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>--%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
 <%--<script src="../FusionCharts/FusionCharts.js" type="text/javascript"></script>
<script type="text/javascript" language="JavaScript">
    function myJS(myVar) {
        window.alert(myVar);
    } 
</script>--%>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<h2 class="text-center">Welcome to Admin Panel</h2>
<div class="row">
<div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
<div class="main-form full">
    <%--<div class="mb-20">
    <div class="row">
            <div class="heading-part">
                <h3 class="text-center">Monthly Sales</h3>
            </div>
            <hr />
            <%--<asp:Literal ID="Literal1" runat="server"></asp:Literal>
       <center>
        <asp:Chart ID="Chart1" runat="server" Height="400" Width="960">
        <Series>
            <asp:Series Name="Monthly Total Sales" XValueMember="MonthName" YValueMembers="TotalSales" IsValueShownAsLabel="True" LabelFormat="{0:0}" Legend="Legend1">
            </asp:Series>
        </Series>
        <ChartAreas>
            <asp:ChartArea Name="ChartArea1">
              <%--<Area3DStyle Enable3D="True"></Area3DStyle>
                <AxisY Title="Sales">
                    <MajorGrid LineColor="DarkGray" LineDashStyle="Dot" />
                </AxisY>
                <AxisX Title="Months">
                    <MajorGrid Enabled="False" />
                </AxisX>
                <AxisY2>
                    <MajorGrid LineColor="DarkGray" LineDashStyle="Dot" />
                <LabelStyle Format="{0:0}" />
            </AxisY2>
              <%--<Area3DStyle Enable3D="True" />
            </asp:ChartArea>
        </ChartAreas>
        <Legends>
        <asp:Legend Alignment="Center" Docking="Top" Name="Legend1">
        </asp:Legend>
    </Legends>
    </asp:Chart>
    </center>
    </div>
    </div>--%>
</div>
</div>
</div>
</asp:Content>

