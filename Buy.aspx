<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Buy.aspx.cs" Inherits="CarSale.Buy" %>
<%@ PreviousPageType VirtualPath ="~/Catalog.aspx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h3>Благодарим за выбор нашего автосалона!</h3>
    <p>
        <table   style="width:35%;">
        <tr>
            <td align="left"><small>Введите ваше имя</small></td>
            <td align="right"><asp:TextBox ID="TextBox1" runat="server"></asp:TextBox></td>
        </tr></table>
    </p>
    <p>
        <table  style="width:35%;">
        <tr>
            <td align="left"><small>Введите вашу фамилию</small></td>
            <td align="right"><asp:TextBox ID="TextBox2" runat="server"></asp:TextBox></td>
        </tr></table>
    </p>
    <p>
        <table  style="width:35%;">
        <tr>
            <td align="left"><small>Введите ваше отчество</small></td>
            <td align="right"><asp:TextBox ID="TextBox3" runat="server"></asp:TextBox></td>
        </tr></table>
    </p>
    <p>
        <table  style="width:35%;">
        <tr>
            <td align="left"><small>Введите ваш телефон для связи</small></td>
            <td align="right"><asp:TextBox ID="TextBox4" runat="server"></asp:TextBox></td>
        </tr></table>
    </p>
    <p>
        <table  style="width:35%;">
        <tr>
            <td align="left"><small>Введите вашу почту для связи</small></td>
            <td align="right"><asp:TextBox ID="TextBox5" runat="server"></asp:TextBox></td>
        </tr></table>
    </p>
    <p>
        <table  style="width:35%;">
        <tr>
            <td align="left"><small>К оплате будет:</small></td>
            <td align="right"><asp:Label runat ="server" ID ="pr"> </asp:Label></td>
        </tr></table>
    </p>
    <p>
            <span style="vertical-align: 40px; font-family: 'Century Gothic';" class="auto-style1" 
            aria-multiline="False" aria-multiselectable="False" aria-orientation="horizontal" 
                dir="ltr">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="Button4" runat="server" Height="47px" Text="Приобрести" Width="244px" BackColor="White" BorderColor="Black" CssClass="auto-style1" OnClick="Button4_Click" />
            </span>
    </p>
<p>
            <asp:Label ID="error" runat="server" Text="Label" Visible="False"></asp:Label>
            <asp:Label ID="error2" runat="server" Text="Label" Visible="False"></asp:Label>
    </p>

</asp:Content>
