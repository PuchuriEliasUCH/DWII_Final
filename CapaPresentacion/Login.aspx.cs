using CapaNegocio;
using System;
using System.Web.UI;

namespace CapaPresentacion
{
    public partial class Login : Page
    {
        private readonly UsuarioNegocio _usuarioNeg = new UsuarioNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["IdUsuario"] != null)
            {
                Response.Redirect("Default.aspx");
            }
        }

        protected void BtnIngresar_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            try
            {
                string correo = TxtCorreo.Text.Trim();
                string contrasena = TxtContrasena.Text;

                var usuario = _usuarioNeg.Autenticar(correo, contrasena);

                if (usuario == null)
                {
                    LblMensaje.Text = "Correo o contrasena incorrectos.";
                    return;
                }

                Session["IdUsuario"] = usuario.Id_usuario;
                Session["NombreUsuario"] = usuario.Nombre + " " + usuario.Apellido;
                Session["RolUsuario"] = usuario.Id_Rol;
                Session["CorreoUsuario"] = usuario.Correo;

                Response.Redirect("Default.aspx");
            }
            catch (Exception ex)
            {
                LblMensaje.Text = "Error al iniciar sesion: " + ex.Message;
            }
        }
    }
}