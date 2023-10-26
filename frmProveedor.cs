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
    public partial class frmProveedor : Form
    {
        public frmProveedor()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            using var db = new AdministracionContext();

            db.Add(new Proveedor { Nombre = txtNombre.Text });
            db.SaveChanges();
            txtNombre.Text = "";
        }

        private void frmProveedor_Load(object sender, EventArgs e)
        {
            llenar_grilla();
        }

        private void llenar_grilla()
        {
            using var db = new AdministracionContext();

            var query = from a in db.Proveedors
                        orderby a.ProveedorId
                        select a;

            dgvProveedores.Rows.Clear();

            foreach(var p in query)
            {
                dgvProveedores.Rows.Add(p.ProveedorId.ToString(), p.Nombre);
            }
        }
    }
}
