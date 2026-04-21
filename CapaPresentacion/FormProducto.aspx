<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormProducto.aspx.cs" Inherits="CapaPresentacion.FormProducto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Productos</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Gestión de Productos</h2>

    <asp:HiddenField ID="HfIdProducto" runat="server" Value="0" />

    <div>
        <div>
            <asp:Label Text="Categoría:" runat="server" />
            <asp:DropDownList ID="DdlCategoria" runat="server"
                DataValueField="Id_categoria"
                DataTextField="Nombre" />
            <asp:RequiredFieldValidator
                ControlToValidate="DdlCategoria"
                InitialValue="0"
                ErrorMessage="Seleccione una categoría."
                Display="Dynamic"
                runat="server" />
        </div>
        <div>
            <asp:Label Text="Nombre:" runat="server" />
            <asp:TextBox ID="TxtNombre" runat="server" />
            <asp:RequiredFieldValidator
                ControlToValidate="TxtNombre"
                ErrorMessage="El nombre es requerido."
                Display="Dynamic"
                runat="server" />
        </div>
        <div>
            <asp:Label Text="Descripción corta:" runat="server" />
            <asp:TextBox ID="TxtDescCorta" runat="server" />
            <asp:RequiredFieldValidator
                ControlToValidate="TxtDescCorta"
                ErrorMessage="La descripción corta es requerida."
                Display="Dynamic"
                runat="server" />
        </div>
        <div>
            <asp:Label Text="Descripción completa:" runat="server" />
            <asp:TextBox ID="TxtDescCompleta" runat="server" TextMode="MultiLine" />
            <asp:RequiredFieldValidator
                ControlToValidate="TxtDescCompleta"
                ErrorMessage="La descripción completa es requerida."
                Display="Dynamic"
                runat="server" />
        </div>
        <div>
            <asp:Label Text="Precio:" runat="server" />
            <asp:TextBox ID="TxtPrecio" runat="server" />
            <asp:RequiredFieldValidator
                ControlToValidate="TxtPrecio"
                ErrorMessage="El precio es requerido."
                Display="Dynamic"
                runat="server" />
            <asp:RangeValidator
                ControlToValidate="TxtPrecio"
                MinimumValue="0" MaximumValue="9999"
                Type="Double"
                ErrorMessage="Debe ser un valor entre 0 y 9999."
                Display="Dynamic"
                runat="server" />
        </div>
        <div>
            <asp:Label Text="Requiere preparación:" runat="server" />
            <asp:CheckBox ID="ChkRequierePreparacion" runat="server" Checked="true" />
        </div>
        <div>
            <asp:Button ID="BtnGuardar" Text="Guardar" runat="server"
                OnClick="BtnGuardar_Click" />
            <asp:Button ID="BtnLimpiar" Text="Limpiar" runat="server"
                OnClick="BtnLimpiar_Click" CausesValidation="false" />
        </div>
        <div>
            <asp:Label ID="LblMensaje" runat="server" />
        </div>
    </div>

    <br />

    <asp:GridView ID="GvProductos" runat="server"
        AutoGenerateColumns="false"
        DataKeyNames="Id_producto,Id_categoria,Desc_completa"
        OnSelectedIndexChanged="GvProductos_SelectedIndexChanged"
        OnRowDeleting="GvProductos_RowDeleting">
        <Columns>
            <asp:BoundField DataField="Nombre"               HeaderText="Nombre"      />
            <asp:BoundField DataField="Desc_corta"           HeaderText="Descripción" />
            <asp:BoundField DataField="Precio"               HeaderText="Precio"      />
            <asp:BoundField DataField="Requiere_preparacion" HeaderText="Preparación" />
            <asp:BoundField DataField="Estado"               HeaderText="Estado"      />
            <asp:ButtonField CommandName="Select" Text="Editar"   ButtonType="Button" />
            <asp:ButtonField CommandName="Delete" Text="Eliminar" ButtonType="Button" />
        </Columns>
    </asp:GridView>
</asp:Content>
