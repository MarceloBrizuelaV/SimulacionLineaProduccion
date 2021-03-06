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

            ----Nuevas Columnas
            14= Inestable               (El tiempo donde el sistema para a ser inestable)
            15= Fin de purga            (Una vez iniciada la purga, cuando finaliza)
            16= Tiempo Remanente        (Controlar si esta haciendo algo, verificar el tiempo faltante y guardarlo para despues terminarlo)
            ----Nuevas Columnas

        17 14= RND1 R 
        18 15= RND2 R 
        19 16=Tiempo llegada R 
        20 17= Proxima llegada R 
        21 18= Stock R 
        22 19= Tiempo ensamblaje AR 
        23 20= Proximo Triciclo AR  
        24 21 = Estado AR
        25 22= TI area ensamblaje 
        26 23= TI area rueda 
        27 24= TI total 
        28 25= Cantidad de triciclos 
        29 26=Cola Maxima Motores 
        30 27= Cola Maxima Ruedas 
        31 28= Cola maxima AM
    */
        public double[,] generarVector() 
        {
            double[,] vector = new double[2, 32];

            return vector;
        }


        public void generarTabla(double[,] vector, double tiempoArmazon, double limiteMaxMotor, double limiteMinMotor, double mediaRuedas, double desviacionEstRuedas, double tiempoEnsamblajeAM, double tiempoEnsamblajeRuedas, double tiempoInterrupcion, double t50, double t70, double t100)
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

            double random = rnd.NextDouble();

            if (vector[0, 6] == random)
            {
                random = rnd.NextDouble();
            }
            
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
                                vector[1, 10] = tiempoEnsamblajeAM;
                                //Calculo tiempo proximo ensamblaje
                                vector[1, 11] = vector[1, 2] + vector[1, 10];
                                //Calculo el tiempo del proximo Armazon
                                vector[1, 4] = vector[1, 2] + tiempoArmazon;
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
                    vector[1, 25] = tiempoInactividadE(vector);

                    //Asigno el valor de inactividad del area Ruedas
                    vector[1, 26] = tiempoInactividadAR(vector);

                    //Asigno el valor de inactividad total
                    vector[1, 27] = tiempoInactividadTotal(vector);

                    //Asigno el valor de la Cola Maxima Motores
                    vector[1, 29] = colaMaximaMotores(vector);

                    //Asigno el valor de la Cola Maxima AM
                    vector[1, 31] = colaMaximaAM(vector);

                    //Asigno el valor de la Cola Maxima Ruedas
                    vector[1, 30] = colaMaximaRuedas(vector);

                    vector[1, 0] = vector[0, 0] + 1;


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
                        vector[1, 10] = tiempoEnsamblajeAM;
                        //Calculo la Proxima llegada E
                        vector[1, 11] = vector[1, 2] + vector[1, 10];

                        //Calculo el proximo armazon
                        vector[1, 3] = tiempoArmazon;
                        vector[1, 4] = vector[1,2] + tiempoArmazon;

                        //Resto los stock de Armazon y Motor
                        vector[1, 5] = vector[1, 5] - 1;
                        vector[1, 9] = vector[1, 9] - 1;

                    }

                    //Calculo la proxima llegada de motores
                    
                    int limiteInf = Convert.ToInt32(limiteMinMotor);
                    int limiteSup = Convert.ToInt32(limiteMaxMotor);
                    double tiempoLlegada = generador.Uniforme(limiteInf, limiteSup, random);

                    //Asigno RND
                    vector[1, 6] = random;
                    //Asigno Tiempo Llegada
                    vector[1, 7] = tiempoLlegada;
                    //Asigno Proxima Llegada
                    vector[1, 8] = tiempoLlegada + vector[1, 2];

                    //Asigno el valor de inactividad del area Ensamblaje
                    vector[1, 25] = tiempoInactividadE(vector);

                    //Asigno el valor de inactividad del area Ruedas
                    vector[1, 26] = tiempoInactividadAR(vector);

                    //Asigno el valor de inactividad total
                    vector[1, 27] = tiempoInactividadTotal(vector);

                    //Asigno el valor de la Cola Maxima Motores
                    vector[1, 29] = colaMaximaMotores(vector);

                    //Asigno el valor de la Cola Maxima AM
                    vector[1, 31] = colaMaximaAM(vector);

                    //Asigno el valor de la Cola Maxima Ruedas
                    vector[1, 30] = colaMaximaRuedas(vector);

                    vector[1, 0] = vector[0, 0] + 1;

                    break;
                    
                //Fin area ensamblaje
                case 11:
                    //Colocacion de Ruedas
                    //Esta el area de ruedas libre?

                    if (vector[0,24] == 0 && vector[0, 21] >= 3)
                    {
                        //Calcular Proximo Triciclo -- Tiempo de Reloj + 5 Minutos
                        vector[1, 23] = vector[1, 2] + tiempoEnsamblajeRuedas;
                        //Actualizar Stock de Ruedas -3
                        vector[1, 21] = vector[0, 21] - 3;
                        //Actualizar Fin_Area_Ruedas a Ocupado
                        vector[1, 24] = 1;
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
                        vector[1, 11] = vector[1, 2] + tiempoEnsamblajeAM;
                        //Cambiamos el estado del area de ensamblaje a Ocupado
                        vector[1, 13] = 1;

                        //Modificacion para intentar arreglar
                        vector[1, 5] = vector[0, 5] - 1;
                        vector[1, 3] = tiempoArmazon;
                        vector[1, 4] = vector[1, 2] + tiempoArmazon;

                    }
                    else
                    {
                        //Cambiamos el estado del area de ensamblaje a libre
                        vector[1, 13] = 0;
                        vector[1, 11] = 0;
                    }

                    //Asigno el valor de inactividad del area Ensamblaje
                    vector[1, 25] = tiempoInactividadE(vector);

                    //Asigno el valor de inactividad del area Ruedas
                    vector[1, 26] = tiempoInactividadAR(vector);

                    //Asigno el valor de inactividad total
                    vector[1, 27] = tiempoInactividadTotal(vector);

                    //Asigno el valor de la Cola Maxima Motores
                    vector[1, 29] = colaMaximaMotores(vector);

                    //Asigno el valor de la Cola Maxima AM
                    vector[1, 31] = colaMaximaAM(vector);

                    //Asigno el valor de la Cola Maxima Ruedas
                    vector[1, 30] = colaMaximaRuedas(vector);

                    vector[1, 0] = vector[0, 0] + 1;

                    break;


                //Evento inestabilidad
                case 14:
                    vector[1, 16] = vector[0, 11] - vector[1, 2];
                    vector[1, 11] = 0;
                    vector[1, 15] = Herramientas.TruncadoMarcelo(vector[1, 2] + tiempoInterrupcion, 4);
                    vector[1, 13] = 3;

                    
                    //Asigno el valor de inactividad del area Ensamblaje
                    vector[1, 25] = tiempoInactividadE(vector);

                    //Asigno el valor de inactividad del area Ruedas
                    vector[1, 26] = tiempoInactividadAR(vector);

                    //Asigno el valor de inactividad total
                    vector[1, 27] = tiempoInactividadTotal(vector);

                    //Asigno el valor de la Cola Maxima Motores
                    vector[1, 29] = colaMaximaMotores(vector);

                    //Asigno el valor de la Cola Maxima AM
                    vector[1, 31] = colaMaximaAM(vector);

                    //Asigno el valor de la Cola Maxima Ruedas
                    vector[1, 30] = colaMaximaRuedas(vector);

                    vector[1, 0] = vector[0, 0] + 1;
                    

                    break;


                //Evento de fin de purga
                case 15:
                    vector[1, 11] = Herramientas.TruncadoMarcelo(vector[1, 2] + vector[0, 16], 4);
                    vector[1, 16] = 0;
                    vector[1, 14] = Herramientas.TruncadoMarcelo(vector[1, 2] + probabilidadPurga(t50, t70, t100), 4);
                    vector[1, 15] = 0;
                    vector[1, 13] = 1;

                    
                    //Asigno el valor de inactividad del area Ensamblaje
                    vector[1, 25] = tiempoInactividadE(vector);

                    //Asigno el valor de inactividad del area Ruedas
                    vector[1, 26] = tiempoInactividadAR(vector);

                    //Asigno el valor de inactividad total
                    vector[1, 27] = tiempoInactividadTotal(vector);

                    //Asigno el valor de la Cola Maxima Motores
                    vector[1, 29] = colaMaximaMotores(vector);

                    //Asigno el valor de la Cola Maxima AM
                    vector[1, 31] = colaMaximaAM(vector);

                    //Asigno el valor de la Cola Maxima Ruedas
                    vector[1, 30] = colaMaximaRuedas(vector);

                    vector[1, 0] = vector[0, 0] + 1;

                   
                    break;

                //Proxima llegada de ruedas
                case 20:
                        if (nuevosRandom)
                        {
                            vector[1,17] = rnd.NextDouble();
                            vector[1,18] = rnd.NextDouble();

                            nuevosRandom = !nuevosRandom;
                        }
                        else
                        {
                            nuevosRandom = !nuevosRandom;
                        }
                        //Pide Ruedas
                        vector[1, 19] = generador.Normal(vector[1, 17], vector[1, 18], mediaRuedas, desviacionEstRuedas);
                        vector[1, 20] = vector[1, 2] + vector[1, 19];

                        //Agregamos las ruedas que pedimos
                        vector[1, 21] = vector[0, 21] + 20;

                        //Verificamos si puede armar triciclos (Si fin_Area_Ruedas esta libre)
                        if (vector[0,24] == 0)
                        {
                            //Esta libre y verificamos si hay conjuntos AM
                            if (vector[0,12] != 0)
                            {
                                //Restamos las 3 ruedas
                                vector[1, 21] = vector[1, 21] - 3;
                                //Restamos un conjunto AM
                                vector[1, 12] = vector[0, 12] - 1;
                                //Cambiamos estado Fin_Area_Ruedas
                                vector[1, 24] = 1;
                                //Colocamos tiempo de ensamblaje ruedas
                                vector[1, 22] = tiempoEnsamblajeRuedas;
                                //Calculamos el fin del ensamblaje
                                vector[1, 23] = vector[1, 2] + vector[1, 22];
                            }
                        }

                        //Asigno el valor de inactividad del area Ensamblaje
                        vector[1, 25] = tiempoInactividadE(vector);

                        //Asigno el valor de inactividad del area Ruedas
                        vector[1, 26] = tiempoInactividadAR(vector);

                        //Asigno el valor de inactividad total
                        vector[1, 27] = tiempoInactividadTotal(vector);

                        //Asigno el valor de la Cola Maxima Motores
                        vector[1, 29] = colaMaximaMotores(vector);

                        //Asigno el valor de la Cola Maxima AM
                        vector[1, 31] = colaMaximaAM(vector);

                        //Asigno el valor de la Cola Maxima Ruedas
                        vector[1, 30] = colaMaximaRuedas(vector);

                        vector[1, 0] = vector[0, 0] + 1;
                        break;

                //Fin Area Ruedas
                case 23:
                    //Asigno estado libre al area de ruedas
                    vector[1, 24] = 0;

                    //Sumo 1 a la columna cantidad de triciclos
                    vector[1, 28] = vector[1, 28] + 1;

                    //Reviso si hay ruedas disponibles y AM disponibles
                    if (vector[0, 21] >= 3 && vector[0, 12] > 0)
                    {

                        //Resto la cantidad de ruedas utilizadas
                        vector[1, 21] = vector[0, 21] - 3;


                        //CAMBIO PARA EL TP 6 - POSIBLE ERROR NO CONTROLADO

                        if (vector[0, 21] >= 3)
                        {
                            //Asigno tiempo llegada
                            vector[1, 22] = tiempoEnsamblajeRuedas;

                            //Asigno proxima llegada
                            vector[1, 23] = tiempoEnsamblajeRuedas + vector[1, 2];
                        }
                        
                        //Asigno estado ocupado al area de ruedas -- cambio xD
                        vector[1, 24] = 1;
                    }

                    /*otro cambio xd 
                     si mi estado actual es 0 (sin trabajar) no se cuando voy a largar el proximo triciclo
                     por lo tanto dejo el valor en 0 para que no afecte a los tiempos de improductividad
                     y se calcule correctamente el tiempo de inactividad del area de ruedas*/
                    if (vector[1, 24] == 0) 
                    {
                        vector[1, 23] = 0;
                    }


                    //Asigno el valor de inactividad del area Ensamblaje
                    vector[1, 25] = tiempoInactividadE(vector);

                    //Asigno el valor de inactividad del area Ruedas
                    vector[1, 26] = tiempoInactividadAR(vector);

                    //Asigno el valor de inactividad total
                    vector[1, 27] = tiempoInactividadTotal(vector);
                   
                    //Asigno el valor de la Cola Maxima Motores
                    vector[1, 29] = colaMaximaMotores(vector);

                    //Asigno el valor de la Cola Maxima AM
                    vector[1, 31] = colaMaximaAM(vector);

                    //Asigno el valor de la Cola Maxima Ruedas
                    vector[1, 30] = colaMaximaRuedas(vector);

                    vector[1, 0] = vector[0, 0] + 1;

                    break;

                //Evento de inicialización
                default:

                    //Genero la fila 0, de la inicialización
                    for (int i = 0; i < vector.GetLength(1); i++)
                    {
                        vector[0, i] = 0;
                    }
                    vector[0, 0] = 1;
                    vector[0, 3] = tiempoArmazon;
                    vector[0, 4] = vector[0, 3] + vector[0, 2];
                    vector[0, 6] = rnd.NextDouble();
                    vector[0, 7] = generador.Uniforme(Convert.ToInt32(limiteMinMotor), Convert.ToInt32(limiteMaxMotor), vector[0,6]);
                    vector[0, 8] = vector[0, 2] + vector[0, 7];
                    vector[0, 14] = vector[0, 2] + probabilidadPurga(t50, t70, t100);
                    vector[0, 17] = rnd.NextDouble();
                    vector[0, 18] = rnd.NextDouble();
                    nuevosRandom = !nuevosRandom;
                    vector[0, 19] = generador.Normal(vector[0, 17], vector[0, 18], mediaRuedas, desviacionEstRuedas);
                    vector[0, 20] = vector[0, 2] + vector[0, 19];

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
            int[] numeros = { 4, 8, 11, 14, 15, 20, 23 };
            //Columnas que chequear
            //4 Proxima llegada A
            //8 Proxima llegada M
            //11 Proximo ensamblaje
            //17 Proxima llegada
            //20 Proximo triciclo
            for (int i = 4; i <= 23; i++)
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


        private double probabilidadPurga(double _50Lleno, double _70Lleno, double _100Lleno) 
        {
            GeneradorVariables generador = new GeneradorVariables();
            Random random = new Random();
            double prob = generador.Uniforme(0, 1, random.NextDouble());
            double tiempo = 0;

            if (prob < 0.2)
            {
                tiempo = _50Lleno;
            }
            if (prob >= 0.2 && prob < 0.5)
            {
                tiempo = _70Lleno;
            }
            if (prob >= 0.5 && prob < 1)
            {
                tiempo = _100Lleno;
            }
            return tiempo;
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

            double TIR = Convert.ToDouble(vector[0, 26]);

            int estadoEAnt = Convert.ToInt32(vector[0, 24]);
            //estado ensamblaje actual
            int estadoE = Convert.ToInt32(vector[1, 24]);
            //Reloj anterior
            double reloj0 = Convert.ToDouble(vector[0, 2]);
            //Reloj Actual
            double reloj1 = Convert.ToDouble(vector[1, 2]);
            //Tiempo improductividad anterior
            double TIRA = Convert.ToDouble(vector[0, 26]);
            // Ultimo fin de ensamblaje
            double UltimoFinEnsamblaje = Convert.ToDouble(vector[0, 23]);

            // Cuando me ocupo resto mi reloj actual con mi ultimo fin de ensamblaje
            //para obtener el tiempo que estuve inactivo
            if (estadoE == 1 && reloj1 >= UltimoFinEnsamblaje && estadoEAnt == 0)
            {
                TIR = TIRA;
                // TIR = TIRA + (reloj1 - UltimoFinEnsamblaje);

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
                TIR = TIRA + (reloj1 - reloj0);

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
            double TI = Convert.ToDouble(vector[0,25]);

            //estado ensamblaje anterior
            int estadoEAnt = Convert.ToInt32(vector[0, 13]);
            //estado ensamblaje actual
            int estadoE = Convert.ToInt32(vector[1, 13]);   
            //Reloj anterior
            double reloj0 = Convert.ToDouble(vector[0, 2]);
            //Reloj Actual
            double reloj1 = Convert.ToDouble(vector[1, 2]);
            //Tiempo improductividad anterior
            double TIA = Convert.ToDouble(vector[0, 25]);
            // Ultimo fin de ensamblaje
            double UltimoFinEnsamblaje = Convert.ToDouble(vector[0, 11]);
            
            // Cuando me ocupo resto mi reloj actual con mi ultimo fin de ensamblaje
            //para obtener el tiempo que estuve inactivo
            if (estadoE == 1 && reloj1 >= UltimoFinEnsamblaje && estadoEAnt == 0)            
            {
                TI =  TIA + (reloj1 - reloj0);
                //TI =  TIA + (reloj1 - UltimoFinEnsamblaje);

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

            if (estadoE == 1 && estadoEAnt == 1 && reloj1 >= UltimoFinEnsamblaje) 
            {
                TI = TIA;
            }

            

            return TI;
        }

        public double tiempoInactividadTotal(double[,] vector)
        {
            //Aca lo que hago es la sumatoria del TI de rueda y el TI de ensamblaje

            double tiRuedas = vector[1,26];
            double tiEnsamblaje = vector[1,25];

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
            int CM = Convert.ToInt32(vector[0,29]);

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
            int CM = Convert.ToInt32(vector[0, 31]);

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

            int stockRuedas_0 = Convert.ToInt32(vector[0,21]);
            int stockRuedas_1 = Convert.ToInt32(vector[1,21]);
            int CM = Convert.ToInt32(vector[0,30]);

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

        public double[] calcularPorcentajes(System.Windows.Forms.DataGridView grilla) 
        {
            double valorGridEnsamblaje = Convert.ToDouble(grilla.Rows[grilla.Rows.Count-1].Cells[25].Value);
            double valorGridAreaRuedas = Convert.ToDouble(grilla.Rows[grilla.Rows.Count-1].Cells[26].Value);
            double reloj = Convert.ToDouble(grilla.Rows[grilla.Rows.Count-1].Cells[2].Value);

            double[] valores = new double[2];

            valores[0] = (valorGridEnsamblaje * 100) / reloj;
            valores[1] = (valorGridAreaRuedas * 100) / reloj;

            return valores;
            
        }

    }
}
