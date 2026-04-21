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
    public partial class FormProducto : System.Web.UI.Page
    {
        private readonly ProductoNegocio _productoNeg = new ProductoNegocio();
        private readonly CategoriaProductoNegocio _categoriaNeg = new CategoriaProductoNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarCategorias();
                CargarDatos();
            }
        }

        private void CargarCategorias()
        {
            DdlCategoria.DataSource = _categoriaNeg.Listar();
            DdlCategoria.DataBind();
            DdlCategoria.Items.Insert(0, new ListItem("-- Seleccione --", "0"));
        }

        private void CargarDatos()
        {
            GvProductos.DataSource = _productoNeg.Listar();
            GvProductos.DataBind();
        }

        protected void BtnGuardar_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            try
            {
                var producto = new Producto
                {
                    Id_producto = int.Parse(HfIdProducto.Value),
                    Id_categoria = int.Parse(DdlCategoria.SelectedValue),
                    Nombre = TxtNombre.Text.Trim(),
                    Desc_corta = TxtDescCorta.Text.Trim(),
                    Desc_completa = TxtDescCompleta.Text.Trim(),
                    Precio = decimal.Parse(TxtPrecio.Text),
                    Requiere_preparacion = ChkRequierePreparacion.Checked
                };

                bool esNuevo = producto.Id_producto == 0;
                bool exito;

                if (esNuevo)
                {
                    exito = _productoNeg.Insertar(producto);
                    LblMensaje.Text = exito
                        ? "Producto agregado correctamente."
                        : "Error al agregar el producto.";
                }
                else
                {
                    exito = _productoNeg.Actualizar(producto);
                    LblMensaje.Text = exito
                        ? "Producto actualizado correctamente."
                        : "Error al actualizar el producto.";
                }

                if (exito)
                {
                    CargarDatos();
                    LimpiarFormulario();
                }
            }
            catch (Exception ex)
            {
                LblMensaje.Text = ex.Message;
            }
        }

        protected void BtnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        protected void GvProductos_SelectedIndexChanged(object sender, EventArgs e)
        {
            HfIdProducto.Value = GvProductos.SelectedDataKey.Values[0].ToString();
            DdlCategoria.SelectedValue = GvProductos.SelectedDataKey.Values[1].ToString();
            TxtDescCompleta.Text = GvProductos.SelectedDataKey.Values[2].ToString();

            TxtNombre.Text = HttpUtility.HtmlDecode(GvProductos.SelectedRow.Cells[0].Text);
            TxtDescCorta.Text = HttpUtility.HtmlDecode(GvProductos.SelectedRow.Cells[1].Text);
            TxtPrecio.Text = GvProductos.SelectedRow.Cells[2].Text;
            ChkRequierePreparacion.Checked = GvProductos.SelectedRow.Cells[3].Text == "True";
            LblMensaje.Text = string.Empty;
        }

        protected void GvProductos_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = (int)GvProductos.DataKeys[e.RowIndex].Values[0];
            bool exito = _productoNeg.Eliminar(id);
            LblMensaje.Text = exito
                ? "Producto desactivado correctamente."
                : "Error al eliminar el producto.";
            if (exito) CargarDatos();
        }

        private void LimpiarFormulario()
        {
            HfIdProducto.Value = "0";
            DdlCategoria.SelectedIndex = 0;
            TxtNombre.Text = string.Empty;
            TxtDescCorta.Text = string.Empty;
            TxtDescCompleta.Text = string.Empty;
            TxtPrecio.Text = string.Empty;
            ChkRequierePreparacion.Checked = true;
            LblMensaje.Text = string.Empty;
        }
    }
}