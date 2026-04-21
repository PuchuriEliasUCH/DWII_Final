using CapaNegocio;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CapaPresentacion
{
    public partial class WebForm1 : System.Web.UI.Page
    {

        private readonly CategoriaProductoNegocio _categoriaNeg = new CategoriaProductoNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["IdUsuario"] == null)
            {
                Response.Redirect("Login.aspx");
            }

            if (!IsPostBack)
            {
                CargarDatos();
            }
        }

        private void CargarDatos()
        {
            GvCategorias.DataSource = _categoriaNeg.Listar();
            GvCategorias.DataBind();
        }

        protected void BtnGuardar_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            try
            {
                var categoria = new Categoria_productos
                {
                    Id_Categoria = int.Parse(HfIdCategoria.Value),
                    Nombre = TxtNombre.Text.Trim(),
                    Descripcion = TxtDescripcion.Text.Trim(),
                    Estado = bool.Parse(DdlEstado.SelectedValue)
                };

                bool esNuevo = categoria.Id_Categoria == 0;
                bool exito;

                if (esNuevo)
                {
                    exito = _categoriaNeg.Insertar(categoria);
                    LblMensaje.Text = exito ? "Categoría agregada correctamente." : "Error al agregar la categoría.";
                }
                else
                {
                    exito = _categoriaNeg.Actualizar(categoria);
                    LblMensaje.Text = exito ? "Categoría actualizada correctamente." : "Error al actualizar la categoría.";
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

        protected void GvCategorias_SelectedIndexChanged(object sender, EventArgs e)
        {
            HfIdCategoria.Value = GvCategorias.SelectedDataKey.Value.ToString();
            TxtNombre.Text = GvCategorias.SelectedRow.Cells[0].Text;
            TxtDescripcion.Text = HttpUtility.HtmlDecode(GvCategorias.SelectedRow.Cells[1].Text);

            bool estado = (bool)GvCategorias.DataKeys[GvCategorias.SelectedIndex].Values["Estado"];
            DdlEstado.SelectedValue = estado.ToString().ToLower();

            LblMensaje.Text = string.Empty;
        }

        protected void GvCategorias_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = (int)GvCategorias.DataKeys[e.RowIndex].Value;
            bool exito = _categoriaNeg.Eliminar(id);

            LblMensaje.Text = exito ? "Categoría deshabilitada correctamente." : "Error al deshabilitar la categoría.";

            if (exito)
                CargarDatos();
        }

        private void BorrarFormulario()
        {
            HfIdCategoria.Value = "0";
            TxtNombre.Text = string.Empty;
            TxtDescripcion.Text = string.Empty;
            DdlEstado.SelectedIndex = 0;
            LblMensaje.Text = string.Empty;
        }
    }
}