<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormMesa.aspx.cs" Inherits="CapaPresentacion.FormMesa" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Mesas</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <h2>Gestión de Mesas</h2>

    <asp:HiddenField ID="HfIdMesa" runat="server" Value="0" />

    <div>
        <div>
            <asp:Label Text="Número:" runat="server" />
            <asp:TextBox ID="TxtNumero" runat="server" />
            <asp:RequiredFieldValidator
                ControlToValidate="TxtNumero"
                ErrorMessage="El número es requerido."
                Display="Dynamic"
                runat="server" />
            <asp:RangeValidator
                ControlToValidate="TxtNumero"
                MinimumValue="1" MaximumValue="999"
                Type="Integer"
                ErrorMessage="Debe ser un número entre 1 y 999."
                Display="Dynamic"
                runat="server" />
        </div>
        <div>
            <asp:Label Text="Capacidad:" runat="server" />
            <asp:DropDownList ID="DdlCapacidad" runat="server">
                <asp:ListItem Value="2">2 personas</asp:ListItem>
                <asp:ListItem Value="4">4 personas</asp:ListItem>
                <asp:ListItem Value="6">6 personas</asp:ListItem>
                <asp:ListItem Value="8">8 personas</asp:ListItem>
            </asp:DropDownList>
        </div>
        <div>
            <asp:Label Text="Estado:" runat="server" />
            <asp:DropDownList ID="DdlEstado" runat="server">
                <asp:ListItem Value="Disponible">Disponible</asp:ListItem>
                <asp:ListItem Value="Ocupada">Ocupada</asp:ListItem>
                <asp:ListItem Value="Inhabilitada">Inhabilitada</asp:ListItem>
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

    <asp:GridView ID="GvMesas" runat="server"
        AutoGenerateColumns="false"
        DataKeyNames="Id_mesa"
        OnSelectedIndexChanged="GvMesas_SelectedIndexChanged"
        OnRowDeleting="GvMesas_RowDeleting">
        <Columns>
            <asp:BoundField DataField="Numero"    HeaderText="Número"    />
            <asp:BoundField DataField="Capacidad" HeaderText="Capacidad" />
            <asp:BoundField DataField="Estado"    HeaderText="Estado"    />
            <asp:ButtonField CommandName="Select" Text="Editar" ButtonType="Button" />
            <asp:ButtonField CommandName="Delete" Text="Eliminar" ButtonType="Button" />
        </Columns>
    </asp:GridView>

</asp:Content>