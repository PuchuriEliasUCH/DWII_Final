<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormUsuario.aspx.cs" Inherits="CapaPresentacion.FormUsuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Gestión de Usuarios</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Gestion de usuarios</h2>

    <asp:HiddenField ID ="HfIdUsuario" runat="server" Value="0" />
    
    <article>
        <div>
            <asp:Label Text="Rol: " runat="server" />
            <asp:DropDownList ID="DdlRol" runat="server">
                <asp:ListItem Value="1">Administrador</asp:ListItem>
                <asp:ListItem Value="2">Chef</asp:ListItem>
                <asp:ListItem Value="3">Mesero</asp:ListItem>
            </asp:DropDownList>
        </div>

        <div>
            <asp:Label Text="Nombre: " runat="server" />
            <asp:TextBox ID="TxtNombre" runat="server" />
            <asp:RequiredFieldValidator
                ControlToValidate="TxtNombre"
                ErrorMessage="El nombre es requerido."
                Display="Dynamic"
                runat="server" />
    
        </div>

        <div>
            <asp:Label Text="Apellido: " runat="server" />
            <asp:TextBox ID="TxtApellido" runat="server" />
            <asp:RequiredFieldValidator
                ControlToValidate="TxtApellido"
                ErrorMessage="El apellido es requerido."
                Display="Dynamic"
                runat="server" />
    
        </div>

        <div>
            <asp:Label Text="Correo: " runat="server" />
            <asp:TextBox ID="TxtCorreo" runat="server" autocomplete="off" />
            <asp:RequiredFieldValidator
                ControlToValidate="TxtCorreo"
                ErrorMessage="El correo es requerido."
                Display="Dynamic"
                runat="server" />
            <asp:RegularExpressionValidator
                ControlToValidate="TxtCorreo"
                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                ErrorMessage="Correo inválido."
                Display="Dynamic"
                runat="server" />
        </div>

        <div>
            <asp:Label Text="Contraseña: " runat="server" />
            <asp:TextBox ID="TxtContrasena" runat="server" TextMode="Password" autocomplete="new-password" />
            <asp:RequiredFieldValidator ID="RfvContrasena"
                ControlToValidate="TxtContrasena"
                ErrorMessage="La contraseña es requerida."
                Display="Dynamic"
                EnableClientScript="false"
                runat="server" />
            <asp:RegularExpressionValidator ID="RevContrasena"
                ControlToValidate="TxtContrasena"
                ValidationExpression="^.{8,}$"
                ErrorMessage="La contraseña debe tener al menos 8 caracteres."
                Display="Dynamic"
                EnableClientScript="false"
                runat="server" />
        </div>

        <div>
            <asp:Button ID="BtnGuardar" Text="Guardar" runat="server" OnClick="BtnGuardar_Click" />
            <asp:Button ID="BtnLimpiar" Text="Limpiar" runat="server" OnClick="BtnLimpiar_Click"/>
        </div>

        <div>
            <asp:Label ID="LblMensaje" runat="server" />
        </div>
    </article>

    <br />

    <asp:GridView ID="GvUsuarios" runat="server"
        AutoGenerateColumns="false"
        DataKeyNames="Id_usuario"
        OnSelectedIndexChanged="GvUsuarios_SelectedIndexChanged"
        OnRowDeleting="GvUsuarios_RowDeleting"
    >
        <Columns>
            <asp:BoundField DataField="NombreCompleto" HeaderText="Nombre Completo"/>
            <asp:BoundField DataField="Correo" HeaderText="Correo"/>
            <asp:BoundField DataField="NombreRol" HeaderText="Rol"/>
            <asp:ButtonField CommandName="Select" Text="Editar" ButtonType="Button"/>
            <asp:ButtonField CommandName="Delete" Text="Eliminar" ButtonType="Button"/>
        </Columns>
    </asp:GridView>
</asp:Content>
