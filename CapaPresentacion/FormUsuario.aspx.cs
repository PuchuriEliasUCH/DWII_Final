using CapaNegocio;
using Entidades.Dto.Usuarios;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CapaPresentacion
{
    public partial class FormUsuario : Page
    {
        private readonly UsuarioNegocio _usuarioNeg = new UsuarioNegocio();
        private readonly RolNegocio _rolNeg = new RolNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["IdUsuario"] == null)
            {
                Response.Redirect("Login.aspx");
            }

            if (!IsPostBack)
            {
                CargarDatos();
                CargarRoles();
            }
        }

        private void CargarDatos()
        {
            GvUsuarios.DataSource = _usuarioNeg.ListarDTO();
            GvUsuarios.DataBind();
        }

        private void CargarRoles()
        { 
            DdlRol.DataSource = _rolNeg.Listar();
            DdlRol.DataBind();
            DdlRol.Items.Insert(0, new ListItem("--Seleccione--", "0"));
        }

        protected void BtnGuardar_Click(object sender, EventArgs e)
        {
            bool esNuevo = int.Parse(HfIdUsuario.Value) == 0;

            if (!esNuevo)
                RfvContrasena.Enabled = false;

            if (!Page.IsValid) return;

            try
            {
                var usuario = new UsuarioGuardarDTO
                {
                    Id_usuario = int.Parse(HfIdUsuario.Value),
                    Id_rol = int.Parse(DdlRol.SelectedValue),
                    Nombre = TxtNombre.Text.Trim(),
                    Apellido = TxtApellido.Text.Trim(),
                    Correo = TxtCorreo.Text.Trim(),
                    Contra_hash = TxtContrasena.Text
                };

                bool exito;

                if (esNuevo)
                {
                    exito = _usuarioNeg.Insertar(usuario);
                    LblMensaje.Text = exito ? "Usuario agregado correctamente." : "Error al agregar el usuario.";
                }
                else
                {
                    exito = _usuarioNeg.Actualizar(usuario);
                    LblMensaje.Text = exito ? "Usuario actualizado correctamente." : "Error al actualizar el usuario.";
                }

                if (exito)
                {
                    CargarDatos();
                    BorrarFormulario();
                }
            }
            catch (Exception ex)
            {
                LblMensaje.Text = "Error inesperado: " + ex.Message;
            }
        }

        protected void BtnLimpiar_Click(object sender, EventArgs e)
        {
            BorrarFormulario();
        }

        protected void GvUsuarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = (int)GvUsuarios.SelectedDataKey.Value;
            var usuario = _usuarioNeg.ObtenerPorId(id);

            if (usuario == null) return;

            HfIdUsuario.Value = usuario.Id_usuario.ToString();
            TxtNombre.Text = usuario.Nombre;
            TxtApellido.Text = usuario.Apellido;
            TxtCorreo.Text = usuario.Correo;
            DdlRol.SelectedValue = usuario.Id_Rol.ToString();

            TxtContrasena.Enabled = false;

            LblMensaje.Text = string.Empty;
        }

        protected void GvUsuarios_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = (int)GvUsuarios.DataKeys[e.RowIndex].Value;
            bool exito = _usuarioNeg.Eliminar(id);
            LblMensaje.Text = exito ? "Usuario deshabilitado correctamente." : "Error al deshabilitar.";
            if (exito) CargarDatos();
        }

        private void BorrarFormulario()
        {
            HfIdUsuario.Value = "0";
            TxtNombre.Text = string.Empty;
            TxtApellido.Text = string.Empty;
            TxtCorreo.Text = string.Empty;
            TxtContrasena.Text = string.Empty;
            TxtContrasena.Enabled = true;
            RfvContrasena.Enabled = true;
            DdlRol.SelectedIndex = 0;
            LblMensaje.Text = string.Empty;
        }
    }
}