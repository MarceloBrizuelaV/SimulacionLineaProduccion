using System;
using System.Collections.Generic;
using System.Configuration.Assemblies;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            double[,] vector = new double[2, 29];

            return vector;
        }


        public void generarTabla(double[,] vector)
        {
            GeneradorVariables generador = new GeneradorVariables();
            Random rnd = new Random();

            double[] vectorMenor = buscarMenor(vector);

            //Obtengo el evento
            vector[1, 1] = vectorMenor[0];
            //Obtengo el reloj
            vector[1, 2] = vectorMenor[1];

            switch (vector[1, 1])
            {
            
                //Proxima Llegada armazon
                case 4:
                    //Verifica Si el area de ensamblaje esta libre (0 es libre)
                    if (vector[0, 13] == 0)
                    {
                        //Verifica si hay motores (Si es diferente de 0 Hay)
                        if (vector[0, 9] != 0)
                        {
                            vector[1, 9] = vector[0, 9] - 1;
                            vector[1, 13] = 1;
                            vector[1, 11] = vector[1, 2] + vector[1, 10];
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


                    //Agrego 5 motores al stock
                    vector[1, 9] = vector[1, 9] + 5;

                    //Verifico si el area ensamblaje esta libre y si disponemos de armazones
                    int estado = Convert.ToInt32(vector[0, 13]);
                    int cantidadArmazon = Convert.ToInt32(vector[0, 5]);

                    if (estado == 0 && cantidadArmazon > 0)
                    {
                        //Asigno el estado ocupado
                        vector[1, 13] = 1;

                        //Resto los stock de Armazon y Motor
                        vector[1, 5] = vector[0, 5] - 1;
                        vector[1, 9] = vector[0, 9] - 1;

                    }

                    //Calculo la proxima llegada de motores
                    double random = Convert.ToDouble(rnd);
                    int limiteInf = 30;
                    int limiteSup = 40;
                    double tiempoLlegada = generador.Uniforme(limiteInf, limiteSup, random);

                    //Asigno RND
                    vector[1, 6] = random;
                    //Asigno Tiempo Llegada
                    vector[1, 7] = tiempoLlegada;
                    //Asigno Proxima Llegada
                    vector[1, 8] = tiempoLlegada + vector[1, 2];


                    break;
                    


                //Proximo ensamblaje
                        case 11:
                            //Colocacion de Ruedas
                            //Esta el area de ruedas libre?
                            if (vector[0,21] == 0){
                                //Calcular Proximo Triciclo -- Tiempo de Reloj + 5 Minutos
                                vector[1, 20] = vector[1, 2] + 5;
                                //Actualizar Stock de Ruedas -3
                                vector[1, 18] = vector[0, 18] - 3;
                                //Actualizar Fin_Area_Ruedas a Ocupado
                                vector[1, 21] = 1;
                            }
                            else
                            {
                                //En caso de que el area de ruedas este ocupada sumamos un Armazon + Motor al Stock esperando la desocupacion
                                //Stock AM + 1
                                vector[1, 12] = vector[0, 12] + 1;
                            }
                            //Comienzo de Nuevo ensamblaje, verificamos que haya un Motor y un Armazon
                            if (vector[0, 5] > 0 && vector[0, 9] > 0)
                            {
                                //Calculamos la finalizacion del proximo ensamblaje
                                vector[1, 11] = vector[1, 2] + 10;
                                //Cambiamos el estado del area de ensamblaje a Ocupado
                                vector[1, 13] = 1;
                            }
                            else
                            {
                                //Cambiamos el estado del area de ensamblaje a libre
                                vector[1, 13] = 0;
                            }
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

                //Fin Area Ruedas
                case 20:
                    //Asigno estado libre al area de ruedas
                    vector[1, 21] = 0;

                    //Sumo 1 a la columna cantidad de triciclos
                    vector[1, 25] = vector[1, 25] + 1;

                    //Reviso si hay ruedas disponibles y AM disponibles
                    if (vector[0, 18] >= 3 && vector[0, 12] > 0)
                    {
                        //Asigno tiempo llegada
                        vector[1, 19] = 5;

                        //Asigno proxima llegada
                        vector[1,20] = 5 + vector[1,2];

                        //Resto la cantidad de ruedas utilizadas
                        vector[1, 18] = vector[0,18] - 3;
                    }


                    break;

                default:

                    break;
            }

        }
        //Esta funcion recorre el vector de estado y devuelve el tiempo menor para determinar el proximo evento por venir
        private double[] buscarMenor(double[,] vector)
        {
            //Configuramos el menor como la primera columna que chequear
            double menor = vector[0,4];
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
                    if (vector[0,i] < menor)
                    {
                        menor = vector[0,i];
                        posicion = i;
                    };
                };
            };
            vectorMenor[0] = posicion;
            vectorMenor[1] = menor;
            return vectorMenor;

        }

        //Estado LIBRE = 0
        //Estado OCUPADO = 1
        public double tiempoInactividadAR(double[,] vector)
        {
            double TI = 0;

            //Tiempo de inactividad del Area de Ruedas
            int estadoEAct = Convert.ToInt32(vector[1, 21]);

            if (estadoEAct == 0)
            {
                //Seria el TI de la fila anterior mas el reloj anterior menos el inicial
                TI = vector[0,23] + vector[1, 2] - vector[0, 2];
            }
            else
            {
                //Seria el TI de la fila anterior, ya que esta ocupado.
                TI = vector[0, 23];
            }
    
            return TI;
        }

        public double tiempoInactividadE(double[,] vector) 
        {
            double TI = 0;

            //Tiempo de inactividad del Area de ensamblaje
            int estadoE = Convert.ToInt32(vector[1, 13]);

            if (estadoE == 0)
            {
                //Seria el TI de la fila anterior mas el reloj anterior menos el inicial
                TI = vector[0, 22] + vector[1, 2] - vector[0, 2];
            }
            else
            {
                //Seria el TI de la fila anterior, ya que esta ocupada
                TI = vector[0, 22];
            }

            return TI;
        }

        public double tiempoInactividadTotal(double[,] vector)
        {
            //Aca lo que hago es la sumatoria del TI de rueda y el TI de ensamblaje

            double tiRuedas = vector[1,23];
            double tiEnsamblaje = vector[1,22];

            double tit = tiRuedas + tiEnsamblaje + vector[0,24];

            return tit;
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
