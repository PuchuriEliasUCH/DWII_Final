using System;
using System.Web.UI;

namespace CapaPresentacion
{
    public partial class Site : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["IdUsuario"] != null)
            {
                LblUsuario.Text = "Bienvenido, " + Session["NombreUsuario"].ToString() + " ";
                BtnCerrarSesion.Visible = true;
            }
            else
            {
                LblUsuario.Text = string.Empty;
                BtnCerrarSesion.Visible = false;
            }
        }

        protected void BtnCerrarSesion_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("Login.aspx");
        }
    }
}