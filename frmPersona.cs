using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;

using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ABMPersonas
{
    public partial class frmPersona : Form
    {
       
        bool nuevo = false;
        const int tamanio = 10;
        Persona[] aPersona = new Persona[tamanio];
        SqlConnection cnn = new SqlConnection(@"Data Source=(localdb)\LOCAL;Initial Catalog=TUPPI;Integrated Security=True");
        SqlCommand comando = new SqlCommand();
        //SqlDataReader lector = new SqlDataReader();



        public frmPersona()
        {
            InitializeComponent();
        }

        private void frmPersona_Load(object sender, EventArgs e)
        {
            habilitar(false);
            //cargar combo
            //cnn.Open();
            cargarCombo("tipo_documento", "id_tipo_documento","n_tipo_documento",  cboTipoDocumento);
            cargarCombo("estado_civil", "id_estado_civil", "n_estado_civil" ,cboEstadoCivil);



        }

        private void cargarCombo(string nombretabla, string ValueMember, string DisplayMember, ComboBox cbo)
        {

          
            cnn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select * from "+ nombretabla;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;

      

            SqlDataReader Reader = cmd.ExecuteReader();
           


            
            DataTable Tabla = new DataTable();
            Tabla.Load(Reader) ; 


            cbo.DataSource = Tabla;
            cbo.ValueMember = ValueMember;

            cbo.DisplayMember = DisplayMember;

           
            cnn.Close();
            //Reader.Close();


            //el SqlConnection SqlDataReader y el SqlCommand son objetos de ado.net que se usan para 
            //llegar a la bd obtener un comando y obtener un conjnto de resultados



            this.cargarLista(lstPersonas, "Personas");
        }
         private void cargarLista(ListBox lista, string nombreTabla)
        {
            //abrre base de datos utilizando el  comando coneccion creado enteriormente
            cnn.Open();
            comando.Connection = cnn;
            
            // configura el comando
            comando.CommandType = CommandType.Text;
            //ejecuta el comando
            comando.CommandText = "SELECT * FROM " + nombreTabla;
            // lee lo que se ejecuto anteriormente 
            //SqlDataReader Reader = comando.ExecuteReader();
            cnn.Close();

        }
       


        private void habilitar(bool x)
        {
            txtApellido.Enabled = x;
            txtNombres.Enabled = x;
            cboTipoDocumento.Enabled = x;
            txtDocumento.Enabled = x;
            cboEstadoCivil.Enabled = x;
            rbtFemenino.Enabled = x;
            rbtMasculino.Enabled = x;
            chkFallecio.Enabled = x;
            btnGrabar.Enabled = x;
            btnCancelar.Enabled = x;
            btnNuevo.Enabled = !x;
            btnEditar.Enabled = !x;
            btnBorrar.Enabled = !x;
            btnSalir.Enabled = !x;
            lstPersonas.Enabled = !x;


            
        }

        private void limpiar()
        {
            txtApellido.Text = "";
            txtNombres.Text = "";
            cboTipoDocumento.SelectedIndex = -1;
            txtDocumento.Text = "";
            cboEstadoCivil.SelectedIndex = -1;
            rbtFemenino.Checked = false;
            rbtMasculino.Checked = false;
            chkFallecio.Checked = false;
        }
      
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            nuevo = true; /*aviso que voy a insertar un registro*/
            habilitar(true);
            limpiar();
            txtApellido.Focus();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            habilitar(true);
            txtDocumento.Enabled = false;
            txtApellido.Focus();
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
                

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            
            limpiar();
            habilitar(false);
            nuevo = false;
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {


            
            
           if (nuevo) //(nuevo==true) es equivalente
            {

                // VALIDAR QUE NO EXISTA LA PK !!!!!! (SI NO ES AUTOINCREMENTAL / IDENTITY)

                // insert con sentencia SQL tradicional

                // insert usando parámetros

                //SqlConnection cnn = new SqlConnection(@"Data Source=(localdb)\LOCAL;Initial Catalog=TUPPI;Integrated Security=True");
                //abrir coneccion a la db
                cnn.Open();
                SqlCommand cmd = new SqlCommand();

                int tipoDocumento = int.Parse(cboTipoDocumento.SelectedValue.ToString());
                int estadoCivil = int.Parse(cboEstadoCivil.SelectedValue.ToString());
                int nroDocumento = int.Parse(txtDocumento.Text);
                int sexo = 1;
                if (rbtMasculino.Checked)
                {
                    sexo = 2;
                }

                Persona oPersona = new Persona(txtApellido.Text, txtNombres.Text, tipoDocumento, nroDocumento, estadoCivil, sexo, chkFallecio.Checked);


                cmd.CommandText = "insert into personas(apellido, nombres, tipo_documento,documento,estado_civil, sexo, fallecio) values('" + oPersona.pApellido + "','" + oPersona.pNombres + "'," + oPersona.pTipoDocumento +
                    "," + oPersona.pDocumento + "," + oPersona.pEstadoCivil + "," + oPersona.pSexo + ",'" + oPersona.pFallecio + "')";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cnn;
                cmd.ExecuteNonQuery();

               
                cnn.Close();






                habilitar(false);
                nuevo = false;
            }
        }

     
        private void btnSalir_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Seguro de abandonar la aplicación ?",
                "SALIR", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                this.Close();
        }

        private void txtApellido_TextChanged(object sender, EventArgs e)
        {

        }

        private void cboEstadoCivil_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtDocumento_TextChanged(object sender, EventArgs e)
        {

        }

        private void lstPersonas_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
