<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormCategoria.aspx.cs" Inherits="CapaPresentacion.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Gestión de Categorías</h2>

    <asp:HiddenField ID="HfIdCategoria" runat="server" Value="0" />

    <div>
        <div>
            <asp:Label Text="Nombre:" runat="server" />
            <asp:TextBox ID="TxtNombre" runat="server" />
            <asp:RequiredFieldValidator
                ID="RfvNombre"
                runat="server"
                ControlToValidate="TxtNombre"
                ErrorMessage="El nombre es requerido."
                Display="Dynamic" />
        </div>

        <div>
            <asp:Label Text="Descripción:" runat="server" />
            <asp:TextBox ID="TxtDescripcion" runat="server" TextMode="MultiLine" Rows="3" />
        </div>

        <div>
            <asp:Label Text="Estado:" runat="server" />
            <asp:DropDownList ID="DdlEstado" runat="server">
                <asp:ListItem Value="true">Activo</asp:ListItem>
                <asp:ListItem Value="false">Inactivo</asp:ListItem>
            </asp:DropDownList>
        </div>

        <div>
            <asp:Button ID="BtnGuardar" Text="Guardar" runat="server" OnClick="BtnGuardar_Click" />
            <asp:Button ID="BtnLimpiar" Text="Limpiar" runat="server" OnClick="BtnLimpiar_Click" CausesValidation="false" />
        </div>

        <div>
            <asp:Label ID="LblMensaje" runat="server" />
        </div>
    </div>

    <br />

    <asp:GridView ID="GvCategorias" runat="server"
        AutoGenerateColumns="false"
        DataKeyNames="Id_Categoria, Estado"
        OnSelectedIndexChanged="GvCategorias_SelectedIndexChanged"
        OnRowDeleting="GvCategorias_RowDeleting">
        <Columns>
            <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
            <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
            <asp:TemplateField HeaderText="Estado">
                <ItemTemplate>
                    <%# (bool)Eval("Estado") ? "Activo" : "Inactivo" %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:ButtonField CommandName="Select" Text="Editar" ButtonType="Button" />
            <asp:ButtonField CommandName="Delete" Text="Eliminar" ButtonType="Button" />
        </Columns>
    </asp:GridView>
</asp:Content>
