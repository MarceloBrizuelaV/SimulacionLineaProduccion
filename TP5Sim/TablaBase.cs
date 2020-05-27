using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP5Sim
{
    class TablaBase
    {
        //Posiciones de las columnas
        // 0 = Fila 1=Evento 2=Reloj 
        // 3=Tiempo llegada A 4=Proxima Llegada A 5=Stock A 6=Estado A
        // 7=RND M 8=Tiempo llegada M 9= Proxima Llegada M 10= Stock M 11=Estado M
        // 12= Tiempo ensamblaje E 13= Proximo ensamblaje E 14= Estado E
        // 15= RND1 R 16= RND2 R 17=Tiempo llegada R 18= Proxima llegada R 19= Stock R 20= Estado R
        // 21= Tiempo ensamblaje AR 22= Proximo Triciclo AR 23= Estado AR
        // 24= Tricilo1 25= Triciclo2 26= Tricilo3 27= Tricilo4 28= Triciclo 5
        // 29= TI area ensamblaje 30= TI area rueda 31= TI total 32= Cantidad de triciclos

        public double[,] generarVector() 
        {
            double[,] vector = new double[1, 33];

            return vector;
        }
    }
}
