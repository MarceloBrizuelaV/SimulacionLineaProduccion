using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TP5Sim
{
    class Herramientas
    {
        public static double TruncadoMarcelo(double valor, int cantATruncar)
        {
            int factor = Convert.ToInt32(Math.Pow(10, cantATruncar));
            return (Math.Truncate(valor * factor)) / factor;
        }

        public static void setearTipoEstado(DataGridView grilla)
        {

            for (int i = 0; i < grilla.Rows.Count; i++)
            {
                switch (grilla.Rows[i].Cells[13].Value)
                {
                    case "0":
                        grilla.Rows[i].Cells[13].Value = "Libre";
                        break;
                    case "1":
                        grilla.Rows[i].Cells[13].Value = "Ocupado";
                        break;
                    default:
                        break;
                }
            }

            for (int i = 0; i < grilla.Rows.Count; i++)
            {
                switch (grilla.Rows[i].Cells[21].Value)
                {
                    case "0":
                        grilla.Rows[i].Cells[21].Value = "Libre";
                        break;
                    case "1":
                        grilla.Rows[i].Cells[21].Value = "Ocupado";
                        break;
                    default:
                        break;
                }
            }
        }

        public static void setearEvento(DataGridView grilla) 
        {
            for (int i = 0; i < grilla.Rows.Count; i++)
            {
                switch (grilla.Rows[i].Cells[1].Value)
                {
                    case "0":
                        grilla.Rows[i].Cells[1].Value = "Inicializacion";
                        break;
                    case "4":
                        grilla.Rows[i].Cells[1].Value = "Llegada_Armazon";
                        break;
                    case "8":
                        grilla.Rows[i].Cells[1].Value = "Llegada_Motor";
                        break;
                    case "17":
                        grilla.Rows[i].Cells[1].Value = "Llegada_Ruedas";
                        break;
                    case "11":
                        grilla.Rows[i].Cells[1].Value = "Fin_Ensamblaje";
                        break;
                    case "20":
                        grilla.Rows[i].Cells[1].Value = "Fin_Area_Rueda";
                        break;
                    default:
                        break;
                }
            }
        }

        public static void matrizAGrid(double[,] matriz, System.Windows.Forms.DataGridView dataGridView, int ordenTruncado)
        {
            if (dataGridView.Rows.Count == 0)
            {
                for (int i = 0; i < matriz.GetLength(0); i++)
                {
                    dataGridView.Rows.Add();
                    for (int c = 0; c < matriz.GetLength(1); c++)
                    {
                        dataGridView.Rows[i].Cells[c].Value = TruncadoMarcelo(matriz[i, c], ordenTruncado).ToString();
                    }
                }
            }
            else
            {

                for (int j = 0; j < matriz.GetLength(0); j++)
                {
                    dataGridView.Rows.Add();

                    for (int i = 0; i < matriz.GetLength(1); i++)
                    {
                        dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[i].Value = TruncadoMarcelo(matriz[j, i], ordenTruncado).ToString();
                    }
                }

            }

        }

        public static void truncarDataGrid(DataGridView dataGridView, int ordenTruncado)
        {
            for (int i = 0; i < dataGridView.Rows.Count; i++)
            {
                for (int j = 0; j < dataGridView.Columns.Count; j++)
                {
                    if (dataGridView.Rows[i].Cells[j].Value is Double)
                    {
                        dataGridView.Rows[i].Cells[j].Value = TruncadoMarcelo(Convert.ToDouble(dataGridView.Rows[i].Cells[j].Value), 4);
                    }

                }
            }
        }

        public static void matrizAGrid2(double[,] vs, DataGridView data, int ordenTruncado)
        {
            
            if (data.Rows.Count == 0)
            {
                data.Rows.Add();
                for (int i = 0; i < vs.GetLength(1); i++)
                {
                      data.Rows[data.Rows.Count - 1].Cells[i].Value = TruncadoMarcelo(vs[0, i], ordenTruncado).ToString();   
                }
            }
            else
            {
                data.Rows.Add();
                for (int i = 0; i < vs.GetLength(1); i++)
                {
                    data.Rows[data.Rows.Count - 1].Cells[i].Value = TruncadoMarcelo(vs[1, i], ordenTruncado).ToString();
                }
            }
        }

    }
}
