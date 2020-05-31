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
            TablaBase genTabla = new TablaBase();

            double[,] vs = genTabla.generarVector();
            for (int i = 0; i < Convert.ToInt32(txtCantidadSim.Text); i++)
            {
                genTabla.generarTabla(vs);
                //Herramientas.matrizAGrid(vs, dataGridView1, 4);
                
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


            }

            

        }
    }
}
