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

        public static String setearTipoDia(double val)
        {
            String err = "";
            switch (Convert.ToInt32(val))
            {
                case 1:
                    return "Soleado";
                    break;
                case 2:
                    return "Nublado";
                    break;
                default:
                    break;
            }
            return err;

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

    }
}
