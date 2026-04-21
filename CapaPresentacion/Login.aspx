<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="CapaPresentacion.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Iniciar sesion</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Iniciar sesion</h2>

    <div>
        <div>
            <asp:Label ID="LblCorreoTexto" runat="server" Text="Correo:" AssociatedControlID="TxtCorreo" />
            <asp:TextBox ID="TxtCorreo" runat="server" />
            <asp:RequiredFieldValidator
                ID="RfvCorreo"
                runat="server"
                ControlToValidate="TxtCorreo"
                ErrorMessage="El correo es requerido."
                Display="Dynamic" />
            <asp:RegularExpressionValidator
                ID="RevCorreo"
                runat="server"
                ControlToValidate="TxtCorreo"
                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                ErrorMessage="Correo invalido."
                Display="Dynamic" />
        </div>

        <div>
            <asp:Label ID="LblContrasenaTexto" runat="server" Text="Contrasena:" AssociatedControlID="TxtContrasena" />
            <asp:TextBox ID="TxtContrasena" runat="server" TextMode="Password" />
            <asp:RequiredFieldValidator
                ID="RfvContrasena"
                runat="server"
                ControlToValidate="TxtContrasena"
                ErrorMessage="La contrasena es requerida."
                Display="Dynamic" />
        </div>

        <div>
            <asp:Button ID="BtnIngresar" runat="server" Text="Ingresar" OnClick="BtnIngresar_Click" />
        </div>

        <div>
            <asp:Label ID="LblMensaje" runat="server" />
        </div>
    </div>
</asp:Content>