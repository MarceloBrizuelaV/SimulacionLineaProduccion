using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TP5Sim
{
    public partial class FormPrincipal : Form
    {
        //Form
        public FormPrincipal()
        {
            InitializeComponent();
        }

        //Form load
        private void Form1_Load(object sender, EventArgs e)
        {

        }


        //Este no
        private void DataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


        //Este no
        private void Label1_Click(object sender, EventArgs e)
        {

        }

        //Este no
        private void LabelHasta_Click(object sender, EventArgs e)
        {

        }

        //Este no
        private void TxtHasta_TextChanged(object sender, EventArgs e)
        {

        }

        //Boton
        private void btnSimular_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            
            TablaBase genTabla = new TablaBase();

            //Variables
            double tiempoArmazon;
            double limiteSuperiorMotor;
            double limiteInferiorMotor;
            double mediaRuedas;
            double desviacion;
            double tiempoEnsamblajeAM;
            double tiempoEnsamblajeRuedas;

            //En caso de ser True, utiliza los valores oringinales del ejercicio
            if (chkValOriginales.Checked)
            {
                tiempoArmazon = Convert.ToDouble(10.0001);
                limiteSuperiorMotor = Convert.ToDouble(40);
                limiteInferiorMotor = Convert.ToDouble(30);
                mediaRuedas = Convert.ToDouble(70);
                desviacion = Convert.ToDouble(8);
                tiempoEnsamblajeAM = Convert.ToDouble(10);
                tiempoEnsamblajeRuedas = Convert.ToDouble(5);
            }
            else
            {
                tiempoArmazon = Convert.ToDouble(txtLlegadaArmazon.Text);
                limiteSuperiorMotor = Convert.ToDouble(txtLimiteMaximo.Text);
                limiteInferiorMotor = Convert.ToDouble(txtLimiteInferior.Text);
                mediaRuedas = Convert.ToDouble(txtMediaRuedas.Text);
                desviacion = Convert.ToDouble(txtDesviacionRuedas.Text);
                tiempoEnsamblajeAM = Convert.ToDouble(txtEnsamblajeAM.Text);
                tiempoEnsamblajeRuedas = Convert.ToDouble(txtEnsamblajeRuedas.Text);
            }


            double[,] vs = genTabla.generarVector();
            for (int i = 0; i < Convert.ToInt32(txtCantidadSim.Text); i++)
            {
                
                genTabla.generarTabla(vs, tiempoArmazon, limiteSuperiorMotor, limiteInferiorMotor, mediaRuedas, desviacion, tiempoEnsamblajeAM, tiempoEnsamblajeRuedas);

                if (i == 0 || ( (i + 1) >= Convert.ToInt32(txtDesde.Text) && i < Convert.ToInt32(txtHasta.Text)))
                {
                    Herramientas.matrizAGrid2(vs, dataGridView1, 6);
                    Herramientas.setearTipoEstado(dataGridView1);
                    Herramientas.setearEvento(dataGridView1);


                }


                /*
                for (int z = 0; z < vs.GetLength(0); z++)
                {
                    for (int j = 0; j < vs.GetLength(1); j++)
                    {
                        Console.Write(vs[z, j] + "\t");
                    }
                    Console.WriteLine();

                }

                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                */

            }

            //Se muestra la ultima fila
            Herramientas.matrizAGrid2(vs, dataGridView1, 6);
            Herramientas.setearEvento(dataGridView1);


            //Calculo los valores
            double[] valores = new double[2];
            valores = genTabla.calcularPorcentajes(dataGridView1);

            txtPorEnsamblej.Text = Herramientas.TruncadoMarcelo(valores[0],2).ToString() + "%";
            txtPorAreaRuedas.Text = Herramientas.TruncadoMarcelo(valores[1], 2).ToString() + "%";

            //Obtengo la cantidad de triciclos y la muestro
            txtCantTriciclos.Text = dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[25].Value.ToString();

            //Obtengo las colas maximas y las muestro
            txtCantMaxRuedas.Text = dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[27].Value.ToString();
            txtCantMaxMotores.Text = dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[26].Value.ToString();
            txtCantMaxAM.Text = dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[28].Value.ToString();


        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void label19_Click(object sender, EventArgs e)
        {

        }
    }
}
