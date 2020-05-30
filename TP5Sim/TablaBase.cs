using System;
using System.Collections.Generic;
using System.Configuration.Assemblies;
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
        // 26=Cola Maxima Motores 27= Cola Maxima Ruedas 28= Cola maxima AM

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


                    //Copio las proximas llegadas
                    //Llegada Armazon
                    vector[1, 4] = vector[0, 4];
                    //Fin Ensamblaje
                    vector[1, 11] = vector[0, 11];
                    //Llegada Ruedas
                    vector[1, 17] = vector[0, 17];
                    //Fin Ensamblaje Ruedas
                    vector[1, 20] = vector[0, 20];


                    break;

                //Proximo ensamblaje
                case 11:

                    break;

                //Proxima llegada de ruedas
                case 17:

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

                    //Copio las proximas llegadas
                    //Llegada Armazon
                    vector[1, 4] = vector[0, 4];
                    //Fin Ensamblaje
                    vector[1, 8] = vector[0, 8];
                    //Llegada Ruedas
                    vector[1, 11] = vector[0, 11];
                    //Fin Ensamblaje Ruedas
                    vector[1, 17] = vector[0, 17];

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
    }
}
