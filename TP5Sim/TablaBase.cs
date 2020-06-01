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

            // El problema es este, nos olvidamos que primero tenemos que pasar la fila 2 al lugar de la 1 y despues copiarla
            //FILA ORIGEN -------FILA DESTINO
            if (vector[1,0] != 0)
            {
                copiarFila(vector, 1, 0);
            }

            copiarFila(vector, 0, 1);

            double[] vectorMenor = buscarMenor(vector);

            

            //Obtengo el evento
            vector[1, 1] = vectorMenor[0];
            //Obtengo el reloj
            vector[1, 2] = vectorMenor[1];

            switch (vector[1, 1])
            {
            
                //Proxima Llegada armazon
                case 4://Ver casos especiales
                    //Verifico si hay armazones en espera
                    if (vector[0,5] == 0)
                    {
                        //Verifica Si el area de ensamblaje esta libre (0 es libre)
                        if (vector[0, 13] == 0)
                        {
                            //Verifica si hay motores (Si es diferente de 0 Hay)
                            if (vector[0, 9] != 0)
                            {
                                //Resto un motor
                                vector[1, 9] = vector[0, 9] - 1;
                                //Cambio el estado del area del ensamblaje
                                vector[1, 13] = Convert.ToDouble(1);
                                //Tiempo de ensamblaje
                                vector[1, 10] = Convert.ToDouble(10);
                                //Calculo tiempo proximo ensamblaje
                                vector[1, 11] = vector[1, 2] + vector[1, 10];
                                //Calculo el tiempo del proximo Armazon
                                vector[1, 4] = vector[1, 2] + 10.0001;
                            }
                            else
                            {
                                //Agrego Stock de armazon
                                vector[1, 5] = vector[0, 5] + 1;
                                vector[1, 3] = 0;
                            }
                        }
                        //No esta libre el area de ensamblaje
                        else
                        {
                            //Agrego Stock de Armazon
                            vector[1, 5] = vector[0, 5] + 1;
                            vector[1, 3] = 0;
                        }
                    }
                    else
                    {
                        vector[1, 3] = 0;
                    }


                        
                    
                    


                    //Asigno el valor de inactividad del area Ensamblaje
                    double tiempoEnsamblaje = tiempoInactividadE(vector);
                    vector[1, 22] = tiempoEnsamblaje;

                    //Asigno el valor de inactividad del area Ruedas
                    double tiempoAreaRuedas = tiempoInactividadAR(vector);
                    vector[1, 23] = tiempoAreaRuedas;

                    //Asigno el valor de inactividad total
                    double tiempoAreaTotal = tiempoInactividadTotal(vector);
                    vector[1, 24] = tiempoAreaTotal;

                    //Asigno el valor de la Cola Maxima Motores
                    vector[1, 26] = colaMaximaMotores(vector);

                    //Asigno el valor de la Cola Maxima AM
                    vector[1, 28] = colaMaximaAM(vector);

                    //Asigno el valor de la Cola Maxima Ruedas
                    vector[1, 27] = colaMaximaRuedas(vector);


                    break;

                //Proxima Llegada Motor
                case 8:
                    //Agrego 5 motores al stock
                    vector[1, 9] = vector[0, 9] + 5;

                    //Verifico si el area ensamblaje esta libre y si disponemos de armazones
                    int estado = Convert.ToInt32(vector[0, 13]);
                    int cantidadArmazon = Convert.ToInt32(vector[0, 5]);

                    if (estado == 0 && cantidadArmazon > 0)
                    {
                        //Asigno el estado ocupado
                        vector[1, 13] = 1;
                        //Asigno tiempo llegada E
                        vector[1, 10] = 10;
                        //Calculo la Proxima llegada E
                        vector[1, 11] = vector[1, 2] + vector[1, 10];

                        //Calculo el proximo armazon
                        vector[1, 3] = 10.0001;
                        vector[1, 4] = vector[1,2] + 10.0001;

                        //Resto los stock de Armazon y Motor
                        vector[1, 5] = vector[1, 5] - 1;
                        vector[1, 9] = vector[1, 9] - 1;

                    }

                    //Calculo la proxima llegada de motores
                    double random = rnd.NextDouble();
                    int limiteInf = 30;
                    int limiteSup = 40;
                    double tiempoLlegada = generador.Uniforme(limiteInf, limiteSup, random);

                    //Asigno RND
                    vector[1, 6] = random;
                    //Asigno Tiempo Llegada
                    vector[1, 7] = tiempoLlegada;
                    //Asigno Proxima Llegada
                    vector[1, 8] = tiempoLlegada + vector[1, 2];

                    //Asigno el valor de inactividad del area Ensamblaje
                    double tiempoEnsamblaj = tiempoInactividadE(vector);
                    vector[1, 22] = tiempoEnsamblaj;

                    //Asigno el valor de inactividad del area Ruedas
                    double tiempoAreaRueda = tiempoInactividadAR(vector);
                    vector[1, 23] = tiempoAreaRueda;

                    //Asigno el valor de inactividad total
                    double tiempoAreaTota = tiempoInactividadTotal(vector);
                    vector[1, 24] = tiempoAreaTota;

                    //Asigno el valor de la Cola Maxima Motores
                    vector[1, 26] = colaMaximaMotores(vector);

                    //Asigno el valor de la Cola Maxima AM
                    vector[1, 28] = colaMaximaAM(vector);

                    //Asigno el valor de la Cola Maxima Ruedas
                    vector[1, 27] = colaMaximaRuedas(vector);


                    break;
                    


                //Proximo ensamblaje
                case 11:
                    //Colocacion de Ruedas
                    //Esta el area de ruedas libre?

                    if (vector[0,21] == 0 && vector[0, 18] >= 3)
                    {
                        //Calcular Proximo Triciclo -- Tiempo de Reloj + 5 Minutos
                        vector[1, 20] = vector[1, 2] + 5;
                        //Actualizar Stock de Ruedas -3
                        vector[1, 18] = vector[0, 18] - 3;
                        //Actualizar Fin_Area_Ruedas a Ocupado
                        vector[1, 21] = 1;
                    }
                    else
                    {
                        //En caso de que el area de ruedas este ocupada sumamos un AM al Stock esperando la desocupacion
                        //Stock AM + 1
                        vector[1, 12] = vector[0, 12] + 1;
                    }
                    //Comienzo de Nuevo ensamblaje, verificamos que haya un Motor y un Armazon
                    if (vector[0, 5] > 0 && vector[0, 9] > 0)
                    {
                        //Calculamos el tiempo ensamblaje

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

                    //Asigno el valor de inactividad del area Ensamblaje
                    double tiempoEnsambla = tiempoInactividadE(vector);
                    vector[1, 22] = tiempoEnsambla;

                    //Asigno el valor de inactividad del area Ruedas
                    double tiempoAreaRued = tiempoInactividadAR(vector);
                    vector[1, 23] = tiempoAreaRued;

                    //Asigno el valor de inactividad total
                    double tiempoAreaTot = tiempoInactividadTotal(vector);
                    vector[1, 24] = tiempoAreaTot;

                    //Asigno el valor de la Cola Maxima Motores
                    vector[1, 26] = colaMaximaMotores(vector);

                    //Asigno el valor de la Cola Maxima AM
                    vector[1, 28] = colaMaximaAM(vector);

                    //Asigno el valor de la Cola Maxima Ruedas
                    vector[1, 27] = colaMaximaRuedas(vector);

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

                        //Asigno el valor de inactividad del area Ensamblaje
                        double tiempoEnsambl = tiempoInactividadE(vector);
                        vector[1, 22] = tiempoEnsambl;

                        //Asigno el valor de inactividad del area Ruedas
                        double tiempoAreaRue = tiempoInactividadAR(vector);
                        vector[1, 23] = tiempoAreaRue;

                        //Asigno el valor de inactividad total
                        double tiempoAreaTo = tiempoInactividadTotal(vector);
                        vector[1, 24] = tiempoAreaTo;

                        //Asigno el valor de la Cola Maxima Motores
                        vector[1, 26] = colaMaximaMotores(vector);

                        //Asigno el valor de la Cola Maxima AM
                        vector[1, 28] = colaMaximaAM(vector);

                        //Asigno el valor de la Cola Maxima Ruedas
                        vector[1, 27] = colaMaximaRuedas(vector);

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

                        //Asigno estado ocupado al area de ruedas -- cambio xD
                        vector[1, 21] = 1;
                    }

                    //Asigno el valor de inactividad del area Ensamblaje
                    double tiempoEnsamb = tiempoInactividadE(vector);
                    vector[1, 22] = tiempoEnsamb;

                    //Asigno el valor de inactividad del area Ruedas
                    double tiempoAreaRu = tiempoInactividadAR(vector);
                    vector[1, 23] = tiempoAreaRu;

                    //Asigno el valor de inactividad total
                    double tiempoAreaT = tiempoInactividadTotal(vector);
                    vector[1, 24] = tiempoAreaT;

                    //Asigno el valor de la Cola Maxima Motores
                    vector[1, 26] = colaMaximaMotores(vector);

                    //Asigno el valor de la Cola Maxima AM
                    vector[1, 28] = colaMaximaAM(vector);

                    //Asigno el valor de la Cola Maxima Ruedas
                    vector[1, 27] = colaMaximaRuedas(vector);

                    break;

                default:
                    //Genero la fila 0, de la inicialización
                    for (int i = 0; i < vector.GetLength(1); i++)
                    {
                        vector[0, i] = 0;
                    }
                    vector[0, 0] = 1;
                    vector[0, 3] = 10.0001;
                    vector[0, 4] = vector[0, 3] + vector[0, 2];
                    vector[0, 6] = rnd.NextDouble();
                    vector[0, 7] = generador.Uniforme(30, 40, vector[0,6]);
                    vector[0, 8] = vector[0, 2] + vector[0, 7];
                    vector[0, 14] = rnd.NextDouble();
                    vector[0, 15] = rnd.NextDouble();
                    nuevosRandom = !nuevosRandom;
                    vector[0, 16] = generador.Normal(vector[0, 14], vector[0, 15], 70, 8);
                    vector[0, 17] = vector[0, 2] + vector[0, 16];

                    break;
            }

        }
        //Esta funcion recorre el vector de estado y devuelve el tiempo menor para determinar el proximo evento por venir
        private double[] buscarMenor(double[,] vector)
        {
            //Verificar que ignore los valores que no tienen tiempos o que son iguales al reloj actual, porque esos estan pasando en el momento

            //Configuramos el menor como la primera columna que chequear
            double menor = 10000000000;
            int posicion = 4;
            //Vector con la posicion y el valor menor
            double[] vectorMenor = { -1, menor };


            if (vector[0, 0] == 0)
            {
                vectorMenor[0] = 0;
                vectorMenor[1] = 0;
                return vectorMenor;
            }

            //El array contiene las posiciones de las columnas para chequear
            int[] numeros = { 4, 8, 11, 17, 20 };
            //Columnas que chequear
            //4 Proxima llegada A
            //8 Proxima llegada M
            //11 Proximo ensamblaje
            //17 Proxima llegada
            //20 Proximo triciclo
            for (int i = 4; i <= 20; i++)
            {
                if (numeros.Contains(i)) {
                    if (vector[0,i] < menor && vector[0,i] != 0 && vector[0,i] > vector[0,2])
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
            /* Posiciones de las columnas
            2=Reloj 
            16=Tiempo llegada R 
            17= Proxima llegada R 
            18= Stock R 
            19= Tiempo ensamblaje AR 
            20= Proximo Triciclo AR  
            21 = Estado AR            
            23= TI area rueda */

            double TIR = Convert.ToDouble(vector[0, 23]);

            int estadoEAnt = Convert.ToInt32(vector[0, 21]);
            //estado ensamblaje actual
            int estadoE = Convert.ToInt32(vector[1, 21]);
            //Reloj anterior
            double reloj0 = Convert.ToDouble(vector[0, 2]);
            //Reloj Actual
            double reloj1 = Convert.ToDouble(vector[1, 2]);
            //Tiempo improductividad anterior
            double TIRA = Convert.ToDouble(vector[0, 23]);
            // Ultimo fin de ensamblaje
            double UltimoFinEnsamblaje = Convert.ToDouble(vector[0, 20]);

            // Cuando me ocupo resto mi reloj actual con mi ultimo fin de ensamblaje
            //para obtener el tiempo que estuve inactivo
            if (estadoE == 1 && reloj1 >= UltimoFinEnsamblaje)
            {
                TIR = TIRA + (reloj1 - UltimoFinEnsamblaje);

                //Si mi ultimo ensamblaje valia 0 no acumulo
                if (UltimoFinEnsamblaje == 0)
                {
                    TIR = TIRA + (reloj1 - UltimoFinEnsamblaje) - reloj0;
                }
                //CHECK POINT
            }
            //Si mi estado actual y anterior valen 0 acumulo los tiempo improductivos
            if (estadoE == 0 && estadoEAnt == 0)
            {
                TIR = TIRA + reloj1 - reloj0;

            }

            return TIR;
            /* double TI = 0;

             //Tiempo de inactividad del Area de Ruedas
             int estadoEAct = Convert.ToInt32(vector[1, 21]);

             if (estadoEAct == 0)
             {
                 //Seria el TI de la fila anterior mas el reloj anterior menos el inicial
                 TI = vector[0, 23];

             }
             else
             {
                 //Seria el TI de la fila anterior, ya que esta ocupado.
                 TI = vector[0, 23] + vector[1, 2] - vector[0, 2];
             }

             return TI;*/

        }

        public double tiempoInactividadE(double[,] vector) 
        {
            double TI = Convert.ToDouble(vector[0,22]);

            //estado ensamblaje anterior
            int estadoEAnt = Convert.ToInt32(vector[0, 13]);
            //estado ensamblaje actual
            int estadoE = Convert.ToInt32(vector[1, 13]);   
            //Reloj anterior
            double reloj0 = Convert.ToDouble(vector[0, 2]);
            //Reloj Actual
            double reloj1 = Convert.ToDouble(vector[1, 2]);
            //Tiempo improductividad anterior
            double TIA = Convert.ToDouble(vector[0, 22]);
            // Ultimo fin de ensamblaje
            double UltimoFinEnsamblaje = Convert.ToDouble(vector[0, 11]);
            
            // Cuando me ocupo resto mi reloj actual con mi ultimo fin de ensamblaje
            //para obtener el tiempo que estuve inactivo
            if (estadoE == 1 && reloj1 >= UltimoFinEnsamblaje)            
            {
                TI =  TIA + (reloj1 - UltimoFinEnsamblaje);

                //Si mi ultimo ensamblaje valia 0 no acumulo
                if (UltimoFinEnsamblaje == 0)
                {
                    TI = TIA + (reloj1 - UltimoFinEnsamblaje) - reloj0;
                }
             //CHECK POINT
            }
            //Si mi estado actual y anterior valen 0 acumulo los tiempo improductivos
            if (estadoE == 0 && estadoEAnt == 0)
            {
                TI = TIA + reloj1 - reloj0;
               
            }

            

            return TI;
        }

        public double tiempoInactividadTotal(double[,] vector)
        {
            //Aca lo que hago es la sumatoria del TI de rueda y el TI de ensamblaje

            double tiRuedas = vector[1,23];
            double tiEnsamblaje = vector[1,22];

            double tit = tiRuedas + tiEnsamblaje;

            return tit;
        }

        //------------------------ COLAS MAXIMAS -----------------------------------
        public int colaMaximaMotores(double[,] vector)
        {
            //26=Cola Maxima Motores 
            //9 = Stock M

            int stockMotores_0 = Convert.ToInt32(vector[0,9]);
            int stockMotores_1 = Convert.ToInt32(vector[1,9]);
            int CM = Convert.ToInt32(vector[0,26]);

            if (stockMotores_1 > CM)
            {
                CM = stockMotores_1;
            }
            if (stockMotores_0 > CM)
            {
                CM = stockMotores_0;
            }
            
            return CM;
        }

        public int colaMaximaAM(double[,] vector)
        {
            //28= Cola maxima AM
            //12 = Stock AM

            int stockAM_0 = Convert.ToInt32(vector[0, 12]);
            int stockAM_1 = Convert.ToInt32(vector[1, 12]);
            int CM = Convert.ToInt32(vector[0, 28]);

            if (stockAM_1 > CM)
            {
                CM = stockAM_1;
            }
            if (stockAM_0 > CM)
            {
                CM = stockAM_0;
            }

            return CM;
        }

        public int colaMaximaRuedas(double[,] vector)
        {
            //27 = Cola Maxima Ruedas
            //18 = Stock R 

            int stockRuedas_0 = Convert.ToInt32(vector[0,18]);
            int stockRuedas_1 = Convert.ToInt32(vector[1,18]);
            int CM = Convert.ToInt32(vector[0,27]);

            if (stockRuedas_1 > CM)
            {
                CM = stockRuedas_1;
            }
            if (stockRuedas_0 > CM)
            {
                CM = stockRuedas_0;
            }

            return CM;
        }

        //----------------------------------------------------------------------------------------

        private void copiarFila(double [,] vs, int filaOrigen, int filaDestino)
        {

            for (int i = 0; i < vs.GetLength(1); i++)
            {
                vs[filaDestino, i] = vs[filaOrigen, i];
            }
        }

    }
}
