<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Catalog.aspx.cs" Inherits="CarSale.Catalog" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Repeater ID="rptTable" runat="server" OnItemCommand="rptTable_ItemCommand">

    <HeaderTemplate>
        <table class="table">
    </HeaderTemplate>
    <ItemTemplate>

        <tr>
            <td width="10%"><%# Eval("Mark") %>&nbsp;&nbsp;<%# Eval("Model") %></td>
        </tr>
        <tr>

            <td width="15%">&nbsp;&nbsp;<%# Eval("Price") %></td>
        </tr>
        <tr>
            <td><asp:Button ID = button1  runat="server" Height="50px"  Text = "Приобрести" Width="250px" BackColor="White" BorderColor="Black" CssClass="auto-style1" OnClick="Button1_Click" /></td>
        </tr>   
    </ItemTemplate>
    <FooterTemplate>
        </table>
    </FooterTemplate>
</asp:Repeater>
    <asp:Label ID="Label1" runat="server" Text="Label" Visible="False"></asp:Label>
</asp:Content>
