using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace ABMMascotas
{
    public partial class frmMascota : Form
    {
        bool nuevo = true;
        AccesoDatos oBD = new AccesoDatos();
        List<Mascota> lMascotas = new List<Mascota>(); // lista dinamica para n Mascotas

        public frmMascota()
        {
            InitializeComponent();
        }

        private void frmMascota_Load(object sender, EventArgs e)
        {
            cargarCombo(cboEspecie, "Especies");
            cargarLista(lstMascotas, "Mascotas");
            habilitar(false);
            limpiar();

        }

        private void cargarCombo(ComboBox combo, string nombreTabla)
        {
            DataTable tabla = oBD.consultarBD("SELECT * FROM " + nombreTabla + " ORDER BY 2"); //
            combo.DataSource = tabla; //es mi tabla 
            combo.ValueMember = tabla.Columns[0].ColumnName; //"idMarca" campo identificador 
            combo.DisplayMember = tabla.Columns[1].ColumnName; //"nombreMarca" campo descriptor 
            combo.DropDownStyle = ComboBoxStyle.DropDownList; //para que el usuario no pueda editar el combo box
        }
       
        private void cargarLista(ListBox lista, string nombreTabla)
        {
            lMascotas.Clear(); // limpio la lista
            oBD.leerBD("SELECT * FROM " + nombreTabla);
            while (oBD.pLector.Read())
            {
                Mascota m = new Mascota();
                if (!oBD.pLector.IsDBNull(0))
                    m.pCodigo = oBD.pLector.GetInt32(0);

                if (!oBD.pLector.IsDBNull(1))
                    m.pNombre = oBD.pLector.GetString(1);

                if (!oBD.pLector.IsDBNull(2))
                    m.pEspecie = oBD.pLector.GetInt32(2);

                if (!oBD.pLector.IsDBNull(3))
                    m.pSexo = oBD.pLector.GetInt32(3);

                if (!oBD.pLector.IsDBNull(4))
                    m.pFechaNacimiento = oBD.pLector.GetDateTime(4);

                lMascotas.Add(m);
            }

            oBD.desconectar(); // lo desconecto despues de usar el data reader
            lista.Items.Clear();

            for (int i = 0; i < lMascotas.Count; i++)
            {
                lista.Items.Add(lMascotas[i].ToString());
            }
            lista.SelectedIndex = 0;
        }


        private void btnNuevo_Click(object sender, EventArgs e)
        {

            nuevo = true;
            habilitar(true);
            limpiar();
            txtCodigo.Focus();
  

        }
  
        private void btnSalir_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Seguro de abandonar la aplicación ?",
                 "SALIR", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                 MessageBoxDefaultButton.Button2) == DialogResult.Yes)

                this.Close();
        }
     
        private void btnGrabar_Click(object sender, EventArgs e)
        {
            if (txtCodigo.Text == "")
            {
                MessageBox.Show("Debe ingresar un codigo");
                return;
            }

            if (txtNombre.Text == "")
            {
                MessageBox.Show("Debe ingresar un Nombre");
                return;
            }

            if (!rbtMacho.Checked && !rbtHembra.Checked)
            {
                MessageBox.Show("Debe seleccionar un sexo...");
                rbtMacho.Focus();
                return;
            }

            if (DateTime.Today <= dtpFechaNacimiento.Value)
            {
                MessageBox.Show("No puede registrar una mascota mayor a 10 años...");
                dtpFechaNacimiento.Focus();
                return;
            }
       


            Mascota m = new Mascota();
            m.pCodigo = int.Parse(txtCodigo.Text);
            m.pNombre = txtNombre.Text;
            m.pEspecie = (int)cboEspecie.SelectedValue;

            if (rbtMacho.Checked)
                m.pSexo = 1;
            else
                m.pSexo = 2;

            m.pFechaNacimiento = dtpFechaNacimiento.Value;

            if (nuevo)
            {
                string insertSQL = $"INSERT INTO Mascotas VALUES ({m.pCodigo},'{m.pNombre}',{m.pEspecie},{m.pSexo},'{m.pFechaNacimiento.ToString("yyyy/MM/dd")}')";

                oBD.actualizarBD(insertSQL);
                cargarLista(lstMascotas, "Mascotas");
            }

            habilitar(false);
            limpiar();

            btnGrabar.Enabled = false;//me deshabilitar el boton guardar despues de guardar
            MessageBox.Show("Operacion Exitosa");
        }


        private void limpiar()
        {
            txtCodigo.Text = "";
            txtNombre.Text = "";
            cboEspecie.SelectedIndex = -1;
            rbtMacho.Checked = false;
            rbtHembra.Checked = false;
            dtpFechaNacimiento.Value = DateTime.Today;
        }
     
        private void habilitar(bool x)
        {
            txtCodigo.Enabled = x;
            txtNombre.Enabled = x;
            cboEspecie.Enabled = x;
            rbtMacho.Enabled = x;
            rbtHembra.Enabled = x;
            dtpFechaNacimiento.Enabled = x;
            lstMascotas.Enabled = !x; 
            btnGrabar.Enabled = x;
            btnSalir.Enabled = !x;
        }

        private void lstMascotas_SelectedIndexChanged(object sender, EventArgs e)
        {

            cargarCampos(lstMascotas.SelectedIndex);
        }

        private void cargarCampos(int posicion)
        {
            txtCodigo.Text = lMascotas[posicion].pCodigo.ToString();
            txtNombre.Text = lMascotas[posicion].pNombre;
            cboEspecie.SelectedValue = lMascotas[posicion].pEspecie;
            if (lMascotas[posicion].pSexo == 1)
                rbtMacho.Checked = true;
            else
                rbtHembra.Checked = true;
            dtpFechaNacimiento.Value = lMascotas[posicion].pFechaNacimiento;
        }
    }
}
