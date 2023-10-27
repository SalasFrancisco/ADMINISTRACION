using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WinFormsApp3.Models;
using System.Linq;

namespace WinFormsApp3
{
    public partial class frmArticulo : Form
    {
        public frmArticulo()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            using var db = new AdministracionContext();
            Articulo p = new Articulo();
            p.Nombre = txtNombre.Text;
            p.Precio = Convert.ToDecimal(txtPrecio.Text);
            p.ProveedorId = Convert.ToInt32(cbProveedor.SelectedValue);
            db.Add(p);
            db.SaveChanges();

            txtNombre.Text = "";
            txtPrecio.Text = "";
            cbProveedor.SelectedIndex = 0;

            llenar_grilla();
        }

        private void llenar_grilla()
        {
            using var db = new AdministracionContext();

            var query = from a in db.Articulos
                        orderby a.ArticuloId
                        select a;

            dgvArticulos.Rows.Clear();

            foreach (var p in query)
            {
                dgvArticulos.Rows.Add(p.ArticuloId, p.Nombre, p.Precio, p.ProveedorId);
            }
        }

        private void frmArticulo_Load(object sender, EventArgs e)
        {
            CargarProveedores();
            llenar_grilla();
        }

        private void CargarProveedores()
        {
            using var db = new AdministracionContext();

            var query = from a in db.Provedors
                        orderby a.ProveedorId
                        select a;

            DataTable tabla = new DataTable();
            tabla.Columns.Add("ProveedorId", typeof(int));
            tabla.Columns.Add("Nombre", typeof(string));

            foreach (var p in query)
            {
                DataRow dr = tabla.NewRow();
                dr["ProveedorId"] = p.ProveedorId;
                dr["Nombre"] = p.Nombre;
                tabla.Rows.Add(dr);
            }
            cbProveedor.DisplayMember = "Nombre";
            cbProveedor.ValueMember = "ProveedorId";
            cbProveedor.DataSource = tabla;
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            using var db = new AdministracionContext();

            int id = Convert.ToInt32(txtIdentificador.Text);
            var query = from a in db.Articulos
                        where a.ArticuloId == id
                        select a;

            if(query.Any())
            {
                var Art = query.First();
                db.Remove(Art);
                db.SaveChanges();
                llenar_grilla();

            }else
            {
                MessageBox.Show("Articulo inexistente", "ERROR");
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbProveedor_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
