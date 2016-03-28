<%@ Page Title="Separador" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="divisor_texto.aspx.cs" Inherits="FormateadorDeTexto.WebForm1" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <br />
    <br />
    <br />
    <br />
    <div>
        <asp:Label ID="LabelIntroduce" Text="Introduce un numero" runat="server"></asp:Label>
        <asp:TextBox ID="Cols" runat="server"></asp:TextBox>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="Cols" runat="server" ErrorMessage="Sólo puedes introducir un número" ValidationExpression="\d+"></asp:RegularExpressionValidator>
        <asp:Button ID="Button1" runat="server" OnClick="Formatear" Text="Formatea la cadena" />
        <br/>
        <asp:TextBox id="TextoAFormatear" TextMode="multiline" Columns="50" Rows="5" runat="server" />
        <asp:TextBox id="TextoFormateado" TextMode="multiline" Columns="50" Rows="5" runat="server" Visible="false" />
    </div>
        
</asp:Content>
