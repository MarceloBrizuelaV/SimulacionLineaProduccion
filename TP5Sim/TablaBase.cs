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
            double[,] vector = new double[1, 33];

            return vector;
        }


        //Esta funcion recorre el vector de estado y devuelve el tiempo menor para determinar el proximo evento por venir
        private double[] buscarMenor(double[] vector)
        {
            //Configuramos el menor como la primera columna que chequear
            double menor = vector[4];
            int posicion = 4;
            //Vector con la posicion y el valor menor
            double[] vectorMenor = { 4, menor };
            //El array contiene las posiciones de las columnas para chequear
            int[] numeros = { 8, 11, 17, 20 };
            //Columnas que chequear
            //4 Proxima llegada A
            //8 Proxima llegada M
            //11 Proximo ensamblaje
            //17 Proxima llegada
            //20 Proximo triciclo
            for (int i = 8; i <= 20; i++)
            {
                if (numeros.Contains(i)) {
                    if (vector[i] < menor)
                    {
                        menor = vector[i];
                        posicion = i;
                    };
                };
            };
            vectorMenor[0] = posicion;
            vectorMenor[1] = menor;
            return vectorMenor;
        }


    }

}
