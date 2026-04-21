using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaNegocio;
using Entidades;

namespace CapaPresentacion
{
    public partial class FormMesa : Page
    {
        private readonly MesaNegocio _mesaNeg = new MesaNegocio();

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
            GvMesas.DataSource = _mesaNeg.Listar();
            GvMesas.DataBind();
        }

        protected void BtnGuardar_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            try
            {
                var mesa = new Mesa
                {
                    Id_mesa = int.Parse(HfIdMesa.Value),
                    Numero = int.Parse(TxtNumero.Text),
                    Capacidad = int.Parse(DdlCapacidad.SelectedValue),
                    Estado = DdlEstado.SelectedValue
                };

                bool esNuevo = mesa.Id_mesa == 0;

                if (esNuevo)
                {
                    LblMensaje.Text = "Mesa agregada correctamente";
                    _mesaNeg.Insertar(mesa);
                }
                else
                {
                    LblMensaje.Text = "Mesa actualizada correctamente";
                    _mesaNeg.Actualizar(mesa);
                }

                CargarDatos();
                BorrarFormulario();
            }
            catch (Exception ex)
            {
                LblMensaje.Text = ex.ToString();
            }
        }

        protected void BtnLimpiar_Click(object sender, EventArgs e)
        {
            BorrarFormulario();
        }

        protected void GvMesas_SelectedIndexChanged(object sender, EventArgs e)
        {
            HfIdMesa.Value = GvMesas.SelectedDataKey.Value.ToString();
            TxtNumero.Text = GvMesas.SelectedRow.Cells[0].Text;
            DdlCapacidad.SelectedValue = GvMesas.SelectedRow.Cells[1].Text.Trim();
            DdlEstado.SelectedValue = GvMesas.SelectedRow.Cells[2].Text.Trim();
            LblMensaje.Text = string.Empty;
        }

        protected void GvMesas_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = (int)GvMesas.DataKeys[e.RowIndex].Value;
            bool exito = _mesaNeg.Eliminar(id);
            LblMensaje.Text = exito ? "Mesa inhabilitada correctamente." : "Error al eliminar.";
            if (exito) CargarDatos();
        }

        private void BorrarFormulario()
        {
            HfIdMesa.Value = "0";
            TxtNumero.Text = string.Empty;
            DdlCapacidad.SelectedIndex = 0;
            DdlEstado.SelectedIndex = 0;
            LblMensaje.Text = string.Empty;
        }
    }
}