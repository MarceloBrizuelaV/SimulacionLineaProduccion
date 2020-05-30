using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TP5Sim
{
    class TablaBase
    {
        //Posiciones de las columnas
        // 0 = Fila 1=Evento 2=Reloj 
        // 3=Tiempo llegada A 4=Proxima Llegada A 5=Stock A
        // 6=RND M 7=Tiempo llegada M 8= Proxima Llegada M 9= Stock M 
        // 10= Tiempo ensamblaje E 11= Proximo ensamblaje E 12= Stock AM 13= Estado AM
        // 14= RND1 R 15= RND2 R 16=Tiempo llegada R 17= Proxima llegada R 18= Stock R 
        // 19= Tiempo ensamblaje AR 20= Proximo Triciclo AR  21 = Estado AR
        // 
        // 22= TI area ensamblaje 23= TI area rueda 24= TI total 25= Cantidad de triciclos 
        //26=Cola Maxima Motores 27= Cola Maxima Ruedas 28= Cola maxima AM

        public double[,] generarVector() 
        {
            double[,] vector = new double[2, 29];

            return vector;
        }

        public void generarTabla(double [,] vector)
        {
            for (int i = 0; i < vector.GetLength(0); i++)
            {

                switch (vector[1,2])
                {
                    //Proxima Llegada armazon
                    case 4:
                        
                        break;
                    
                    //Proxima Llegada Motor
                    case 8:
                        
                        break;
                    
                    //Proximo ensamblaje
                    case 11:
                        
                        break;
                    
                    //Proxima llegada de ruedas
                    case 17:
                        
                        break;
                    
                    //Proximo Triciclo
                    case 20:
                        
                        break;

                    default:
                        
                        break;
                }
            }
        }

        //------------------------ COLAS MAXIMAS -----------------------------------
        public int colaMaximaMotores(DataGridView grilla)
        {
            int stockMotores_0 = Convert.ToInt32(grilla.Rows[0].Cells[9].Value);
            int stockMotores_1 = Convert.ToInt32(grilla.Rows[1].Cells[9].Value);
            int CM; 

            if (stockMotores_1 > stockMotores_0)
            {
                CM = stockMotores_1;
            }
            else
            {
                CM = stockMotores_0;
            }
            
            return CM;
        }

        public int colaMaximaAM(DataGridView grilla)
        {
       
            int stockAM_0 = Convert.ToInt32(grilla.Rows[0].Cells[12].Value);
            int stockAM_1 = Convert.ToInt32(grilla.Rows[1].Cells[12].Value);
            int CM;

            if (stockAM_1 > stockAM_0)
            {
                CM = stockAM_1;
            }
            else
            {
                CM = stockAM_0;
            }

            return CM;
        }

        public int colaMaximaRuedas(DataGridView grilla)
        {
       
            int stockRuedas_0 = Convert.ToInt32(grilla.Rows[0].Cells[18].Value);
            int stockRuedas_1 = Convert.ToInt32(grilla.Rows[1].Cells[18].Value);
            int CM;

            if (stockRuedas_1 > stockRuedas_0)
            {
                CM = stockRuedas_1;
            }
            else
            {
                CM = stockRuedas_0;
            }

            return CM;
        }

        //----------------------------------------------------------------------------------------


    }
}
