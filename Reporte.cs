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
    public partial class Reporte : Form
    {
        public Reporte()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Reporte_Load(object sender, EventArgs e)
        {
            CargarProveedores();
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

        private void cbProveedor_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void btnConsulta_Click(object sender, EventArgs e)
        {
            dgvArticulos.Rows.Clear();

            using var db = new AdministracionContext();
            int pro = Convert.ToInt32(cbProveedor.SelectedValue);
            var query = from a in db.Articulos
                        orderby a.ArticuloId
                        where a.ProveedorId == pro
                        select a;

            dgvArticulos.Rows.Clear();

            foreach (var p in query)
            {
                dgvArticulos.Rows.Add(p.ArticuloId, p.Nombre, p.Precio, p.ProveedorId);
            }
        }
    }
}
