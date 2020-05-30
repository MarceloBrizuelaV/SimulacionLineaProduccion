using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP5Sim
{
    class TablaBase
    {
        private static bool nuevosRandom = true;
        /*
        //Posiciones de las columnas
        0 = Fila 
        1=Evento 
        2=Reloj 
        3=Tiempo llegada A 
        4=Proxima Llegada A 
        5=Stock A
        6=RND M 
        7=Tiempo llegada M 
        8= Proxima Llegada M 
        9= Stock M 
        10= Tiempo ensamblaje E 
        11= Proximo ensamblaje E 
        12= Stock AM 
        13= Estado AM
        14= RND1 R 
        15= RND2 R 
        16=Tiempo llegada R 
        17= Proxima llegada R 
        18= Stock R 
        19= Tiempo ensamblaje AR 
        20= Proximo Triciclo AR  
        21 = Estado AR
        22= TI area ensamblaje 
        23= TI area rueda 
        24= TI total 
        25= Cantidad de triciclos 
        26=Cola Maxima Motores 
        27= Cola Maxima Ruedas 
        28= Cola maxima AM


    */
        public double[,] generarVector() 
        {
            double[,] vector = new double[1, 29];

            return vector;
        }

        public void generarTabla(double [,] vector)
        {
            GeneradorVariables generador = new GeneradorVariables();
            Random rnd = new Random();
            for (int i = 0; i < vector.GetLength(0); i++)
            {

                switch (vector[1,1])
                {
                    //Proxima Llegada armazon
                    case 4:
                        //Verifica Si el area de ensamblaje esta libre (0 es libre)
                        if (vector[0,13] == 0)
                        {
                            //Verifica si hay motores (Si es diferente de 0 Hay)
                            if (vector[0,9] != 0)
                            {
                                vector[1,9] = vector[0,9] - 1;
                                vector[1,13] = 1;
                                vector[1, 11] = vector[1,2] + vector[1,10];
                            }
                            else
                            {
                                vector[1, 5] = vector[0, 5] + 1;
                            }
                        }
                        //No esta libre el area de ensamblaje
                        else
                        {
                            vector[1, 5] = vector[0, 5] + 1;
                        }
                        break;
                    
                    //Proxima Llegada Motor
                    case 8:
                        
                        break;
                    
                    //Proximo ensamblaje
                    case 11:
                        
                        break;
                    
                    //Proxima llegada de ruedas
                    case 17:
                        if (nuevosRandom)
                        {
                            vector[1,14] = rnd.NextDouble();
                            vector[1,15] = rnd.NextDouble();

                            nuevosRandom = !nuevosRandom;
                        }
                        else
                        {
                            nuevosRandom = !nuevosRandom;
                        }
                        //Pide Ruedas
                        vector[1, 16] = generador.Normal(vector[1, 14], vector[1, 15], 70, 8);
                        vector[1, 17] = vector[1, 2] + vector[1, 16];

                        //Agregamos las ruedas que pedimos
                        vector[1, 18] = vector[0, 18] + 20;

                        //Verificamos si puede armar triciclos (Si fin_Area_Ruedas esta libre)
                        if (vector[0,21] == 0)
                        {
                            //Esta libre y verificamos si hay conjuntos AM
                            if (vector[0,12] != 0)
                            {
                                //Restamos las 3 ruedas
                                vector[1, 18] = vector[1, 18] - 3;
                                //Restamos un conjunto AM
                                vector[1, 12] = vector[0, 12] - 1;
                                //Cambiamos estado Fin_Area_Ruedas
                                vector[1, 21] = 1;
                                //Colocamos tiempo de ensamblaje
                                vector[1, 19] = 5;
                                //Calculamos el fin del ensamblaje
                                vector[1, 20] = vector[1, 2] + vector[1, 19];
                            }
                        }
                        break;
                    
                    //Proximo Triciclo
                    case 20:
                        
                        break;

                    default:
                        
                        break;
                }
            }
        }
    }
}
