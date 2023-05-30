using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ABMProductos
{
    public partial class frmProducto : Form
    {

        DBConnection connDB = new DBConnection();
        List<Producto> productos = new List<Producto>();

        public frmProducto()
        {
            InitializeComponent();
            
        }

        private void frmProducto_Load(object sender, EventArgs e)
        {
            Habilitar(false);
            CargaCombo(cboMarca, "Marcas");
            CargaLista(lstProducto, "Productos");

        }


        private void Habilitar(bool x)
        {
            txtCodigo.Enabled = x;
            txtDetalle.Enabled = x;
            cboMarca.Enabled = x;
            rbtNetBook.Enabled = x;
            rbtNoteBook.Enabled = x;
            txtPrecio.Enabled = x;
            dtpFecha.Enabled = x;
            btnGrabar.Enabled = x;
            btnCancelar.Enabled = x;

            btnEditar.Enabled = !x;
            btnBorrar.Enabled = !x;

            // ver como habilitar varios controles a la vez
        }

        private void CargaCombo(ComboBox combo, string nombreTabla)
        {
            // carga el combo con las marcas guardadas en la BD


            string query = "SELECT * FROM " + nombreTabla;

            DataTable tabla = connDB.ConsultarBD(query);

            combo.DataSource = tabla;
            combo.ValueMember = tabla.Columns[0].ColumnName;
            combo.DisplayMember = tabla.Columns[1].ColumnName;
            combo.DropDownStyle  = ComboBoxStyle.DropDownList;

        }

        private void CargaLista(ListBox lista, string nombreTabla)
        {
            // comienzo con la List<> y la ListBox vacias
            lstProducto.Items.Clear();
            productos.Clear();

            // traer prods de la BD
            connDB.LeerTabla(nombreTabla); // con este metodo se cargan en memoria los datos

            while (connDB.Reader.Read())
            {
                Producto p = new Producto();
                if (!connDB.Reader.IsDBNull(0)) { p.Codigo = Convert.ToInt32(connDB.Reader["codigo"]); };
                //if (!connDB.Reader.IsDBNull(0)) { p.Codigo = connDB.Reader.GetInt32(0); };

                if (!connDB.Reader.IsDBNull(1)) { p.Detalle = (connDB.Reader["detalle"]).ToString(); };

                if (!connDB.Reader.IsDBNull(2)) { p.Marca = Convert.ToInt32(connDB.Reader["marca"]); };

                if (!connDB.Reader.IsDBNull(3)) { p.Tipo = Convert.ToInt32(connDB.Reader["tipo"]); };

                if (!connDB.Reader.IsDBNull(4)) { p.Precio = Convert.ToDouble(connDB.Reader["precio"]); };

                if (!connDB.Reader.IsDBNull(3)) { p.Fecha = (DateTime)(connDB.Reader["fecha"]); };

                // cargar la lista de productos
                productos.Add(p);    
            }
                connDB.Desconectar(); // esta es la diferencia entre el dataTable y el dataReader
            // el DataReader funciona con la conexion abierta, entonces tengo que cerrarla desde aca 

            // mostrar prod en el listBox
            //foreach(Producto p in productos)
            //{
            //    lstProducto.Items.Add(p);
            //}
            // o se puede pasar la lista
            lstProducto.Items.AddRange(productos.ToArray());


        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Seguro deseas salir?", "Cerrar Formulario", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Habilitar(true);
        }
    }
}
